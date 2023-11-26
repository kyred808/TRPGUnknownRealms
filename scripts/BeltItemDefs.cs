// Breaking out belt item definitions from belt.cs
// That way, belt.cs and be exec'ed during runtime
// for debugging without messing up belt item defitions


// =============================
// Clean-up
// =============================
// If this file gets executed again while the server is running
// it will mess up the belt menu operation. I'm not sure if wiping
// the globals out is any more stable, but it will do much less damage
// and protect save files.

deleteVariables("Belt::*");
deleteVariables("beltitem*");

// =============================
// End Clean-up
// =============================



// =============================
// Belt Groups
// =============================
// (InternalName,DisplayName,ID)

// IDs must have contiguous (no gaps) or they will not process right
$Belt::NumberOfBeltGroups = 0;
BeltItem::AddBeltItemGroup("RareItems","Rares",0);
BeltItem::AddBeltItemGroup("KeyItems","Keys",1);
BeltItem::AddBeltItemGroup("GemItems","Gems",2);
BeltItem::AddBeltItemGroup("LoreItems","Lore",3);
BeltItem::AddBeltItemGroup("AmmoItems","Ammo",4);
BeltItem::AddBeltItemGroup("EquipItems","Equip",5);
BeltItem::AddBeltItemGroup("PotionItems","Potions",6);
BeltItem::AddBeltItemGroup("OreItems","Ores",7);
BeltItem::AddBeltItemGroup("MetalItems","Metals",8);
BeltItem::AddBeltItemGroup("WoodItems","Wood",9);
BeltItem::AddBeltItemGroup("PlantItems","Plants",10);
BeltItem::AddBeltItemGroup("FoodItems","Food",11);
BeltItem::AddBeltItemGroup("ScrollItems","Spell Scrolls",12);
// =============================
// End Belt Groups
// =============================

// =============================
// Belt Equipment Slots and Types
// =============================
$BeltEquip::Type[0] = "finger";
$BeltEquip::Type[1] = "arm";
$BeltEquip::Type[2] = "neck";

$BeltEquip::NumberOfSlots = 0;

BeltEquip::AddEquipmentSlot("finger1","Ring 1",$BeltEquip::Type[0],0);
BeltEquip::AddEquipmentSlot("finger2","Ring 2",$BeltEquip::Type[0],1);
BeltEquip::AddEquipmentSlot("arm","Arm",$BeltEquip::Type[1],2);
BeltEquip::AddEquipmentSlot("neck","Necklace",$BeltEquip::Type[2],3);

// =============================
// End Belt Equipment Slots and Types
// =============================



// =============================
// Belt Items
// =============================
// (DisplayName,InternalName,BeltItemGroupInternalName,Weight,Price,ShopIndex)

// Shop index is only necessary if you want the item to be buyable at shops
// They are references in the Townbot section of the .mis file similar to normal shop items
// instant SimGroup "BELTSHOP 1 2 3 4 5 8 10 11";

// Gem Items

BeltItem::Add("Quartz","quartz","GemItems",0.2,250,1);
BeltItem::Add("Granite","granite","GemItems",0.2,480,2);
BeltItem::Add("Opal","opal","GemItems",0.2,600,3);
BeltItem::Add("Jade","jade","GemItems",0.25,750,4);
BeltItem::Add("Turquoise","turquoise","GemItems",0.3,1350,5);
BeltItem::Add("Ruby","ruby","GemItems",0.3,1600,6);
BeltItem::Add("Topaz","topaz","GemItems",0.3,3004,7);
BeltItem::Add("Sapphire","sapphire","GemItems",0.3,4930,8);
BeltItem::Add("Gold","gold","GemItems",3.5,9680,9);
BeltItem::Add("Emerald","emerald","GemItems",0.2,15702,10);
BeltItem::Add("Diamond","diamond","GemItems",0.1,28575,11);
BeltItem::Add("Keldrinite","keldrinite","GemItems",5.0,225200,12);

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

// Ore Items

BeltItem::Add("Copper Ore","copperore","OreItems",1,250,200);
BeltItem::Add("Tin Ore","tinore","OreItems",1,250,201);
BeltItem::Add("Galena","galena","OreItems",1,250,202);
BeltItem::Add("Coal","coal","OreItems",1,3000,203);
BeltItem::Add("Iron Ore","ironore","OreItems",1,3000,204);
BeltItem::Add("Cobalt Ore","cobaltore","OreItems",1,3000,205);
BeltItem::Add("Mithrite","mithrite","OreItems",1,3000,206);
BeltItem::Add("Adamantite","adamantite","OreItems",1,3000,207);
BeltItem::Add("Meteor Chunk","meteorchunk","OreItems",1,5000,208,"RestoreMana 10");
BeltItem::Add("Meteor Core","meteorcore","OreItems",1,25042,209);

// Metal Items

