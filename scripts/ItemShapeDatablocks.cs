
//Dagger
ItemImageData DaggerShapeImage
{
	shapeFile  = "dagger";
	mountPoint = 0;

	weaponType = 1;
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Knife);
	
	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;
    
	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData DaggerShape
{
	heading = "bWeapons";
	description = "DaggerShape";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = DaggerShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Sword
ItemImageData SwordShapeImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 1;
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Gladius);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData SwordShape
{
	heading = "bWeapons";
	description = "SwordShape";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = SwordShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Shortsword
ItemImageData ShortswordShapeImage
{
	shapeFile  = "short_sword";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Shortsword);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing2;
	sfxActivate = AxeSlash2;
};
ItemData ShortswordShape
{
	heading = "bWeapons";
	description = "Short Sword Sahpe";
	className = "Weapon";
	shapeFile  = "short_sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = ShortswordShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Longsword
ItemImageData LongswordShapeImage
{
	shapeFile  = "long_sword";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Longsword);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData LongswordShape
{
	heading = "bWeapons";
	description = "Long Sword";
	className = "Weapon";
	shapeFile  = "long_sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = LongswordShapeImage;
	price = 0;
	showWeaponBar = true;
};

//KeldriniteLS
ItemImageData KeldriniteLSShapeImage
{
	shapeFile  = "elfinblade";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(KeldriniteLS);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing2;
	sfxActivate = ActivateAS;
};
ItemData KeldriniteLSShape
{
	heading = "bWeapons";
	description = "Keldrinite Long Sword";
	className = "Weapon";
	shapeFile  = "elfinblade";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = KeldriniteLSShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Katana
ItemImageData KatanaShapeImage
{
	shapeFile  = "katana";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0; //GetDelay(Rapier);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData KatanaShape
{
	heading = "bWeapons";
	description = "Rapier";
	className = "Weapon";
	shapeFile  = "katana";
	hudIcon = "katana";
	shadowDetailMask = 4;
	imageType = KatanaShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Hatchet
ItemImageData HatchetShapeImage
{
	shapeFile  = "hatchet";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Hatchet);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData HatchetShape
{
	heading = "bWeapons";
	description = "Hatchet";
	className = "Weapon";
	shapeFile  = "hatchet";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = HatchetShapeImage;
	price = 0;
	showWeaponBar = true;
};

//WarAxe
ItemImageData WarAxeShapeImage
{
	shapeFile  = "axe";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(WarAxe);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData WarAxeShape
{
	heading = "bWeapons";
	description = "War Axe";
	className = "Weapon";
	shapeFile  = "axe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = WarAxeShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Pickaxe
ItemImageData PickAxeShapeImage
{
	shapeFile = "Pick";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(PickAxe);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = CrossbowSwitch1;
};
ItemData PickAxeShape
{
	heading = "bWeapons";
	description = "Pick Axe";
	className = "Weapon";
	shapeFile = "Pick";
	hudIcon = "pick";
	shadowDetailMask = 4;
	imageType = PickAxeShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Battleaxe
ItemImageData BattleAxeShapeImage
{
	shapeFile  = "BattleAxe";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(BattleAxe);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing7;
	sfxActivate = AxeSlash2;
};
ItemData BattleAxeShape
{
	heading = "bWeapons";
	description = "Battle Axe";
	className = "Weapon";
	shapeFile  = "BattleAxe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = BattleAxeShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Spear
ItemImageData SpearShapeImage
{
	shapeFile  = "spear";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Spear);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData SpearShape
{
	heading = "bWeapons";
	description = "Spear";
	className = "Weapon";
	shapeFile  = "spear";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = SpearShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Trident
ItemImageData TridentShapeImage
{
	shapeFile  = "trident";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Trident);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData TridentShape
{
	heading = "bWeapons";
	description = "Trident";
	className = "Weapon";
	shapeFile  = "trident";
	hudIcon = "trident";
	shadowDetailMask = 4;
	imageType = TridentShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Mace
ItemImageData MaceShapeImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Club);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData MaceShape
{
	heading = "bWeapons";
	description = "Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "club";
	shadowDetailMask = 4;
	imageType = MaceShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Hammer
ItemImageData HammerShapeImage
{
	shapeFile  = "hammer";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(WarHammer);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData HammerShape
{
	heading = "bWeapons";
	description = "War Hammer";
	className = "Weapon";
	shapeFile  = "hammer";
	hudIcon = "hammer";
	shadowDetailMask = 4;
	imageType = HammerShapeImage;
	price = 0;
	showWeaponBar = true;
};

//QuarterStaff
ItemImageData QuarterStaffShapeImage
{
	shapeFile  = "quarterstaff";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(QuarterStaff);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData QuarterStaffShape
{
	heading = "bWeapons";
	description = "Quarter Staff";
	className = "Weapon";
	shapeFile  = "quarterstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = QuarterStaffShapeImage;
	price = 0;
	showWeaponBar = true;
};

//LongStaff
ItemImageData LongStaffShapeImage
{
	shapeFile  = "longstaff";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(LongStaff);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData LongStaffShape
{
	heading = "bWeapons";
	description = "Long Staff";
	className = "Weapon";
	shapeFile  = "longstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = LongStaffShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Crossbow
ItemImageData CrossbowShapeImage
{
	shapeFile = "crossbow";
	mountPoint = 0;

	weaponType = 1;
    ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Sling);

    spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;
    
	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	//sfxReload = NoSound;
};
ItemData CrossbowShape
{
	description = "Crossbow";
	className = "Weapon";
	shapeFile = "crossbow";
	hudIcon = "grenade";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = CrossbowShapeImage;
	price = 0;
	showWeaponBar = true;
};

//Longbow
ItemImageData LongBowShapeImage
{
	shapeFile = "longbow";
	mountPoint = 0;

	weaponType = 1; 
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(ShortBow);

    spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;
    
	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData LongBowShape
{
	description = "Long Bow";
	className = "Weapon";
	shapeFile = "longbow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = LongBowShapeImage;
	price = 0;
	showWeaponBar = true;
};

//CompositeBow
ItemImageData CompositeBowShapeImage
{
	shapeFile = "comp_bow";
	mountPoint = 0;

	weaponType = 1; 
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(CompositeBow);

    spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;
    
	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData CompositeBowShape
{
	description = "Composite Bow";
	className = "Weapon";
	shapeFile = "comp_bow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = CompositeBowShapeImage;
	price = 0;
	showWeaponBar = true;
};

ItemImageData TreeShapeItemImage
{
	shapeFile  = "tree1";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(QuarterStaff);
    mountOffset = { -1, -1, -2 }; 
	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundHitLeather;
	sfxActivate = SoundHitLeather;
};
ItemData TreeShapeItem
{
	heading = "bWeapons";
	description = "Treeatk";
	className = "Weapon";
	shapeFile  = "tree1";
	hudIcon = "trident";
	shadowDetailMask = 4;
	imageType = TreeShapeItemImage;
	price = 0;
	showWeaponBar = true;
};


ItemImageData KnightShieldImage
{
	shapeFile = "shield3";
	mountPoint = 2;
	mountOffset = {0.18, -0.1, -0.2};
	mountRotation = {0, 0, 0.5};
};
ItemData KnightShield
{
	description = "Knight's Shield";
	className = "Accessory";
	shapeFile = "shield3";
	imageType = KnightShieldImage;

	heading = "eMiscellany";
	price = 0;
};
ItemData KnightShield0
{
	description = "Knight's Shield";
	className = "Equipped";
	shapeFile = "shield3";

	heading = "aArmor";
};

ItemImageData InvisShapeImage
{
	shapeFile  = "OneWayWallInvis_8x8";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Club);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData InvisShape
{
	heading = "bWeapons";
	description = "Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "club";
	shadowDetailMask = 4;
	imageType = InvisShapeImage;
	price = 0;
	showWeaponBar = true;
};

ItemImageData PotionShapeImage
{
	shapeFile  = "Potion";
	mountPoint = 0;

	weaponType = 1; 
	reloadTime = 0;
	fireTime = 0.01; //GetDelay(Club);

	spinUpTime = 0;
	spinDownTime = 0;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};

ItemData PotionShape
{
	heading = "bWeapons";
	description = "PotionShape";
	className = "Accessory";
	shapeFile  = "Potion";
	//hudIcon = "dagger";
	shadowDetailMask = 4;
	//imageType = DaggerShapeImage;
	price = 0;
	showWeaponBar = false;
};