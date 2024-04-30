$ImpactDamageType		  = -1;
$LandingDamageType	  =  0;
$BulletDamageType      =  1;
$EnergyDamageType      =  2;
$PlasmaDamageType      =  3;
$ExplosionDamageType   =  4;
$ShrapnelDamageType    =  5;
$LaserDamageType       =  6;
$MortarDamageType      =  7;
$BlasterDamageType     =  8;
$ElectricityDamageType =  9;
$CrushDamageType       = 10;
$DebrisDamageType      = 11;
$MissileDamageType     = 12;
$MineDamageType        = 13;
$NullDamageType        = 14;
$SpellDamageType        = 15;
$DragonDamageType      = 16;
$AIProjDamageType        = 17;
$BladeBoltDamageType = 18;
$SlamDamageType = 19;
$StaffDamageType = 20;
$MeteorDamageType = 21;

$ProjectileTrackerCounter = 0;

function Projectile::startTracking(%ownerClient,%proj,%rate,%range)
{
    if($ProjectileTrackerCounter > 5000)
        $ProjectileTrackerCounter = 0;
    
    %trackId = $ProjectileTrackerCounter;
    $ProjectileTrackerCounter++;
    
    $Projectile::trackIdList[%proj] = $Projectile::trackIdList[%proj] @ %trackId @" ";
    
    Projectile::TrackProjectile(%proj,%trackId,%rate,%range,%ownerClient);
}

function Projectile::getTrackId(%this)
{
    %trackId = getWord($Projectile::trackIdList[%this],0);
    $Projectile::trackIdList[%this] = String::removeWords($Projectile::trackIdList[%this],%trackId);
    return %trackid;
}

function Projectile::TrackProjectile(%this,%trackId,%rate,%range,%owner)
{
    $Projectile::tracking[%this,%trackId,PrevPos] = $Projectile::tracking[%this,%trackId,Pos];
    $Projectile::tracking[%this,%trackId,PrevTime] = $Projectile::tracking[%this,%trackId,Time];
    if(%owner != "")
        $Projectile::tracking[%this,%trackId,Owner] = %owner;
    if(%range != "")
        $Projectile::tracking[%this,%trackid,Range] = %range;
    $Projectile::tracking[%this,%trackId,Pos] = Gamebase::getPosition(%this);
    $Projectile::tracking[%this,%trackId,Time] = getSimTime();
    //echo("Track Pos: "@ $Projectile::tracking[%this,%trackId,Pos]);
    schedule("Projectile::TrackProjectile("@%this@","@ %trackId @","@%rate@");",%rate,%this);
}

//function Projectile::TrackProjectile(%this,%rate,%owner)
//{
//    $Projectile::tracking[%this,PrevPos] = $Projectile::tracking[%this,Pos];
//    $Projectile::tracking[%this,PrevTime] = $Projectile::tracking[%this,Time];
//    if(%owner != "")
//        $Projectile::tracking[%this,Owner] = %owner;
//    $Projectile::tracking[%this,Pos] = Gamebase::getPosition(%this);
//    $Projectile::tracking[%this,Time] = getSimTime();
//    schedule("Projectile::TrackProjectile("@%this@","@%rate@");",%rate,%this);
//}

function Projectile::TrackCleanup(%this,%trackId)
{
    $Projectile::tracking[%this,%trackId,PrevPos] = "";
    $Projectile::tracking[%this,%trackId,PrevTime] = "";
    $Projectile::tracking[%this,%trackId,Pos] = "";
    $Projectile::tracking[%this,%trackId,Time] = "";
    $Projectile::tracking[%this,%trackId,Owner] = "";
    $Projectile::tracking[%this,%trackId,Range] = "";
}

function Projectile::PropagateTrack(%this,%trackId,%timeLess)
{
    %dir = Vector::sub($Projectile::tracking[%this,%trackId,Pos],$Projectile::tracking[%this,%trackId,PrevPos]);
    %dt = $Projectile::tracking[%this,%trackId,Time] - $Projectile::tracking[%this,%trackId,PrevTime];
    //Divide directon vector by time, to get velocity
    %vel = ScaleVector(%dir,1/%dt);
    
    // Get time difference between last recorded time and now.
    %newDt = getSimTime() - $Projectile::tracking[%this,%trackId,Time]; 
    
    // Multiply that by the calculated velocity to estimate how far the projectile has travelled
    %propDelta = ScaleVector(%vel,%newDt- %timeLess); // Take off timeless incase we want to back the propagation up a little.
    
    // Add that to the last recorded position to propagate to our estimated position
    return Vector::add($Projectile::tracking[%this,%trackId,Pos],%propDelta);
}

