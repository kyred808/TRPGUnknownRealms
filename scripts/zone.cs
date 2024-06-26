function InitZones()
{
	dbecho($dbechoMode, "InitZones()");

	$numZones = 0;
	%zcnt = 0;
	%umusiccnt = 0;
    
    if($Realms::MapUsesRealms)
    {
        Realms::InitZones();
        return;
    }
    
    //Old code
    ///===============================================
	//%group = nameToId("MissionGroup\\Zones");
    //
	//if(%group != -1)
	//{
	//	%count = Group::objectCount(%group);
	//	for(%i = 0; %i <= %count-1; %i++)
	//	{
	//		%object = Group::getObject(%group, %i);
	//		%system = Object::getName(%object);
	//		%type = GetWord(%system, 0);
	//		%desc = String::getSubStr(%system, String::len(%type)+1, 9999);
    //        echo(%system);
	//		//---------------------------------------------------------------
	//		//THIS PART GATHERS SOUNDS FOR THE GENERIC UNKNOWN ZONE
	//		// there is no EXIT sound for the unknown zone.
	//		//---------------------------------------------------------------
	//		if(GetWord(%system, 0) == "ENTERSOUND")
	//		{	
	//			$Zone::EnterSound[0] = GetWord(%system, 1);
	//		}
	//		else if(GetWord(%system, 0) == "AMBIENTSOUND")
	//		{
	//			$Zone::AmbientSound[0] = GetWord(%system, 1);
	//			$Zone::AmbientSoundPerc[0] = GetWord(%system, 2);
	//		}
	//		else if(GetWord(%system, 0) == "MUSIC")
	//		{
    //            
	//			$Zone::Music[0, %umusiccnt++] = GetWord(%system, 1);
	//			$Zone::MusicTicks[0, %umusiccnt] = GetWord(%system, 2);
	//		}
	//		//---------------------------------------------------------------
	//		else
	//		{
	//			%zcnt++;
    //
	//			%tmpgroup = nameToId("MissionGroup\\Zones\\" @ %system);
	//			%tmpcount = Group::objectCount(%tmpgroup);
	//			%marker = "";
	//			%musiccnt = 0;
    //
	//			for(%z = 0; %z <= %tmpcount-1; %z++)
	//			{
	//				%tmpobject = Group::getObject(%tmpgroup, %z);
	//
	//				if(getObjectType(%tmpobject) == "Marker")
	//				{
	//					if(%marker == "")
	//					{
	//						%marker = %tmpobject;
	//						$numZones++;
	//					}
	//				}
	//				else if(getObjectType(%tmpobject) == "SimGroup")
	//				{
	//					%n = Object::getName(%tmpobject);
	//					
	//					if(GetWord(%n, 0) == "ENTERSOUND")
	//					{	
	//						$Zone::EnterSound[%zcnt] = GetWord(%n, 1);
	//					}
	//					else if(GetWord(%n, 0) == "AMBIENTSOUND")
	//					{
	//						$Zone::AmbientSound[%zcnt] = GetWord(%n, 1);
	//						$Zone::AmbientSoundPerc[%zcnt] = GetWord(%n, 2);
	//					}
	//					else if(GetWord(%n, 0) == "EXITSOUND")
	//					{
	//						$Zone::ExitSound[%zcnt] = GetWord(%n, 1);
	//					}
	//					else if(GetWord(%n, 0) == "MUSIC")
	//					{
	//						$Zone::Music[%zcnt, %musiccnt++] = GetWord(%n, 1);
	//						$Zone::MusicTicks[%zcnt, %musiccnt] = GetWord(%n, 2);
	//					}
	//				}
	//			}
	//			
	//			%mname = Object::getName(%marker);
	//			$Zone::Marker[%zcnt] = GameBase::getPosition(%marker);
	//			$Zone::Length[%zcnt] = GetWord(%mname, 0);
	//			$Zone::Width[%zcnt] = GetWord(%mname, 1);
	//			$Zone::Height[%zcnt] = GetWord(%mname, 2);
	//			$Zone::SHeight[%zcnt] = GetWord(%mname, 3);
	//			$Zone::Type[%zcnt] = %type;
	//			$Zone::Desc[%zcnt] = %desc;
	//			$Zone::FolderID[%zcnt] = %tmpgroup;
	//		}
	//	}
	//	echo($numZones @ " zones initialized.");
	//}
}

function Zone::addZone(%group)
{
    %zcnt = $ZoneData::NumberZones+1;
    %tmpgroup = %group;
    %tmpcount = Group::objectCount(%tmpgroup);
    %marker = "";
    %musiccnt = 0;
    %system = Object::getName(%group);
    %type = getWord(%system,0);
    %desc = Word::getSubWord(%system,1,999);
    echo("new zone: "@%zcnt);
    $Zone::QuantumObjs[%zcnt,NumberGroups] = 0;
    for(%z = 0; %z <= %tmpcount-1; %z++)
    {
        %tmpobject = Group::getObject(%tmpgroup, %z);
        
        
        if(getObjectType(%tmpobject) == "Marker")
        {
            if(%marker == "")
            {
                %marker = %tmpobject;
                $ZoneData::NumberZones++;
            }
        }
        else if(getObjectType(%tmpobject) == "SimGroup")
        {
            %n = Object::getName(%tmpobject);
            %w0 = GetWord(%n, 0);
            if(%w0 == "ENTERSOUND")
            {	
                echo("EnterSound "@ %zcnt);
                $Zone::EnterSound[%zcnt] = GetWord(%n, 1);
                echo($Zone::EnterSound[%zcnt]);
            }
            else if(%w0 == "AMBIENTSOUND")
            {
                $Zone::AmbientSound[%zcnt] = GetWord(%n, 1);
                $Zone::AmbientSoundPerc[%zcnt] = GetWord(%n, 2);
            }
            else if(%w0 == "EXITSOUND")
            {
                $Zone::ExitSound[%zcnt] = GetWord(%n, 1);
            }
            else if(%w0 == "MUSIC")
            {
                $Zone::Music[%zcnt, %musiccnt++] = GetWord(%n, 1);
                $Zone::MusicTicks[%zcnt, %musiccnt] = GetWord(%n, 2);
            }
            else if(%w0 == "SpawnPoints")
            {
                Zone::addSpawnPoints(%zcnt,%tmpobject);
            }
            else if(%w0 == "QuantumObjects")
            {
                $Zone::QuantumObjs[%zcnt,NumberGroups]++;
                %qId = $Zone::QuantumObjs[%zcnt,NumberGroups]-1;
                $Zone::QuantumObjs[%zcnt,%qId,Points,Max] = 0;
                Zone::addQuantumObjectGroup(%zcnt,%qId,%tmpobject);
                
            }
        }
    }
    
    %mname = Object::getName(%marker);
    $Zone::Marker[%zcnt] = GameBase::getPosition(%marker);
    $Zone::Length[%zcnt] = GetWord(%mname, 0);
    $Zone::Width[%zcnt] = GetWord(%mname, 1);
    $Zone::Height[%zcnt] = GetWord(%mname, 2);
    $Zone::SHeight[%zcnt] = GetWord(%mname, 3);
    $Zone::Type[%zcnt] = %type;
    $Zone::Desc[%zcnt] = %desc;
    $Zone::FolderID[%zcnt] = %tmpgroup;
    $Zone::FolderToID[%tmpgroup] = %zcnt;
    
    $Zone::Active[%zcnt] = false;
    
    echo($Zone::Desc[%zcnt] @" "@ %zcnt);
    
    // Just something silly
    if(Object::getName(%group) == "FREEFORALL Top of Well")
        Zone::setupSpookyWell(%group);
    
    return %zcnt;
}

