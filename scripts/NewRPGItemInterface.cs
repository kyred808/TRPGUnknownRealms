
$RPGItem::ItemClassCount = 0;
$RPGItem::ItemCount = 0;

function RPGItem::ClearVariables()
{
    deleteVariables("RPGItem::ItemDef*");
    deleteVariables("RPGItem::ItemClass*");
    deleteVariables("RPGItem::ItemDefList*");
    $RPGItem::ItemCount = 0;
    $RPGItem::ItemClassCount = 0;
}

//Item ID vs ItemTag
//Item ID
//The item ID is a number assigned to the item.  I will be used to lookup info on the item.

//ItemTag
//The Item Tag is used to represent the item in inventory and storage. It is the item id prefixed with "id"
//So item of ID 25 will have the tag "id25"

//The tag is used so other data can be affixed to the item tag later via _.

function RPGItem::AddItemClass(%classLabel,%name,%invTag,%storageTag)
{
    $RPGItem::ItemClass[%classLabel,Name] = %name;
    $RPGItem::ItemClass[%classLabel,InventoryTag] = %invTag;
    $RPGItem::ItemClass[%classLabel,StorageTag] = %storageTag;
    $RPGItem::ItemClass[%classLabel,Index] = $RPGItem::ItemClassCount;
    $RPGItem::ItemClass[$RPGItem::ItemClassCount,Label] = %classLabel;
    
    $RPGItem::ItemClassCount++;
}

function RPGItem::AddItemDefinition(%itemLabel,%name,%class,%id,%datablk,%useAction)
{
    if($RPGItem::ItemDef[%id,Label] == "")
    {
        $RPGItem::ItemDefList[$RPGItem::ItemCount] = %id;
        $RPGItem::ItemCount++;
    }
    $RPGItem::ItemDef[%id,Name] = %name;
    $RPGItem::ItemDef[%id,BaseName] = %name;
    $RPGItem::ItemDef[%id,BaseItemTag] = "id"@%id;
    $RPGItem::ItemDef[%id,ClassName] = %class;
    $RPGItem::ItemDef[%id,Label] = %itemLabel;
    $RPGItem::ItemDef[%itemLabel,LabelToID] = %id;
    $RPGItem::ItemDef[%id,Datablock] = %datablk;
    $RPGItem::ItemDef[%id,Equippable] = false;
    
    if(%useAction != "")
    {
        $RPGItem::ItemDef[%id,Action] = %useAction;
    }
}

function RPGItem::AddWeapon(%label,%name,%id,%weaponType,%datablk)
{
    RPGItem::AddItemDefinition(%label,%name,$RPGItem::WeaponClass,%id,%datablk);
    $RPGItem::ItemDef[%id,WeaponType] = %weaponType;
    $RPGItem::ItemDef[%id,Equippable] = true;
}

function RPGItem::pairEquipWithItem(%itemId,%equipId)
{
    $RPGItem::ItemDef[%itemId,Alternate] = %equipId;
    $RPGItem::ItemDef[%equipId,Alternate] = %itemId;
    $RPGItem::ItemDef[%itemId,Equippable] = true;
    $RPGItem::ItemDef[%equipId,Equippable] = true;
}

function RPGItem::getDatablock(%itemId)
{
    return $RPGItem::ItemDef[%itemId,Datablock];
}

function RPGItem::getDatablockFromTag(%itemTag)
{
    return $RPGItem::ItemDef[RPGItem::getItemIDFromTag(%itemTag),Datablock];
}

function RPGItem::getInvStrGroup(%itemId)
{
    return $RPGItem::ItemClass[$RPGItem::ItemDef[%itemId,ClassName],InventoryTag];
}

function RPGItem::getStorageStrGroup(%itemId)
{
    return $RPGItem::ItemClass[$RPGItem::ItemDef[%itemId,ClassName],StorageTag];
}

function RPGItem::LabelToItemID(%itemLabel)
{
    %itemLabel = String::copyUntil(%itemLabel,"_");
    return $RPGItem::ItemDef[%itemLabel,LabelToID];
}

