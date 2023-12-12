
//To my knowledge this is the only function I am overriding
function CmdInventoryGui::onOpen()
{
	if($RPGMenu::serv == $ServerName)
	{
        $RPGMenu::expandItemOptions[Buy] = false;
        $RPGMenu::expandItemOptions[Inv] = false;
		RPGMenu::ReloadPlayerInventory();
		RPGMenu::ReloadInventoryBuyList();
	}
}

//And now this one too -Kyred
function CmdInventoryGui::onClose()
{
    closeInventory();
}

$RPGMenu::rebuild = false;
$RPGMenu::expandItemOptions[Buy] = false;
$RPGMenu::expandItemOptions[Inv] = false;
$RPGMenu::unlimittedItemMenu = false;
$RPGMenu::buyListTempBuffer = "";
$RPGMenu::playerInvListTempBuffer = "";

//================================
// Remote Functions
//================================

//%costFlag determines if items of 0 value should be kept in the list
function remoteBufferedBuyList(%mgr,%shopList,%costFlag,%finish)
{
    if(%mgr != 2048)
	{
		return;
	}
    if(%amt == "")
	{
		%amt = " ";
	}
    
    //echo("Length: "@ String::len(%shopList));
    $RPGMenu::buyListActive = true;
    $RPGMenu::buyListTempBuffer = $RPGMenu::buyListTempBuffer @","@ %shopList;
    $RPGMenu::unlimittedItemMenu = %costFlag;
    if(%finish)
    {
        %temp = $RPGMenu::itemList[Buy,Showing];
        $RPGMenu::itemList[Buy,Showing] = false;
        echo($RPGMenu::buyListTempBuffer @" "@ String::len($RPGMenu::buyListTempBuffer));
        
        
        for(%i = 0; (%elem = String::getWord($RPGMenu::buyListTempBuffer,",",%i)) != ","; %i++)
        {
            //String::getWord(%elem,"|",0); //ID
            //String::getWord(%elem,"|",1); //Name
            //String::getWord(%elem,"|",2); //Cost
            //String::getWord(%elem,"|",3); //Type
            remoteSetBuyList(2048, String::getWord(%elem,"|",0) , String::getWord(%elem,"|",1), String::getWord(%elem,"|",2), String::getWord(%elem,"|",3));
        }
        $RPGMenu::buyListTempBuffer = "";
        
        if(%temp)
        {
            $RPGMenu::itemList[Buy,Showing] = true;
            RPGMenu::ReloadInventoryBuyList();
        }
    }
}

function remoteBufferedPlayerInvList(%mgr,%shopList,%finish)
{
    if(%mgr != 2048)
	{
		return;
	}
    
    echo("Length: "@ String::len(%shopList));
    
    $RPGMenu::playerInvListTempBuffer = $RPGMenu::playerInvListTempBuffer @","@ %shopList;
    
    if(%finish)
    {
        %temp = $RPGMenu::itemList[Inv,Showing];
        $RPGMenu::itemList[Inv,Showing] = false;
        echo($RPGMenu::playerInvListTempBuffer @" "@ String::len($RPGMenu::playerInvListTempBuffer));
        
        for(%i = 0; (%elem = String::getWord($RPGMenu::playerInvListTempBuffer,",",%i)) != ","; %i++)
        {
            //String::getWord(%elem,"|",0); //ID
            //String::getWord(%elem,"|",1); //Name
            //String::getWord(%elem,"|",2); //Cost
            //String::getWord(%elem,"|",3); //Type
            %uniqueId = buildUniqueId(String::getWord(%elem,"|",0), String::getWord(%elem,"|",1));
            echo("UNIQUE_ID: "@ %uniqueId);
            RPGMenu::UpdateInventoryList(%uniqueId,String::getWord(%elem,"|",2),String::getWord(%elem,"|",3),Inv);
            //remoteSetItemCount(2048, String::getWord(%elem,"|",0), String::getWord(%elem,"|",1), String::getWord(%elem,"|",2), String::getWord(%elem,"|",3));
        }
        $RPGMenu::playerInvListTempBuffer = "";
        
        if(%temp)
        {
            $RPGMenu::itemList[Inv,Showing] = true;
            RPGMenu::ReloadPlayerInventory();
        }
    }
    
}

