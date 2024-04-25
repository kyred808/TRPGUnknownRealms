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
                %bottomText = "<jc><f1>Weapon: <f0>" @RPGItem::getItemNameFromTag(%itemTag);
                %ammo = fetchData(%clientId, "LoadedProjectile " @ %itemTag);
                if(%ammo != "")
                    %bottomText = %bottomText @"\n<f1>Ammo: <f0>"@RPGItem::getItemNameFromTag(%ammo);
                if(fetchData(%clientId,"attunedWeapon") == %itemTag)
                {
                    %weapMana = fetchData(%clientId,"attunedWeaponMana");
                    %maxMana = $MageStaff[%itemTag,MaxMana];
                    %bottomText = %bottomText @"\n<f1>Mana: <f0>"@%weapMana @"<f1>/<f0>"@%maxMana;
                }
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

function BaseWeaponImage::onFire(%player,%slot)
{
    %clientId = Player::getClient(%player);
    %mm = Player::getMountedItem(%player,$WeaponSlot);
    if(%mm == -1)
    {
        storeData(%clientId,"EquippedWeapon","");
        return;
    }
    
    %weapon = fetchData(%clientId,"EquippedWeapon");
    %id = RPGItem::getItemIDFromTag(%weapon);
    %wtype = $RPGItem::ItemDef[%id,WeaponType];
    if(%wtype != "")
    {
        %clientId.isAtRestCounter = 0;
        if(%clientId.isAtRest)
        {
            %clientId.isAtRest = 0;
            refreshHPREGEN(%clientId);
        }
        %label = $RPGItem::ItemDef[%id,Label];
        if(getSimTime() >= $lastAttackTime[%clientId] + GetDelay(%label))
        {
            $lastAttackTime[%clientId] = getSimTime();
            if(%wtype == $RPGItem::WeaponTypeMelee)
            {
                MeleeAttack(%player, GetRange(%label), %weapon);
            }
            else if(%wtype == $RPGItem::WeaponTypeRange)
            {
               
                //Need to fix for other weapons
                %vel = $RangeWeaponFireVel[%label];
                ProjectileAttack(%clientId, %weapon, %vel);
            }
            else if(%wtype == $RPGItem::WeaponTypePick)
            {
                PickAxeSwing(%player, GetRange(%label), %weapon);
            }
            else if(%wtype == $RPGItem::WeaponTypeBotSpell)
            {
                if(%clientId == "")
                    %clientId = 0;
                
                %index = GetBestSpell(%clientId, 1, True);
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
    //New to adapt a new weapon type
    //else
    //{
    //    %spell = fetchData(%clientId,"EquippedSpell");
    //    if(%spell != "")
    //    {
    //        if(Player::isTriggered(%player,%slot))
    //        {
    //            if(getSimTime() >= $lastAttackTime[%clientId] + 0.2) //Spell update rate
    //            {
    //                %time = getSimTime();
    //                if(%player.startChargeTime == "")
    //                    %player.startChargeTime = %time;
    //                $lastAttackTime[%clientId] = %time;
    //                %timeDiff = %time - %player.startChargeTime;
    //                if(%timeDiff < $Spell::chargeTime[%spell])
    //                {
    //                    %msg = ChargeMagic::CreateBottomPrintMsg(%clientId,%spell,%timeDiff);
    //                }
    //                else
    //                {
    //                    %msg = "<jc>Charge:\n<f1>[====================]\n<f0>You are ready to cast!";
    //                }
    //                
    //                bottomprint(%clientId,%msg,0.4);
    //            }
    //        }
    //        else
    //        {
    //            %time = getSimTime();
    //            %timeDiff = %time - %player.startChargeTime;
    //            if(%timeDiff >= $Spell::chargeTime[%spell])
    //            {
    //            
    //            }
    //            %player.startChargeTime = "";
    //        }
    //    }
    //}
}

function ChargeMagicImage::onActivate(%player,%slot)
{
    //echo("ChargeMagicImage::onActivate("@%player@","@%slot@")");
    %player.chargeStartTime = getSimTime();
    %clientId = Player::getClient(%player);
    %i = fetchData(%clientId,"EquippedSpell");
    playSound($Spell::chargeSound[%i], GameBase::getPosition(%clientId));
    //%player.chargeStage = -1;
    %ct = $Spell::chargeTime[%i];
    remoteEval(%clientId,"rpgbarhud",%ct,1,0,"||",4,$Spell::name[%i]);
}

function ChargeMagicImage::onDeactivate(%player,%slot)
{
    //echo("ChargeMagicImage::onDeactivate("@%player@","@%slot@")");
    //%trans = Gamebase::getMuzzleTransform(%player);
    //%vel = Item::getVelocity(%player);
    %clientId = Player::getClient(%player);
    %spell = fetchData(%clientId,"EquippedSpell");
    %timeDiff = getSimTime()-%player.chargeStartTime;
    Spell::CastChargedMagic(%clientId,%spell,%timeDiff);
    
    if(%timeDiff < $Spell::chargeTime[%spell])
        remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"CANCELLED");
    //if(%player.chargeStage == 0)
    //{
    //    Projectile::spawnProjectile(Firebolt,%trans,%player,%vel);
    //    playSound(HitPawnDT,Gamebase::getPosition(%player));
    //}
    //else if(%player.chargeStage == 1)
    //{
    //    Projectile::spawnProjectile(Fireball,%trans,%player,%vel);
    //    playSound(ActivateAB,Gamebase::getPosition(%player));
    //}
    //else if(%player.chargeStage == 2)
    //{
    //    Projectile::spawnProjectile(Melt,%trans,%player,%vel);
    //    playSound(LaunchFB,Gamebase::getPosition(%player));
    //}
    
    //Player::unmountItem(%player,7);
    %player.chargeStartTime = "";
    //%player.chargeStage = "";
}

$Charge::spacerLen = 20;

function ChargeMagicImage::onUpdateFire(%player,%slot)
{
    //echo("ChargeMagicImage::onUpdateFire("@%player@","@%slot@")");
    
    %clientId = Player::getClient(%player);
    %time = getSimTime();
    %timeDiff = %time - %player.chargeStartTime;
    %spell = fetchData(%clientId,"EquippedSpell");
    
    if(%timeDiff >= $Spell::chargeTime[%spell])
    {
        if(%time >= $lastAttackTime[%clientId] + 3)
        {
            $lastAttackTime[%clientId] = %time;
            remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"You are ready to cast!");
        }
    }
    
    //if(%time >= $lastAttackTime[%clientId] + 0.2) //Spell update rate
    //{
    //    
    //    
    //    %chargeTime = $Spell::chargeTime[%spell];
    //    if(%timeDiff < %chargeTime)
    //    {
    //        //%msg = ChargeMagic::CreateBottomPrintMsg(%clientId,%spell,%timeDiff);
    //    }
    //    else
    //    {
    //        
    //        remoteEval(%clientId,"rpgbarhud",0,1,0,"||",4,"You are ready to cast!");
    //        //if(%player.flash)
    //        //    %msg = "<jc>Charge:\n<f1>[====================]\n<f0>You are ready to cast!";
    //        //else
    //        //    %msg = "<jc>Charge:\n<f1>[====================]\n<f1>You are ready to cast!";
    //        //%player.flash = !%player.flash;
    //    }
    //    
    //    bottomprint(%clientId,%msg,0.4);
    //}
    //%player.chargeLastUpdate = getSimTime();
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