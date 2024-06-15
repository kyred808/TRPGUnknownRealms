
// Set true at bottom of map file to enable realms
//$Realms::MapUsesRealms = true;

//$RealmData::RealmIdToLabel[0] = "keldrinia";
//$RealmData::RealmIdToLabel[1] = "oldworld";
//
//$RealmData["keldrinia", ID] = 0;
//$RealmData["keldrinia", Name] = "Keldrinia";
//$RealmData["keldrinia", MinHeight] = -2000;
//$RealmData["keldrinia", MaxHeight] = 2000;
//
//
//$RealmData["oldworld", ID] = 1;
//$RealmData["oldworld", MinHeight] = -6000;
//$RealmData["oldworld", MaxHeight] = -2000;
//$RealmData["oldworld", Name] = "Old World";

$RealmBounds[0,Xmin] = -5120;
$RealmBounds[0,Xmax] = -5120 + $MapExtent[0] + 1000; //2x
$RealmBounds[0,Ymin] = -3072;
$RealmBounds[0,Ymax] = -3072 + $MapExtent[1];

$RealmHeight[0] = 0;
$RealmHeight[1] = -4000;
$RealmHeight[2] = 4000;

function Realms::getRealmGroupName(%realmId)
{
    return "MissionGroup\\Realm"@%i;
}

function InitRealms()
{
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        %group = nameToID("MissionGroup\\Realm"@%i);

        %tmpcount = Group::objectCount(%group);
        %realm_name = "";
        %label = "";
        %dims = "";
        %spawnPt = "";
        for(%z = 0; %z < %tmpcount; %z++)
		{
            
            %realmGpObj = Group::getObject(%group, %z);
            %type = getObjectType(%realmGpObj);
            %name = Object::getName(%realmGpObj);
            %w0 = getWord(%name,0);
            if(%type == "SimGroup")
			{
				%name = Object::getName(%realmGpObj);
                %w0 = getWord(%name,0);
                echo("Name: "@ %name @" W0: "@ %w0);
                if(%w0 == "REALM_LABEL")
                {
                    %label = getWord(%name,1);
                }
                else if(%w0 == "REALM_NAME")
                {
                    %realm_name = Word::getSubWord(%name,1,999);
                }
                else if(%w0 == "REALM_DIMS")
                {
                    %dims = Word::getSubWord(%name,1,2);
                }
            }
            else if(%type == "Marker")
            {
                echo("Marker: "@ %w0);
                if(%w0 == "REALM_DROP_POINT")
                    %spawnPt = %realmGpObj;
            }
        }
        if(%label != "")
        {
            $RealmData::RealmIdToLabel[%i] = %label;
            $RealmData::RealmGroupToLabel[%group] = %label;
            $RealmData[%label, ID] = %i;
            $RealmData[%label, Name] = %realm_name;
            $RealmData[%label, MinHeight] = getWord(%dims,0);
            $RealmData[%label, MaxHeight] = getWord(%dims,1);
            $RealmData[%label, SpawnMarker] = %spawnPt;
            
            $RealmData[%label, Active] = false;
            $RealmData[%label, ForceActive] = false;
            $RealmData[%label, CrystalCount] = 0;
            
            echo("Realm"@ %i@": "@%label @" aka "@ %name @" ["@$RealmData[%label, MinHeight] @","@$RealmData[%label, MaxHeight] @"]");
            
            %realmCleanupSet = newObject(%label, SimGroup);
            addToSet("MissionCleanup",%realmCleanupSet);
        }
        else
        {
            echo("ERROR: Realm "@%i@" had no REALM_LABEL");
        }
    }
    
    
}

