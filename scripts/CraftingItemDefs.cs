
deleteVariables("Crafting::*");

//=================
// Crafting Types
//=================
Crafting::AddCraftingType("smithing","Smithing","#smith","smith","smithed",SoundCanSmith,$SkillSmithing,0);
Crafting::AddCraftingType("alchemy","Alchemy","#mix","mix","mixed",SoundCanSmith,$SkillAlchemy,1);
Crafting::AddCraftingType("smelting","Smelting","#smelt","smelt","smelted",SoundCanSmith2,$SkillSmithing,2);
Crafting::AddCraftingType("cooking","Cooking","#cook","cook","cooked",SoundCanSmith2,$SkillCooking,3);

//===================
// Smithing recipes
//===================
Crafting::Addrecipe("smithing","Knife",$SkillSmithing @" 15","Quartz 6",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smithing","Broadsword",$SkillSmithing @" 40","Quartz 6 Jade 2");
Crafting::Addrecipe("smithing","WarAxe",$SkillSmithing @" 80","Quartz 6 Jade 2");
Crafting::Addrecipe("smithing","MeteorDagger",$SkillSmithing @" 0","Dagger 1 MeteorChunk 5",1,1,true);
Crafting::Addrecipe("smithing","MeteorAxe",$SkillSmithing @" 0","WarAxe 1 MeteorCore 5",1,1,true);
Crafting::Addrecipe("smithing","CrudeAxe",$SkillSmithing @" 0.1","SmallRock 3 Splint 5",1,1000,true);
Crafting::SetCraftSound("Broadsword",SoundCanSmith2);


//===================
// Alchemy recipes
//===================

Crafting::Addrecipe("alchemy","EnergyShot",$SkillAlchemy @" 0","GobbieBerry 3 quartz 1",1,1,true);
Crafting::Addrecipe("alchemy","EnergyVial",$SkillAlchemy @" 0","GoblinEar 2 GobbieBerry 3",1,1,true);
Crafting::Addrecipe("alchemy","CrystalEnergyVial",$SkillAlchemy @" 0","turquoise 2 redberry 3",1,1,true);
Crafting::Addrecipe("alchemy","EnergizedPotion",$SkillAlchemy @" 0","sapphire 3 skeletonbone 2 yuccavera 5",1,1,true);

//Crafting::Addrecipe("alchemy","EnergyShot",$SkillAlchemy @" 5","GobbieBerry 3 quartz 1",1);
//Crafting::Addrecipe("alchemy","EnergyVial",$SkillAlchemy @" 60","GoblinEar 2 GobbieBerry 3",1);
//Crafting::Addrecipe("alchemy","CrystalEnergyVial",$SkillAlchemy @" 250","turquoise 2 redberry 3",1);
//Crafting::Addrecipe("alchemy","EnergizedPotion",$SkillAlchemy @" 500","sapphire 3 skeletonbone 2 yuccavera 5",1);

//===================
// Cooking Recipes
//===================

Crafting::Addrecipe("cooking","Bread",$SkillCooking @" 0","Grain 20",1,1,true);
Crafting::Addrecipe("cooking","EarBread",$SkillCooking @" 0","Grain 15 GoblinEar 2",1,1,true);
Crafting::Addrecipe("cooking","GobCookie",$SkillCooking @" 0","Grain 10 GobbieBerry 5",3,1,true);
Crafting::Addrecipe("cooking","YucJuice",$SkillCooking @" 0","Yuccavera 5",1,1,true);
Crafting::Addrecipe("cooking","RedBerryPie",$SkillCooking @" 0","Yuccavera 2 RedBerry 10 Grain 50",1,1,true);
Crafting::Addrecipe("cooking","StrawberryCake",$SkillCooking @" 0","Bread 10 Eggs 5 Strawberry 15",1,1,true);

//Crafting::Addrecipe("cooking","Bread",$SkillCooking @" 15","Grain 20",1);
//Crafting::Addrecipe("cooking","EarBread",$SkillCooking @" 20","Grain 15 GoblinEar 2",1);
//Crafting::Addrecipe("cooking","GobCookie",$SkillCooking @" 50","Grain 10 GobbieBerry 5",3);
//Crafting::Addrecipe("cooking","YucJuice",$SkillCooking @" 180","Yuccavera 5",1);
//Crafting::Addrecipe("cooking","RedBerryPie",$SkillCooking @" 220","Yuccavera 2 RedBerry 10 Grain 50",1);
//Crafting::Addrecipe("cooking","StrawberryCake",$SkillCooking @" 400","Bread 10 Eggs 5 Strawberry 15",1);

//===================
// Smelting recipes
//===================

Crafting::Addrecipe("smelting","Copper",$SkillSmithing @" 30","copperore 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smelting","Tin",$SkillSmithing @" 30","tinore 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smelting","Bronze",$SkillSmithing @" 150","tin 1 copper 3",4);
Crafting::Addrecipe("smelting","Lead",$SkillSmithing @" 175","galena 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smelting","Iron",$SkillSmithing @" 250","ironore 5",1,$BaseCraftingDifficulty/1.2);
Crafting::Addrecipe("smelting","Steel",$SkillSmithing @" 350","iron 1 coal 5",1,$BaseCraftingDifficulty/1.5);
Crafting::Addrecipe("smelting","Cobalt",$SkillSmithing @" 500 R 1","cobaltore 15 coal 10",2,$BaseCraftingDifficulty/3); // Smaller number means harder to craft
Crafting::Addrecipe("smelting","Mythril",$SkillSmithing @" 700 R 5","iron 5 cobalt 2 coal 20 mythrite 1",1,$BaseCraftingDifficulty/10); // Harder to craft
Crafting::Addrecipe("smelting","Adamantium",$SkillSmithing @" 1000 R 10","iron 5 cobalt 2 coal 20 mythrite 1",1,$BaseCraftingDifficulty/10); // Harder to craft