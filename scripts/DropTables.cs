//    $DropTable[%key,0,Item];
//    $DropTable[%key,0,Amount];
//    $DropTable[%key,0,AmountMax];
//    $DropTable[%key,0,Rate];
function ParseDropTable(%key,%str)
{
    for(%i = 0; (%dropstr = String::getWord(%str,"|",%i)) != "|"; %i++)
    {
        %item = getWord(%dropstr,0);
        %amtStr = getWord(%dropstr,1);
        %percent = getWord(%dropstr,2);
        
        $DropTable[%key,%i,Item] = %item;
        %max = String::getWord(%amtStr,",",1);
        if(%max == ",")
        {
            $DropTable[%key,%i,Amount] = %amtStr;
            $DropTable[%key,%i,AmountMax] = "";
        }
        else
        {
            $DropTable[%key,%i,Amount] = String::getWord(%amtStr,",",0);
            $DropTable[%key,%i,AmountMax] = %max;
        }
        
        $DropTable[%key,%i,Rate] = (%percent/100) * 1.000001;
    }
}

//Enemy Type Tables
//Goblins-Mines
ParseDropTable("Runt","Knife 1 10");
ParseDropTable("Thief","Knife 1 5|Sling 1 5|BlackStatue 1 10");
ParseDropTable("Wizard","Turquoise 1 5");
ParseDropTable("Raider","Pickaxe 1 10|BlackStatue 15|Granite 1 15");

//Gnolles-Mines
ParseDropTable("Pup","CrystalBluePotion 1 5|BluePotion 1 20|Ruby 1 0.5");
ParseDropTable("Shaman","EnergyShot 1 20|Club 1 10|Jade 1 20");
ParseDropTable("Scavenger","Club 1 5|Sapphire 1 0.5");
ParseDropTable("Hunter","Waraxe 1 10|Sling 1 10|Granite [1,3] 15");

//Orcs-Yolanda
ParseDropTable("Warlock","EnchantedStone 1 10");
ParseDropTable("Berserker","BroadSword 1 10|Opal [1,2] 10");
ParseDropTable("Ravager","BroadSword 1 10|BluePotion [1,3] 40|Opal [1,3] 20");
ParseDropTable("Slayer","BroadSword 1 10|ShortBow 1 10|Opal 1 15");
ParseDropTable("Oracle","BoneClub 1 10|EnchantedStone 1 10|Turquoise 1 5");

//Zombies-Crypt
ParseDropTable("Mauler","SpikedClub 1 10");
ParseDropTable("Thrasher","Longsword 1 10");

//Undead-Crypt
ParseDropTable("Skeleton","Waraxe 1 10");
ParseDropTable("Necromancer","Darktome 1 1.5");

//Undead-?
ParseDropTable("Spawn","Gladius 1 10");

//Elven
ParseDropTable("Protector","Shortsword 1 5");
ParseDropTable("Peacekeeper","Longsword 1 10|Shortbow 1 5");
ParseDropTable("Lord","LightCrossbow 1 5|SpikedClub 1 5");
ParseDropTable("Champion","Longsword 1 10|LightCrossbow 1 10");
ParseDropTable("Conjurer","Topaz 1 5");

//Travellers
ParseDropTable("Brigand","Longsword 1 10");
ParseDropTable("Marauder","Longsword 1 10");
ParseDropTable("Knight","Longsword 1 10|ShortBow 1 5|Longbow 1 5");
ParseDropTable("Paladin","Turquoise 1 5|Turquoise 1 12");

//Mino
ParseDropTable("Goliath","BattleAxe 1 2|warhammer 1 5|BroadSword 1 15");
ParseDropTable("Reaper","quarterstaff 1 5");

ParseDropTable("Ent","Strawberry [1,3] 5");

//Race Tables
ParseDropTable("Goblin","GoblinEar 1 5|GobbieBerry 1 8|Quartz [1,2] 25");
ParseDropTable("Gnoll","traitorsamulet 1 0.25");
ParseDropTable("Orc","Jade [1,2] 20|LongSword 1 0.5");
ParseDropTable("Zombie","Jade [1,2] 25");
ParseDropTable("Undead","Bonedust 1 15");
ParseDropTable("Minotaur","MeteorChunk 1 5");

//Zone Tables
ParseDropTable("Keldrin Mine","COINS [5,20] 15|Quartz [1,2] 15");
ParseDropTable("Stronghold Yolanda","COINS [15,30] 15|TitaniteShard 1 5|MorningStar 1 0.8");
ParseDropTable("Ancient Crypt","COINS [45,60] 15|Bonedust [1,2] 5");
ParseDropTable("Elven Outpost","COINS [45,60] 15|Jade [1,4] 10");
ParseDropTable("Traveller's Den","COINS [50,75] 15|turquoise [1,3] 5");
ParseDropTable("Minotaur's Lair","COINS [100,300] 25|gladius 1 5|Topaz [1,4] 10");

function DropTable::AddTableToPlayer(%clientId,%tableKey)
{
    storeData(%clientId,"DropTableList",%tableKey@",","strinc");
}

function DropTable::GenerateLootDrops(%clientId,%lootstr)
{
    %keyList = fetchData(%clientId,"DropTableList");
    for(%i = 0; (%key = String::getWord(%keyList,",",%i)) != ","; %i++)
    {
        for(%k = 0; $DropTable[%key,%k,Item] != ""; %k++)
        {
            %amt = DropTable::RollLoot(%key,%k);
            if(%amt != "")
                %lootstr = SetStuffString(%lootstr,$DropTable[%key,%k,Item],%amt);
        }
        
    }
    
    return %lootstr;
}

function DropTable::RollLoot(%key,%index)
{
    %p = getRandomMT();
    if(getRandomMT() >= (1 - $DropTable[%key,%index,Rate]))
    {
        if($DropTable[%key,%index,AmountMax] == "")
            %amt = $DropTable[%key,%index,Amount];
        else
            %amt = getIntRandomMT($DropTable[%key,%index,Amount],$DropTable[%key,%index,AmountMax]);
    }
    else
        %amt = "";
        
    return %amt;
}