function Zone::addQuantumObjectGroup(%zcnt,%quantumGrpId,%pointGroupObj)
{
    %n = Object::getName(%pointGroupObj);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,Datablock] = getWord(%n,1);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,NumActive] = getWord(%n,2);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,UpdateTicks] = getWord(%n,3);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,Raycast] = getWord(%n,4);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,MobSpawnIndex] = getWord(%n,5);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,MobOdds] = getWord(%n,6);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,RestoreTime] = getWord(%n,7);
    if($Zone::QuantumObjs[%zcnt,%quantumGrpId,MobOdds] < 1)
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,MobOdds] = 1;
        
    echo("NumActive: "@ $Zone::QuantumObjs[%zcnt,%quantumGrpId,NumActive]);
    $Zone::QuantumObjs[%zcnt,%quantumGrpId,Tick] = 0;
    for(%i = 0; %i < Group::objectCount(%pointGroupObj); %i++)
    {
        %point = Group::getObject(%pointGroupObj, %i);
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,%i] = %point;
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,Max]++;
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,%i,InUse] = false;
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,%i,Observers] = 0;
    }
    
    for(%i = 0; %i < $Zone::QuantumObjs[%zcnt,%quantumGrpId,NumActive]; %i++)
    {
        %obj = newObject("QuantumObj"@%i,StaticShape,$Zone::QuantumObjs[%zcnt,%quantumGrpId,Datablock],false);
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Object,%i] = %obj;
        %cnt = 0;
        for(%k = 0; %k < $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,Max]; %k++)
        {
            %x[%cnt] = $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,%k];
            %x[%cnt,pidx] = %k;
            %cnt++;
        }
        %idx = getIntRandomMT(0,%cnt-1);
        %pt = %x[%idx];
        %pidx = %x[%idx,pidx];
        Gamebase::setPosition(%obj,Gamebase::getPosition(%pt));
        Gamebase::setRotation(%obj,Gamebase::getRotation(%pt));
        addToSet("MissionCleanup",%obj);
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Points,%pidx,InUse] = true;
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Object,%i,Point] = %pidx;
        $Zone::QuantumObjs[%zcnt,%quantumGrpId,Object,%i,Enabled] = true; 
    }
    
}

function Zone::addSpawnPoints(%zoneId,%spawnGroup)
{
	if(%spawnGroup != -1)
	{
		for(%i = 0; %i <= Group::objectCount(%spawnGroup)-1; %i++)
		{
		    %this = Group::getObject(%spawnGroup, %i);
			%info = Object::getName(%this);
            
			if(%info != "")
			{
                $MarkerZone[%this] = $Zone::FolderID[%zoneId];

				$numAIperSpawnPoint[%this] = 0;
				%indexes = Word::getSubWord(%info,5,9999);
                
				//for(%z = 5; GetWord(%info, %z) != -1; %z++)
				//	%indexes = %indexes @ GetWord(%info, %z) @ " ";

                $Zone::SpawnPoint[%zoneId,%i,Marker] = %this;
                $Zone::SpawnPoint[%zoneId,%i,MaxSpawnPer] = GetWord(%info, 0);
                $Zone::SpawnPoint[%zoneId,%i,MinRadius] = GetWord(%info, 1);
                $Zone::SpawnPoint[%zoneId,%i,MaxRadius] = GetWord(%info, 2);
                $Zone::SpawnPoint[%zoneId,%i,MinDelay] = GetWord(%info, 3);
                $Zone::SpawnPoint[%zoneId,%i,MaxDelay] = GetWord(%info, 4);
                $Zone::SpawnPoint[%zoneId,%i,SpawnIndexes] = %indexes;
                $Zone::SpawnPoint[%zoneId,%i,IndexCount] = GetWordCount(%indexes);
                $Zone::SpawnPoint[%zoneId,%i,SpawnCount] = 0;
                $Zone::SpawnPoint[%zoneId,%i,NextSpawnCheck] = -1;
                $Zone::SpawnPonitMarkerToIndex[%zoneId,%this] = %i;
				echo("===================================================");
				echo("Spawn Point was initialized, %this = " @ %this);
				echo("Max spawn per: " @ GetWord(%info, 0));
				echo("Min radius: " @ GetWord(%info, 1));
				echo("Max radius: " @ GetWord(%info, 2));
				echo("Min delay: " @ GetWord(%info, 3));
				echo("Max delay: " @ GetWord(%info, 4));
				echo("Spawn indexes: " @ %indexes);
				echo("Marker Zone ID: " @ $MarkerZone[%this]);
				echo("===================================================");

				//SpawnLoop(%this);
			}
		}
	}
}

function Zone::RollAllSpawnDelays(%zoneId)
{
    for(%i = 0; $Zone::SpawnPoint[%zoneId,%i,Marker] != ""; %i++)
    {
        $Zone::SpawnPoint[%zoneId,%i,NextSpawnCheck] = getSimTime() + Zone::RollSpawnDelayTime(%zoneId,%i);
    }
}

function Zone::RollSpawnDelayTime(%zoneId,%spawnIndex)
{
    %diff = $Zone::SpawnPoint[%zoneId,%spawnIndex,MaxDelay] - $Zone::SpawnPoint[%zoneId,%spawnIndex,MinDelay];
    return floor(getRandom() * %diff) + $Zone::SpawnPoint[%zoneId,%spawnIndex,MinDelay];
}

