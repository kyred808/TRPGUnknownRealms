//rpg admin

$curVoteTopic = "";
$curVoteAction = "";
$curVoteOption = "";
$curVoteCount = 0;

function Admin::changeMissionMenu(%clientId)
{
}

function processMenuCMType(%clientId, %options)
{
}

function processMenuCMission(%clientId, %option)
{
}

function remoteAdminPassword(%clientId, %password)
{
	//if($AdminPassword != "" && %password == $AdminPassword[4])
	//{
	//	%clientId.adminLevel = 4;
	//}
}


function remoteSetPassword(%clientId, %password)
{
	if(%clientId.adminLevel >= 5)
		$Server::Password = %password;
}

function remoteSetTimeLimit(%clientId, %time)
{
}

function remoteSetTeamInfo(%clientId, %team, %teamName, %skinBase)
{
}

function remoteVoteYes(%clientId)
{
   %clientId.vote = "yes";
   centerprint(%clientId, "", 0);
}

function remoteVoteNo(%clientId)
{
   %clientId.vote = "no";
   centerprint(%clientId, "", 0);
}

function Admin::startMatch(%admin)
{
}

function Admin::setTeamDamageEnable(%admin, %enabled)
{
}

function Admin::kick(%admin, %clientId, %ban)
{
   if(%admin == -1 || %admin.adminLevel >= 4)
   {
      if(%ban && %admin.adminLevel < 5)
         return;
         
      if(%ban)
      {
         %word = "banned";
         %cmd = "BAN: ";
      }
      else
      {
         %word = "kicked";
         %cmd = "KICK: ";
      }
      if(%clientId.adminLevel >= 5)
      {
         if(%admin == -1)
            messageAll(0, "A super admin cannot be " @ %word @ ".");
         else
            Client::sendMessage(%admin, 0, "A super admin cannot be " @ %word @ ".");
         return;
      }
      %ip = Client::getTransportAddress(%clientId);

      echo(%cmd @ %admin @ " " @ %clientId @ " " @ %ip);

      if(%ip == "")
         return;
      if(%ban)
         BanList::add(%ip, 1800);
      else
         BanList::add(%ip, 180);

      %name = Client::getName(%clientId);

      if(%admin == -1)
      {
         MessageAll(0, %name @ " was " @ %word @ " from vote.");
         Net::kick(%clientId, "You were " @ %word @ " by  consensus.");
      }
      else
      {
         MessageAll(0, %name @ " was " @ %word @ " by " @ Client::getName(%admin) @ ".");
         Net::kick(%clientId, "You were " @ %word @ " by " @ Client::getName(%admin));
      }
   }
}


function Admin::setModeFFA(%clientId)
{
}

function Admin::setModeTourney(%clientId)
{
}

function Admin::voteFailed()
{
   $curVoteInitiator.numVotesFailed++;

   if($curVoteAction == "kick" || $curVoteAction == "admin")
      $curVoteOption.voteTarget = "";
}

function Admin::voteSucceded()
{
   $curVoteInitiator.numVotesFailed = "";
   if($curVoteAction == "kick")
   {
//      if($curVoteOption.voteTarget)
//         Admin::kick(-1, $curVoteOption);
   }
   else if($curVoteAction == "admin")
   {
      if($curVoteOption.voteTarget)
      {
//         $curVoteOption.adminLevel = 4;
         messageAll(0, Client::getName($curVoteOption) @ " has become an administrator.");
         if($curVoteOption.menuMode == "options")
            Game::menuRequest($curVoteOption);
      }
      $curVoteOption.voteTarget = false;
   }
}

function Admin::countVotes(%curVote)
{
   // if %end is true, cancel the vote either way
   if(%curVote != $curVoteCount)
      return;

   %votesFor = 0;
   %votesAgainst = 0;
   %votesAbstain = 0;
   %totalClients = 0;
   %totalVotes = 0;
   for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
   {
      %totalClients++;
      if(%cl.vote == "yes")
      {
         %votesFor++;
         %totalVotes++;
      }
      else if(%cl.vote == "no")
      {
         %votesAgainst++;
         %totalVotes++;
      }
      else
         %votesAbstain++;
   }
   %minVotes = floor($Server::MinVotesPct * %totalClients);
   if(%minVotes < $Server::MinVotes)
      %minVotes = $Server::MinVotes;

   if(%totalVotes < %minVotes)
   {
      %votesAgainst += %minVotes - %totalVotes;
      %totalVotes = %minVotes;
   }
   %margin = $Server::VoteWinMargin;
   if($curVoteAction == "admin")
   {
      %margin = $Server::VoteAdminWinMargin;
      %totalVotes = %votesFor + %votesAgainst + %votesAbstain;
      if(%totalVotes < %minVotes)
         %totalVotes = %minVotes;
   }
   if(%votesFor / %totalVotes >= %margin)
   {
      messageAll(0, "Vote to " @ $curVoteTopic @ " passed: " @ %votesFor @ " to " @ %votesAgainst @ " with " @ %totalClients - (%votesFor + %votesAgainst) @ " abstentions.");
      Admin::voteSucceded();
   }
   else  // special team kick option:
   {
      if($curVoteAction == "kick") // check if the team did a majority number on him:
      {
         %votesFor = 0;
         %totalVotes = 0;
         for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         {
            if(GameBase::getTeam(%cl) == $curVoteOption.kickTeam)
            {
               %totalVotes++;
               if(%cl.vote == "yes")
                  %votesFor++;
            }
         }
         if(%totalVotes >= $Server::MinVotes && %votesFor / %totalVotes >= $Server::VoteWinMargin)
         {
            messageAll(0, "Vote to " @ $curVoteTopic @ " passed: " @ %votesFor @ " to " @ %totalVotes - %votesFor @ ".");
            Admin::voteSucceded();
            $curVoteTopic = "";
            return;
         }
      }
      messageAll(0, "Vote to " @ $curVoteTopic @ " did not pass: " @ %votesFor @ " to " @ %votesAgainst @ " with " @ %totalClients - (%votesFor + %votesAgainst) @ " abstentions.");
      Admin::voteFailed();
   }
   $curVoteTopic = "";
}