function RPGItem::LabelToItemName(%itemLabel)
{
    return $RPGItem::ItemDef[RPGItem::LabelToItemID(%itemLabel),Name];
}

function RPGItem::GetItemGroupFromTag(%itemTag)
{
    return RPGItem::getItemGroup(RPGItem::getItemIDFromTag(%itemTag));
}

function RPGItem::GetBaseTag(%itemTag)
{
    return $RPGItem::ItemDef[RPGItem::getItemIDFromTag(%itemTag),BaseItemTag];
}

function RPGItem::LabelToItemTag(%itemLabel)
{
    %id = RPGItem::LabelToItemID(%itemLabel);
    return RPGItem::transferAffixesToItem(%itemLabel,%id);
}

function RPGItem::ItemTagToLabel(%itemTag)
{
    return $RPGItem::ItemDef[RPGItem::getItemIDFromTag(%itemTag),Label];
}

function RPGItem::getItemNameFromTag(%itemTag)
{
	%customName = RPGItem::getAffixValue(%itemTag,"nn");
	if(!Math::IsInteger(%customName))
		%ret = %customName;
	else
		%ret = RPGItem::getItemName(RPGItem::getItemIDFromTag(%itemTag));
	
    %prefix = RPGItem::getAffixValue(%itemTag,"pr");
    if(String::ICompare(%prefix,0) != 0)
        %ret = %prefix @" "@%ret;
    
    %im = RPGItem::getAffixValue(%itemTag,"im");
    if(%im != 0)
    {
        if(RPGItem::getItemGroupFromTag(%itemTag) == "Gems")
        {
            %ret = $GemAffix[%im] @" "@ %ret;
        }
        else
        {
            if( %im > 0)
                %ret = "+"@%im@" "@%ret;
            else if(%im < 0)
                %ret = %im@" "@%ret;
        }
    }
    
    //if(RPGItem::getItemGroupFromTag(%itemTag) == "Gems")
    //{
    //    %gidx = String::findSubStr(%itemTag,"_gm");
    //    %str = String::getSubStr(%itemTag,%gidx+2,9999);
    //    %str = String::copyUntil(%str,"_");
    //    //echo(%str);
    //    %prefix = $GemAffix[%str];
    //    if(%prefix != "")
    //        %ret = %prefix @" "@ %ret;
    //}
    return %ret;
}

function RPGItem::getItemName(%itemId)
{
    return $RPGItem::ItemDef[%itemId,Name];
}

function RPGItem::getItemGroup(%itemId)
{
    return $RPGItem::ItemDef[%itemId,ClassName];
}

function RPGItem::getItemGroupFromTag(%itemTag)
{
    return RPGItem::getItemGroup(RPGItem::getItemIDFromTag(%itemTag));
}

function RPGItem::isValidItem(%itemId)
{
    return $RPGItem::ItemDef[%itemId,BaseName] != "";
}

function RPGItem::getAlternateTag(%itemTag)
{
    %id = RPGItem::getItemIDFromTag(%itemTag);
    if($RPGItem::ItemDef[%id,Alternate] != "")
    {
        %newId = $RPGItem::ItemDef[%id,Alternate];
        return RPGItem::transferAffixesToItem(%itemTag,%newId);
    }
    else
        return %itemTag;
}

function RPGItem::SendItemDataToClient(%clientId,%itemTag,%amt,%storageFlag)
{
    if(%storageFlag == "")
        %storageFlag = false;
    if(!Player::isAIControlled(%clientId))
    {
        if(%storageFlag)
            remoteEval(%clientId,"SetBuyList",%itemTag,RPGItem::getItemNameFromTag(%itemTag),%amt,RPGItem::getItemGroupFromTag(%itemTag));
        else
            remoteEval(%clientId,"SetItemCount",%itemTag,RPGItem::getItemNameFromTag(%itemTag),%amt,RPGItem::getItemGroupFromTag(%itemTag));
    }
}