// Add an item to the buy list
function remoteSetBuyList(%mgr, %num, %name, %amt, %type)
{
    echo("remoteSetBuyList("@%mgr@","@%num@","@%name@","@%amt@","@%type@")");
    if(%mgr != 2048)
	{
		return;
	}
    if(%amt == "")
	{
		%amt = " ";
	}
    %uniqueId = buildUniqueId(%name, %num);
    
    if($RPGMenu::expandItemOptions[Buy] && %amt == 0)
    {
        $RPGMenu::expandItemOptions[Buy] = false;
    }
    
    RPGMenu::UpdateInventoryList(%uniqueId,%amt,%type,Buy);
    
    if($RPGMenu::itemList[Buy,Showing] == true)
    {
        triggerreload(%num);
    }
}

//Add an item to the inventory list (aka Sell list)
function remoteSetItemCount(%mgr, %num, %name, %amt, %type)
{
    echo("remoteSetItemCount("@%mgr@","@%num@","@%name@","@%amt@","@%type@")");
    if(%mgr != 2048)
	{
		return;
	}
    
    if(%amt == "")
	{
		%amt = 0;
	}
    
    %uniqueId = buildUniqueId(%name, %num);

    RPGMenu::UpdateInventoryList(%uniqueId,%amt,%type,Inv);
    
    if($RPGMenu::itemList[Inv,Showing] == true)
	{
		RPGMenu::ReloadPlayerInventory();
	}
}

// Called by server when finished connecting
function remoteRPGMenuInfo(%mgr)
{
    if(%mgr != 2048)
	{
		return;
	}
    $RPGMenu::serv = $ServerName;
	clearAllInv();
	switchInventory(1);
	remoteeval(2048, RPGMenuVer, $RPGMenu::ver);
}

//Was remoteClearInv
function remoteClearBuyList(%mgr)
{
	if(%mgr != 2048)
	{
		return;
	}
    echo("CLEAR");
    %temp = $RPGMenu::itemList[Buy,Showing];
	//TextList::clear(RPGBuy);
	//TextList::clear(RPGBuyCnt);
	deletevariables("RPGMenu::itemListbuy*");
	$RPGTypeCount[Buy] = 0;
    $RPGMenu::expandItemOptions[Buy] = false;
	$RPGMenu::itemList[Buy,Showing] = %temp;
}

function remoteClearPlayerInv(%mgr)
{
    if(%mgr != 2048)
	{
		return;
	}
    %temp = $RPGMenu::itemList[Inv,Showing];
    deletevariables("RPGMenu::itemListinv*");
    $RPGTypeCount[Inv] = 0;
    $RPGMenu::itemList[Inv,Showing] = %temp;
    $RPGMenu::playerInvListTempBuffer = "";
    $RPGMenu::rebuild = false;
    $RPGMenu::expandItemOptions[Inv] = false;
}

function remoteClearInv(%mgr)
{
    if(%mgr != 2048)
	{
		return;
	}
    clearAllInv();
    $RPGMenu::rebuild = false;
    $RPGMenu::expandItemOptions[Inv] = false;
    $RPGMenu::expandItemOptions[Buy] = false;
    
    $RPGMenu::buyListTempBuffer = "";
    $RPGMenu::playerInvListTempBuffer = "";
}

//================================
// END Remote Functions
//================================

//================================
// GUI Functions
//================================
// Functions defined by gui elements

