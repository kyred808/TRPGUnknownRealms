
$UberBoss::triggerRange = 40;

$UberBoss::StandWaitingSetup = 0;
$UberBoss::StandWaitingMoveToPos = 1;
$UberBoss::StandWaitingWaitForTarget = 2;
$UberBoss::StandWaitingTeleportDelay = 90;

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
    
    storeData(%aiId,"ubMarker",%marker);
    storeData(%aiId,"isUberBoss",true);
    
    storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingSetup);
}

function UberBoss::clearVars(%aiId)
{
    storeData(%aiId,"ubStandWaiting","");
    storeData(%aiId,"ubCombatReady","");
    storeData(%aiId,"currentTarget","");
    storeData(%aiId,"ubCombatStarted","");
    storeData(%aiId,"ubStandWaitingTime","");
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
    %combatStarted = fetchData(%aiId,"ubStandWaiting");
    if(%standWaiting != "")
    {
        if(%standWaiting == 0)
        {
            AI::newDirectiveWaypoint(%aiName, Gamebase::getPosition(%marker), 70);
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
                    UberBoss::PreCombat(%aiName,%aiId);
                }
            }
            deleteObject(%set);
        }
    }
    
        
    //Hold still!
    //AI::newDirectiveWaypoint(%aiName, %aiPos, 20);
   // Dragon::SelectMovement(%aiName);
}

function UberBoss::teleportToMarker(%aiId)
{
    Gamebase::setPositon(%aiId,fetchData(%aiId,"ubMarker"));
}

// Laugh
function UberBoss::PreCombat(%aiName,%aiId)
{
    dbecho($dbechoMode, "UberBoss::PreCombat(" @ %aiName @ ","@%aiId@")");
    AI::DirectiveRemove(%aiName,70);

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
	$PlayerSkill[%aiId, $SkillEndurance] = ( (getRandom() * $SkillRangePerLevel) + ((fetchData(%aiId, "LVL")-1) * $SkillRangePerLevel) ) / 2;

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