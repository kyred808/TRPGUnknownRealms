function Item::giveItem(%player, %item, %delta, %showmsg)
{
	dbecho($dbechoMode, "Item::giveItem(" @ %player @ ", " @ %item @ ", " @ %delta @ ", " @ %showmsg @ ")");
    if(%delta < 0)
        return Item::takeItem(%player, -1*%item, %delta, %showmsg);
        
	%clientId = Player::getClient(%player);

	//i used to restrict what you could pick up here, but that sucks, so i made
	//it so you can pick up anything, but you can't EQUIP anything. (see Item::onUse)

	//also, the only reason you'd be getting a giveItem of an Equipped type is
	//by giving the client an item and pre-equipping it.

	if(%showmsg)
		Client::sendMessage(%clientId, 0, "You received " @ %delta @ " " @ %item.description @ ".");

	%amt = Player::incItemCount(%clientId, %item, %delta);

	return %amt;
}

function Item::takeItem(%player, %item, %delta, %showmsg)
{
	dbecho($dbechoMode, "Item::takeItem(" @ %player @ ", " @ %item @ ", " @ %delta @ ", " @ %showmsg @ ")");
    if(%delta < 0)
        return Item::giveItem(%player, -1*%item, %delta, %showmsg);
        
	%clientId = Player::getClient(%player);

	//i used to restrict what you could pick up here, but that sucks, so i made
	//it so you can pick up anything, but you can't EQUIP anything. (see Item::onUse)

	//also, the only reason you'd be getting a giveItem of an Equipped type is
	//by giving the client an item and pre-equipping it.

	if(%showmsg)
		Client::sendMessage(%clientId, 0, "You lost " @ %delta @ " " @ %item.description @ ".");

	%amt = Player::decItemCount(%clientId, %item, %delta);

	return %amt;
}