//When you click on an item element in the buy list
function SelectBuyItem()
{
    if($RPGMenu::rebuild != true)
	{
        %val = Control::getValue(RPGBuy);
        //if(String::findsubstr(%val, "|num|") == -1)
		//{
		//	RPGMenu::ReloadInventoryBuyList();
		//	return;
		//}
        if($RPGMenu::itemList[Buy,LastClick] == %val)
        {
            %isItem = determineItemNum(%val) != "";
            $RPGMenu::itemList[Buy,LastClick,Time,%val]++;
            schedule("$RPGMenu::itemList[Buy,LastClick, Time, \"" @ %val @ "\"]--;", 1);
            
            if(%isItem)
                $RPGMenu::itemList[Buy,isSelected] = true;
            
            if($RPGMenu::itemList[Buy,LastClick, Time, %val] > 2)
            {
                if(%isItem)
                {
                    //%itemNum = determineItemNum(%val);
                    Inv::buySelectedItem();
                }
                else
                {
                    if(isItemOptionElem(%val))
                    {
                        %w1 = getWord(%val,0);
                        %w2 = getWord(%val,1);
                        if(%w1 == "Buy:" || %w1 == "Take:")
                        {
                            if(%w2 != "ALL")
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Buy,Select]);
                                
                                remoteEval(2048, buyItem, %itemNum, %w2);
                            }
                            else
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Buy,Select]);
                                
                                remoteEval(2048, buyItem, %itemNum, "ALL");
                            }
                        }
                    }
                    else
                    {
                        %type = String::getSubStr(%val,1,9999);
                        $RPGMenu::TypeGroupIsVisible[Buy,%type] = !$RPGMenu::TypeGroupIsVisible[Buy,%type];
                    }
                    
                    RPGMenu::ReloadInventoryBuyList();
                }
                $RPGMenu::itemList[Buy,isSelected] = false;
            }
        }
        else
        {
            $RPGMenu::itemList[Buy,LastClick] = %val;
            %itemID = determineItemNum(%val);
            if(%itemID != "")
                $RPGMenu::itemList[Buy,Select] = %val;
            if($RPGMenu::expandItemOptions[Buy] && %itemID != "")
            {
                $RPGMenu::expandItemOptions[Buy] = false;
                RPGMenu::ReloadInventoryBuyList();
            }
            
        }
    }
}

//When you click on an item element in the sell list
function SelectSellItem()
{
    //echo($RPGMenu::itemList[Inv,Select]);
    if($RPGMenu::rebuild != true)
	{
		%val = Control::getValue(RPGSell);
        //%isOpt = isItemOptionElem(%val);
        //if(String::findsubstr(%val, "|num|") == -1) //&& !isOpt)
		//{
		//	RPGMenu::ReloadPlayerInventory();
		//	return;            
		//}
        if($RPGMenu::itemList[Inv,LastClick] == %val)
        {
            %isItem = determineItemNum(%val) != "";
			//Don't ask why but if you click once the thing spams twice so I am going to
			//make the code work off the double input
			$RPGMenu::itemList[Inv,LastClick, Time, %val]++;
            if(%isItem)
                $RPGMenu::itemList[Inv,isSelected] = true;
			schedule("$RPGMenu::itemList[Inv,LastClick, Time, \"" @ %val @ "\"]--;", 1);
			if($RPGMenu::itemList[Inv,LastClick, Time, %val] > 2)
			{
                if(%isItem)
                    Inv::sellSelectedItem();
                else
                {
                    if(isItemOptionElem(%val))
                    {
                        %w1 = getWord(%val,0);
                        %w2 = getWord(%val,1);
                        if(%w1 == "Drop:")
                        {
                            if(%w2 != "ALL")
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                                
                                remoteEval(2048, dropItem, %itemNum, %w2);
                            }
                            else
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                                
                                remoteEval(2048, dropItem, %itemNum, "ALL");
                            }
                        }
                        else if(%w1 == "Sell:" || %w1 == "Take:")
                        {
                            if(%w2 != "ALL")
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                                
                                remoteEval(2048, sellItem, %itemNum, %w2);
                            }
                            else
                            {
                                %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                                
                                remoteEval(2048, sellItem, %itemNum, "ALL");
                            }
                        }
                    }
                    else
                    {
                        %type = String::getSubStr(%val,1,9999);
                        $RPGMenu::TypeGroupIsVisible[Inv,%type] = !$RPGMenu::TypeGroupIsVisible[Inv,%type];
                    }
                    RPGMenu::ReloadPlayerInventory();
                }
                $RPGMenu::itemList[Inv,isSelected] = false;
			}
		}
		else
		{
			$RPGMenu::itemList[Inv,LastClick] = %val;
            %itemID = determineItemNum(%val);
            if(%itemID != "")
                $RPGMenu::itemList[Inv,Select] = %val;
            if($RPGMenu::expandItemOptions[Inv] && %itemID != "")
            {
                $RPGMenu::expandItemOptions[Inv] = false;
                RPGMenu::ReloadPlayerInventory();
            }
		}
    }
}

