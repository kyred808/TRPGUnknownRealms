function getBuyCost(%clientId, %item)
{
	dbecho($dbechoMode, "getBuyCost(" @ %clientId @ ", " @ %item @ ")");

	%aiName = fetchData(%clientId.currentShop, "BotInfoAiName");
    %label = RPGItem::ItemTagToLabel(%item);
	%p = GetItemCost(%label,%item);
	if($NewItemBuyCost[%aiName, %label] != "")
		%cost = $NewItemBuyCost[%aiName, %label];
	else
		%cost = %p;

	%p = round(CalculatePlayerSkill(%clientId, $SkillHaggling) / 20) / 100;
	%x = round(%cost * Cap(%p, 0.0, 0.5) );
	%cost -= %x;

	return %cost;
}
function getSellCost(%clientId, %item)
{
	dbecho($dbechoMode, "getSellCost(" @ %clientId @ ", " @ %item @ ")");

	%aiName = fetchData(%clientId.currentShop, "BotInfoAiName");
    %label = RPGItem::ItemTagToLabel(%item);
	%p = GetItemCost(%label,%item);
	if($NewItemSellCost[%aiName, %label] != "")
		%cost = $NewItemSellCost[%aiName, %label];
	else
		%cost = round(%p * ($resalePercentage/100));

	%p = round(CalculatePlayerSkill(%clientId, $SkillHaggling) / 11) / 100;
	%x = round(%cost * Cap(%p, 0.0, 1.0) );
	%cost += %x;

	return %cost;
}
function GetItemCost(%item,%itemTag)
{
	dbecho($dbechoMode, "GetItemCost(" @ %item @ ")");
	if($HardcodedItemCost[%item] != "")
    {
        %cost = $HardcodedItemCost[%item];
    }
    else
        %cost = $ItemCost[%item];
    
    %im = RPGItem::getAffixValue(%itemTag,"im");
    if(%im != 0)
    {
        %cost += %cost*%im*0.3;
    }
    
    %va = RPGItem::getAffixValue(%itemTag,"va");
    if(%va != 0)
    {
        %cost += %va;
    }
	return round(%cost);		
}

function BuySell(%player, %item, %delta, %buyORsell)
{
	dbecho($dbechoMode, "BuySell(" @ %player @ ", " @ %item @ ", " @ %delta @ ", " @ %buyORsell @ ")");

	// IF - Cost positive selling    IF - Cost Negative buying 

	%clientId = Player::getClient(%player);
	//%station = %player.Station;
	//%stationName = GameBase::getDataName(%station); 

	if(%clientId.adminLevel < 4)
	{
		//Add entry to merchant counter.
		//Admin purchases do not count towards the economy.
		%aiName = fetchData(%clientId.currentShop, "BotInfoAiName");
		if(%buyORsell == BUY)
		{
			$MerchantCounterB[%aiName, %item] += %delta;
			%cost = getBuyCost(%clientId, %item) * %delta * -1;
		}
		else if(%buyORsell == SELL)
		{
			$MerchantCounterS[%aiName, %item] += %delta;
			%cost = getSellCost(%clientId, %item) * %delta;
		}
		UseSkill(%clientId, $SkillHaggling, True, True);
		storeData(%clientId, "COINS", %cost, "inc");
	}

	%txt = "<f1><jc>COINS: " @ fetchData(%clientId, "COINS");
	Client::setInventoryText(%clientId, %txt);
}

function IsClientShopping(%clientId)
{
    return (Client::getGuiMode(%clientId) == $GuiModeInventory && 
    ( %clientId.currentBank != "" || 
      %clientId.currentShop != "" || 
      %clientId.currentInvSteal != "" ||
      %clientId.currentSmith != "" ||
      %clientId.currentAnvil != ""));
    
}