//--------------------------------------
BulletData FusionBolt
{
   bulletShapeName    = "fusionbolt.dts";
   explosionTag       = turretExp;
   mass               = 0.05;

   damageClass        = 0;       // 0 impact, 1, radius
   damageValue        = 0.25;
   damageType         = $EnergyDamageType;

   muzzleVelocity     = 50.0;
   totalTime          = 6.0;
   liveTime           = 4.0;
   isVisible          = True;

   rotationPeriod = 1.5;
};

LightningData ThornStaffBolt
{	bitmapName = "plasmabolt.bmp";
	damageType = $NullDamageType;
	boltLength = 40.0;
	coneAngle = 45.0;
	damagePerSec = 0.0;
	energyDrainPerSec = 0.0;
	segmentDivisions = 4;
	numSegments = 5;
	beamWidth = 0.3;  //10
	updateTime = 120;
	skipPercent = 0.5;
	displaceBias = 0.15;
	lightRange = 3.0;
	lightColor = { 0.25, 0.25, 0.85 };
	soundId = SoundElfFire;
};

LightningData DDBolt
{	bitmapName = "plasmabolt.bmp";
	damageType = $NullDamageType;
	boltLength = 40.0;
	coneAngle = 45.0;
	damagePerSec = 0;
	energyDrainPerSec = 0.0;
	segmentDivisions = 4;
	numSegments = 5;
	beamWidth = 0.3;  //10
	updateTime = 120;
	skipPercent = 0.5;
	displaceBias = 0.15;
	lightRange = 3.0;
	lightColor = { 0.25, 0.25, 0.85 };
	soundId = SoundElfFire;
};

//Thorn
//RocketData Thorn
//{ 
//	bulletShapeName = "bullet.dts"; 
//	explosionTag = bulletExp0; 
//	collisionRadius = 0.0; 
//	mass = 2.0;
//	damageClass = 1;
//	damageValue = 20;
//	damageType = $SpellDamageType;
//	explosionRadius = 6.0;
//	kickBackStrength = 0.0;
//	muzzleVelocity   = 50.0;
//	terminalVelocity = 50.0;
//	acceleration = 2.0;
//	totalTime = 3.1;
//	liveTime = 3.0;
//	lightRange = 20.0;
//	colors[0] = { 10.0, 0.75, 0.75 };
//	colors[1] = { 1.0, 0.25, 10.25 };
//	inheritedVelocityScale = 0.5;
//	trailType = 0;
//	trailString = "MortarTrail.dts";
//	smokeDist = 0;
//	soundId = SoundJetHeavy;
//	rotationPeriod = 0.1;
//};

BulletData ManaBoltProj
{
   bulletShapeName    = "enbolt.dts";
   explosionTag       = energyExp;

   damageClass        = 0;
   damageValue        = 26;
   damageType         = $SpellDamageType;

   muzzleVelocity     = 80.0;
   totalTime          = 2.0;
   liveTime           = 2.0;

   lightRange         = 3.0;
   lightColor         = { 0.25, 0.25, 1.0 };
   inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 0.5;
};

BulletData Thorn
{
   bulletShapeName    = "bullet.dts";
   explosionTag       = bulletExp0;

   damageClass        = 1;
   damageValue        = 26;
   damageType         = $SpellDamageType;
   explosionRadius = 3.0;
   kickBackStrength = 0.0;
   muzzleVelocity     = 90.0;
   totalTime          = 3.1;
   liveTime           = 3.0;

   lightRange         = 3.0;
   lightColor         = { 10.0, 0.75, 0.75 };
   inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 0.1;
};

BulletData Firebolt
{
   bulletShapeName    = "shotgunbolt.dts";
   explosionTag       = blasterExpBoom;

   damageClass        = 1;
   damageValue        = 45;
   damageType         = $SpellDamageType;
   explosionRadius = 6.0;
   kickBackStrength = 0.0;
   muzzleVelocity     = 50.0;
   totalTime          = 3.1;
   liveTime           = 3.0;

   lightRange         = 3.0;
   lightColor         = { 10.0, 0.75, 0.75 };
   inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 0.1;
};

