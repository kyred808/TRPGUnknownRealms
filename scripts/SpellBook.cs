
function SpellBook::AddSpellType(%label,%name,%skillIndex,%index)
{
    $SpellType::NumSpellTypes++;
    $SpellType::IndexToLabel[%index] = %label;
    $SpellType[%label,Name] = %name;
    $SpellType[%label,Skill] = %skillIndex;
    $SpellType[%label,SpellCount] = 0;
}

function SpellBook::AddSpellToType(%spellTypeLabel,%spellIndex)
{
    $SpellType[%spellTypeLabel,Spells,$SpellType[%spellTypeLabel,SpellCount]] = %spellIndex;
    $SpellType[%spellTypeLabel,SpellCount]++;
}

$SpellType::NumSpellTypes = 0;
SpellBook::AddSpellType("offcast","Offensive Casting",$SkillOffensiveCasting,0);
SpellBook::AddSpellType("defcast","Defensive Casting",$SkillDefensiveCasting,1);
SpellBook::AddSpellType("neucast","Neutral Casting",$SkillNeutralCasting,2);

SpellBook::AddSpellToType("offcast",$Spell::index[thorn]);
SpellBook::AddSpellToType("offcast",$Spell::index[fireball]);
SpellBook::AddSpellToType("offcast",$Spell::index[firebomb]);
SpellBook::AddSpellToType("offcast",$Spell::index[icespike]);
SpellBook::AddSpellToType("offcast",$Spell::index[icestorm]);
SpellBook::AddSpellToType("offcast",$Spell::index[ironfist]);
SpellBook::AddSpellToType("offcast",$Spell::index[cloud]);
SpellBook::AddSpellToType("offcast",$Spell::index[melt]);
SpellBook::AddSpellToType("offcast",$Spell::index[powercloud]);
SpellBook::AddSpellToType("offcast",$Spell::index[hellstorm]);
SpellBook::AddSpellToType("offcast",$Spell::index[beam]);
SpellBook::AddSpellToType("offcast",$Spell::index[dimensionrift]);

SpellBook::AddSpellToType("defcast",$Spell::index[heal]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal1]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal2]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal3]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal4]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal5]);
SpellBook::AddSpellToType("defcast",$Spell::index[advheal6]);
SpellBook::AddSpellToType("defcast",$Spell::index[godlyheal]);
SpellBook::AddSpellToType("defcast",$Spell::index[fullheal]);
SpellBook::AddSpellToType("defcast",$Spell::index[massheal]);
SpellBook::AddSpellToType("defcast",$Spell::index[massfullheal]);
SpellBook::AddSpellToType("defcast",$Spell::index[shield]);
SpellBook::AddSpellToType("defcast",$Spell::index[advshield1]);
SpellBook::AddSpellToType("defcast",$Spell::index[advshield2]);
SpellBook::AddSpellToType("defcast",$Spell::index[advshield3]);
SpellBook::AddSpellToType("defcast",$Spell::index[advshield4]);
SpellBook::AddSpellToType("defcast",$Spell::index[advshield5]);
SpellBook::AddSpellToType("defcast",$Spell::index[massshield]);

SpellBook::AddSpellToType("neucast",$Spell::index[teleport]);
SpellBook::AddSpellToType("neucast",$Spell::index[transport]);
SpellBook::AddSpellToType("neucast",$Spell::index[advtransport]);
SpellBook::AddSpellToType("neucast",$Spell::index[remort]);
SpellBook::AddSpellToType("neucast",$Spell::index[mimic]);
SpellBook::AddSpellToType("neucast",$Spell::index[masstransport]);

function MenuViewSpellBook(%clientId, %page)
{
    if(%page == "")
        %page = 1;
	Client::buildMenu(%clientId, "Magic Types:", "ViewSpellBook", true);
    
    //%num = 0;
    //%activeGroups[0] = "";
    //for(%i = 0; %i <= $Belt::NumberOfBeltGroups; %i++)
    //{
    //    %group = $Belt::ItemGroup[%i];
    //    if(getWord(fetchData(%clientId,%group),0) != -1)
    //    {
    //        %activeGroups[%num] = %group;
    //        //echo(%group @ " " @ %num-1);
    //        %num++;
    //    }
    //}
    
    
    
    %num = $SpellType::NumSpellTypes;
    //echo(%num);
    %menuULB = BeltMenu::GetUpperLowerBounds(%num,%page);

    %numFullPages = getWord(%menuULB,0);
    %lb = getWord(%menuULB,1);
    %ub = getWord(%menuULB,2);
    //echo(%lb @ " > "@ %ub);
    %x = %lb - 1;
	for(%i = %lb; %i <= %ub; %i++)
	{
        //echo(%x);
        //%group = %activeGroups[%x]; //$Belt::ItemGroup[%i];
        //echo(%group);
        %group = $SpellType::IndexToLabel[%x];
        %disp = $SpellType[%group,Name]; //$Belt::ItemGroupShortName[%i];
        //echo(%disp);
        Client::addMenuItem(%clientId, %cnt++ @ %disp, %group);
        %x++;
            
    }
    
    if(%page == 1)
	{
		if(%num > 6) Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1);
		Client::addMenuItem(%clientId, "xDone", "done");
	}
	else if(%page == %numFullPages+1)
	{
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1);
		Client::addMenuItem(%clientId, "xDone", "done");
	}
	else
	{
		Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1);
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1);
	}
    return;
	//if(Belt::GetNS(%clientid,"RareItems") > 0)	Client::addMenuItem(%clientId, %cnt++ @ "Rares", "RareItems");
	//if(Belt::GetNS(%clientid,"GemItems") > 0)		Client::addMenuItem(%clientId, %cnt++ @ "Gems", "GemItems");
	//if(Belt::GetNS(%clientid,"KeyItems") > 0)		Client::addMenuItem(%clientId, %cnt++ @ "Keys", "KeyItems");
	//if(Belt::GetNS(%clientid,"LoreItems") > 0)	Client::addMenuItem(%clientId, %cnt++ @ "Lore", "LoreItems");
    //if(Belt::GetNS(%clientid,"AmmoItems") > 0)	Client::addMenuItem(%clientId, %cnt++ @ "Ammo", "AmmoItems");
    ////if(Belt::GetNS(%clientid,"GearItems") > 0)
    //    Client::addMenuItem(%clientId, %cnt++ @ "Equip", "EquipItems");
	////Client::addMenuItem(%clientId, %cnt++ @ "Smithing Book", "smithbook");
	//Client::addMenuItem(%clientId, "xDone", "done");
	//return;
}