//When you click on the number/amount element to the right of the item
// Called by both the buy and sell side, despite the name.
function SelectSellNum()
{
    //Make sure people can't click on the item but will be called when the inventory is opened
	//this is the place where I will make sure that you aren't in a different server that doesn't
	//support it if you switched
	if($ServerName != $RPGMenu::serv)
	{
		loadplaygui();
		switchInventory(-1);
	}
}

function Inv::useSelectedItem()
{
	%val = Control::getValue(RPGSell);
	%itemNum = determineItemNum(%val);
    if(%itemNum != "")
        remoteEval(2048, useItem, %itemNum);
}

//My bad I made a mistake in that I forgot to add item but it is to much work
//changing it so I am just forwarding it
//When you click the BUY button
function Inv::buySelected()
{
	Inv::buySelectedItem();
}

//Technically not a gui function, but acts as such for consistancy
function Inv::buySelectedItem()
{
    %val = Control::getValue(RPGBuy);
	%itemNum = determineItemNum(%val);
    if(%itemNum != "")
    {
        if(!$RPGMenu::expandItemOptions[Buy] && $RPGMenu::bulkCount == 1)
        {
            
            $RPGMenu::expandItemOptions[Buy] = true;
            if($RPGMenu::unlimittedItemMenu)
                $RPGMenu::expandType[Buy] = "Buy";
            else
                $RPGMenu::expandType[Buy] = "Take";
            RPGMenu::ReloadInventoryBuyList();
        }
        else
        {
            remoteEval(2048, buyItem, %itemNum,$RPGMenu::bulkCount);
            $RPGMenu::bulkCount = 1;
        }
    }
    else
    {
        if(isItemOptionElem(%val))
        {
            %w1 = getWord(%val,0);
            %w2 = getWord(%val,1);
            if(%w1 == "Buy:" || %w1 == "Take:")
            {
                if(Math::isInteger(%w2))
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Buy,Select]);
                    
                    remoteEval(2048, buyItem, %itemNum, %w2);
                }
                if(%w2 == "ALL")
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Buy,Select]);
                    
                    remoteEval(2048, buyItem, %itemNum, "ALL");
                }
            }
        }
    }
    //remoteEval(2048, buyItem, %itemNum,$RPGMenu::bulkCount);
}

//When you click the drop button
function Inv::dropSelectedItem()
{
    %val = Control::getValue(RPGSell);
    %itemNum = determineItemNum(%val);
    if(%itemNum != "")
    {
        if(!$RPGMenu::expandItemOptions[Inv] && $RPGMenu::bulkCount == 1)
        {
            $RPGMenu::expandItemOptions[Inv] = true;
            $RPGMenu::expandType[Inv] = "Drop";
            RPGMenu::ReloadPlayerInventory();
        }
        else
        {
            remoteEval(2048, dropItem, %itemNum, $RPGMenu::bulkCount);
            $RPGMenu::bulkCount = 1;
        }
    }
    else
    {
        if(isItemOptionElem(%val))
        {
            %w1 = getWord(%val,0);
            %w2 = getWord(%val,1);
            if(%w1 == "Drop:")
            {
                if(Math::isInteger(%w2))
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                    
                    remoteEval(2048, dropItem, %itemNum, %w2);
                }
                if(%w2 == "ALL")
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                    
                    remoteEval(2048, dropItem, %itemNum, "ALL");
                }
            }
        }
    }
}

