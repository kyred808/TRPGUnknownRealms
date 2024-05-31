
function AI::NewPeriodic(%aiName)
{
	dbecho($dbechoMode, "AI::Periodic(" @ %aiName @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "dumbAIflag") || fetchData(%aiId, "frozen"))
		return;

	%aiTeam = GameBase::getTeam(%aiId);
	%aiPos = GameBase::getPosition(%aiId);

	//=================================================================
	//Everytime this function is called, the bot looks at all clients
	//(including bots) and sets a waypoint to the nearest one that is
	//NOT on the same team, and that it can see in its FOV (by
	//comparing Rotations).  These loops will undoubtedly cause alot
	//of CPU drain.
	//=================================================================
    
    if(fetchData(%aiId, "SpawnBotInfo") != "")
	{
        if(fetchData(%aiId, "SpellCastStep") != 1 && !fetchData(%aiId, "noBotSniff"))
		{
            %closest = 500000;

            %noTargetFlag = "";
            %b = $AImaxRange * 2;
            %set = newObject("set", SimSet);
            %n = containerBoxFillSet(%set, $SimPlayerObjectType, %aiPos, %b, %b, %b, 0);
            for(%i = 0; %i < Group::objectCount(%set); %i++)
            {
                %id = Player::getClient(Group::getObject(%set, %i));
                if(GameBase::getTeam(%id) != %aiTeam && !fetchData(%id, "invisible"))
                {
                    %dist = Vector::getDistance(%aiPos, GameBase::getPosition(%id));
                    if(%dist < %closest)
                    {
                        %closest = %dist;
                        %closestId = %id;
                    }
                }
            }
            deleteObject(%set);
            
            if(%closest <= $AImaxRange)
            {
                storeData(%aiId,"target",%closestId);
                %target = %closestId;
                
                %weap = RPGItem::ItemTagToLabel(fetchData(%aiId,"EquippedWeapon"));
                
                if($AccessoryVar[%weap, $AccessoryType] == $RangedAccessoryType)
                {
                    AI::newDirectiveFollow(%aiName, %closestId, 0, 99);
                    if(%closest <= 10)
                    {
                        %offPos = ScaleVector(Vector::Normalize(Vector::sub(%aiPos,Gamebase::getPosition(%closestId))),15);
                        AI::SetVar( %aiName,  seekOff, %offPos);
                        AI::newDirectiveFollow(%aiName, %target, 0, 99);
                    }
                }
                else
                {
                    if(%closest <= 10)
                        AI::newDirectiveWaypoint(%aiName, GameBase::getPosition(%closestId), 99);
                    else
                        AI::newDirectiveFollow(%aiName, %closestId, 0, 99);
                }
                PlaySound(RandomRaceSound(fetchData(%aiId, "RACE"), Acquired), GameBase::getPosition(%aiId));
            }
            else
                %noTargetFlag = true;
        }
        
        if(%noTargetFlag || fetchData(%aiId, "noBotSniff"))
        {
            AI::SelectMovement(%aiName);
        }
        else if(!%noTargetFlag)
        {
            %loadout = fetchData(%aiId,"BotLoadoutTag");
            if(%loadout == "")
                %loadout = "Default";
            if($AIBehavior[%loadout,UseBash] && fetchData(%aiId, "blockBash") == "")
            {
                storeData(%aiId, "NextHitBash", True);
                storeData(%aiId, "blockBash", True);
            }
        }
    }

	//=================================================================
	// Event stuff
	//=================================================================
	%i = GetEventCommandIndex(%aiId, "onPosCloseEnough");
	if(%i != -1)
	{
		%x = GetWord($EventCommand[%aiId, %i], 2);
		%y = GetWord($EventCommand[%aiId, %i], 3);
		%z = GetWord($EventCommand[%aiId, %i], 4);
		%dpos = %x @ " " @ %y @ " " @ %z;

		if(Vector::getDistance(%dpos, GameBase::getPosition(%aiId)) <= 5)
		{
			%name = GetWord($EventCommand[%aiId, %i], 0);
			%type = GetWord($EventCommand[%aiId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%aiId, %i], String::findSubStr($EventCommand[%aiId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %aiId, "");
			$EventCommand[%aiId, %i] = "";
			schedule("remoteSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 1);
		}
	}
	%i = GetEventCommandIndex(%aiId, "onIdCloseEnough");
	if(%i != -1)
	{
		%id = GetWord($EventCommand[%aiId, %i], 2);
		%dpos = GameBase::getPosition(%id);

		if(Vector::getDistance(%dpos, %aiPos) <= 10)
		{
			%name = GetWord($EventCommand[%aiId, %i], 0);
			%type = GetWord($EventCommand[%aiId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%aiId, %i], String::findSubStr($EventCommand[%aiId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %aiId, "");
			$EventCommand[%aiId, %i] = "";
			schedule("remoteSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 1);
		}
	}

	//=================================================================
	//1 thru 4 = animation 10, 5 thru 10 = animation 11, else do nothing
	//=================================================================
	if(Item::getVelocity(%aiId) == "0 0 0")
	{
		%r = floor(getRandom() * 200)+1;
		if(%r >= 1 && %r <= 4)
			RemotePlayAnim(%aiId, 10);
		else if(%r >= 5 && %r <= 10)
			RemotePlayAnim(%aiId, 11);

		if(GameBase::getTeam(%aiId) > 1)
		{
			if(OddsAre(5))
				RemotePlayAnim(%aiId, 2);
		}
	}
	
	//=============================================
	//do other stuff...
	//=============================================
	//%curTarget = ai::getTarget( %aiName );

	if(OddsAre(4))
		AI::SelectBestWeapon(%aiId);
}