function Admin::startVote(%clientId, %topic, %action, %option)
{
   if(%clientId.lastVoteTime == "")
      %clientId.lastVoteTime = -$Server::MinVoteTime;

   // we want an absolute time here.
   %time = getIntegerTime(true) >> 5;
   %diff = %clientId.lastVoteTime + $Server::MinVoteTime - %time;

   if(%diff > 0)
   {
      Client::sendMessage(%clientId, 0, "You can't start another vote for " @ floor(%diff) @ " seconds.");
      return;
   }
   if($curVoteTopic == "")
   {
      if(%clientId.numFailedVotes)
         %time += %clientId.numFailedVotes * $Server::VoteFailTime;

      %clientId.lastVoteTime = %time;
      $curVoteInitiator = %clientId;
      $curVoteTopic = %topic;
      $curVoteAction = %action;
      $curVoteOption = %option;
      if(%action == "kick")
         $curVoteOption.kickTeam = GameBase::getTeam($curVoteOption);
      $curVoteCount++;
      bottomprintall("<jc><f1>" @ Client::getName(%clientId) @ " <f0>initiated a vote to <f1>" @ $curVoteTopic, 10);
      for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         %cl.vote = "";
      %clientId.vote = "no"; // yes
      for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         if(%cl.menuMode == "options")
            Game::menuRequest(%clientId);
      schedule("Admin::countVotes(" @ $curVoteCount @ ", true);", $Server::VotingTime, 35);
   }
   else
   {
      Client::sendMessage(%clientId, 0, "Voting already in progress.");
   }
}

function Game::menuRequest(%clientId,%page)
{
	if(%clientId.IsInvalid)
		return;

	if(%clientId.choosingGroup)
	{
		MenuGroup(%clientId);
		return;
	}
	else if(%clientId.choosingClass)
	{
		MenuClass(%clientId);
		return;
	}

    if(%page == "")
        %page = 1;
    
	%curItem = 0;
	Client::buildMenu(%clientId, "Options", "options", true);
	if($curVoteTopic != "" && %clientId.vote == "")
	{
		Client::addMenuItem(%clientId, %curItem++ @ "Vote YES to " @ $curVoteTopic, "voteYes " @ $curVoteCount);
		Client::addMenuItem(%clientId, %curItem++ @ "Vote NO to " @ $curVoteTopic, "voteNo " @ $curVoteCount);
	}
	else
	{
		if(%clientId.selClient)
		{
			%sel = %clientId.selClient;
			%selname = Client::getName(%sel);
	
			if(%clientId != %sel && fetchData(%sel, "HasLoadedAndSpawned"))
			{
                        if(IsInCommaList(fetchData(%clientId, "grouplist"), %selname))
					Client::addMenuItem(%clientId, %curItem++ @ "Remove from group-list", "remgroup " @ %sel);
				else
					Client::addMenuItem(%clientId, %curItem++ @ "Add to group-list", "addgroup " @ %sel);

                        if(IsInCommaList(fetchData(%clientId, "targetlist"), %selname))
					Client::addMenuItem(%clientId, %curItem++ @ "Remove from target-list", "remtarget " @ %sel);
				else
					Client::addMenuItem(%clientId, %curItem++ @ "Add to target-list", "addtarget " @ %sel);

				if(fetchData(%clientId, "partyOwned"))
				{
					if(IsInCommaList(fetchData(%clientId, "partylist"), %selname))
						Client::addMenuItem(%clientId, %curItem++ @ "Remove from your party", "remparty " @ %sel);
					else
					{
						if(CountObjInCommaList(fetchData(%clientId, "partylist")) < $maxpartymembers)
						{
							%p = IsInWhichParty(Client::getName(%sel));
							if(%p == -1)
								Client::addMenuItem(%clientId, %curItem++ @ "Invite to your party", "addparty " @ %sel);
							else if(GetWord(%p, 1) == "i")
								Client::addMenuItem(%clientId, %curItem++ @ "Cancel invitation", "cancelinv " @ %sel);
							else
								Client::addMenuItem(%clientId, %curItem++ @ "(Can't invite, already in a party)", "");
						}
						else
							Client::addMenuItem(%clientId, %curItem++ @ "(Can't invite, too many members)", "");
					}
				}

				if(%clientId.muted[%sel])
					Client::addMenuItem(%clientId, %curItem++ @ "Unmute", "unmute " @ %sel);
				else
					Client::addMenuItem(%clientId, %curItem++ @ "Mute", "mute " @ %sel);

			}
		}
		else
		{
            RPG::gameMenu(%clientId,%page);
			//if(!IsDead(%clientId))
			//	Client::addMenuItem(%clientId, %curItem++ @ "View your stats" , "viewstats");
	        //
			//if(fetchData(%clientId, "defaultTalk") == "#say")
			//	Client::addMenuItem(%clientId, %curItem++ @ "Set default talk: #group" , "defgroup");
			//else
			//	Client::addMenuItem(%clientId, %curItem++ @ "Set default talk: #say" , "defsay");
            //
			//if(GetAccessoryList(%clientId, 9, -1) != "")
			//	Client::addMenuItem(%clientId, %curItem++ @ "Ranged weapons..." , "rweapons");
	        //
            //Client::addMenuItem(%clientId, %curItem++ @ "Spell Book..." , "spellbook");
			//if(!IsDead(%clientId))
			//	Client::addMenuItem(%clientId, %curItem++ @ "Skill points..." , "sp");
            //
			//if(fetchData(%clientId, "ignoreGlobal"))
			//	Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global OFF" , "gignoreoff");
			//else
			//	Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global ON" , "gignoreon");
            //
			//if(fetchData(%clientId, "LCKconsequence") == "miss")
			//	Client::addMenuItem(%clientId, %curItem++ @ "Set LCK mode = death" , "lckdeath");
			//else if(fetchData(%clientId, "LCKconsequence") == "death")
			//	Client::addMenuItem(%clientId, %curItem++ @ "Set LCK mode = miss" , "lckmiss");
            //
			//Client::addMenuItem(%clientId, %curItem++ @ "Party options..." , "partyoptions");
            //
            //if(!IsDead(%clientId))
			//	Client::addMenuItem(%clientId, %curItem++ @ "Belt","viewbelt");
		}
//		Client::addMenuItem(%clientId, %curItem++ @ "other...", "Other");
	}
}