function Zone::SpawnCheck(%zoneId)
{
    if($Zone::Active[%zoneId])
    {
        for(%i = 0; $Zone::SpawnPoint[%zoneId,%i,Marker] != ""; %i++)
        {
            %time = getSimTime();
            if($Zone::SpawnPoint[%zoneId,%i,NextSpawnCheck] < %time)
            {
                //Setup next spawn check time
                $Zone::SpawnPoint[%zoneId,%i,NextSpawnCheck] = getSimTime() + Zone::RollSpawnDelayTime(%zoneId,%i);
                
                %maxs = Cap(round($Zone::SpawnPoint[%zoneId,%i,MaxSpawnPer] * $spawnMultiplier), 0, "inf");
                
                if($Zone::SpawnPoint[%zoneId,%i,SpawnCount] < %maxs)
                {
                    //Select spawn index
                    %indexes = $Zone::SpawnPoint[%zoneId,%i,SpawnIndexes];
                    %r = floor(getRandom() * ($Zone::SpawnPoint[%zoneId,%i,IndexCount]));
                    %index = GetWord(%indexes, %r);
                    
                    AI::helper($spawnIndex[%index], $spawnIndex[%index], "ZoneSpawn " @ %zoneId @" "@ %i);//$Zone::SpawnPoint[%zoneId,%i,Marker]);
                }
            }
        }
    }
}

function RecursiveZone(%delay)
{
	dbecho($dbechoMode, "RecursiveZone(" @ %delay @ ")");

	//increment by 1 every $zoneCheckDelay seconds
	$zoneTicker[1]++;
	$zoneTicker[2]++;

	if($zoneTicker[1] >= 1)		//check zone every 2 seconds for players
	{
		DoZoneCheck(2, %delay);
		$zoneTicker[1] = "";

        //DoSpookyZoneUpdate();
	}
//	if($zoneTicker[2] >= 15)	//check zone every 30 seconds for bots
//	{
//		DoZoneCheck(1, %delay);
//		$zoneTicker[2] = "";
//	}

	schedule("RecursiveZone(" @ %delay @ ");", %delay);
}

function DoZoneCheck(%w, %d)
{
	dbecho($dbechoMode, "DoZoneCheck(" @ %w @ ", " @ %d @ ")");

    $Zone::SpookyWellPlayerCount = 0;
    $Zone::SomeoneIsLookingAtWell = false;
	//Massive zone check for entire world
	%mset = newObject("set", SimSet);
	%n = containerBoxFillSet(%mset, $SimPlayerObjectType, "0 0 0", 99999, 99999, 99999, 0);

	for(%z = 1; %z <= $ZoneData::NumberZones; %z++)
	{
		%set = newObject("set", SimSet);
		%n = containerBoxFillSet(%set, $SimPlayerObjectType, $Zone::Marker[%z], $Zone::Length[%z], $Zone::Width[%z], $Zone::Height[%z], $Zone::SHeight[%z]);
		Group::iterateRecursive(%set, setzoneflags, %z);
		deleteObject(%set);
        
        Zone::SpawnCheck(%z);
        Zone::DoQuantumUpdate(%z);
	}
	
	Group::iterateRecursive(%mset, UpdateZone);
	deleteObject(%mset);
}

function Zone::DoQuantumUpdate(%zid)
{
    if($Zone::Active[%zid] && $Zone::QuantumObjs[%zid,NumberGroups] > 0)
    {
        for(%g = 0; (%ticks = $Zone::QuantumObjs[%zid,%g,UpdateTicks]) != ""; %g++)
        {
            if($Zone::QuantumObjs[%zid,%g,Tick] >= %ticks)
            {
                %cnt = 0;
                for(%k = 0; %k < $Zone::QuantumObjs[%zid,%g,Points,Max]; %k++)
                {
                    //No one is observing the point and the point is not in use.
                    //echo("OBS "@ %k@": "@$Zone::QuantumObjs[%zid,%g,Points,%k,Observers]);
                    if($Zone::QuantumObjs[%zid,%g,Points,%k,Observers] == 0 && !$Zone::QuantumObjs[%zid,%g,Points,%k,InUse])
                    {
                        %validPts[%cnt] = %k;
                        %cnt++;
                    }
                    //Delegate cleanup to later.
                    schedule("$Zone::QuantumObjs["@%zid@","@%g@",Points,"@%k@",Observers] = 0;",0.5);
                }
                if(%cnt > 0)
                {
                    for(%i = 0; %i < $Zone::QuantumObjs[%zid,%g,NumActive]; %i++)
                    {
                        if(!$Zone::QuantumObjs[%zid,%g,Object,%i,Enabled])
                            continue;
                        %ptIdx = $Zone::QuantumObjs[%zid,%g,Object,%i,Point];
                        %obj = $Zone::QuantumObjs[%zid,%g,Object,%i];
                        //If no one is observing this object.
                        if($Zone::QuantumObjs[%zid,%g,Points,%ptIdx,Observers] == 0)
                        {
                            %mob = $Zone::QuantumObjs[%zid,%g,MobSpawnIndex];
                            %noSpawnFlag = true;
                            if(%mob != -1)
                            {
                                %odds = $Zone::QuantumObjs[%zid,%g,MobOdds];
                                if(OddsAre(%odds))
                                {
                                    %spawnFlag = false;
                                    $Zone::QuantumObjs[%zid,%g,Object,%i,Enabled] = false;
                                    $Zone::QuantumObjs[%zid,%g,Points,%ptIdx,InUse] = false;
                                    %pos = Gamebase::getPosition(%obj);
                                    deleteObject(%obj);
                                    %aiName = AI::helper($spawnIndex[%mob],$spawnIndex[%mob],"ZoneSpawn "@ %zid @" "@ %ptIdx);
                                    %aiCl = AI::getId(%aiName);
                                    Gamebase::setPosition(%aiCl,%pos);
                                    storeData(%aiCl,"QTObjIndex",%zid@" "@%g@" "@%i);
                                    storeData(%aiCl,"aiGuardMarker",$Zone::QuantumObjs[%zid,%g,Points,%ptIdx]);
                                    storeData(%aiCl,"aiAutoRetaliate",true);
                                    Ai::callbackPeriodic(%aiName, 3, AI::GuardPositionPeriodic);
                                    AI::CallbackDied(%aiName,QTObjMob::onDeath);
                                }
                            }
                            if(%noSpawnFlag)
                            {
                                %cc = getIntRandomMT(0,%cnt-1);
                                %newIdx = %validPts[%cc];
                                %ptObj = $Zone::QuantumObjs[%zid,%g,Points,%newIdx];
                                Gamebase::setPosition(%obj,Gamebase::getPosition(%ptObj));
                                Gamebase::setRotation(%obj,Gamebase::getRotation(%ptObj));
                                $Zone::QuantumObjs[%zid,%g,Points,%ptIdx,InUse] = false;
                                $Zone::QuantumObjs[%zid,%g,Points,%newIdx,InUse] = true;
                                $Zone::QuantumObjs[%zid,%g,Object,%i,Point] = %newIdx;
                                %validPts[%cc] = %ptIdx;
                            }
                        }
                    }
                }
                $Zone::QuantumObjs[%zid,%g,Tick] = 0;
            }
            $Zone::QuantumObjs[%zid,%g,Tick]++;
        }
    }
}

