
$RPGItem::AccessoryClass = "Accessory";
$RPGItem::WeaponClass = "Weapon";
$RPGItem::EquipppedClass = "Equipped";
$RPGItem::AmmoClass = "Ammo";

$RPGItem::PlayerWeaponList = "WeaponItemInv";

RPGItem::AddItemClass("Weapon","Weapons","WeaponItemInv","WeaponItemStorage");
RPGItem::AddItemClass("Equipped","Equipped","EquippedItemInv","");
RPGItem::AddItemClass("Accessory","Accessories","AccessoryItemInv","AccessoryItemStorage");
RPGItem::AddItemClass("Rares","Rares","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Potion","Potions","PouchItemInv","PouchItemStorage");
RPGItem::AddItemClass("Ammo","Ammo","AmmoItemInv","AmmoItemStorage");


RPGItem::AddWeapon("shiv","Shiv",0,$RPGItem::WeaponTypeMelee,DaggerShape);
//RPGItem::AddItemDefinition("shiv","Shiv",$RPGItem::WeaponClass,0,DaggerShape);
RPGItem::AddItemDefinition("bluepotion","Blue Potion","Potion",1);
RPGItem::AddItemDefinition("crystalbluepotion","Crystal Blue Potion","Potion",2);
RPGItem::AddItemDefinition("hidearmor0","Hide Armor",$RPGItem::EquipppedClass,3,MiscLootShape);
RPGItem::AddItemDefinition("hidearmor","Hide Armor",$RPGItem::AccessoryClass,4,MiscLootShape);
RPGItem::AddWeapon("club","Club",5,$RPGItem::WeaponTypeMelee,MaceShape);
RPGItem::AddWeapon("spear","Spear",6,$RPGItem::WeaponTypeMelee,SpearShape);
RPGItem::AddWeapon("dagger","Dagger",7,$RPGItem::WeaponTypeMelee,DaggerShape);
//Roadmap
//Weapons
//Armor
//Usable Items
//Accessories (rings and other equippables)

//Accessory.cs
//Functions needing replacement:
//GetAccessoryList //Done but needs testing
//AddPoints //Needs testing
//NullItemList //Might be fine?
//NullBeltList // Get rid of references
//GetCurrentlyWearingArmor //Needs testing

//RPGFunk.cs
//HasThisStuff //Needs test
//TakeThisStuff //Needs test
//GiveThisStuff //Needs test
//WhatIs //Done
//UnequipMountedStuff //TO DO

//Economy.cs
//BuyItem
//SellItem

//Weapon.cs
//WeaponHandling.cs

//ItemEvents.cs
//Item::onCollision
//Item::onDrop //Needs replacement
//Item::onUse //Needs replacement

//Item.cs
//Change mounting slots

//Search for .className

//Remove all belt references


//Player::onKilled //Needs rework for item dropping

//GetWeight //Needs rework