function RPG::gameMenu(%clientId,%page)
{
    if(!IsDead(%clientId))
    {
        if(%page == 1)
        {
            Client::addMenuItem(%clientId, %curItem++ @ "View your stats" , "viewstats");
            Client::addMenuItem(%clientId, %curItem++ @ "Attributes..." , "viewattributes");
            Client::addMenuItem(%clientId, %curItem++ @ "Weapon Options..." , "weaponopts");
            Client::addMenuItem(%clientId, %curItem++ @ "Skill points..." , "sp");
            Client::addMenuItem(%clientId, %curItem++ @ "Info..." , "info");
            Client::addMenuItem(%clientId, %curItem++ @ "RPG Options..." , "rpgopt");
            
            //Client::addMenuItem(%clientId, "nNext >>" , "page "@ %page+1);
        //}
        //else if(%page == 2)
        //{
                
            
            Client::addMenuItem(%clientId, %curItem++ @ "Party options..." , "partyoptions");
            
            
            
        //    Client::addMenuItem(%clientId, "pPrev <<" , "page "@ %page-1);
        }
    }
}

function processMenuOptions(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuOptions(" @ %clientId @ ", " @ %option @ ")");

	%opt = getWord(%option, 0);
	%cl = floor(getWord(%option, 1));

	//**RPG
	if(%opt == "selspell")
	{
		Client::buildMenu(%clientId, "Select a spell", "selectspell", true);
		%curitem=1;
		%name = Client::getName(%clientId);

		for(%i=1; $spellShell[%i] != ""; %i++)
		{
			if(isInSpellList(%name, $spellShell[%i]) == 1)
			{
				Client::addMenuItem(%clientId, %curitem @ $spellName[%i], %i);
				%curitem++;
			}
		}

		return;
	}
    if(%opt == "weaponopts")
    {
        RPGWeaponOptionMenu(%clientId,1);
        return;
    }
    else if(%opt == "info")
    {
        RPGInfoMenu(%clientId,1);
    }
    else if(%opt == "rpgopt")
    {
        RPGOptionsMenu(%clientId,1);
    }
    else if(%opt == "viewattributes")
    {
        RPGAttributeMenu(%clientId,1);
    }
	else if(%opt == "viewstats")
	{
		%a[%tmp++] = "<f1>" @ Client::getName(%clientId) @ ", LEVEL " @ fetchData(%clientId, "LVL") @ " " @ fetchData(%clientId, "RACE") @ " " @ fetchData(%clientId, "CLASS") @ "<f0>\n\n";
        //%raw = CalculateRawDamage(%clientId,fetchData(%clientId,"EquippedWeapon"));
		%a[%tmp++] = "ATK: " @ Number::Beautify(fetchData(%clientId, "ATK"),0,2) @ "\n";
        %a[%tmp++] = "DMG: " @ PlayerDamageRangeText(%clientId,fetchData(%clientId,"EquippedWeapon")) @ "\n"; //Number::Beautify(%raw,0,2) @ "\n";//Number::Beautify(%raw * 0.85,0,2) @" - "@ Number::Beautify(%raw * 1.15,0,2) @"\n";
		%a[%tmp++] = "DEF: " @ Number::Beautify(fetchData(%clientId, "DEF"),0,2) @ " ("@ round(CalculateDamageReduction(%clientId)*100) @"%)\n";
        %amrval = fetchData(%clientId, "AMR");
        if(%amrval != 0)
            %a[%tmp++] = "AMR: " @ Number::Beautify(%amrval,0,2) @ "\n";
        %barval = fetchData(%clientId, "BAR");
        if(%barval != 0)
            %a[%tmp++] = "BAR: " @ Number::Beautify(%barval,0,2) @ "\n";
		%a[%tmp++] = "MDEF: " @ Number::Beautify(fetchData(%clientId, "MDEF"),0,2) @ "\n";
		%a[%tmp++] = "Hit Pts: " @ fetchData(%clientId, "HP") @ " / " @ fetchData(%clientId, "MaxHP") @ "\n";
        
        //%recharge = calcRechargeRate(%clientId);
        //%stam = fetchData(%clientId, "Stamina");
        //%maxStam = fetchData(%clientId, "MaxStam");
        //if(%stam == %maxStam)
        //    %recharge = 0;
        //if(%recharge >= 0)
        //    %recharge = "+"@Number::Beautify(%recharge, 0, 2);
        //else
        //    %recharge = Number::Beautify(%recharge, 0, 2);
            
		//%a[%tmp++] = "Stamina: " @ Number::Beautify(%stam,0,2) @ " / " @ %maxStam @ " ("@%recharge@")\n";
        %a[%tmp++] = "TP: " @ fetchData(%clientId,"TP") @"\n";
        %a[%tmp++] = "LCK: " @ fetchData(%clientId, "LCK") @ "\n";

		if(fetchData(%clientId, "MyHouse") != "")
		{
			%a[%tmp++] = "Rank Pts: " @ fetchData(%clientId, "RankPoints") @ "\n";
			%a[%tmp++] = "House: " @ fetchData(%clientId, "MyHouse") @ "\n";
		}

		%a[%tmp++] = "Experience: " @ fetchData(%clientId, "EXP") @ "\n";
            %a[%tmp++] = "Exp needed: " @ (GetExpToLevel(GetLevel(fetchData(%clientId, "EXP"), %clientId)+1, %clientId) - fetchData(%clientId, "EXP") @ "\n\n");
        %a[%tmp++] = "Magic Scaling\n<f1>Arcane: <f0>" @ fetchData(%clientId,"MagicScaling") @ "    <f1>Incant: <f0>"@ fetchData(%clientId,"IncantScaling") @"    <f1>Nature: <f0>"@ fetchData(%clientId,"NatureScaling") @"\n\n";
		%a[%tmp++] = "Coins: " @ fetchData(%clientId, "COINS") @ " - Bank: " @ fetchData(%clientId, "BANK") @ "\n";
		%a[%tmp++] = "TOTAL $: " @ fetchData(%clientId, "COINS") + fetchData(%clientId, "BANK") @ "\n\n";
		
		%a[%tmp++] = "Weight: " @ Number::Beautify(fetchData(%clientId, "Weight"),0,2) @ " / " @ Number::Beautify(fetchData(%clientId, "MaxWeight"),0,2) @ "\n";
		%a[%tmp++] = "Mana: " @ fetchData(%clientId, "MANA") @ " / " @ fetchData(%clientId, "MaxMANA") @ "\n";

		for(%i = 1; %a[%i] != ""; %i++)
			%f = %f @ %a[%i];

        %len = String::len(%f);
        if(%len > 255)
        {
            %substr = String::getsubstr(%f,0,255);
            remoteEval(%clientId,"BufferedCenterPrint",%substr, floor(%len / 20), 1);
            %substr = String::getSubstr(%f,255,%len);
            remoteEval(%clientId,"BufferedCenterPrint",%substr, -1, 1);
        }
        else
            bottomprint(%clientId, %f, floor(%len / 20));

		return;
	}
    else if(%opt == "page")
    {
        Game::menuRequest(%clientid,getWord(%option,1));
        return;
    }
	else if(%opt == "addgroup")
	{
		if(countObjInCommaList(fetchData(%clientId, "grouplist")) <= 30)
		{
			%name = Client::getName(%cl);
			storeData(%clientId, "grouplist", AddToCommaList(fetchData(%clientId, "grouplist"), %name));

			Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has added you to his/her group-list.");
			Client::sendMessage(%clientId, $MsgBeige, %name @ " is now on your group-list.");
		}
		else
			Client::sendMessage(%clientId, $MsgRed, "You have too many people on your group-list.");
	}
	else if(%opt == "remgroup")
	{
		%name = Client::getName(%cl);
		storeData(%clientId, "grouplist", RemoveFromCommaList(fetchData(%clientId, "grouplist"), %name));

		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has removed you from his/her group-list.");
		Client::sendMessage(%clientId, $MsgBeige, %name @ " is no longer on your group-list.");
	}
	else if(%opt == "addtarget")
	{
		if(countObjInCommaList(fetchData(%clientId, "targetlist")) <= 30)
		{
			%delay = 20;
			%name = Client::getName(%cl);
			Client::sendMessage(%clientId, $MsgRed, %name @ " will be added to your target-list in " @ %delay @ " seconds.");
			Client::sendMessage(%cl, $MsgRed, Client::getName(%clientId) @ " is thinking about killing you.");

			schedule("AddToTargetList(" @ %clientId @ ", " @ %cl @ ");", %delay, %cl);
		}
		else
			Client::sendMessage(%clientId, $MsgRed, "You have too many people on your target-list.");
	}
	else if(%opt == "remtarget")
	{
		%name = Client::getName(%cl);
		storeData(%clientId, "targetlist", RemoveFromCommaList(fetchData(%clientId, "targetlist"), %name));

		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has declared a truce.");
		Client::sendMessage(%clientId, $MsgBeige, %name @ " is no longer on your target-list.");
	}
	else if(%opt == "addparty")
	{
		%clientId.invitee[%cl] = True;
		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has invited you to join his/her party.");
		Client::sendMessage(%clientId, $MsgBeige, "You have invited " @ Client::getName(%cl) @ " to join your party.");
	}
	else if(%opt == "remparty")
	{
		%name = Client::getName(%cl);
		RemoveFromParty(%clientId, %name);
	}
	else if(%opt == "cancelinv")
	{
		%clientId.invitee[%cl] = "";
		Client::sendMessage(%cl, $MsgRed, Client::getName(%clientId) @ " has cancelled his invitation.");
		Client::sendMessage(%clientId, $MsgBeige, "You cancelled your invitation to " @ Client::getName(%cl) @ ".");
	}
	else if(%opt == "mute")
	      %clientId.muted[%cl] = True;
	else if(%opt == "unmute")
		%clientId.muted[%cl] = "";
	else if(%opt == "sp")
	{
		MenuSP(%clientId, 1);
		return;
	}
	else if(%opt == "partyoptions")
	{
		Client::buildMenu(%clientId, "Party options", "partyopt", true);

		if(fetchData(%clientId, "partyOwned"))
			Client::addMenuItem(%clientId, "xDisband party", "disbandparty");
		else
			Client::addMenuItem(%clientId, "cCreate party", "createparty");

		%name = Client::getName(%clientId);
		if( (%p = IsInWhichParty(%name)) != -1)
		{
			%id = GetWord(%p, 0);
			%inv = GetWord(%p, 1);
			if(%inv == -1)
			{
				//this player is in the party
				Client::addMenuItem(%clientId, "pLeave current party", "leaveparty " @ %id);
			}
			else if(%inv == "i")
			{
				//this player is being invited
				Client::addMenuItem(%clientId, "pAccept " @ Client::getName(%id) @ "'s party invitation", "acceptinv " @ %id);
			}
		}

		%list = fetchData(%clientId, "partylist");
		for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999))
		{
			%w = String::NEWgetSubStr(%list, 0, %p);
			Client::addMenuItem(%clientId, %curitem++ @ "Remove " @ %w, "remparty " @ %w);
		}
	}
	//**
}