function processMenuViewSpellBook(%clientId, %opt)
{
    %option = getWord(%opt,0);
    
    if(%option == "page")
    {
        %pageNum = getWord(%opt,1);
        MenuViewSpellBook(%clientId,%pageNum);
    }
    else if(%option != "done")
    {
        MenuViewSpellList(%clientId,%option,1);
    }
}

function MenuViewSpellList(%clientId,%type,%page)
{
    if(%page == "")
        %page = 1;
	Client::buildMenu(%clientId, "Spells:", "ViewSpellList", true);
    
    %num = 0;
    %activeSpells[0] = "";
    for(%i = 0; %i < $SpellType[%type,SpellCount]; %i++)
    {
        %spellIndex = $SpellType[%type,Spells,%i];
        if(SkillCanUse(%clientId,$Spell::keyword[%spellIndex]))
        {
            %activeSpells[%num] = %spellIndex;
            //echo(%group @ " " @ %num-1);
            %num++;
        }
    }
    %num = %num;
    
    %menuULB = BeltMenu::GetUpperLowerBounds(%num,%page);
    
    
    %numFullPages = getWord(%menuULB,0);

    if(%num == %numFullPages*6)
        %numFullPages--; // If the page is full, don't add a Next option
    
    %lb = getWord(%menuULB,1);
    %ub = getWord(%menuULB,2);
    %x = %lb - 1;
	for(%i = %lb; %i <= %ub; %i++)
	{
        %spellIndex = %activeSpells[%x];
        %disp = $Spell::keyword[%spellIndex];
        //echo(%disp);
        Client::addMenuItem(%clientId, %cnt++ @ %disp, "spell "@ %spellIndex @" "@ %type @" "@ %page);
        %x++;
    }
    
    if(%page == 1)
	{
		if(%num > 6) Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1 @" "@ %type);
		Client::addMenuItem(%clientId, "xBack", "back");
	}
	else if(%page == %numFullPages+1)
	{
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1@" "@ %type);
		Client::addMenuItem(%clientId, "xBack", "back");
	}
	else
	{
		Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1@" "@ %type);
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1@" "@ %type);
	}
    return;
}

function processMenuViewSpellList(%clientId, %opt)
{
    %option = getWord(%opt,0);
    
    if(%option == "spell")
    {
        %index = getWord(%opt,1);
        %prevType = getWord(%opt,2);
        %prevPageNum = getWord(%opt,3);
        MenuSelectSpellInfo(%clientId,%index,%prevType,%prevPageNum);
    }
    else if(%option == "page")
    {
        %pageNum = getWord(%opt,1);
        %type = getWord(%opt,2);
        MenuViewSpellList(%clientId,%type,%pageNum);
    }
    else if(%option == "back")
    {
        MenuViewSpellBook(%clientId,1);
    }
}

function MenuSelectSpellInfo(%clientId,%spellIndex,%prevType,%prevPage)
{
    Client::buildMenu(%clientId, "Spell: "@ $Spell::name[%spellIndex], "SelectSpellInfo", true);
    
    Client::addMenuItem(%clientId, %cnt++ @ "Examine", "spelldata "@ %spellIndex @" "@ %prevType @" "@ %prevPage);
    Client::addMenuItem(%clientId, %cnt++ @ "Cast", "cast "@ %spellIndex @" "@ %prevType@" "@ %prevPage);
    Client::addMenuItem(%clientId, "xBack <<", "back "@ %prevType@" "@ %prevPage);
}

function processMenuSelectSpellInfo(%clientId,%opt)
{
    %option = getWord(%opt,0);
    
    if(%option == "spelldata")
    {
        %spellIndex = getWord(%opt,1);
        %prevType = getWord(%opt,2);
        %prevPage = getWord(%opt,3);
        
        %msg = WhatIs($Spell::keyword[%spellIndex]);
		bottomprint(%clientId, %msg, floor(String::len(%msg) / 15));
        MenuSelectSpellInfo(%clientId,%spellIndex,%prevType,%prevPage);
    }
    else if(%option == "cast")
    {
        %spellIndex = getWord(%opt,1);
        %prevType = getWord(%opt,2);
        %prevPage = getWord(%opt,3);
        
        remoteSay(%clientId, 0, "#cast "@ $Spell::keyword[%spellIndex]);
        
        MenuSelectSpellInfo(%clientId,%spellIndex,%prevType,%prevPage);
    }
    else if(%option == "back")
    {
        %prevType = getWord(%opt,1);
        %prevPage = getWord(%opt,2);
        echo("PrevType: "@%prevType);
        MenuViewSpellList(%clientId,%prevType,%prevPage);
    }
    
}