function QTObjMob::onDeath(%aiName)
{
    %client = AI::getId(%aiName);
    %dd = fetchData(%client,"QTObjIndex");
    AI::onDroneKilled(%aiName);
    %zid = getWord(%dd,0);
    %gid = getWord(%dd,1);
    %idx = getWord(%dd,2);
    %tt = $Zone::QuantumObjs[%zid,%gid,RestoreTime];
    schedule("restoreQuantumObject("@%zid@","@%gid@","@%idx@");",%tt);
}

function restoreQuantumObject(%zid,%groupId,%index)
{
    %obj = newObject("QuantumObj"@%index,StaticShape,$Zone::QuantumObjs[%zid,%groupId,Datablock],false);
    $Zone::QuantumObjs[%zid,%groupId,Object,%index] = %obj;
    $Zone::QuantumObjs[%zid,%groupId,Object,%index,Enabled] = true;
    addToSet("MissionCleanup",%obj);
}

function setzoneflags(%object, %z)
{
	dbecho($dbechoMode, "setzoneflags(" @ %object @ ", " @ %z @ ")");

	%clientId = Player::getClient(%object);
	storeData(%clientId, "tmpzone", %z);
    
    if($Zone::QuantumObjs[%z,NumberGroups] != "" && $Zone::QuantumObjs[%z,NumberGroups] > 0)
        DoQuantumCheckCalc(%object,%z);

}

$QuantumFOV = cos(deg2rad(150)/2); //150 degree FOV cone
$QuantumMinDist = 4;
function WithInFOVOrDistCheck(%playerPos,%eyetrans,%otherPos)
{
    %dist = Vector::getDistance(%playerPos,%otherPos);
    %dir = Vector::normalize(Vector::sub(%otherPos,%playerPos));
    %eyeDir = Word::getSubWord(%eyetrans,3,3);
    
    //Dot = 1 if you are looing right at it
    //0 if 90degs off (left or right)
    //-1 if looking directly away
    %dot = Vector::dot(%dir,%eyeDir);
    return %dot >= $QuantumFOV || %dist <= $QuantumMinDist;
}

function DoQuantumCheckCalc(%player,%zid)
{
    %cPos = Gamebase::getPosition(%player);
    %eyeTrans = Gamebase::getEyeTransform(%player);
    for(%g = 0; %g < $Zone::QuantumObjs[%zid,NumberGroups]; %g++)
    {
        if($Zone::QuantumObjs[%zid,%g,Tick] >= $Zone::QuantumObjs[%zid,%g,UpdateTicks])
        {
            for(%k = 0; %k < $Zone::QuantumObjs[%zid,%g,Points,Max]; %k++)
            {
                %point = $Zone::QuantumObjs[%zid,%g,Points,%k];
                %pointPos = Gamebase::getPosition(%point);
                if(WithInFOVOrDistCheck(%cPos,%eyeTrans,%pointPos))
                {
                    $Zone::QuantumObjs[%zid,%g,Points,%k,Observers]++;
                }
            }
        }
    }
}