BeltItem::Add("Copper","copper","MetalItems",1,250,300);
BeltItem::Add("Tin","tin","MetalItems",1,250,301);
BeltItem::Add("Bronze","bronze","MetalItems",1,250,302);
BeltItem::Add("Lead","lead","MetalItems",1,250,303);
BeltItem::Add("Iron","iron","MetalItems",1,250,304);
BeltItem::Add("Steel","steel","MetalItems",1,250,305);
BeltItem::Add("Cobalt","cobalt","MetalItems",1,3000,306);
BeltItem::Add("Mythril","mythrite","MetalItems",1,3000,307);
BeltItem::Add("Adamantium","adamantium","MetalItems",1,3000,308);

//Wood Items
BeltItem::Add("Splint","splint","WoodItems",0.1,0,400);
BeltItem::Add("Twig","twig","WoodItems",0.2,0,401);
BeltItem::Add("Stick","Stick","WoodItems",0.4,0,402);
BeltItem::Add("Rod","rod","WoodItems",0.8,64,403);
BeltItem::Add("Long Rod","longrod","WoodItems",1.6,128,404);
BeltItem::Add("Lumber","lumber","WoodItems",3.2,256,405);
BeltItem::Add("Oak Wood","oakwood","WoodItems",6.4,512,406);
BeltItem::Add("Pine Wood","pinewood","WoodItems",12.8,1024,407);
BeltItem::Add("Bire Wood","birewood","WoodItems",25.6,2048,408);
BeltItem::Add("Worm Wood","wormwood","WoodItems",51.2,4096,409);

$AccessoryVar[Splint, $MiscInfo] = "Splint";
$AccessoryVar[Twig, $MiscInfo] = "Twig";
$AccessoryVar[Stick, $MiscInfo] = "Stick";
$AccessoryVar[Rod, $MiscInfo] = "Rod";
$AccessoryVar[LongRod, $MiscInfo] = "LongRod";
$AccessoryVar[Lumber, $MiscInfo] = "Lumber";
$AccessoryVar[OakWood, $MiscInfo] = "OakWood";
$AccessoryVar[PineWood, $MiscInfo] = "PineWood";
$AccessoryVar[BireWood, $MiscInfo] = "BireWood";
$AccessoryVar[WormWood, $MiscInfo] = "WormWood";

%woodCuttingMod[Splint] = 8;
%woodCuttingMod[Twig] = 50;
%woodCuttingMod[Stick] = 180;
%woodCuttingMod[Rod] = 380;
%woodCuttingMod[LongRod] = 850;
%woodCuttingMod[Lumber] = 1200;
%woodCuttingMod[OakWood] = 2604;
%woodCuttingMod[PineWood] = 4930;
%woodCuttingMod[BireWood] = 8680;
%woodCuttingMod[WormWood] = 19702;

%f = 43;
$ItemList[WoodCutting, 1] = "Splint " @ round(%woodCuttingMod[Splint] / %f)+2;
$ItemList[WoodCutting, 2] = "Twig " @ round(%woodCuttingMod[Twig] / %f)+2;
$ItemList[WoodCutting, 3] = "Stick " @ round(%woodCuttingMod[Stick] / %f)+2;
$ItemList[WoodCutting, 4] = "Rod " @ round(%woodCuttingMod[Rod] / %f)+2;
$ItemList[WoodCutting, 5] = "LongRod " @ round(%woodCuttingMod[LongRod] / %f)+2;
$ItemList[WoodCutting, 6] = "Lumber " @ round(%woodCuttingMod[Lumber] / %f)+2;
$ItemList[WoodCutting, 7] = "OakWood " @ round(%woodCuttingMod[OakWood] / %f)+2;
$ItemList[WoodCutting, 8] = "PineWood " @ round(%woodCuttingMod[PineWood] / %f)+2;
$ItemList[WoodCutting, 9] = "BireWood " @ round(%woodCuttingMod[BireWood] / %f)+2;
$ItemList[WoodCutting, 10] = "WormWood " @ round(%woodCuttingMod[WormWood] / %f)+2;

// Equip Items

//Unused
BeltEquip::AddEquipmentItem("Ring of Minor Power","ringofminpower","EquipItems",0.2,5000,13,"ATK 5","finger");
BeltEquip::AddEquipmentItem("Brawler's Ring","brawlring","EquipItems",0.2,5000,31,"ATK 10","finger");
BeltEquip::AddEquipmentItem("Ring of Power","ringofpower","EquipItems",0.2,5000,32,"ATK 150","finger");
BeltEquip::AddEquipmentItem("Mage's Ring","magesring","EquipItems",0.2,50000,33,"SKILL"@$SkillOffensiveCasting@" 25 SKILL"@$SkillDefensiveCasting@" 5 SKILL"@$SkillNatureCasting@" 10 SKILL"@$SkillEnergy@" 25","finger");
BeltEquip::AddEquipmentItem("Power Bracelet","armbandofhurt","EquipItems",0.2,5000,35,"ATK 250","arm");
BeltEquip::AddEquipmentItem("Swordsman Armband","swordsmanarmband","EquipItems",0.2,5000,36,"SKILL"@$SkillSlashing@" 150","arm");