function RPGItem::isItemTag(%input)
{
    return String::getSubStr(%input,0,2) == "id" && Math::isInteger(String::copyUntil(String::getSubStr(%input,2,9999),"_"));
}

function RPGItem::transferAffixesToItem(%itemTag,%newItemId)
{
    %affixesIdx = String::findSubStr(%itemTag,"_");
    %ret = "id"@%newItemId;
    if(%affixesIdx == -1)
        return %ret;
    else
    {
        %affixes = String::getSubStr(%itemTag,%affixesIdx,9999); //If an item name is >250 we got a problem
        %ret = %ret @ %affixes;
        return %ret;
    }
}

//=========================
// Inventory Functions
//=========================
// All these functions use item tags
function RPGItem::getItemIDFromTag(%itemTag)
{
    %str = String::CopyUntil(%itemTag,"_");
    return String::getSubStr(%str,2,String::len(%str));
}

function RPGItem::getItemCount(%clientId,%itemTag,%exact)
{
    if(%exact == "")
        %exact = true;
    //echo("RPGItem::getItemCount("@%clientId@","@%itemTag@","@%exact@")");
    %itemId = RPGItem::getItemIDFromTag(%itemTag); 
    %classInvTag = RPGItem::getInvStrGroup(%itemId);
    %invStr = fetchData(%clientId,%classInvTag);
    //return GetStuffStringCount(%invStr,%itemTag); 
    return RPGItem::GetStuffStringCount(%invStr,%itemTag,%exact);
}

function RPGItem::getStorageItemCount(%clientId,%itemTag,%exact)
{
    if(%exact == "")
        %exact = true;
    //echo("RPGItem::getItemCount("@%clientId@","@%itemTag@","@%exact@")");
    %itemId = RPGItem::getItemIDFromTag(%itemTag); 
    %classInvTag = RPGItem::getStorageStrGroup(%itemId);
    %invStr = fetchData(%clientId,%classInvTag);
    //return GetStuffStringCount(%invStr,%itemTag); 
    return RPGItem::GetStuffStringCount(%invStr,%itemTag,%exact);
}

function RPGItem::GetStuffStringCount(%string,%tag,%exactFlag)
{
    if(%exactFlag)
        return GetStuffStringCount(%string, %tag);
    else
    {
        %add = 0;
        for(%i = 0; (%itemTag = getWord(%string,%i)) != -1; %i+=2)
        {
            if(String::icompare(String::CopyUntil(%itemTag,"_"),%tag) == 0)
            {
                %add += getWord(%string,%i+1);
            }
        }
        return %add;
    }
}

function RPGItem::incItemCount(%clientId,%itemTag,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    if(%amt < 0)
        return RPGItem::decItemCount(%clientId,%itemTag,%amt,%showmsg);
    %ret = "";
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getInvStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt);
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        
        RPGItem::updateWeight(%clientId,%itemTag,%amt,"inc");
        
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret);
        
        if(%showmsg)
            Client::sendMessage(%clientId, 0, "You received " @ %amt @ " " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
    }
    else
        echo(%itemTag @" - Invalid Item");
    return %ret;
}

function RPGItem::incStorageItemCount(%clientId,%itemTag,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    if(%amt < 0)
        return RPGItem::decStorageItemCount(%clientId,%itemTag,%amt,%showmsg);
    %ret = "";
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getStorageStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt);
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret,true);
        
        if(%showmsg)
            Client::sendMessage(%clientId, 0, "You stored " @ %amt @ " " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
    }
    else
        echo(%itemTag @" - Invalid Item");
    return %ret;
}

function RPGItem::decItemCount(%clientId,%itemTag,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    if(%amt < 0)
        return RPGItem::incItemCount(%clientId,%itemTag,%amt,%showmsg);
    %ret = "";
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getInvStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt,"dec");
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        
        if(%ret == 0)
        {
            if($RPGItem::ItemDef[%itemId,ClassName] == $RPGItem::WeaponClass && %itemTag == fetchData(%clientId,"EquippedWeapon"))
            {
                //Player::unmountItem(%clientId,$WeaponSlot);
                RPGItem::UnequipItem(%clientId,%itemTag,false);
                storeData(%clientId,"EquippedWeapon","");
            }
        }
        
        RPGItem::updateWeight(%clientId,%itemTag,%amt,"dec");
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret);
        if(%showmsg)
            Client::sendMessage(%clientId, 0, "You lost " @ %amt @ " " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
    }
    else
        echo(%itemTag @" - Invalid Item");
        
    return %ret;
}

