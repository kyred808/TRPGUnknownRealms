$RPGItem::WeaponTypeMelee = 0;
$RPGItem::WeaponTypeRange = 1;
$RPGItem::WeaponTypePick = 2;
$RPGItem::WeaponTypeBotSpell = 3;

$RPGItem::AccessoryClass = "Accessory";
$RPGItem::WeaponClass = "Weapon";
$RPGItem::EquippedClass = "Equipped";
$RPGItem::AmmoClass = "Ammo";

$RPGItem::PlayerWeaponList = "WeaponItemInv";

$RPGItem::InvItemLists[0] = $RPGItem::PlayerWeaponList;
$RPGItem::InvItemLists[1] = "EquippedItemInv";
$RPGItem::InvItemLists[2] = "AccessoryItemInv";
$RPGItem::InvItemLists[3] = "PouchItemInv";
$RPGItem::InvItemLists[4] = "AmmoItemInv";
$RPGItem::InvItemLists[5] = "LoreItemInv";

$RPGItem::StorageItemLists[0] = "WeaponItemStorage";
$RPGItem::StorageItemLists[1] = "AccessoryItemStorage";
$RPGItem::StorageItemLists[2] = "PouchItemStorage";
$RPGItem::StorageItemLists[3] = "AmmoItemStorage";

function RPGItem::AddAccessoryEquipment(%label,%name,%class,%id,%datablk)
{
    RPGItem::AddItemDefinition(%label,%name,%class,%id,%datablk);
    RPGItem::AddItemDefinition(%label@"0",%name,$RPGItem::EquippedClass,%id+1,%datablk);
    RPGItem::pairEquipWithItem(%id,%id+1);
}

function AddItemHelper(%label,%name,%class,%id,%weight,%value,%datablk,%action)
{
    RPGItem::AddItemDefinition(%label,%name,%class,%id,%datablk,%action);
    $AccessoryVar[%label, $Weight] = %weight;
    $HardcodedItemCost[%label] = %value;
}

$GemAffix[-2] = "Cracked";
$GemAffix[-1] = "Rough";
$GemAffix[0] = "";
$GemAffix[1] = "Shiny";
$GemAffix[2] = "Pure";
$GemAffix[3] = "Pristine";
function GenerateGemAffix(%itemTag)
{
    if(OddsAre(4)) //5
    {
        %random = getIntRandomMT(-2,2);
        if(%random == 2 && OddsAre(3))
            %random = 3;
        if(%random != 0)
            return %itemTag @"_im"@%random;
    }
    return %itemTag;
}

$RangeWeaponFireVel[sling] = 60;
$RangeWeaponFireVel[shortbow] = 100;
$RangeWeaponFireVel[longbow] = 100;
$RangeWeaponFireVel[elvenbow] = 100;
$RangeWeaponFireVel[compositebow] = 100;
$RangeWeaponFireVel[lightcrossbow] = 100;
$RangeWeaponFireVel[heavycrossbow] = 100;
$RangeWeaponFireVel[repeatingcrossbow] = 100;
$RangeWeaponFireVel[aeoluswing] = 100;

$RangeWeaponFireVel[rshortbow] = 100;

RPGItem::AddItemClass("Weapon","Weapons","WeaponItemInv","WeaponItemStorage");
RPGItem::AddItemClass("Equipped","Equipped","EquippedItemInv","");
RPGItem::AddItemClass("Accessory","Accessories","AccessoryItemInv","AccessoryItemStorage");
RPGItem::AddItemClass("Rares","Rares","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Gems","Gems","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Ores","Ores","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Pouch","Pouch","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Potion","Potions","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Plants","Plants","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Ammo","Ammo","AmmoItemInv","AmmoItemStorage");
RPGItem::AddItemClass("Lore","Lore","LoreItemInv","");

