
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
$UberBoss::CombatStateTowerLeapSetup = 1;
$UberBoss::CombatStateTowerLeaping = 2;
$UberBoss::CombatStateOnTower = 3;

$UberBoss::LeapFudgeFactor = 5; //So their feet clear the tower

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
    storeData(%aiId,"customAiFlag",true);
    UberBoss::setupAIDefaults(%aiName);
    UberBoss::GiveStuff(%aiId);
    
    AI::CallbackDied(%aiName,UberBoss::doDeath);
    $ZoneCleanupProtected[%aiId] = true;
    storeData(%aiId,"ubMarker",%marker);
    storeData(%aiId,"isUberBoss",true);
    storeData(%aiId,"isResetting","");
    
    storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingSetup);
}

$UberBoss::mobLoadout[0] = "CLASS Fighter COINS 300 ShortSword 1";
$UberBoss::mobLoadout[1] = "CLASS Fighter COINS 300 WarAxe 1";
$UberBoss::mobLoadout[2] = "CLASS Ranger COINS 300 Shortbow 1 SheafArrow 100 Dagger 1";

function UberBoss::SpawnAddGroup(%aiId)
{
    %pos = Gamebase::getPosition(%aiId);
    %spMkr = fetchData(%aiId,"ubMarker");
    %gndZ = getWord(Gamebase::getPosition(%spMkr),2);
    %aiPosOnGround = Word::getSubWord(%pos,0,2) @" "@%gndZ;
    
    %minDist = 40;
    
    for(%i = 0; $UberBoss::mobLoadout[%i] != ""; %i++)
    {
        %spawnOffset = %minDist*cos(getRandomMT()*2*$pi) + (getRandomMT()*10) @" "@ %minDist*sin(getRandomMT()*2*$pi) + (getRandomMT()*10) @" 0";
        UberBoss::SpawnAdd(%aiId,"Minion",OrcArmor,Vector::add(%aiPosOnGround,%spawnOffset),"0 0 "@ getRandomMT()*$pi/2,$UberBoss::mobLoadout[%i]);
    }
    
    storeData(%aiId,"ubAddsSpawned",true);
}

function UberBoss::SpawnAdd(%ubAiId,%name,%armor,%pos,%rot,%loadout)
{
    %n = getAInumber();
    %aiName = %name@%n;
    
    AI::otherCreate(%aiName,%aiName,%armor,%pos,%rot);
    setAINumber(%aiName,%n);
    %aiId = AI::getId(%aiName);
    
    Gamebase::setTeam(%aiId,Gamebase::getTeam(%ubAiId));
    
    %loadout = "LVL "@ fetchData(%ubAiId,"LVL")/3 @" "@%loadout;
    
    storeData(%aiId,"ubAiId",%ubAiId);
    %mobList = fetchData(%ubAiId,"mobList");
    storeData(%ubAiId,"mobList",%mobList @ %aiId @" ");
    GiveThisStuff(%aiId,%loadout);
    RPGItem::incItemCount(%aiId,ScaleMail0);
    AI::CallbackDied(%aiName,UberBoss::mobDeath);
    $ZoneCleanupProtected[%aiId] = true;
    
    HardcodeAIskills(%aiId);
	Game::refreshClientScore(%aiId);
    
    AI::SetVar(%aiName, triggerPct, 1.0 );
	AI::setVar(%aiName, iq, 100 );
	AI::setVar(%aiName, attackMode, $AIattackMode);
    AI::setVar(%aiName, pathType, $AI::defaultPathType);
    AI::SetVar(%aiName, spotDist, $AIspotDist);
    AI::setAutomaticTargets(%aiName);
    Ai::callbackPeriodic(%aiName, 5, AI::Periodic);
    
    AI::SelectBestWeapon(%aiId);
}

function UberBoss::mobDeath(%aiName)
{
    %aiId = AI::getId(%aiName);
    %ubId = fetchData(%aiId,"ubAiId");
    %list = fetchData(%ubId,"mobList");
    // Remove from list
    %nlist = String::removeWords(%list,%aiId);
    storeData(%ubId,"mobList",%nlist);
    AI::onDroneKilled(%aiName);
    $ZoneCleanupProtected[%aiId] = "";
    storeData(%aiId,"ubAiId","");
    echo("UB Reset: "@ fetchData(%ubId,"isResetting"));
    if(%nlist == "" && fetchData(%ubId,"isResetting") == "")
        UberBoss::AllMobsDead(fetchData(%ubId,"BotInfoAiName"));
}