function RPGItem::decStorageItemCount(%clientId,%itemTag,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    if(%amt < 0)
        return RPGItem::incStorageItemCount(%clientId,%itemTag,%amt,%showmsg);
    
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getStorageStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt,"dec");
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret,true);
        
        if(%showmsg)
            Client::sendMessage(%clientId, 0, "You withdrew " @ %amt @ " " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
    }
    else
        echo(%itemTag @" - Invalid Item");
        
    return %ret;
}

function RPGItem::setItemCount(%clientId,%itemTag,%amt)
{
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getInvStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %currentAmnt = RPGItem::getItemCount(%clientId,%itemTag,true);
        %newStr = SetStuffString(%invStr,%itemTag,%amt,"set");
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        if(%ret == 0)
        {
            if($RPGItem::ItemDef[%itemId,ClassName] == $RPGItem::WeaponClass && %itemTag == fetchData(%clientId,"EquippedWeapon"))
            {
                //Player::unmountItem(%clientId,$WeaponSlot);
                RPGItem::UnequipItem(%clientId,%itemTag,false);
                storeData(%clientId,"EquippedWeapon","");
            }
        }
        
        if(%ret > %currentAmnt)
            RPGItem::updateWeight(%clientId,%itemTag,%ret - %currentAmnt,"inc");
        else if(%ret < %currentAmnt)
            RPGItem::updateWeight(%clientId,%itemTag,%currentAmnt - %ret,"dec");
        
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret);
    }
    else
        echo(%itemTag @" - Invalid Item");
    return %ret;
}

function RPGItem::setStorageItemCount(%clientId,%itemTag,%amt)
{
    %ret = "";
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getStorageStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt,"set");
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
        
        RPGItem::SendItemDataToClient(%clientId,%itemTag,%ret,true);
    }
    else
        echo(%itemTag @" - Invalid Item");
    return %ret;
}

function RPGItem::getFullItemList(%clientId,%storageFlag)
{
    if(%storageFlag == "")
        %storageFlag = false;
        
    %invStr = "";
    if(%storageFlag)
    {
        for(%i = 0; (%inv = $RPGItem::StorageItemLists[%i]) != ""; %i++)
        {
            %invStr = %invStr @ fetchData(%clientId,%inv);
        }
    }
    else
    {
        for(%i = 0; (%inv = $RPGItem::InvItemLists[%i]) != ""; %i++)
        {
            %invStr = %invStr @ fetchData(%clientId,%inv);
        }
    }
    
    return FixStuffString(%invStr);
}
//Enable the adding of weight modifiers
function RPGItem::getWeight(%itemTag)
{
    %weight = $AccessoryVar[getCroppedItem(RPGItem::ItemTagToLabel(%itemTag)), $Weight];
    if(%weight == "")
        return 0;
        
    return %weight;
}

function RPGItem::updateWeight(%clientId,%itemTag,%amnt,%dir)
{
    %weight = RPGItem::getWeight(%itemTag) * %amnt;
    if(%dir == "inc" || %dir == "dec")
    {
        storeData(%clientId,"totalWeight",%weight,%dir);
        storeData(%clientId,"refreshWeight",1,"inc");
    }
}

