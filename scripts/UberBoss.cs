
$UberBoss::triggerRange = 30;

$UberBoss::ProxyRange = 60;

$UberBoss::StandWaitingSetup = 0;
$UberBoss::StandWaitingMoveToPos = 1;
$UberBoss::StandWaitingWaitForTarget = 2;
$UberBoss::StandWaitingTeleportDelay = 90;

$UberBurstScale = 900;
$SkillType[UBBlast] = $BludgeoningDamageType;
$UberBoss::LoseTargetDist = 350;

$UberBoss::CombatStateAttacking = 0;

function UberBoss::TestSpawn()
{
    UberBoss::Spawn(nameToId("MissionGroup/Realm0/UberArena/SpawnMarker"));
}

function UberBoss::TestSpawnLOS(%client)
{
    %pl = Client::getControlObject(%client);
    $los::position = "";
    %los = Gamebase::getLOSInfo(%pl,500);
    if(%los)
    {
        UberBoss::Spawn(nameToId("MissionGroup/Realm0/UberArena/SpawnMarker"),$los::position);
    }
}

function UberBoss::Spawn(%marker,%pos)
{
    %name = "UberBrute";
    %n = getAInumber();
    %aiName = %name@%n;
    
    if(%pos == "")
        %pos = Gamebase::getPosition(%marker);
    %rot = Gamebase::getRotation(%marker);

    AI::otherCreate(%aiName,%aiName,UberArmor,%pos,%rot);
    setAINumber(%aiName,%n);
    %aiId = AI::getId(%aiName);
    GameBase::setTeam(%aiId,7);
    
    UberBoss::setupAIDefaults(%aiName);
    UberBoss::GiveStuff(%aiId);
    
    AI::CallbackDied(%aiName,UberBoss::doDeath);
    $ZoneCleanupProtected[%aiId] = true;
    storeData(%aiId,"ubMarker",%marker);
    storeData(%aiId,"isUberBoss",true);
    
    storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingSetup);
}

function UberBoss::doDeath(%aiName)
{
    %aiId = AI::getId(%aiName);
    if(fetchData(%aiId,"ubCombatStarted"))
    {
        UberBoss::stopMusic(%aiId);
    }
    
    UberBoss::clearVars(%aiId);
    storeData(%aiId,"ubMarker","");
    storeData(%aiId,"isUberBoss","");
    $ZoneCleanupProtected[%aiId] = false;
}

function UberBoss::clearVars(%aiId)
{
    storeData(%aiId,"ubStandWaiting","");
    storeData(%aiId,"ubCombatReady","");
    storeData(%aiId,"currentTarget","");
    storeData(%aiId,"ubCombatStarted","");
    storeData(%aiId,"ubStandWaitingTime","");
    storeData(%aiId,"ubCombatState","");
}

function UberBoss::DoReset(%aiName,%aiId)
{
    UberBoss::clearVars(%aiId);
    AI::newDirectiveRemove(%aiName, 99);
    AI::newDirectiveRemove(%aiName, 100);
    storeData(%aiId,"ubStandWaiting",0);
    Player::unmountItem(Client::getOwnedObject(%aiId),0);
}

function UberBoss::switchTarget(%aiName,%aiId,%targetClient)
{
    storeData(%aiId,"currentTarget",%targetClient);
    AI::newDirectiveFollow(%aiName,%targetClient,0,99);
    AI::setVar(%aiName, seekOff, Vector::getFromRot(Gamebase::getRotation(%targetClient),GetRange(WarMaul)-1));
}

function UberBoss::startMusic(%aiId)
{
    //music_antaris_quake3.wav
    %zone = fetchData(%aiId,"zone");
    storeData(%aiId,"musicZone",%zone);
    %zid = $Zone::FolderToID[%zone];
    $Zone::Music[%zid,1] = "music_antaris_quake3.wav";
    $Zone::MusicTicks[%zid,1] = 48;
    
    for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
    {
        if(!Player::isAIControlled(%cl) && fetchData(%cl, "zone") == %zone)
        {
            //Repack
            if(%cl.repack)
            {
                remoteeval(%cl,RSound,3);
                %cl.MusicTicksLeft = 0;
            }
        }
    }
}