function Realms::UpdateRealm(%realm)
{
    //echo("REALM UPDATE: "@ %realm);
    %time = getSimTime();
    //Check crystals
    //echo("Realm Crystal Count: " @ $RealmData[%realm,CrystalCount]);
    for(%i = 0; %i < $RealmData[%realm,CrystalCount]; %i++)
    {
        //echo("Crystal Info "@%i@": S - "@ $RealmData[%realm,CrystalSpawned,%i] @" R - "@$RealmData[%realm,CrystalRespawnTime,%i]);
        //echo("Crystal "@%i@" Check: "@ $RealmData[%realm,CrystalSpawned,%i]);
        if(!$RealmData[%realm,CrystalSpawned,%i] && $RealmData[%realm,CrystalRespawnTime,%i] <= %time)
        {
            %cmkr = $RealmData[%realm,CrystalMarkerList,%i];
            %crystal = newObject("GemCrystal",StaticShape,"GemCrystal",true);
            %crystal.id = %realm @" "@ %i;
            
            Gamebase::setPosition(%crystal,Gamebase::getPosition(%cmkr));
            Gamebase::setRotation(%crystal,Gamebase::getRotation(%cmkr));
            
            for(%k = 1; %cmkr.bonus[%k] != ""; %k++)
            {
                %crystal.bonus[%k] = %cmkr.bonus[%k];
            }
            Gamebase::startFadeIn(%crystal);
            addToSet("MissionCleanup\\"@%realm,%crystal);
            $RealmData[%realm,CrystalSpawned,%i] = true;
        }
    }
    
    for(%i = 0; %i < $RealmData[%realm,CrystalGroupCount]; %i++)
    {
        //echo(%realm);
        //echo("Group Count: "@ $RealmData[%realm,CrystalGroupCount]);
        //See if we are ready to check
        if($RealmData[%realm,CrystalGroup,%i,NextSpawnCheck] <= %time)
        {
            if($RealmData[%realm,CrystalGroup,%i,ActiveCrystalCnt] < $RealmData[%realm,CrystalGroup,%i,MaxCrystalsAtOnce])
            {
                %crList = "";
                %crCnt = 0;
                for(%k = 0; %k < $RealmData[%realm,CrystalGroup,%i,CrystalCount]; %k++)
                {
                    if(!$RealmData[%realm,CrystalGroup,%i,Crystal,%k,Active])
                    {
                        %crList = %crList @ %k @ " ";
                        %crCnt++;
                    }
                }
                
                %newIdx = getWord(%crList,getIntRandomMT(0,%crCnt-1));
                %crystal = Realms::SpawnCrystalFromMarker($RealmData[%realm,CrystalGroup,%i,CrystalMkr,%newIdx],%realm);
                $RealmData[%realm,CrystalGroup,%i,ActiveCrystalCnt]++;
                $RealmData[%realm,CrystalGroup,%i,Crystal,%newIdx,Active] = true;
                %crystal.id = %realm @" "@ %i @" "@ %newIdx;
                
                %mkr = $RealmData[%realm,CrystalGroup,%i,CrystalMkr,%newIdx];
                //echo("Marker: "@%mkr);
                if(%mkr.mhp != "")
                {
                    %mean = %mkr.mhp;
                    %var = %mkr.hpVar;
                    if(%var == "")
                        %var = $OreCrystalData::defaultHPVariance;
                    //echo("Mean: "@ %mean);
                    //echo("Var: "@ %var);
                    %crystal.hp = getIntRandomMT(%mean-%var,%mean+%var);
                    //echo(%crystal.hp);
                }
            }
            $RealmData[%realm,CrystalGroup,%i,NextSpawnCheck] = %time + getIntRandomMT($RealmData[%realm,CrystalGroup,%i,SpawnTimeMin],$RealmData[%realm,CrystalGroup,%i,SpawnTimeMax]);
        }
    }
}

function Realms::SpawnCrystalFromMarker(%marker,%realm)
{
    %type = %marker.crystalType;
    if(%type == "OreCrystal")
    {
        %totalWeight = 0; //Not heaviness, but weighted average weight
        %k = 0;
        for(%i = 0; %i < getWordCount(%marker.oreType); %i+=2)
        {
            %tag[%k] = getWord(%marker.oreType,%i);
            %we = getWord(%marker.oreType,%i+1);
            %weight[%k] = %we+%totalWeight;
            %totalWeight += %we;
            %k++;
        }
        
        %mm = getRandomMT()*%totalWeight;
        %select = "";
        //Select ore from weighted average
        for(%i = 0; %tag[%i] != ""; %i++)
        {
            //echo(%mm @" < "@ %weight[%i]);
            if(%mm < %weight[%i])
            {
                %select = %tag[%i];
                break;
            }
        }
        //echo("Selected Ore: "@ %select);
        %crystal = newObject("OreCrystal",StaticShape,"OreCrystal",true);
        Gamebase::setPosition(%crystal,Gamebase::getPosition(%marker));
        Gamebase::setRotation(%crystal,Gamebase::getRotation(%marker));
        %crystal.oreTag = %select;
        Gamebase::startFadeIn(%crystal);
        addToSet("MissionCleanup\\"@%realm,%crystal);
        
        echo("Crystal Spawned! "@ %crystal);
        return %crystal;
    }
}