//Weapons
RPGItem::AddWeapon("shiv","Shiv",0,$RPGItem::WeaponTypeMelee,DaggerShape);
RPGItem::AddWeapon("crudeaxe","Crude Axe",1,$RPGItem::WeaponTypeMelee,HatchetShape);
RPGItem::AddWeapon("hatchet","Hatchet",2,$RPGItem::WeaponTypeMelee,HatchetShape);
RPGItem::AddWeapon("broadsword","Broadsword",3,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("waraxe","War Axe",4,$RPGItem::WeaponTypeMelee,WarAxeShape);
RPGItem::AddWeapon("longsword","Longsword",5,$RPGItem::WeaponTypeMelee,LongswordShape);
RPGItem::AddWeapon("battleaxe","Battle Axe",6,$RPGItem::WeaponTypeMelee,BattleAxeShape);
RPGItem::AddWeapon("bastardsword","Bastard Sword",7,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("halberd","Halberd",8,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("claymore","Claymore",9,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("club","Club",10,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("spikedclub","Spiked Club",11,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("mace","Mace",12,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("hammerpick","Hammer Pick",13,$RPGItem::WeaponTypePick,PickAxeShape);
RPGItem::AddWeapon("warhammer","War Hammer",14,$RPGItem::WeaponTypeMelee,HammerShape);
RPGItem::AddWeapon("warmaul","War Maul",15,$RPGItem::WeaponTypeMelee,HammerShape);
RPGItem::AddWeapon("quarterstaff","Quarter Staff",16,$RPGItem::WeaponTypeMelee,QuarterStaffShape);
RPGItem::AddWeapon("longstaff","Long Staff",17,$RPGItem::WeaponTypeMelee,LongStaffShape);
RPGItem::AddWeapon("justicestaff","Justice Staff",18,$RPGItem::WeaponTypeMelee,LongStaffShape);
RPGItem::AddWeapon("knife","Knife",19,$RPGItem::WeaponTypeMelee,DaggerShape);
RPGItem::AddWeapon("dagger","Dagger",20,$RPGItem::WeaponTypeMelee,DaggerShape);
RPGItem::AddWeapon("shortsword","Short Sword",21,$RPGItem::WeaponTypeMelee,ShortswordShape);
RPGItem::AddWeapon("spear","Spear",22,$RPGItem::WeaponTypeMelee,SpearShape);
RPGItem::AddWeapon("gladius","Gladius",23,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("trident","Trident",24,$RPGItem::WeaponTypeMelee,TridentShape);
RPGItem::AddWeapon("rapier","Rapier",25,$RPGItem::WeaponTypeMelee,KatanaShape);
RPGItem::AddWeapon("awlpike","Awl Pike",26,$RPGItem::WeaponTypeMelee,SpearShape);
RPGItem::AddWeapon("pickaxe","PickAxe",27,$RPGItem::WeaponTypePick,PickAxeShape);

RPGItem::AddWeapon("sling","Sling",28,$RPGItem::WeaponTypeRange,CrossbowShape);
RPGItem::AddWeapon("shortbow","Short Bow",29,$RPGItem::WeaponTypeRange,LongBowShape);
RPGItem::AddWeapon("longbow","Long Bow",30,$RPGItem::WeaponTypeRange,LongBowShape);
RPGItem::AddWeapon("elvenbow","Elven Bow",31,$RPGItem::WeaponTypeRange,LongBowShape);
RPGItem::AddWeapon("compositebow","Composite Bow",32,$RPGItem::WeaponTypeRange,CompositeBowShape);
RPGItem::AddWeapon("lightcrossbow","Light Crossbow",33,$RPGItem::WeaponTypeRange,CrossbowShape);
RPGItem::AddWeapon("heavycrossbow","Heavy Crossbow",34,$RPGItem::WeaponTypeRange,CrossbowShape);
RPGItem::AddWeapon("repeatingcrossbow","Repeating Crossbow",35,$RPGItem::WeaponTypeRange,CrossbowShape);
RPGItem::AddWeapon("aeoluswing","Aeolus Wing",36,$RPGItem::WeaponTypeRange,CompositeBowShape);

RPGItem::AddWeapon("boneclub","Bone Club",37,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("spikedboneclub","Spiked Bone Club",38,$RPGItem::WeaponTypeMelee,MaceShape);

RPGItem::AddWeapon("rknife","Rusty Knife",39,$RPGItem::WeaponTypeMelee,DaggerShape);
RPGItem::AddWeapon("RClub","Cracked Club",40,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("rwaraxe","Rusty War Axe",41,$RPGItem::WeaponTypeMelee,WarAxeShape);
RPGItem::AddWeapon("RPickAxe","Rusty Pickaxe",42,$RPGItem::WeaponTypePick,PickAxeShape);
RPGItem::AddWeapon("CastingBlade","Casting Blade",43,$RPGItem::WeaponTypeBotSpell,DaggerShape);
RPGItem::AddWeapon("rlightcrossbow","Cracked Light Crossbow",44,$RPGItem::WeaponTypeMelee,CrossbowShape);
RPGItem::AddWeapon("TreeAtk","TreeAtk",45,$RPGItem::WeaponTypeMelee,TreeShapeItem);
RPGItem::AddWeapon("rshortbow","Cracked Short Bow",46,$RPGItem::WeaponTypeRange,LongBowShape);
RPGItem::AddWeapon("rbroadsword","Rusty Broadsword",47,$RPGItem::WeaponTypeMelee,SwordShape);
RPGItem::AddWeapon("rlongsword","Rusty Longsword",48,$RPGItem::WeaponTypeMelee,LongswordShape);
RPGItem::AddWeapon("rspikedclub","Cracked Spiked Club",49,$RPGItem::WeaponTypeMelee,MaceShape);

$SkillType[TreeAtk] = $SkillPiercing;
$AccessoryVar[TreeAtk, $AccessoryType] = $PolearmAccessoryType;
$AccessoryVar[TreeAtk, $SpecialVar] = "6 75";
$AccessoryVar[TreeAtk, $MiscInfo] = "A Treeatk";
$AccessoryVar[TreeAtk, $Weight] = 0.1;
$WeaponRange[TreeAtk] = $minRange + 1;
$WeaponDelay[TreeAtk] = 2;
$HardcodedItemCost[TreeAtk] = 0;

//RPGItem::AddWeapon("sling","Sling",39,$RPGItem::WeaponTypeMelee,DaggerShape);
//RPGItem::AddWeapon("sling","Sling",40,$RPGItem::WeaponTypeMelee,DaggerShape);
//RPGItem::AddWeapon("sling","Sling",41,$RPGItem::WeaponTypeMelee,DaggerShape);

//Id's by 2's because the equipped version is the 2nd item
RPGItem::AddAccessoryEquipment(PaddedArmor,"Padded Armor",$RPGItem::AccessoryClass,50,MiscLootShape);
RPGItem::AddAccessoryEquipment(LeatherArmor,"Leather Armor",$RPGItem::AccessoryClass,52,MiscLootShape);
RPGItem::AddAccessoryEquipment(StuddedLeather,"Studded Leather Armor",$RPGItem::AccessoryClass,54,MiscLootShape);
RPGItem::AddAccessoryEquipment(SpikedLeather,"Spiked Leather Armor",$RPGItem::AccessoryClass,56,MiscLootShape);
RPGItem::AddAccessoryEquipment(HideArmor,"Hide Armor",$RPGItem::AccessoryClass,58,MiscLootShape);
RPGItem::AddAccessoryEquipment(ScaleMail,"Scale Mail",$RPGItem::AccessoryClass,60,MiscLootShape);
RPGItem::AddAccessoryEquipment(BrigandineArmor,"Brigandine Armor",$RPGItem::AccessoryClass,62,MiscLootShape);
RPGItem::AddAccessoryEquipment(ChainMail,"Chain Mail",$RPGItem::AccessoryClass,64,MiscLootShape);
RPGItem::AddAccessoryEquipment(RingMail,"Ring Mail",$RPGItem::AccessoryClass,66,MiscLootShape);
RPGItem::AddAccessoryEquipment(SplintMail,"Splint Mail",$RPGItem::AccessoryClass,68,MiscLootShape);

RPGItem::AddAccessoryEquipment(BronzePlateMail,"Bronze Plate Mail",$RPGItem::AccessoryClass,70,MiscLootShape);
RPGItem::AddAccessoryEquipment(PlateMail,"Plate Mail",$RPGItem::AccessoryClass,72,MiscLootShape);
RPGItem::AddAccessoryEquipment(FieldPlateArmor,"Field Plate Armor",$RPGItem::AccessoryClass,74,MiscLootShape);
RPGItem::AddAccessoryEquipment(FullPlateArmor,"Full Plate Armor",$RPGItem::AccessoryClass,76,MiscLootShape);

RPGItem::AddAccessoryEquipment(ApprenticeRobe,"Apprentice Robe",$RPGItem::AccessoryClass,78,MiscLootShape);
RPGItem::AddAccessoryEquipment(LightRobe,"Light Robe",$RPGItem::AccessoryClass,80,MiscLootShape);
RPGItem::AddAccessoryEquipment(BloodRobe,"Blood Robe",$RPGItem::AccessoryClass,82,MiscLootShape);
RPGItem::AddAccessoryEquipment(AdvisorRobe,"Advisor Robe",$RPGItem::AccessoryClass,84,MiscLootShape);
RPGItem::AddAccessoryEquipment(RobeOfVenjance,"Robe of Venjance",$RPGItem::AccessoryClass,86,MiscLootShape);
RPGItem::AddAccessoryEquipment(PhensRobe,"Phen's Robe",$RPGItem::AccessoryClass,88,MiscLootShape);
RPGItem::AddAccessoryEquipment(QuestMasterRobe,"Quest Master Robe",$RPGItem::AccessoryClass,90,MiscLootShape);
RPGItem::AddAccessoryEquipment(FineRobe,"Fine Robe",$RPGItem::AccessoryClass,92,MiscLootShape);
RPGItem::AddAccessoryEquipment(ElvenRobe,"Elven Robe",$RPGItem::AccessoryClass,94,MiscLootShape);
RPGItem::AddAccessoryEquipment(DragonMail,"Dragon Mail",$RPGItem::AccessoryClass,96,MiscLootShape);
RPGItem::AddAccessoryEquipment(KeldrinArmor,"Keldrin Armor",$RPGItem::AccessoryClass,98,MiscLootShape);

$HardcodedItemCost[SmallRock] = 13;
AddItemHelper(BasicArrow,"Basic Arrow","Ammo",99,0.1,GenerateItemCost(BasicArrow),BowArrow);
AddItemHelper(SheafArrow,"Sheaf Arrow","Ammo",100,0.1,GenerateItemCost(SheafArrow),BowArrow);
AddItemHelper(BladedArrow,"Bladed Arrow","Ammo",101,0.1,GenerateItemCost(BladedArrow),BowArrow);
AddItemHelper(LightQuarrel,"Light Quarrel","Ammo",102,0.1,GenerateItemCost(LightQuarrel),CrossbowBolt);
AddItemHelper(HeavyQuarrel,"Heavy Quarrel","Ammo",103,0.1,GenerateItemCost(HeavyQuarrel),CrossbowBolt);
AddItemHelper(ShortQuarrel,"Short Quarrel","Ammo",104,0.1,GenerateItemCost(ShortQuarrel),CrossbowBolt);
AddItemHelper(StoneFeather,"Stone Feather","Ammo",105,0.1,GenerateItemCost(StoneFeather),BowArrow);
AddItemHelper(MetalFeather,"Metal Feather","Ammo",106,0.1,GenerateItemCost(MetalFeather),BowArrow);
AddItemHelper(Talon,"Talon","Ammo",107,0.1,GenerateItemCost(Talon),BowArrow);
AddItemHelper(CeraphumsFeather,"Ceraphums Feather","Ammo",108,0.1,GenerateItemCost(CeraphumsFeather),BowArrow);
AddItemHelper(SmallRock,"Small Rock","Ammo",109,0.1,GenerateItemCost(SmallRock),SmallRock);

$SkillType[SmallRock] = $SkillArchery;
$SkillType[BasicArrow] = $SkillArchery;
$SkillType[SheafArrow] = $SkillArchery;
$SkillType[BladedArrow] = $SkillArchery;
$SkillType[LightQuarrel] = $SkillArchery;
$SkillType[HeavyQuarrel] = $SkillArchery;
$SkillType[ShortQuarrel] = $SkillArchery;
$SkillType[CastingBlade] = $SkillPiercing;
$SkillType[KeldriniteLS] = $SkillSlashing;
$SkillType[AeolusWing] = $SkillArchery;
$SkillType[StoneFeather] = $SkillArchery;
$SkillType[MetalFeather] = $SkillArchery;
$SkillType[Talon] = $SkillArchery;
$SkillType[CeraphumsFeather] = $SkillArchery;


AddItemHelper("bluepotion","Blue Potion","Potion",120,4,15,PotionShape,"DrinkHealingPotion 15");
AddItemHelper(CrystalBluePotion,"Crystal Blue Potion","Potion",121,10,100,PotionShape,"DrinkHealingPotion 60");
AddItemHelper(EnergyPotion,"Energy Potion","Potion",122,4,15,PotionShape,"DrinkManaPotion 16");
AddItemHelper(CrystalEnergyPotion,"Crystal Energy Potion","Potion",123,10,100,PotionShape,"DrinkManaPotion 50");

$AccessoryVar[bluepotion, $Weight] = 4;
$AccessoryVar[CrystalBluePotion, $Weight] = 10;
$AccessoryVar[EnergyPotion, $Weight] = 4;
$AccessoryVar[CrystalEnergyPotion, $Weight] = 10;

//BeltItem::Add("Red Potion","RedPotion","PotionItems",4,12000,50,"DrinkRedPotion 30");
//
//// Crafted and not sold in stores.
//BeltItem::Add("Energy Shot","EnergyShot","PotionItems",0.2,50,"","DrinkStaminaPotion 15");
//BeltItem::Add("Energy Vial","EnergyVial","PotionItems",0.5,250,"","DrinkStaminaPotion 25");
//BeltItem::Add("Crystal Energy Vial","CrystalEnergyVial","PotionItems",1,1500,"","DrinkStaminaPotion 50");
//BeltItem::Add("Energized Potion","EnergizedPotion","PotionItems",1,5000,"","DrinkStaminaPotion 100");

$AccessoryVar[BluePotion, $MiscInfo] = "A blue potion that heals 15 HP";
$AccessoryVar[CrystalBluePotion, $MiscInfo] = "A crystal blue potion that heals 60 HP";


$AccessoryVar[EnergyPotion, $MiscInfo] = "An energy potion that provides 16 MP";
$AccessoryVar[CrystalEnergyPotion, $MiscInfo] = "A crystal energy potion that provides 50 MP";
$AccessoryVar[EnergyShot, $MiscInfo] = "A very small vial of potion that restores 5 MP. Not much, but very light weight."; 
$AccessoryVar[EnergyVial, $MiscInfo] = "A small energy vial restores 15 MP. Doesn't seem like much, but very light weight.";
$AccessoryVar[CrystalEnergyVial, $MiscInfo] = "A small energy vial that restores 50 MP. Similar to a Crystal Energy Potion, but lighter.";
$AccessoryVar[EnergizedPotion, $MiscInfo] = "An energy potion that restores 100 MP.";


AddItemHelper(Grain,"Grain","Plants",124,0.2,8,MiscLootShape);
AddItemHelper(GobbieBerry,"Gobbie Berry","Plants",125,0.2,30,MiscLootShape);
AddItemHelper("Yuccavera","Yuccavera","Plants",126,0.2,100,MiscLootShape);
AddItemHelper("RedBerry","Red Berry","Plants",127,0.2,30,MiscLootShape);
AddItemHelper("TreeFruit","Tree Fruit","Plants",128,0.5,1,MiscLootShape);
AddItemHelper("Strawberry","Strawberry","Plants",129,0.3,250,MiscLootShape);
AddItemHelper("Nixphyllum","Nixphyllum","Plants",130,0.3,300,MiscLootShape);
AddItemHelper("DeezNuts","Deez Nuts","Plants",131,0.3,1500,MiscLootShape);
AddItemHelper("Lunabrosia","Lunabrosia","Plants",132,0.3,3000,MiscLootShape);

AddItemHelper("quartz","Quartz","Gems",133,0.2,250,MiscLootShape);
AddItemHelper("granite","Granite","Gems",134,0.2,480,MiscLootShape);
AddItemHelper("opal","Opal","Gems",135,0.2,600,MiscLootShape);
AddItemHelper("jade","Jade","Gems",136,0.25,750,MiscLootShape);
AddItemHelper("turquoise","Turquoise","Gems",137,0.3,1350,MiscLootShape);
AddItemHelper("ruby","Ruby","Gems",138,0.3,1600,MiscLootShape);
AddItemHelper("topaz","Topaz","Gems",139,0.3,3004,MiscLootShape);
AddItemHelper("sapphire","Sapphire","Gems",140,0.3,4930,MiscLootShape);
AddItemHelper("gold","Gold","Gems",141,3.5,9680,MiscLootShape);
AddItemHelper("emerald","Emerald","Gems",142,0.2,15702,MiscLootShape);
AddItemHelper("diamond","Diamond","Gems",143,0.1,28575,MiscLootShape);
AddItemHelper("keldrinite","Keldrinite","Gems",144,5.0,225200,MiscLootShape);

%f = 43;
$ItemList[Mining, 1] = "SmallRock " @ round($HardcodedItemCost[SmallRock] / %f)+2;
$ItemList[Mining, 2] = "Quartz " @ round($HardcodedItemCost[Quartz] / %f)+2;
$ItemList[Mining, 3] = "Granite " @ round($HardcodedItemCost[Granite] / %f)+2;
$ItemList[Mining, 4] = "Opal " @ round($HardcodedItemCost[Opal] / %f)+2;
$ItemList[Mining, 5] = "Jade " @ round($HardcodedItemCost[Jade] / %f)+2;
$ItemList[Mining, 6] = "Turquoise " @ round($HardcodedItemCost[Turquoise] / %f)+2;
$ItemList[Mining, 7] = "Ruby " @ round($HardcodedItemCost[Ruby] / %f)+2;
$ItemList[Mining, 8] = "Topaz " @ round($HardcodedItemCost[Topaz] / %f)+2;
$ItemList[Mining, 9] = "Sapphire " @ round($HardcodedItemCost[Sapphire] / %f)+2;
$ItemList[Mining, 10] = "Gold " @ round($HardcodedItemCost[Gold] / %f)+2;
$ItemList[Mining, 11] = "Emerald " @ round($HardcodedItemCost[Emerald] / %f)+2;
$ItemList[Mining, 12] = "Diamond " @ round($HardcodedItemCost[Diamond] / %f)+2;
$ItemList[Mining, 13] = "Keldrinite " @ round($HardcodedItemCost[Keldrinite] / %f)+2;


AddItemHelper("copperore","Copper Ore","Ores",145,1,250,MiscLootShape);
AddItemHelper("tinore","Tin Ore","Ores",146,1,250,MiscLootShape);
AddItemHelper("galena","Galena","Ores",147,1,250,MiscLootShape);
AddItemHelper("coal","Coal","Ores",148,1,3000,MiscLootShape);
AddItemHelper("ironore","Iron Ore","Ores",149,1,3000,MiscLootShape);
AddItemHelper("cobaltore","Cobalt Ore","Ores",150,1,3000,MiscLootShape);
AddItemHelper("mithrite","Mithrite","Ores",151,1,3000,MiscLootShape);
AddItemHelper("adamantite","Adamantite","Ores",152,1,3000,MiscLootShape);
AddItemHelper("meteorchunk","Meteor Chunk","Ores",153,1,5000,MiscLootShape);
AddItemHelper("meteorcore","Meteor Core","Ores",154,1,25042,MiscLootShape);

$RPGItem::ItemDef[153,Action] = "RestoreMana 10";
$RPGItem::ItemDef[154,Action] = "RestoreMana 50";

AddItemHelper("BlackStatue","Black Statue","Rares",155,3,1,MiscLootShape);
AddItemHelper("GoblinEar","Goblin Ear","Rares",156,0.2,6200,MiscLootShape);
AddItemHelper("DarkTome","Dark Tome","Rares",157,3,250000,MiscLootShape);
AddItemHelper("BoneDust","Bone Dust","Rares",158,0.2,9000,MiscLootShape);
AddItemHelper("SkeletonBone","Skeleton Bone","Rares",159,1,1,MiscLootShape);
AddItemHelper("EnchantedStone","Enchanted Stone","Rares",160,2,1,MiscLootShape);
AddItemHelper("DragonScale","Dragon Scale","Rares",161,8,245310,MiscLootShape);
AddItemHelper("Magicite","Magicite","Rares",162,0.2,550000,MiscLootShape);

$AccessoryVar[blackstatue, $MiscInfo] = "A strange black statue.";
$AccessoryVar[skeletonbone, $MiscInfo] = "A bone from an old skeleton.";
$AccessoryVar[EnchantedStone, $MiscInfo] = "A weird glowing stone.";
$AccessoryVar[DragonScale, $MiscInfo] = "A dragon scale.";

$AccessoryVar[goblinear, $MiscInfo] = "Ear of a goblin.";
$AccessoryVar[DarkTome, $MiscInfo] = "A very rare spell book. Used in the crafting of rare items.";
$AccessoryVar[BoneDust, $MiscInfo] = "Rare dust of a reanimated skeleton.";
$AccessoryVar[Magicite, $MiscInfo] = "A crystal filled with powerful magical energy.";


AddItemHelper("parchment","Parchment","Lore",163,0.2,1,MiscLootShape);
AddItemHelper("magicdust","Magic Dust","Lore",164,0.2,1,MiscLootShape);

$ItemList[Lore, 1] = "parchment";
$ItemList[Lore, 2] = "magicdust";
$LoreItem[Parchment] = True;
$LoreItem[MagicDust] = True;
$AccessoryVar[Parchment, $MiscInfo] = "A parchment";
$AccessoryVar[MagicDust, $MiscInfo] = "A small bag containing magic dust";

AddItemHelper(Tent,"Tent","Pouch",165,40,4000,MiscLootShape);
$AccessoryVar[Tent, $Weight] = 40;
$AccessoryVar[Tent, $MiscInfo] = "A tent. Use #camp to set it up, and #uncamp to disassemble it.";
AddItemHelper(Anvil,"Anvil","Pouch",166,20,5000,MiscLootShape);
$AccessoryVar[AnvilItem, $MiscInfo] = "An anvil for smthing and smelting. Use #anvil to setup and #unanvil to pickup.";


RPGItem::AddAccessoryEquipment(CheetaursPaws,"Cheetaur's Paws",$RPGItem::AccessoryClass,168,MiscLootShape);
RPGItem::AddAccessoryEquipment(BootsOfGliding,"Boots of Gliding",$RPGItem::AccessoryClass,170,MiscLootShape);
RPGItem::AddAccessoryEquipment(WindWalkers,"Wind Walkers",$RPGItem::AccessoryClass,172,MiscLootShape);

RPGItem::AddAccessoryEquipment(KnightShield,"Knight Shield",$RPGItem::AccessoryClass,174,KnightShield);

RPGItem::AddAccessoryEquipment(BandedMail,"Banded Mail",$RPGItem::AccessoryClass,176,MiscLootShape);

//WIP
RPGItem::AddAccessoryEquipment(OrbOfLuminance,"Orb of Luminance",$RPGItem::AccessoryClass,178,MiscLootShape);
RPGItem::AddAccessoryEquipment(OrbOfBreath,"Orb of Breath",$RPGItem::AccessoryClass,180,MiscLootShape);

RPGItem::AddWeapon("batteeth","BatTeeth",182,$RPGItem::WeaponTypeMelee,InvisShape);
$SkillType[batteeth] = $SkillPiercing;
$AccessoryVar[batteeth, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[batteeth, $SpecialVar] = "6 10";
$AccessoryVar[batteeth, $MiscInfo] = "Teeth of a bat.";
$AccessoryVar[batteeth, $Weight] = 0.1;
$WeaponRange[batteeth] = $minRange + 1;
$WeaponDelay[batteeth] = 2;
$HardcodedItemCost[batteeth] = 0;
$StealProtectedItem[batteeth] = true;

function AddRingItemHelper(%label,%name,%id,%datablk,%weight,%cost,%special)
{
    RPGItem::AddAccessoryEquipment(%label,%name,$RPGItem::AccessoryClass,%id,%datablk);
    $AccessoryVar[%label, $AccessoryType] = $RingAccessoryType;
    $AccessoryVar[%label, $Weight] = %weight;
    $HardcodedItemCost[%label] = %cost;
    $AccessoryVar[%label, $SpecialVar] = %special;
}

function AddArmAccessoryHelper(%label,%name,%id,%datablk,%weight,%cost,%special)
{
    RPGItem::AddAccessoryEquipment(%label,%name,$RPGItem::AccessoryClass,%id,%datablk);
    $AccessoryVar[%label, $AccessoryType] = $ArmAccessoryType;
    $AccessoryVar[%label, $Weight] = %weight;
    $HardcodedItemCost[%label] = %cost;
    $AccessoryVar[%label, $SpecialVar] = %special;
}

function AddNeckAccessoryHelper(%label,%name,%id,%datablk,%weight,%cost,%special)
{
    RPGItem::AddAccessoryEquipment(%label,%name,$RPGItem::AccessoryClass,%id,%datablk);
    $AccessoryVar[%label, $AccessoryType] = $TalismanAccessoryType;
    $AccessoryVar[%label, $Weight] = %weight;
    $HardcodedItemCost[%label] = %cost;
    $AccessoryVar[%label, $SpecialVar] = %special;
}

// Rings
AddRingItemHelper(ringofharm,"Ring of Harm",200,MiscLootShape,0.2,10000,$SpecialVarATK@" 10");
AddRingItemHelper(ringofdefense,"Ring of Defense",202,MiscLootShape,0.2,8000,$SpecialVarDEF@" 50");
AddRingItemHelper(ringofburden,"Ring of Burden",204,MiscLootShape,0.2,12000,$SkillWeightCapacity@" 25");
AddRingItemHelper(bladering,"Blade Ring",206,MiscLootShape,0.2,15000,"SKILL"@$SkillSlashing@" 20");
AddRingItemHelper(sharpring,"Sharp Ring",208,MiscLootShape,0.2,15000,"SKILL"@$SkillPiercing@" 20");
AddRingItemHelper(bluntring,"Blunt Ring",210,MiscLootShape,0.2,15000,"SKILL"@$SkillBludgeoning@" 20");
AddRingItemHelper(bashring,"Bash Ring",212,MiscLootShape,0.2,15000,"SKILL"@$SkillBashing@" 20");
AddRingItemHelper(backstabring,"Backstab Ring",214,MiscLootShape,0.2,15000,"SKILL"@$SkillBackstabbing@" 20");
AddRingItemHelper(hidering,"Hide Ring",216,MiscLootShape,0.2,15000,"SKILL"@$SkillHiding@" 20");
AddRingItemHelper(wizardring,"Wizard Ring",218,MiscLootShape,0.2,15000,"SKILL"@$SkillOffensiveCasting@" 25");
AddRingItemHelper(healerring,"Healer Ring",220,MiscLootShape,0.2,15000,"SKILL"@$SkillDefensiveCasting@" 25");
AddRingItemHelper(naturering,"Nature Ring",222,MiscLootShape,0.2,15000,"SKILL"@$SkillNatureCasting@" 25");
AddRingItemHelper(harvestring,"Harvest Ring",224,MiscLootShape,0.2,15000,"SKILL"@$SkillFarming@" 20");
AddRingItemHelper(archeryring,"Archery Ring",226,MiscLootShape,0.2,15000,"SKILL"@$SkillArchery@" 20");
AddRingItemHelper(ringofrest,"Ring of Rest",228,MiscLootShape,0.2,8000,$SpecialVarRestStamRegen@" 0.8");
AddRingItemHelper(ringofidle,"Ring of Idle",230,MiscLootShape,0.2,8000,$SpecialVarIdleStamRegen@" 0.2");
AddRingItemHelper(manaring,"Mana Ring",232,MiscLootShape,0.2,25000,$SpecialVarMana@" 50");

$AccessoryVar[ringofharm, $MiscInfo] = "Ring that gives <f0>+10 ATK";
$AccessoryVar[ringofdefense, $MiscInfo] = "Ring that gives <f0>+50 DEF";
$AccessoryVar[ringofburden, $MiscInfo] = "Ring that gives <f0>+25 Weight Capacity";
$AccessoryVar[bladering, $MiscInfo] = "Ring that gives <f0>+20 Slashing";
$AccessoryVar[sharpring, $MiscInfo] = "Ring that gives <f0>+20 Piercing";
$AccessoryVar[bluntring, $MiscInfo] = "Ring that gives <f0>+20 Bludgeoning";
$AccessoryVar[bashring, $MiscInfo] = "Ring that gives <f0>+20 Bashing";
$AccessoryVar[backstabring, $MiscInfo] = "Ring that gives <f0>+20 Backstabbing";
$AccessoryVar[hidering, $MiscInfo] = "Ring that gives <f0>+20 Hiding";
$AccessoryVar[wizardring, $MiscInfo] = "Ring that gives <f0>+25 Offensive Casting";
$AccessoryVar[healerring, $MiscInfo] = "Ring that gives <f0>+25 Defensive Casting";
$AccessoryVar[naturering, $MiscInfo] = "Ring that gives <f0>+25 Nature Casting";
$AccessoryVar[harvestring, $MiscInfo] = "Ring that gives <f0>+25 Farming";
$AccessoryVar[ringofrest, $MiscInfo] = "Ring that gives <f0>+0.8 <f1>stamina regen when resting.";
$AccessoryVar[ringofidle, $MiscInfo] = "Ring that gives <f0>+0.2 stamina regen idle (no moving or attacking).";
$AccessoryVar[archeryring, $MiscInfo] = "Ring that gives <f0>+20 Archery";
$AccessoryVar[manaring, $MiscInfo] = "Ring that gives <f0>+50 Max Mana";

AddArmAccessoryHelper(soldierband,"Soldier Band",234,MiscLootShape,0.2,150000,"SKILL"@$SkillSlashing@" 100 SKILL"@$SkillPiercing@" 100 SKILL"@ $SkillBludgeoning @" 100");
AddArmAccessoryHelper(exileband,"Exile Band",236,MiscLootShape,0.2,150000,"SKILL"@$SkillNatureCasting@" 180");
AddArmAccessoryHelper(liftingband,"Lifting Band",238,MiscLootShape,0.2,25000,$SkillWeightCapacity@" 150");
AddArmAccessoryHelper(bashersbangle,"Basher's Bangle",240,MiscLootShape,0.2,250000,"SKILL"@ $SkillBludgeoning @" 20 SKILL"@$SkillBashing@" 100");

$AccessoryVar[soldierband, $MiscInfo] = "A clasp that raises all your melee weapon skills by 100";
$AccessoryVar[exileband, $MiscInfo] = "An armband that raises your Nature Casting 180";
$AccessoryVar[liftingband, $MiscInfo] = "An armband that raises your max weight by 150";
$AccessoryVar[bashersbangle, $MiscInfo] = "Bangle that increashing your Bludgeoning by 20 and Bashing by 100";


AddItemHelper("titaniteshard","Titanite Shard","Ores",242,0.3,1500,MiscLootShape);

$AccessoryVar[titaniteshard, $MiscInfo] = "A shard of titanite, used for weapon upgrading";

AddItemHelper("lowqscrap","Low Quality Scrap","Pouch",243,0.1,0,MiscLootShape);
$AccessoryVar[lowqscrap, $MiscInfo] = "Low quality scrap";

AddItemHelper(EnergyShot,"Energy Shot","Potion",250,0.2,50,PotionShape,"DrinkManaPotion 5");
AddItemHelper(EnergyVial,"Energy Vial","Potion",251,0.5,250,PotionShape,"DrinkManaPotion 15");


AddItemHelper(Bread,"Bread","Pouch",252,0.5,1200,MiscLootShape,"EatFoodItem,cooldown 60,StamRegen 0.2 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
AddItemHelper(EarBread,"Ear Bread?","Pouch",253,0.5,25000,MiscLootShape,"EatFoodItem,cooldown 80,StamRegen 0.3 80,HPRegen "@0.45/$TribesDamageToNumericDamage@" 80,ATK 10 80");
AddItemHelper(GobCookie,"Gob Cookie","Pouch",254,0.5,1200,MiscLootShape,"EatFoodItem,cooldown 60,StamRegen 0.5 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
AddItemHelper(YucJuice,"Yuc Juice","Pouch",255,0.5,7500,MiscLootShape,"EatFoodItem,cooldown 100,StamRegen 0.4 100,HPRegen "@0.5/$TribesDamageToNumericDamage@" 100");
AddItemHelper(RedBerryPie,"Red Berry Pie","Pouch",256,0.5,12500,MiscLootShape);
AddItemHelper(StrawberryCake,"Strawberry Cake","Pouch",257,0.5,25000,MiscLootShape);

// Food Items
//BeltItem::Add("Bread","Bread","FoodItems",0.5,1200,"","EatFoodItem,cooldown 60,StamRegen 0.2 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
//BeltItem::Add("Ear Bread?","EarBread","FoodItems",0.5,25000,750,"EatFoodItem,cooldown 80,StamRegen 0.3 80,HPRegen "@0.45/$TribesDamageToNumericDamage@" 80,ATK 10 80");
//BeltItem::Add("Gob Cookie","GobCookie","FoodItems",0.5,1200,751,"EatFoodItem,cooldown 60,StamRegen 0.5 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
//BeltItem::Add("YucJuice","YucJuice","FoodItems",0.5,7500,752,"EatFoodItem,cooldown 100,StamRegen 0.4 100,HPRegen "@0.5/$TribesDamageToNumericDamage@" 100");
//BeltItem::Add("Red Berry Pie","RedBerryPie","FoodItems",0.5,12500,753);
//BeltItem::Add("Strawberry Cake","StrawberryCake","FoodItems",0.5,25000,754);

$AccessoryVar[Bread, $MiscInfo] = "A loaf of bread.  Eating it will boost health and stamina regen slightly.";
$AccessoryVar[EarBread, $MiscInfo] = "Is this edible? Boosts health and stam regen and raises ATK by 10";
$AccessoryVar[GobCookie, $MiscInfo] = "A cookie made of Gobbie Berries.  Eating it will boost stamina regen";
$AccessoryVar[YucJuice, $MiscInfo] = "A flask of healthy yuccavera juice.  Eating it will boost health and stamina regen."; 
$AccessoryVar[RedBerryPie, $MiscInfo] = "Delicious red berry pie. Eating it will boost stamina regen.";
$AccessoryVar[StrawberryCake, $MiscInfo] = "A cake slices of strawberries.  Greatly boosts stamina regen.";


RPGItem::AddWeapon(morningstar,"Morning Star",280,$RPGItem::WeaponTypeMelee,MaceShape);
$AccessoryVar[morningstar, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[morningstar, $SpecialVar] = "6 95";
$AccessoryVar[morningstar, $Weight] = 6;
$AccessoryVar[morningstar, $MiscInfo] = "The Morning Star is a rare club that cannot be found in shops";
$SkillType[morningstar] = $SkillBludgeoning;
$ItemCost[morningstar] = GenerateItemCost(morningstar)+2500; //Small rare boost
$SkillRestriction[morningstar] = $SkillBludgeoning @ " 250";

AddNeckAccessoryHelper(traitorsamulet,"Traitor's Amulet",281,MiscLootShape,0.2,150000,"SKILL"@$SkillHiding@" 100 SKILL"@$SkillBackstabbing@" 100");
$AccessoryVar[traitorsamulet, $MiscInfo] = "A rare amulet that raises Hiding and Backstabbing by 100";

//Next item starts at 283

function RPGItem::DoUseAction(%clientId,%itemTag,%action)
{
    %type = getWord(%action,0);
    if(%type == -1)
        return; //Invalid action
        
    if(%type == "DrinkHealingPotion")
    {
        %baseAmt = getWord(%action,1);
        NewDrinkHealingPotion(%clientId,RPGItem::getItemNameFromTag(%itemTag),%baseAmt);
        RPGItem::decItemCount(%clientId,%itemTag,1);
        RefreshAll(%clientId,false);
        return true;
    }
    else if(%type == "DrinkManaPotion")
    {
        %baseAmt = getWord(%action,1);
        NewDrinkManaPotion(%clientId,RPGItem::getItemNameFromTag(%itemTag),%baseAmt);
        RPGItem::decItemCount(%clientId,%itemTag,1);
        RefreshAll(%clientId,false);
        return true;
    }
    else if(%type == "RestoreMana")
    {
        %baseAmt = getWord(%action,1);
        RestoreMana(%clientId,%baseAmt,"crushed a "@ RPGItem::getItemNameFromTag(%itemTag));
        RPGItem::decItemCount(%clientId,%itemTag,1);
        return true;
    }
    else if(String::getWord(%action,",",0) == "EatFoodItem")
    {
        if(AddBonusStatePoints(%clientId,"FoodCooldown") == 0)
        {
            if(%clientId.sleepMode == "")
            {
                EatFoodItem(%clientId,RPGItem::getItemNameFromTag(%itemTag),Word::getSubWord(%action,1,999,","));
                RPGItem::decItemCount(%clientId,%itemTag,1);
                RefreshAll(%clientId,false);
            }
            else
                Client::sendMessage(%clientId, $MsgWhite, "You can't eat right now.");
        }
        else
            Client::sendMessage(%clientId, $MsgWhite, "You aren't ready to eat again.");
            
        return true;
    }
}

function EatFoodItem(%clientId,%item,%special)
{
    //"EatFoodItem,Cooldown 80,StamRegen 0.1 60,HPRegen 0.1 60"
    %cooldown = "";
    
    for(%i = 0; String::getWord(%special,",",%i) != ","; %i++)
    {
        %data = String::getWord(%special,",",%i);
        //echo(%data);
        if(String::icompare(getWord(%data,0),"cooldown") == 0)
            %cooldown = getWord(%data,1);
        else
        {
            %bonusType = getWord(%data,0);
            //echo(%bonusType);
            %bonusAmnt = getWord(%data,1);
            %bonusTicks = getWord(%data,2);
            UpdateBonusState(%clientId, %bonusType @" "@%bonusAmnt, %bonusTicks);
        }
    }
    refreshHPREGEN(%clientId);
    //refreshStaminaREGEN(%clientId);
    refreshMANAREGEN(%clientId);
    UpdateBonusState(%clientId, "FoodCooldown 1", %cooldown);
    Client::sendMessage(%clientId, $MsgWhite, "You ate a "@%item@".");
}

function NewDrinkHealingPotion(%clientId,%itemName,%amt)
{
    %hp = fetchData(%clientId, "HP");
    refreshHP(%clientId,%amt * -0.01);
    if(fetchData(%clientId,"HP") != %hp)
        UseSkill(%clientId, $SkillHealing, True, True);
    Client::sendMessage(%clientId, $MsgWhite, "You drank a "@%itemName@" and recovered "@ %amt @"HP~wActivateAR.wav");
}

function NewDrinkManaPotion(%clientId,%itemName,%amt)
{
    %stam = fetchData(%clientId,"MANA");
    //refreshStamina(%clientId,%amt*-1);
    refreshMANA(%clientId,%amt*-1);
    if(fetchData(%clientId,"MANA") != %stam)
        UseSkill(%clientId, $SkillEnergy, True, True);
    Client::sendMessage(%clientId, $MsgWhite, "You drank a "@%itemName@" and recovered "@ %amt @" MP~wActivateAR.wav");

}

function RestoreMana(%clientId,%amt,%desc)
{
    refreshMana(%clientId,%amt*-1);
    Client::sendMessage(%clientId, $MsgWhite, "You "@%desc@" and recovered "@ %amt @" Mana~wActivateAR.wav");
}