//When you click the sell button
function Inv::sellSelectedItem()
{
    %val = Control::getValue(RPGSell);
	%itemNum = determineItemNum(%val);
    if(%itemNum != "")
    {
        if($RPGMenu::buyListActive && !$RPGMenu::expandItemOptions[Inv] && $RPGMenu::bulkCount == 1 )
        {
            $RPGMenu::expandItemOptions[Inv] = true;
            if($RPGMenu::unlimittedItemMenu)
                $RPGMenu::expandType[Inv] = "Sell";
            else
                $RPGMenu::expandType[Inv] = "Take";
            RPGMenu::ReloadPlayerInventory();
        }
        else
        {
            remoteEval(2048, sellItem, %itemNum,$RPGMenu::bulkCount);
            $RPGMenu::bulkCount = 1;
        }
    }
    else
    {
        if(isItemOptionElem(%val))
        {
            %w1 = getWord(%val,0);
            %w2 = getWord(%val,1);
            if(%w1 == "Sell:" || %w1 == "Take:")
            {
                if(%w2 != "ALL")
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                    
                    remoteEval(2048, sellItem, %itemNum, %w2);
                }
                else
                {
                    %itemNum = determineItemNum($RPGMenu::itemList[Inv,Select]);
                    
                    remoteEval(2048, sellItem, %itemNum, "ALL");
                }
            }
        }
    }
}

//================================
// END GUI Functions
//================================

//================================
// Core Functions
//================================

function clearAllInv()
{
	//Clearing buying
	TextList::clear(RPGBuy);
	TextList::clear(RPGBuyCnt);

	//Clearing selling
	TextList::clear(RPGSell);
	TextList::clear(RPGSellCnt);

    
    
    deletevariables("RPGMenu::itemList*");
    $RPGTypeCount[Buy] = 0;
	$RPGTypeCount[Inv] = 0;
}


function closeInventory()
{
    $RPGMenu::itemList[Inv,Showing] = false;
    $RPGMenu::itemList[Buy,Showing] = false;
    $RPGMenu::itemList[Inv,isSelected] = false;
    $RPGMenu::itemList[Buy,isSelected] = false;
    $RPGMenu::expandItemOptions[Inv] = false;
    $RPGMenu::expandItemOptions[Buy] = false;
    $RPGMenu::unlimittedItemMenu = false; //Is this a shop?  Or a bank?
    $RPGMenu::bulkCount = 1;
    $RPGMenu::expandType[Inv] = false;
    $RPGMenu::expandType[Buy] = false;
    
    if($RPGMenu::buyListActive)
    {
        remoteClearBuyList(2048);
        $RPGMenu::buyListActive = false;
    }
}

function RPGMenu::ReloadPlayerInventory()
{
    $RPGMenu::rebuild = true;
    schedule("$RPGMenu::rebuild=false;", 0.1);
    $RPGMenu::bulkCount = 1;
    $RPGMenu::itemList[Inv,Showing] = true;
    Control::setActive(RPGSellCnt, true);
    TextList::clear(RPGSell);
    TextList::clear(RPGSellCnt);
    
    for(%z=0; %z<2+$RPGTypeCount[Inv]; %z++)
    {
        %type = $RPGMenu::itemList[Inv,Types,%z];
        %total = $RPGMenu::itemList[Inv,TypeItemList,%type,ListCount];
        //echo(%total @" "@ %type);
        if(%total != "")
        {
            if($RPGMenu::TypeGroupIsVisible[Inv,%type])
            {
                %setGroupFlag = true;
                for(%i=1; %i<=%total; %i++)
                {
                    //echo(%i);
                    %itemUID = $RPGMenu::ItemList[Inv,TypeItemList, %type, %i];
                    %amt = $RPGMenu::ItemList[Inv,Item,%itemUID];
                    if(%amt != 0)
                    {
                        if(%setGroupFlag)
                        {
                            TextList::addLine(RPGSell, "-"@%type);
                            addAmt("");
                            %setGroupFlag = false;
                        }
                        TextList::addLine(RPGSell, tab("\x20", 2) @
							%itemUID);
						addAmt(%amt);
                        
                        if(%amt > 1 && determineItemNum(%itemUID) == determineItemNum($RPGMenu::itemList[Inv,Select]) && $RPGMenu::expandItemOptions[Inv])
                        {
                            showItemOptions($RPGMenu::expandType[Inv],RPGSell,%amt);
                        }
                    }
                }
            }
            else
            {
                TextList::addLine(RPGSell, "+"@%type);
				addAmt("");
            }
        }
    }
    
    Control::setValue(RPGSell, $RPGMenu::itemList[Inv,Select]); //$RPGMenu::itemList[Inv,LastClick]);
}

