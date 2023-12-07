$RPGItem::WeaponTypeMelee = 0;
$RPGItem::WeaponTypeRange = 1;
$RPGItem::WeaponTypePick = 2;


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

function RPGItem::AddItemDefinition(%itemLabel,%name,%class,%id,%datablk)
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
}

function RPGItem::AddWeapon(%label,%name,%id,%weaponType,%datablk)
{
    RPGItem::AddItemDefinition(%label,%name,$RPGItem::WeaponClass,%id,%datablk);
    $RPGItem::ItemDef[%id,WeaponType] = %weaponType;
    $RPGItem::ItemDef[%id,Equippable] = true;
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
    return $RPGItem::ItemDef[%itemLabel,LabelToID];
}

function RPGItem::LabelToItemName(%itemLabel)
{
    return $RPGItem::ItemDef[RPGItem::LabelToItemID(%itemLabel),Name];
}

//Careful when using this
function RPGItem::LabelToItemTag(%itemLabel)
{
    %id = RPGItem::LabelToItemID(%itemLabel);
    return "id"@%id;
}

function RPGItem::ItemTagToLabel(%itemTag)
{
    return $RPGItem::ItemDef[RPGItem::getItemIDFromTag(%itemTag),Label];
}

function RPGItem::getItemNameFromTag(%itemTag)
{
    return RPGItem::getItemName(RPGItem::getItemIDFromTag(%itemTag));
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

function RPGItem::SendItemDataToClient(%clientId,%itemId,%amt)
{
    if(!Player::isAIControlled(%clientId))
        remoteEval(%clientId,"SetItemCount",%itemId,RPGItem::getItemName(%itemId),%amt,RPGItem::getItemGroup(%itemId));
}

function RPGItem::isItemTag(%input)
{
    return String::getSubStr(%input,0,2) == "id" && Math::isInteger(String::copyUntil(String::getSubStr(%input,2,9999),"_"));
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

//For now, this function only looks for exact tag matches, because that's what inc and dec do as well
function RPGItem::getItemCount(%clientId,%itemTag)
{
    %itemId = RPGItem::getItemIDFromTag(%itemTag); 
    %classInvTag = RPGItem::getInvStrGroup(%itemId);
    %invStr = fetchData(%clientId,%classInvTag);
    return GetStuffStringCount(%invStr,%itemTag); 
    //return RPGItem::countItemsInStuffString(%invStr,%itemTag,%exactFlag);
}

function RPGItem::countItemsInStuffString(%string,%tag,%exactFlag)
{
    if(%exactFlag)
        return GetStuffStringCount(%stuff, %tag);
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

    //echo(%itemTag);
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    if(RPGItem::isValidItem(%itemId))
    {
        %classInvTag = RPGItem::getInvStrGroup(%itemId);
        %invStr = fetchData(%clientId,%classInvTag);
        %newStr = SetStuffString(%invStr,%itemTag,%amt);
        storeData(%clientId,%classInvTag,%newStr);
        %ret = $RPGStuffStr::amount;
    
        RPGItem::SendItemDataToClient(%clientId,%itemId,%ret);
    }
    else
        echo(%itemTag @" - Invalid Item");
    if(%showmsg)
        Client::sendMessage(%clientId, 0, "You received " @ %amt @ " " @ RPGItem::getItemName(%itemId) @ ".");
    
    return %ret;
}

function RPGItem::decItemCount(%clientId,%itemTag,%amt,%showmsg)
{
    if(%amt == "")
        %amt = 1;
    if(%amt < 0)
        return RPGItem::incItemCount(%clientId,%itemTag,%amt,%showmsg);
        
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    %classInvTag = RPGItem::getInvStrGroup(%itemId);
    %invStr = fetchData(%clientId,%classInvTag);
    %newStr = SetStuffString(%invStr,%itemTag,%amt,"dec");
    storeData(%clientId,%classInvTag,%newStr);
    %ret = $RPGStuffStr::amount;
    
    if(%ret == 0)
    {
        if($RPGItem::ItemDef[%itemId,ClassName] == $RPGItem::WeaponClass && %itemTag == fetchData(%clientId,"EquippedWeapon"))
        {
            Player::unmountItem(%clientId,$WeaponSlot);
            storeData(%clientId,"EquippedWeapon","");
        }
    }
    
    RPGItem::SendItemDataToClient(%clientId,%itemId,%ret);
    
    if(%showmsg)
        Client::sendMessage(%clientId, 0, "You lost " @ %amt @ " " @ RPGItem::getItemName(%itemId) @ ".");
    
    return %ret;
}

function RPGItem::setItemCount(%clientId,%itemTag,%amt)
{
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    echo(%itemTag);
    %classInvTag = RPGItem::getInvStrGroup(%itemId);
    %invStr = fetchData(%clientId,%classInvTag);
    %newStr = SetStuffString(%invStr,%itemTag,%amt,"set");
    echo(%newStr);
    storeData(%clientId,%classInvTag,%newStr);
    %ret = $RPGStuffStr::amount;
    echo(%ret);
    if(%ret == 0)
    {
        if($RPGItem::ItemDef[%itemId,ClassName] == $RPGItem::WeaponClass && %itemTag == fetchData(%clientId,"EquippedWeapon"))
        {
            Player::unmountItem(%clientId,$WeaponSlot);
            storeData(%clientId,"EquippedWeapon","");
        }
    }
    
    RPGItem::SendItemDataToClient(%clientId,%itemId,%ret);
    
    return %ret;
}

function RPGItem::getFullItemList(%clientId,%storageFlag)
{
    if(%storageFlag == "")
        %storageFlag = false;
        
    %invStr = "";
    for(%i = 0; %i < $RPGItem::ItemClassCount; %i++)
    {
        %classLabel = $RPGItem::ItemClass[%i,Label];
        if(%storageFlag)
            %classInvTag = $RPGItem::ItemClass[%classLabel,StorageTag];
        else
            %classInvTag = $RPGItem::ItemClass[%classLabel,InventoryTag];
        %invStr = %invStr @ fetchData(%clientId,%classInvTag);
    }
    
    return %invStr;
}

//=========================
// END Inventory Functions
//=========================

// WIP
function RPGItem::useItem(%clientId,%itemTag,%amnt)
{
    if(!IsDead(%clientId))
    {
        //Use item
    }
}

// WIP
function RPGItem::dropItem(%clientId,%itemTag,%amnt)
{
    if(%amnt == "")
        %amnt = 1;
    if(%amnt == "ALL")
        %amnt = RPGItem::getItemCount(%clientId,%itemTag);
    
    %itemId = RPGItem::getItemIDFromTag(%itemTag);
    %class = RPGItem::getItemGroup(%clientId,%itemId);
    
    %curAmnt = RPGItem::getItemCount(%clientId,%itemTag);
    
    if(%class == $RPGItem::EquipppedClass)
    {
        Client::sendMessage(%clientId, $MsgRed, "You can't drop an equipped item!~wC_BuySell.wav");
        return;
    }
    else if(%class == $RPGItem::WeaponClass)
    {
        %item = Player::getMountedItem(%clientId,$WeaponSlot);
        if(%curAmnt - %amnt == 0)
            Player::unmountItem(%clientId,$WeaponSlot);
        //Toss item
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