//Does not seem to work.  Do not use
function RPGItem::refreshPlayerInv(%clientId)
{
    %invList = RPGItem::getFullItemList(%clientId,false);
    echo("Full INV: "@%invList);
    %sendBuffer = "";
    for(%i = 0; (%itemTag = getWord(%invList,%i)) != -1; %i+=2)
    {
        %desc = RPGItem::getItemNameFromTag(%itemTag);
        %amt = RPGItem::getItemCount(%clientId,%itemTag,true);
        %type = RPGItem::getItemGroupFromTag(%itemTag);
        
        %len = String::len(%sendBuffer);
        %itemStr = %itemTag @"|"@ %desc @"|"@%amt@"|"@%type @",";
        %strLen = String::len(%itemStr);
        if(%len + %strLen > 200)
        {
            %ll = String::getSubStr(%sendBuffer,0,%len-1); //Drop the last comma
            remoteEval(%clientId,"BufferedPlayerInvList",%ll,false);
            %sendBuffer = "";
        }
        %sendBuffer = %sendBuffer @ %itemStr; //= %itemTag @"|"@ %desc @"|"@%amt@"|"@%type @",";
    }
    
    %ll = String::getSubStr(%sendBuffer,0,%len-1); //Drop the last comma
    remoteEval(%clientId,"BufferedPlayerInvList",%sendBuffer,true);
}

//Bank stuff

//=========================
// END Inventory Functions
//=========================
//We might want to add different use functions based on the item tag later
function RPGItem::getUseAction(%itemTag)
{
    %id = RPGItem::getItemIDFromTag(%itemTag);
    return $RPGItem::ItemDef[%id,Action];
}

function RPGItem::useItem(%clientId,%itemTag,%amnt)
{
    if(RPGItem::getItemCount(%clientId,%itemTag) > 0)
    {
        if(!IsDead(%clientId))
        {
            %action = RPGItem::getUseAction(%itemTag);
            
            %class = RPGItem::getItemGroupFromTag(%itemTag);
            //echo(%action);
            if(%class == $RPGItem::WeaponClass)
            {
                if(fetchData(%clientId,"EquippedWeapon") != %itemTag)
                {
                    RPGItem::EquipItem(%clientId,%itemTag);
                }
                else
                    RPGItem::UnequipItem(%clientId,%itemTag);
            }
            else if(%class == $RPGItem::AccessoryClass)
            {
                RPGItem::EquipItem(%clientId,%itemTag);
            }
            else if(%class == $RPGItem::EquippedClass)
            {
                RPGItem::UnequipItem(%clientId,%itemTag,true);
            }
            else if(%action != "")
            {
                RPGItem::DoUseAction(%clientId,%itemTag,%action);
            }
        }
    }
    else
    {
        Client::sendMessage(%clientId, $MsgRed, "You don't have any "@ RPGItem::getItemNameFromTag(%itemTag)@"!~wC_BuySell.wav");
        RPGItem::SendItemDataToClient(%clientId,%itemTag,0); //Clear out the item if there was one.
    }
}


function RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,%specialVar,%mod)
{
    for(%i = 0; (%s = getWord(%specialVar,%i)) != -1; %i+= 2)
    {
        //%stats = SetStuffString(%stats,%s,getWord(%specialVar,%i+1),"inc");
        storeData(%clientId,"EquipStat"@%s,getWord(%specialVar,%i+1),%mod);
    }
}

function RPGItem::GetPlayerEquipStats(%clientId,%statType)
{
    %amt = fetchData(%clientId,"EquipStat"@%statType);
    if(%amt == "")
        return 0;
        
    return %amt;
}

function RPGItem::ClearPlayerEquipStats(%clientId)
{
    deleteVariables("ClientData"@%clientId@"_EquipStat*");
    //for(%i = 1; $SpecialVarDesc[%i] != ""; %i++)
    //{
    //    //echo("CLEARING: "@$SpecialVarDesc[%i]);
    //    storeData(%clientId,"EquipStat"@%i,0);
    //}
    //
    //for(%i = 1; %i <= $NumberOfSkills; %i++)
    //{
    //    storeData(%clientId,"EquipStatSKILL"@%i,0);
    //}
}