function RPGMenu::ReloadInventoryBuyList()
{
    $RPGMenu::rebuild = true;
    schedule("$RPGMenu::rebuild=false;", 0.1);
    $RPGMenu::bulkCount = 1;
    $RPGMenu::itemList[Buy,Showing] = true;
    Control::setActive(RPGBuyCnt, true);
    TextList::clear(RPGBuy);
    TextList::clear(RPGBuyCnt);
    
    for(%z=0; %z<2+$RPGTypeCount[Buy]; %z++)
    {
        %type = $RPGMenu::itemList[Buy,Types,%z];
        %total = $RPGMenu::itemList[Buy,TypeItemList,%type,ListCount];
        if(%total != "")
        {
            if($RPGMenu::TypeGroupIsVisible[Buy,%type])
            {
                %setGroupFlag = true;
                for(%i=1; %i<=%total; %i++)
                {
                    
                    %itemUID = $RPGMenu::ItemList[Buy,TypeItemList, %type, %i];
                    %amt = $RPGMenu::ItemList[Buy,Item,%itemUID];
                    if(%amt != 0 || $RPGMenu::unlimittedItemMenu)
                    {
                        if(%setGroupFlag)
                        {
                            TextList::addLine(RPGBuy, "-"@%type);
                            addAmt("",bl);
                            %setGroupFlag = false;
                        }
                        TextList::addLine(RPGBuy, tab("\x20", 2) @
                            %itemUID);
                        addAmt(%amt,bl);
                    }
                    
                    echo($RPGMenu::expandItemOptions[Buy]);
                    
                    if(determineItemNum(%itemUID) == determineItemNum($RPGMenu::itemList[Buy,Select]) && $RPGMenu::expandItemOptions[Buy])
                    {
                        showBuyItemOptions($RPGMenu::expandType[Buy],RPGBuy,%amt,!$RPGMenu::unlimittedItemMenu);
                    }
                }
            }
            else
            {
                TextList::addLine(RPGBuy, "+"@%type);
				addAmt("",bl);
            }
        }
    }
    
    Control::setValue(RPGBuy, $RPGMenu::itemList[Buy,Select]);
    //Control::setValue(RPGBuy, $RPGMenu::itemList[Buy,LastClick]);
}



function RPGMenu::UpdateInventoryList(%uid,%amt,%type,%listTag)
{
    echo("RPGMenu::UpdateInventoryList("@%uid@","@%amt@","@%type@","@%listTag@")");
    if($RPGMenu::itemList[%listTag,TypeItemList,%type,ListCount] == "")
    {
		if(String::iCompare(%type, "Armor") == 0 || String::iCompare(%type, "Equipped") == 0)
        {
            //if($RPGTypes[%listTag] == "")
			//{
            //    $RPGTypes[%listTag] = 0;
			//}
            $RPGMenu::itemList[%listTag,Types,0] = %type;
        }
        else if(String::iCompare(%type, "Weapon") == 0)
        {

            $RPGMenu::itemList[%listTag,Types,1] = %type;
        }
        else
        {
			$RPGMenu::itemList[%listTag,Types,2+$RPGTypeCount[%listTag]] = %type;
            $RPGTypeCount[%listTag]++;
        }
        $RPGMenu::TypeGroupIsVisible[%listTag,%type] = true;
    }
    if($RPGMenu::itemList[%listTag,Item,%uid] == "")
    {
        $RPGMenu::itemList[%listTag,TypeItemList,%type,ListCount]++;
        $RPGMenu::itemList[%listTag,TypeItemList,%type,$RPGMenu::itemList[%listTag,TypeItemList,%type,ListCount]] = %uid;
    }
    
    $RPGMenu::itemList[%listTag,Item,%uid] = %amt;
}

function addAmt(%amt, %type)
{
	if(%type == "bl")
	{
		TextList::addLine(RPGBuyCnt, %amt);
	}
	else
		TextList::addLine(RPGSellCnt, %amt);
}

