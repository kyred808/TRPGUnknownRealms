
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
        for(%z = 0; %z < %tmpcount; %z++)
		{
            %name = "";
            %label = "";
            %dims = "";
            %spawnPt = "";
            %realmGp = Group::getObject(%group, %z);
            %type = getObjectType(%realmGp);
            if(%type == "SimGroup")
			{
				%name = Object::getName(%realmGp);
                %w0 = getWord(%name,0);
                if(%w0 == "REALM_LABEL")
                {
                    %label = getWord(%name,1);
                }
                else if(%w0 == "REALM_NAME")
                {
                    %name = Word::getSubWord(%name,1,999);
                }
                else if(%w0 == "REALM_DIMS")
                {
                    %dims = Word::getSubWord(%name,1,2);
                }
            }
            else if(%type == "Marker")
            {
                %spawnPt = %realmGp;
            }
        }
        if(%label != "")
        {
            $RealmData::RealmIdToLabel[%i] = %label;
            $RealmData[%label, ID] = %i;
            $RealmData[%label, Name] = %name;
            $RealmData[%label, MinHeight] = getWord(%dims,0);
            $RealmData[%label, MaxHeight] = getWord(%dims,1);
            $RealmData[%label, SpawnMarker] = %spawnPt;
        }
        else
        {
            echo("ERROR: Realm "@%i@" had no REALM_LABEL");
        }
    }
}

function Realms::InitZones()
{
    $numZones = 0;
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeZone("MissionGroup\\Realm"@%i@"\\Zones",$RealmData::RealmIdToLabel[%i]);
    }
}

function Realms::InitializeZone(%groupPath,%realm)
{
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
            echo(%system);
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
				%zcnt++;

				%tmpgroup = nameToId(%groupPath @"\\" @ %system);
				%tmpcount = Group::objectCount(%tmpgroup);
				%marker = "";
				%musiccnt = 0;

				for(%z = 0; %z <= %tmpcount-1; %z++)
				{
					%tmpobject = Group::getObject(%tmpgroup, %z);
	
					if(getObjectType(%tmpobject) == "Marker")
					{
						if(%marker == "")
						{
							%marker = %tmpobject;
							$numZones++;
						}
					}
					else if(getObjectType(%tmpobject) == "SimGroup")
					{
						%n = Object::getName(%tmpobject);
						
						if(GetWord(%n, 0) == "ENTERSOUND")
						{	
							$Zone::EnterSound[%zcnt] = GetWord(%n, 1);
						}
						else if(GetWord(%n, 0) == "AMBIENTSOUND")
						{
							$Zone::AmbientSound[%zcnt] = GetWord(%n, 1);
							$Zone::AmbientSoundPerc[%zcnt] = GetWord(%n, 2);
						}
						else if(GetWord(%n, 0) == "EXITSOUND")
						{
							$Zone::ExitSound[%zcnt] = GetWord(%n, 1);
						}
						else if(GetWord(%n, 0) == "MUSIC")
						{
							$Zone::Music[%zcnt, %musiccnt++] = GetWord(%n, 1);
							$Zone::MusicTicks[%zcnt, %musiccnt] = GetWord(%n, 2);
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
                $Zone::Realm[%zcnt] = %realm;
                $RealmData[%realm,ZoneList] = $RealmData[%realm,ZoneList] @" "@%zcnt;
			}
		}
		echo($numZones @ " zones initialized.");
	}
}

function Realms::InitSpawnPoints()
{
    for(%i = 0; nameToID("MissionGroup\\Realm"@%i) != -1; %i++)
    {
        Realms::InitializeSpawnPoints("MissionGroup\\Realm"@%i@"\\SpawnPoints",$RealmData::RealmIdToLabel[%i]);
    }
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

// Called by recursive world
function PlayerRealmCheck()
{
    for(%c = Client::getFirst(); %c != -1; %c = Client::getNext(%c))
	{
        if(fetchData(%c,"noRealmCheck") == "")
            %zpos = getWord(Gamebase::getPosition(%c),2);
            %currentRealm = fetchData(%c,"realm");

            // Find player's current Realm by Z position
            
            if(%zpos > $RealmData[%currentRealm,MaxHeight] || %zpos < $RealmData[%currentRealm,MinHeight])
            {
                Realm::KickPlayerBackInRealm(%c,%currentRealm);
            }
        }
	}
}

function Realm::KickPlayerBackInRealm(%client,%realm)
{
    echo("Realm Kick!");
    if(%realm == "")
        %realm = fetchData(%client,"realm");
    
    Item::setVelocity(%client, "0 0 0");
    Gamebase::setPosition(%client,$RealmData[%realm, SpawnMarker]);
    Client::sendMessage(%id, $MsgRed, "You kicked back into your current realm.~wError_Message.wav");
}