function RPGAttributeMenu(%clientId,%page)
{
    Client::buildMenu(%clientId, "Attributes", "attributemenu", true);
    for(%i = 0; %i < $RPGStats::AttributeCount; %i++)
    {
        %attr = $RPGStats::Attributes[%i];
        //%desc = $RPGStats::Attributes[%i,Name];
        
        %base = fetchData(%clientId,%attr);
        %extra = RPGStats::getExtraAttributeValue(%clientId,%attr);
        
        %valStr = "[" @ %base;
        if(%extra > 0)
            %valStr = %valStr @ "+"@ %extra;
        %valStr = %valStr @ "]";
        Client::addMenuItem(%clientId, %i+1 @ %attr @ ": " @ %valStr, "selAttr "@ %i);
    }
    %ap = fetchData(%clientId,"APcredits");
    if(%ap > 0)
    {
        Client::addMenuItem(%clientId, "sSpend AP ("@ %ap @")", "spend");
    }
    Client::addMenuItem(%clientId, "bBack <<" , "back");
}

function processMenuattributemenu(%clientId,%option)
{
    %opt = getWord(%option,0);
    %attrId = getWord(%option,1);
    
    if(%opt == "selAttr")
    {
        RPGStats::DisplayAttributeInfo(%clientId,%attrId);
    }
    else if(%opt == "spend")
    {
        RPGIncreaseAttributesMenu(%clientId,1,"attr");
    }
    else if(%opt == "back")
    {
        Game::menuRequest(%clientId,1);
        return;
    }
}

