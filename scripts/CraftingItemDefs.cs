
deleteVariables("Crafting::*");

//=================
// Crafting Types
//=================
Crafting::AddCraftingType("craft","Crafting","#craft","craft","crafted",SoundCanSmith,"",0);
Crafting::AddCraftingType("smithing","Smithing","#smith","smith","smithed",SoundCanSmith,"",1);
//Crafting::AddCraftingType("alchemy","Alchemy","#mix","mix","mixed",SoundCanSmith,"",1);
//Crafting::AddCraftingType("smelting","Smelting","#smelt","smelt","smelted",SoundCanSmith2,"",2);
//Crafting::AddCraftingType("cooking","Cooking","#cook","cook","cooked",SoundCanSmith2,"",3);

//===================
// Smithing recipes
//===================
Crafting::Addrecipe("smithing","Knife","","Quartz 6",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smithing","Broadsword","","Quartz 6 Jade 2");
Crafting::Addrecipe("smithing","WarAxe","","Quartz 6 Jade 2");
Crafting::Addrecipe("smithing","MeteorDagger","","Dagger 1 MeteorChunk 5",1,1,true);
Crafting::Addrecipe("smithing","MeteorAxe","","WarAxe 1 MeteorCore 5",1,1,true);
Crafting::Addrecipe("smithing","CrudeAxe","","SmallRock 3 Splint 5",1,1000,true);
Crafting::SetCraftSound("Broadsword",SoundCanSmith2);

Crafting::Addrecipe("smithing","soldierband","R 1","DarkTome 1 Gold 5 GoblinEar 10",1,1,true);
Crafting::Addrecipe("smithing","exileband","R 1","DarkTome 1 Gold 5 Jade 5",1,1,true);
//===================
// Alchemy recipes
//===================

Crafting::Addrecipe("craft","EnergyShot","","GobbieBerry 3 quartz 1",1,1,true);
Crafting::Addrecipe("craft","EnergyVial","","GoblinEar 2 GobbieBerry 3",1,1,true);
Crafting::Addrecipe("craft","CrystalEnergyVial","","turquoise 2 redberry 3",1,1,true);
Crafting::Addrecipe("craft","EnergizedPotion","","sapphire 3 skeletonbone 2 yuccavera 5",1,1,true);

//Crafting::Addrecipe("alchemy","EnergyShot",$SkillAlchemy @" 5","GobbieBerry 3 quartz 1",1);
//Crafting::Addrecipe("alchemy","EnergyVial",$SkillAlchemy @" 60","GoblinEar 2 GobbieBerry 3",1);
//Crafting::Addrecipe("alchemy","CrystalEnergyVial",$SkillAlchemy @" 250","turquoise 2 redberry 3",1);
//Crafting::Addrecipe("alchemy","EnergizedPotion",$SkillAlchemy @" 500","sapphire 3 skeletonbone 2 yuccavera 5",1);

//===================
// Cooking Recipes
//===================

Crafting::Addrecipe("craft","Bread","","Grain 20",1,1,true);
Crafting::Addrecipe("craft","EarBread","","Grain 15 GoblinEar 2",1,1,true);
Crafting::Addrecipe("craft","GobCookie","","Grain 10 GobbieBerry 5",3,1,true);
Crafting::Addrecipe("craft","YucJuice","","Yuccavera 5",1,1,true);
Crafting::Addrecipe("craft","RedBerryPie","","Yuccavera 2 RedBerry 10 Grain 50",1,1,true);
Crafting::Addrecipe("craft","StrawberryCake","","Bread 10 Eggs 5 Strawberry 15",1,1,true);

//Crafting::Addrecipe("cooking","Bread",$SkillCooking @" 15","Grain 20",1);
//Crafting::Addrecipe("cooking","EarBread",$SkillCooking @" 20","Grain 15 GoblinEar 2",1);
//Crafting::Addrecipe("cooking","GobCookie",$SkillCooking @" 50","Grain 10 GobbieBerry 5",3);
//Crafting::Addrecipe("cooking","YucJuice",$SkillCooking @" 180","Yuccavera 5",1);
//Crafting::Addrecipe("cooking","RedBerryPie",$SkillCooking @" 220","Yuccavera 2 RedBerry 10 Grain 50",1);
//Crafting::Addrecipe("cooking","StrawberryCake",$SkillCooking @" 400","Bread 10 Eggs 5 Strawberry 15",1);

//===================
// Smelting recipes
//===================

Crafting::Addrecipe("smithing","Copper","","copperore 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smithing","Tin","","tinore 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smithing","Bronze","","tin 1 copper 3",4);
Crafting::Addrecipe("smithing","Lead","","galena 5",1,$BaseCraftingDifficulty);
Crafting::Addrecipe("smithing","Iron","","ironore 5",1,$BaseCraftingDifficulty/1.2);
Crafting::Addrecipe("smithing","Steel","","iron 1 coal 5",1,$BaseCraftingDifficulty/1.5);
Crafting::Addrecipe("smithing","Cobalt","","cobaltore 15 coal 10",2,$BaseCraftingDifficulty/3); // Smaller number means harder to craft
Crafting::Addrecipe("smithing","Mythril","","iron 5 cobalt 2 coal 20 mythrite 1",1,$BaseCraftingDifficulty/10); // Harder to craft
Crafting::Addrecipe("smithing","Adamantium","",1,$BaseCraftingDifficulty/10); // Harder to craft