//Used
BeltEquip::AddEquipmentItem("Ring of Harm","ringofharm","EquipItems",0.2,10000,900,"ATK 10","finger");
BeltEquip::AddEquipmentItem("Ring of Defense","ringofdefense","EquipItems",0.2,8000,901,"DEF 50","finger");
BeltEquip::AddEquipmentItem("Ring of Stamina","ringofstamina","EquipItems",0.2,12000,902,"MaxStam 10","finger");
BeltEquip::AddEquipmentItem("Blade Ring","bladering","EquipItems",0.2,15000,903,"SKILL"@$SkillSlashing@" 20","finger");
BeltEquip::AddEquipmentItem("Sharp Ring","sharpring","EquipItems",0.2,15000,904,"SKILL"@$SkillPiercing@" 20","finger");
BeltEquip::AddEquipmentItem("Blunt Ring","bluntring","EquipItems",0.2,15000,905,"SKILL"@$SkillBludgeoning@" 20","finger");
BeltEquip::AddEquipmentItem("Bash Ring","bashring","EquipItems",0.2,15000,906,"SKILL"@$SkillBashing@" 20","finger");
BeltEquip::AddEquipmentItem("Backstab Ring","backstabring","EquipItems",0.2,15000,907,"SKILL"@$SkillBackstabbing@" 20","finger");
BeltEquip::AddEquipmentItem("Hide Ring","hidering","EquipItems",0.2,15000,908,"SKILL"@$SkillHiding@" 20","finger");
BeltEquip::AddEquipmentItem("Wizard Ring","wizardring","EquipItems",0.2,15000,909,"SKILL"@$SkillOffensiveCasting@" 25","finger");
BeltEquip::AddEquipmentItem("Healer Ring","healerring","EquipItems",0.2,15000,910,"SKILL"@$SkillDefensiveCasting@" 25","finger");
BeltEquip::AddEquipmentItem("Nature Ring","naturering","EquipItems",0.2,15000,911,"SKILL"@$SkillNatureCasting@" 25","finger");
BeltEquip::AddEquipmentItem("Harvest Ring","harvestring","EquipItems",0.2,15000,912,"SKILL"@$SkillFarming@" 25","finger");
//BeltEquip::AddEquipmentItem("Chef Ring","chefring","EquipItems",0.2,15000,913,"SKILL"@$SkillCooking@" 25","finger");
BeltEquip::AddEquipmentItem("Ring of Rest","ringofrest","EquipItems",0.2,8000,914,"RestStam 0.8","finger");
BeltEquip::AddEquipmentItem("Ring of Idle","ringofidle","EquipItems",0.2,8000,915,"IdleStam 0.2","finger");
BeltEquip::AddEquipmentItem("Archery Ring","archeryring","EquipItems",0.2,15000,916,"SKILL"@$SkillArchery@" 20","finger");
BeltEquip::AddEquipmentItem("Mana Ring","manaring","EquipItems",0.2,25000,917,"MaxMANA 25","finger");


$AccessoryVar[ringofharm, $MiscInfo] = "Ring that gives <f0>+10 ATK";
$AccessoryVar[ringofdefense, $MiscInfo] = "Ring that gives <f0>+50 DEF";
$AccessoryVar[ringofstamina, $MiscInfo] = "Ring that gives <f0>+10 Max Stamina";
$AccessoryVar[bladering, $MiscInfo] = "Ring that gives <f0>+20 Slashing";
$AccessoryVar[sharpring, $MiscInfo] = "Ring that gives <f0>+20 Piercing";
$AccessoryVar[bluntring, $MiscInfo] = "Ring that gives <f0>+20 Bludgeoning";
$AccessoryVar[bashring, $MiscInfo] = "Ring that gives <f0>+20 Bashing";
$AccessoryVar[backstabring, $MiscInfo] = "Ring that gives <f0>+20 Backstabbing";
$AccessoryVar[hidering, $MiscInfo] = "Ring that gives <f0>+20 Hiding";
$AccessoryVar[wizardring, $MiscInfo] = "Ring that gives <f0>+25 Offensive Casting";
$AccessoryVar[healerring, $MiscInfo] = "Ring that gives <f0>+25 Defensive Casting";
$AccessoryVar[naturering, $MiscInfo] = "Ring that gives <f0>+25 Neutral Casting";
$AccessoryVar[harvestring, $MiscInfo] = "Ring that gives <f0>+25 Farming";
$AccessoryVar[chefring, $MiscInfo] = "Ring that gives <f0>+25 Cooking";
$AccessoryVar[ringofrest, $MiscInfo] = "Ring that gives <f0>+0.8 <f1>stamina regen when resting.";
$AccessoryVar[ringofidle, $MiscInfo] = "Ring that gives <f0>+0.2 stamina regen idle (no moving or attacking).";
$AccessoryVar[archeryring, $MiscInfo] = "Ring that gives <f0>+20 Archery";