function Item::onCollision(%this,%object)
{
	dbecho($dbechoMode, "Item::onCollision(" @ %this @ ", " @ %object @ ")");

	%clientId = Player::getClient(%object);
	%armor = Player::getArmor(%clientId);

	if(getObjectType(%object) == "Player" && !IsDead(%clientId))
    {
		%time = getIntegerTime(true) >> 5;
		if(%time - %clientId.lastItemPickupTime <= 0.1)
			return 0;

		%clientId.lastItemPickupTime = %time;

		%item = Item::getItemData(%this);
        
        //if(%item == "belt")
        //{
        //    %msg = "";
        //
        //    %ownerName = GetWord($loot[%this], 0);
        //    %namelist = GetWord($loot[%this], 1);
        //
        //    if($loot[%this] == "")
        //        %msg = "You found an empty belt.";
        //    else
        //    {
        //        if(IsInCommaList(%namelist, Client::getName(%clientId)) || %namelist == "*")
        //        {
        //            if(String::ICompare(%ownerName, Client::getName(%clientId)) == 0)
        //                %msg = "You found one of your backpacks.";
        //            else if(%ownerName == "*")
        //                %msg = "You found a belt.";
        //            else
        //                %msg = "You found one of " @ %ownerName @ "'s belts.";
        //        }
        //    }
        //
        //    if(%msg != "")
        //    {
        //        %newloot = String::getSubStr($loot[%this], String::len(%ownerName)+String::len(%namelist)+2, 99999);
        //        %r = String::replace(%newloot, "belt", "");
        //        %newloot = String::NEWgetSubStr(%r, 1, 99999);
        //
        //        Client::sendMessage(%clientId, 0, %msg);
        //        GiveThisStuff(%clientId, %newloot, True);
        //
        //    if(%this.tag != "")
        //    {
        //        $tagToObjectId[%this.tag] = "";
        //        $SpawnPackList = RemoveFromCommaList($SpawnPackList, %this.tag);
        //    }
        //
        //    Item::playPickupSound(%this);
        //    $loot[%this] = "";
        //
        //    if(%ownerName != "*")
        //    {
        //        %ownerId = NEWgetClientByName(%ownerName);
        //        storeData(%ownerId, "lootbaglist", RemoveFromCommaList(fetchData(%ownerId, "lootbaglist"), %this));
        //    }
        //
        //    //event stuff
        //    %i = GetEventCommandIndex(%this, "onpickup");
        //    if(%i != -1)
        //    {
        //        %name = GetWord($EventCommand[%this, %i], 0);
        //        %type = GetWord($EventCommand[%this, %i], 1);
        //        %cl = NEWgetClientByName(%name);
        //        if(%cl == -1)
        //            %cl = 2048;
        //
        //        %cmd = String::NEWgetSubStr($EventCommand[%this, %i], String::findSubStr($EventCommand[%this, %i], ">")+1, 99999);
        //        %pcmd = ParseBlockData(%cmd, %clientId, "");
        //        $EventCommand[%this, %i] = "";
        //        remoteSay(%cl, 0, %pcmd, %name);
        //    }
        //
        //    deleteObject(%this);
        //    ClearEvents(%this);
		//	}
		//	else
		//	{
		//		if(%ownerName == "*")
		//			Client::sendMessage(%clientId, $MsgRed, "You do not have the right to take this belt.");
		//		else
		//			Client::sendMessage(%clientId, $MsgRed, "You do not have the right to take " @ %ownerName @ "'s belt.");
		//	}
		//}
        if(%item == "Lootbag")
        {
            %msg = "";

            %ownerName = GetWord($loot[%this], 0);
            %namelist = GetWord($loot[%this], 1);
            if($loot[%this] == "")
                %msg = "You found an empty backpack.";
            else
            {
                %skip = false;
                if(IsInCommaList(%namelist, Client::getName(%clientId)) || %namelist == "*")
                {
                    if(String::ICompare(%ownerName, Client::getName(%clientId)) == 0)
                        %msg = "You found one of your backpacks.";
                    else if(%ownerName == "*")
                        %msg = "You found a backpack.";
                    else
                        %msg = "You found one of " @ %ownerName @ "'s backpacks.";
                        
                    if(Player::isAIControlled(%clientId))
                    {
                        %ownerClient = NEWgetClientByName(%ownerName);
                        //Was this a player's pack?
                        if(!Player::isAIControlled(%ownerClient))
                        {
                            if($BotsCanPickupPlayerPacks)
                            {
                                if($MessagePlayerIfBotStolePack)
                                    Client::sendMessage(%ownerClient, $MsgRed, Client::getName(%clientId) @" just stole one of your packs!~wError_Message.wav");
                                $ZoneCleanupProtected[%clientId] = true;
                            }
                            else
                                %skip = true;
                        }
                    }
                }
            }

            if(%msg != "" && !%skip)
            {
                %newloot = String::getSubStr($loot[%this], String::len(%ownerName)+String::len(%namelist)+2, 99999);

                Client::sendMessage(%clientId, 0, %msg);

                GiveThisStuff(%clientId, %newloot, True);

                if(%this.tag != "")
                {
                    $tagToObjectId[%this.tag] = "";
                    $SpawnPackList = RemoveFromCommaList($SpawnPackList, %this.tag);
                }
                Item::playPickupSound(%this);
                $loot[%this] = "";

                if(%ownerName != "*")
                {
                    %ownerId = NEWgetClientByName(%ownerName);
                    storeData(%ownerId, "lootbaglist", RemoveFromCommaList(fetchData(%ownerId, "lootbaglist"), %this));
                }

                //event stuff
                %i = GetEventCommandIndex(%this, "onpickup");
                if(%i != -1)
                {
                    %name = GetWord($EventCommand[%this, %i], 0);
                    %type = GetWord($EventCommand[%this, %i], 1);
                    %cl = NEWgetClientByName(%name);
                    if(%cl == -1)
                        %cl = 2048;

                    %cmd = String::NEWgetSubStr($EventCommand[%this, %i], String::findSubStr($EventCommand[%this, %i], ">")+1, 99999);
                    %pcmd = ParseBlockData(%cmd, %clientId, "");
                    $EventCommand[%this, %i] = "";
                    remoteSay(%cl, 0, %pcmd, %name);
                }

                deleteObject(%this);
                ClearEvents(%this);
            }
            else
            {
                if(%ownerName == "*")
                    Client::sendMessage(%clientId, $MsgRed, "You do not have the right to take this backpack.");
                else
                    Client::sendMessage(%clientId, $MsgRed, "You do not have the right to take " @ %ownerName @ "'s backpack.");
            }
        }
        else if(%item.className == "Projectile")
        {
            %damagedClient = %clientId;
            %shooterClient = %this.owner;
            %item = %this.itemProj;
            if(%item == "")
                %item = %this.itemTag; //If the item was dropped instead of shot
            %label = RPGItem::ItemTagToLabel(%item);
            
            if(%shooterClient != "")
            {
                %vec = Vector::getDistance("0 0 0", Item::getVelocity(%this));
                if(%vec == 0 && $ProjectileDoubleCheck[%this])
                    %vec = 3.0;
            }
            else
                %vec = 0;	//don't let thrown projectiles damage!

            $ProjectileDoubleCheck[%this] = "";

            if(%vec >= 2.5)
            {
                GameBase::virtual(%object, "onDamage", $DamageType[%label], 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", %this.weapon, %shooterClient, %item);
            }
            else
            {
                RPGItem::incItemCount(%clientId,%item,%this.delta,true);
                //belt::givethisstuff(%clientId, %item, %this.delta,True);
                //if(Item::giveItem(%clientId, %item, %this.delta, True))
                //{
                    Item::playPickupSound(%this);
                    RefreshAll(%clientId,false);
                //}
            }

            deleteObject(%this);
		}
        else if(%item == "BeltLoot") //Needs to come before the item.className == accessory check
        {
            if(!Player::isAIControlled(%clientId))
            {
                %itemType = %this.itemType;
                //echo(%item @" "@ %itemType);
                if(%itemType != "")
                {
                    %amnt = %this.delta;
                    if(%amnt == "")
                        %amnt = 1;
                    if(%amnt < 0)
                        %amnt = 0;
                        
                    if(%amnt > 0)
                    {
                        RPGItem::incItemCount(%clientId,%itemType,%amnt,true);
                        Item::playPickupSound(%this);
                        RefreshAll(%clientId,false);
                    }
                    
                }
                deleteObject(%this);
            }
        }
        else if(%item.className == "Accessory" || $LoreItem[%item] == True)
        {
            if(RPGItem::isItemTag(%this.itemTag))
                %item = %this.itemTag;
            %amnt = %this.delta;
            if(%amnt == "")
                %amnt = 1;
            if(RPGItem::incItemCount(%clientId, %item, %amnt, True))
            {
                Item::playPickupSound(%this);
                RefreshAll(%clientId,false);
                deleteObject(%this);
            }
		}
        else if(%item == "MeteorBits" || %item == "MeteorBitsRed" || %item == "OreShape")
        {
            %itemType = %this.itemObj;
            if(%itemType != "")
            {
                RPGItem::incItemCount(%clientId,%itemType,1,true);
                Item::playPickupSound(%this);
                RefreshAll(%clientId,false);
                deleteObject(%this);
            }
            
        }
		else if(%item.className == "TownBot")
		{
			//do nothing.
		}
        else
        {
            //%count = Player::getItemCount(%object,%item);
            //if(Item::giveItem(%object, %item, %this.delta, True))
            //Bots seem to not pickup the full item?
            if(RPGItem::isItemTag(%this.itemTag))
                %item = %this.itemTag;
            
            if(RPGItem::incItemCount(%clientId, %item, %this.delta, True))
            {
                Item::playPickupSound(%this);
                RefreshAll(%clientId);
                Item::respawn(%this);
            }
		}
	}
}

function Item::onMount(%player,%item)
{
}

function Item::onUnmount(%player,%item)
{
}

// No longer used, or used rarely
function Item::onUse(%player,%item)
{
	dbecho($dbechoMode, "Item::onUse(" @ %player @ ", " @ %item @ ")");
    echo("Item::onUse(" @ %player @ ", " @ %item @ ")");
	%clientId = Player::getClient(%player);
    //echo("Item::onUse(" @ %player @ ", " @ %item @ ")");
	if(!IsDead(%clientId))
	{
		//this is how you toggle back and forth from equipped to carrying.
		if(%item.className == Accessory)
		{
			%cnt = 0;
            %itemList = fetchData(%clientId,"InvItemList");
            for(%i = 0; String::getWord(%itemList,",",%i) != ","; %i++)
            {
                %checkItem = String::getWord(%itemList,",",%i);
                if(String::icompare($RPGItem::InvItem[%checkItem,ClassName],"Equipped") == 0 && GetAccessoryVar(%checkItem, $AccessoryType) == GetAccessoryVar(%item, $AccessoryType))
                    %cnt += Player::getItemCount(%player, %checkItem);
            }
            //echo(%cnt);
			//%max = getNumItems();
			//for(%i = 0; %i < %max; %i++)
			//{
			//	%checkItem = getItemData(%i);
			//	if(%checkItem.className == Equipped && GetAccessoryVar(%checkItem, $AccessoryType) == GetAccessoryVar(%item, $AccessoryType))
			//		%cnt += Player::getItemCount(%player, %checkItem);
			//}
            
            

			if(SkillCanUse(%clientId, %item))
			{
				if(%cnt < $maxAccessory[GetAccessoryVar(%item, $AccessoryType)])
				{
					Client::sendMessage(%clientId, $MsgBeige, "You equipped " @ %item.description @ ".");
					RPGItem::setItemCount(%clientId, %item, Player::getItemCount(%player, %item)-1);
					RPGItem::setItemCount(%clientId, %item @ "0", Player::getItemCount(%player, %item @ "0")+1);
				}
				else
					Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you have too many already equipped.~wC_BuySell.wav");
			}
			else
				Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you lack the necessary skills.~wC_BuySell.wav");

			if($OverrideMountPoint[%item] == "")
				Player::mountItem(%player, %item @ "0", 1, 0);
		}
		else if(%item.className == Equipped)
		{
			%o = String::getSubStr(%item, 0, String::len(%item)-1);	//remove the 0
			Client::sendMessage(%clientId, $MsgBeige, "You unequipped " @ %item.description @ ".");
			RPGItem::setItemCount(%clientId, %item, Player::getItemCount(%player, %item)-1);
			RPGItem::setItemCount(%clientId, %o, Player::getItemCount(%player, %o)+1);

			if($OverrideMountPoint[%item] == "")
				Player::unMountItem(%player, 1);
		}
		else
		{
			RPGmountItem(%player, %item, $DefaultSlot);
		}

		refreshHP(%clientId, 0);
		refreshMANA(%clientId, 0);
		RefreshAll(%clientId,false);
	}
}

// No longer used (or used rarely)
function Item::onDrop(%player,%item)
{
	dbecho($dbechoMode, "Item::onDrop(" @ %player @ ", " @ %item @ ")");
	if($matchStarted)
	{
		if(%item.className != Armor)
		{
			if(%item.className == Projectile)
				%delta = 20;
			else
				%delta = 1;

			if(Player::getItemCount(%player, %item) < %delta)
				%delta = Player::getItemCount(%player, %item);

			if(%delta > 0)
			{
                %client = Player::getClient(%player);
                if(%client.bulkDrop != "")
                {
                    %delta = %client.bulkDrop;
                    %client.bulkDrop = "";
                }
				%obj = newObject("","Item",%item,1,false);
				%obj.delta = %delta;
	 	 	  	schedule("Item::Pop(" @ %obj @ ");", $ItemPopTime, %obj);
	 	 	 	addToSet("MissionCleanup", %obj);

				if(IsDead(%player)) 
					GameBase::throw(%obj, %player, 10, true);
				else {
					GameBase::throw(%obj, %player, 15, false);
					Item::playPickupSound(%obj);
				}
				RPGItem::decItemCount(%client,%item,%delta);
				RefreshAll(%client,false);

				return %obj;
			}
		}
	}
}

function Ammo::onDrop(%player,%item)
{
	dbecho($dbechoMode, "Ammo::onDrop(" @ %player @ ", " @ %item @ ")");

	if($matchStarted)
	{
		if(%item.className == Ammo)
			%delta = 20;
		else
			%delta = 1;

		if(Player::getItemCount(%player, %item) < %delta)
			%delta = Player::getItemCount(%player, %item);

		if(%delta > 0)
		{
			%obj = newObject("","Item",%item,%delta,false);
			%obj.delta = %delta;
	      	schedule("Item::Pop(" @ %obj @ ");", $ItemPopTime, %obj);

      		addToSet("MissionCleanup", %obj);
			GameBase::throw(%obj,%player,20,false);
			Item::playPickupSound(%obj);
			Player::decItemCount(%player,%item,%delta);

			RefreshAll(Player::getClient(%player),false);
		}
	}
}	

function Item::onDeploy(%player,%item,%pos)
{
}