//Can be added to
function RPGWeaponOptionMenu(%clientId,%page)
{
    Client::buildMenu(%clientId, "Weapon Options", "weaponoptions", true);
    Client::addMenuItem(%clientId, %curItem++ @ "Ranged weapons..." , "rweapons");
    Client::addMenuItem(%clientId, "bBack <<" , "back");
}

function processMenuweaponoptions(%clientId, %option)
{
    %opt = getWord(%option, 0);
	
    if(%opt == "rweapons")
	{
		%list = GetAccessoryList(%clientId, 9, -1);

		Client::buildMenu(%clientId, "Ranged weapons:", "selectrweapon", true);
		for(%i = 0; GetWord(%list, %i) != -1; %i++)
		{
			%item = GetWord(%list, %i);
            %name = RPGItem::getItemNameFromTag(%item);
            
			Client::addMenuItem(%clientId, %curitem++ @ %name, %item);
		}
		return;
	}
    else if(%opt == "back")
    {
        Game::menuRequest(%clientId,1);
        return;
    }
}

function RPGOptionsMenu(%clientId,%page)
{
    Client::buildMenu(%clientId, "RPG Options", "rpgoptionsmenu", true);
    
    if(fetchData(%clientId, "ignoreGlobal"))
        Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global OFF" , "gignoreoff");
    else
        Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global ON" , "gignoreon");
        
    if(fetchData(%clientId, "LCKconsequence") == "miss")
        Client::addMenuItem(%clientId, %curItem++ @ "Set LCK mode = death" , "lckdeath");
    else if(fetchData(%clientId, "LCKconsequence") == "death")
        Client::addMenuItem(%clientId, %curItem++ @ "Set LCK mode = miss" , "lckmiss");
        
    if(fetchData(%clientId, "defaultTalk") == "#say")
        Client::addMenuItem(%clientId, %curItem++ @ "Set default talk: #global" , "defglobal");
    else
        Client::addMenuItem(%clientId, %curItem++ @ "Set default talk: #say" , "defsay");
        
    Client::addMenuItem(%clientId, "bBack <<" , "back");
}