//Allow for item tag values to add to stats later
function RPGItem::GetEquipmentStats(%itemTag,%itemLabel,%mult)
{
    if(%itemLabel == "")
        %itemLabel = RPGItem::ItemTagToLabel(%itemTag);
    
    if(%mult == "")
        %mult = 1;
    
    %nstats = "";
    %stats = GetAccessoryVar(%itemLabel, $SpecialVar);
    %hasAffixes = RPGItem::hasAffixes(%itemTag);
    //echo("Has Affix: "@ %hasAffixes);
    if(!%hasAffixes && %mult == 1)
    {
        return %stats;
    }
    
    for(%i = 0; (%s = getWord(%stats,%i)) != -1; %i+=2)
    {
        %bonus = 0;
        %affixType = $RPGItem::SpecialVarToAffix[%s];
        if(%hasAffixes && %affixType != "")
            %bonus = RPGItem::getAffixValue(%itemTag,%affixType);
            
        %skipSpec[%s] = true;
        //echo("Bonus: "@ %bonus);
        //echo(%stats);
        //echo("Mult: "@%mult);
        %nstats = %nstats @ %s @" "@ (getWord(%stats,%i+1)+%bonus)*%mult@" ";
    }
    
    //Add affixes for values we don't already have on the weapon.
    for(%i = 0; %i < $RPGItem::AffixCount; %i++)
    {
        %type = $RPGItem::AffixType[%i];
        %spec = $RPGItem::AffixSpecialVar[%i];
        if(%spec != "" && %skipSpec[%spec] == "")
        {
            %val = RPGItem::getAffixValue(%itemTag,%type);
            if(%val > 0)
                %nstats = %nstats @ %spec @ " "@ RPGItem::getAffixValue(%itemTag,%type)*%mult@" ";
        }
    }
    
    return %nstats;
    
    //if(%mult == 1)
    //{
    //    %baseStats = GetAccessoryVar(%itemLabel, $SpecialVar);
    //    return %baseStats;
    //}
    //else
    //{
    //    %nstats = 0;
    //    %stats = GetAccessoryVar(%itemLabel, $SpecialVar);
    //    for(%i = 0; (%s = getWord(%stats,%i)) != -1; %i+=2)
    //    {
    //        %nstats = %nstats @ %s @" "@ getWord(%stats,%i+1)*%mult@" ";
    //    }
    //    return %nstats;
    //}
} 

//Handles equiping items and applying stats to the character
function RPGItem::EquipItem(%clientId,%itemTag,%showmsg)
{
    if(%showmsg == "")
        %showmsg = true;

    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if($RPGItem::ItemDef[%itemId,Equippable])
    {
        %class = RPGItem::getItemGroup(%itemId);
        %label = $RPGItem::ItemDef[%itemId,Label];
        %refreshEquip = false;
        %oldHp = fetchData(%clientId,"HP");
        %oldMp = fetchData(%clientId,"MANA");
        if(SkillCanUse(%clientId, %label))
        {
            if(%class == $RPGItem::AccessoryClass)
            {
                %equipId = $RPGItem::ItemDef[%itemId,Alternate];
                %equipTag = "id"@%equipId;
                //echo("ItemTag vs EquipTag: "@ %itemTag @" "@ %equipTag);
                //%cnt = RPGItem::getItemCount(%clientId,%equipTag,false);
                %equipList = RPGItem::getItemList(%clientId,$RPGItem::ItemClass[$RPGItem::EquippedClass,InventoryTag]);
                for(%i = 0; (%eqItem = getWord(%equipList,%i)) != -1; %i+=2)
                {
                    %eqLabel = RPGItem::ItemTagToLabel(%eqItem);
                    if(GetAccessoryVar(%label, $AccessoryType) == GetAccessoryVar(%eqLabel, $AccessoryType))
                        %cnt += getWord(%equipList,%i+1);
                }
                
                
                if(%cnt < $maxAccessory[GetAccessoryVar(%label, $AccessoryType)])
                {
                    if(%showmsg)
                        Client::sendMessage(%clientId, $MsgBeige, "You equipped " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
                    %equipTag = RPGItem::transferAffixesToItem(%itemTag,%equipId);
                    RPGItem::decItemCount(%clientId,%itemTag,1,false);
                    RPGItem::incItemCount(%clientId,%equipTag,1,false);
                    RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%itemTag,%label),"inc");
                }
                else if(%showmsg)
                    Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you have too many already equipped.~wC_BuySell.wav");
            }
            else if(%class == $RPGItem::WeaponClass)
            {
                if(Player::getMountedItem(%clientId,$BaseWeaponSlot) != "BaseWeapon")
                {
                    Player::mountItem(%clientId,"BaseWeapon",$BaseWeaponSlot);
                }
                %curWeapon = fetchData(%clientId,"EquippedWeapon");
                if(%curWeapon != %itemTag)
                {
                    if(%curWeapon != "")
                    {
                        RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%curWeapon,"",1),"dec");
                        %refreshEquip = true;
                    }
                    echo(RPGItem::GetEquipmentStats(%itemTag,"",1));
                    RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%itemTag,"",1),"inc");
                    RPGmountItem(Client::getOwnedObject(%clientId), %itemTag, $WeaponSlot);
                }
            }
            if(%oldHP > 0)
                setHP(%clientId,%oldHP);
            setMana(%clientId,%oldMP);
            refreshHP(%clientId, 0);
            refreshMANA(%clientId, 0);
            RefreshAll(%clientId,%refreshEquip);
            
        }
        else if(%showmsg)
            Client::sendMessage(%clientId, $MsgRed, "You can't equip this item because you lack the necessary skills.~wC_BuySell.wav");
    }
}