function UberBoss::doDeath(%aiName)
{
    echo("Brute Dead");
    %aiId = AI::getId(%aiName);
    echo(fetchData(%aiId,"mobList"));
    
    if(fetchData(%aiId,"ubCombatStarted"))
    {
        UberBoss::stopMusic(%aiId);
    }
    AI::newDirectiveRemove(%aiName, 99);
    AI::newDirectiveRemove(%aiName, 100);
    //Clean up mobs
    %list = fetchData(%aiId,"mobList");
    %mobCnt = GetWordCount(%list);
    if(%mobCnt > 0)
    {
        storeData(%aiId,"isResetting",true);
        for(%i = 0; %i < %mobCnt; %i++)
        {
            %mobId = getWord(%list,%i);
            storeData(%mobId,"noDropLootbagFlag",true);
            Player::Kill(%mobId);
        }
    }
    AI::onDroneKilled(%aiName);
    UberBoss::clearVars(%aiId);
    storeData(%aiId,"ubMarker","");
    storeData(%aiId,"isUberBoss","");
    $ZoneCleanupProtected[%aiId] = "";
}

function UberBoss::AllMobsDead(%aiName)
{
    %aiId = AI::getId(%aiName);
    storeData(%aiId,"ubAddsSpawned","");
    storeData(%aiId,"ubShielded","");
    storeData(%aiId,"ubCombatState",$UberBoss::CombatStateAttacking);
    UberBoss::switchTarget(%aiName,%aiId,fetchData(%aiId,"currentTarget"));
    AI::setVar(%aiName, spotDist, GetRange(warhammer));
    Player::mountItem(%aiId,"warhammer",0);
}

function UberBoss::clearVars(%aiId)
{
    storeData(%aiId,"ubStandWaiting","");
    storeData(%aiId,"ubCombatReady","");
    storeData(%aiId,"currentTarget","");
    storeData(%aiId,"ubCombatStarted","");
    storeData(%aiId,"ubStandWaitingTime","");
    storeData(%aiId,"ubCombatState","");
    storeData(%aiId,"CombatZone","");
    storeData(%aiId,"ubTowerMarker","");
    storeData(%aiId,"ubLeapTime","");
    storeData(%aiId,"ubLeapSetupPos","");
    storeData(%aiId,"ubAddsSpawned","");
    storeData(%aiId,"ubShielded","");
    storeData(%aiId,"leapCoolDown","");
}

function UberBoss::DoReset(%aiName,%aiId)
{
    //Clean up mobs
    %list = fetchData(%aiId,"mobList");
    %mobCnt = GetWordCount(%list);
    if(GetWordCount(%list) > 0)
    {
        storeData(%aiId,"isResetting",true);
        for(%i = 0; %i < %mobCount; %i++)
        {
            %mobId = getWord(%list,%i);
            storeData(%mobId,"noDropLootbagFlag",true);
            Player::Kill(%mobId);
        }
    }
    UberBoss::clearVars(%aiId);
    AI::newDirectiveRemove(%aiName, 99);
    AI::newDirectiveRemove(%aiName, 100);
    storeData(%aiId,"ubStandWaiting",0);
    Player::unmountItem(Client::getOwnedObject(%aiId),0);
}

function UberBoss::switchTarget(%aiName,%aiId,%targetClient)
{
    storeData(%aiId,"currentTarget",%targetClient);
    AI::newDirectiveFollow(%aiName,%targetClient,3,99);
    AI::setVar(%aiName, seekOff, Vector::getFromRot(Gamebase::getRotation(%targetClient),GetRange(warhammer)-1));
}

