function CreateWeaponCyclingTables()
{
    %first = true;
    for(%i = 0; %i < $RPGItem::ItemCount; %i++)
    {
        %itemId = $RPGItem::ItemDefList[%i];
        if(RPGItem::getItemGroup(%itemId) == $RPGItem::WeaponClass)
        {
            if(%first)
                %firstitem = %itemId;
            else
            {
                $NextWeapon[%lastitem] = %itemId;
                $PrevWeapon[%itemId] = %lastitem;
            }
                
            %lastitem = %itemId;
        }
    }
    $NextWeapon[%lastitem] = %firstitem;
    $PrevWeapon[%firstitem] = %lastitem;
}
function OldCreateWeaponCyclingTables()
{
	dbecho($dbechoMode, "CreateWeaponCyclingTables()");
    // Only called once on server start, so this is fine
	%n = getNumItems();
	%counter = 0;
	for (%i = 0; %i < %n; %i++)
	{
		%item = getItemData(%i);
		if(%item.className == Weapon)
		{
			if(%counter > 0)
				$NextWeapon[%lastitem] = %item;
			else
				%firstitem = %item;

			%lastitem = %item;
			%counter++;
		}
	}
	$NextWeapon[%lastitem] = %firstitem;

	for(%i = Dagger; $NextWeapon[%i] != Dagger; %i = $NextWeapon[%i])
	      $PrevWeapon[$NextWeapon[%i]] = %i;
	$PrevWeapon[$NextWeapon[%i]] = %i;
}

function RPGmountItem(%player, %itemTag, %slot)
{
	dbecho($dbechoMode, "RPGmountItem(" @ %player @ ", " @ %itemTag @ ", " @ %slot @ ")");
    %clientId = Player::getClient(%player);
    
    if(IsDead(%clientId) || !fetchData(%clientId, "HasLoadedAndSpawned") || %clientId.IsInvalid)
		return 0;
    
	
    %label = RPGItem::ItemTagToLabel(%itemTag);
	if(SkillCanUse(%clientId, %label))
	{
        if(fetchData(%clientId,"attuningToWeapon"))
        {
            CancelAttunement(%clientId);
        }
        
        if(%slot == $WeaponSlot)
        {
            if(!Player::isAIControlled(%clientId))
            {
                %bottomText = "<jc><f0>Weapon: <f1>" @RPGItem::getItemNameFromTag(%itemTag);
                if(RPGItem::hasAffixes(%itemTag))
                {
                    %extra = GetAffixBonusText(%itemTag);
                    if(%extra != "")
                        %bottomText = %bottomText @" - "@ %extra;
                }
                %ammo = fetchData(%clientId, "LoadedProjectile " @ %itemTag);
                if(%ammo != "")
                {
                    %bottomText = %bottomText @"\n<f0>Ammo: <f1>"@RPGItem::getItemNameFromTag(%ammo);
                    if(RPGItem::hasAffixes(%ammo))
                    {
                        %extra = GetAffixBonusText(%ammo);
                        if(%extra != "")
                            %bottomText = %bottomText @" - "@ %extra;
                    }
                }
                if(fetchData(%clientId,"attunedWeapon") == %itemTag)
                {
                    %weapMana = fetchData(%clientId,"attunedWeaponMana");
                    %maxMana = $MageStaff[%itemTag,MaxMana];
                    %bottomText = %bottomText @"\n<f0>Mana: <f1>"@%weapMana @"<f0>/<f1>"@%maxMana;
                }
                %len = String::len(%bottomText);
                if(%len > 255)
                {
                    %substr = String::getsubstr(%bottomText,0,255);
                    remoteEval(%clientId,"BufferedCenterPrint",%substr, floor(String::len(%bottomText) / 20), 1);
                    %substr = String::getSubstr(%bottomText,255,%len);
                    remoteEval(%clientId,"BufferedCenterPrint",%substr, -1, 1);
                }
                else
                    bottomprint(%clientId,%bottomText,String::len(%bottomText)/20);
            }
            //echo("Store Weapon! " @%itemTag);
            storeData(%clientId,"EquippedWeapon",%itemTag);
        }
        
        %itemShape = RPGItem::getDatablockFromTag(%itemTag);
        //echo(%itemShape);
		Player::mountItem(%player, %itemShape, %slot);
		return True;
	}
	else
	{
		Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you lack the necessary skills.~wC_BuySell.wav");
		return False;
	}
}

