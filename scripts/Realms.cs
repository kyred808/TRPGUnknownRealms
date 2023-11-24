
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

function Realms::InitZones()
{
    deleteVariables("Zone::*");
    $ZoneData::NumberZones = 0;
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeZone("MissionGroup\\Realm"@%i@"\\Zones",$RealmData::RealmIdToLabel[%i]);
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
            //echo(%name);
			%townbot = newObject("", "Item", $BotInfo[%name, RACE] @ "TownBot", 1, false);

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