BeltEquip::AddEquipmentItem("Soldier Band","soldierband","EquipItems",0.2,150000,800,"SKILL"@$SkillSlashing@" 100 SKILL"@$SkillPiercing@" 100 SKILL"@ $SkillBludgeoning @" 100","arm");
BeltEquip::AddEquipmentItem("Exile Band","exileband","EquipItems",0.2,150000,801,"SKILL"@$SkillNatureCasting@" 180","arm");
BeltEquip::AddEquipmentItem("Energy Band","energyband","EquipItems",0.2,25000,802,"MaxStam 50","arm");
BeltEquip::AddEquipmentItem("Basher's Bangle","bashersbangle","EquipItems",0.2,250000,803,"SKILL"@ $SkillBludgeoning @" 20 SKILL"@$SkillBashing@" 100","arm");

$AccessoryVar[soldierband, $MiscInfo] = "A clasp that raises all your melee weapon skills by 100";
$AccessoryVar[exileband, $MiscInfo] = "An armband that raises all your Neutral Casting 180";
$AccessoryVar[energyband, $MiscInfo] = "An armband that raises your max stamina by 50";
$AccessoryVar[bashersbangle, $MiscInfo] = "Bangle that increashing your Bludgeoning by 20 and Bashing by 100";

BeltEquip::AddEquipmentItem("Protection Amulet","protectamulet","EquipItems",0.2,50000,850,"DEF 150 MDEF 150","neck");
BeltEquip::AddEquipmentItem("Necklace of Defence","necklaceofdef","EquipItems",0.2,750000,851,"ARM 3 DEF 50 MDEF 50","neck");
BeltEquip::AddEquipmentItem("Blink Amulet","blinkamulet","EquipItems",0.2,750000,852,"","neck");
BeltEquip::AddEquipmentItem("Traitor's Amulet","traitorsamulet","EquipItems",0.1,50000,853,"SKILL"@$SkillBackstabbing@" 80 SKILL"@$SkillHiding@" 80","neck");

$AccessoryVar[traitorsamulet, $MiscInfo] = "Increases your hiding and backstabbing by 80.";

BeltEquip::AddUseAbility(blinkamulet,"Blink 150 50");

// Spell Scroll Items
BeltItem::Add("Enhanced Offensive Casting","enhancedoffcastscroll","ScrollItems",0,5000,600);
BeltItem::Add("Enhanced Defensive Casting","enhanceddefcastscroll","ScrollItems",0,5000,601);
BeltItem::Add("Enhanced Neutral Casting","enhancedneucastscroll","ScrollItems",0,5000,602);

$StealProtectedItem["enhancedoffcastscroll"] = true;
$StealProtectedItem["enhanceddefcastscroll"] = true;
$StealProtectedItem["enhancedneucastscroll"] = true;