function UberBoss::stopMusic(%aiId)
{
    %zz = fetchData(%aiId,"musicZone");
    %zid = $Zone::FolderToID[%zz];
    $Zone::Music[%zid,1] = "";
    $Zone::MusicTicks[%zid,1] = "";
    
    for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
    {
        if(!Player::isAIControlled(%cl) && fetchData(%cl, "zone") == %zone)
        {
            //Repack zone exit
            if(%cl.repack)
            {
                remoteeval(%cl,RSound,3);
                %cl.MusicTicksLeft = 0;
            }
        }
    }
}

function UberBoss::Periodic(%aiName)
{
    dbecho($dbechoMode, "UberBoss::Periodic(" @ %aiName @ ")");
    %aiId = AI::getId(%aiName);
    
    if(fetchData(%aiId, "dumbAIflag") || fetchData(%aiId, "frozen"))
		return;
        
    %aiTeam = GameBase::getTeam(%aiId);
	%aiPos = GameBase::getPosition(%aiId);
    %marker = fetchData(%aiId,"ubMarker");
    %standWaiting = fetchData(%aiId,"ubStandWaiting");
    %combatStarted = fetchData(%aiId,"ubCombatStarted");
    if(%standWaiting != "")
    {
        if(%standWaiting == 0)
        {
            setHP(%aiId);
            AI::newDirectiveWaypoint(%aiName, Gamebase::getPosition(%marker), 100);
            storeData(%aiId,"ubCombatReady",false);
            //AI::DirectiveCallback1(%aiName,UberBoss::setReadyFlag,70); //This might be causing a syntax error.  Not sure yet
            AI::SetVar(%aiName, spotDist, 500);
            storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingMoveToPos);
            storeData(%aiId,"ubStandWaitingTime",getSimTime());
        }
        else if(%standWaiting == $UberBoss::StandWaitingMoveToPos)
        {
            %dt = getSimTime() - fetchData(%aiId,"ubStandWaitingTime");
            
            // Boss must be stuck.  Teleport them.
            if(%dt > $UberBoss::StandWaitingTeleportDelay)
                UberBoss::teleportToMarker(%aiId);
            
            if(Vector::getDistance(%aiPos,Gamebase::getPosition(%marker)) < 5)
            {
                UberBoss::setReadyFlag(%aiName);
            }
        }
        
        // Search for close enough combatant
        if(fetchData(%aiId,"ubCombatReady"))
        {
            %range = 2*$UberBoss::triggerRange;
            %set = newObject("set", SimSet);
            %n = containerBoxFillSet(%set, $SimPlayerObjectType, %aiPos, %range, %range, %range, 0);
            for(%i = 0; %i < Group::objectCount(%set); %i++)
            {
                %id = Player::getClient(Group::getObject(%set, %i));
                if(GameBase::getTeam(%id) != %aiTeam && !fetchData(%id, "invisible"))
                {
                    storeData(%aiId,"currentTarget",%id);
                    UberBoss::StartMusic(%aiId);
                    UberBoss::PreCombat(%aiName,%aiId);
                }
            }
            deleteObject(%set);
        }
    }
    else if(%combatStarted)
    {
        //Bash check
        if(fetchData(%aiId,"blockBash") == "" && OddsAre(4))
            storeData(%aiId,"NextHitBash",true);
            
        %state = fetchData(%aiId,"ubCombatState");
        if(%state == $UberBoss::CombatStateAttacking)
        {
            %range = 2*$UberBoss::ProxyRange;
            %set = newObject("set", SimSet);
            %playerCnt = containerBoxFillSet(%set, $SimPlayerObjectType, %aiPos, %range, %range, %range, 0);
            %ctarget = fetchData(%aiId,"currentTarget");
            %tgtCnt = 0;
            %targetDist = 0;
            %newTarget = false;
            %closest = 500000;
            %closestId = "";
            
            %targetDist = Vector::getDistance(%aiPos, GameBase::getPosition(%ctarget));
            
            for(%i = 0; %i < %playerCnt; %i++)
            {
                %id = Player::getClient(Group::getObject(%set, %i));
                
                if(GameBase::getTeam(%id) != %aiTeam && !fetchData(%id, "invisible"))
                {
                    %tgtCnt++;
                    if(%dist < %closest)
                    {
                        %closest = %dist;
                        %closestId = %id;
                        
                    }
                    if(%id != %ctarget)
                    {
                        if(OddsAre(5))
                        {
                            UberBoss::switchTarget(%aiName,%aiId,%id);
                            %newTarget = true;
                        }
                        
                    }
                }
            }
            if(!%newTarget)
            {
                //Check if the target is still around
                echo(%targetDist);
                if(%targetDist >= $UberBoss::LoseTargetDist)
                {
                    if(%closestId != "")
                    {
                        UberBoss::switchTarget(%aiName,%aiId,%closestId);
                    }
                    else
                        UberBoss::DoReset(%aiName,%aiId);
                }
            }
            if(%tgtCnt >= 1 && OddsAre(3))
            {
                UberBoss::DoUberBurst(%aiId,%aiPos,%set);
            }
            
            deleteObject(%set);
        }
    }
    
        
    //Hold still!
    //AI::newDirectiveWaypoint(%aiName, %aiPos, 20);
   // Dragon::SelectMovement(%aiName);
}