function UberBoss::startMusic(%aiId)
{
    //music_antaris_quake3.wav
    %zone = fetchData(%aiId,"CombatZone");
    //storeData(%aiId,"musicZone",%zone);
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
    %zone = fetchData(%aiId,"CombatZone");
    %zz = fetchData(%aiId,"CombatZone");
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
                //schedule("UberBoss::setReadyFlag(\""@%aiName@"\");",2.5);
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
            %burst = false;
            if(%tgtCnt >= 1 && OddsAre(3))
            {
                UberBoss::DoUberBurst(%aiId,%aiPos,%set);
                %burst = true;
            }
            
            if(!%burst && fetchData(%aiId,"ubTowerMarker") != "" && OddsAre(15) && fetchData(%aiId,"leapCoolDown") < getSimTime())
            {
                UberBoss::SetupLeapToPoint(%aiId,%aiName);
            }
            
            deleteObject(%set);
        }
        else if(%state == $UberBoss::CombatStateTowerLeapSetup)
        {
            //Moving to point.
            %ckPoint = fetchData(%aiId,"ubLeapSetupPos");
            if(Vector::getDistance(%aiPos,%ckPoint) < 5)
            {
                storeData(%aiId,"ubLeapSetupPos","");
                UberBoss::readyLeap(%aiName,%aiId,fetchData(%aiId,"ubTowerMarker"));
            }
        }
        else if(%state == $UberBoss::CombatStateTowerLeaping)
        {
        }
        else if(%state == $UberBoss::CombatStateOnTower)
        {
            if(fetchData(%aiId,"ubAddsSpawned") == "")
            {
                AI::SetVar(%aiName, spotDist, 300);
                UberBoss::SpawnAddGroup(%aiId);
                remotePlayAnim(%aiId,9);
            }
            else
            {
                UberBoss::LaunchProjRain(%aiId);
                
                %rng = getIntRandomMT(0,10);
            
                if(%rng >=0 && %rng <= 3)
                {
                    remotePlayAnim(%aiId,9);
                }
                else if(%rnt < 7)
                {
                    remotePlayAnim(%aiId,5);
                }
                else
                {
                    remotePlayAnim(%aiId,6);
                }
            }
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
    //playSound(SoundUberBossLaugh1,%pos);
    Gamebase::playSound(Client::getOwnedObject(%aiId),SoundUberBossLaugh1,0);
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
    //playSound(SoundUberBossAcquired1,Gamebase::getPosition(%aiId));
    Gamebase::playSound(Client::getOwnedObject(%aiId),SoundUberBossAcquired1,1);
    AI::setVar(%aiName, spotDist, GetRange(warhammer));
    %target = fetchData(%aiId,"currentTarget");
    AI::newDirectiveFollow(%aiName,%target,3,99);
    AI::setVar(%aiName, seekOff, Vector::getFromRot(ScaleVector(Gamebase::getRotation(%target),GetRange(warhammer)-1)));
    storeData(%aiId,"ubCombatStarted",true);
    storeData(%aiId,"NextHitBash",true);
    storeData(%aiId,"ubCombatState",$UberBoss::CombatStateAttacking);
    schedule("UberBoss::StartMusic("@%aiId@");",0.5);
    //%zone = fetchData(%aiId,"zone");
    
}

function UberBoss::setReadyFlag(%aiName)
{
    dbecho($dbechoMode, "UberBoss::setReadyFlag(" @ %aiName @ ")");
    %aiId = AI::getId(%aiName);
    AI::SetVar(%aiName, spotDist, 500); //He'll start watching people when he's ready
    // This acts as boss reset.
    UberBoss::clearVars(%aiId);
    %zone = fetchData(%aiId,"Zone");
    storeData(%aiId,"CombatZone",%zone);
    storeData(%aiId,"ubStandWaiting",$UberBoss::StandWaitingWaitForTarget);
    
    %mkrGrp = getGroup(fetchData(%aiId,"ubMarker"));
    for(%i = 0; %i < Group::objectCount(%mkrGrp); %i++)
    {
        %obj = Group::getObject(%mkrGrp,%i);
        %name = Object::getName(%obj);
        if(String::icompare(%name,"TowerMarker") == 0)
        {
            storeData(%aiId,"ubTowerMarker",%obj);
            continue;
        }
    }

    for(%i = 0; %i < Group::objectCount(%zone); %i++)
    {
        %obj = Group::getObject(%zone,%i);
        if(getObjectType(%obj) == "Marker")
        {
            storeData(%aiId,"ZoneMarker",%obj);
            break;
        }
    }
    
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
    echo("Tower Marker: "@fetchData(%aiId,"ubTowerMarker"));
    echo("Leap Time: "@fetchData(%aiId,"ubLeapTime"));
    echo("Leap from Pos: "@fetchData(%aiId,"ubLeapSetupPos"));
    echo("Zone Marker: "@fetchData(%aiId,"ZoneMarker"));
    if(fetchData(%aiId,"currentTarget") != "")
    {
        %dist = Vector::getDistance(Gamebase::getPosition(%aiId),Gamebase::getPosition(fetchData(%aiId,"currentTarget")));
        echo("Target Dist: "@ %dist);
    }
}

function UberBoss::LaunchProjRain(%aiId)
{
    %mkr = fetchData(%aiId,"ZoneMarker");
    //echo(%mkr);
    if(%mkr != "")
    {
        %pos = Gamebase::getPosition(%mkr);
        %name = Object::getName(%mkr);
        %sizex = getWord(%name,0)/4;
        %sizey = getWord(%name,1)/4;
        for(%i = 0; %i < 24; %i++)
        {
            //%randPos = %pos;
            %randPos = Vector::add(%pos,getRandomMT()*%sizex*2 - %sizex @" "@ getRandomMT()*%sizey*2 - %sizey @" 0");
            %randSkyPos = Vector::add(%pos,getRandomMT()*%sizex*2 - %sizex @" "@ getRandomMT()*%sizey*2 - %sizey @" "@ 300 + getRandomMT()*30);
            
            %dir = Vector::Normalize(Vector::sub(%randPos,%randSkyPos));
            
            %trans = "0 0 0 "@ %dir @" 0 0 0 "@ %randSkyPos;
            //echo(%trans);
            %proj = Projectile::spawnProjectile(UberBossRainProj,%trans,Client::getOwnedObject(%aiId),"0 0 0");
        }
    }
}