function OldRPGmountItem(%player, %item, %slot)
{
	dbecho($dbechoMode, "RPGmountItem(" @ %player @ ", " @ %item @ ", " @ %slot @ ")");

	%clientId = Player::getClient(%player);
    
	if(SkillCanUse(%clientId, %item))
	{
        if(fetchData(%clientId,"attuningToWeapon"))
        {
            CancelAttunement(%clientId);
        }
        
        if(%slot == $WeaponSlot)
        {
            %bottomText = "<jc><f1>Weapon: <f0>" @RPGItem::getDesc(%item);
            %ammo = fetchData(%clientId, "LoadedProjectile " @ %item);
            if(%ammo != "")
                %bottomText = %bottomText @"\n<f1>Ammo: <f0>"@RPGItem::getDesc(%ammo);
            if(fetchData(%clientId,"attunedWeapon") == %item)
            {
                %weapMana = fetchData(%clientId,"attunedWeaponMana");
                %maxMana = $MageStaff[%item,MaxMana];
                %bottomText = %bottomText @"\n<f1>Mana: <f0>"@%weapMana @"<f1>/<f0>"@%maxMana;
            }
            bottomprint(%clientId,%bottomText,String::len(%bottomText)/20);
        }
		Player::mountItem(%player, %item, %slot);
		return True;
	}
	else
	{
		Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you lack the necessary skills.~wC_BuySell.wav");
		return False;
	}
}

function SelectNextWeapon(%clientId,%idx,%len,%dir)
{
    if(%dir == "")
        %dir = "inc";
        
    if(%dir == "inc")
    {
        %next = %idx + 2;
        if(%next >= %len)
            %next = 0;
    }
    else if(%dir == "dec")
    {
        %next = %idx - 2;
        if(%next < 0)
            %next = %len-2;
    }
    return %next;
}

function remoteNextWeapon(%clientId)
{
	dbecho($dbechoMode, "remoteNextWeapon(" @ %clientId @ ")");
    //echo(Player::getMountedItem(%clientId,$BaseWeaponSlot) );
    if(Player::getMountedItem(%clientId,$BaseWeaponSlot) == "ChargeMagicItem")
    {
        if(fetchData(%clientId, "SpellCastStep") == 1)
        {
            Client::sendMessage(%clientId,0,"You can't equip weapons while charging as spell!");
            return;
        }
    }
    //%mm = Player::getMountedItem(%player,$WeaponSlot);
    
    %itemList = RPGItem::getItemList(%clientId,$RPGItem::PlayerWeaponList);
    %itemTag = fetchData(%clientId,"EquippedWeapon");
    
    if(%itemTag == "" || !RPGItem::isItemTag(%itemTag))
        %startIdx = 0;
    else
        %startIdx = Word::FindWord(%itemList,%itemTag);
    %current = %startIdx;
    %len = GetWordCount(%itemList);
    
    if(%len % 2 != 0)
    {
        echo("ERROR: Weapon List for "@ %clientId @" is uneven!");
        return;
    }
    
    %next = SelectNextWeapon(%clientId,%current,%len,"inc");

    if(%len == 2)
    {
        %nextWeap = getWord(%itemList,%next);
        if(isSelectableWeapon(%clientId, %nextWeap))
        {
            RPGItem::EquipItem(%clientId,%nextWeap);
            break;
        }
        return;
    }
    
    while( %next != %startIdx)
    {
        
        %nextWeap = getWord(%itemList,%next);
        if(isSelectableWeapon(%clientId, %nextWeap))
        {
            RPGItem::EquipItem(%clientId,%nextWeap);
            break;
        }
        %next = SelectNextWeapon(%clientId,%next,%len,"inc");
    }
    
    if(%next == %startIdx && fetchData(%clientId,"EquippedWeapon") == "")
    {
        %nextWeap = getWord(%itemList,%next);
        if(isSelectableWeapon(%clientId, %nextWeap))
        {
            RPGItem::EquipItem(%clientId,%nextWeap);
        }
    }
    
    //%item = Player::getMountedItem(%clientId,$WeaponSlot);
    
	//if(%item == -1 || $NextWeapon[%item] == "")
	//	selectValidWeapon(%clientId);
	//else
	//{
	//	for(%weapon = $NextWeapon[%item]; %weapon != %item; %weapon = $NextWeapon[%weapon])
	//	{
	//		if(isSelectableWeapon(%clientId, %weapon))
	//		{
	//			Player::useItem(%clientId,%weapon);
	//			// Make sure it mounted (laser may not), or at least
	//			// next in line to be mounted.
	//			if (Player::getMountedItem(%clientId,$WeaponSlot) == %weapon || Player::getNextMountedItem(%clientId,$WeaponSlot) == %weapon)
	//				break;
	//		}
	//	}
	//}
}

