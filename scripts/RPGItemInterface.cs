$RPGItem::InvItemType = 0;
$RPGItem::BeltItemType = 1;

$RPGItem::ItemCount = 0;

function RPGItem::ClearVariables()
{
    deleteVariables("RPGItem::ItemDef*");
    $RPGItem::ItemCount = 0;
}

// Only run this function once after startup.
function RPGItem::BuildInventoryItemList()
{
    %max = getNumItems();
	for(%i = 0; %i < %max; %i++)
	{
        %item = getItemData(%i);
        RPGItem::AddItemDefinition(%item,%item.description,%item.className,%i,$RPGItem::InvItemType);
        //$RPGItem::InvItem[%item,Name] = %item.description;
        //$RPGItem::InvItem[%item,ClassName] = %item.className;
        //$RPGItem::InvItem[%item,Index] = %i;
        //$RPGItem::IndexToInvItem[%i] = %item;
        $RPGItem::ItemCount++;
    }
}

function RPGItem::BuildBeltItemList()
{
    for(%i = 0; %i < $Belt::NumberOfBeltGroups; %i++)
	{
        %type = $Belt::ItemGroup[%i];
        for(%k = 1; %k <= $Belt::ItemGroupItemCount[%type]; %k++)
        {
            %item = $beltitem[%k, "Num", %type];
            %itemType = $Belt::ItemGroupShortName[%i];
            RPGItem::AddItemDefinition(%item,$beltitem[%item, "Name"],%itemType,$RPGItem::ItemCount,$RPGItem::BeltItemType);
            $RPGItem::ItemCount++;
        }
    }
}

function RPGItem::getItemID(%itemLabel)
{
    return $RPGItem::ItemDef[%itemLabel,LabelToID];
}

function RPGItem::ItemIDToLabel(%id)
{
    return $RPGItem::ItemDef[%id,Label];
}

function RPGItem::AddItemDefinition(%itemLabel,%name,%class,%id,%itype)
{
    $RPGItem::ItemDef[%id,Name] = %name;
    if(%class == "weapon")
        %class = "Weapon";
    $RPGItem::ItemDef[%id,ClassName] = %class;
    $RPGItem::ItemDef[%id,InternalType] = %itype;
    $RPGItem::ItemDef[%id,Label] = %itemLabel;
    $RPGItem::ItemDef[%itemLabel,LabelToID] = %id;
}

function RPGItem::getItemGroup(%item)
{
    %type = RPGItem::getItemInternalType(%item);
    if(%type == $RPGItem::InvItemType)
    {
        %ret = $RPGItem::ItemDef[$RPGItem::ItemDef[%item,LabelToID],ClassName]; //$RPGItem::InvItem[%item,ClassName];
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        return $RPGItem::ItemDef[$RPGItem::ItemDef[%item,LabelToID],ClassName];//$Belt::ItemGroupShortName[$Belt::ItemGroupIndex[Belt::getItemType(%item)]];
    }
    return %ret;
}

function RPGItem::setItemCount(%clientId,%item,%amt)
{
    %type = RPGItem::getItemInternalType(%item);
    %ret = 0;
    if(%type == $RPGItem::InvItemType)
    {
        %ret = Player::setItemCount(%clientId,%item,%amt);
        RPGItem::updateItemList(%clientId,%item);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%amt,RPGItem::getItemGroup(%item));
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        %ret = Belt::SetItemCount(%item,%amt);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%amt,RPGItem::getItemGroup(%item));
    }
    return %ret;
}

function RPGItem::incItemCount(%clientId,%item,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    %type = RPGItem::getItemInternalType(%item);
    if(%type == $RPGItem::InvItemType)
    {
        %ret = Item::giveItem(Client::getOwnedObject(%clientId), %item, %amt, %showmsg);
        RPGItem::updateItemList(%clientId,%item);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%ret,RPGItem::getItemGroup(%item));
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        Belt::GiveThisStuff(%clientid, %item, %amt, %showmsg);
        %ret = Belt::HasThisStuff(%clientid,%item);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%ret,RPGItem::getItemGroup(%item));
    }
    return %ret;
}