function UberBoss::SetupLeapToPoint(%aiId,%aiName)
{
    echo("Setting Up Leap");
    %mkr = fetchData(%aiId,"ubTowerMarker");
    %spMkr = fetchData(%aiId,"ubMarker");
    %aiPos = Gamebase::getPosition(%aiId);
    %gndZ = getWord(Gamebase::getPosition(%spMkr),2);
    %towerMarkerOnGround = Word::getSubWord(Gamebase::getPosition(%mkr),0,2) @" "@%gndZ;
    %dist = Vector::getDistance(%aiPos, %towerMarkerOnGround);
    //echo(%towerMarkerOnGround);
    //echo("Distance: "@ %dist);
    
    if(%dist < 20)
    {
        storeData(%aiId,"ubCombatState",$UberBoss::CombatStateTowerLeapSetup);
        // Place a move position on the ground, 36m out from the tower
        
        //%dir = Vector::sub(Word::getSubWord(%aiPos,0,2) @ " 0",Word::getSubWord(%towerMarkerOnGround,0,2) @" 0");
        //%movePos = Vector::add(%towerMarkerOnGround,ScaleVector(Vector::Normalize(%dir),36));
        
        //All that above math would work, and place the marker 36m out, near the ai's position.
        //But we could also just place it 36m east of the tower.  Just hope they dont' get caught on the tower.
        
        %movePos = Vector::add(%towerMarkerOnGround,"20 0 0");
        AI::newDirectiveWaypoint(%aiName,%movePos,99);
        storeData(%aiId,"ubLeapSetupPos",%movePos);
    }
    else
    {
        UberBoss::readyLeap(%aiName,%aiId,%mkr);
    }
}

function UberBoss::readyLeap(%aiName,%aiId,%mkr)
{
    %mkrPos = Gamebase::getPosition(%mkr);
    storeData(%aiId,"ubCombatState",$UberBoss::CombatStateTowerLeaping);
    AI::newDirectiveWaypoint(%aiName,%mkrPos,99);
    //Start leap check
    UberBoss::DoLeap(%aiId,%aiName,%mkr,3);
}

function UberBoss::DoLeap(%clientId,%aiName,%mkr,%time)
{
    %cpos = Gamebase::getPosition(%clientId);
    %mpos = Gamebase::getPosition(%mkr); 

    %dir = Vector::sub(%mpos,%cpos);
    %dist = Vector::getDistance(%cpos,%mpos);
    
    //getWord(%dir,2) is height
    %vx = getWord(%dir,0) / %time;
    %vy = getWord(%dir,1) / %time;
    %vz = ((getWord(%dir,2) - 0.5 + $UberBoss::LeapFudgeFactor) + 0.5*(20 * %time * %time))/%time; // 20 is gravity
    Gamebase::setPosition(%clientId,Vector::add(%cpos,"0 0 0.5")); // Move a little up to avoid ground friction
    Item::setVelocity(Client::getOwnedObject(%clientId),%vx @" "@ %vy @" " @ %vz);
    storeData(%clientId,"ubLeapTime",getSimTime());
    UB::CheckLeap(%clientId,%mkr);
    
}

function UB::CheckLeap(%aiId,%mkr)
{
    %time = fetchData(%aiId,"ubLeapTime");
    %mkrPos = Gamebase::getPosition(%mkr);
    %stopFlag = false;
    if(Vector::getDistance(Gamebase::getPosition(%aiId),%mkrPos) < 6)
    {
        echo("Leap worked!");
        storeData(%aiId,"ubCombatState",$UberBoss::CombatStateOnTower);
        storeData(%aiId,"ubShielded",true);
        Item::setVelocity(Client::getOwnedObject(%aiId),"0 0 0");
        Gamebase::setPosition(%aiId,%mkrPos);
        storeData(%aiId,"ubLeapTime","");
        Player::unmountItem(Client::getOwnedObject(%aiId),0);
        %stopFlag = true;
    }
    
    if(getSimTime() - %time > 5)
    {
        echo("Leap failed!");
        //Leap failed!
        // Go back to fighting
        %aiName = fetchData(%aiId,"BotInfoAiName");
        storeData(%aiId,"ubLeapTime","");
        storeData(%aiId,"ubCombatState",$UberBoss::CombatStateAttacking);
        AI::newDirectiveFollow(%aiName,fetchData(%aiId,"currentTarget"),3,99);
        %stopFlag = true;
        storeData(%aiId,"leapCoolDown",getSimTime()+120);
    }
    if(!%stopFlag)
        schedule("UB::CheckLeap("@%aiId@","@%mkr@");",0.2);
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
	$PlayerSkill[%aiId, $SkillManaManipulation] = %sr;
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