//function Player::equipWeapon(%clientId,%itemTag)
//{
//    RPGMountItem(%clientId,%itemTag,$WeaponSlot);
//    refreshHP(%clientId, 0);
//    refreshMANA(%clientId, 0);
//    refreshStamina(%clientId, 0);
//    RefreshAll(%clientId,false);
//}

function remotePrevWeapon(%clientId)
{
	dbecho($dbechoMode, "remotePrevWeapon(" @ %clientId @ ")");
    if(Player::getMountedItem(%clientId,$BaseWeaponSlot) == "ChargeMagicItem")
    {
        if(fetchData(%clientId, "SpellCastStep") == 1)
        {
            Client::sendMessage(%clientId,0,"You can't equip weapons while charging as spell!");
            return;
        }
    }
    %itemList = RPGItem::getItemList(%clientId,$RPGItem::PlayerWeaponList);
    %itemTag = fetchData(%clientId,"EquippedWeapon");
    
    if(%itemTag == "" || !RPGItem::isItemTag(%itemTag))
        %startIdx = 0;
    else
        %startIdx = Word::FindWord(%itemList,%itemTag);
    %current = %startIdx;
    %len = GetWordCount(%itemList);
    
    if(%len % 2 != 0)
    {
        echo("ERROR: Weapon List for "@ %clientId @" is uneven!");
        return;
    }
    
    if(%len == 2)
    {
        %nextWeap = getWord(%itemList,%next);
        if(isSelectableWeapon(%clientId, %nextWeap))
        {
            RPGItem::EquipItem(%clientId,%nextWeap);
            break;
        }
        return;
    }
    
    %next = SelectNextWeapon(%clientId,%current,%len,"dec");
    
    while( %next != %startIdx)
    {
        %nextWeap = getWord(%itemList,%next);
        if(isSelectableWeapon(%clientId, %nextWeap))
        {
            RPGItem::EquipItem(%clientId,%nextWeap);
            break;
        }
        %next = SelectNextWeapon(%clientId,%next,%len,"dec");
    }
    
	//%item = Player::getMountedItem(%clientId,$WeaponSlot);
	//if (%item == -1 || $PrevWeapon[%item] == "")
	//	selectValidWeapon(%clientId);
	//else {
	//	for (%weapon = $PrevWeapon[%item]; %weapon != %item;
	//			%weapon = $PrevWeapon[%weapon]) {
	//		if (isSelectableWeapon(%clientId,%weapon)) {
	//			Player::useItem(%clientId,%weapon);
	//			// Make sure it mounted (laser may not), or at least
	//			// next in line to be mounted.
	//			if (Player::getMountedItem(%clientId,$WeaponSlot) == %weapon || Player::getNextMountedItem(%clientId,$WeaponSlot) == %weapon)
	//				break;
	//		}
	//	}
	//}
}

