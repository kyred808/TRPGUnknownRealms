function Client::onKilled(%clientId, %killerId, %damageType)
{
	dbecho($dbechoMode, "Client::onKilled(" @ %clientId @ ", " @ %killerId @ ", " @ %damageType @ ")");

	//This function is NOT an event, it must be MANUALLY CALLED!
	//At this point, the client can still be queried for getItemCounts, but is not considered an object anymore.

	//we can award the other players exp
	DistributeExpForKilling(%clientId);

    %mh = fetchData(%killerId,"MANAHarvest");
    if(%mh > 0)
    {
        refreshMANA(%killerId,-1*%mh);
        Client::sendMessage(%killerId,$MsgWhite,"You restored "@%mh@" mana.");
    }

	//The player with the killshot gets the official "kill"
	if(!IsInCommaList(fetchData(%killerId, "TempKillList"), Client::getName(%clientId)))
		storeData(%killerId, "TempKillList", AddToCommaList(fetchData(%killerId, "TempKillList"), Client::getName(%clientId)));

	if(%killerId != %clientId)
	{
		//a human player killed %clientId
		%n = Client::getName(%killerId);

		Client::sendMessage(%clientId, 0, "You were killed by " @ %n @ "!");
        storeData(%killerId,"HealBurstCounter",$HealBurstCntPerKill,"inc");
		//if(fetchData(%killerId, "bounty") == Client::getName(%clientId))
		//	storeData(%killerId, "bounty", fetchData(%clientId, "LVL") @ " !Q@W#E$R%T^Y&U*I(O)P");
	}
	else if(%killerId == %clientId)
	{
		Client::sendMessage(%clientId, 0, "You killed yourself!");
	}
	else if(%damageType == 11 || %damageType == 12 || %damageType == 2)
	{
		Client::sendMessage(%clientId, 0, "You were killed!");
	}

	UnHide(%clientId);

//========================================================================================================================

	echo("GAME: kill " @ %killerId @ " " @ %clientId @ " " @ %damageType);
	%clientId.guiLock = true;
	Client::setGuiMode(%clientId, $GuiModePlay);

	Game::clientKilled(%clientId, %killerId);
}

function Game::clientKilled(%playerId, %killerId)
{
	dbecho($dbechoMode, "Game::clientKilled(" @ %playerId @ ", " @ %killerId @ ")");

	%set = nameToID("MissionCleanup/ObjectivesSet");
	for(%i = 0; (%obj = Group::getObject(%set, %i)) != -1; %i++)
		GameBase::virtual(%obj, "clientKilled", %playerId, %killerId);
}

