function Game::pickObserverSpawn(%clientId)
{
	dbecho($dbechoMode2, "Game::pickObserverSpawn(" @ %clientId @ ")");

	%group = nameToID("MissionGroup\\ObserverDropPoints");
	%count = Group::objectCount(%group);

	if(%group == -1 || !%count)
		%group = nameToID("MissionGroup\\Teams\\team0\\DropPoints");
	%count = Group::objectCount(%group);
	if(%group == -1 || !%count)
		%group = nameToID("MissionGroup\\Teams\\team1\\DropPoints");
	%count = Group::objectCount(%group);
	if(%group == -1 || !%count)
		return -1;
	%spawnIdx = %clientId.lastObserverSpawn + 1;
	if(%spawnIdx >= %count)
		%spawnIdx = 0;
	%clientId.lastObserverSpawn = %spawnIdx;

	return Group::getObject(%group, %spawnIdx);
}

function Game::pickPlayerSpawn(%clientId, %respawn)
{
	dbecho($dbechoMode2, "Game::pickPlayerSpawn(" @ %clientId @ ", " @ %respawn @ ")");

    if(%clientId.newPlayer)
    {
        %group = nameToID("MissionGroup/Realm0/Zones/"@$NewPlayerSpawnZone@"/DropPoints");
        echo("SpawnNewPlayer: "@ %group);
    }
    else
    {
        %realmId = $RealmData[fetchData(%clientId,"realm"), ID];
        if(fetchData(%clientId, "lastzone") == "")
            %group = nameToID("MissionGroup/Teams/team0/DropPoints");
        else
            %group = nameToID("MissionGroup/Realm"@%realmId@"/Zones/" @ Object::getName(fetchData(%clientId, "lastzone")) @ "/DropPoints");
    }
	%count = Group::objectCount(%group);
	if(!%count)
		return -1;
	%spawnIdx = floor(getRandom() * (%count - 0.1));
	%value = %count;

	for(%i = %spawnIdx; %i < %value; %i++)
	{
		%set = newObject("set",SimSet);
		%obj = Group::getObject(%group, %i);
		if(containerBoxFillSet(%set,$SimPlayerObjectType|$VehicleObjectType,GameBase::getPosition(%obj),2,2,4,0) == 0)
		{
			deleteObject(%set);
			return %obj;		
		}
		if(%i == %count - 1)
		{
			%i = -1;
			%value = %spawnIdx;
		}
		deleteObject(%set);
	}
	return false;
}