function selectValidWeapon(%clientId)
{
	dbecho($dbechoMode, "selectValidWeapon(" @ %clientId @ ")");
    if(Player::getMountedItem(%clientId,$BaseWeaponSlot) == "ChargeMagicItem")
    {
        if(fetchData(%clientId, "SpellCastStep") == 1)
        {
            Client::sendMessage(%clientId,0,"You can't equip weapons while charging as spell!");
            return;
        }
    }
    %itemList = RPGItem::getItemList(%clientId,$RPGItem::PlayerWeaponList);
    for(%i = 0; (%itemTag = getWord(%itemList,%i)) != -1; %i+=2)
    {
        if(isSelectableWeapon(%clientId, %itemTag))
        {
            RPGMountItem(%clientId,%itemTag,$WeaponSlot);
            refreshHP(%clientId, 0);
            refreshMANA(%clientId, 0);
            RefreshAll(%clientId,false);
            break;
        }
    }
    
    
	//%item = Dagger;	//any weapon, it doesn't matter
    //
	//if($NextWeapon[%item] == "")	//the $NextWeapon table has NOT yet been created.
	//	return;
    //
	//%weapon = %item;
	//while(%item == Dagger)
	//{
	//	if(isSelectableWeapon(%clientId, %weapon))
	//	{
	//		RPGMountItem(%clientId,%nextWeap,$WeaponSlot);
    //        refreshHP(%clientId, 0);
    //        refreshMANA(%clientId, 0);
    //        refreshStamina(%clientId, 0);
    //        RefreshAll(%clientId,false);
	//		break;
	//	}
    //
	//	%weapon = $NextWeapon[%weapon];
    //
	//	if(%weapon == %item)
	//		break;
	//}
}

//Now assumes you have the weapon in inv already
function isSelectableWeapon(%clientId,%weapon)
{
	dbecho($dbechoMode, "isSelectableWeapon(" @ %clientId @ ", " @ %weapon @ ")");

	if(IsDead(%clientId) || !fetchData(%clientId, "HasLoadedAndSpawned") || %clientId.IsInvalid)
		return 0;
    
    
	if(!SkillCanUse(%clientId, RPGItem::ItemTagToLabel(%weapon)))
		return false;

	//if(RPGItem::getItemCount(%clientId, %weapon))
	//{
		//%ammo = $WeaponAmmo[%weapon];
		//if (%ammo == "" || Player::getItemCount(%clientId,%ammo) > 0)
	return true;
	//}
	//return false;
}

ItemData Weapon
{
	description = "Weapon";
	showInventory = false;
};

function Weapon::onUse(%player,%item)
{
	dbecho($dbechoMode, "Weapon::onUse(" @ %player @ ", " @ %item @ ")");

	%clientId = Player::getClient(%player);

	if(IsDead(%clientId) || !fetchData(%clientId, "HasLoadedAndSpawned") || %clientId.IsInvalid)
		return 0;

	%ammo = %item.imageType.ammoType;
	if (%ammo == "") {
		// Energy weapons dont have ammo types
		RPGmountItem(%player,%item,$WeaponSlot);
	}
	else {
		if (Player::getItemCount(%player,%ammo) > 0) 
			RPGmountItem(%player,%item,$WeaponSlot);
		else {
			Client::sendMessage(Player::getClient(%player),0,
			strcat(%item.description," has no ammo"));
		}
	}
}

function Weapon::onFire(%player, %slot)
{
}

//Needs rework
function GetBestRangedProj(%clientId, %item)
{
	dbecho($dbechoMode, "GetBestRangedProj(" @ %clientId @ ", " @ %item @ ")");

	//this function returns the best projectile for a %clientId's ranged weapon

	//This function was written for BOTS.  It WILL work for players, but it will defeat the purpose of manually
	//equipping projectiles for each ranged weapon.

	%list = GetAccessoryList(%clientId, 10, -1);

	%highest = -1;
	%bestProj = -1;
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%proj = GetWord(%list, %i);
        %label = RPGItem::ItemTagToLabel(%proj);
		if(String::findSubStr($ProjRestrictions[%label], "," @ RPGItem::ItemTagToLabel(%item) @ ",") != -1 && RPGItem::getItemCount(%clientId,%item) > 0) // && belt::hasthisstuff(%clientId, %proj) > 0)
		{
            
			%v = AddItemSpecificPoints(%label, 6);
			if(%v > %highest)
			{
				%bestProj = %proj;
				%highest = %v;
			}
		}
	}
	return %bestProj;
}