function UberBoss::DoUberBurst(%aiId,%pos,%set)
{
    playSound(SoundUberDeath1,%pos);
    CreateAndDetBomb(%aiId, "ShockBomb", %pos, false, -1);
    
    for(%i = 0; %i < Group::objectCount(%set); %i++)
    {
        %pl = Group::getObject(%set, %i);
        if(GameBase::getTeam(%pl) != GameBase::getTeam(%aiId))
        {
            %ppos = Gamebase::getPosition(%pl);
            %dist = Vector::getDistance(%ppos,%pos);
            %dir = Vector::Normalize(Vector::sub(%ppos,%pos));
            %imp = ScaleVector(%dir,$UberBurstScale/%dist);
            echo("Impulse: "@ %imp);
            Player::applyImpulse(%pl,%imp);
            GameBase::virtual(%pl, "onDamage", $BludgeoningDamageType, 350/%dist, "0 0 0", "0 0 0", "0 0 0", "torso", "", %aiId, "UBBlast");
        }
    }
}

function UberBoss::teleportToMarker(%aiId)
{
    Gamebase::setPosition(%aiId,Gamebase::getPosition(fetchData(%aiId,"ubMarker")));
}

// Laugh
function UberBoss::PreCombat(%aiName,%aiId)
{
    dbecho($dbechoMode, "UberBoss::PreCombat(" @ %aiName @ ","@%aiId@")");
    AI::newDirectiveRemove(%aiName,100);

    //OgreAcquired1
    %pos = Gamebase::getPosition(%aiId);
    playSound(SoundUberBossLaugh1,%pos);
    
    schedule("Player::mountItem("@%aiId@",warhammer,0);",1.7);
    schedule("UberBoss::StartCombat("@%aiName@","@%aiId@");",2);
}



function UberBoss::StartCombat(%aiName,%aiId,%interrupt)
{
    dbecho($dbechoMode, "UberBoss::StartCombat(" @ %aiName @ ","@%aiId@","@%interrupt@")");
    if(%interrupt != "")
        Player::mountItem(%aiId,"warhammer",0);
    storeData(%aiId,"ubCombatReady","");
    storeData(%aiId,"ubStandWaiting","");
    playSound(SoundUberBossAcquired1,Gamebase::getPosition(%aiId));
    AI::setVar(%aiName, spotDist, GetRange(warhammer));
    %target = fetchData(%aiId,"currentTarget");
    AI::newDirectiveFollow(%aiName,%target,0,99);
    AI::setVar(%aiName, seekOff, Vector::getFromRot(ScaleVector(Gamebase::getRotation(%target),GetRange(WarMaul)-1)));
    storeData(%aiId,"ubCombatStarted",true);
    storeData(%aiId,"NextHitBash",true);
    storeData(%aiId,"ubCombatState",$UberBoss::CombatStateAttacking);
    
    %zone = fetchData(%aiId,"zone");
    
}