function UpdateZone(%object)
{
	dbecho($dbechoMode, "UpdateZone(" @ %object @ ")");
	%clientId = Player::getClient(%object);
	%zoneflag = fetchData(%clientId, "tmpzone");

	//check if the player was found inside a zone
	if(%zoneflag != "")
	{
		//the player is inside a zone!
		//check if the player's current zone matches the one he's detected in
		if(fetchData(%clientId, "zone") != $Zone::FolderID[%zoneflag])
		{
			//the client's current zone does not match the one he really is in, so boot the player out of his
			//current zone (if any)
			if(fetchData(%clientId, "zone") != "")
				Zone::DoExit(Zone::getIndex(fetchData(%clientId, "zone")), %clientId);
			//throw the player inside this new zone
			Zone::DoEnter(%zoneflag, %clientId);
		}
		else
		{
			//the client is in the same zone as he was since the last zonecheck
			if($Zone::AmbientSound[%zoneflag] != "")
			{
				%m = $Zone::AmbientSoundPerc[%zoneflag];
				if(%m == "") %m = 100;
	
				%r = floor(getRandomMT() * 100)+1;
				if(%r <= %m)
					Client::sendMessage(%clientId, 0, "~w" @ $Zone::AmbientSound[%zoneflag]);
			}
			if($Zone::Music[%zoneflag, 1] != "")
			{
				if(%clientId.MusicTicksLeft < 1)
				{
					for(%m = 1; $Zone::Music[%zoneflag, %m] != ""; %m++){}
					%m--;
					%clientId.currentMusic = floor(getRandom() * %m) + 1;

					Client::sendMessage(%clientId, 0, "~w" @ $Zone::Music[%zoneflag, %clientId.currentMusic]);
					%clientId.MusicTicksLeft = $Zone::MusicTicks[%zoneflag, %clientId.currentMusic]+2;
				}
			}
			if($Zone::Type[%zoneflag] == "WATER")
			{
				if(!IsDead(%clientId))
				{
					%noDrown = "";
					for(%i = 1; (%orb = $ItemList[Orb, %i]) != ""; %i++)
					{
						if($ProtectFromWater[%orb])
						{
							if(Player::getItemCount(%clientId, %orb @ "0"))
							{
								storeData(%clientId, "drownCounter", 0);
								%noDrown = True;
								break;
							}
						}
					}

					if(!%noDrown)
					{
						%dn = 10;

						storeData(%clientId, "drownCounter", 1, "inc");
						if((%dc = fetchData(%clientId, "drownCounter")) > %dn)
						{
							%dmg = Cap(floor(pow((%dc - %dn) / 1.2, 2)), 1.0, 1000) * "0.01";
							GameBase::virtual(%clientId, "onDamage", 0, %dmg, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId);
							%snd = radnomItems(3, SoundDrown1, SoundDrown2, SoundDrown3);
							playSound(%snd, GameBase::getPosition(%clientId));
						}
					}
				}
			}
		}

		//this simulates underwater
		if($Zone::Type[%zoneflag] == "WATER")
			if($underwaterEffects)
				gravWorkaround(%clientId, 1);
	}
	else
	{
		//the player is not inside any zone.
		//if the player has a current zone, then we need to kick him out of it
		if(fetchData(%clientId, "zone") != "")
        {
			Zone::DoExit(Zone::getIndex(fetchData(%clientId, "zone")), %clientId);
            //play the enter sound for the unknown zone
            if($Zone::EnterSound[0] != "")
                Client::sendMessage(%clientId, 0, "~w" @ $Zone::EnterSound[0]);
        }
	
        //Been a little crashy on startup
        //Wrap around world
        //%rid = $RealmData[fetchData(%clientId,"Realm"), ID];
        ////echo("Check "@ %rid);
        //if($RealmBounds[%rid,Xmin] != "" && fetchData(%clientId,"HasLoadedAndSpawned") && !Player::isAIControlled(%clientId))
        //{
        //    %pos = Gamebase::getPosition(%object);
        //    %xpos = getWord(%pos,0);
        //    %ypos = getWord(%pos,1);
        //    %zpos = getWord(%pos,2);
        //    
        //    if(%xpos > $RealmBounds[%rid,Xmax]-10)
        //        %npos = $RealmBounds[%rid,Xmin]+10 @ " "@ %ypos @ " "@ %zpos;
        //    else if(%ypos > $RealmBounds[%rid,Ymax]-10)
        //        %npos = %xpos @" "@ $RealmBounds[%rid,Ymin]+10 @ " " @ %zpos;
        //    else if(%xpos < $RealmBounds[%rid,Xmin]+10)
        //        %npos = $RealmBounds[%rid,Xmax]-10 @ " "@ %ypos @ " "@ %zpos;
        //    else if(%ypos < $RealmBounds[%rid,Ymin]+10)
        //        %npos = %xpos @" "@ $RealmBounds[%rid,Ymax]-10 @ " " @ %zpos;
        //    
        //    if(%npos != "")
        //    {
        //        Gamebase::setPosition(%clientId,%npos);
        //        %los = RaycastUp(%clientId,500);
        //        if(%los)
        //        {
        //            if(getObjectType($los::object) == "SimTerrain")
        //            {
        //                Gamebase::setPosition(%clientId,$los::position);
        //                Item::setVelocity(%object,"0 0 0");
        //            }
        //        }
        //        Client::sendMessage(%clientId,0,"~wCapturedTower.wav");
        //    }
        //}
        %currentRealm = fetchData(%clientId,"Realm");
        %rid = $RealmData[%currentRealm, ID];
        %pos = Gamebase::getPosition(%clientId);
        %zpos = getWord(%pos,2);
        //start playing the ambient sound for the unknown zone
		if($Zone::AmbientSound[0] != "" && (%zpos - $RealmHeight[%rid]) >= $UnknownZoneAmbientSoundMinHeight)
		{
			%m = $Zone::AmbientSoundPerc[0];
			if(%m == "") %m = 100;
			
			%r = floor(getRandomMT() * 100)+1;
			if(%r <= %m)
				Client::sendMessage(%clientId, 0, "~w" @ $Zone::AmbientSound[0]);
		}
    
		//start playing the ambient sound for the unknown zone
		if($Zone::Music[0, 1] != "")
		{
            if(%clientId.MusicTicksLeft < 1)
            {
                %clientId.currentMusic = 1;
                
                Client::sendMessage(%clientId, 0, "~w" @ $Zone::Music[0, 1]);
                %clientId.MusicTicksLeft = $Zone::MusicTicks[0, 1]+2;
            }
        
		}
	
		if(!Player::isAIControlled(%clientId))
        {
            if(%zpos > $RealmData[%currentRealm,MaxHeight] || %zpos < $RealmData[%currentRealm,MinHeight])
            {
                if(fetchData(%clientId,"noRealmCheck") == "")
                    Realm::KickPlayerBackInRealm(%clientId,%currentRealm);
            }
        }

		//play unknown zone music
	//	if($Zone::Music[0, 1] != "")
	//	{
	//		if(%clientId.MusicTicksLeft < 1)
	//		{
	//			for(%m = 1; $Zone::Music[0, %m] != ""; %m++){}
	//			%m--;
	//			%clientId.currentMusic = floor(getRandom() * %m) + 1;
	//
	//			Client::sendMessage(%clientId, 0, "~w" @ $Zone::Music[0, %clientId.currentMusic]);
	//			%clientId.MusicTicksLeft = $Zone::MusicTicks[0, %clientId.currentMusic]+2;
	//		}
	//	}
	}

	//-----------------------------------------------------------
	// Decrease music ticks
	//-----------------------------------------------------------
	if(%clientId.MusicTicksLeft > 0)
		%clientId.MusicTicksLeft--;

	//-----------------------------------------------------------
	// Decrease bonus state ticks
	//-----------------------------------------------------------
	DecreaseBonusStateTicks(%clientId);

    //Heal burst stuff
    if(fetchData(%clientId,"HealBurst") < fetchData(%clientId,"HealBurstMax"))
    {
        %rate = fetchData(%clientId,"HealBurstRate");
        if(%rate == "")
            %rate = 5;
            
        storeData(%clientId,"HealBurstCounter",2*%rate,"inc");
        
    }
    
    if(%clientId.currentShop != "")
    {
        %pos = Gamebase::getPosition(%clientId);
        if(Vector::getDistance(Gamebase::getPosition(%clientId.currentShop),%pos) > $maxSAYdistVec)
        {
            ClearCurrentShopVars(%clientId);
            Client::setGuiMode(%clientId, $GuiModePlay);
        }
    }
    
    if(%clientId.currentBank != "")
    {
        %pos = Gamebase::getPosition(%clientId);
        if(Vector::getDistance(Gamebase::getPosition(%clientId.currentBank),%pos) > $maxSAYdistVec)
        {
            ClearCurrentShopVars(%clientId);
            Client::setGuiMode(%clientId, $GuiModePlay);
        }
    }
    
    if(%clientId.currentAnvil != "")
    {
        %obj = getWord(%clientId.currentAnvil,1);
        %pos = Gamebase::getPosition(%clientId);
        if(Vector::getDistance(Gamebase::getPosition(%obj),%pos) > $maxSAYdistVec)
        {
            ClearCurrentShopVars(%clientId);
            Client::setGuiMode(%clientId, $GuiModePlay);
        }
    }
    
    //-----------------------------------------------------------
	// Do passive mana regen ticks
	//-----------------------------------------------------------
    //ManaRegenTick(%clientId);
    
    //if(%clientId.sleepMode == 2 && fetchData(%clientId, "Stamina") < fetchData(%clientId,"MaxStam"))
    //{
    //    UseSkill(%clientId, $SkillEnergy, True, True);
    //}
    
	//-----------------------------------------------------------
	// Check if the player has moved since last ZoneCheck
	//-----------------------------------------------------------
    if(%pos == "")
        %pos = GameBase::getPosition(%clientId);
	if(%pos != %clientId.zoneLastPos && !IsDead(%clientId))
	{
        if(%clientId.isMoving == 0)
        {
            %clientId.isMoving = 1;
            %clientId.isAtRestCounter = 0;
            %clientId.isAtRest = 0;
            refreshHPREGEN(%clientId,%zoneflag);
            //refreshStaminaREGEN(%clientId);
        }
		//train Weight Capacity
		if(OddsAre(8))
			UseSkill(%clientId, $SkillWeightCapacity, True, True, "", True);

		//cycle thru orbs
		for(%i = 1; (%orb = $ItemList[Orb, %i]) != ""; %i++)
		{
			if(OddsAre($BurnOut[%orb]))
			{
				if(Player::getItemCount(%clientId, %orb @ "0"))
				{
					Client::sendMessage(%clientId, $MsgRed, "Your " @ %orb.description @ " has burned out.");
					RPGItem::decItemCount(%clientId, %orb @ "0", 1);
					RefreshAll(%clientId,true);
				}
			}
			if($BurnOutInRain[%orb] > 0)
			{
				if(fetchData(%clientId, "zone") == "" && $isRaining)
				{
					if(OddsAre($BurnOutInRain[%orb]))
					{
						if(Player::getItemCount(%clientId, %orb @ "0"))
						{
							Client::sendMessage(%clientId, $MsgRed, "The rain has burned out your " @ %orb.description @ ".");
							RPGItem::decItemCount(%clientId, %orb @ "0", 1);
							RefreshAll(%clientId,true);
						}
					}
				}
			}
		}

		//hard-coded list to save on CPU
		for(%z = 1; $ItemList[Badge, %z] != ""; %z++)
		{
			if(Player::getItemCount(%clientId, $ItemList[Badge, %z]))
			{
				%a = GetWord($BonusItem[$ItemList[Badge, %z]], 0);
				%b = GetWord($BonusItem[$ItemList[Badge, %z]], 1);
				%c = GetWord($BonusItem[$ItemList[Badge, %z]], 2);

				if(OddsAre(%c))
					GiveThisStuff(%clientId, %a @ " " @ %b, True);
			}
		}

		//perhaps leave scent
		if(!fetchData(%clientId, "invisible"))
		{
			if(OddsAre(floor(CalculatePlayerSkill(%clientId, $SkillSenseHeading) / 100)+1))
			{
				storeData(%clientId, "lastScent", GameBase::getPosition(%clientId));
                storeData(%clientId, "lastScentZone", fetchData(%clientId,"zone"));
			}
		}
	}
    else if(%pos == %clientId.zoneLastPos && %clientId.sleepMode == "")
    {
        if(%clientId.isMoving == 1)
        {
            %clientId.isMoving = 0;
            refreshHPREGEN(%clientId,fetchData(%clientId, "zone"));
            
            //refreshStaminaREGEN(%clientId);
        }
        if(%clientId.isAtRestCounter > 2)
        {
            %clientId.isAtRest = 1;
            refreshHPREGEN(%clientId,fetchData(%clientId, "zone"));
            if(fetchData(%clientId,"HP") < fetchData(%clientId,"MaxHP"))
            {
                UseSkill(%clientId, $SkillHealing, True, True,12);
            }
            //refreshStaminaREGEN(%clientId);
        }
        else
            %clientId.isAtRestCounter++;
    }
	%clientId.zoneLastPos = %pos;

	storeData(%clientId, "tmpzone", "");
}