function processMenurpgoptionsmenu(%clientId,%option)
{
    %opt = getWord(%option, 0);
    if(%opt == "gignoreon")
	{
		storeData(%clientId, "ignoreGlobal", True);
	}
	else if(%opt == "gignoreoff")
	{
		storeData(%clientId, "ignoreGlobal", "");
	}
	else if(%opt == "lckmiss")
	{
		storeData(%clientId, "LCKconsequence", "miss");
	}
	else if(%opt == "lckdeath")
	{
		storeData(%clientId, "LCKconsequence", "death");
	}
    else if(%opt == "defglobal")
	{
		storeData(%clientId, "defaultTalk", "#global");
	}
	else if(%opt == "defsay")
	{
		storeData(%clientId, "defaultTalk", "#say");
	}
    else if(%opt == "back")
    {
        Game::menuRequest(%clientId,1);
        return;
    }
}

function RPGInfoMenu(%clientId,%page)
{
    Client::buildMenu(%clientId, "Information", "infomenu", true);
    Client::addMenuItem(%clientId, %curItem++ @ "Skill Book..." , "skillbook");
    Client::addMenuItem(%clientId, %curItem++ @ "Spell Book..." , "spellbook");
    Client::addMenuItem(%clientId, "bBack <<" , "back");
}

function processMenuinfomenu(%clientId, %option)
{
    %opt = getWord(%option, 0);
    if(%opt == "spellbook")
    {
        MenuViewSpellBook(%clientid, 1);
        return;
    }
    else if(%opt == "skillbook")
    {
        MenuViewSkillBook(%clientId,"root",1);
        return;
    }
    else if(%opt == "back")
    {
        Game::menuRequest(%clientId,1);
        return;
    }
}

function BonfireMenu(%clientId,%page)
{
    Client::buildMenu(%clientId, "What do you want to do?", "bonfiremenu", true);
    Client::addMenuItem(%clientId, %cnt++ @ "Rest","dorest");
    Client::addMenuItem(%clientId, %cnt++ @ "Level Up","dolevel");
    Client::addMenuItem(%clientId, %cnt++ @ "Spend Attribute Points","doattr");
    Client::addMenuItem(%clientId, "xDone", "done");
}

function processMenubonfiremenu(%clientId,%option)
{
    if(%option == "rest")
    {
        return;
    }
    else if(%option == "dolevel")
    {
        RPGLevelUpMenu(%clientId,1);
    }
    else if(%option == "doattr")
    {
        %clientId.attrSpentAmt = 0;
        %clientId.attrSpent = "";
        %clientId.attrSeq = "";
        RPGIncreaseAttributesMenu(%clientId,1,"bonfire");
    }
}

function RPGLevelUpMenu(%clientId,%page)
{
    %exp = fetchData(%clientId,"EXP");
    %lvl = fetchData(%clientId,"LVL");
    %need = GetExpToLevel(%lvl, %clientId);
    Client::buildMenu(%clientId, "EXP: "@ %exp @ " Need: "@ %need, "levelmenu", true);
    
    if(%exp >= %need)
        Client::addMenuItem(%clientId, %cnt++ @ "Level Up","level,1");
    else
        Client::addMenuItem(%clientId, %cnt++ @ "EXP too low to level","nope");
        
    Client::addMenuItem(%clientId, "xDone <<" , "done");
}

function processMenulevelmenu(%clientId,%option)
{
    //Using get word risks you losing a level if something messes up, because no word found results in -1
    %opt = String::getWord(%option,",",0);
    if(%opt == "level")
    {
        %amt = String::getWord(%option,",",1);
        if(%amt != ",")
        {
            %lvl = fetchData(%clientId,"LVL");
            %need = GetExpToLevel(%lvl, %clientId);
            RPG::DoLevelUp(%clientId,%amt);
            storeData(%clientId,"EXP",%need,"dec");
            RPGLevelUpMenu(%clientId,1);
        }
    }
    else if(%opt == "nope")
    {
        RPGLevelUpMenu(%clientId,1);
    }
    else if(%opt == "done")
    {
        return;
    }
}