function GetBestWeapon(%clientId)
{
    dbecho($dbechoMode, "GetBestWeapon(" @ %clientId @ ")");

	%highest = -1;
	%bestWeapon = -1;
    %itemList = RPGItem::getItemList(%clientId,$RPGItem::PlayerWeaponList);
    
    for(%i = 0; (%itemTag = getWord(%itemList,%i)) != -1; %i += 2)
    {
        if(isSelectableWeapon(%clientId, %itemTag))
		{
            %label = RPGItem::ItemTagToLabel(%itemTag);
            %x = "";
			%add = 0;
			if(GetAccessoryVar(%label, $AccessoryType) == $RangedAccessoryType)
			{
				%x = GetBestRangedProj(%clientId, %itemTag);
                %x = RPGItem::ItemTagToLabel(%x);
				if(%x != -1)
					%add += AddItemSpecificPoints(%x, 6);
				else
					%add = -99999;
			}
            if(%label == CastingBlade)
				%add += 200;

            if(%label == Treeatk)
                %add += 9999;
                
            if(%x != -1)
			{
				%atk = AddItemSpecificPoints(%itemTag, 6) + %add;
				%delay = GetDelay(%label);

				%v = %atk / %delay;

				if(%v > %highest)
				{
					%bestWeapon = %itemTag;
					%highest = %v;
				}
			}
        }
    }
    
    return %bestWeapon;
}

//Likely needs rework
function OldGetBestWeapon(%clientId)
{
	dbecho($dbechoMode, "GetBestWeapon(" @ %clientId @ ")");

	%highest = -1;
	%bestWeapon = -1;
 
	%item = Knife;
	for(%weapon = $NextWeapon[%item]; %weapon != %item; %weapon = $NextWeapon[%weapon])
	{
		if(isSelectableWeapon(%clientId, %weapon))
		{
			%x = "";
			%add = 0;
			if(GetAccessoryVar(%weapon, $AccessoryType) == $RangedAccessoryType)
			{
				%x = GetBestRangedProj(%clientId, %weapon);
				if(%x != -1)
					%add += AddItemSpecificPoints(%x, 6);
				else
					%add = -99999;
			}

			if(%weapon == CastingBlade)
				%add += 200;

            if(%weapon == Treeatk)
                %add += 9999;
                
			if(%x != -1)
			{
				%atk = AddItemSpecificPoints(%weapon, 6) + %add;
				%delay = GetDelay(%weapon);

				%v = %atk / %delay;

				if(%v > %highest)
				{
					%bestWeapon = %weapon;
					%highest = %v;
				}
			}
		}
	}

	return %bestWeapon;
}

function CalcWeaponSpeed(%itemTag,%label)
{
    %x = GetDelay(%label);
    %p = RPGItem::getAffixValue(%itemTag,"sp")/100;
    if(%p > 0)
        %mod = %x/(1+%p);
    else
        %mod = %x;
    return %mod;
}

function BaseWeaponImage::onFire(%player,%slot)
{
    %clientId = Player::getClient(%player);
    //$WeaponSlot is now 1, $BaseWeaponSlot is 0
    %mm = Player::getMountedItem(%player,$WeaponSlot);
    if(%mm == -1)
    {
        //Catch all.  Could use for a punching action later
        storeData(%clientId,"EquippedWeapon","");
        return;
    }
    
    //Load player's equipped weapon's tag
    %weapon = fetchData(%clientId,"EquippedWeapon");
    %id = RPGItem::getItemIDFromTag(%weapon);
    %wtype = $RPGItem::ItemDef[%id,WeaponType];
    if(%wtype != "")
    {
        %clientId.isAtRestCounter = 0;
        if(%clientId.isAtRest)
        {
            //Stop HP Regen
            %clientId.isAtRest = 0;
            refreshHPREGEN(%clientId);
        }
        %label = $RPGItem::ItemDef[%id,Label];
        if(getSimTime() >= $lastAttackTime[%clientId] + CalcWeaponSpeed(%weapon,%label)) //GetDelay(%label))
        {
            $lastAttackTime[%clientId] = getSimTime();
            
            //Swing Melee Weapon
            if(%wtype == $RPGItem::WeaponTypeMelee)
            {
                MeleeAttack(%player, GetRange(%label), %weapon);
            }
            //Fire Projectile Weapon
            else if(%wtype == $RPGItem::WeaponTypeRange)
            {
               
                //Need to fix to allow for modifying projectile speed
                %vel = $RangeWeaponFireVel[%label];
                ProjectileAttack(%clientId, %weapon, %vel);
            }
            //Swing pickaxe
            else if(%wtype == $RPGItem::WeaponTypePick)
            {
                PickAxeSwing(%player, GetRange(%label), %weapon);
            }
            //Default AI Casting
            else if(%wtype == $RPGItem::WeaponTypeBotSpell)
            {
                if(%clientId == "")
                    %clientId = 0;
                
                %index = GetBestSpell(%clientId, 1, True);
                echo("Spell Index: "@ %index @" - "@ $Spell::keyword[%index]);
                %length = $Spell::LOSrange[%index]-1;
                if(%length == "")
                    %length = 80;
                $los::object = "";
                if(GameBase::getLOSinfo(%player, %length) && %index != -1)
                {
                    %obj = getObjectType($los::object);
                    if(%obj == "Player")
                    {
                        if(Player::isAiControlled(%clientId))
                        {
                            AI::newDirectiveRemove(fetchData(%clientId, "BotInfoAiName"), 99);
                        }
                        remoteSay(%clientId, 0, "#cast " @ $Spell::keyword[%index]);
                        %hasCast = True;
                    }
                }
                if(!%hasCast)
                {
                    if(OddsAre(3))
                        MeleeAttack(%player, GetRange(Hatchet), CastingBlade);	//mimic the hatchet range
                }
                %hasCast = "";
            }
            
            Player::trigger(%player,$WeaponSlot,true);
            schedule("Player::trigger("@%player@","@$WeaponSlot@",false);",0.1,%player);
        }
    }
}