function gravWorkaround(%clientId, %method)
{
	dbecho($dbechoMode, "gravWorkaround(" @ %clientId @ ", " @ %method @ ")");

	if(%method == 1)
	{
		%rzdelay = 2;
		%steps = 24;

		for(%i = 0; %i < %steps; %i++)
		{
			%d = %i / (%steps / %rzdelay);
			schedule("Player::applyImpulse(" @ %clientId @ ", \"0 0 13\");", %d, %clientId);
		}
	}
	else if(%method == 2)
	{
		if($xyvel == "") $xyvel = 0.8;
		if($nzvel == "") $nzvel = 0.2;
		if($pzvel == "") $pzvel = 1.0;
		if($impulse == "") $impulse = 4;

		Player::applyImpulse(%clientId, "0 0 " @ $impulse);

		%vel = Item::getVelocity(%clientId);
		
		%xvel = GetWord(%vel, 0) * $xyvel;
		%yvel = GetWord(%vel, 1) * $xyvel;
		%zvel = GetWord(%vel, 2);

		if(%zvel < 0)
			%zvel *= $nzvel;
		else
			%zvel *= $pzvel;

		%nvel = %xvel @ " " @ %yvel @ " " @ %zvel;

		Item::setVelocity(%clientId, %nvel);
	}
}

function Zone::DoEnter(%z, %clientId)
{
	dbecho($dbechoMode, "Zone::DoEnter(" @ %z @ ", " @ %clientId @ ")");
    
	%oldZone = fetchData(%clientId, "zone");
	%newZone = $Zone::FolderID[%z];
	storeData(%clientId, "zone", $Zone::FolderID[%z]);

	if($Zone::Type[%z] == "PROTECTED")
	{
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".  This is protected territory.";
        storeData(%clientId,"HealBurstRate",100);
		%color = $MsgBeige;
	}
	else if($Zone::Type[%z] == "DUNGEON")
	{
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".  Beware of enemies!";
        storeData(%clientId,"HealBurstRate",1);
		%color = $MsgRed;
	}
	else if($Zone::Type[%z] == "WATER")
	{
		%msg = "";
	}
	else if($Zone::Type[%z] == "FREEFORALL")
	{
        storeData(%clientId,"HealBurstRate",1);
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".";
		%color = $MsgRed;
	}
    %txtMsg = %msg;
	if($Zone::EnterSound[%z] != "")
		%msg = %msg @ "~w" @ $Zone::EnterSound[%z];
    
    //echo(%z @" "@ %msg);
	if(%msg != "")
    {
		Client::sendMessage(%clientId, %color, %msg);
        remoteEval(%clientId,"ZONEText",%txtMsg);
    }

	if(!Player::isAiControlled(%clientId))
    {
		Game::refreshClientScore(%clientId);	//this is so players can see which zone this client is in
        if(!$Zone::Active[%z])
        {
            Zone::RollAllSpawnDelays(%z);
            $Zone::Active[%z] = true;
        }
    }

	Zone::onEnter(%clientId, %oldZone, %newZone);
}

function Zone::DoExit(%z, %clientId)
{
	dbecho($dbechoMode, "Zone::DoExit(" @ %z @ ", " @ %clientId @ ")");

	%zoneLeft = fetchData(%clientId, "zone");

	storeData(%clientId, "zone", "");

	if($Zone::Type[%z] == "PROTECTED")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgRed;
	}
	else if($Zone::Type[%z] == "DUNGEON")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgBeige;
	}
	else if($Zone::Type[%z] == "WATER")
	{
		%msg = "";
	}
	else if($Zone::Type[%z] == "FREEFORALL")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgBeige;
	}
    storeData(%clientId,"HealBurstRate",5);

	//Repack zone exit
	if(%clientId.repack){
		remoteeval(%clientId,RSound,3);
		%clientId.MusicTicksLeft = 0;
	}

	if($Zone::ExitSound[%z] != "")
		%msg = %msg @ "~w" @ $Zone::ExitSound[%z];

	if(%msg != "")
	      Client::sendMessage(%clientId, %color, %msg);

	if(!Player::isAiControlled(%clientId))
		Game::refreshClientScore(%clientId);	//this is so players can see which zone this client is in

	Zone::onExit(%clientId, %zoneLeft);
}