function RPGIncreaseAttributesMenu(%clientId,%page,%prev)
{
    %ap = fetchData(%clientId,"APcredits");
    //if(%clientId.attrSpentAmt > 0)
    //    %txt = "You have " @ %ap - %clientId.attrSpentAmt @ " AP";
    //else
    //    %txt = "You have " @ %ap @ " AP";
    Client::buildMenu(%clientId, "You have " @ %ap - %clientId.attrSpentAmt @ " AP", "LevelAttr", true);

    for(%i = 0; %i < $RPGStats::AttributeCount; %i++)
    {
        %attr = $RPGStats::Attributes[%i];
        //%desc = $RPGStats::Attributes[%i,Name];
        
        %base = fetchData(%clientId,%attr);
        %extra = GetStuffStringCount(%clientId.attrSpent,%attr);
        //echo(%extra);
        %valStr = "[" @ %base;
        if(%extra > 0)
            %valStr = %valStr @ "+"@ %extra;
        %valStr = %valStr @ "]";
        Client::addMenuItem(%clientId, %i+1 @ %attr @ ": " @ %valStr, "selAttr "@ %attr @" "@%prev);
    }
    if(%clientId.attrSpentAmt > 0)
    {
        Client::addMenuItem(%clientId, "cConfirm to Apply","confirm "@ %prev);
        Client::addMenuItem(%clientId, "uUndo","undo " @ %prev);
    }
    else
        Client::addMenuItem(%clientId, "xBack","back "@ %prev);
}

function processMenuLevelAttr(%clientId,%option)
{
    %opt = getWord(%option,0);
    
    if(%opt == "selAttr")
    {
        if(%clientId.attrSpentAmt < fetchData(%clientId,"APcredits"))
        {
            %attr = getWord(%option,1);
            %prev = getWord(%option,2);
            %clientId.attrSpent = SetStuffString(%clientId.attrSpent,%attr,1,"inc");
            %clientId.attrSpentAmt++;
            %clientId.attrSeq = %attr @ " " @ %clientId.attrSeq;
        }
        RPGIncreaseAttributesMenu(%clientId,1,%prev);
    }
    else if(%opt == "undo")
    {
        %attr = getWord(%clientId.attrSeq,0);
        %clientId.attrSpent = SetStuffString(%clientId.attrSpent,%attr,1,"dec");
        %clientId.attrSpentAmt--;
        %clientId.attrSeq = Word::getSubWord(%clientId.attrSeq,1,99999);
        RPGIncreaseAttributesMenu(%clientId,1,getWord(%option,1));
    }
    else if(%opt == "confirm")
    {
        RPGApplyAttributes(%clientId,%clientId.attrSpent);
        storeData(%clientId,"APcredits",%clientId.attrSpentAmt,"dec");
        %clientId.attrSpentAmt = 0;
        %clientId.attrSpent = "";
        %clientId.attrSeq = "";
        RPGIncreaseAttributesMenu(%clientId,1,getWord(%option,1));
    }
    else if(%opt == "back")
    {
        %clientId.attrSpentAmt = 0;
        %clientId.attrSpent = "";
        %clientId.attrSeq = "";
        %prev = getWord(%option,1);
        if(%prev == "bonfire")
            BonfireMenu(%clientId,1);
        else if(%prev == "attr")
            RPGAttributeMenu(%clientId,1);
    }
        
}

function RPGApplyAttributes(%clientId,%attrUpdateStr)
{
    %playedSound = false;
    for(%i = 0; %i < $RPGStats::AttributeCount; %i++)
    {
        %attr = $RPGStats::Attributes[%i];
        %amt = GetStuffStringCount(%attrUpdateStr,%attr);
        if(%amt > 0)
        {
            storeData(%clientId,%attr,%amt,"inc");
            %txt = "Your "@ $RPGStats::Attributes[%i,Name] @" increased by "@ %amt @".";
            if(!%playedSound && $AttributeIncreaseSound != "")
            {
                %txt = %txt @ "~w" @ $AttributeIncreaseSound;
                %playedSound = true;
            }
            Client::sendMessage(%clientId,$MsgWhite,%txt);
        }
    }
}

function processMenupartyopt(%clientId, %option)
{
	dbecho($dbechoMode, "processMenupartyopt(" @ %clientId @ ", " @ %option @ ")");

	%opt = getWord(%option, 0);
	%cl = getWord(%option, 1);

	if(%opt == "disbandparty")
	{
		DisbandParty(%clientId);
	}
	else if(%opt == "createparty")
	{
		CreateParty(%clientId);
	}
	else if(%opt == "remparty")
	{
		RemoveFromParty(%clientId, %cl);
	}
	else if(%opt == "acceptinv")
	{
		%name = Client::getName(%clientId);
		if( (%p = IsInWhichParty(%name)) != -1)
		{
			%id = GetWord(%p, 0);
			%inv = GetWord(%p, 1);
			if(%inv == "i")
				AddToParty(%id, %name);
		}
	}
	else if(%opt == "leaveparty")
	{
		RemoveFromParty(%cl, Client::getName(%clientId));
	}

	return;
}