function RPGItem::forceUpdateBeltItems(%clientId,%type)
{
    %ns = Belt::GetNS(%clientId,%type);
    %itemList = Word::getSubWord(%ns,1,9999);
    echo(%ns);
    for(%i = 0; %i < getWord(%ns,0); %i++)
    {
        %item = getWord(%itemList,%i);
        //echo(%item);
        %cnt = RPGItem::getItemCount(%clientId,%item);
        
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%cnt,RPGItem::getItemGroup(%item));
    }
}

function RPGItem::refreshPlayerInv(%clientId)
{
    %invList = RPGItem::getFullItemList(%clientId);
    
    %sendBuffer = "";
    for(%i = 0; (%item = String::getWord(%invList," ",%i)) != " "; %i++)
    {
        %id = RPGItem::getItemID(%item);
        %desc = RPGItem::getDesc(%item);
        %amt = RPGItem::getItemCount(%clientId,%item);
        %type = RPGItem::getItemGroup(%item);
        
        %len = String::len(%sendBuffer);
        %itemStr = %id @"|"@ %desc @"|"@%amt@"|"@%type @",";
        %strLen = String::len(%itemStr);
        if(%len + %strLen > 200)
        {
            %ll = String::getSubStr(%sendBuffer,0,%len-1); //Drop the last comma
            remoteEval(%clientId,"BufferedPlayerInvList",%ll,false);
            %sendBuffer = "";
        }
        %sendBuffer = %sendBuffer @ %itemStr = %id @"|"@ %desc @"|"@%amt@"|"@%type @",";
    }
    
    %ll = String::getSubStr(%sendBuffer,0,%len-1); //Drop the last comma
    remoteEval(%clientId,"BufferedPlayerInvList",%sendBuffer,true);
}

function RPGItem::getFullItemList(%clientId)
{
    %invList = fetchData(%clientId,"InvItemList");
    %invList = String::replaceAll(%invList,","," ");
    %beltList = "";
    for(%i = 0; %i < $Belt::NumberOfBeltGroups; %i++)
    {
        %grp = $Belt::ItemGroup[%i];
        %ns = Belt::GetNS(%clientId,%grp);
        %itemList = Word::getSubWord(%ns,1,9999);
        %invList = %invList @" "@ %itemList;
    }
    return String::replaceAll(%invList,"  "," ");
}

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

function RPGItem::getItemCount(%clientId,%item)
{
    %type = RPGItem::getItemInternalType(%item);
    %ret = "";
    if(%type == $RPGItem::InvItemType)
    {
        %ret = Player::getItemCount(%clientId,%item);
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        %ret = Belt::HasThisStuff(%clientid,%item);
    }
    return %ret;
}

function RPGItem::decItemCount(%clientId,%item,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    %type = RPGItem::getItemInternalType(%item);
    %ret = "";
    if(%type == $RPGItem::InvItemType)
    {
        %ret = Item::takeItem(Client::getOwnedObject(%clientId), %item, %amt, %showmsg);
        RPGItem::updateItemList(%clientId,%item);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%ret,RPGItem::getItemGroup(%item));
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        Belt::TakeThisStuff(%clientid, %item, %amt, %showmsg);
        %ret = Belt::HasThisStuff(%clientid,%item);
        remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%ret,RPGItem::getItemGroup(%item));
    }
    return %ret;
}

function RPGItem::getDesc(%item)
{
    return $RPGItem::ItemDef[$RPGItem::ItemDef[%item,LabelToID],Name];
    //%type = RPGItem::getItemInternalType(%item);
    //%desc = "";
    //if(%type == $RPGItem::InvItemType)
    //{
    //    if(%item.description == False)	
    //        %desc = %item;
    //    else
    //        %desc = %item.description;
    //}
    //else if(%type == $RPGItem::BeltItemType)
    //{
    //    return $beltitem[%item, "Name"];
    //}
    //return %desc;
}