// Food Items
BeltItem::Add("Bread","Bread","FoodItems",0.5,1200,"","EatFoodItem,cooldown 60,StamRegen 0.2 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
BeltItem::Add("Ear Bread?","EarBread","FoodItems",0.5,25000,750,"EatFoodItem,cooldown 80,StamRegen 0.3 80,HPRegen "@0.45/$TribesDamageToNumericDamage@" 80,ATK 10 80");
BeltItem::Add("Gob Cookie","GobCookie","FoodItems",0.5,1200,751,"EatFoodItem,cooldown 60,StamRegen 0.5 60,HPRegen "@0.32/$TribesDamageToNumericDamage@" 60");
BeltItem::Add("YucJuice","YucJuice","FoodItems",0.5,7500,752,"EatFoodItem,cooldown 100,StamRegen 0.4 100,HPRegen "@0.5/$TribesDamageToNumericDamage@" 100");
BeltItem::Add("Red Berry Pie","RedBerryPie","FoodItems",0.5,12500,753);
BeltItem::Add("Strawberry Cake","StrawberryCake","FoodItems",0.5,25000,754);

$AccessoryVar[Bread, $MiscInfo] = "A loaf of bread.  Eating it will boost health and stamina regen slightly.";
$AccessoryVar[EarBread, $MiscInfo] = "Is this edible? Boosts health and stam regen and raises ATK by 10";
$AccessoryVar[GobCookie, $MiscInfo] = "A cookie made of Gobbie Berries.  Eating it will boost stamina regen";
$AccessoryVar[YucJuice, $MiscInfo] = "A flask of healthy yuccavera juice.  Eating it will boost health and stamina regen."; 
$AccessoryVar[RedBerryPie, $MiscInfo] = "Delicious red berry pie. Eating it will boost stamina regen.";
$AccessoryVar[StrawberryCake, $MiscInfo] = "A cake slices of strawberries.  Greatly boosts stamina regen.";


// Plant Items (Other vars defined in Farming.cs
BeltItem::Add("Grain","Grain","PlantItems",0.25,8,700);
BeltItem::Add("Gobbie Berry","GobbieBerry","PlantItems",0.2,30,701);
BeltItem::Add("Yuccavera","Yuccavera","PlantItems",0.2,100,702);
BeltItem::Add("Red Berry","RedBerry","PlantItems",0.2,30,703);
BeltItem::Add("Tree Fruit","TreeFruit","PlantItems",0.5,1,704);
BeltItem::Add("Strawberry","Strawberry","PlantItems",0.3,250,705);
BeltItem::Add("Nixphyllum","Nixphyllum","PlantItems",0.3,300,706);
BeltItem::Add("Deez Nuts","DeezNuts","PlantItems",0.3,1500,707);
BeltItem::Add("Lunabrosia","Lunabrosia","PlantItems",0.3,3000,708);

//Ammo Items
BeltItem::Add("Small Rock","SmallRock","AmmoItems",0.2,13,16);
BeltItem::Add("Basic Arrow","BasicArrow","AmmoItems",0.1,GenerateItemCost(BasicArrow),17);
BeltItem::Add("Sheaf Arrow","SheafArrow","AmmoItems",0.1,GenerateItemCost(SheafArrow),18);
BeltItem::Add("Bladed Arrow","BladedArrow","AmmoItems",0.1,GenerateItemCost(BladedArrow),19);
BeltItem::Add("Light Quarrel","LightQuarrel","AmmoItems",0.1,GenerateItemCost(LightQuarrel),20);
BeltItem::Add("Heavy Quarrel","HeavyQuarrel","AmmoItems",0.1,GenerateItemCost(HeavyQuarrel),21);
BeltItem::Add("Short Quarrel","ShortQuarrel","AmmoItems",0.1,GenerateItemCost(ShortQuarrel),22);
BeltItem::Add("Stone Feather","StoneFeather","AmmoItems",0.1,GenerateItemCost(StoneFeather),23);
BeltItem::Add("Metal Feather","MetalFeather","AmmoItems",0.1,GenerateItemCost(MetalFeather),24);
BeltItem::Add("Talon","Talon","AmmoItems",0.1,GenerateItemCost(Talon),25);
BeltItem::Add("Ceraphum's Feather","CeraphumsFeather","AmmoItems",0.1,GenerateItemCost(CeraphumsFeather),26);
//BeltItem::Add("Poison Arrow","PoisonArrow","AmmoItems",0.1,200,27);

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

$ProjRestrictions[SmallRock] = ",Sling,";
$ProjRestrictions[BasicArrow] = ",ShortBow,LongBow,ElvenBow,CompositeBow,RShortBow,";
$ProjRestrictions[SheafArrow] = ",ShortBow,LongBow,ElvenBow,CompositeBow,RShortBow,";
$ProjRestrictions[BladedArrow] = ",ShortBow,LongBow,ElvenBow,CompositeBow,RShortBow,";
$ProjRestrictions[LightQuarrel] = ",LightCrossbow,HeavyCrossbow,RLightCrossbow,";
$ProjRestrictions[HeavyQuarrel] = ",LightCrossbow,HeavyCrossbow,RLightCrossbow,";
$ProjRestrictions[ShortQuarrel] = ",RepeatingCrossbow,";
$ProjRestrictions[StoneFeather] = ",AeolusWing,";
$ProjRestrictions[MetalFeather] = ",AeolusWing,";
$ProjRestrictions[Talon] = ",AeolusWing,";
$ProjRestrictions[CeraphumsFeather] = ",AeolusWing,";

$ProjItemData[SmallRock] = SmallRock;
$ProjItemData[BasicArrow] = BowArrow;
$ProjItemData[SheafArrow] = BowArrow;
$ProjItemData[BladedArrow] = BowArrow;
$ProjItemData[LightQuarrel] = CrossbowBolt;
$ProjItemData[HeavyQuarrel] = CrossbowBolt;
$ProjItemData[ShortQuarrel] = CrossbowBolt;
$ProjItemData[StoneFeather] = BowArrow;
$ProjItemData[MetalFeather] = BowArrow;
$ProjItemData[Talon] = BowArrow;
$ProjItemData[CeraphumsFeather] = BowArrow;

// Lore Items

BeltItem::Add("Parchment","parchment","LoreItems",0.2,1);
BeltItem::Add("Magic Dust","magicdust","LoreItems",0.2,1);


$ItemList[Lore, 1] = "parchment";
$ItemList[Lore, 2] = "magicdust";


$LoreItem[Parchment] = True;
$LoreItem[MagicDust] = True;


$AccessoryVar[Parchment, $MiscInfo] = "A parchment";
$AccessoryVar[MagicDust, $MiscInfo] = "A small bag containing magic dust";


// Rare and Quest Items

BeltItem::Add("Black Statue","BlackStatue","RareItems",3,1);
BeltItem::Add("Goblin Ear","GoblinEar","RareItems",0.2,12000);
BeltItem::Add("Dark Tome","DarkTome","RareItems",3,250000);
BeltItem::Add("Bone Dust","BoneDust","RareItems","",16000);
BeltItem::Add("Skeleton Bone","SkeletonBone","RareItems",1,1);
BeltItem::Add("Enchanted Stone","EnchantedStone","RareItems",2,1);
BeltItem::Add("Dragon Scale","DragonScale","RareItems",8,245310);

BeltItem::Add("Magicite","Magicite","RareItems",0.2,550000);
//BeltItem::Add("Testing Anvil","Anvil","RareItems",20,5000);

$AccessoryVar[blackstatue, $MiscInfo] = "A strange black statue.";
$AccessoryVar[skeletonbone, $MiscInfo] = "A bone from an old skeleton.";
$AccessoryVar[EnchantedStone, $MiscInfo] = "A weird glowing stone.";
$AccessoryVar[DragonScale, $MiscInfo] = "A dragon scale.";

$AccessoryVar[goblinear, $MiscInfo] = "Ear of a goblin.";
$AccessoryVar[DarkTome, $MiscInfo] = "A very rare spell book. Used in the crafting of rare items.";
$AccessoryVar[BoneDust, $MiscInfo] = "Rare dust of a reanimated skeleton.";

BeltItem::Add("Blue Potion","BluePotion","PotionItems",4,80,27,"DrinkHealingPotion 15");
BeltItem::Add("Crystal Blue Potion","CrystalBluePotion","PotionItems",10,200,28,"DrinkHealingPotion 60");

BeltItem::Add("Energy Potion","EnergyPotion","PotionItems",4,80,29,"DrinkStaminaPotion 25");
BeltItem::Add("Crystal Energy Potion","CrystalEnergyPotion","PotionItems",10,200,30,"DrinkStaminaPotion 50");

BeltItem::Add("Red Potion","RedPotion","PotionItems",4,12000,50,"DrinkRedPotion 30");

// Crafted and not sold in stores.
BeltItem::Add("Energy Shot","EnergyShot","PotionItems",0.2,50,"","DrinkStaminaPotion 15");
BeltItem::Add("Energy Vial","EnergyVial","PotionItems",0.5,250,"","DrinkStaminaPotion 25");
BeltItem::Add("Crystal Energy Vial","CrystalEnergyVial","PotionItems",1,1500,"","DrinkStaminaPotion 50");
BeltItem::Add("Energized Potion","EnergizedPotion","PotionItems",1,5000,"","DrinkStaminaPotion 100");


$AccessoryVar[BluePotion, $MiscInfo] = "A blue potion that heals 15 HP";
$AccessoryVar[CrystalBluePotion, $MiscInfo] = "A crystal blue potion that heals 60 HP";


$AccessoryVar[EnergyPotion, $MiscInfo] = "An energy potion that provides 50 Stamina";
$AccessoryVar[CrystalEnergyPotion, $MiscInfo] = "A crystal energy potion that provides 100 Stamina";
$AccessoryVar[EnergyShot, $MiscInfo] = "A very small vial of potion that restores 15 Stamina. Not much, but very light weight."; 
$AccessoryVar[EnergyVial, $MiscInfo] = "A small energy vial restores 15 Stamina. Doesn't seem like much, but very light weight.";
$AccessoryVar[CrystalEnergyVial, $MiscInfo] = "A small energy vial that restores 50 Stamina. Similar to a Crystal Energy Potion, but lighter.";
$AccessoryVar[EnergizedPotion, $MiscInfo] = "An energy potion that restores 100 Stamina.";

$AccessoryVar[RedPotion, $MiscInfo] = "A potion that recovers 30 HP and 30 Stamina.";

function Belt::UseItem(%clientId,%item)
{
    echo(%item);
    if(Belt::HasThisStuff(%clientId,%item) > 0 || BeltEquip::IsItemEquipped(%clientId,%item))
    {
        %useTag = getWord($beltitem[%item, "UseTag"],0);
        if(%useTag == "DrinkHealingPotion")
        {
            DrinkHealingPotion(%clientId,%item,getWord($beltitem[%item, "UseTag"],1));
            Belt::TakeThisStuff(%clientId,%item,1);
            RefreshAll(%clientId,false);
            return true;
        }
        else if(%useTag == "DrinkStaminaPotion")
        {
            DrinkStaminaPotion(%clientId,%item,getWord($beltitem[%item, "UseTag"],1));
            Belt::TakeThisStuff(%clientId,%item,1);
            RefreshAll(%clientId,false);
            return true;
        }
        else if(%useTag == "DrinkRedPotion")
        {
            DrinkRedPotion(%clientId,%item,getWord($beltitem[%item, "UseTag"],1));
            Belt::TakeThisStuff(%clientId,%item,1);
            RefreshAll(%clientId,false);
            return true;
        }
        else if(String::getWord($beltitem[%item, "UseTag"],",",0) == "EatFoodItem")
        {
            if(AddBonusStatePoints(%clientId,"FoodCooldown") == 0)
            {
                if(%clientId.sleepMode == "")
                {
                    EatFoodItem(%clientId,%item,Word::getSubWord($beltitem[%item, "UseTag"],1,999,","));
                    Belt::TakeThisStuff(%clientId,%item,1);
                    RefreshAll(%clientId,false);
                }
                else
                    Client::sendMessage(%clientId, $MsgWhite, "You can't eat right now.");
            }
            else
                Client::sendMessage(%clientId, $MsgWhite, "You aren't ready to eat again.");
                
            return true;
        }
        else if(%useTag == "RestoreMana")
        {
            RestoreMana(%clientId,%item,getWord($beltitem[%item, "UseTag"],1),"crushed a "@ $beltitem[%item, "Name"]);
            Belt::TakeThisStuff(%clientId,%item,1);
            return true;
        }
        else if(%useTag == "Blink")
        {
            %mana = fetchData(%clientId,"MANA");
            %cost = getWord($beltitem[%item, "UseTag"],2);
            if(%mana >= %cost)
            {
                %los = Gamebase::getLOSInfo(Client::getOwnedObject(%clientId),getWord($beltitem[%item, "UseTag"],1));
                if(%los)
                {
                    %castPos = $los::position;
                    %castPosNorm = $los::normal;
                    //Plasmatic's code to prevent phasing through floors and walls
                    %xpos = getWord(%castPos,0);
                    %ypos = getWord(%castPos,1);
                    %zpos = getWord(%castPos,2);
                    
                    if(Vector::dot(%castPosNorm,"0 0 1") > 0.6) 
                    {
                        %zpos += 0.5;//floor
                    }
                    else 
                    {
                        if(Vector::dot(%castPosNorm,"0 0 -1") > 0.6) 
                        {
                            %zpos -= 2;//ceiling
                        }
                        else if(Vector::dot(%castPosNorm,"0 0 -1") >= -0.1 || Vector::dot(%castPosNorm,"0 0 -1") <= 0.1 ) 
                        {
                            //wall
                            %xopos = getWord(%castPosNorm,0);
                            %yopos = getWord(%castPosNorm,1);
                            //%xpos = %xpos + %xopos + %xopos;
                            %xpos = %xpos + %xopos;
                            //%ypos = %ypos + %yopos + %yopos;
                            %ypos = %ypos + %yopos;
                        }
                    }
                    %extraDelay = 0.22;
                    %dest = %xpos@" "@%ypos@" "@%zpos;
                    
                    Gamebase::setPosition(%clientId,%dest);
                    Client::sendMessage(%clientId,$MsgWhite,"You blink forward!");
                    refreshMana(%clientId,%cost);
                    return true;
                }
                else
                    Client::sendMessage(%clientId,$MsgRed,"No point in sight to blink toward.");
            }
            else
                Client::sendMessage(%clientId,$MsgRed,"Insufficient mana to use that amulet.");
        }
        else
            return false;
    }
    else
        return false;
}

function DrinkHealingPotion(%clientId,%item,%amt)
{
    %hp = fetchData(%clientId, "HP");
    refreshHP(%clientId,%amt * -0.01);
    if(fetchData(%clientId,"HP") != %hp)
        UseSkill(%clientId, $SkillHealing, True, True);
    Client::sendMessage(%clientId, $MsgWhite, "You drank a "@$beltitem[%item, "Name"]@" and recovered "@ %amt @"HP~wActivateAR.wav");
}

function DrinkStaminaPotion(%clientId,%item,%amt)
{
    %stam = fetchData(%clientId,"Stamina");
    refreshStamina(%clientId,%amt*-1);
    if(fetchData(%clientId,"Stamina") != %stam)
        UseSkill(%clientId, $SkillEnergy, True, True);
    Client::sendMessage(%clientId, $MsgWhite, "You drank a "@$beltitem[%item, "Name"]@" and recovered "@ %amt @" Stamina~wActivateAR.wav");

}

function DrinkRedPotion(%clientId,%item,%amt)
{
    %hp = fetchData(%clientId, "HP");
    refreshHP(%clientId,%amt * -0.01);
    if(fetchData(%clientId,"HP") != %hp)
        UseSkill(%clientId, $SkillHealing, True, True);
    
    %stam = fetchData(%clientId,"Stamina");
    refreshStamina(%clientId,%amt*-1);
    if(fetchData(%clientId,"Stamina") != %stam)
        UseSkill(%clientId, $SkillEnergy, True, True);
    Client::sendMessage(%clientId, $MsgWhite, "You drank a "@$beltitem[%item, "Name"]@" and recovered "@ %amt @" HP and "@ %amt @" Stamina~wActivateAR.wav");
}

function RestoreMana(%clientId,%item,%amt,%desc)
{
    refreshMana(%clientId,%amt*-1);
    Client::sendMessage(%clientId, $MsgWhite, "You "@%desc@" and recovered "@ %amt @" Mana~wActivateAR.wav");
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
            echo(%bonusType);
            %bonusAmnt = getWord(%data,1);
            %bonusTicks = getWord(%data,2);
            UpdateBonusState(%clientId, %bonusType @" "@%bonusAmnt, %bonusTicks);
        }
    }
    refreshHPREGEN(%clientId);
    refreshStaminaREGEN(%clientId);
    UpdateBonusState(%clientId, "FoodCooldown 1", %cooldown);
    Client::sendMessage(%clientId, $MsgWhite, "You ate a "@$beltitem[%item, "Name"]@".");
}