function UberBoss::setReadyFlag(%aiName)
{
    dbecho($dbechoMode, "UberBoss::setReadyFlag(" @ %aiName @ ")");
    %aiId = AI::getId(%aiName);
    // This acts as boss reset.
    UberBoss::clearVars(%aiId);
    storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingWaitForTarget);
    storeData(%aiId,"ubCombatReady",true);
}

function UberBoss::debugState(%aiId)
{
    echo("Combat Ready: "@fetchData(%aiId,"ubCombatReady"));
    echo("Combat Started: "@fetchData(%aiId,"ubCombatStarted"));
    echo("Marker: "@fetchData(%aiId,"ubMarker"));
    echo("Stand Waiting State: "@fetchData(%aiId,"ubStandWaiting"));
    echo("Stand Waiting Time: "@fetchData(%aiId,"ubStandWaitingTime"));
    echo("Current Target: "@fetchData(%aiId,"currentTarget"));
    echo("Combat State: "@fetchData(%aiId,"ubCombatState"));
    
    if(fetchData(%aiId,"currentTarget") != "")
    {
        %dist = Vector::getDistance(Gamebase::getPosition(%aiId),Gamebase::getPosition(fetchData(%aiId,"currentTarget")));
        echo("Target Dist: "@ %dist);
    }
}

function UberBoss::setupAIDefaults(%aiName)
{
    AI::SetVar(%aiName, triggerPct, 1.0 );
	AI::setVar(%aiName, iq, 100 );
	AI::setVar(%aiName, attackMode, $AIattackMode);
    AI::setVar(%aiName, pathType, $AI::defaultPathType);
    AI::SetVar(%aiName, spotDist, $AIspotDist);
    AI::setAutomaticTargets(%aiName);
    Ai::callbackPeriodic(%aiName, 5, UberBoss::Periodic);
    //if(%per != "")
    //    
}

function UberBoss::SkillSetup(%aiId)
{
    %ns = getNumSkills();
	%a = $autoStartupSP + round($initSPcredits / %ns) + round(((fetchData(%aiId, "LVL")-1) * $SPgainedPerLevel) / %ns);
	for(%i = 1; %i <= %ns; %i++)
		AddSkillPoint(%aiId, %i, %a);

    $PlayerSkill[%aiId, $SkillBludgeoning] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel)*0.5;
    $PlayerSkill[%aiId, $SkillBashing] = (getRandom() * $SkillRangePerLevel) + 400;
    $PlayerSkill[%aiId, $SkillEnergy] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel);
	$PlayerSkill[%aiId, $SkillDefensiveCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel);
	$PlayerSkill[%aiId, $SkillNeutralCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel);
    $PlayerSkill[%aiId, $SkillWeightCapacity] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel);
	$PlayerSkill[%aiId, $SkillEndurance] = ( (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel) ) / 1.2;

	$PlayerSkill[%aiId, $SkillOffensiveCasting] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel) / 2;
	$PlayerSkill[%aiId, $SkillSenseHeading] = (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel);

	%a = (  (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel)  ) / 4;
	%sr = round(%a * GetSkillMultiplier(%aiId, $SkillOffensiveCasting));
	$PlayerSkill[%aiId, $SkillSpellResistance] = %sr;
    //=============================================================
}

function UberBoss::GiveStuff(%aiId)
{
    GiveThisStuff(%aiId,"LVL 85 CLASS Fighter warhammer 1");
    //HardcodeAIskills(%aiId);
    UberBoss::SkillSetup(%aiId);
    RPGItem::incItemCount(%aiId,FieldPlateArmor0);
    //Player::mountItem(%aiId,WarMaul,0);
}