function Realms::InitZones()
{
    deleteVariables("Zone::*");
    $ZoneData::NumberZones = 0;
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeZone("MissionGroup\\Realm"@%i@"\\Zones",$RealmData::RealmIdToLabel[%i]);
    }
}

function Realms::InitCrystals()
{
	//dbecho($dbechoMode, "Realms::InitCrystals()");
    echo("Realms Init Crystals!");
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeCrystals("MissionGroup\\Realm"@%i@"\\Crystals",$RealmData::RealmIdToLabel[%i]);
        Realms::InitializeCrystalGroups("MissionGroup\\Realm"@%i@"\\CrystalGroups",$RealmData::RealmIdToLabel[%i]);
    }
}

function Realms::InitializeCrystals(%groupPath,%realm)
{
    %group = nameToID(%groupPath);
    echo("Crystals: "@%group);
	if(%group != -1)
	{
		for(%i = 0; %i < Group::objectCount(%group); %i++)
		{
			%this = Group::getObject(%group, %i);
            echo(%this.bonus[1]);
			%info = Object::getName(%this);
            echo("Crystal: "@%info);
			if(%info != "")
			{
				%cnt = 0;
				for(%z = 0; (%p1 = GetWord(%info, %z)) != -1; %z+=2)
				{
					%p2 = GetWord(%info, %z+1);
					%this.bonus[%cnt++] = %p2;
				}
                %num = $RealmData[%realm,CrystalCount];
                $RealmData[%realm,CrystalMarkerList,%num] = %this;
                $RealmData[%realm,CrystalSpawned,%num] = false;
                $RealmData[%realm,CrystalRespawnTime,%num] = 0;
                $RealmData[%realm,CrystalCount]++;
			}
		}
	}
}

function Realms::InitializeCrystalGroups(%groupPath,%realm)
{
    %group = nameToID(%groupPath);
    echo("CrystalSimGroup: "@ %group);
    $RealmData[%realm,CrystalGroupCount] = 0;
    if(%group != -1)
	{
        for(%i = 0; %i < Group::objectCount(%group); %i++)
		{
            %obj = Group::getObject(%group,%i);
            %type = getObjectType(%obj);
            if(%type == "SimGroup")
                Realms::RegisterCrystalGroup(%obj,%realm);
        }
    }
}

function Realms::RegisterCrystalGroup(%group,%realm)
{
    echo("Crystal Group: "@%group);
    %grpIdx = $RealmData[%realm,CrystalGroupCount];
    $RealmData[%realm,CrystalGroup,%grpIdx] = %group;
    $RealmData[%realm,CrystalGroup,%grpIdx,NextSpawnCheck] = 0;
    $RealmData[%realm,CrystalGroup,%grpIdx,MaxCrystalsAtOnce] = 0;
    $RealmData[%realm,CrystalGroup,%grpIdx,CrystalCount] = 0;
    $RealmData[%realm,CrystalGroupCount]++;
    for(%i = 0; %i <= Group::objectCount(%group)-1; %i++)
    {
        %obj = Group::getObject(%group,%i);
        %type = getObjectType(%obj);
        $RealmData[%realm,CrystalGroup,%grpIdx,ActiveCrystalCnt] = 0;
        if(%type == "SimGroup")
        {
            %nn = Object::getName(%obj);
            $RealmData[%realm,CrystalGroup,%grpIdx,MaxCrystalsAtOnce] = getWord(%nn,0);
            $RealmData[%realm,CrystalGroup,%grpIdx,SpawnTimeMin] = round(getWord(%nn,1));
            $RealmData[%realm,CrystalGroup,%grpIdx,SpawnTimeMax] = round(getWord(%nn,2));
        }
        else
        {
            echo("CrystalMkr: " @ %obj @ " - " @%type);
            echo("Crystal Type: "@ %obj.crystalType);
            %cidx = $RealmData[%realm,CrystalGroup,%grpIdx,CrystalCount];
            $RealmData[%realm,CrystalGroup,%grpIdx,Crystal,%cidx,Active] = false;
            $RealmData[%realm,CrystalGroup,%grpIdx,CrystalMkr,%cidx] = %obj;
            $RealmData[%realm,CrystalGroup,%grpIdx,CrystalCount]++;
        }
    }
}