// =============================
// Other items for Salmon's server
// =============================
// Just keeping these around in case i want them later


//BeltItem::Add("The Holy Grail","holygrail","LoreItems",40,1);
//BeltItem::Add("Mithril Brooch","mithrilbrooch","LoreItems",0.2,1);
//BeltItem::Add("Gemmed Statue","gemmedstatue","LoreItems",1,1);
//BeltItem::Add("Spell Book","spellbook","LoreItems",1.5,1);

//$ItemList[Lore, 3] = "holygrail";
//$ItemList[Lore, 4] = "mithrilbrooch";
//$ItemList[Lore, 5] = "gemmedstatue";
//$ItemList[Lore, 6] = "spellbook";

//$LoreItem[MithrilBrooch] = True;
//$LoreItem[GemmedStatue] = True;
//$LoreItem[SpellBook] = True;
//$LoreItem[HolyGrail] = True;

//$AccessoryVar[MithrilBrooch, $MiscInfo] = "A Mithril Brooch";
//$AccessoryVar[GemmedStatue, $MiscInfo] = "A Gemmed Statue";
//$AccessoryVar[SpellBook, $MiscInfo] = "A SpellBook";
//$AccessoryVar[HolyGrail, $MiscInfo] = "<f2>I fart in you general direction! -Monty Python";