function RPGItem::UnequipItem(%clientId,%itemTag,%showmsg,%refreshEquipOverride)
{
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if($RPGItem::ItemDef[%itemId,Equippable])
    {
        %class = RPGItem::getItemGroup(%itemId);
        %label = $RPGItem::ItemDef[%itemId,Label];
        %refresh = true;
        %oldHp = fetchData(%clientId,"HP");
        %oldMp = fetchData(%clientId,"MANA");
        if(%class == $RPGItem::EquippedClass)
        {
            %unequipId = $RPGItem::ItemDef[%itemId,Alternate];
            if(%showmsg)
                Client::sendMessage(%clientId, $MsgBeige, "You unequipped " @ RPGItem::getItemNameFromTag(%itemTag) @ ".");
            %unequipTag = RPGItem::transferAffixesToItem(%itemTag,%unequipId);
            RPGItem::decItemCount(%clientId,%itemTag,1,false);
            RPGItem::incItemCount(%clientId,%unequipTag,1,false);
            RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%itemTag,getCroppedItem(%label)),"dec");
        }
        else if(%class == $RPGItem::WeaponClass)
        {
            %curWeapon = fetchData(%clientId,"EquippedWeapon");
            if(%itemTag == %curWeapon)
            {
                RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%curWeapon,"",1),"dec");
                storeData(%clientId,"EquippedWeapon","");
                Player::unmountItem(Client::getOwnedObject(%clientId),$WeaponSlot);
            }
            else
            {
                Client::sendMessage(%clientId, $MsgRed, RPGItem::getItemNameFromTag(%itemTag) @ " is not currently equipped.");
                %refresh = false;
            }
        }
        
        if(%refresh)
        {
            if(%oldHP > 0)
                setHP(%clientId,%oldHP);
            setMana(%clientId,%oldMP);
            if(fetchData(%clientId,"HP") > 0) //If the player is already dead, this risks dinging LCK twice
                refreshHP(%clientId, 0);
            refreshMANA(%clientId, 0);
            
            //RefreshEquipment sets this to FALSE so it doesn't constantly call itself.  Otherwise refreshEquipOverride should always be blank
            if(%refreshEquipOverride != "")
                RefreshAll(%clientId,%refreshEquipOverride);
            else
                RefreshAll(%clientId,true);
        }
    }
}