function Player::onKilled(%this)
{
	dbecho($dbechoMode, "Player::onKilled(" @ %this @ ")");

	//At this point, the client can still be queried for getItemCounts, and is also still an object
	//Player::Kill calls this function

	%clientId = Player::getClient(%this);
	%killerId = fetchData(%clientId, "tmpkillerid");
	storeData(%clientId, "tmpkillerid", "");

	//revert
	Client::setControlObject(%clientId.possessId, %clientId.possessId);
	Client::setControlObject(%clientId, %clientId);
	storeData(%clientId.possessId, "dumbAIflag", "");
	$possessedBy[%clientId.possessId] = "";

    Player::setJet(%this,false);
    
    %dlist = fetchData(%clientId,"DrainList");
    %dlistLen = countObjInCommaList(%dlist);
    if(%dlistLen > 0)
    {
        for(%i = 0; %i < %dlistLen; %i++)
        {
            %entry = String::getWord(%dlist,",",%i);
            %victim = getWord(%entry,0);
            %debuff = Word::getSubWord(%entry,1,30); //I assume it won't go over 30 words for a debuff string
            UpdateBonusState(%victim, %debuff, 0); //Clear the debuff
        }
    }
    
	if(IsStillArenaFighting(%clientId))
	{
		//player's dueling flag is still at ALIVE, make him DEAD

		%a = GetArenaDuelerIndex(%clientId);
		$ArenaDueler[%a] = GetWord($ArenaDueler[%a], 0) @ " DEAD";

		if(!Player::IsAiControlled(%clientId))
			%clientId.RespawnMeInArena = True;
	}
	else if(IsInRoster(%clientId))
	{
		//player was in the waiting room
		//the only way someone could have died in there is if a player was added
		//to the roster, and an AI was killed to make way for this player.
		//so don't drop lootbag

		if(Player::isAiControlled(%clientId)) //if it was an AI, remove him right away, the same AI never spawns back
			RemoveFromRoster(%clientId);
	}
	else if(fetchData(%clientId, "noDropLootbagFlag"))
	{
		//do nothing
	}
	else
	{
		//player died when not dueling
		//drop lootbag
		%tmploot = "";

		if(fetchData(%clientId, "COINS") > 0)
			%tmploot = %tmploot @ "COINS " @ floor(fetchData(%clientId, "COINS")) @ " ";
		storeData(%clientId, "COINS", 0);
        
        if(Player::isAiControlled(%clientId))
            %tmploot = DropTable::GenerateLootDrops(%clientId,%tmploot);
        
        %itemList = RPGItem::getFullItemList(%clientId,false);
        
        for(%i = 0; (%itemTag = getWord(%itemList,%i)) != -1; %i+=2)
        {
            %bInclude = false;
            %a = RPGItem::ItemTagToLabel(%itemTag);
            
            if(Player::isAiControlled(%clientId) && Word::findWord(fetchData(%clientId,"NoDropLootList"),%a) != -1)
            {
                %bInclude = false;
            }
            else
            {
                if(fetchData(%clientId,"LCK") >= 0)
                {
                    %eq = fetchData(%clientId,"EquippedWeapon");
                    %class = RPGItem::getItemGroupFromTag(%itemTag);
                    
                    if(%eq == %itemTag || %class == $RPGItem::EquippedClass || $LoreItem[%label] == true)
                    {
                        %bInclude = true;
                    }
                }
                else
                    %bInclude = true;
                
                if(%a == CastingBlade)
					%bInclude = false;
                else if(String::icompare(%a,"TreeAtk") == 0)
                    %bInclude = false;
                
                if($StealProtectedItem[%a])
					%bInclude = false;
            }
            
            if(%bInclude)
            {
                %class = RPGItem::getItemGroupFromTag(%itemTag);
                %newTag = %itemTag;
                %eq = fetchData(%clientId,"EquippedWeapon");
                if(%class == $RPGItem::EquippedClass)
                    %newTag = RPGItem::getAlternateTag(%itemTag);
                
                //echo("Tag: "@ %itemTag @" NewTag: "@%newTag);
                if(%eq == %itemTag)
                {
                    //special handling for currently held weapon
                    %tmploot = %tmploot @ %itemTag @ " 1 ";
                    RPGItem::decItemCount(%clientId, %itemTag);
                }
                else
                {
                    %tmploot = %tmploot @ %newTag @ " " @ getWord(%itemList,%i+1) @ " ";
                    RPGItem::setItemCount(%clientId, %itemTag, 0); //Don't worry about equip stats.  Will be refreshed on spawn.
                    
                }
                if(String::len(%tmploot) > 200)
                {
                    if(Player::isAiControlled(%clientId))
                        TossLootbag(%clientId, %tmploot, 1, "*", 300);
                    else
                    {
                        %namelist = Client::getName(%clientId) @ ",";
                        if(fetchData(%clientId, "LCK") >= 0)
                            %tehLootBag = TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 300, 300, 3600));
                        else
                            %tehLootBag = TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 0.2, 5, "inf"));
                    }
                    %tmploot = "";
                }
            }
        }

        //Rework needed for virtual items
        //Needs optimization, but not critical yet
		//%max = getNumItems();
		//for (%i = 0; %i < %max; %i++)
		//{
		//	%a = getItemData(%i);
		//	%itemcount = Player::getItemCount(%clientId, %a);
        //
		//	if(%itemcount)
		//	{
		//		%flag = False;
        //
		//		if(fetchData(%clientId, "LCK") >= 0)
		//		{
		//			//currently mounted weapon and all equipped stuff + lore items are thrown into lootbag.
		//			if(Player::getMountedItem(%clientId, $WeaponSlot) == %a || %a.className == "Equipped" || $LoreItem[%a] == True)
		//				%flag = True;
		//		}
		//		else
		//			%flag = True;
        //
		//		if(fetchData(%clientId, "LCK") < 0 && Player::isAiControlled(%clientId))
		//			%flag = True;
        //
		//		if(%a == CastingBlade)
		//			%flag = False;		//HARDCODED because we DONT want any players to have this item.
		//		
        //        if(%a == Treeatk)
        //            %flag = False;
        //        
		//		if($StealProtectedItem[%a])
		//			%flag = False;
        //        
        //        if(Player::isAiControlled(%clientId) && Word::findWord(fetchData(%clientId,"NoDropLootList"),%a) != -1)
        //        {
        //            
        //            //echo(%a," "@fetchData(%clientId,"NoDropLootList"));
        //            %flag = False;
        //        }
        //        
		//		if(%flag)
		//		{
		//			%b = %a;
		//			if(%b.className == "Equipped")
		//				%b = String::getSubStr(%b, 0, String::len(%b)-1);
        //
		//			if(Player::getMountedItem(%clientId, $WeaponSlot) == %a)
		//			{
		//				//special handling for currently held weapon
		//				%tmploot = %tmploot @ %b @ " 1 ";
		//				Player::decItemCount(%clientId, %a);
		//			}
		//			else
		//			{
		//				%tmploot = %tmploot @ %b @ " " @ Player::getItemCount(%clientId, %a) @ " ";
		//				RPGItem::setItemCount(%clientId, %a, 0);
		//			}
		//			if(String::len(%tmploot) > 200)
		//			{
		//				if(Player::isAiControlled(%clientId))
		//					TossLootbag(%clientId, %tmploot, 1, "*", 300);
		//				else
		//				{
		//					%namelist = Client::getName(%clientId) @ ",";
		//					if(fetchData(%clientId, "LCK") >= 0)
		//						%tehLootBag = TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 300, 300, 3600));
		//					else
		//						%tehLootBag = TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 0.2, 5, "inf"));
		//				}
		//				%tmploot = "";
		//			}
		//		}
		//	}
		//}
        
        //%beltItems = Belt::getDeathItems(%clientid);
        //
        //%tmploot = %tmploot @ %beltItems; 
		if(%tmploot != "")
		{
			if(Player::isAiControlled(%clientId))
				TossLootbag(%clientId, %tmploot, 1, "*", 300);
			else
			{
				%namelist = Client::getName(%clientId) @ ",";
				if(fetchData(%clientId, "LCK") >= 0)
				{
					TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 300, 300, 3600));
				}
				else
					TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 0.2, 5, "inf"));
			}
		}

		//updateSpawnStuff(%clientId);

		//house stuff
		%victimH = fetchData(%clientId, "MyHouse");
		%killerH = fetchData(%killerId, "MyHouse");
		%vhn = GetHouseNumber(%victimH);
		%khn = GetHouseNumber(%killerH);
		if(%vhn != "")
		{
			//a house member is killed

			if(fetchData(%clientId, "LCK") < 0)
			{
				//this house member had no LCK at time of death

				if(fetchData(%clientId, "RankPoints") <= 0)
				{
					//no LCK and no Rank Points! you're booted!
					BootFromCurrentHouse(%clientId, True);
				}
				else
					Client::sendMessage(%clientId, $MsgRed, "You have lost all your Rank Points because you died with 0 LCK!");

				storeData(%clientId, "RankPoints", 0);
			}

			//victim loses two rank points
			Client::sendMessage(%clientId, $MsgWhite, "You lost 2 Rank Points.");
			storeData(%clientId, "RankPoints", 2, "dec");

			if(%khn != "")
			{
				if(%khn != %vhn)
				{
					//both contenders are in a house, different from each other
					Client::sendMessage(%killerId, $MsgWhite, "You gained 1 Rank Point!");
					storeData(%killerId, "RankPoints", 1, "inc");
				}
				else
				{
					//both contenders are in the same house, happens if one target-lists the other.
					Client::sendMessage(%killerId, $MsgWhite, "You lost 1 Rank Point.");
					storeData(%killerId, "RankPoints", 1, "dec");
				}
			}
		}
		else if(%vhn == "" && %khn != "")
		{
			//a house member killed a non-house member. no bonuses or punishments
		}

		//CLEAR!!!!
		if(!IsInArenaDueler(%clientId) && !Player::isAiControlled(%clientId) && fetchData(%clientId, "LCK") < 0)
		{
			//HARDCORE player reset (must turn on $hardcore switch)
			if($hardcore)
			{
				if(fetchData(%clientId, "LVL") > 8 && fetchData(%clientId, "RemortStep") > 0)
				{
					if(!Player::isAiControlled(%killerId))
						ResetPlayer(%clientId);
				}
			}

			storeData(%clientId, "zone", "");	//so the player spawns back at start points
		}
	}

	if(fetchData(%clientId, "deathmsg") != "")
	{
		%kitem = Player::getMountedItem(%killerId, $WeaponSlot);
		%msg = nsprintf(fetchData(%clientId, "deathmsg"), Client::getName(%killerId), Client::getName(%clientId), %kitem.description);
		remoteSay(%clientId, 0, %msg);
	}

	if(Player::isAiControlled(%clientId))
	{
        $ZoneCleanupProtected[%clientId] = false;
		//event stuff
		%i = GetEventCommandIndex(%clientId, "onkill");
		if(%i != -1)
		{
			%name = GetWord($EventCommand[%clientId, %i], 0);
			%type = GetWord($EventCommand[%clientId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%clientId, %i], String::findSubStr($EventCommand[%clientId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %clientId, %killerid);
			$EventCommand[%clientId, %i] = "";
			schedule("remoteSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 2);
		}
		ClearEvents(%clientId);
	}

	storeData(%clientId, "noDropLootbagFlag", "");

	storeData(%clientId, "SpellCastStep", "");
    storeData(%clientId, "EquippedWeapon", "");
	%clientId.sleepMode = "";
	refreshHPREGEN(%clientId);
    refreshMANAREGEN(%clientId);

	Client::setControlObject(%clientId, %clientId);
	storeData(%clientId, "dumbAIflag", "");

	//remember the last zone the player was in.
	storeData(%clientId, "lastzone", fetchData(%clientId, "zone"));

	PlaySound(RandomRaceSound(fetchData(%clientId, "RACE"), Death), GameBase::getPosition(%clientId));
    //if(fetchData(%clientId, "RACE") == "Bat")
    //{
    //    Player::setAnimation(%clientId,50);
    //    echo("Bat Kill!");
    //}
//========================================================================================================================

	%clientId.dead = 1;
	if($AutoRespawn > 0)
		schedule("Game::autoRespawn(" @ %clientId @ ");",$AutoRespawn,%clientId);

	Player::setDamageFlash(%this,0.75);
	if(%clientId != -1)
	{
		if(%this.vehicle != "")
		{
			if(%this.driver != "")
			{
				%this.driver = "";
				Client::setControlObject(Player::getClient(%this), %this);
				Player::setMountObject(%this, -1, 0);
			}
			else
			{
				%this.vehicle.Seat[%this.vehicleSlot-2] = "";
			%this.vehicleSlot = "";
			}
			%this.vehicle = "";
		}
		schedule("GameBase::startFadeOut(" @ %this @ ");", $CorpseTimeoutValue, %this);
		if(!Player::isAiControlled(%clientId)){
			//This AI check fixes spam related to a horrible mass-crashing bug,
			//but it does not seem to fix the crashing bug itself.
			//If you happen to know how to completely fix these problems,
			//please contact me at beatme101.com or beatme101@gmail.com.
			//This is as far as I could track the bug. It seems that this function,
			//when called on an AI under unknown circumstances, could cause
			//the AI to not be considered an AI by Player::isAiControlled.
			//Once the player is not considered an AI, other sets of code start to get confused.
			//The spam came from a section of code meant to handle a character's weight after being hit.
			//The function got called with a delay after the AI died, and it fails to check if the AI is dead,
			//so the server's console recieves a large amount of spam shortly after the AI dies.
			//The following function causes the bug if called on AI.
			Client::setOwnedObject(%clientId, -1);
			Client::setControlObject(%clientId, Client::getObserverCamera(%clientId));
			Observer::setOrbitObject(%clientId, %this, 15, 15, 15);
		}
		schedule("deleteObject(" @ %this @ ");", $CorpseTimeoutValue + 2.5, %this);
		%clientId.observerMode = "dead";
		%clientId.dieTime = getSimTime();
	}
}

function CalculateDamageReduction(%clientId)
{
    %def = fetchData(%clientId, "DEF");
    
    if(%def <= 150)
    {
        return %def / 1000;
    }
    else if(%def <= 500)
    {
        %upper = ((%def - 150) / 20)/100;
        return 0.15 + %upper;
    }
    else
    {
        %upper = ((%def - 500) / 30)/100;
        return 0.15 + 0.175 + %upper;
    }
    //if(%def <= 500)
    //{
    //    return (%def /1000);
    //}
    //else
    //{
    //    %upper = ((%def-500)/15)/100;
    //    return 0.5 + %upper;
    //}
   //if(%def <= 300)
   //{
   //    return (%def/1000);
   //}
   //else if(%def <= 500)
   //{
   //    %upper = ((%def-300)/15)/100;
   //    return 0.3 + %upper;
   // }
   // else
   // {
   //     %lower = 0.3 + 0.13;
   //     %upper = ((%def-500)/20)/100; // %def factors in less
   //     return %lower + %upper; //0.5 for the lower 500 def
   // }
    
    
}

function Player::onDamage(%this,%type,%value,%pos,%vec,%mom,%vertPos,%rweapon,%object,%weapon,%preCalcMiss,%dmgMult)
{
	//dbecho($dbechoMode2, "Player::onDamage(" @ %this @ ", " @ %type @ ", " @ %value @ ", " @ %pos @ ", " @ %vec @ ", " @ %mom @ ", " @ %vertPos @ ", " @ %rweapon @ ", " @ %object @ ", " @ %weapon @ ", " @ %preCalcMiss @ ", " @ %dmgMult @ ")");
    //echo("Player::onDamage(" @ %this @ ", " @ %type @ ", " @ %value @ ", " @ %pos @ ", " @ %vec @ ", " @ %mom @ ", " @ %vertPos @ ", " @ %rweapon @ ", " @ %object @ ", " @ %weapon @ ", " @ %preCalcMiss @ ", " @ %dmgMult @ ")");
    
    %weapTag = %weapon;
    %weapon = RPGItem::ItemTagToLabel(%weapon);
    %skilltype = $SkillType[%weapon];

	if(Player::isExposed(%this) && %object != -1 && %type != $NullDamageType && !Player::IsDead(%this))
	{
		%damagedClient = Player::getClient(%this);
		%shooterClient = %object;
        
        if(%type == $MissileDamageType)
        {
            %weapon = RPGItem::ItemTagToLabel(fetchData(%shooterClient,"EquippedWeapon"));
            %skilltype = $SkillType[%weapon];  
        }
        
        if(fetchData(%damagedClient,"isUberBoss") && fetchData(%damagedClient,"ubCombatStarted") == "")
        {
            %swState = fetchData(%damagedClient,"ubStandWaiting");
            if(%swState != "")
            {
                if(%swState == 1)
                {
                    UberBoss::teleportToMarker(%damagedClient);
                    return;
                }
                else if(%swState == 2)
                {
                    UberBoss::StartCombat(fetchData(%damagedClient, "BotInfoAiName"),%damagedClient,true);
                    return;
                }
            }
        }
        
        
        if(Player::isAIControlled(%damagedClient) && fetchData(%damagedClient,"AiIgnoreTargets") != "")
        {
            storeData(%damagedClient,"AiIgnoreTargets","");
            AI::newDirectiveFollow(fetchData(%damagedClient, "BotInfoAiName"), %shooterClient, 0, 99);
        }
        
        //if(%shooterClient == 0 && fetchData(%damagedClient,"dragonAttack") != "")
        //{
        //    %shooterClient = fetchData(%damagedClient,"dragonAttack");
        //}
		%damagedClientPos = GameBase::getPosition(%damagedClient);
		%shooterClientPos = GameBase::getPosition(%shooterClient);

        %damagedClient.isAtRestCounter = 0;
        if(%damagedClient.isAtRest)
        {
            %damagedClient.isAtRest = 0;
            refreshHPREGEN(%damagedClient);
        }
        
		%damagedCurrentArmor = GetCurrentlyWearingArmor(%damagedClient);
        if(%dmgMult == "")
        {
            %dmgMult = 1;
        }
		//==============
		//PROCESS STATS
		//==============
		%isMiss = False;
		%Backstab = False;
		%Bash = False;
		%arenaNull = False;
		%sameTeamNull = False;
        %heavyStrike = false;
        if(fetchData(%shooterClient,"HeavyStrikeFlag") && %weapon != "")
            %heavyStrike = true;

        %empower = fetchData(%shooterClient,"EmpowerFlag");
            
        if(fetchData(%shooterClient,"NextHitFlurry"))
        {
            if($AccessoryVar[%weapon, $AccessoryType] == $SwordAccessoryType)
            {
                storeData(%shooterClient, "NextHitFlurry", "");
                if(%shooterClient != %damagedClient)
					Client::sendMessage(%shooterClient, $MsgWhite, "You hit " @ Client::getName(%damagedClient) @ " with a flurry of strikes!");
                schedule("Player::onDamage("@ %this @ ",\"\", " @ %value @ ", \"" @ %pos @ "\", \"" @ %vec @ "\", \"" @ %mom @ "\", \"" @ %vertPos @ "\", \"" @ %rweapon @ "\", " @ %object @ ", \"" @ %weapon @ "\", \"\",0.65);",0.01,%shooterClient);
                schedule("Player::onDamage("@ %this @ ",\"\", " @ %value @ ", \"" @ %pos @ "\", \"" @ %vec @ "\", \"" @ %mom @ "\", \"" @ %vertPos @ "\", \"" @ %rweapon @ "\", " @ %object @ ", \"" @ %weapon @ "\", \"\",0.65);",0.35,%shooterClient);
                schedule("Player::onDamage("@ %this @ ",\"\", " @ %value @ ", \"" @ %pos @ "\", \"" @ %vec @ "\", \"" @ %mom @ "\", \"" @ %vertPos @ "\", \"" @ %rweapon @ "\", " @ %object @ ", \"" @ %weapon @ "\", \"\",0.65);",0.7,%shooterClient);
                schedule("storeData(" @ %shooterClient @ ", \"blockFlurry\", \"\");", $SkillFlurryDelay);
                return;
            }
        }
		//------------- CREATE DAMAGE VALUE -------------
		if(%type == $SpellDamageType || %type == $StaffDamageType)
		{
            //For the case of SPELLS, the initial damage has already been determined before calling this function
            
            if(%type == $StaffDamageType)
            {
                //%staffWeap = Player::getMountedItem(%shooterClient,$WeaponSlot);
                %staffWeap = RPGItem::ItemTagToLabel(fetchData(%shooterClient,"EquippedWeapon"));
                %skilltype = $SkillType[%staffWeap];
                %atkIdx = Word::FindWord($AccessoryVar[%staffWeap, $SpecialVar],$SpecialVarATK)+1;
                %value = getWord($AccessoryVar[%staffWeap, $SpecialVar],%atkIdx) * %value; //Projectile damage should always be 1 for staff proj
            }
            else
            {
                echo($Spell::index[%weapTag]);
                if($Spell::index[%weapTag] != "")
                {
                    %skilltype = $SkillType[%weapTag];
                }
                else
                    %skilltype = $skilloffensivecasting;
            }
            echo("Spell Type: "@ %skilltype);
			%sdm = AddBonusStatePoints(%shooterClient, "SDM"); //Spell Damage bonus
			%dmg = %value + %sdm;
            if(%empower)
                %dmg += 15;
			%value = round(((%dmg / 1000) * CalculatePlayerSkill(%shooterClient, %skilltype)));

			%ab = (getRandom() * (fetchData(%damagedClient, "MDEF") / 10));
			%value = Cap(%value - %ab, 0, "inf");

            if(%empower)
                %value += 2;
			%value = (%value / $TribesDamageToNumericDamage);
            echo("Spell Damage: "@%value);
		}
		else if(%type != $LandingDamageType && %type != $MeteorDamageType)
		{
			%multi = 1;
            
            if(%type == $DragonDamageType)
                %multi = 100;

			//Backstab
			if(fetchData(%shooterClient, "invisible"))
			{
				%dRot = GetWord(GameBase::getRotation(%damagedClient), 2);
				%sRot = GetWord(GameBase::getRotation(%shooterClient), 2);
				%diff = %dRot - %sRot;
				if(%diff >= -0.9 && %diff <= 0.9)
				{
					if(%skilltype == $SkillPiercing)
					{
						%multi += CalculatePlayerSkill(%shooterClient, $SkillBackstabbing) / 175;
						%Backstab = True;
						%dotherdebugmsg = "\n\nyou were backstabbed";
						%sotherdebugmsg = "\n\nyou successfully backstabbed!";
					}
				}
				if(%shooterClient.adminLevel < 5)
					UnHide(%shooterClient,"Attacking has revealed you!");
			}
			if(fetchData(%damagedClient, "invisible") && %damagedClient.adminLevel < 5)
			{
				UnHide(%damagedClient,"An attack has revealed you!");
			}

			//Bash
			if(fetchData(%shooterClient, "NextHitBash"))
			{
				if(%skilltype == $SkillBludgeoning)
				{
					%multi += CalculatePlayerSkill(%shooterClient, $SkillBashing) / 470;
					%b = GameBase::getRotation(%shooterClient);
					%c = CalculatePlayerSkill(%shooterClient, $SkillBashing)/15;
                    if(fetchData(%shooterClient,"isUberBoss"))
                        %c = %c * 1.4;

					%Bash = True;

					%delay = Cap(101 - fetchData(%shooterClient, "LVL"), 5, 50);
					schedule("storeData(" @ %shooterClient @ ", \"blockBash\", \"\");", %delay);
				}

				storeData(%shooterClient, "NextHitBash", "");
			}
			//if(%rweapon != "")
			//	%rweapondamage = GetRoll(GetWord(GetAccessoryVar(%rweapon, $SpecialVar), 1));
			//else
			//	%rweapondamage = 0;

			%weapondamage = fetchData(%shooterClient,"ATK"); //GetRoll(GetWord(GetAccessoryVar(%weapon, $SpecialVar), 1));
            if(%weapon == "UBBlast")
                %weapondamage = %value;
                
            if(%type == $BladeBoltDamageType)
            {
                %skilltype = $SkillType[RPGItem::ItemTagToLabel(fetchData(%shooterClient,"EquippedWeapon"))];
                %weapondamage = %weapondamage*0.75;
            }
            
            if(%empower)
                %weapondamage += 15;
			%value = round((( (%weapondamage) / 1000) * CalculatePlayerSkill(%shooterClient, %skilltype)) * %multi * %dmgMult);
            %a = (%value * 0.15);
			%r = round((getRandom() * (%a*2)) - %a);
			%value += %r;
            %amrAdj = (1 - Cap(fetchData(%shooterClient,"AMRP"),0,100)/100);
            //Value before any armor reduction.
            %trueDmg = %value;
            
            
            
			%amr = fetchData(%damagedClient,"AMR");
            if(%heavyStrike)
                %amr = Cap(%amr - 5,"0","inf");
			%value = Cap(%value - (%amr * %amrAdj), 1, "inf");
            
            %dmgRedPct = CalculateDamageReduction(%damagedClient);
        
            %reductionValue = %value * (%dmgRedPct);
            //echo("AMRP: "@ %amrp);
            //echo("DR: "@ %reductionValue);
            %value = %value - ((%reductionValue)*%amrAdj);

			if(%value < 1)
				%value = 1;

			if(%Bash)	//i'm doing this condition here because %mom is dependant on %value
			{
				%c1 = ( %c / 15 * %value );
				%c2 = %c2 / 10;
				%mom = Vector::Add(%mom,Vector::getFromRot( %b, %c1, %c2 ));
			}
            
            //Maybe a little too harsh.  Will handle with ATK and DEF/MDEF stats instead.
            //if(fetchData(%damagedClient,"Stamina") <= 25)
            //    %value = %value * 25/Cap(fetchData(%damagedClient,"Stamina"),1,25);
            //%stam = fetchData(%shooterClient,"Stamina");
            //if(%stam <= 25)
            //    %value = %value * %stam/25;

            if(%empower)
                %value += 2;
			%value = (%value / $TribesDamageToNumericDamage);
            
		}

		//------------- DETERMINE MISS OR HIT -------------
		//if(%preCalcMiss == "")
		//{
		//	if(%type != $LandingDamageType && %shooterClient != %damagedClient && %shooterClient != 0)
		//	{
		//		if(%type == $SpellDamageType)
		//			%x = (fetchData(%damagedClient, "MDEF") / 5) + CalculatePlayerSkill(%damagedClient, $SkillSpellResistance) + 5;
		//		else
		//			%x = (fetchData(%damagedClient, "DEF") / 5); //+ $PlayerSkill[%damagedClient, $SkillDodging] + 5;
		//		%y = CalculatePlayerSkill(%shooterClient, %skilltype) + 5;
	    //
		//		%n = %x + %y;
	    //
		//		%r = floor(getRandom() * %n) + 1;
	    //
		//		if(%r <= %x)
		//			%isMiss = True;
		//	}
		//}

		//=======================================|WATER CHECKS|=========================================
		//------------------------------------
		// CHECK IF PLAYER LANDED ON WATER
		//------------------------------------
		if(%damagedClient == %shooterClient && %type == $LandingDamageType)
		{
			%object = "";
			for(%i = 0; %i >= -3.15; %i -= 1.57)
			{
				if(GameBase::getLOSInfo(Client::getOwnedObject(%damagedClient), 5, %i @ " 0 0"))
				{
					if(getObjectType($los::object) == "InteriorShape" && String::getSubStr(Object::getName($los::object), 0, 5) == "water")
					{
						%object = $los::object;
						break;
					}
				}
			}
			
			if(%object != "")
			{
				%value *= $waterDamageAmp;
				playSound(SoundSplash1, %damagedClientPos);
			}
		}

		//---------------------------------------
		// CHECK IF PLAYER LANDED WHILE IN WATER
		//---------------------------------------
		if(%damagedClient == %shooterClient && %type == $LandingDamageType)
		{
			if(Zone::getType(fetchData(%damagedClient, "zone")) == "WATER")
				%value *= $waterDamageAmp;
                
            if(fetchData(%damagedClient,"CatsFeetFlag"))
            {
                if(getSimTime() <= fetchData(%damagedClient,"CatsFeetTimeout") )
                {
                    %value = 0;
                }
                else
                {
                    storeData(%damagedClient,"CatsFeetFlag","");
                }
            }
            
            %value *= $FallDamageScale;
		}
		//============================================================================================

		//-------------------------------------------------
		// IF PLAYER IS ADMIN, NULLIFY LANDING DAMAGE
		// IF PLAYER IS SUPERADMIN, NULLIFY ALL DAMAGE
		//-------------------------------------------------
		if(%damagedClient.adminLevel >= 4 && %type == $LandingDamageType)
			%value = 0;
		if(%damagedClient.adminLevel >= 5)
			%value = 0;

		//------------------------------------------------
		// SAME TEAM CHECKS
		//------------------------------------------------
		if(Client::getTeam(%damagedClient) == Client::getTeam(%shooterClient) && %shooterClient != %damagedClient)
		{
			if(!HasTheftFlag(%damagedClient))
			{
				if(Zone::getType(fetchData(%damagedClient, "zone")) == "PROTECTED" && Zone::getType(fetchData(%shooterClient, "zone")) != "PROTECTED")
				{
					//echo("guy inside gets hit by guy outside, or vice-versa, no damage");
					%value = 0;
					%isMiss = False;
					%noImpulse = True;
					%sameTeamNull = True;
				}
				else if(Zone::getType(fetchData(%damagedClient, "zone")) != "PROTECTED" && Zone::getType(fetchData(%shooterClient, "zone")) != "PROTECTED")
				{
					//echo("both guys outside, do damage");
				}
				else if(Zone::getType(fetchData(%damagedClient, "zone")) == "PROTECTED" && Zone::getType(fetchData(%shooterClient, "zone")) == "PROTECTED" && %shooterClient != %damagedClient)
				{
					//echo("both inside, so no damage");
					%value = 0;
					%isMiss = False;
					%noImpulse = True;
					%sameTeamNull = True;
				}

				//not in the same house
				if( !(IsInCommaList(fetchData(%damagedClient, "targetlist"), Client::getName(%shooterClient)) || IsInCommaList(fetchData(%shooterClient, "targetlist"), Client::getName(%damagedClient))) )
				{
					//no target-list involved
		
					%dhn = GetHouseNumber(fetchData(%damagedClient, "MyHouse"));
					%shn = GetHouseNumber(fetchData(%shooterClient, "MyHouse"));
					if(%dhn == %shn)
					{
						%value = 0;
						%isMiss = False;
						%noImpulse = True;
						%sameTeamNull = True;
					}
					else
					{
						if(%dhn == "" || %shn == "")
						{
							//one of the people involved is not in a house, so no damage occurs
							%value = 0;
							%isMiss = False;
							%noImpulse = True;
							%sameTeamNull = True;
						}
					}
				}
				else
				{
					//one of the people involved has the other one on his/her target-list.
					//so let damage go thru
				}
	
				//AI same team check
				//if(Player::isAiControlled(%damagedClient))
				//{
				//	%value = 0;
				//	%isMiss = False;
				//	%noImpulse = True;
				//}
			}
		}
		//-------------------------------------------------
		// SAME PLAYER CHECKS
		//-------------------------------------------------
		if(%damagedClient == %shooterClient)
		{
			if(%type == $SpellDamageType)
				%value = %value / 3;
            if(%type == $BladeBoltDamageType)
                %value = 0;
		}

		//-------------------------------------------------
		// ARENA DAMAGE CHECKS
		//-------------------------------------------------
		if(IsStillArenaFighting(%damagedClient) != IsStillArenaFighting(%shooterClient))
		{
			%value = 0;						//example: spectator shooting in arena
			%arenaNull = True;
		}
		if(IsInRoster(%damagedClient) != IsInRoster(%shooterClient))
		{
			%value = 0;						//example: roster shooting in arena
			%arenaNull = True;
		}
		if(IsInRoster(%damagedClient))
		{
			%value = 0;						//example: arena shooting in roster
			%arenaNull = True;
		}

        if(fetchData(%damagedClient,"ubShielded"))
        {
            %isMiss = true;
            %noImpulse = true;
        }
        
		if(!IsDead(%this))
		{
			%armor = Player::getArmor(%this);
			storeData(%damagedClient, "tmpkillerid", %shooterClient);

			%hitby = Client::getName(%shooterClient);
			%msgcolor = "";
			if(%isMiss)
			{
				%msgcolor = $MsgRed;
				%value = 0;
			}
			else if(!%isMiss && %value == 0 && %shooterClient != %damagedClient)
			{
				%msgcolor = $MsgWhite;
			}
			if(%msgcolor != "")
			{
				if(%type != $SpellDamageType)
				{
					Client::sendMessage(%shooterClient, %msgcolor, "You try to hit " @ Client::getName(%damagedClient) @ ", but miss!");

					%time = getIntegerTime(true) >> 5;
					if(%time - %damagedClient.lastMissMessage > 2)
					{
						%damagedClient.lastMissMessage = %time;
						Client::sendMessage(%damagedClient, %msgcolor, %hitby @ " tries to hit you, but misses!");
					}
				}
				else
				{
					Client::sendMessage(%shooterClient, %msgcolor, Client::getName(%damagedClient) @ " resists your spell!");
                    if(!Player::isAIControlled(%shooterClient))
                        remoteEval(%shooterClient,"ATKText", "<jc>RESISTED!", true);
					Client::sendMessage(%damagedClient, %msgcolor, "You resist " @ %hitby @ "'s spell!");
                    if(!Player::isAIControlled(%damagedClient))
                        remoteEval(%damagedClient,"ATKText", "<jc>You RESISTED "@%hitby@"'s spell!", false);
				}
			}

			//-------------------------------------------------
			// SKILLS
			//-------------------------------------------------
			if(%skilltype >= 1 && !%arenaNull && !%sameTeamNull && %shooterClient != %damagedClient)
			{
				%base1 = Cap(35 + (fetchData(%shooterClient, "LVL") - fetchData(%damagedClient, "LVL")), 1, "inf");
				%base2 = Cap(35 + (fetchData(%damagedClient, "LVL") - fetchData(%shooterClient, "LVL")), 1, "inf");
				if(%isMiss)
				{
					UseSkill(%shooterClient, %skilltype, False, True);
					UseSkill(%damagedClient, $SkillEndurance, True, True, 60);
					if(%type == $SpellDamageType)
						UseSkill(%damagedClient, $SkillManaManipulation, True, True, %base2);
					//else
						//UseSkill(%damagedClient, $SkillDodging, True, True, %base2 * (3/5));
				}
				else if(!%isMiss && %value == 0)
				{
					UseSkill(%shooterClient, %skilltype, False, True);
					UseSkill(%damagedClient, $SkillEndurance, True, True, 60);
					if(%type == $SpellDamageType)
						UseSkill(%damagedClient, $SkillManaManipulation, True, True, %base2);
					//else
						//UseSkill(%damagedClient, $SkillDodging, True, True, %base2 * (3/5));
				}
				else
				{
                    UseSkill(%damagedClient, $SkillEndurance, True, True, 80);
					UseSkill(%shooterClient, %skilltype, True, True, %base1);
					if(%type == $SpellDamageType)
						UseSkill(%damagedClient, $SkillManaManipulation, True, True, %base2);
				}
                
                //Award TP
                if(%shooterClient.blockTP == "")
                {
                    addTP(%shooterClient,1);
                    %shooterClient.blockTP = true;
                    schedule("resetTPBlock("@%shooterClient@");",0.3);
                }
                
				if(%Backstab)
					UseSkill(%shooterClient, $SkillBackstabbing, True, True);
				if(%Bash)
					UseSkill(%shooterClient, $SkillBashing, True, True);
			}

			if(%value)
			{
				if(%value < 0)
					%value = 0;
				%backupValue = %value;
                //echo(%damagedClient);
                if(!(%damagedClient.sleepMode == "" || !%damagedClient.sleepMode))
                {
                    //echo(%damagedClient.sleepMode);
                    %value = %value * 10.0; //OUCH!
                    Client::sendMessage(%damagedClient, $MsgRed, "You got caught off guard!");
                    Client::sendMessage(%shooterClient, $MsgBeige, "You sneak attacked "@ Client::getName(%damagedClient));
                    UseSkill(%shooterClient, $SkillBackstabbing, True, True,1); //Give them a level in backstabbing.  They deserve it
                    ForceWakeUp(%damagedClient);
                }
                
                if(!%sameTeamNull && %heavyStrike)
                {
                    UpdateBonusState(%damagedClient,"AMR -5",$Ability::ticks[$Ability::index[heavystrike]]);
                }
                
				%rhp = refreshHP(%damagedClient, %value);
                if(%shooterClient != %damagedClient && %type != $SpellDamageType)
                {
                    %mt = fetchData(%shooterClient,"MANAThief");
                    if(%mt > 0)
                    {
                        refreshMANA(%shooterClient,-1*%mt);
                        
                        Client::sendMessage(%shooterClient,$MsgWhite,"You restored "@%mt@" mana.");
                    }
                }
                
				if(%rhp == -1)
					%value = -1;	//There was an LCK miss
				else
				{
					if(!%noImpulse)
                    {
                        %brace = fetchData(%damagedClient,"Brace");
                        if(%brace > 0)
                            %mom = ScaleVector(%mom,%brace/100);
                        Player::applyImpulse(%this,%mom);
                    }
					%noImpulse = "";

					if(%damagedCurrentArmor != "")
						%ahs = $ArmorHitSound[%damagedCurrentArmor];
					else
						%ahs = SoundHitFlesh;
					if(%skilltype == $SkillSlashing)
						PlaySound(%ahs, %damagedClientPos);
					else if(%skilltype == $SkillBludgeoning)
						PlaySound(%ahs, %damagedClientPos);
					else if(%skilltype == $SkillPiercing)
						PlaySound(%ahs, %damagedClientPos);
					else if(%skilltype == $SkillArchery)
						PlaySound(SoundArrowHit2, %damagedClientPos);
				}

				if(Player::isAiControlled(%damagedClient) && fetchData(%damagedClient, "SpawnBotInfo") != "")
				{
					if(!IsDead(%damagedClient))
					{
                        if(fetchData(%damagedClient,"customAIFlag") == "")
                        {
                            %loadOutTag = fetchData(%damagedClient,"BotLoadoutTag");
                            if(%loadOutTag == "")
                                %loadOutTag = "Default";
                            if($AIBehavior[%loadOutTag,RunOnLowHP])
                            {
                                %perc = fetchData(%damagedClient,"HP") / fetchData(%damagedClient,"MaxHP");
                                echo(%perc);
                                if(%perc <= 0.15)
                                {
                                    AI::RunAway(fetchData(%damagedClient, "BotInfoAiName"));
                                }
                            }
                            %tgt = AI::getTarget(fetchData(%damagedClient, "BotInfoAiName"));
                            if(%tgt != %shooterClient)
                            {
                                if(fetchData(%damagedClient,"aiAutoRetaliate") && %tgt == -1)
                                    AI::newDirectiveFollow(fetchData(%damagedClient, "BotInfoAiName"), %shooterClient, 0, 99);
                                else
                                    AI::SelectMovement(fetchData(%damagedClient, "BotInfoAiName"));
                                
                            }
                        }
					}

					PlaySound(RandomRaceSound(fetchData(%damagedClient, "RACE"), Hit), %damagedClientPos);
				}

				//display amount of damage caused
				%convValue = round(%value * $TribesDamageToNumericDamage);

				if(%convValue > 0)
				{
					if(%shooterClient == %damagedClient)
					{
						if(%type == $CrushDamageType)
							%hitby = "moving object";
						else if(%type == $DebrisDamageType)
							%hitby = "debris";
						else
							%hitby = "yourself";
					}
					else if(%shooterClient == 0)
                    {
                        if(%type == $MeteorDamageType)
                            %hitby = "meteor";
                        else
                            %hitby = "an NPC";
                    }
					else
					{
						if(fetchData(%shooterClient, "invisible"))
							%hitby = "an unknown assailant";
						else
							%hitby = Client::getName(%shooterClient);
					}
                    %dtxt = %convValue@" DMG!";
                    %stxt = %convValue@" DMG!";
					if(%Backstab)
					{
						%daction = "backstabbed";
						%saction = "backstabbed";
                        %dtxt = "Backstabbed! "@ %dtxt;
                        %stxt = "Backstabbed! "@ %stxt;
					}
					else if(%Bash)
					{
						%daction = "bashed";
						%saction = "bashed";
                        %dtxt = "Bashed! "@ %dtxt;
                        %stxt = "Bashed! "@ %stxt;
					}
                    else if(%heavyStrike)
                    {
                        %daction = "heavily struck";
                        %saction = "heavily struck";
                        %dtxt = "HEAVY STRIKE! "@ %dtxt;
                        %stxt = "HEAVY STRIKE! "@ %stxt;
                    }
					else
					{
						%daction = "damaged";
						%saction = "damaged";
					}

					//--------------------
					//display to involved
					//--------------------
					if(%shooterClient != %damagedClient)
                    {
                        %mm = "You " @ %saction @ " " @ Client::getName(%damagedClient) @ " for " @ %convValue @ " points of damage!";
                        if(%empower)
                            %mm = %mm @ "~wUnravelAM.wav";
						Client::sendMessage(%shooterClient, $MsgRed, %mm);
                        storeData(%shooterClient,"EmpowerFlag","");
                        remoteEval(%shooterClient,"ATKText", "<jc>"@%stxt, true);
                    }

					Client::sendMessage(%damagedClient, $MsgRed, "You were " @ %daction @ " by " @ %hitby @ " for " @ %convValue @ " points of damage!");
                    remoteEval(%damagedClient,"ATKText", "<jc>"@%dtxt, false);
					//--------------------
					//display to radius
					//--------------------
					if(%shooterClient == 0)
					{
						%sname = "An NPC";
						%dname = Client::getName(%damagedClient);
					}
					else if(%shooterClient == %damagedClient)
					{
						%sname = Client::getName(%shooterClient);
						if(String::ICompare(Client::getGender(%damagedClient), "Male") == 0)
							%dname = "himself";
						else if(String::ICompare(Client::getGender(%damagedClient), "Female") == 0)
							%dname = "herself";
						else
							%dname = "itself";
					}
					else
					{
						if(fetchData(%shooterClient, "invisible"))
							//%sname = "An unknown assailant";
                            %sname = "A smooth criminal";
						else
							%sname = Client::getName(%shooterClient);
						%dname = Client::getName(%damagedClient);
					}

					radiusAllExcept(%damagedClient, %shooterClient, %sname @ " hits " @ %dname @ " for " @ %convValue @ " points of damage!");
				}
				else if(%convValue < 0)
				{
					//this happens when there's a LCK consequence as miss

					%hitby = Client::getName(%shooterClient);

					Client::sendMessage(%shooterClient, $MsgRed, "You try to hit " @ Client::getName(%damagedClient) @ ", but miss! (LCK)");
					Client::sendMessage(%damagedClient, $MsgRed, %hitby @ " tries to hit you, but misses! (LCK)");
                    if(!Player::isAiControlled(%shooterClient))
                        remoteEval(%shooterClient,"ATKText", "<jc>MISS! (LCK)", true);
                    if(!Player::isAiControlled(%damagedClient))
                        remoteEval(%damagedClient,"ATKText", "<jc>"@%hitby@" MISSED! (LCK)", false);
				}

				//-------------------------------------------
				//add entry to damagedClient's damagedBy list
				//-------------------------------------------

				//make new entry with shooter's name
				if( %shooterClient != 0 && !%isMiss)
				{
					if(%shooterClient == 0)
						%sname = "NPC";
					else
						%sname = Client::getName(%shooterClient);
					%dname = Client::getName(%damagedClient);
					if(%shooterClient != %damagedClient)
					{
						%index = "";
						for(%i = 1; %i <= $maxDamagedBy; %i++)
						{
							if($damagedBy[%dname, %i] == "" && %index == "")
								%index = %i;
						}
						if(%index != "")
						{
							$damagedBy[%dname, %index] = %sname @ " " @ %backupValue;
							schedule("$damagedBy[\"" @ %dname @ "\", " @ %index @ "] = \"\";", $damagedByEraseDelay);
						}
						else
						{
							//too many hits on waiting list, he doesn't get in on exp.
						}
					}
				}

				%flash = Player::getDamageFlash(%this) + %value * 2;
				if(%flash > 0.75)
					%flash = 0.75;
				Player::setDamageFlash(%this,%flash);

				//slow the player down for a bit
                if($SlowDownHitEnabled)
                {
                    if(!Player::isAiControlled(%damagedClient))
                    {
                        storeData(%damagedClient, "SlowdownHitFlag", True);
                        RefreshWeight(%damagedClient);
                        schedule("storeData(" @ %damagedClient @ ", \"SlowdownHitFlag\", False);", $SlowdownHitDelay);
                        schedule("RefreshWeight(" @ %damagedClient @ ");", $SlowdownHitDelay, Client::getOwnedObject(%damagedClient));
                    }
                }
				//If player not dead then play a random hurt sound
				if(!Player::IsDead(%this))
				{
					if(%damagedClient.lastDamage < getSimTime())
					{
						%sound = radnomItems(3,injure1,injure2,injure3);
						playVoice(%damagedClient,%sound);
						%damagedClient.lastdamage = getSimTime() + 1.5;
					}
				}
				else		//player died
				{
					if(Player::isAiControlled(%shooterClient))
					{
						RemotePlayAnim(%shooterClient, 8);
						PlaySound(RandomRaceSound(fetchData(%shooterClient, "RACE"), Taunt), %shooterClientPos);
					}

					if( Player::isCrouching(%this) )
						%curDie = $PlayerAnim::Crouching;
					else
						%curDie = radnomItems(3, $PlayerAnim::DieLeftSide, $PlayerAnim::DieChest, $PlayerAnim::DieForwardKneel);

					Player::setAnimation(%this, %curDie);

					if(%type == $ImpactDamageType && %object.clLastMount != "")
						%shooterClient = %object.clLastMount;

					Client::onKilled(%damagedClient, %shooterClient, %type);
				}
			}

			if(%isMiss)
			{
				if(fetchData(%damagedClient, "isBonused") || fetchData(%damagedClient,"ubShielded"))
				{
					GameBase::activateShield(%this, "0 0 1.57", 1.47);
					PlaySound(SoundHitShield, %damagedClientPos);
				}
			}
		}
	}
}

function remoteKill(%clientId)
{
	dbecho($dbechoMode2, "remoteKill(" @ %clientId @ ")");
    echo("kill");
	if(!$matchStarted)
		return;
	if(IsJailed(%clientId))
		return;

	%player = Client::getOwnedObject(%clientId);
	if(%player != -1 && getObjectType(%player) == "Player" && !IsDead(%clientId) && IsInRoster(%clientId) == False)
	{
		storeData(%clientId, "LCK", 1, "dec");

		if(fetchData(%clientId, "LCK") >= 0)
			Client::sendMessage(%clientId, $MsgRed, "You have permanently lost an LCK point!");

		playNextAnim(%clientId);
		Player::kill(%clientId);
	}
}
