// Each projectile is assigned an id from a global counter
$ProjectileTrackerCounter = 0;

// Id is also stored in a global list index by projectile id.
// List is a growing string of $ProjectileTrackerCounter assigned ids
//$Projectile::trackIdList[%this]

function WeaponOrSomething::onFire(%player,%slot)
{
    if($ProjectileTrackerCounter > 10000) //Reset counter if it gets too big
        $ProjectileTrackerCounter = 0;
    %trackId = $ProjectileTrackerCounter;
    $ProjectileTrackerCounter++;
    
    $Projectile::trackIdList[%this] = $Projectile::trackIdList[%this] @ %trackId @" ";
    %proj = Projectile::spawnProjectile(SomeProjectileOrSomething,%playerTrans,%player,%playerVel);
    
    Projectile::TrackProjectile(%proj,%trackId,0.1,Player::getClient(%player));
}

function Projectile::TrackProjectile(%this,%trackId,%rate,%owner)
{ 
    if(%owner != "")
        $Projectile::tracking[%this,%trackId,Owner] = %owner;
    $Projectile::tracking[%this,%trackId,Pos] = Gamebase::getPosition(%this);
    $Projectile::tracking[%this,%trackId,Time] = getSimTime();
    schedule("Projectile::TrackProjectile("@%this@","@ %trackId @","@%rate@");",%rate,%this);
}


function SomeProjectileOrSomething::onRemove(%this)
{
    %oldestId = getWord($Projectile::trackIdList[%this],0);
    // Remove oldest from list
    $Projectile::trackIdList[%this] = Word::getSubWord($Projectile::trackIdList[%this],1,9999);
    
    %pos = $Projectile::tracking[%this,%oldestId,Pos];
    %clientId = $Projectile::tracking[%this,%trackId,Owner];
    
    //Do you code here. Can also use $Projectile::tracking[%this,%trackId,Time] if time checking is helpful
    
    //Blah blah
    
    //Clean up 
    $Projectile::tracking[%this,%trackId,Owner] = "";
    $Projectile::tracking[%this,%trackId,Pos] = "";
    $Projectile::tracking[%this,%trackId,Time] = "";
}