function Realms::InitializeZone(%groupPath,%realm)
{
    %zcnt = 0;
	%umusiccnt = 0;
    %group = nameToID(%groupPath);
    if(%group != -1)
	{
		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%system = Object::getName(%object);
			%type = GetWord(%system, 0);
			%desc = String::getSubStr(%system, String::len(%type)+1, 9999);
            //echo(%system);
			//---------------------------------------------------------------
			//THIS PART GATHERS SOUNDS FOR THE GENERIC UNKNOWN ZONE
			// there is no EXIT sound for the unknown zone.
			//---------------------------------------------------------------
			if(GetWord(%system, 0) == "ENTERSOUND")
			{	
				$Zone::EnterSound[0] = GetWord(%system, 1);
			}
			else if(GetWord(%system, 0) == "AMBIENTSOUND")
			{
				$Zone::AmbientSound[0] = GetWord(%system, 1);
				$Zone::AmbientSoundPerc[0] = GetWord(%system, 2);
			}
			else if(GetWord(%system, 0) == "MUSIC")
			{
                
				$Zone::Music[0, %umusiccnt++] = GetWord(%system, 1);
				$Zone::MusicTicks[0, %umusiccnt] = GetWord(%system, 2);
			}
			//---------------------------------------------------------------
			else
			{
                %zoneGrp = nameToId(%groupPath @"\\" @ %system);
				%zoneId = Zone::addZone(%zoneGrp);
                
                $RealmData[%realm,ZoneList] = $RealmData[%realm,ZoneList] @" "@%zoneId;
                $Zone::Realm[%zoneId] = %realm;
			}
		}
		echo($ZoneData::NumberZones @ " zones initialized.");
	}
}

function Realms::InitSpawnPoints()
{
  // for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
  // {
  //     Realms::InitializeSpawnPoints("MissionGroup\\Realm"@%i@"\\SpawnPoints",$RealmData::RealmIdToLabel[%i]);
  // }
}

function Realms::InitializeSpawnPoints(%groupPath,%realm)
{
    %group = nameToID(%groupPath);

	if(%group != -1)
	{
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++)
		{
		      %this = Group::getObject(%group, %i);
			%info = Object::getName(%this);

			$MarkerZone[%this] = ObjectInWhichZone(%this);

			if(%info != "")
			{
				$numAIperSpawnPoint[%this] = 0;
				%indexes = "";

				for(%z = 5; GetWord(%info, %z) != -1; %z++)
					%indexes = %indexes @ GetWord(%info, %z) @ " ";

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

				SpawnLoop(%this);
			}
		}
	}
}

function Realms::InitTownBots()
{
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeTownBots("MissionGroup\\Realm"@%i@"\\TownBots",$RealmData::RealmIdToLabel[%i]);
    }
}

function Realms::InitializeTownBots(%groupPath,%realmLabel)
{
    %group = nameToId(%groupPath);

	if(%group != -1)
	{
		%cnt = Group::objectCount(%group);
		for(%i = 0; %i <= %cnt - 1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%name = Object::getName(%object);
			if(getObjectType(%object) == "SimGroup")
			{
				%marker = GatherBotInfo(%object);
			}
            //echo($BotInfo[%name, RACE]);
            %townbot = newObject("",StaticShape,$BotInfo[%name, RACE] @"NPCTownBot",false);
			//%townbot = newObject("", "Item", $BotInfo[%name, RACE] @ "TownBot", 1, false);

			addToSet("MissionCleanup\\"@%realmLabel, %townbot);
			GameBase::setMapName(%townbot, $BotInfo[%name, NAME]);
			GameBase::setPosition(%townbot, GameBase::getPosition(%marker));
			GameBase::setRotation(%townbot, GameBase::getRotation(%marker));
			GameBase::setTeam(%townbot, $BotInfo[%name, TEAM]);
			GameBase::playSequence(%townbot, 0, "root");	//thanks Adger!!
			%townbot.name = %name;
            $BotInfo[%name, REALM] = %realmLabel;
            
			$TownBotList[$RealmData[%realmLabel, ID]] = $TownBotList[$RealmData[%realmLabel, ID]] @ %townbot @ " ";
		}
	}
}