function ChargeMagicImage::onActivate(%player,%slot)
{
    if(!%player.lockActivate) //This will be set for 1 tenth of a second. Otherwise when you change armors due to the slowdown, the weapon reactivates and deactivates constantly.
    {
        echo("ChargeMagicImage::onActivate("@%player@","@%slot@")");
        %player.chargeStartTime = getSimTime();
        %clientId = Player::getClient(%player);
        %index = fetchData(%clientId,"EquippedSpell");
        %player.castBlock = false;
        //playSound($Spell::chargeSound[%i], GameBase::getPosition(%clientId));
        //%player.chargeStage = -1;
        
        %clientId.isAtRestCounter = 0;
        if(%clientId.isAtRest)
        {
            //Stop HP Regen
            %clientId.isAtRest = 0;
            refreshHPREGEN(%clientId);
        }

        %scs = fetchData(%clientId,"SpellCastStep");
        if(%scs == "")
        {
            if(Player::GetMana(%clientId) >= CalcSpellManaCost(%clientId,%index))
            {
                %ct = $Spell::chargeTime[%index];
                %clientId.spellChargeStack = 0;
                storeData(%clientId, "SpellCastStep", 1);
                storeData(%clientId, "SpellMovementFlag", true);
                %player.lockActivate = true;
                if($Spell::auraEffect[%index] != "")
                    Player::mountItem(%player,$Spell::auraEffect[%index],$SpellAuraSlot);
                RefreshWeight(%clientId);
                //schedule("RefreshWeight("@%clientId@");",1);
                //echo("TEST");
                remoteEval(%clientId,"rpgbarhud",%ct,1,0,"||",4,$Spell::name[%index]);
            }
            else
            {
                %player.castBlock = true;
                Client::sendMessage(%clientId, $MsgWhite, "Insufficient mana to cast this spell.");
            }
        }
        else if(%scs == 1)
        {
            %player.castBlock = true;
            Client::sendMessage(%clientId, $MsgWhite, "You are already casting a spell!");
            return;
        }
        else if(%scs == 2)
        {
            %player.castBlock = true;
            Client::sendMessage(%clientId, 0, "You are still recovering from your last spell cast.");
            return;
        }
    }
}