function processMenuselectspell(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuselectspell(" @ %clientId @ ", " @ %option @ ")");

	%name = Client::getName(%clientId);

	$playerCurrentSpell[%clientId] = $spellShell[%option];
}
function processMenuselectrweapon(%clientId, %item)
{
	%list = GetAccessoryList(%clientId, 10, -1);
    //echo(%list);
	Client::buildMenu(%clientId, "Projectiles:", "selectproj", true);
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%proj = GetWord(%list, %i);
        //echo(%proj);
        %name = RPGItem::getItemNameFromTag(%proj);
        %label = RPGItem::ItemTagToLabel(%proj);
		if(String::findSubStr($ProjRestrictions[%label], "," @ RPGItem::ItemTagToLabel(%item) @ ",") != -1)
			Client::addMenuItem(%clientId, %curitem++ @ %name, %item @ " " @ %proj);
	}
	return;
}
function processMenuselectproj(%clientId, %itemandproj)
{
	%item = GetWord(%itemandproj, 0);
	%proj = GetWord(%itemandproj, 1);

	storeData(%clientId, "LoadedProjectile " @ %item, %proj);
}

function processMenuOtheropt(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuOtheropt(" @ %clientId @ ", " @ %option @ ")");

	%opt = GetWord(%option, 0);
	%cl = GetWord(%option, 1);
	if(%opt == "mute")
	      %clientId.muted[%cl] = true;
	else if(%opt == "unmute")
		%clientId.muted[%cl] = "";
	else if(%opt == "voteYes" && %cl == $curVoteCount)
	{
	      %clientId.vote = "yes";
	 	centerprint(%clientId, "", 0);
	}
	else if(%opt == "voteNo" && %cl == $curVoteCount)
	{
	      %clientId.vote = "no";
	      centerprint(%clientId, "", 0);
	}
	else if(%opt == "kick")
	{
	      Client::buildMenu(%clientId, "Confirm kick:", "kaffirm", true);
	      Client::addMenuItem(%clientId, "1Kick " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't kick " @ Client::getName(%cl), "no " @ %cl);
	      return;
	}
	else if(%opt == "admin")
	{
	      Client::buildMenu(%clientId, "Confirm admim:", "aaffirm", true);
	      Client::addMenuItem(%clientId, "1Admin " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't admin " @ Client::getName(%cl), "no " @ %cl);
	      return;
	}
	else if(%opt == "ban")
	{
	      Client::buildMenu(%clientId, "Confirm Ban:", "baffirm", true);
	      Client::addMenuItem(%clientId, "1Ban " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't ban " @ Client::getName(%cl), "no " @ %cl);
		return;
	}
	Game::menuRequest(%clientId);
}

function remoteSelectClient(%clientId, %selId)
{
	dbecho($dbechoMode, "remoteSelectClient(" @ %clientId @ ", " @ %selId @ ")");

   if(%clientId.selClient != %selId)
   {
      %clientId.selClient = %selId;
      if(%clientId.menuMode == "options")
         Game::menuRequest(%clientId);
      remoteEval(%clientId, "setInfoLine", 1, "Player Info for " @ Client::getName(%selId) @ ":");
      remoteEval(%clientId, "setInfoLine", 2, "Real Name: " @ $Client::info[%selId, 1]);
      remoteEval(%clientId, "setInfoLine", 3, "Email Addr: " @ $Client::info[%selId, 2]);
      remoteEval(%clientId, "setInfoLine", 5, "URL: " @ $Client::info[%selId, 4]);
   }
}


function processMenuPickTeam(%clientId, %team, %adminClient)
{
	dbecho($dbechoMode, "processMenuPickTeam(" @ %clientId @ ", " @ %team @ ", " @ %adminClient @ ")");

   if(%team != -1 && %team == Client::getTeam(%clientId))
      return;

   if(%clientId.observerMode == "justJoined")
   {
      %clientId.observerMode = "";
      centerprint(%clientId, "");
   }

   if((!$matchStarted || !$Server::TourneyMode || %adminClient) && %team == -2)
   {
      if(Observer::enterObserverMode(%clientId))
      {
         %clientId.notready = "";
         if(%adminClient == "") 
            messageAll(0, Client::getName(%clientId) @ " became an observer.");
         else
            messageAll(0, Client::getName(%clientId) @ " was forced into observer mode by " @ Client::getName(%adminClient) @ ".");
		   Game::refreshClientScore(%clientId);
		}
      return;
   }

   %player = Client::getOwnedObject(%clientId);
   %clientId.observerMode = "";

   if(%team == -1)
   {
      UpdateTeam(%clientId);
      %team = Client::getTeam(%clientId);
   }
   GameBase::setTeam(%clientId, %team);
   %clientId.teamEnergy = 0;
	Client::clearItemShopping(%clientId);
	if(Client::getGuiMode(%clientId) != 1)
		Client::setGuiMode(%clientId,1);		
	Client::setControlObject(%clientId, -1);

   Game::playerSpawn(%clientId, false);
	%team = Client::getTeam(%clientId);
	if($TeamEnergy[%team] != "Infinite")
		$TeamEnergy[%team] += $InitialPlayerEnergy;
}