//BeltItem::Add("Vial of Goo","vialofgoo","RareItems",1,1);
//BeltItem::Add("Ogre Brain","ogrebrain","RareItems",1,1);
//BeltItem::Add("Red Herb","redherb","RareItems",1,1);
//BeltItem::Add("Green Herb","greenherb","RareItems",1,1);
//BeltItem::Add("Minotaur Steak","MinotaurSteak","RareItems",2,1);
//BeltItem::Add("Minotaur Rib","MinotaurRib","RareItems",2,1);
//BeltItem::Add("Minotaur Horn","MinotaurHorn","RareItems",2,1);
//BeltItem::Add("Dracos Tooth","Dracostooth","RareItems",2,1);
//BeltItem::Add("Dracos Claw","Dracosclaw","RareItems",2,1);
//BeltItem::Add("Book Page","BookPage","RareItems",2,1);
//BeltItem::Add("Book of Life","BookpfLife","RareItems",5,1);
//BeltItem::Add("SoulStone","SoulStone","RareItems",4,1);
//BeltItem::Add("Manuscript Piece","ManuscriptPiece","RareItems",1,1);
//BeltItem::Add("Rose","Rose","RareItems",1,1);
//BeltItem::Add("Pearl","pearl","RareItems",1,1);
//BeltItem::Add("Brooch Sliver","BroochSliver","RareItems",1,1);
//BeltItem::Add("Silver Brooch","SilverBrooch","RareItems",6,1);
//BeltItem::Add("Mithril Shard","mithrilshard","RareItems",1,1);
//BeltItem::Add("Mithril","Mithril","RareItems",6,1);
//BeltItem::Add("Shield Receipt","ShieldReceipt","RareItems",2,1);
//BeltItem::Add("Gnoll Eye","GnollEye","RareItems",1,1);
//BeltItem::Add("Fish Scale","FishScale","RareItems",1,1);
//BeltItem::Add("Fishermens Knife","FishermensKnife","RareItems",5,1);
//
//$AccessoryVar[vialofgoo, $MiscInfo] = "A vial of strange orange goo.";
//$AccessoryVar[ogrebrain, $MiscInfo] = "A smelly lump of shit that comes from an ogres skull.";
//$AccessoryVar[redherb, $MiscInfo] = "A red herb.";
//$AccessoryVar[greenherb, $MiscInfo] = "A green herb.";
//$AccessoryVar[MinotaurSteak, $MiscInfo] = "A fat chunk of meat from a minotaur.";
//$AccessoryVar[MinotaurRib, $MiscInfo] = "A rack of mino ribs.";
//$AccessoryVar[MinotaurHorn, $MiscInfo] = "Ripped right from a minotaurs skull.";
//$AccessoryVar[DracosTooth, $MiscInfo] = "A dracos tooth.";
//$AccessoryVar[DracosClaw, $MiscInfo] = "A dracos claw.";
//$AccessoryVar[BookPage, $MiscInfo] = "A page from the book of life.";
//$AccessoryVar[BookofLife, $MiscInfo] = "The Book of Life.";
//$AccessoryVar[SoulStone, $MiscInfo] = "An ancient stone.";
//$AccessoryVar[ManuscriptPiece, $MiscInfo] = "A piece from an ancient Manuscript that dates back to the beginning of religion.";
//$AccessoryVar[Rose, $MiscInfo] = "A rose.";
//$AccessoryVar[pearl, $MiscInfo] = "A pearl.";
//$AccessoryVar[BroochSliver, $MiscInfo] = "A sliver from a silver brooch.";
//$AccessoryVar[SilverBrooch, $MiscInfo] = "An old silver brooch.";
//$AccessoryVar[MithrilShard, $MiscInfo] = "A shard of mithril.";
//$AccessoryVar[Mithril, $MiscInfo] = "A nice sized chunk of mithril.";
//$AccessoryVar[ShieldReceipt, $MiscInfo] = "A receipt for a well made shield.";
//$AccessoryVar[GnollEye, $MiscInfo] = "Fish seem to enjoy the juices from gnoll eyes.";
//$AccessoryVar[FishScale, $MiscInfo] = "A big scale from a guppy.";
//$AccessoryVar[FishermensKnife, $MiscInfo] = "A knife only used by fishermen.";