function ChargeMagicImage::onDeactivate(%player,%slot)
{
    if(!%player.lockActivate)
    {
        echo("ChargeMagicImage::onDeactivate("@%player@","@%slot@")");
        //%trans = Gamebase::getMuzzleTransform(%player);
        //%vel = Item::getVelocity(%player);
        %clientId = Player::getClient(%player);
        storeData(%clientId, "SpellMovementFlag", "");
        RefreshWeight(%clientId);
        %player.lockActivate = false;
        echo("Cast Blk: "@ %player.castBlock);
        if(!%player.castBlock)
        {
            %clientId.isAtRestCounter = 0;
            %index = fetchData(%clientId,"EquippedSpell");
            %timeDiff = getSimTime()-%player.chargeStartTime;
            %chargeType = $Spell::chargeType[%index];
            if(%chargeType == "")
                %chargeType = $ChargeCastingTypeSingleShot;
            
            %stack = 1;
            if(%chargeType == $ChargeCastingTypeStack)
            {
                %stack = %clientId.spellChargeStack;
                %clientId.spellChargeStack = 0;
            }
            
            if($Spell::auraEffect[%index] != "")
                Player::unmountItem(%player,$SpellAuraSlot);
            
            Spell::CastChargedMagic(%clientId,%index,%timeDiff,%stack);
            
            if(%timeDiff < $Spell::chargeTime[%index])
            {
                if(%chargeType != $ChargeCastingTypeStack)
                    remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"CANCELLED");
                else
                {
                    if(%stack > 0)
                        remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,$Spell::name[%index] @" "@ %stack);
                    else
                        remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"CANCELLED");
                }
            }
            //storeData(%clientId, "SpellMovementFlag", "");
            //RefreshWeight(%clientId);
            %player.chargeStartTime = "";
        }
    }
}

$Charge::spacerLen = 20;

function ChargeMagicImage::onUpdateFire(%player,%slot)
{
    //echo("ChargeMagicImage::onUpdateFire("@%player@","@%slot@")");
    if(!%player.castBlock)
    {
        %clientId = Player::getClient(%player);
        %time = getSimTime();
        %timeDiff = %time - %player.chargeStartTime;
        %index = fetchData(%clientId,"EquippedSpell");
        if(%timeDiff > 0.1)
            %player.lockActivate = false;
        if(%timeDiff >= $Spell::chargeTime[%index])
        {
            %chargeType = $Spell::chargeType[%index];
            if(%chargeType == "")
                %chargeType = $ChargeCastingTypeSingleShot;
            if( %chargeType == $ChargeCastingTypeSingleShot )
            {
                if(%time >= $lastAttackTime[%clientId] + 3)
                {
                    $lastAttackTime[%clientId] = %time;
                    remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"You are ready to cast!");
                }
            }
            else if(%chargeType == $ChargeCastingTypeStack)
            {
                %mcost = CalcSpellManaCost(%clientId,%index);
                //echo("Mana: "@Player::getMana(%clientId) @" "@ %mcost);
                if( %clientId.spellChargeStack < $Spell::chargeStackLimit[%index] && Player::getMana(%clientId) >= %mcost)
                {
                    %clientId.spellChargeStack += 1;
                    
                    Player::UseMana(%clientId,%mcost);
                    
                    if(Player::getMana(%clientId) >= %mcost)
                    {
                        if(%clientId.spellChargeStack != $Spell::chargeStackLimit[%index])
                            %player.chargeStartTime = %time;
                        remoteEval(%clientId,"rpgbarhud",$Spell::chargeTime[%index],1,0,"||",4,$Spell::name[%index] @" "@ %clientId.spellChargeStack);
                    }
                    else
                        Client::sendMessage(%clientId,0,"Not enough mana to store another charge.");
                }
                else
                {
                    //if(%clientId.spellChargeStack == $Spell::chargeStackLimit[%index])
                    //    %clientId.spellChargeStack+=1;
                    if(%time >= $lastAttackTime[%clientId] + 3)
                    {
                        $lastAttackTime[%clientId] = %time;
                        remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,$Spell::name[%index] @" "@ %clientId.spellChargeStack);
                    }
                }
            }
            
        }
    }
}

function ChargeMagic::CreateBottomPrintMsg(%clientId,%spellIndex,%timeDiff)
{
    %chargeTime = $Spell::chargeTime[%spellIndex];
    %mm = floor(%timeDiff* $Charge::spacerLen/%chargeTime);
    %bmsg = "<jc>Charge: "@ floor(100*(%timeDiff/%chargeTime)) @"%\n[<f1>";
    %msg = String::rpad(%bmsg,String::len(%bmsg) +%mm,"=");
    %bb = ceil($Charge::spacerLen - %mm);
    %msg = String::rpad(%msg,String::len(%msg)+%bb," ");
    %msg = %msg @ "<f0>]";
    return %msg;
}