function buyItem(%clientId, %itemTag, %amnt)
{
    dbecho($dbechoMode, "buyItem(" @ %clientId @ ", " @ %itemTag @ ", "@ %amnt @")");
    
    if(IsDead(%clientId))
		return 0;
        
    if(%amnt <= 0 && %amnt != "ALL")
        return 0;
    %player = Client::getOwnedObject(%clientId);

    if(IsClientShopping(%clientId)) //Client::isItemShoppingOn(%clientId, %item))
	{
        if(%clientId.currentBank != "")
		{
            //============================================================
			//  Player is at a bank, removing from his/her bank storage
			//============================================================
			//if(%clientId.bulkNum != "")
			//	%n = %clientId.bulkNum;
			//else
			//	%n = 1;
            //%coinFlag = %itemTag == "COINS";
            %n = %amnt;
            //if(%coinFlag)
            //    %cnt = fetchData(%clientId,"BANK");
            //else
                %cnt = GetStuffStringCount(RPGItem::getFullItemList(%clientId,true), %itemTag);
            if(%amnt == "ALL")
                %n = %cnt;
			if(%cnt >= %n)
			{
                //if(%coinFlag)
                //{
                //    storeData(%clientId, "BANK", %n, "dec");
                //    storeData(%clientId, "COINS", %n, "inc");
                //    RefreshAll(%clientId,false);
                //    playSound(SoundMoney1, GameBase::getPosition(%clientId));
                //    remoteEval(%clientId,"SetItemCount","COINS","COINS",fetchData(%clientId,"COINS"),"Money");
                //    remoteEval(%clientId,"SetBuyList","COINS","COINS",fetchData(%clientId,"BANK"),"Money");
                //}
                //else
                //{
                    RPGItem::incItemCount(%clientId, %itemTag, %n);
                    //storeData(%clientId, "BankStorage", SetStuffString(fetchData(%clientId, "BankStorage"), %item, -%n));
                    RPGItem::decStorageItemCount(%clientId,%itemTag,%n);
                    //SetupBank(%clientId, %clientId.currentBank);	//refresh
                    //echo(%cnt @" "@ %n);
                    //remoteEval(%clientId,"SetBuyList",RPGItem::getItemID(%item), RPGItem::getDesc(%item), %cnt-%n, RPGItem::getItemGroup(%item));
                    RefreshAll(%clientId,false);
                //}
			}
			else
            {
                //if(%coinFlag)
                //    Client::sendMessage(%clientId, $MsgRed, "You do not have that many coins in the bank.~wC_BuySell.wav");
                //else
                    Client::sendMessage(%clientId, $MsgRed, "You only have " @ %cnt @ " of this item.~wC_BuySell.wav");
            }
            return 1;
        }
        else if(%clientId.currentShop != "")
		{
            %cost = getBuyCost(%clientId, %itemTag);
            echo(%clientId.bulkNum @" vs "@ %amnt); 
            if($LastClickItemB[%clientId, %itemTag] != %itemTag || %clientId.bulkNum != %amnt)
			{
                if(%amnt > 1)
                {
                    Client::sendMessage(%clientId, $MsgWhite, "The "@ RPGItem::getItemNameFromTag(%itemTag) @ " ("@ %amnt @") will cost you " @ %cost * %amnt @ " coins.");
                }
                else
                {
                    Client::sendMessage(%clientId, $MsgWhite, "The "@ RPGItem::getItemNameFromTag(%itemTag) @ " will cost you " @ %cost @ " coins.");
                }
				$LastClickItemB[%clientId, %itemTag] = %itemTag;
				schedule("$LastClickItemB[" @ %clientId @ ", " @ %itemTag @ "] = \"\";", 5);
				%clientId.bulkNum = %amnt;
				return 0;
			}
            else
			{
				if(checkResources(%player,%itemTag,%cost,%clientId.bulkNum) && !IsDead(%clientId))
				{
					RPGItem::incItemCount(%clientId, %itemTag, %clientId.bulkNum);
					BuySell(%player, %itemTag, %clientId.bulkNum, BUY);
                    Client::SendMessage(%clientId, $MsgWhite, "~wbuysellsound.wav");
					RefreshAll(%clientId,false);
					%clientId.bulkNum = 1;

					return 1;
				}
			}
        }
        else if(%clientId.currentInvSteal != "")
		{
			//=========================================
			//  Player is trying to pickpocket/mug
			//=========================================
			%cl = %clientId.currentInvSteal;
            %label = RPGItem::ItemTagToLabel(%itemTag);
			%itemweight = GetAccessoryVar(%label, $Weight);

			if(Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%cl)) > 2)
			{
				remotePlayMode(%clientId);
				Client::sendMessage(%clientId, $MsgWhite, "Your target has wandered off...");
			}
			else if(%clientId.TryingToSteal)
			{
				Client::sendMessage(%clientId, $MsgRed, "You are already trying to steal an item...");
			}
			else
			{
				if(%clientId.stealType == 1)
				{
					%max = 10;
					%w = "pickpocket";
					%canStealEquipped = False;
					%weightToTimeFactor = 0.6;
				}
				else if(%clientId.stealType == 2)
				{
					%max = 99999;
					%w = "mug";
					%canStealEquipped = True;
					%weightToTimeFactor = 0.55;
				}

				%eflag = "";
				%fitem = %itemTag;
                %itemClass = RPGItem::getItemGroupFromTag(%itemTag);
				if(%itemClass == "Equipped")
				{
					%fitem = String::getSubStr(%itemTag, 0, String::len(%itemTag)-1);
					if(%canStealEquipped)
						%eflag = True;
				}
				else
					%eflag = True;

				if(%eflag)
				{
					if(%itemweight <= %max)
					{
						if(!$StealProtectedItem[%label])
						{
							%icnt = RPGItem::getItemCount(%cl,%itemTag); //Player::getItemCount(%cl, %item);
							if(%icnt)
							{
								Client::sendMessage(%clientId, $MsgBeige, "Attempting to " @ %w @ "...");
	
								%a = %itemweight * %weightToTimeFactor;
								%b = (1000 - CalculatePlayerSkill(%clientId, $SkillStealing)) / 50;
								%c = Cap(%b, 0, "inf");
								%d = %c * %weightToTimeFactor;
	
								%time = %a + %d;
	
								%clientId.TryingToSteal = True;
								schedule("DoSteal(" @ %clientId @ ", " @ %cl @ ", " @ %icnt @ ", " @ %itemTag @ ", " @ %fitem @ ", \"" @ %w @ "\");", %time);
							}
						}
						else
							Client::sendMessage(%clientId, $MsgWhite, "This item is magically protected from your thieving hands...");
					}
					else
						Client::sendMessage(%clientId, $MsgWhite, "This item is too heavy to " @ %w @ "...");
				}
				else
					Client::sendMessage(%clientId, $MsgWhite, "You can't " @ %w @ " an equipped item...");
			}
		}
        else if(%clientId.currentSmith != "")
		{
			//=================================================
			//  Player is at a blacksmith unselecting an item
			//=================================================
			BlackSmithClick(%clientId, %item, -1);
		}
		else
		{
			storeData(%clientId, "TempPack", SetStuffString(fetchData(%clientId, "TempPack"), %item, -1));
			SetupCreatePack(%clientId);
		}
    }
    else
    {
        
    }
    return 0;
}