function Game::playerSpawn(%clientId, %respawn)
{
	dbecho($dbechoMode2, "Game::playerSpawn(" @ %clientId @ ", " @ %respawn @ ")");

	if(!$ghosting)
		return false;

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(fetchData(%clientId, "isMimic"))
	{
		storeData(%clientId, "RACE", Client::getGender(%clientId) @ "Human");
		storeData(%clientId, "isMimic", "");
	}

	if(%clientId.RespawnMeInArena)
	{
		%group = nameToID("MissionGroup\\TheArena\\TeleportEntranceMarkers");

		if(%group != -1)
		{
			%num = Group::objectCount(%group);

			%r = floor(getRandom() * %num);
			%spawnMarker = Group::getObject(%group, %r);
		}
		else
		{
			%spawnMarker = Game::pickPlayerSpawn(%clientId, %respawn);
		}

		RefreshArenaTextBox(%clientId);
	}
	else
	{
		%spawnMarker = Game::pickPlayerSpawn(%clientId, %respawn);

		//the player is spawning normally, ie. not in the arena
		storeData(%clientId, "inArena", "");
		CloseArenaTextBox(%clientId);
	}

	if(%spawnMarker)
	{
		%clientId.guiLock = "";
		%clientId.dead = "";
		if(%spawnMarker == -1)
		{
			%spawnPos = "0 0 600";
			%spawnRot = "0 0 0";
		}
		else if(fetchData(%clientId, "campPos") != "" && !%respawn)
		{
			//if the player HAS a campPos and it is his FIRST TIME SPAWNING, then spawn him at this campPos
			%spawnPos = fetchData(%clientId, "campPos");
			%spawnRot = fetchData(%clientId, "campRot");
		}
		else
		{
			%spawnPos = GameBase::getPosition(%spawnMarker);
			%spawnRot = GameBase::getRotation(%spawnMarker);
		}

		%armor = $RaceToArmorType[fetchData(%clientId, "RACE")];
        %spawnPos = Farming::PlayerSpawnProtection(%spawnPos);
        
		%pl = spawnPlayer(%armor, %spawnPos, %spawnRot);
		PlaySound(SoundSpawn2, %spawnPos);
		GameBase::startFadeIn(Client::getOwnedObject(%clientId));

        
        
		echo("SPAWN: cl:" @ %clientId @ " pl:" @ %pl @ " marker:" @ %spawnMarker @ " position: " @ %spawnPos @ " armor:" @ %armor);

		if(%pl != -1)
		{
			UpdateTeam(%clientId);
			GameBase::setTeam(%pl, Client::getTeam(%clientId));
			Client::setOwnedObject(%clientId, %pl);
			Client::setControlObject(%clientId, %pl);
			Game::playerSpawned(%pl, %clientId, %armor, %respawn);

            %spawnhp = 1;
			%spawnmana = 0;
            
			if(%respawn)	      
			{
                %spawnhp = fetchData(%clientId, "MaxHP");
				%spawnmana = fetchData(%clientId, "MaxMANA");
				//setHP(%clientId, fetchData(%clientId, "MaxHP"));
				//setStamina(%clientId, fetchData(%clientId, "MaxMANA"));
			}
			else
			{
				%spawnhp = fetchData(%clientId, "tmphp");
				%spawnmana = fetchData(%clientId, "tmpmana");
				storeData(%clientId, "tmphp", "");
				storeData(%clientId, "tmpmana", "");
			}
            if(%spawnhp < 1){ %spawnhp = 1; echo("Error: spawn hp was less than 1"); }
			if(%spawnmana < 0){ %spawnmana = 0; echo("Error: spawn mp was less than 0"); }
			setHP(%clientId, %spawnhp);
			setMANA(%clientId, %spawnmana);
			storeData(%clientId.possessId, "dumbAIflag", "");
            storeData(%clientId, "isDead", False);
		}
		schedule("repackAlert("@%clientId@");",0.1);
		return true;
	}
	else
	{
		Client::sendMessage(%clientId,0,"Sorry No Respawn Positions Are Empty - Try again later ");
		return false;
	}
}

function Game::playerSpawned(%pl, %clientId, %armor)
{
	dbecho($dbechoMode2, "Game::playerSpawned(" @ %pl @ ", " @ %clientId @ ", " @ %armor @ ")");

	storeData(%clientId, "HasLoadedAndSpawned", True);

	if(%clientId.RespawnMeInArena)
	{
		//give him his equipment back
		RestorePreviousEquipment(%clientId);

		%clientId.RespawnMeInArena = "";
	}
	else
	{
        //echo(fetchData(%clientId, "spawnStuff"));
        %stuff = fetchData(%clientId, "spawnStuff");
        if(%stuff != "")
        {
            GiveThisStuff(%clientId, fetchData(%clientId, "spawnStuff"), False);
            
            storeData(%clientId,"spawnStuff","");
        }
        storeData(%clientId,"totalWeight",WeightRecalculate(%clientId));
        RPGItem::RefreshPlayerEquipStats(%clientId);
        //if(%clientId.loadCharacterFlag)
        //{
        //    %clientId.loadCharacterFlag = false;
        //    //Update inventory on client end.
        //    schedule("RPGItem::refreshPlayerInv("@%clientId@");",3);
        //    schedule("storeData("@%clientId@",\"totalWeight\",WeightRecalculate("@%clientId@"));",3.5);
        //}
	}
    
    Player::mountItem(%pl,BaseWeapon,$BaseWeaponSlot);
    
	if(fetchData(%clientId, "LCK") < 0)
		storeData(%clientId, "LCK", 0);

	RefreshAll(%clientId,true);
} 

function Game::autoRespawn(%clientId)
{
	dbecho($dbechoMode2, "Game::autoRespawn(" @ %clientId @ ")");

	if(%clientId.dead == 1)
		Game::playerSpawn(%clientId, True);
}