//FireBall
RocketData Flare
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = bulletExp0; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 35; 
	damageType = $SpellDamageType; //Energy gets translated to spell damage type in onDamage
	explosionRadius = 8.0;
	kickBackStrength = 0.1;
	muzzleVelocity   = 5.0;
	terminalVelocity = 5.0;
	acceleration = 0.0;
	totalTime = 5;
	liveTime = 0.1;
	lightRange = 40.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0; // needs a trail =X
	trailString = "plasmaex.dts";
	smokeDist = 2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

//FireBall
RocketData Fireball
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = PlasmaEXP; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 35; 
	damageType = $SpellDamageType; //Energy gets translated to spell damage type in onDamage
	explosionRadius = 8.0;
	kickBackStrength = 0.1;
	muzzleVelocity   = 60.0;
	terminalVelocity = 80.0;
	acceleration = 2.0;
	totalTime = 3.1;
	liveTime = 3.0;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0; // needs a trail =X
	trailString = "plasmaex.dts";
	smokeDist = 2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

GrenadeData FireBomb
{
   bulletShapeName    = "mortar.dts";
   explosionTag       = FireBombExp;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.1;
   mass               = 5.0;
   elasticity         = 0.1;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 55;
   damageType         = $SpellDamageType;
   explosionRadius    = 10.0;
   kickBackStrength   = 3.0;
   maxLevelFlightDist = 75;
   totalTime          = 30.0;
   liveTime           = 2.0;
   projSpecialTime    = 0.01;

   inheritedVelocityScale = 0.5;
   smokeName              = "plasmaBolt.dts";
};