//================================
// END Core Functions
//================================

//================================
// Utility Functions
//================================

function requestInvRefresh()
{
    remoteEval(2048,"RequestPlayerInventoryRefresh");
}

function isItemOptionElem(%val)
{
    %w1 = getWord(%val,0);
    
    return %w1 == "Drop:" || %w1 == "Buy:" || %w1 == "Sell:" || %w1 == "Take:";
}

function switchInventory(%type)
{
	%id = nameToId("CmdInventoryGui");
	if(%id != -1)
	{
		deleteobject(%id);
	}
	if(%type == 1)
	{
		loadObject("CmdInventoryGui", "gui\\menuInv.gui");
	}
	else
	{
		GuiloadContentCtrl(MainWindow, "gui\\cmdInventory.gui");
	}
}

function buildUniqueId(%name, %num)
{
	return (%name @ tab(" ", 30) @ "|num|" @ %num);
}

function determineItemNum(%name)
{
	%loc = String::findsubstr(%name, "|num|");
    if(%loc == -1)
        return "";
	%itm = String::getsubStr(%name, %loc + 5, 9999);
	return %itm;
}

function tab(%type, %amt)
{
	%result = "";
	for(%i=1; %i<=%amt; %i++)
	{
		%result = %result @ %type;
	}
	return (%result);
}

function triggerreload(%num, %var)
{
	if(%var == "")
	{
		$RPGMenu::buydelay = %num;
		schedule("triggerreload(\"t\", " @ %num @ ");", 0.1);
	}
	else if(%num == "t" && $RPGMenu::buydelay == %var)
	{
		RPGMenu::ReloadInventoryBuyList();
	}
}

function showItemOptions(%prefix,%obj,%amt,%showAll)
{
    if(%showAll == "")
        %showAll = true;
    if(%amt >= 5)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 5");
        addAmt("");
    }
    if(%amt >= 10)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 10");
        addAmt("");
    }
    if(%amt >= 50)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 50");
        addAmt("");
    }
    if(%amt >= 100)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 100");
        addAmt("");
    }
    if(%amt >= 500)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 500");
        addAmt("");
    }
    if(%showAll)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": ALL");
        addAmt("");
    }
}
function showBuyItemOptions(%prefix,%obj,%amt,%showAll)
{
    if(%showAll == "")
        %showAll = true;
    if(%amt >= 5 || $RPGMenu::unlimittedItemMenu)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 5");
        if($RPGMenu::unlimittedItemMenu)
            addAmt(5 * %amt,bl);
        else
            addAmt("",bl);
    }
    if(%amt >= 10 || $RPGMenu::unlimittedItemMenu)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 10");
        if($RPGMenu::unlimittedItemMenu)
            addAmt(10*%amt,bl);
        else
            addAmt("",bl);
    }
    if(%amt >= 50 || $RPGMenu::unlimittedItemMenu)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 50");
        if($RPGMenu::unlimittedItemMenu)
            addAmt(50*%amt,bl);
        else
            addAmt("",bl);
    }
    if(%amt >= 100 || $RPGMenu::unlimittedItemMenu)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 100");
        if($RPGMenu::unlimittedItemMenu)
            addAmt(100*%amt,bl);
        else
            addAmt("",bl);
    }
    if(%amt >= 500 || $RPGMenu::unlimittedItemMenu)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": 500");
        if($RPGMenu::unlimittedItemMenu)
            addAmt(500*%amt,bl);
        else
            addAmt("",bl);
    }
    if(%showAll)
    {
        TextList::addLine(%obj, tab("\x20", 4) @
        %prefix@": ALL");
        addAmt("",bl);
    }
}

function String::length(%string)
{
	%chunk = 10;
	%length = 0;

	for(%i = 0; String::getSubStr(%string, %i, 1) != ""; %i += %chunk)
		%length += %chunk;
	%length -= %chunk;

	%checkstr = String::getSubStr(%string, %length, 99999);
	for(%k = 0; String::getSubStr(%checkstr, %k, 1) != ""; %k++)
		%length++;

	if(%length == -%chunk)
		%length = 0;

	return %length;
}

//================================
// END Utility Functions
//================================