function RPGItem::useItem(%clientId,%item)
{
    if(!IsDead(%clientId))
    {
        %type = RPGItem::getItemInternalType(%item);
        if(%type == $RPGItem::InvItemType)
        {
            //Item::onUse(Client::getOwnedObject(%clientId),%item);
            Player::useItem(%clientId,%item);
            return true;
        }
        else if(%type == $RPGItem::BeltItemType && Belt::IsUsableItem(%item))
        {
            Belt::UseItem(%clientid,%item);
            return true;
        }
        else
            return false;
    }
    else
        Client::sendMessage(%clientId,$MsgRed,"You can't use items while dead.");
}

function RPGItem::dropItem(%clientId,%item,%amnt)
{
    if(%amnt == "")
        %amnt = 1;
    if(%amnt == "ALL")
        %amnt = RPGItem::getItemCount(%clientId,%item);
    %type = RPGItem::getItemInternalType(%item);
    if(%type == $RPGItem::InvItemType)
    {
        %curAmnt = RPGItem::getItemCount(%clientId,%item);
        if(%item == Weapon)
        {
            %item = Player::getMountedItem(%clientId,$WeaponSlot);
            if(%curAmnt - %amnt == 0)
                Player::unmountItem(%clientId,$WeaponSlot);
            //remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%curAmnt - %amnt,RPGItem::getItemGroup(%item));
        }
        else if(%item == Ammo)
        {
            %item = Player::getMountedItem(%clientId,$WeaponSlot);
            if(%item.className == Weapon)
            {
                %item = %item.imageType.ammoType;
                Player::dropItem(%clientId,%item);
                //remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%curAmnt - %amnt,RPGItem::getItemGroup(%item));
            }
        }
        else if (%item.className == Equipped)
        {
            Client::sendMessage(%clientId, $MsgRed, "You can't drop an equipped item!~wC_BuySell.wav");
        }
        else if ($LoreItem[%item])
        {
            Client::sendMessage(%clientId, $MsgRed, "You can't drop a lore item!~wC_BuySell.wav");
        }
        else
        {
            %clientId.bulkDrop = %amnt;
            Player::dropItem(%clientId,%item);
            //dropItem eventually leads to decItemCount, where setitemcount will be handled.
            //remoteEval(%clientId,"SetItemCount",$RPGItem::ItemDef[%item,LabelToID],RPGItem::getDesc(%item),%curAmnt - %amnt,RPGItem::getItemGroup(%item));
        }
    }
    else if(%type == $RPGItem::BeltItemType)
    {
        Belt::DropItem(%clientId,%item,%amnt);
        Client::SendMessage(%clientId, $MsgWhite, "You dropped "@ %amnt @" "@%item@".~wPku_weap.wav");
    }
}

function RPGItem::isValidItem(%item)
{
    return RPGItem::isBeltItem(%item) || RPGItem::isInventoryItem(%item);
}

function RPGItem::isBeltItem(%item)
{
    return isBeltItem(%item);
}

function RPGItem::isInventoryItem(%item)
{
    //The one weird exception
    if(String::icompare(%item,"smallrock") == 0)
        return false;
    else
    {
        return $RPGItem::ItemDef[$RPGItem::ItemDef[%item,LabelToID],InternalType] == $RPGItem::InvItemType;
    }
}

function RPGItem::getItemInternalType(%item)
{
    if(%item == "SmallRock") //One exception
        return $RPGItem::BeltItemType;
        
    return $RPGItem::ItemDef[$RPGItem::ItemDef[%item,LabelToID],InternalType];
        
    //if(RPGItem::isInventoryItem(%item))
    //    return $RPGItem::InvItemType;
    //else if(RPGItem::isBeltItem(%item))
    //    return $RPGItem::BeltItemType;
    //else
    //    return -1;
}