//IceSpike
RocketData IceSpike
{ 
	bulletShapeName = "bullet.dts";
	explosionTag = energyExp;
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 35; 
	damageType = $SpellDamageType;
	explosionRadius = 6.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 50.0;
	terminalVelocity = 90.0;
	acceleration = 10.0;
	totalTime = 2.0;
	liveTime = 1.8;
	lightRange = 5.0;
	colors[0] = { 15.0, 0.75, 0.75 };
	colors[1] = { 15.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 1;
	trailString = "tumult_large.dts";
	smokeDist = 9;
	soundId = SoundJetHeavy;
	rotationPeriod = 5.1;
};

RocketData IceStorm
{ 
	bulletShapeName = "fusionbolt.dts"; 
	explosionTag = turretExp; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 45/6; 
	damageType = $SpellDamageType;
	explosionRadius = 11.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 40.0;
	terminalVelocity = 40.0;
	acceleration = 2.0;
	totalTime = 2.0;
	liveTime = 1.8;
	lightRange = 20.0;
	colors[0] = { 1.0, 5.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "fusionbolt.dts";
	smokeDist = 20;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

GrenadeData Cloud
{
   bulletShapeName    = "shockwave_large.dts";
   explosionTag       = rocketExpBoom;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.3;
   mass               = 8.0;
   elasticity         = 0.1;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 85;
   damageType         = $SpellDamageType;
   explosionRadius    = 10.0;
   kickBackStrength   = 5.0;
   maxLevelFlightDist = 75;
   totalTime          = 30.0;
   liveTime           = 2.0;
   projSpecialTime    = 0.01;

   inheritedVelocityScale = 0.5;
   smokeName              = "plasmawall.dts";
};

//Melt
RocketData Melt
{ 
	bulletShapeName = "mortar.dts"; 
	explosionTag = grenadeEXPBoom; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 140; 
	damageType = $SpellDamageType;
	explosionRadius = 13.0;
	kickBackStrength = -2.0;
	muzzleVelocity   = 80.0;
	terminalVelocity = 120.0;
	acceleration = 2.0;
	totalTime = 10.0;
	liveTime = 9.6;
	lightRange = 20.0;
	colors = { 10.0, 0.75, 0.75 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 0.3;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
	trailLength = 70;
	trailWidth  = 5.8;
};

//Beam
LaserData sniperLaser
{
	laserBitmapName   = "forcefield.bmp";
	hitName           = "laserhit.dts";

	damageConversion  = 0.0;
	baseDamageType    = $SpellDamageType;

 	beamTime          = 3.5;

	lightRange        = 20.0;
	lightColor        = { 255, 0, 0 };

	detachFromShooter = false;
	hitSoundId        = NoSound;
};

//DimensionRift
RocketData DimensionRift
{ 
	bulletShapeName = "mortarex.dts"; 
	explosionTag = turretExpBoom; 
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 320; 
	damageType = $SpellDamageType;
	explosionRadius = 60.0;
	kickBackStrength = -30.0;
	muzzleVelocity   = 10.0;
	terminalVelocity = 90.0;
	acceleration = 2.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors = { 10.0, 2.75, 1.75 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "shockwave_large.dts";
	smokeDist = 1.35; //0.5;
	soundId = dimensionRiftExplosionLoopB;
	rotationPeriod = 0.1;
	trailLength = 20;
	trailWidth  = 10.8;
};
RocketData DimensionRift2
{ 
	bulletShapeName = ""; 
	explosionTag = LargeShockwaveBoom; 
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 0.0; 
	damageType = $NullDamageType;
	explosionRadius = 0.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 10.0;
	terminalVelocity = 90.0;
	acceleration = 2.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors[0] = { 5.0, 30.75, 1.75 };
	colors[1] = { 16.0, 6.25, 0.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "shockwave.dts";
	smokeDist = 0.5;
	soundId = dimensionRiftExplosionLoopA;
	rotationPeriod = 0.01;
	trailLength = 30;
	trailWidth  = 0.8;
};
RocketData DimensionRift3
{ 
	bulletShapeName = ""; 
	explosionTag = energyExpBoom;
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 0.0; 
	damageType = $NullDamageType;
	explosionRadius = 0.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 10.0;
	terminalVelocity = 90.0;
	acceleration = 2.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors[0] = { 5.0, 30.75, 1.75 };
	colors[1] = { 16.0, 6.25, 0.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0;
	trailString = "";
	smokeDist = 0.5;
	soundId = dimensionRiftExplosionLoopB;
	rotationPeriod = 0.01;
	trailLength = 30;
	trailWidth  = 0.8;
};
RocketData DimensionRift4
{ 
	bulletShapeName = ""; 
	explosionTag = energyExpBoom;
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 0.0; 
	damageType = $NullDamageType;
	explosionRadius = 0.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 10.0;
	terminalVelocity = 90.0;
	acceleration = 2.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors[0] = { 5.0, 30.75, 1.75 };
	colors[1] = { 16.0, 6.25, 0.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0;
	trailString = "";
	smokeDist = 0.5;
	soundId = dimensionRiftExplosionLoopB;
	rotationPeriod = 0.01;
	trailLength = 30;
	trailWidth  = 0.8;
};

RocketData Fireblast
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = fireBlastExpBoom; 
	collisionRadius = 0.0; //Having a collision radius on non-grenades make them not collide.  I don't know why, but okay.
	mass = 2.0;
	damageClass = 1;
	damageValue = 240; 
	damageType = $SpellDamageType;
	explosionRadius = 85;
	kickBackStrength = 500;
	muzzleVelocity   = 60.0;
	terminalVelocity = 80.0;
	acceleration = 2.0;
	totalTime = 8.1;
	liveTime = 8.0;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.4;
};

RocketData FireblastInvis
{ 
	bulletShapeName = "invisable.dts"; 
	explosionTag = LargeShockwaveBoom; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 0; 
	damageType = $NullDamageType;
	explosionRadius = 20.0;
	kickBackStrength = 250;
	muzzleVelocity   = 60.0;
	terminalVelocity = 80.0;
	acceleration = 2.0;
	totalTime = 8.1;
	liveTime = 8.0;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 8;
	soundId = NoSound;
	rotationPeriod = 0.1;
};

RocketData Meteor
{ 
	bulletShapeName = ""; 
	explosionTag = LargeShockwaveBoom; 
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 1.8; 
	damageType = $MeteorDamageType;
	explosionRadius = 250.0;
	kickBackStrength = 500.0;
	muzzleVelocity   = 175.0;
	terminalVelocity = 7000.0;
	acceleration = 200.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors[0] = { 80, 30.75, 1.75 };
	colors[1] = { 70.0, 6.25, 0.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "fiery.dts";
	smokeDist = 0.5;
	soundId = dimensionRiftExplosionLoopA;
	rotationPeriod = 0.01;
	trailLength = 30;
	trailWidth  = 0.8;
};

GrenadeData MeteorChunkDebris
{
   bulletShapeName    = "ruby.dts";
   explosionTag       = FireBombExp;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.1;
   mass               = 5.0;
   elasticity         = 0.1;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 1;
   damageType         = $MeteorDamageType;
   explosionRadius    = 80.0;
   kickBackStrength   = 100.0;
   maxLevelFlightDist = 175;
   totalTime          = 30.0;
   liveTime           = 1.0;
   projSpecialTime    = 0.01;

   inheritedVelocityScale = 0.5;
   smokeName              = "fiery.dts";
};

function BombSpread(%objpos)
{	%obj = newObject("","Mine","bomba");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 5.0";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombb");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 15.0";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombc");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombd");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombe");
	addToSet("MissionCleanup", %obj);
	%padd = "10.0 0 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombe");
	addToSet("MissionCleanup", %obj);
	%padd = "-10.0 0 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombe");
	addToSet("MissionCleanup", %obj);
	%padd = "0 10. 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombe");
	addToSet("MissionCleanup", %obj);
	%padd = "0 -10.0 25";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombe");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 35";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);

	%obj = newObject("","Mine","bombf");
	addToSet("MissionCleanup", %obj);
	%padd = "0 0 15";
	%pos = Vector::add(%objpos, %padd);
	GameBase::setPosition(%obj, %pos);
}

RocketData Meteor2
{ 
	bulletShapeName = ""; 
	explosionTag = LargeShockwaveBoom; 
	collisionRadius = 0.0; 
	mass = 0.5;
	damageClass = 1;
	damageValue = 2.2; 
	damageType = $MeteorDamageType;
	explosionRadius = 100.0;
	kickBackStrength = 200.0;
	muzzleVelocity   = 175.0;
	terminalVelocity = 7000.0;
	acceleration = 200.0;
	totalTime = 80.0;
	liveTime = 80.6;
	lightRange = 80.0;
	colors[0] = { 80, 30.75, 1.75 };
	colors[1] = { 70.0, 6.25, 0.25 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 0.75; //0.5
	soundId = SoundJetHeavy;
	rotationPeriod = 0.01;
	trailLength = 120; //30
	trailWidth  = 0.8;
};

MineData Bomba
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = grenadeExp;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 100;
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bomba::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.1,%this);
}