function RPGItem::RefreshPlayerEquipStats(%clientId)
{
    %equipList = RPGItem::getItemList(%clientId,$RPGItem::ItemClass[$RPGItem::EquippedClass,InventoryTag]) @ fetchData(%clientId,"EquippedWeapon");
    RPGItem::ClearPlayerEquipStats(%clientId);
    for(%i = 0; (%itemTag = getWord(%equipList,%i)) != -1; %i+=2)
    {
        if(RPGItem::getItemGroupFromTag(%itemTag) == $RPGItem::WeaponClass)
            %amnt = 1;
        else
            %amnt = getWord(%equipList,%i+1);
        //%label = getCroppedItem(RPGItem::ItemTagToLabel(%itemTag)); //GetAccessoryVar handles the cropping
        %label = RPGItem::ItemTagToLabel(%itemTag);
        RPGItem::SetPlayerEquipStatsFromSpecialVar(%clientId,RPGItem::GetEquipmentStats(%itemTag,%label,%amnt),"inc");
    }
}

// WIP
function RPGItem::dropItem(%clientId,%itemTag,%amnt)
{
    if(%amnt == "ALL")
        %amnt = RPGItem::getItemCount(%clientId,%itemTag);
    if(%amnt < 1)
        %amnt = 1;
    
    else if(!Math::isInteger(%amnt))
        return;
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %class = RPGItem::getItemGroup(%itemId);
        
        %curAmnt = RPGItem::getItemCount(%clientId,%itemTag);
        if(%curAmnt >= %amnt)
        {
            echo("Item Class: "@ %class);
            if(%class == $RPGItem::EquippedClass)
            {
                Client::sendMessage(%clientId, $MsgRed, "You can't drop an equipped item!~wC_BuySell.wav");
                return;
            }
            else if(%class == $RPGItem::WeaponClass)
            {
                echo("New Amnt: "@ %curAmnt - %amnt);
                if(%curAmnt - %amnt == 0 && fetchData(%clientId,"EquippedWeapon") == %itemTag)
                {
                    echo("Dropping Equipped Weapon");
                    RPGItem::UnequipItem(%clientId,%itemTag,true);
                    //Player::unmountItem(%clientId,$WeaponSlot);
                }
            }
            else if ($LoreItem[%item])
            {
                Client::sendMessage(%clientId, $MsgRed, "You can't drop a lore item!~wC_BuySell.wav");
                return;
            }
            else
            {
                %clientId.bulkDrop = %amnt;
                //Toss item
            }
            
            %datablk = RPGItem::getDatablock(%itemId);
            %loot = newObject("", "Item", %datablk, 1, false);
            %loot.itemTag = %itemTag;
            %loot.delta = %amnt;
            
            playSound(SoundPickupItem,Gamebase::getPosition(%clientId));
            addToSet("MissionCleanup", %loot);
            GameBase::throw(%loot, Client::getOwnedObject(%clientId), 15, false);
            RPGItem::decItemCount(%clientId,%itemTag,%amnt,false);
            schedule("Item::Pop(" @ %loot @ ");", 120, %loot);
            RefreshAll(%clientId);
        }
        else
            Client::sendMessage(%clientId, $MsgRed, "You don't have "@ %amnt @" "@ RPGItem::getItemNameFromTag(%itemTag) @"! ("@%curAmnt@")~wC_BuySell.wav");
    }
}

function RPGItem::getItemList(%clientId,%listType)
{
    return fetchData(%clientId,%listType);
}


//Inventory String Format
//idNUM_special1_special2..._specialN number

// Might no longer need
function RPGItem::updateItemList(%clientId,%item)
{
    %invList = fetchData(%clientId,"InvItemList");
    %found = Word::FindWord(%invList,%item,",") != -1;
    if(Player::getItemCount(%clientId,%item) == 0)
    {
        if(%found)
        {
            %invList = String::replace(%invList,%item@",","");
            if(String::right(%invList,1) != ",")
                %invList = %invList @",";
            storeData(%clientId,"InvItemList",%invList);
        }
    }
    else
    {
        if(!%found)
        {
            %invList = %invList @ %item @",";
            storeData(%clientId,"InvItemList",%invList);
        }
    }
}