function IsInBetween(%x, %r1, %r2)
{
	dbecho($dbechoMode, "IsInBetween(" @ %x @ ", " @ %r1 @ ", " @ %r2 @ ")");

	if(%r1 > %r2)
	{
		%tmp = %r1;
		%r1 = %r2;
		%r2 = %tmp;
	}
	if(%x >= %r1 && %x <= %r2)
		return True;
	else
		return False;
}

function Zone::getIndex(%z)
{
	dbecho($dbechoMode, "Zone::getIndex(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::FolderToID[%z];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return %i;
		//	}
		//}
	}
	return -1;
}
function Zone::getMarker(%z)
{
	dbecho($dbechoMode, "Zone::getMarker(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::Marker[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::Marker[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getType(%z)
{
	dbecho($dbechoMode, "Zone::getType(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::Type[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::Type[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getDesc(%z)
{
	dbecho($dbechoMode, "Zone::getDesc(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::Desc[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::Desc[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getEnterSound(%z)
{
	dbecho($dbechoMode, "Zone::getEnterSound(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::EnterSound[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::EnterSound[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getAmbientSound(%z)
{
	dbecho($dbechoMode, "Zone::getAmbientSound(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::AmbientSound[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::AmbientSound[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getAmbientSoundPerc(%z)
{
	dbecho($dbechoMode, "Zone::getAmbientSoundPerc(" @ %z @ ")");

	if(%z != "")
	{
        return $Zone::AmbientSoundPerc[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::AmbientSoundPerc[%i];
		//	}
		//}
	}
	return -1;
}
function Zone::getExitSound(%z)
{
	dbecho($dbechoMode, "Zone::getExitSound(" @ %z @ ")");

        
	if(%z != "")
	{
        return $Zone::ExitSound[$Zone::FolderToID[%z]];
		//for(%i = 1; %i <= $numZones; %i++)
		//{
		//	if($Zone::FolderID[%i] == %z)
		//	{
		//		return $Zone::ExitSound[%i];
		//	}
		//}
	}
	return -1;
}

function Zone::onEnter(%clientId, %oldZone, %newZone)
{
	dbecho($dbechoMode, "Zone::onEnter(" @ %clientId @ ", " @ %oldZone @ ", " @ %newZone @ ")");

	refreshHPREGEN(%clientId,%newZone);	//this is because you regen faster or slower depending on the zone you are in
    
	if(Zone::getType(%newZone) == "WATER")
	{
		//Client::sendMessage(%clientId, $MsgBeige, "You have entered water!");
        remoteeval(%clientId, "client::setOverlayColor", 0, 0, 1, 0.5);
		storeData(%clientId, "drownCounter", "");
	}
	if(Zone::getType(%newZone) == "PROTECTED")
	{
		if(fetchData(%clientId, "isMimic"))
		{
			storeData(%clientId, "RACE", Client::getGender(%clientId) @ "Human");
			storeData(%clientId, "isMimic", "");
			UpdateTeam(%clientId);
			RefreshAll(%clientId,true);

			playSound(AbsorbABS, GameBase::getPosition(%clientId));
		}
	}
}

function Zone::onExit(%clientId, %zoneLeft)
{
	dbecho($dbechoMode, "Zone::onExit(" @ %clientId @ ", " @ %zoneLeft @ ")");

	refreshHPREGEN(%clientId);	//this is because you regen faster or slower depending on the zone you are in
    
    if(getWordCount(Zone::getPlayerList(%zoneLeft, 2)) == 0)
    {
        $Zone::Active[Zone::getIndex(%zoneLeft)] = false;
    }
    
    %type = Zone::getType(%zoneLeft);
    
    if($CleanUpBotsOnZoneEmpty &&  (%type == "DUNGEON" || %type == "FREEFORALL"))
    {
        %isAi = Player::isAiControlled(%clientId);
        if(%isAi && !$ZoneCleanupProtected[%clientId])
        {
            storeData(%clientId,"noDropLootbagFlag",true);
            Player::Kill(%clientId);
            return;
        }
        
        
        if(!$Zone::Active[Zone::getIndex(%zoneLeft)])
        {
            
            %bots = Zone::getPlayerList(%zoneLeft, 3);
            for(%i = 0; %i < getWordCount(%bots); %i++)
            {
                %b = getWord(%bots,%i);
                if(!$ZoneCleanupProtected[%b])
                {
                    storeData(%b,"noDropLootbagFlag",true);
                    Player::Kill(%b);
                }
            }
        }
    }
    
	if(Zone::getType(%zoneLeft) == "WATER")
	{
		//Client::sendMessage(%clientId, $MsgBeige, "You have left water!");
		storeData(%clientId, "drownCounter", "");
        remoteeval(%clientId, "client::setOverlayColor", 0, 0, 0, 0);
	}
}

function GetNearestZone(%clientId, %zonetype, %returnType)
{
	dbecho($dbechoMode, "GetNearestZone(" @ %clientId @ ", " @ %zonetype @ ", " @ %returnType @ ")");

	//%zonetype can be "town", "dungeon" or "freeforall"

	%closestDist = 500000;
	%closestZone = "";
	%mpos = "";
	%clpos = GameBase::getPosition(%clientId);
    
    %realm = fetchData(%clientId,"Realm");
    
    for(%i = 0; %i <= getWordCount($RealmData[%realm,ZoneList]); %i++)
    {
        %zid = getWord($RealmData[%realm,ZoneList],%i);
        %type = $Zone::Type[%zid];
        if( (%type == "PROTECTED" && String::ICompare(%zonetype, "town") == 0 ) || (%type == "DUNGEON" && String::ICompare(%zonetype, "dungeon") == 0 ) || (%type == "FREEFORALL" && String::ICompare(%zonetype, "freeforall") == 0 ) || %zonetype == -1)
		{
            %finalpos = $Zone::Marker[%zid];
            
            %dist = Vector::getDistance(%finalpos, %clpos);
            if(%dist < %closestDist)
			{
				%closestDist = %dist;
				%closestZoneDesc = $Zone::Desc[%zid];
				%closestZone = $Zone::FolderID[%zid];
				%mpos = %finalpos;
			}
        }
    }
    
    // Old Code
	//for(%i = 1; %i <= $numZones; %i++)
	//{
	//	%type = $Zone::Type[%i];
	//	if(%type == "PROTECTED" && String::ICompare(%zonetype, "town") == 0 || %type == "DUNGEON" && String::ICompare(%zonetype, "dungeon") == 0 || %type == "FREEFORALL" && String::ICompare(%zonetype, "freeforall") == 0 || %zonetype == -1)
	//	{
	//		%finalpos = $Zone::Marker[%i];
	//
	//		%dist = Vector::getDistance(%finalpos, %clpos);
	//		if(%dist < %closestDist)
	//		{
	//			%closestDist = %dist;
	//			%closestZoneDesc = $Zone::Desc[%i];
	//			%closestZone = $Zone::FolderID[%i];
	//			%mpos = %finalpos;
	//		}
	//	}
	//}

	if(%mpos == "")		//no zones were found (this means there are NO zones in the map...)
		return False;
	
	//%returnType:
	//1 = returns the distance from the client to the nearest zone
	//2 = returns the description of the zone nearest to the client
	//3 = returns the zone id of the zone nearest to the client
	//4 = returns the position of the middle of the zone nearest to the client

	if(%returnType == 1)
		return %closestDist;
	else if(%returnType == 2)
		return %closestZoneDesc;
	else if(%returnType == 3)
		return %closestZone;
	else if(%returnType == 4)
		return %mpos;
}

function GetZoneByKeywords(%clientId, %keywords, %returnType, %realm)
{
	dbecho($dbechoMode, "GetZoneByKeywords(" @ %clientId @ ", " @ %keywords @ ", " @ %returnType @ ")");

    if(%realm == "") // Grab them all
    {
        for(%i = 1; %i <= $ZoneData::NumberZones; %i++)
        {
            if(String::findSubStr($Zone::Desc[%i], %keywords) != -1)
			{                
                if(%returnType == 1)
                    return Vector::getDistance($Zone::Marker[%i], GameBase::getPosition(%clientId));
                else if(%returnType == 2)
                    return $Zone::Desc[%i];
                else if(%returnType == 3)
                    return $Zone::FolderID[%i];
            }
        }
        return False;
    }
    else
    {
        for(%i = 0; %i < getWordCount($RealmData[%realm,ZoneList]); %i++)
        {
            %zid = getWord($RealmData[%realm,ZoneList],%i);
            if(String::findSubStr($Zone::Desc[%zid], %keywords) != -1)
			{
                if(%returnType == 1)
                    return Vector::getDistance($Zone::Marker[%zid], GameBase::getPosition(%clientId));
                else if(%returnType == 2)
                    return $Zone::Desc[%zid];
                else if(%returnType == 3)
                    return $Zone::FolderID[%zid];
            }
        }
        return False;
    }
    return False;
	//%group = nameToId("MissionGroup\\Zones");
    //
	//if(%group != -1)
	//{
	//	//IMPORTANT: zone markers must be objects 0 and 1 in the zone's folder
    //
	//	%count = Group::objectCount(%group);
	//	for(%i = 0; %i <= %count-1; %i++)
	//	{
	//		%object = Group::getObject(%group, %i);
	//		%system = Object::getName(%object);
	//		%type = GetWord(%system, 0);
	//		%desc = String::getSubStr(%system, String::len(%type)+1, 9999);
	//		if(%type == "PROTECTED" || %type == "DUNGEON" || %type == "FREEFORALL")
	//		{
	//			if(String::findSubStr(%desc, %keywords) != -1)
	//			{
	//				//get the two markers
	//				%tmpgroup = nameToId("MissionGroup\\Zones\\" @ %system);
    //
	//				%m1pos = GameBase::getPosition(Group::getObject(%tmpgroup, 0));
	//				%m2pos = GameBase::getPosition(Group::getObject(%tmpgroup, 1));
    //
	//				%mx = (((GetWord(%m2pos, 0) - GetWord(%m1pos, 0)) / 2) + GetWord(%m1pos, 0));
	//				%my = (((GetWord(%m2pos, 1) - GetWord(%m1pos, 1)) / 2) + GetWord(%m1pos, 1));
	//				%mz = (((GetWord(%m2pos, 2) - GetWord(%m1pos, 2)) / 2) + GetWord(%m1pos, 2));
    //
	//				%mpos = %mx @ " " @ %my @ " " @ %mz;
	//				%dist = Vector::getDistance(%mpos, GameBase::getPosition(%clientId));
    //
	//				//%returnType:
	//				//1 = returns the distance from the client to the zone
	//				//2 = returns the description of the zone
	//				//3 = returns the zone id
	//				//4 = returns the position of the middle of the zone
    //
	//				if(%returnType == 1)
	//					return %dist;
	//				else if(%returnType == 2)
	//					return %desc;
	//				else if(%returnType == 3)
	//					return %object;
	//				else if(%returnType == 4)
	//					return %mpos;
	//			}
	//		}
	//	}
	//	return False;	
	//}
	//else
	//	return False;
}

function Zone::getNumPlayers(%z, %all)
{
	dbecho($dbechoMode, "Zone::getNumPlayers(" @ %z @ ", " @ %all @ ")");

	if(%all)
		%list = GetEveryoneIdList();
	else
		%list = GetPlayerIdList();

	%n = 0;
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%id = GetWord(%list, %i);

		if(fetchData(%id, "zone") == %z)
			%n++;
	}

	return %n;
}

function ObjectInWhichZone(%object)
{
	dbecho($dbechoMode, "ObjectInWhichZone(" @ %object @ ")");

	//not perfect but good enough

	%fid = "";
	%closest = 99999;
	%objpos = GameBase::getPosition(%object);
	for(%z = 1; %z <= $ZoneData::NumberZones; %z++)
	{
		%rad = ($Zone::Length[%z] + $Zone::Width[%z] + $Zone::Height[%z]) / 3;
		%dist = Vector::getDistance(%objpos, $Zone::Marker[%z]);
		if(%dist <= %rad)
		{
			if(%dist < %closest)
			{
				%closest = %dist;
				%fid = $Zone::FolderID[%z];
			}
		}
	}
	return %fid;
}

function Zone::getPlayerList(%z, %type)
{
	dbecho($dbechoMode, "Zone::getPlayerList(" @ %z @ ", " @ %type @ ")");

	if(%type == 1)
		%list = GetEveryoneIdList();
	else if(%type == 2)
		%list = GetPlayerIdList();
	else if(%type == 3)
		%list = GetBotIdList();

	%n = 0;
	%aa = "";
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%id = GetWord(%list, %i);

		if(fetchData(%id, "zone") == %z)
			%aa = %aa @ %id @ " ";
	}

	return %aa;
}