MineData Bombb
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = grenadeExp;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 100;
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bombb::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.1,%this);
}

MineData Bombc
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = grenadeExp;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 100;
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bombc::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.25,%this);
}

MineData Bombd
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = GrenadeExp;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 100;
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bombd::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.5,%this);
}

MineData Bombe
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = mortarExp;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 100;
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bombe::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.75,%this);
}

MineData Bombf
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = LargeShockwave;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $ShrapnelDamageType;
	kickBackStrength = 0; //200
	triggerRadius = 0.5;
	maxDamage = 1.5;
};

function Bombf::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.375,%this);
}


MineData ShockBomb
{
	mass = 5.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Bomblet";
	shapeFile = "force";
	shadowDetailMask = 4;
	explosionId = LargeShockwaveBoom;
	explosionRadius = 25.0;
	damageValue = 0.5;
	damageType = $NullDamageType;
	kickBackStrength = 0; //200
	triggerRadius = 0.5;
	maxDamage = 1.5;
};
function ShockBomb::onAdd(%this)
{
	schedule("Mine::Detonate(" @ %this @ ");",0.1,%this);
}

RocketData MeteorCrystalBeaconEffect
{ 
	bulletShapeName = "shockwave_large.dts"; 
	explosionTag = turretExp; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 0; 
	damageType = $NullDamageType;
	explosionRadius = 11.0;
	kickBackStrength = 0.0;
	muzzleVelocity   = 10.0;
	terminalVelocity = 10.0;
	acceleration = 0.0;
	totalTime = 60.0;
	liveTime = 1.6;
	lightRange = 30.0;
	colors[0] = { 50.0, 5.75, 0 };
	colors[1] = { 50.0, 0.25, 0 };
	inheritedVelocityScale = 0.5;
	trailType = 1;
	soundId = PortalLoop3;
	rotationPeriod = 0.5;
    trailLength = 600;
	trailWidth  = 1;
};

