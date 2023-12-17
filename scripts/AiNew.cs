
//Debating using these
//AI with this flag will be anchored to a zone.
//If all players leave the zone, bots with this spawntype will be cleaned up
$RPGAI::SpawnTypeZone = 0;

//AI with this flag spawned from a marker not necessarily tied to a zone
$RPGAI::SpawnTypeSpawnPoint = 1;

//AI with this flag spawned in the world due to some sort of event
$RPGAI::SpawnTypeWorld = 2;


function SpawnAI(%newName, %displayName, %aiSpawnPos, %commandIssuer, %loadout)
{
	dbecho($dbechoMode, "SpawnAI(" @ %newName @ ", " @ %displayName @ ", " @ %aiSpawnPos @ ", " @ %commandIssuer @ ")");

	%retval = createAI(%newName, %aiSpawnPos, %displayName);
    %w0 = GetWord(%commandIssuer, 0);
	if(%retval != -1)
	{
		%aiId = AI::getId( %newName );
		AI::setVar( %newName,  iq,  100 );
		AI::setVar( %newName,  attackMode, $AIattackMode);
		AI::setVar( %newName,  pathType, $AI::defaultPathType);
		//AI::SetVar( %newName,  seekOff, 1);
		AI::setAutomaticTargets( %newName );

		if(%w0 == "TempSpawn")
		{
			//the %commandIssuer is a data string
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);
			%team = GetWord(%commandIssuer, 4);
			GameBase::setTeam(%aiId, %team);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}
		else if(%w0 == "MarkerSpawn")
		{
			//the %commandIssuer is a marker
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);
			%team = GameBase::getMapName(GetWord(%commandIssuer, 1));
			if(%team == "") %team = 0;
			GameBase::setTeam(%aiId, %team);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}
        else if(%w0 == "ZoneSpawn")
        {
            storeData(%aiId, "SpawnBotInfo", %commandIssuer);
            %zid = getWord(fetchData(%aiId, "SpawnBotInfo"), 1);
            %spawnIdx = getWord(fetchData(%aiId, "SpawnBotInfo"), 2);
            $Zone::SpawnPoint[%zid,%spawnIdx,SpawnCount]++;
            UpdateTeam(%aiId);

			AI::SetVar(%newName, spotDist, $AIspotDist);
        }
        else if(%w0 == "WorldSpawn")
        {
            storeData(%aiId, "SpawnBotInfo", %commandIssuer);
            %team = GameBase::setTeam(%aiId,getWord(%commandIssuer,1));
            
            AI::SetVar(%newName, spotDist, $AIspotDist);
        }
		else if(%w0 == "SpawnPoint") //Old spawn type
		{
			//the %commandIssuer is a spawn crystal
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);

			$numAIperSpawnPoint[GetWord(%commandIssuer, 1)]++;
            
			UpdateTeam(%aiId);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}

		AI::setWeapons(%newName, %loadout);

		return ( %newName );
	}
	else
	{
		return -1;
	}
}