function OldbuyItem(%clientId, %item)
{
	dbecho($dbechoMode, "buyItem(" @ %clientId @ ", " @ %item @ ")");

	if(IsDead(%clientId))
		return;

	%player = Client::getOwnedObject(%clientId);
    
	if(Client::isItemShoppingOn(%clientId, %item))
	{
		if(%clientId.currentBank != "")
		{
			//============================================================
			//  Player is at a bank, removing from his/her bank storage
			//============================================================
			if(%clientId.bulkNum != "")
				%n = %clientId.bulkNum;
			else
				%n = 1;
			%cnt = GetStuffStringCount(fetchData(%clientId, "BankStorage"), %item);
			if(%cnt >= %n)
			{
				RPGItem::incItemCount(%clientId, %item, %n);
				storeData(%clientId, "BankStorage", SetStuffString(fetchData(%clientId, "BankStorage"), %item, -%n));
	
				SetupBank(%clientId, %clientId.currentBank);	//refresh

				RefreshAll(%clientId,false);
			}
			else
				Client::sendMessage(%clientId, $MsgRed, "You only have " @ %cnt @ " of this item.~wC_BuySell.wav");
		}
		else if(%clientId.currentShop != "")
		{
			//=========================================
			//  Player is at a regular shop
			//=========================================

			%cost = getBuyCost(%clientId, %item);
			if($LastClickItemB[%clientId, %item] != %item)
			{
				Client::sendMessage(%clientId, $MsgWhite, "The " @ %item.description @ " will cost you " @ %cost @ " coins.");

				$LastClickItemB[%clientId, %item] = %item;
				schedule("$LastClickItemB[" @ %clientId @ ", " @ %item @ "] = \"\";", 5);
				%clientId.bulkNum = 1;

				return 0;
			}
			else
			{
				if(checkResources(%player,%item,%cost,%clientId.bulkNum) && !IsDead(%clientId))
				{
					RPGItem::incItemCount(%clientId, %item, %clientId.bulkNum);
					BuySell(%player, %item, %clientId.bulkNum, BUY);
		
					RefreshAll(%clientId,false);
					%clientId.bulkNum = 1;

					return 1;
				}
			}
		}
		else if(%clientId.currentInvSteal != "")
		{
			//=========================================
			//  Player is trying to pickpocket/mug
			//=========================================
			%cl = %clientId.currentInvSteal;

			%itemweight = GetAccessoryVar(%item, $Weight);

			if(Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%cl)) > 2)
			{
				remotePlayMode(%clientId);
				Client::sendMessage(%clientId, $MsgWhite, "Your target has wandered off...");
			}
			else if(%clientId.TryingToSteal)
			{
				Client::sendMessage(%clientId, $MsgRed, "You are already trying to steal an item...");
			}
			else
			{
				if(%clientId.stealType == 1)
				{
					%max = 10;
					%w = "pickpocket";
					%canStealEquipped = False;
					%weightToTimeFactor = 0.6;
				}
				else if(%clientId.stealType == 2)
				{
					%max = 99999;
					%w = "mug";
					%canStealEquipped = True;
					%weightToTimeFactor = 0.55;
				}

				%eflag = "";
				%fitem = %item;
				if(%item.className == Equipped)
				{
					%fitem = String::getSubStr(%item, 0, String::len(%item)-1);
					if(%canStealEquipped)
						%eflag = True;
				}
				else
					%eflag = True;

				if(%eflag)
				{
					if(%itemweight <= %max)
					{
						if(!$StealProtectedItem[%item])
						{
							%icnt = Player::getItemCount(%cl, %item);
							if(%icnt)
							{
								Client::sendMessage(%clientId, $MsgBeige, "Attempting to " @ %w @ "...");
	
								%a = %itemweight * %weightToTimeFactor;
								%b = (1000 - CalculatePlayerSkill(%clientId, $SkillStealing)) / 50;
								%c = Cap(%b, 0, "inf");
								%d = %c * %weightToTimeFactor;
	
								%time = %a + %d;
	
								%clientId.TryingToSteal = True;
								schedule("DoSteal(" @ %clientId @ ", " @ %cl @ ", " @ %icnt @ ", " @ %item @ ", " @ %fitem @ ", \"" @ %w @ "\");", %time);
							}
						}
						else
							Client::sendMessage(%clientId, $MsgWhite, "This item is magically protected from your thieving hands...");
					}
					else
						Client::sendMessage(%clientId, $MsgWhite, "This item is too heavy to " @ %w @ "...");
				}
				else
					Client::sendMessage(%clientId, $MsgWhite, "You can't " @ %w @ " an equipped item...");
			}
		}
		else if(%clientId.currentSmith != "")
		{
			//=================================================
			//  Player is at a blacksmith unselecting an item
			//=================================================
			BlackSmithClick(%clientId, %item, -1);
		}
		else
		{
			storeData(%clientId, "TempPack", SetStuffString(fetchData(%clientId, "TempPack"), %item, -1));
			SetupCreatePack(%clientId);
		}
 	}

	return 0;
}
function DoSteal(%clientId, %cl, %itemcount, %item, %fitem, %w)
{
	dbecho($dbechoMode, "DoSteal(" @ %clientId @ ", " @ %cl @ ", " @ %itemcount @ ", " @ %item @ ", " @ %fitem @ ", " @ %w @ ")");

	%victimName = Client::getName(%cl);
	%stealerName = Client::getName(%clientId);

	%icnt = Player::getItemCount(%cl, %item);

	%clientId.TryingToSteal = "";

	//weights
	%itemweight = GetAccessoryVar(%item, $Weight);
	%wweight = 10;
	if(Player::getMountedItem(%clientId, $WeaponSlot) == %item || %item.className == Equipped)
		%handweight = 5.0;
	else
		%handweight = 1.0;
	%FailWeight = (%itemweight * %wweight) * %handweight;

	if(Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%cl)) > 2)
		Client::sendMessage(%clientId, $MsgWhite, "Your target has wandered off...");
	else
	{
		%r1 = GetRoll("1d" @ CalculatePlayerSkill(%clientId, $SkillStealing));
		%r2 = GetRoll("1d" @ (CalculatePlayerSkill(%cl, $SkillStealing))); //+ $PlayerSkill[%cl, $SkillDodging]));
		%b = %r1 - %r2;
		%a = %b - %FailWeight;
		if(%a > 0 && %itemcount == %icnt)
		{
			Client::sendMessage(%clientId, $MsgBeige, "You successfully stole a " @ %fitem.description @ " from " @ %victimName @ "!");
			RPGItem::decItemCount(%cl, %item, 1);
			RPGItem::incItemCount(%clientId, %fitem, 1);
			PerhapsPlayStealSound(%clientId, %clientId.stealType);
		
			SetupInvSteal(%clientId, %cl);
					
			RefreshAll(%clientId,false);
			RefreshAll(%cl,true);
				
			UseSkill(%clientId, $SkillStealing, True, True);
			PostSteal(%clientId, True, %clientId.stealType);

			return True;
		}
		else
		{
			Client::sendMessage(%clientId, $MsgRed, "You failed to " @ %w @ " " @ %victimName @ "!");
			Client::sendMessage(%cl, $MsgRed, %stealerName @ " just failed to " @ %w @ " you!");
	
			UseSkill(%clientId, $SkillStealing, False, True);
			PostSteal(%clientId, False, %clientId.stealType);
		}
	}

	//force kick from Inv Screen
	remotePlayMode(%clientId);

	return False;
}