//SunFlare
RocketData SunFlare
{ 
	bulletShapeName = "mortar.dts"; 
	explosionTag = grenadeEXPBoom; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 140; 
	damageType = $SpellDamageType;
	explosionRadius = 13.0;
	kickBackStrength = -2.0;
	muzzleVelocity   = 200.0;
	terminalVelocity = 200.0;
	acceleration = 0;
	totalTime = 5.0;
	liveTime = 5.0; //10.0;
	lightRange = 20.0;
	colors = { 10.0, 0.75, 0.75 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 0.3;
	soundId = SoundScreamingLoop;
	rotationPeriod = 0.4;
	trailLength = 70;
	trailWidth  = 5.8;
};




RocketData DragonFireball
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = plasmaExpBoom; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 70; 
	damageType = $DragonDamageType; //Energy gets translated to spell damage type in onDamage
	explosionRadius = 8.0;
	kickBackStrength = 0.1;
	muzzleVelocity   = 100.0;
	terminalVelocity = 120.0;
	acceleration = 15.0;
	totalTime = 3.1;
	liveTime = 3.0;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0; // needs a trail =X
	trailString = "plasmaex.dts";
	smokeDist = 2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

RocketData DragonBlast
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = BigRedExp; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 300; 
	damageType = $DragonDamageType;
	explosionRadius = 13.0;
	kickBackStrength = -2.0;
	muzzleVelocity   = 80.0;
	terminalVelocity = 120.0;
	acceleration = 2.0;
	totalTime = 10.0;
	liveTime = 9.6;
	lightRange = 20.0;
	colors = { 10.0, 0.75, 0.75 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 0.3;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
	trailLength = 70;
	trailWidth  = 5.8;
};

ExplosionData MageBoltExp
{
	shapeName = "bluex.dts";
	soundId = rocketExplosion;
	faceCamera = true;
	randomSpin = true;
	hasLight = true;
	lightRange = 8.0;
	timeScale = 1.5;
	timeZero = 0.250;
	timeOne = 0.850;
	colors[0] = { 0.4, 0.4, 1.0 };
	colors[1] = { 1.0, 1.0, 1.0 };
	colors[2] = { 1.0, 0.95, 1.0 };
	radFactors = { 0.5, 1.0, 1.0 };
};

RocketData MageBoltTail
{
	//bulletShapeName = "discb.dts";
    bulletShapeName = "newproj.dts"; //"AUTO_MAGIC.dts";
	explosionTag = MageBoltExp; //energyExp
	collisionRadius = 0.0;
	mass = 2.0;
	damageClass = 1; // 0 impact, 1, radius
	damageValue = 75;
	damageType = $SpellDamageType;
	explosionRadius = 7.5;
	kickBackStrength = 150.0;
	muzzleVelocity = 80.0;
	terminalVelocity = 80.0;
	acceleration = 0.0;
	totalTime = 3.1;
	liveTime = 3.0;
	lightRange = 5.0;
	lightColor = { 0.4, 0.4, 1.0 };
	inheritedVelocityScale = 0.5;
	// rocket specific
	trailType = 1;
	trailLength = 28;
	trailWidth = 1.2;
	soundId = SoundDiscSpin;
};

RocketData BladeBoltTail
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = MageBoltExp; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 75; 
	damageType = $BladeBoltDamageType;
	explosionRadius = 8.0;
	kickBackStrength = 150.0;
	muzzleVelocity   = 60.0;
	terminalVelocity = 80.0;
	acceleration = 2.0;
	totalTime = 3.1;
	liveTime = 3.0;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 1;
	trailLength = 28;
	trailWidth = 1.2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

RocketData UberBossRainProj
{
    bulletShapeName = "redorb.dts"; 
	explosionTag = BigRedExp; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 100; 
	damageType = $AIProjDamageType;
	explosionRadius = 13.0;
	kickBackStrength = 2.0;
	muzzleVelocity   = 80.0;
	terminalVelocity = 120.0;
	acceleration = 2.0;
	totalTime = 10.0;
	liveTime = 9.6;
	lightRange = 20.0;
	colors = { 10.0, 0.75, 0.75 };
	inheritedVelocityScale = 0.5;
	trailType = 2;
	trailString = "plasmaex.dts";
	smokeDist = 0.3;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
	trailLength = 70;
	trailWidth  = 5.8;
};

BulletData TrueShotArrow
{
   bulletShapeName    = "tracer.dts";
   explosionTag       = arrowExp0;

   damageClass        = 0;
   damageValue        = 1;
   damageType         = $MissileDamageType;

   muzzleVelocity     = 200.0;
   totalTime          = 4.0;
   liveTime           = 4.0;

   lightRange         = 3.0;
   lightColor         = { 0.25, 0.25, 1.0 };
   //inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 1;
};


BulletData BlueStaffBolt
{
   bulletShapeName    = "enbolt.dts";
   explosionTag       = energyExp;

   damageClass        = 0;
   damageValue        = 1;
   damageType         = $StaffDamageType;

   muzzleVelocity     = 80.0;
   totalTime          = 2.0;
   liveTime           = 2.0;

   lightRange         = 3.0;
   lightColor         = { 0.25, 0.25, 1.0 };
   inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 1;
};

//BulletData FireBallBolt
//{
//   bulletShapeName    = "PlasmaBolt.dts";
//   explosionTag       = plasmaExpBoom;
//
//   damageClass        = 1;
//   explosionRadius    = 8.0;
//   kickBackStrength   = 0.1;
//   damageValue        = 1;
//   damageType         = $StaffDamageType;
//
//   muzzleVelocity     = 80.0;
//   totalTime          = 2.0;
//   liveTime           = 0.1;
//
//   lightRange         = 3.0;
//   lightColor         = { 0.25, 0.25, 1.0 };
//   inheritedVelocityScale = 0.5;
//   isVisible          = True;
//
//   rotationPeriod = 1;
//};

RocketData FireBallBolt
{ 
	bulletShapeName = "PlasmaBolt.dts"; 
	explosionTag = PlasmaEXPBoom; 
	collisionRadius = 0.0; 
	mass = 2.0;
	damageClass = 1;
	damageValue = 1; 
	damageType = $StaffDamageType;
	explosionRadius = 7.0;
	kickBackStrength = 0.1;
	muzzleVelocity   = 80.0;
	terminalVelocity = 80.0;
	acceleration = 2.0;
	totalTime = 2.0;
	liveTime = 1.9;
	lightRange = 20.0;
	colors[0] = { 10.0, 0.75, 0.75 };
	colors[1] = { 1.0, 0.25, 10.25 };
	inheritedVelocityScale = 0.5;
	trailType = 0; // needs a trail =X
	trailString = "plasmaex.dts";
	smokeDist = 2;
	soundId = SoundJetHeavy;
	rotationPeriod = 0.1;
};

RepairEffectData HealBolt
{
	bitmapName = "lightningNew.bmp";//"repairadd.bmp";
	boltLength = 7.5;
	segmentDivisions = 4;
	beamWidth = 0.125;
	updateTime = 450;
	skipPercent = 0.6;
	displaceBias = 0.15;
	lightRange = 3.0;
	lightColor = { 0.85, 0.25, 0.25 };
};

//--------------------------------------
BulletData MiniFusionBolt
{
   bulletShapeName    = "enbolt.dts";
   explosionTag       = energyExp;

   damageClass        = 0;
   damageValue        = 0.1;
   damageType         = $EnergyDamageType;

   muzzleVelocity     = 80.0;
   totalTime          = 4.0;
   liveTime           = 2.0;

   lightRange         = 3.0;
   lightColor         = { 0.25, 0.25, 1.0 };
   //inheritedVelocityScale = 0.5;
   isVisible          = True;

   rotationPeriod = 1;
};
function MiniFusionBolt::onAdd(%this)
{

}




//--------------------------------------
GrenadeData MortarTurretShell
{
   bulletShapeName    = "mortar.dts";
   explosionTag       = mortarExp;
   collideWithOwner   = True;
   ownerGraceMS       = 400;
   collisionRadius    = 1.0;
   mass               = 5.0;
   elasticity         = 0.1;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 1.32;
   damageType         = $NullDamageType;

   explosionRadius    = 30.0;
   kickBackStrength   = 250.0;
   maxLevelFlightDist = 400;
   totalTime          = 1000.0;
   liveTime           = 2.0;
   projSpecialTime    = 0.05;

   inheritedVelocityScale = 0.5;
   smokeName              = "mortartrail.dts";
};

//--------------------------------------
RocketData FlierRocket
{
   bulletShapeName  = "rocket.dts";
   explosionTag     = rocketExp;
   collisionRadius  = 0.0;
   mass             = 2.0;

   damageClass      = 1;       // 0 impact, 1, radius
   damageValue      = 0.5;
   damageType       = $MissileDamageType;

   explosionRadius  = 9.5;
   kickBackStrength = 250.0;
   muzzleVelocity   = 65.0;
   terminalVelocity = 80.0;
   acceleration     = 5.0;
   totalTime        = 10.0;
   liveTime         = 11.0;
   lightRange       = 5.0;
   lightColor       = { 1.0, 0.7, 0.5 };
   //inheritedVelocityScale = 0.5;

   // rocket specific
   trailType   = 2;                // smoke trail
   trailString = "rsmoke.dts";
   smokeDist   = 1.8;

   soundId = SoundJetHeavy;
};

//--------------------------------------
SeekingMissileData TurretMissile
{
   bulletShapeName = "rocket.dts";
   explosionTag    = rocketExp;
   collisionRadius = 0.0;
   mass            = 2.0;

   damageClass      = 1;       // 0 impact, 1, radius
   damageValue      = 0.5;
   damageType       = $MissileDamageType;
   explosionRadius  = 9.5;
   kickBackStrength = 175.0;

   muzzleVelocity    = 72.0;
   totalTime         = 10;
   liveTime          = 10;
   seekingTurningRadius    = 9;
   nonSeekingTurningRadius = 75.0;
   proximityDist     = 1.5;
   smokeDist         = 1.75;

   lightRange       = 5.0;
   lightColor       = { 0.4, 0.4, 1.0 };

   inheritedVelocityScale = 0.5;

   soundId = SoundJetHeavy;
};

LaserData sniperLaser
{
	laserBitmapName   = "forcefield.bmp";
	hitName           = "laserhit.dts";

	damageConversion  = 0.0;
	baseDamageType    = $LaserDamageType;

 	beamTime          = 1.5;

	lightRange        = 10.0;
	lightColor        = { 0.2, 0.2, 1.0 };

	detachFromShooter = false;
	hitSoundId        = NoSound;
};

function SeekingMissile::updateTargetPercentage(%target)
{
	dbecho($dbechoMode, "SeekingMissile::updateTargetPercentage(" @ %target @ ")");

	return GameBase::virtual(%target, "getHeatFactor");
}

LightningData turretCharge
{
   bitmapName       = "lightningNew.bmp";

   damageType       = $ElectricityDamageType;
   boltLength       = 40.0;
   coneAngle        = 35.0;
   damagePerSec      = 0.06;
   energyDrainPerSec = 60.0;
   segmentDivisions = 4;
   numSegments      = 5;
   beamWidth        = 0.125;

   updateTime   = 120;
   skipPercent  = 0.5;
   displaceBias = 0.15;

   lightRange = 3.0;
   lightColor = { 0.25, 0.25, 0.85 };

   soundId = SoundELFFire;
};

function Lightning::damageTarget(%target, %timeSlice, %damPerSec, %enDrainPerSec, %pos, %vec, %mom, %shooterId)
{
	dbecho($dbechoMode, "Lightning::damageTarget(" @ %target @ ", " @ %timeSlice @ ", " @ %damPerSec @ ", " @ %enDrainPerSec @ ", " @ %pos @ ", " @ %vec @ ", " @ %mom @ ", " @ %shooterId @ ")");

   %damVal = %timeSlice * %damPerSec;
   %enVal  = %timeSlice * %enDrainPerSec;

   GameBase::applyDamage(%target, $ElectricityDamageType, %damVal, %pos, %vec, %mom, %shooterId);

   %energy = GameBase::getEnergy(%target);
   %energy = %energy - %enVal;
   if (%energy < 0) {
      %energy = 0;
   }
   GameBase::setEnergy(%target, %energy);
}