function Realms::InitFerry()
{
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeFerry("MissionGroup\\Realm"@%i@"\\Ferry",$RealmData::RealmIdToLabel[%i]);
    }
}

function Realms::InitializeFerry(%groupPath,%realmLabel)
{
    %group = nameToId(%groupPath);

	if(%group != -1)
	{
		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%system = Object::getName(%object);
			%wferry = String::getSubStr(%system, String::len($FerryfolderNameForSystem), String::len(%system)-String::len($FerryfolderNameForSystem));

			//find %ferry id
			%g = nameToId(%groupPath @ "\\" @ %system);
			%c = Group::objectCount(%g);
			for(%k = 0; %k <= %c-1; %k++)
			{
				%o = Group::getObject(%g, %k);
				if(getObjectType(%o) == "Moveable")
					%ferry = %o;
			}

			$Ferry::FolderName[%ferry] = %groupPath @ "\\" @ %system;
            $Ferry::Realm[%ferry] = %realmLabel;
			//go thru all the markers / droppoints and perhaps do something?
			%groupForPath = nameToId(%groupPath@ "\\" @ %system @ "\\" @ $FerryfolderNameForPath);
			%countForPath = Group::objectCount(%groupForPath);
			for(%j = 0; %j <= %countForPath-1; %j++)
			{
				%o1 = Group::getObject(%groupForPath, %j);
				$Ferry::MarkerPos[%ferry, %j] = GameBase::getPosition(%o1);
				$Ferry::PauseTime[%ferry, %j] = floor((Object::getName(%o1)) * 100) / 100;
			}
		}
	}
}

function Realms::activateRealm(%realmLabel)
{
    $RealmData[%realLabel, Active] = true;
}

function Realms::deactivateRealm(%realLabel)
{
    $RealmData[%realLabel, Active] = false;
}

function Realms::onClientEnter(%realmLabel,%client,%echo)
{
    %isActive = $RealmData[%realmLabel, Active];
    
    if(!%isActive)
        Realms::activateRealm(%realmLabel);
}

// Called by recursive world
function PlayerRealmCheck()
{
    for(%c = Client::getFirst(); %c != -1; %c = Client::getNext(%c))
	{
        if(fetchData(%c,"HasLoadedAndSpawned") && fetchData(%c,"noRealmCheck") == "")
        {
            //%pos = Gamebase::getPosition(%c);
            //%zpos = getWord(%pos,2);
            %currentRealm = fetchData(%c,"Realm");
            %rid = $RealmData[%currentRealm, ID];
            %realmList[%rid] = true;
            // Find player's current Realm by Z position
            //if(%zpos > $RealmData[%currentRealm,MaxHeight] || %zpos < $RealmData[%currentRealm,MinHeight])
            //{
            //    Realm::KickPlayerBackInRealm(%c,%currentRealm);
            //}
        }
	}
    
    for(%i = 0; $RealmData::RealmIdToLabel[%i] != ""; %i++)
    {
        if(%realmList[%i] == "" && $RealmData[$RealmData::RealmIdToLabel[%i], Active])
        {
            Realms::deactivateRealm($RealmData::RealmIdToLabel[%i]);
        }
    }
}

function Realm::KickPlayerBackInRealm(%client,%realm)
{
    echo("Realm Kick!");
    if(%realm == "")
        %realm = fetchData(%client,"Realm");
    
    Item::setVelocity(%client, "0 0 0");
    Gamebase::setPosition(%client,Gamebase::getPosition($RealmData[%realm, SpawnMarker]));
    Client::sendMessage(%client, $MsgRed, "You kicked back into your current realm.~wError_Message.wav");
}