function HasTheftFlag(%clientId)
{
	%b = AddBonusStatePoints(%clientId, "Theft");

	if(%b >= 1)
		return True;
	else
		return False;
}

function remoteBuyItem(%clientId,%type,%amnt)
{
	dbecho($dbechoMode, "remoteBuyItem(" @ %clientId @ ", " @ %type @ ")");
    if(%amnt == "")
        %amnt = 1;
        
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastWaitActionTime > $waitActionDelay)
	{
		%clientId.lastWaitActionTime = %time;

		//%item = RPGItem::ItemIDToLabel(%type);//getItemData(%type);
        
        echo("Buying Item: "@ %type @" "@ %amnt);
		buyItem(%clientId, %type, %amnt);
	}
}

function sellItem(%clientId,%itemTag, %amnt)
{
    dbecho($dbechoMode, "buyItem(" @ %clientId @ ", " @ %itemTag @ ", "@ %amnt @")");
    
    if(IsDead(%clientId))
		return;
    
    %player = Client::getOwnedObject(%clientId);
    
    if(IsClientShopping(%clientId))
    {
        if(%clientId.currentBank != "")
		{
			//============================================================
			//  Player is at a bank, adding to his/her bank storage
			//============================================================
            //if(%clientId.bulkNum != "")
            //	%n = %clientId.bulkNum;
            //else
            //	%n = 1;
            //%coinFlag = %itemTag == "COINS";
            %n = %amnt;
            //if(%coinFlag)
            //    %cnt = fetchData(%clientId,"COINS");
            //else
                %cnt = RPGItem::getItemCount(%clientId, %itemTag);
            if(%amnt == "ALL")
                %n = %cnt;
            if(%cnt >= %n)
            {
                //if(%coinFlag)
                //{
                //    storeData(%clientId, "BANK", %n, "inc");
                //    storeData(%clientId, "COINS", %n, "dec");
                //    RefreshAll(%clientId,false);
                //    playSound(SoundMoney1, GameBase::getPosition(%clientId));
                //    remoteEval(%clientId,"SetItemCount","COINS","COINS",fetchData(%clientId,"COINS"),"Money");
                //    remoteEval(%clientId,"SetBuyList","COINS","COINS",fetchData(%clientId,"BANK"),"Money");
                //}
                //else
                //{
                    %class = RPGItem::getItemGroupFromTag(%itemTag);
                    if(%class != $RPGItem::EquippedClass)
                    {
                        if(%itemTag == fetchData(%clientId,"EquippedWeapon"))
                        {
                            RPGItem::UnequipItem(%clientId,%itemTag,false);
                        }
                        RPGItem::decItemCount(%clientId, %itemTag, %n);
                        RPGItem::incStorageItemCount(%clientId,%itemTag,%n,true);
                        //storeData(%clientId, "BankStorage", SetStuffString(fetchData(%clientId, "BankStorage"), %item, %n));
                        //%newCnt = GetStuffStringCount(fetchData(%clientId, "BankStorage"), %item);
                        //SetupBank(%clientId, %clientId.currentBank);	//refresh
                        //remoteEval(%clientId,"SetBuyList",RPGItem::getItemID(%item), RPGItem::getDesc(%item), %newCnt, RPGItem::getItemGroup(%item));
                        RefreshAll(%clientId,false);
                    }
                    else
                        Client::sendMessage(%clientId, $MsgRed, "Unequip this item before storing it.~wC_BuySell.wav");
                //}                
            }
            else
            {
                //if(%coinFlag)
                //    Client::sendMessage(%clientId, $MsgRed, "You do not have that many coins.~wC_BuySell.wav");
                //else
                    Client::sendMessage(%clientId, $MsgRed, "You only have " @ %cnt @ " of this item.~wC_BuySell.wav");
            }

            return 1;
		}
        else if(%clientId.currentShop != "")
		{
            %label = RPGItem::ItemTagToLabel(%itemTag);
			%nitem = getCroppedItem(%label);
            if(%amnt == "ALL")
                %amnt = RPGItem::getItemCount(%clientId, %itemTag);
			//=========================================
			//  Player is at a regular shop
			//=========================================
			if($LastClickItemS[%clientId, %itemTag] != %itemTag || %clientId.bulkNum != %amnt)
			{
				%cost = getSellCost(%clientId, %itemTag);
                if(%amnt == 1)
                    Client::sendMessage(%clientId, $MsgWhite, "This merchant will give you " @ %cost @ " coins for the " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
                else
                    Client::sendMessage(%clientId, $MsgWhite, "This merchant will give you " @ %cost * %amnt @ " coins for "@ %amnt @" " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
				$LastClickItemS[%clientId, %itemTag] = %itemTag;
				schedule("$LastClickItemS[" @ %clientId @ ", " @ %itemTag @ "] = \"\";", 5);
				%clientId.bulkNum = %amnt;

				return 0;
			}
			else
			{
				%itemCnt = RPGItem::getItemCount(%clientId, %itemTag,true);
				if(RPGItem::getItemGroupFromTag(%itemTag) == $RPGItem::EquippedClass)
				{
					Client::sendMessage(%clientId, $MsgRed, "You cannot sell an equipped item.~wC_BuySell.wav");
				}
				else if(%itemCnt && !IsDead(%clientId))
				{
					if(%clientId.bulkNum > %itemCnt)
						%clientId.bulkNum = %itemCnt;

					if($LoreItem[%label])
						Client::sendMessage(%clientId, $MsgRed, "(You have sold a lore item)");
	
					//%count = RPGItem::getItemCount(%clientId, %item);
					%numsell = %clientId.bulkNum;
                    if(%itemTag == fetchData(%clientId,"EquippedWeapon"))
                    {
                        RPGItem::UnequipItem(%clientId,%itemTag,false);
                    }
					BuySell(%player, %itemTag, %clientId.bulkNum, SELL);
					RPGItem::decItemCount(%clientId,%itemTag,%numsell); //setItemCount(%player, %item, (%count-%numsell));
					Client::SendMessage(%clientId, $MsgWhite, "~wbuysellsound.wav");
	
					RefreshAll(%clientId,false);
					%clientId.bulkNum = 1;

					return 1;
				}
			}
		}
        else if(%clientId.currentSmith != "")
		{
			//=========================================
			//  Player is at a blacksmith
			//=========================================
			BlackSmithClick(%clientId, %item, 1);
		}
        else if(%clientId.currentAnvil != "")
        {
            %menuType = getWord(%clientId.currentAnvil,0);
            if(%menuType == "RefineEquip")
            {
                %itemCnt = RPGItem::getItemCount(%clientId,%itemTag,true);
                %itemType = RPGItem::getItemGroupFromTag(%itemTag);
                if(%itemType == $RPGItem::WeaponClass)
                {
                    if(%itemCnt > 0)
                    {
                        %curLevel = RPGItem::getImprovementLevel(%itemTag);
                        if(%curLevel < 5)
                        {
                            if(%curLevel <= 0)
                                %reqAmt = 5;
                            else
                            {
                                %reqAmt = 5*(%curLevel+1);
                            }
                            
                            %cost = round(GetItemCost(RPGItem::ItemTagToLabel(%itemTag),%itemTag)/4);
                            
                            if($LastClickItemS[%clientId, %itemTag] != %itemTag)
                            {
                                Client::SendMessage(%clientId,$MsgWhite,"Upgrading "@RPGItem::getItemNameFromTag(%itemTag)@" will cost "@%reqAmt@" Titanite Shards and "@%cost@" COINS");
                                $LastClickItemS[%clientId, %itemTag] = %itemTag;
                                schedule("$LastClickItemS[" @ %clientId @ ", " @ %itemTag @ "] = \"\";", 5);
                            }
                            else
                            {
                                %shardCnt = RPGItem::getItemCount(%clientId,RPGItem::LabelToItemTag("TitaniteShard"));
                                %money = fetchData(%clientId, "COINS");

                                if(%shardCnt >= %reqAmt && %money >= %cost)
                                {
                                    RPGItem::decItemCount(%clientId,RPGItem::LabelToItemTag("TitaniteShard"),%reqAmt,true);
                                    %newItem = RPGItem::setItemAffix(%itemTag,"im",1,"inc");
                                    for(%i = 0; %i < $RPGStats::AttributeCount; %i++)
                                    {
                                        %spec = Attribute::GetScalingSpecialVar($RPGStats::Attributes[%i]);
                                        if(GetStuffStringCount($AccessoryVar[RPGItem::ItemTagToLabel(%itemTag), $SpecialVar],%spec) > 0)
                                        {
                                            %newItem = RPGItem::setItemAffix(%newItem,$RPGItem::SpecialVarToAffix[%spec],$WeaponImprovementScaleFactor,"inc");
                                        }
                                    }
                                    %equipped = fetchData(%clientId,"EquippedWeapon") == %itemTag;
                                    RPGItem::decItemCount(%clientId,%itemTag,1);
                                    RPGItem::incItemCount(%clientId,%newItem,1);
                                    if(%equipped)
                                        RPGItem::EquipItem(%clientId,%newItem);
                                    Client::SendMessage(%clientId,$MsgWhite,"You upgraded "@ RPGItem::getItemNameFromTag(%itemTag) @" to "@ RPGItem::getItemNameFromTag(%newItem) @".~wbuysellsound.wav");
                                    playSound(SoundSmith, GameBase::getPosition(%clientId));
                                    
                                    storeData(%clientId, "COINS", %cost, "dec");
                                    %txt = "<f1><jc>COINS: " @ fetchData(%clientId, "COINS");
                                    Client::setInventoryText(%clientId, %txt);
                                }
                                else
                                {
                                    Client::SendMessage(%clientId,$MsgWhite,"You do not have the resources required to upgrade "@ RPGItem::getItemNameFromTag(%itemTag)@".~wC_BuySell.wav");
                                }
                            }
                        }
                    }
                    else
                    {
                        Client::sendMessage(%clientId, $MsgRed, "You do not have "@RPGItem::getItemNameFromTag(%itemTag)@".");
                        return 0;
                    }
                }
                else
                {
                    Client::sendMessage(%clientId, $MsgRed, "You cannot improve this item (Currently only weapon improvement is supported).");
                    return 0;
                }
            }
            else if(%menuType == "CraftEquip")
            {
                if(%clientId.selectedRecipeTag == "")
                {
                    
                }
            }
            return 0;
        }
    }
    else
	{
        
        //SetupItemOptions(%clientId,%itemTag);
        
        
        %msg = WhatIs(%itemTag);
        %len = String::len(%msg);
        if(%len > 255)
        {
            %substr = String::getsubstr(%msg,0,255);
            remoteEval(%clientId,"BufferedCenterPrint",%substr, floor(String::len(%msg) / 20), 1);
            %substr = String::getSubstr(%msg,255,%len);
            remoteEval(%clientId,"BufferedCenterPrint",%substr, -1, 1);
        }
        else 
            bottomprint(%clientId, %msg, floor(String::len(%msg) / 20));
        
        
        
        
        return 0;
		//if(RPGItem::getItemGroup(%item) != "Equipped" && !$LoreItem[%item])
		//{
		//	storeData(%clientId, "TempPack", SetStuffString(fetchData(%clientId, "TempPack"), %item, 1));
		//	SetupCreatePack(%clientId);
		//}
		//else
		//	Client::sendMessage(%clientId, $MsgRed, "You can't select this item.~wC_BuySell.wav");
	}
    return 0;
}

function OldsellItem(%clientId, %item)
{
	dbecho($dbechoMode, "sellItem(" @ %clientId @ ", " @ %item @ ")");

	if(IsDead(%clientId))
		return;

	%player = Client::getOwnedObject(%clientId);

	if(%clientId.currentShop != "" || %clientId.currentBank != "" || %clientId.currentSmith != "")
	{
		if(%clientId.currentBank != "")
		{
			//============================================================
			//  Player is at a bank, adding to his/her bank storage
			//============================================================
			if(CountObjInList(fetchData(%clientId, "BankStorage")) / 2 < 25)
			{
				if(%clientId.bulkNum != "")
					%n = %clientId.bulkNum;
				else
					%n = 1;
				%cnt = Player::getItemCount(%clientId, %item);
				if(%cnt >= %n)
				{
					if(%item.className != Equipped)
					{
						RPGItem::decItemCount(%clientId, %item, %n);
						storeData(%clientId, "BankStorage", SetStuffString(fetchData(%clientId, "BankStorage"), %item, %n));
		
						SetupBank(%clientId, %clientId.currentBank);	//refresh
		
						RefreshAll(%clientId,false);
					}
					else
						Client::sendMessage(%clientId, $MsgRed, "Unequip this item before storing it.~wC_BuySell.wav");
				}
				else
					Client::sendMessage(%clientId, $MsgRed, "You only have " @ %cnt @ " of this item.~wC_BuySell.wav");
	
				return 1;
			}
			else
				Client::sendMessage(%clientId, $MsgRed, "You can only store 25 different types of items.~wC_BuySell.wav");
		}
		else if(%clientId.currentShop != "")
		{
			%nitem = getCroppedItem(%item);

			//=========================================
			//  Player is at a regular shop
			//=========================================
			if($LastClickItemS[%clientId, %item] != %item)
			{
				%cost = getSellCost(%clientId, %item);
				Client::sendMessage(%clientId, $MsgWhite, "This merchant will give you " @ %cost @ " coins for the " @ %nitem.description @ ".");

				$LastClickItemS[%clientId, %nitem] = %nitem;
				schedule("$LastClickItemS[" @ %clientId @ ", " @ %nitem @ "] = \"\";", 5);
				%clientId.bulkNum = 1;

				return 0;
			}
			else
			{
				%itemCnt = Player::getItemCount(%clientId, %item);
				if(%item.className == Equipped)
				{
					Client::sendMessage(%clientId, $MsgRed, "You cannot sell an equipped item.~wC_BuySell.wav");
				}
				else if(%itemCnt && !IsDead(%clientId))
				{
					if(%clientId.bulkNum > %itemCnt)
						%clientId.bulkNum = %itemCnt;

					if($LoreItem[%item])
						Client::sendMessage(%clientId, $MsgRed, "(You have sold a lore item)");
	
					%count = Player::getItemCount(%clientId, %item);
					%numsell = %clientId.bulkNum;
	
					BuySell(%player, %item, %clientId.bulkNum, SELL);
					RPGItem::setItemCount(%player, %item, (%count-%numsell));
					Client::SendMessage(%clientId, $MsgWhite, "~wbuysellsound.wav");
	
					RefreshAll(%clientId,false);
					%clientId.bulkNum = 1;

					return 1;
				}
			}
		}
		else if(%clientId.currentSmith != "")
		{
			//=========================================
			//  Player is at a blacksmith
			//=========================================
			BlackSmithClick(%clientId, %item, 1);
		}
	}
	else
	{
		if(%item.className != Equipped && !$LoreItem[%item])
		{
			storeData(%clientId, "TempPack", SetStuffString(fetchData(%clientId, "TempPack"), %item, 1));
			SetupCreatePack(%clientId);
		}
		else
			Client::sendMessage(%clientId, $MsgRed, "You can't select this item.~wC_BuySell.wav");
	}

	return 0;
}
function remoteSellItem(%clientId, %type, %amnt)
{
	dbecho($dbechoMode, "remoteSellItem(" @ %clientId @ ", " @ %type @ ", "@ %amnt @")");

	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastWaitActionTime > $waitActionDelay)
	{
        if(%amnt <= 0 && %amnt != "ALL")
            return 0;
		%clientId.lastWaitActionTime = %time;
        //echo("Type: "@%type);
		//%item = RPGItem::ItemIDToLabel(%type);//getItemData(%type);
        //echo("Selling Item: "@ %item @" "@ %amnt);
		sellItem(%clientId, %type, %amnt);
	}
}


function checkResources(%player, %itemTag, %cost, %delta, %noMessage)
{
	dbecho($dbechoMode, "checkResources(" @ %player @ ", " @ %item @ ", " @ %cost @ ", " @ %delta @ ", " @ %noMessage @ ")");

	%clientId = Player::getClient(%player);

	if(%cost * %delta > fetchData(%clientId, "COINS") && %clientId.adminLevel < 4)
	{
		if(%noMessage == "")
			Client::sendMessage(%clientId, $MsgRed, "You cannot afford the " @ RPGItem::getItemNameFromTag(%itemTag) @ ".~wC_BuySell.wav");
		return 0;
	}
	return 1;
}

function CompleteSmith(%clientId, %cost, %sc, %tempsmith, %multiplier)
{
	dbecho($dbechoMode, "CompleteSmith(" @ %clientId @ ", " @ %cost @ ", " @ %sc @ ", " @ %tempsmith @ ", " @ %multiplier @ ")");

	%clientId.IsSmithing = "";

	if(fetchData(%clientId, "COINS") < %cost)
		return;

	storeData(%clientId, "COINS", %cost, "dec");
	playSound(SoundMoney1, GameBase::getPosition(%clientId));
	GiveThisStuff(%clientId, $SmithComboResult[%sc], True, %multiplier);

	for(%i = 0; (%w = GetWord(%tempsmith, %i)) != -1; %i+=2)
	{
		%w2 = GetWord(%tempsmith, %i+1) * %multiplier;
		storeData(%clientId, "BankStorage", SetStuffString(fetchData(%clientId, "BankStorage"), %w, -%w2));
	}

	AI::sayLater(%clientId, %clientId.currentSmith, "Here you go.", True);
}

function BlackSmithClick(%clientId, %item, %delta)
{
	dbecho($dbechoMode, "BlackSmithClick(" @ %clientId @ ", " @ %item @ ", " @ %delta @ ")");

	if(%clientId.IsSmithing)
	{
		Client::sendMessage(%clientId, $MsgRed, "The blacksmith is busy...");
	}
	else
	{
		if(%item.className != Equipped)
		{
			storeData(%clientId, "TempSmith", SetStuffString(fetchData(%clientId, "TempSmith"), %item, %delta));
			SetupBlacksmith(%clientId, %clientId.currentSmith);

			%tempsmith = LTrim(fetchData(%clientId, "TempSmith"));
			if((%sc = GetSmithCombo(%tempsmith)) != 0)
			{
				%cost = GetSmithComboCost(%clientId, %sc);

				Client::sendMessage(%clientId, $MsgWhite, "It will cost you " @ %cost @ " coins to smith these items.~wcanSmith.wav");
				Client::sendMessage(%clientId, $MsgBeige, "(type #smith to accept the cost and start smithing)");

				return 0;
			}
			else
			{
				%cnt = GetStuffStringCount(fetchData(%clientId, "TempSmith"), %item);
				Client::sendMessage(%clientId, $MsgRed, "Invalid combination. (" @ %cnt @ " " @ %item.description @ ")");
			}
		}
	}
}

function GetSmithCombo(%tempsmith)
{
	dbecho($dbechoMode, "GetSmithCombo(" @ %tempsmith @ ")");

	for(%i = 1; $SmithCombo[%i] != ""; %i++)
	{
		if(IsStuffStringEquiv(%tempsmith, $SmithCombo[%i], True))
			return %i;
	}
	return 0;
}

function GetSmithComboCost(%clientId, %sc)
{
	dbecho($dbechoMode, "GetSmithComboCost(" @ %clientId @ ", " @ %sc @ ")");

	%c1 = GetStuffStringCost(%clientId, $SmithCombo[%sc]);
	%c2 = GetStuffStringCost(%clientId, $SmithComboResult[%sc]);

	return Cap( round(%c2 - (%c1 * 1.35)), 1, "inf");
}

function GetStuffStringCost(%clientId, %itemlist)
{
	dbecho($dbechoMode, "GetStuffStringCost(" @ %clientId @ ", " @ %itemlist @ ")");

	%cost = 0;
	for(%i = 0; (%w = GetWord(%itemlist, %i)) != -1; %i++)
	{
		%w2 = GetWord(%itemlist, %i+1);
		%c = getBuyCost(%clientId, %w) * %w2;
		%cost += %c;
	}

	return %cost;
}