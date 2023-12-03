$fireTimeDelay = 0.1;

$RustyDamageAmp = 0.7;
$RustyWeightAmp = 1.5;
$RustyCostAmp = 0.3;

$RangeTable[$AxeAccessoryType] = 3;
$RangeTable[$SwordAccessoryType] = 3;
$RangeTable[$ShortBladeAccessoryType] = 3;
$RangeTable[$PickAxeAccessoryType] = 3;
$RangeTable[$PolearmAccessoryType] = 4;
$RangeTable[$BludgeonAccessoryType] = 3;

$DelayFactorTable[$RingAccessoryType] = "0.0";
$DelayFactorTable[$BodyAccessoryType] = "0.0";
$DelayFactorTable[$BootsAccessoryType] = "0.0";
$DelayFactorTable[$BackAccessoryType] = "0.0";
$DelayFactorTable[$ShieldAccessoryType] = "0.0";
$DelayFactorTable[$TalismanAccessoryType] = "0.0";
$DelayFactorTable[$AxeAccessoryType] = "1.0";
$DelayFactorTable[$SwordAccessoryType] = "1.0";
$DelayFactorTable[$PolearmAccessoryType] = "1.0";
$DelayFactorTable[$BludgeonAccessoryType] = "1.0";
$DelayFactorTable[$RangedAccessoryType] = "1.0";
$DelayFactorTable[$ProjectileAccessoryType] = "1.0";
$DelayFactorTable[$ShortBladeAccessoryType] = "1.0";
$DelayFactorTable[$PickAxeAccessoryType] = "1.0";
$DelayFactorTable[$MageStaffAccessoryType] = "1.0";

$CostFactorTable[$RingAccessoryType] = "1.0";
$CostFactorTable[$BodyAccessoryType] = "1.0";
$CostFactorTable[$BootsAccessoryType] = "1.0";
$CostFactorTable[$BackAccessoryType] = "1.0";
$CostFactorTable[$ShieldAccessoryType] = "1.0";
$CostFactorTable[$TalismanAccessoryType] = "1.0";
$CostFactorTable[$SwordAccessoryType] = "1.0";
$CostFactorTable[$AxeAccessoryType] = "1.0";
$CostFactorTable[$PolearmAccessoryType] = "1.0";
$CostFactorTable[$BludgeonAccessoryType] = "1.0";
$CostFactorTable[$RangedAccessoryType] = "1.0";
$CostFactorTable[$ProjectileAccessoryType] = "0.01";
$CostFactorTable[$ShortBladeAccessoryType] = "1.0";
$CostFactorTable[$PickAxeAccessoryType] = "1.0";
$CostFactorTable[$MageStaffAccessoryType] = "1.0";
//****************************************************************************************************
$AccessoryVar[CrudeAxe, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[Hatchet, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[BroadSword, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[WarAxe, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[LongSword, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[BattleAxe, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[BastardSword, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[Halberd, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[Claymore, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[Club, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[SpikedClub, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[Mace, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[HammerPick, $AccessoryType] = $PickAxeAccessoryType;
$AccessoryVar[WarHammer, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[WarMaul, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[QuarterStaff, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[LongStaff, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[JusticeStaff, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[Knife, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[Dagger, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[ShortSword, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[Spear, $AccessoryType] = $PolearmAccessoryType;
$AccessoryVar[Gladius, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[Trident, $AccessoryType] = $PolearmAccessoryType;
$AccessoryVar[Rapier, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[AwlPike, $AccessoryType] = $PolearmAccessoryType;
$AccessoryVar[PickAxe, $AccessoryType] = $PickAxeAccessoryType;
$AccessoryVar[Sling, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[ShortBow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[LongBow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[ElvenBow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[CompositeBow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[LightCrossbow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[HeavyCrossbow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[RepeatingCrossbow, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[SmallRock, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[BasicArrow, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[SheafArrow, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[BladedArrow, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[LightQuarrel, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[HeavyQuarrel, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[ShortQuarrel, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[CastingBlade, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[KeldriniteLS, $AccessoryType] = $SwordAccessoryType;
$AccessoryVar[AeolusWing, $AccessoryType] = $RangedAccessoryType;
$AccessoryVar[StoneFeather, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[MetalFeather, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[Talon, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[CeraphumsFeather, $AccessoryType] = $ProjectileAccessoryType;
$AccessoryVar[BoneClub, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[SpikedBoneClub, $AccessoryType] = $BludgeonAccessoryType;
$AccessoryVar[MeteorDagger, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[MeteorAxe, $AccessoryType] = $AxeAccessoryType;
$AccessoryVar[NoviceStaff, $AccessoryType] = $MageStaffAccessoryType;
$AccessoryVar[MagesStaff, $AccessoryType] = $MageStaffAccessoryType;
$AccessoryVar[FireStaff, $AccessoryType] = $MageStaffAccessoryType;
$AccessoryVar[ThornStaff, $AccessoryType] = $MageStaffAccessoryType;
$AccessoryVar[HealerStaff, $AccessoryType] = $MageStaffAccessoryType;

$AccessoryVar[CrudeAxe, $SpecialVar] = "6 10";	
$AccessoryVar[Hatchet, $SpecialVar] = "6 20";			//12 (5)
$AccessoryVar[BroadSword, $SpecialVar] = "6 35";		//21 (5)
$AccessoryVar[WarAxe, $SpecialVar] = "6 70";			//30 (7)
$AccessoryVar[LongSword, $SpecialVar] = "6 65";			//39 (5)
$AccessoryVar[BattleAxe, $SpecialVar] = "6 144";		//48 (9)
$AccessoryVar[BastardSword, $SpecialVar] = "6 133";		//57 (7)
$AccessoryVar[Halberd, $SpecialVar] = "6 176";			//66 (8)
$AccessoryVar[Claymore, $SpecialVar] = "6 188";			//75.2 (7.5)
$AccessoryVar[KeldriniteLS, $SpecialVar] = "6 90";		//90 (0.5)
$AccessoryVar[MeteorAxe, $SpecialVar] = "6 110 13 5"; //Mana harvest
//.................................................................................
$AccessoryVar[Club, $SpecialVar] = "6 12";			//12 (3)
$AccessoryVar[QuarterStaff, $SpecialVar] = "6 35";		//21 (5)
$AccessoryVar[BoneClub, $SpecialVar] = "6 34";			//26 (4)
$AccessoryVar[SpikedClub, $SpecialVar] = "6 30";		//30 (3)
$AccessoryVar[Mace, $SpecialVar] = "6 78";			//39 (6)
$AccessoryVar[HammerPick, $SpecialVar] = "6 80";		//48 (5)
$AccessoryVar[SpikedBoneClub, $SpecialVar] = "6 70";		//52.5 (4)
$AccessoryVar[LongStaff, $SpecialVar] = "6 114";		//57 (6)
$AccessoryVar[WarHammer, $SpecialVar] = "6 176";		//66 (8)
$AccessoryVar[JusticeStaff, $SpecialVar] = "6 118";		//70.8 (5)
$AccessoryVar[WarMaul, $SpecialVar] = "6 175";			//75 (7)
//.................................................................................
$AccessoryVar[PickAxe, $SpecialVar] = "6 16";			//12 (4)
$AccessoryVar[Knife, $SpecialVar] = "6 18";			//18 (1)
$AccessoryVar[Dagger, $SpecialVar] = "6 23";			//23 (3)
$AccessoryVar[ShortSword, $SpecialVar] = "6 50";		//30 (5)
$AccessoryVar[Spear, $SpecialVar] = "6 78";			//39 (6)
$AccessoryVar[Gladius, $SpecialVar] = "6 80";			//48 (5)
$AccessoryVar[Trident, $SpecialVar] = "6 114";			//57 (6)
$AccessoryVar[Rapier, $SpecialVar] = "6 110";			//66 (5)
$AccessoryVar[AwlPike, $SpecialVar] = "6 200";			//75 (8)
$AccessoryVar[MeteorDagger, $SpecialVar] = "6 40 12 1"; // Mana theif
//.................................................................................
$AccessoryVar[CastingBlade, $SpecialVar] = "6 18";
//.................................................................................
$AccessoryVar[Sling, $SpecialVar] = "6 11";			//11 (2)
$AccessoryVar[ShortBow, $SpecialVar] = "6 23";			//23 (3)
$AccessoryVar[LightCrossbow, $SpecialVar] = "6 72";		//36 (6)
$AccessoryVar[LongBow, $SpecialVar] = "6 86";			//51.6 (5)
$AccessoryVar[CompositeBow, $SpecialVar] = "6 85";		//63.75 (4)
$AccessoryVar[RepeatingCrossbow, $SpecialVar] = "6 75";	//75 (3)
$AccessoryVar[ElvenBow, $SpecialVar] = "6 89";			//89 (3)
$AccessoryVar[AeolusWing, $SpecialVar] = "6 101";		//101 (2)
$AccessoryVar[HeavyCrossbow, $SpecialVar] = "6 300";		//112.5 (8)
//.................................................................................
$AccessoryVar[SmallRock, $SpecialVar] = "6 10";
$AccessoryVar[BasicArrow, $SpecialVar] = "6 12";
$AccessoryVar[ShortQuarrel, $SpecialVar] = "6 14";
$AccessoryVar[LightQuarrel, $SpecialVar] = "6 16";
$AccessoryVar[SheafArrow, $SpecialVar] = "6 30";
$AccessoryVar[StoneFeather, $SpecialVar] = "6 40";
$AccessoryVar[BladedArrow, $SpecialVar] = "6 42";
$AccessoryVar[HeavyQuarrel, $SpecialVar] = "6 44";
$AccessoryVar[MetalFeather, $SpecialVar] = "6 60";
$AccessoryVar[Talon, $SpecialVar] = "6 80";
$AccessoryVar[CeraphumsFeather, $SpecialVar] = "6 105";

$AccessoryVar[NoviceStaff, $SpecialVar] = "6 25";
$AccessoryVar[MagesStaff, $SpecialVar] = "6 50";
$AccessoryVar[FireStaff, $SpecialVar] = "6 75";
$AccessoryVar[ThornStaff, $SpecialVar] = "6 20";
$AccessoryVar[HealerStaff, $SpecialVar] = "6 10";
//.................................................................................
$AccessoryVar[CrudeAxe, $Weight] = 1;
$AccessoryVar[Hatchet, $Weight] = 5;
$AccessoryVar[BroadSword, $Weight] = 5;
$AccessoryVar[WarAxe, $Weight] = 7;
$AccessoryVar[LongSword, $Weight] = 5;
$AccessoryVar[BattleAxe, $Weight] = 9;
$AccessoryVar[BastardSword, $Weight] = 7;
$AccessoryVar[Halberd, $Weight] = 8;
$AccessoryVar[Claymore, $Weight] = "7.5";
$AccessoryVar[KeldriniteLS, $Weight] = "0.5";
$AccessoryVar[MeteorAxe, $Weight] = 7;
//.................................................................................
$AccessoryVar[Club, $Weight] = 3;
$AccessoryVar[QuarterStaff, $Weight] = 5;
$AccessoryVar[BoneClub, $Weight] = 4;
$AccessoryVar[SpikedClub, $Weight] = 3;
$AccessoryVar[Mace, $Weight] = 6;
$AccessoryVar[HammerPick, $Weight] = 5;
$AccessoryVar[SpikedBoneClub, $Weight] = 4;
$AccessoryVar[LongStaff, $Weight] = 6;
$AccessoryVar[WarHammer, $Weight] = 8;
$AccessoryVar[JusticeStaff, $Weight] = 5;
$AccessoryVar[WarMaul, $Weight] = 7;
//.................................................................................
$AccessoryVar[PickAxe, $Weight] = 4;
$AccessoryVar[Knife, $Weight] = 1;
$AccessoryVar[Dagger, $Weight] = 3;
$AccessoryVar[ShortSword, $Weight] = 5;
$AccessoryVar[Spear, $Weight] = 6;
$AccessoryVar[Gladius, $Weight] = 5;
$AccessoryVar[Trident, $Weight] = 6;
$AccessoryVar[Rapier, $Weight] = 5;
$AccessoryVar[AwlPike, $Weight] = 8;
$AccessoryVar[MeteorDagger, $Weight] = 4;
//.................................................................................
$AccessoryVar[Sling, $Weight] = 2;
$AccessoryVar[ShortBow, $Weight] = 3;
$AccessoryVar[LightCrossbow, $Weight] = 6;
$AccessoryVar[LongBow, $Weight] = 5;
$AccessoryVar[CompositeBow, $Weight] = 4;
$AccessoryVar[RepeatingCrossbow, $Weight] = 3;
$AccessoryVar[ElvenBow, $Weight] = 3;
$AccessoryVar[AeolusWing, $Weight] = 2;
$AccessoryVar[HeavyCrossbow, $Weight] = 8;

$AccessoryVar[NoviceStaff, $Weight] = 5;
$AccessoryVar[MagesStaff, $Weight] = 5;
$AccessoryVar[FireStaff, $Weight] = 5;
$AccessoryVar[DruidStaff, $Weight] = 5;
$AccessoryVar[HealerStaff, $Weight] = 5;
//.................................................................................
$AccessoryVar[SmallRock, $Weight] = "0.2";
$AccessoryVar[BasicArrow, $Weight] = "0.1";
$AccessoryVar[SheafArrow, $Weight] = "0.1";
$AccessoryVar[BladedArrow, $Weight] = "0.1";
$AccessoryVar[LightQuarrel, $Weight] = "0.1";
$AccessoryVar[HeavyQuarrel, $Weight] = "0.2";
$AccessoryVar[ShortQuarrel, $Weight] = "0.1";
$AccessoryVar[CastingBlade, $Weight] = "0.5";
$AccessoryVar[StoneFeather, $Weight] = "0.1";
$AccessoryVar[MetalFeather, $Weight] = "0.1";
$AccessoryVar[Talon, $Weight] = "0.2";
$AccessoryVar[CeraphumsFeather, $Weight] = "0.08";

$AccessoryVar[CrudeAxe, $MiscInfo] = "A crude axe built out of twigs and rocks";
$AccessoryVar[Hatchet, $MiscInfo] = "A hatchet";
$AccessoryVar[BroadSword, $MiscInfo] = "A broad sword";
$AccessoryVar[WarAxe, $MiscInfo] = "A war axe";
$AccessoryVar[LongSword, $MiscInfo] = "A long sword";
$AccessoryVar[BattleAxe, $MiscInfo] = "A battle axe";
$AccessoryVar[BastardSword, $MiscInfo] = "A bastard sword";
$AccessoryVar[Halberd, $MiscInfo] = "A halberd";
$AccessoryVar[Claymore, $MiscInfo] = "A claymore";
$AccessoryVar[Club, $MiscInfo] = "A club";
$AccessoryVar[SpikedClub, $MiscInfo] = "A spiked club";
$AccessoryVar[Mace, $MiscInfo] = "A mace";
$AccessoryVar[HammerPick, $MiscInfo] = "A hammer pick";
$AccessoryVar[WarHammer, $MiscInfo] = "A war hammer";
$AccessoryVar[WarMaul, $MiscInfo] = "A war maul";
$AccessoryVar[QuarterStaff, $MiscInfo] = "A quarter staff";
$AccessoryVar[LongStaff, $MiscInfo] = "A long staff";
$AccessoryVar[JusticeStaff, $MiscInfo] = "A Justice long staff";
$AccessoryVar[Knife, $MiscInfo] = "A knife";
$AccessoryVar[Dagger, $MiscInfo] = "A dagger";
$AccessoryVar[ShortSword, $MiscInfo] = "A short sword";
$AccessoryVar[Spear, $MiscInfo] = "A spear";
$AccessoryVar[Gladius, $MiscInfo] = "A gladius";
$AccessoryVar[Trident, $MiscInfo] = "A trident";
$AccessoryVar[Rapier, $MiscInfo] = "A rapier";
$AccessoryVar[AwlPike, $MiscInfo] = "An awl pike";
$AccessoryVar[PickAxe, $MiscInfo] = "A pick axe";
$AccessoryVar[Sling, $MiscInfo] = "A sling";
$AccessoryVar[ShortBow, $MiscInfo] = "A short bow";
$AccessoryVar[LongBow, $MiscInfo] = "A long bow";
$AccessoryVar[ElvenBow, $MiscInfo] = "An elven bow";
$AccessoryVar[CompositeBow, $MiscInfo] = "A composite bow";
$AccessoryVar[LightCrossbow, $MiscInfo] = "A light crossbow";
$AccessoryVar[HeavyCrossbow, $MiscInfo] = "A heavy crossbow";
$AccessoryVar[RepeatingCrossbow, $MiscInfo] = "A repeating crossbow";
$AccessoryVar[SmallRock, $MiscInfo] = "A small rock";
$AccessoryVar[BasicArrow, $MiscInfo] = "A basic arrow";
$AccessoryVar[SheafArrow, $MiscInfo] = "A sheaf arrow";
$AccessoryVar[BladedArrow, $MiscInfo] = "A bladed arrow";
$AccessoryVar[LightQuarrel, $MiscInfo] = "A light quarrel";
$AccessoryVar[HeavyQuarrel, $MiscInfo] = "A heavy quarrel";
$AccessoryVar[ShortQuarrel, $MiscInfo] = "A heavy quarrel";
$AccessoryVar[CastingBlade, $MiscInfo] = "Selects the best spell and casts it.  Used only for bots.";
$AccessoryVar[KeldriniteLS, $MiscInfo] = "The Keldrinite LongSword is one of the rarest and most powerful weapons in the world of Tribes RPG.";
$AccessoryVar[AeolusWing, $MiscInfo] = "Aeolus's wing is a mystical bow with the power of wind";
$AccessoryVar[StoneFeather, $MiscInfo] = "A feather made of stone";
$AccessoryVar[MetalFeather, $MiscInfo] = "A Sharp metal feather. Beautifully crafted";
$AccessoryVar[Talon, $MiscInfo] = "A gemmed talon. It is terribly sharp";
$AccessoryVar[CeraphumsFeather, $MiscInfo] = "Said to have come from the wing of a ceraphum. But we all knew that it came from the forge";
$AccessoryVar[BoneClub, $MiscInfo] = "A club made made of skeleton bones";
$AccessoryVar[SpikedBoneClub, $MiscInfo] = "A spiked club made of skeleton bones";
$AccessoryVar[MeteorDagger, $MiscInfo] = "A dagger enhanced with meteorite. Restores a little mana with every hit.";
$AccessoryVar[MeteorAxe, $MiscInfo] = "An axe enhanced with meteorite. Restores a little mana with each kill.";
$AccessoryVar[NoviceStaff, $MiscInfo] = "An entry level mage's staff. Use #attune to recharge.";
$AccessoryVar[MagesStaff, $MiscInfo] = "A stronger staff with higher mana capacity";
$AccessoryVar[FireStaff, $MiscInfo] = "A staff that shoots fireballs";
$AccessoryVar[ThornStaff, $MiscInfo] = "A staff attuned with nature";
$AccessoryVar[HealerStaff, $MiscInfo] = "A staff that heals";
//NOTE: See shopping.cs for the shopIndexes
$SkillType[CrudeAxe] = $SkillSlashing;
$SkillType[Hatchet] = $SkillSlashing;
$SkillType[BroadSword] = $SkillSlashing;
$SkillType[WarAxe] = $SkillSlashing;
$SkillType[LongSword] = $SkillSlashing;
$SkillType[BattleAxe] = $SkillSlashing;
$SkillType[BastardSword] = $SkillSlashing;
$SkillType[Halberd] = $SkillSlashing;
$SkillType[Claymore] = $SkillSlashing;
$SkillType[Club] = $SkillBludgeoning;
$SkillType[SpikedClub] = $SkillBludgeoning;
$SkillType[Mace] = $SkillBludgeoning;
$SkillType[HammerPick] = $SkillBludgeoning;
$SkillType[WarHammer] = $SkillBludgeoning;
$SkillType[WarMaul] = $SkillBludgeoning;
$SkillType[QuarterStaff] = $SkillBludgeoning;
$SkillType[LongStaff] = $SkillBludgeoning;
$SkillType[JusticeStaff] = $SkillBludgeoning;
$SkillType[Knife] = $SkillPiercing;
$SkillType[Dagger] = $SkillPiercing;
$SkillType[ShortSword] = $SkillPiercing;
$SkillType[Spear] = $SkillPiercing;
$SkillType[Gladius] = $SkillPiercing;
$SkillType[Trident] = $SkillPiercing;
$SkillType[Rapier] = $SkillPiercing;
$SkillType[AwlPike] = $SkillPiercing;
$SkillType[PickAxe] = $SkillPiercing;
$SkillType[Sling] = $SkillArchery;
$SkillType[ShortBow] = $SkillArchery;
$SkillType[LongBow] = $SkillArchery;
$SkillType[ElvenBow] = $SkillArchery;
$SkillType[CompositeBow] = $SkillArchery;
$SkillType[LightCrossbow] = $SkillArchery;
$SkillType[HeavyCrossbow] = $SkillArchery;
$SkillType[RepeatingCrossbow] = $SkillArchery;
$SkillType[BoneClub] = $SkillBludgeoning;
$SkillType[SpikedBoneClub] = $SkillBludgeoning;
$SkillType[MeteorDagger] = $SkillPiercing;
$SkillType[MeteorAxe] = $SkillSlashing;

$SkillType[NoviceStaff] = $SkillOffensiveCasting;
$SkillType[MagesStaff] = $SkillOffensiveCasting;
$SkillType[FireStaff] = $SkillOffensiveCasting;
$SkillType[ThornStaff] = $SkillNatureCasting;
$SkillType[HealerStaff] = $SkillDefensiveCasting;

$WeaponRange[Sling] = 35;
$WeaponRange[ShortBow] = 120;
$WeaponRange[LongBow] = 200;
$WeaponRange[ElvenBow] = 260;
$WeaponRange[CompositeBow] = 360;
$WeaponRange[LightCrossbow] = 300;
$WeaponRange[AeolusWing] = 400;
$WeaponRange[HeavyCrossbow] = 500;
$WeaponRange[RepeatingCrossbow] = 280;
$WeaponRange[CastingBlade] = 1000;	//will swing from anywhere...BUT will be able to snipe with beam

$WeaponDelay[Sling] = 1.5;

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

function GenerateAllWeaponCosts()
{
	dbecho($dbechoMode, "GenerateAllWeaponCosts()");

	//All item costs that need to be Generated must be in a function, later called after all files have been exec'd.
	//This function, among other similar ones, is run once only in server.cs.

	$ItemCost[Hatchet] = GenerateItemCost(Hatchet);
	$ItemCost[BroadSword] = GenerateItemCost(BroadSword);
	$ItemCost[WarAxe] = GenerateItemCost(WarAxe);
	$ItemCost[LongSword] = GenerateItemCost(LongSword);
	$ItemCost[BattleAxe] = GenerateItemCost(BattleAxe);
	$ItemCost[BastardSword] = GenerateItemCost(BastardSword);
	$ItemCost[Halberd] = GenerateItemCost(Halberd);
	$ItemCost[Claymore] = GenerateItemCost(Claymore);
	$ItemCost[Club] = GenerateItemCost(Club);
	$ItemCost[SpikedClub] = GenerateItemCost(SpikedClub);
	$ItemCost[Mace] = GenerateItemCost(Mace);
	$ItemCost[HammerPick] = GenerateItemCost(HammerPick);
	$ItemCost[WarHammer] = GenerateItemCost(WarHammer);
	$ItemCost[WarMaul] = GenerateItemCost(WarMaul);
	$ItemCost[QuarterStaff] = GenerateItemCost(QuarterStaff);
	$ItemCost[LongStaff] = GenerateItemCost(LongStaff);
	$ItemCost[JusticeStaff] = GenerateItemCost(JusticeStaff);
	$ItemCost[Knife] = GenerateItemCost(Knife);
	$ItemCost[Dagger] = GenerateItemCost(Dagger);
	$ItemCost[ShortSword] = GenerateItemCost(ShortSword);
	$ItemCost[Spear] = GenerateItemCost(Spear);
	$ItemCost[Gladius] = GenerateItemCost(Gladius);
	$ItemCost[Trident] = GenerateItemCost(Trident);
	$ItemCost[Rapier] = GenerateItemCost(Rapier);
	$ItemCost[AwlPike] = GenerateItemCost(AwlPike);
	$ItemCost[PickAxe] = GenerateItemCost(PickAxe);
	$ItemCost[Sling] = GenerateItemCost(Sling);
	$ItemCost[ShortBow] = GenerateItemCost(ShortBow);
	$ItemCost[LongBow] = GenerateItemCost(LongBow);
	$ItemCost[ElvenBow] = GenerateItemCost(ElvenBow);
	$ItemCost[CompositeBow] = GenerateItemCost(CompositeBow);
	$ItemCost[LightCrossbow] = GenerateItemCost(LightCrossbow);
	$ItemCost[HeavyCrossbow] = GenerateItemCost(HeavyCrossbow);
	$ItemCost[RepeatingCrossbow] = GenerateItemCost(RepeatingCrossbow);
	//$ItemCost[BasicArrow] = GenerateItemCost(BasicArrow);
	//$ItemCost[SheafArrow] = GenerateItemCost(SheafArrow);
	//$ItemCost[BladedArrow] = GenerateItemCost(BladedArrow);
	//$ItemCost[LightQuarrel] = GenerateItemCost(LightQuarrel);
	//$ItemCost[HeavyQuarrel] = GenerateItemCost(HeavyQuarrel);
	//$ItemCost[ShortQuarrel] = GenerateItemCost(ShortQuarrel);
	$ItemCost[CastingBlade] = 0;
	$ItemCost[KeldriniteLS] = GenerateItemCost(KeldriniteLS);
	$ItemCost[AeolusWing] = GenerateItemCost(AeolusWing);
	//$ItemCost[StoneFeather] = GenerateItemCost(StoneFeather);
	//$ItemCost[MetalFeather] = GenerateItemCost(MetalFeather);
	//$ItemCost[Talon] = GenerateItemCost(Talon);
	//$ItemCost[CeraphumsFeather] = GenerateItemCost(CeraphumsFeather);
	$ItemCost[BoneClub] = GenerateItemCost(BoneClub);
	$ItemCost[SpikedBoneClub] = GenerateItemCost(SpikedBoneClub);
    $ItemCost[MeteorDagger] = GenerateItemCost(MeteorDagger) + 500;
    $ItemCost[MeteorAxe] = GenerateItemCost(MeteorAxe) + 500;
    
    $ItemCost[NoviceStaff] = GenerateItemCost(NoviceStaff);
    $ItemCost[MagesStaff] = GenerateItemCost(MagesStaff);
    $ItemCost[FireStaff] = GenerateItemCost(FireStaff);
    $ItemCost[ThornStaff] = GenerateItemCost(ThornStaff);
    $ItemCost[HealerStaff] = GenerateItemCost(HealerStaff) + 1500;
    
	$ItemCost[RHatchet] = round($ItemCost[Hatchet] * $RustyCostAmp);
	$ItemCost[RBroadSword] = round($ItemCost[BroadSword] * $RustyCostAmp);
	$ItemCost[RLongSword] = round($ItemCost[LongSword] * $RustyCostAmp);
	$ItemCost[RClub] = round($ItemCost[Club] * $RustyCostAmp);
	$ItemCost[RSpikedClub] = round($ItemCost[SpikedClub] * $RustyCostAmp);
	$ItemCost[RKnife] = round($ItemCost[Knife] * $RustyCostAmp);
	$ItemCost[RDagger] = round($ItemCost[Dagger] * $RustyCostAmp);
	$ItemCost[RShortSword] = round($ItemCost[ShortSword] * $RustyCostAmp);
	$ItemCost[RPickAxe] = round($ItemCost[PickAxe] * $RustyCostAmp);
	$ItemCost[RShortBow] = round($ItemCost[ShortBow] * $RustyCostAmp);
	$ItemCost[RLightCrossbow] = round($ItemCost[LightCrossbow] * $RustyCostAmp);
	$ItemCost[RWarAxe] = round($ItemCost[WarAxe] * $RustyCostAmp);
}

//****************************************************************************************************

function MeleeAttack(%player, %length, %weapon)
{
	dbecho($dbechoMode, "MeleeAttack(" @ %player @ ", " @ %length @ ")");

	%clientId = Player::getClient(%player);
	if(%clientId == "")
		%clientId = 0;

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
		return;
	%clientId.lastFireTime = %time;
	//=======================================================
	%mult = 1;
    %stamMult = 1;
    %dmgMult = 1;
    %mom = "0 0 0";
    //I don't want to iterate over all bonuses every weapon swing. Making it a fetchData flag is less costly
    if(fetchData(%clientId,"DoubleStrikeFlag") != "")
    {
        %to = fetchData(%clientId,"DoubleStrikeTimeout");
        if(getSimTime() <= %to)
            %mult = 2;
        else
            storeData(%clientId,"DoubleStrikeFlag",false);
    }
    else if(fetchData(%clientId,"HeavyStrikeFlag"))
    {
        %stamMult += 0.75;
        %dmgMult += 0.33;
        
        %etrans = Gamebase::getEyeTransform(%clientId);
        %dir = Word::getSubWord(%etrans,3,3);
        %mom = ScaleVector(%dir,$Ability::heavyStrikeForce);
        //%mom = Vector::getFromRot(%clientId,$Ability::heavyStrikeForce,15);
        playSound(SoundSwing7,Gamebase::getPosition(%clientId));
    }
    
    %stamMult *= %mult;
    
    WeaponStamina(%clientId,%weapon,%stamMult);
	
	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		%obj = getObjectType($los::object);
		if(%obj == "Player")
		{
			GameBase::virtual($los::object, "onDamage", "", 1.0, "0 0 0", "0 0 0", %mom, "torso", "", %clientId, %weapon,"",%dmgMult);
            if(%mult > 1)
            {
                for(%i = 1; %i < %mult; %i++)
                {
                    schedule("GameBase::virtual("@$los::object@", \"onDamage\", \"\", 1.0, \"0 0 0\", \"0 0 0\", \""@ %mom @"\", \"torso\", \"\", "@%clientId@", "@%weapon@",\"\","@%dmgMult@");",0.2*%i,%player);
                }
            }
		}
	}
    if(fetchData(%clientId,"HeavyStrikeFlag"))
        storeData(%clientId,"HeavyStrikeFlag","");
	PostAttack(%clientId, %weapon);
}

function ProjectileAttack(%clientId, %weapon, %vel)
{
	dbecho($dbechoMode, "ProjectileAttack(" @ %clientId @ ", " @ %weapon @ ", " @ %vel @ ")");

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
		return;
	%clientId.lastFireTime = %time;
	
    WeaponStamina(%clientId,%weapon,1);
    %loadedProjectile = fetchData(%clientId, "LoadedProjectile " @ %weapon);
	if(%loadedProjectile == "")
		return;
    if(RPGItem::getItemCount(%clientId,%loadedProjectile) <= 0) //belt::hasthisstuff(%clientId, %loadedProjectile) <= 0)
		return;
    if(fetchData(%clientId,"TrueShot"))
    {
        Weapon::FireTrueShot(%clientId,%weapon,%vel,%loadedProjectile);
    }
    else
    {
        Weapon::FireItemProjectile(%clientId,%weapon,%vel,%loadedProjectile);
    }

	PostAttack(%clientId, %weapon);
}

function Weapon::FireItemProjectile(%clientId,%weapon,%vel,%loadedProjectile)
{
    //	%losflag = "";
//	if(GameBase::getLOSinfo(Client::getOwnedObject(%clientId), 50000))
//	{
//		%target = $los::object;
//		%obj = getObjectType(%target);
//		%dist = Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(Player::getClient(%target)));
//
//		if(%dist <= GetRange(%weapon))
//		{
//	        if(%obj == "Player")
//			{
//				%factor = sqrt(%dist) / 6.454;
//				%vel = %dist / %factor;
//	
//				%zoffset = 0.25;
//				%losflag = True;
//			}
//		}
//	}
//	if(!%losflag)
//	{
		%zoffset = 0.44;
//	}
    %arrow = newObject("", "Item", $ProjItemData[%loadedProjectile], 1, false);
	%arrow.owner = %clientId;
	%arrow.delta = 1;
	%arrow.weapon = %weapon;
    %arrow.itemProj = %loadedProjectile;

	addToSet("MissionCleanup", %arrow);
  	schedule("Item::Pop(" @ %arrow @ ");", 30, %arrow);

	//double-check stuff
	$ProjectileDoubleCheck[%arrow] = True;
	schedule("$ProjectileDoubleCheck[" @ %arrow @ "] = \"\";", 1.5, %arrow);

	%rot = GameBase::getRotation(%clientId);
	%newrot = (GetWord(%rot, 0) - %zoffset) @ " " @ GetWord(%rot, 1) @ " " @ GetWord(%rot, 2);

	GameBase::setRotation(%clientId, %newrot);
	GameBase::throw(%arrow, Client::getOwnedObject(%clientId), %vel, false);
	GameBase::setRotation(%arrow, %rot);
	GameBase::setRotation(%clientId, %rot);
    
    RPGItem::decItemCount(%clientId,%loadedProjectile,1);
    //Belt::TakeThisStuff(%clientId, %loadedProjectile,1);
}

function Weapon::FireTrueShot(%clientId,%weapon,%vel,%loadedProjectile)
{
    %player = Client::getOwnedObject(%clientId);
    $los::position = "";
    %trans = Gamebase::getMuzzleTransform(%player);
    if(Gamebase::getLOSInfo(%player,$TrueShotMaxRange))
    {
        %pos = Word::getSubWord(%trans,9,3);
        %dir = Vector::Normalize(Vector::sub($los::position,%pos));
        %trans = "0 0 0 "@ %dir @" 0 0 0 "@ %pos;
    }
    Projectile::spawnProjectile(TrueShotArrow,%trans,%player,%vel);
    RPGItem::decItemCount(%clientId,%loadedProjectile,1);
}

function DoMiningSwing(%clientId,%target,%weapon,%mom,%dmgMult)
{
    %obj = getObjectType(%target);
    %type = GameBase::getDataName(%target);

    if(%type == "Crystal")
    {
        %brflag = String::findSubStr(fetchData(%clientId, "RACE"), "Human");	//must be human to mine
        if(Vector::getDistance(%clientId.lastMinePos, GameBase::getPosition(%clientId)) > 1.0 && %brflag != -1)
        {
            playSound(SoundHitore, GameBase::getPosition(%target));	//vectrex, modified by JI

            %score = DoRandomMining(%clientId, %target);
            if(%score != "")
            {
                belt::givethisstuff(%clientId, %score, 1, 1, 1);
                //Player::incItemCount(%clientId, %score, 1);
                RefreshAll(%clientId,false);
                //Client::sendMessage(%clientId, 0, "You found " @ %score.description @ ".");

                if( floor(getRandom() * 10) == 5)
                    %clientId.lastMinePos = GameBase::getPosition(%clientId);
            }
            UseSkill(%clientId, $SkillMining, True, True);
        }
        else
            playSound(SoundHitore2, GameBase::getPosition(%target));
    }
    else if(%type == "MeteorCrystal")
    {
        %brflag = String::findSubStr(fetchData(%clientId, "RACE"), "Human");	//must be human to mine
        if(%brflag)
        {
            playSound(SoundHitore, GameBase::getPosition(%target));	//vectrex, modified by JI
            %rewardIdx = MineMeteorCrystal();
            %item = $MeteorMiningList[%rewardIdx];
            //Player::incItemCount(%clientId, %item, 1);
            belt::givethisstuff(%clientId, %item, 1, 1, 1);
            if(OddsAre($MeteorMineChunkOdds))
            {
                belt::givethisstuff(%clientId, "MeteorChunk", 1, 1, 1);
            }
            RefreshAll(%clientId,false);
            
            //Client::sendMessage(%clientId, 0, "You found " @ %item.description @ ".");
            
            UseSkill(%clientId, $SkillMining, True, True,5);
            
            //if( floor(getRandom() * 10) == 5)
            //{
                //Damage crystal
            GameBase::virtual(%target, "onDamage", %clientId, 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, %weapon);
            //}
        }
    }

    if(%obj == "Player")
        GameBase::virtual(%target, "onDamage", "", 1.0, "0 0 0", "0 0 0", %mom, "torso", "", %clientId, %weapon,%dmgMult);
}

function PickAxeSwing(%player, %length, %weapon)
{
	dbecho($dbechoMode, "PickAxeSwing(" @ %player @ ", " @ %length @ ")");

	%clientId = Player::getClient(%player);
	if(%clientId == "")
		%clientId = 0;

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
		return;
	%clientId.lastFireTime = %time;
	//=======================================================
    %mult = 1;
    %stamMult = 1;
    %dmgMult = 1;
    %mom = "0 0 0";
    if(fetchData(%clientId,"DoubleStrikeFlag"))
    {
        %to = fetchData(%clientId,"DoubleStrikeTimeout");
        if(getSimTime() <= %to)
            %mult = 2;
        else
            storeData(%clientId,"DoubleStrikeFlag",false);
    }
    else if(fetchData(%clientId,"HeavyStrikeFlag"))
    {
        %stamMult += 0.75;
        %dmgMult += 0.33;
        
        %etrans = Gamebase::getEyeTransform(%clientId);
        %dir = Word::getSubWord(%etrans,3,3);
        %mom = ScaleVector(%dir,$Ability::heavyStrikeForce);
        //%mom = Vector::getFromRot(%clientId,$Ability::heavyStrikeForce,15);
        playSound(SoundSwing7,Gamebase::getPosition(%clientId));
    }
    
    %stamMult *= %mult;
    WeaponStamina(%clientId,%weapon,%stamMult);

	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		%target = $los::object;
        DoMiningSwing(%clientId,%target,%weapon,%mom,%dmgMult);
        if(%mult > 1)
        {
            for(%i = 1; %i < %mult; %i++)
            {
                schedule("DoMiningSwing("@%clientId@","@%target@","@%weapon@",\""@%mom@"\","@%dmgMult@");",0.2*%i,%player);
            }
        }
	}
    
    if(fetchData(%clientId,"HeavyStrikeFlag"))
        storeData(%clientId,"HeavyStrikeFlag","");

	PostAttack(%clientId, %weapon);
}

function DoWoodChopSwing(%clientId,%target,%weapon)
{
    %obj = getObjectType(%target);
    %type = GameBase::getDataName(%target);
    if(%type == "TreeShape" || %type == "TreeShapeTwo")
    {
        PlaySound(SoundHitLeather, GameBase::getPosition(%clientId));
        
        if(Player::isAIcontrolled(%clientId)) return;
        
        %score = tree::chop(%clientId, Client::getOwnedObject(%clientId), %target);
        if(%score != "")
        {
            GiveThisStuff(%clientId, %score @" 1", this);
            RefreshAll(%clientId,false);
            Client::sendMessage(%clientId, 0, "You found " @ $beltitem[%score, "Name"] @ ".");

            useskill(%clientId, $SkillWoodCutting, True, True);
        }
        else
            useskill(%clientId, $SkillWoodCutting, False, True);
    }
    
    if(%obj == "Player")
        GameBase::virtual(%target, "onDamage", "", 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, %weapon);
}

function WoodAxeSwing(%player, %length, %weapon)
{
	dbecho($dbechoMode, "WoodAxeSwing(" @ %player @ ", " @ %length @ ")");
	//echo("crap");
	%clientId = Player::getClient(%player);
	if(%clientId == "")
		%clientId = 0;
		//if (%clientid.sleepMode == 1)
		//	return;
		//else if (%clientid.sleepMode == 2)
		//	return;
		//else if (Client::getGuiMode(%clientId) == $GuiModeInventory)
		//	return;
	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
		return;
	%clientId.lastFireTime = %time;
	//=======================================================
    %mult = 1;
    if(fetchData(%clientId,"DoubleStrikeFlag"))
    {
        %to = fetchData(%clientId,"DoubleStrikeTimeout");
        if(getSimTime() <= %to)
            %mult = 2;
        else
            storeData(%clientId,"DoubleStrikeFlag",false);
    }
    WeaponStamina(%clientId,%weapon,%mult);
	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		%target = $los::object;
		DoWoodChopSwing(%clientId,%target,%weapon);
        for(%i = 1; %i < %mult; %i++)
        {
            schedule("DoWoodChopSwing("@%clientId@","@%target@","@%weapon@");",0.2*%i,%player);
        }
	}
	PostAttack(%clientId, %weapon);
}

function DoRandomMining(%clientId, %crystal)
{
	dbecho($dbechoMode, "DoRandomMining(" @ %clientId @ ", " @ %crystal @ ")");

	%lastscore = "";
	for(%i = 1; $ItemList[Mining, %i] != ""; %i++)
	{
		%w1 = GetWord($ItemList[Mining, %i], 1) - %crystal.bonus[%i];
		%n = Cap( (%w1 * getRandom()) + (%w1 / 2), 0, %w1);
		%r = 1 + (CalculatePlayerSkill(%clientId, $SkillMining) * (1/10)) * getRandom();

		if(%n > %r)
			return %lastscore;

		%lastscore = GetWord($ItemList[Mining, %i], 0);
	}
	return %lastscore;
}

function tree::chop(%client, %player, %obj, %harvest)
{
    %skill = CalculatePlayerSkill(%client, $SkillWoodCutting);
    
    
    
    %r2 = %skill / ( %skill + 10 )/100*getRandom()/2 + getRandom()*2;
    if ( %r2 > 1 )
    {
        if(%obj.bonuscut >= 0)
            %r = 1+ (%skill+%obj.bonuscut) * (1/5) * getRandom();
        else
            %r = 1+ %skill * (1/5) * getRandom();
        %hit = GetWord($ItemList[WoodCutting, 1], 0);
        for(%i = 1; $ItemList[WoodCutting, %i] != ""; %i++)
        {
            %w1 = GetWord($ItemList[WoodCutting, %i], 1);
            %n = Cap( (%w1 * getRandom()) + (%w1 / 2), 0, %w1);
            //%r = 1 + ($PlayerSkill[%client, $SkillWoodCutting] * (1/5)) * getRandom();

            if(%n > %r)
            {
                return %hit;
            }
            %hit = GetWord($ItemList[WoodCutting, %i], 0);
        }

        return %hit;
    }
    else
    {
        Client::sendMessage(%client, 0, "You fail to split the wood with your axe.");	
        return "";
    }
}

$MeteorMiningListLength = 23;
$MeteorMiningList[1] = "Jade";
$MeteorMiningList[2] = "Turquoise";
$MeteorMiningList[3] = "Ruby";
$MeteorMiningList[4] = "Ruby";
$MeteorMiningList[5] = "Ruby";
$MeteorMiningList[6] = "Ruby";
$MeteorMiningList[7] = "Ruby"; //More likely to find this one
$MeteorMiningList[8] = "Topaz";
$MeteorMiningList[9] = "Topaz";
$MeteorMiningList[10] = "Topaz";
$MeteorMiningList[11] = "Sapphire";
$MeteorMiningList[12] = "Sapphire";
$MeteorMiningList[13] = "Sapphire";
$MeteorMiningList[14] = "Gold";
$MeteorMiningList[15] = "Gold";
$MeteorMiningList[16] = "Emerald";
$MeteorMiningList[17] = "Emerald";
$MeteorMiningList[18] = "Diamond";
$MeteorMiningList[19] = "Jade";
$MeteorMiningList[20] = "Turquoise";
$MeteorMiningList[21] = "Jade";
$MeteorMiningList[22] = "Turquoise";
$MeteorMiningList[23] = "Jade";
$MeteorMineChunkOdds = 10;
function MineMeteorCrystal()
{
    %i = getIntRandomMT(1,$MeteorMiningListLength);//round(1+getRandomMT()*($MeteorMiningListLength-1));
    return %i;
}

function PostAttack(%clientId, %weapon)
{
	dbecho($dbechoMode, "PostAttack(" @ %clientId @ ", " @ %weapon @ ")");

	if($postAttackGraphBar)
	{
		%t = GetDelay(%weapon);
		%ticks = 30;
		%chunks = 10;

		%chunklen = floor(%ticks / %chunks);
		%d = %t / %chunks;

		for(%i = 0; %i <= %chunks; %i++)
			schedule("bottomprint(" @ %clientId @ ", \" \" @ String::create(\"•\", " @ %ticks @ " - (" @ %chunklen @ " * " @ %i @ ")) @ \"\", " @ %d @ " + 0.25);", %d * %i);
	}

	if(%weapon == CastingBlade)
	{
		%x = floor(%clientId.castingBladeBeat);
		if(%x != 0)
		{
			if(%x == 1)
				playSound(MClip5, GameBase::getPosition(%clientId));
			else if(%x == 2)
				playSound(MClip6, GameBase::getPosition(%clientId));
		}

		%x++;
		if(%x > 2) %x = 1;

		%clientId.castingBladeBeat = %x;
	}
}

function GetRange(%weapon)
{
	dbecho($dbechoMode, "GetRange(" @ %weapon @ ")");

	%minRange = 2.0;
	if($WeaponRange[%weapon] != "")
		return %minRange + $WeaponRange[%weapon];
	else
		return %minRange + $RangeTable[$AccessoryVar[%weapon, $AccessoryType]];
}
function GetDelay(%weapon)
{
	dbecho($dbechoMode, "GetDelay(" @ %weapon @ ")");

	if($WeaponDelay[%weapon] != "")
		return $WeaponDelay[%weapon];
	else
	{
		%a = 3.0;
		%b = Cap($AccessoryVar[%weapon, $Weight] / %a, 1.0, "inf");
		%c = %b * $DelayFactorTable[$AccessoryVar[%weapon, $AccessoryType]];
		return %c;
	}
}

function GenerateItemCost(%item)
{
	dbecho($dbechoMode, "GenerateItemCost(" @ %item @ ")");

	if($HardcodedItemCost[%item] != "")
		return $HardcodedItemCost[%item];

	%cft = $CostFactorTable[$AccessoryVar[%item, $AccessoryType]];

	%a = GetDelay(%item);
	if(floor(%a) == 0)
		%a = 1.0;

	%b6 = AddItemSpecificPoints(%item, "6") * 1.2;	//ATK
	%b7 = AddItemSpecificPoints(%item, "7") / 6;	//DEF
	%b3 = AddItemSpecificPoints(%item, "3") / 6;	//MDEF

	%extracost = 0;
	for(%i = 1; $SmithCombo[%i] != ""; %i++)
	{
		for(%j = 0; (%w = GetWord($SmithComboResult[%i], %j)) != -1; %j+=2)
		{
			if(String::ICompare(%item, %w) == 0)
			{
				%n = GetWord($SmithComboResult[%i], %j+1);
				for(%k = 0; (%w2 = GetWord($SmithCombo[%i], %k)) != -1; %k+=2)
				{
					%n2 = GetWord($SmithCombo[%i], %k+1);
					%extracost += (GenerateItemCost(%w2) * %n2);
				}
				%extracost *= %n;
				break;
			}
		}
		if(%extracost > 0)
			break;
	}
	%extracost = %extracost * ($ResalePercentage / 100);
	
	%c = (%b6 + %b7 + %b3) / %a;
	%d = Cap(0.01 * pow(%c, 3.7), 0, "inf");
	%e = Cap(%d * %cft, 1, "inf");
	%f = floor(%e + %extracost);

	return %f;
}



//WIP Entry items
ItemImageData ShivImage
{
	shapeFile  = "dagger";
	mountPoint = 0;

	weaponType = 0;
	reloadTime = 0;
	fireTime = GetDelay(Shiv);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData Shiv
{
	heading = "bWeapons";
	description = "Shiv";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = ShivImage;
	price = 0;
	showWeaponBar = true;
};
function ShivImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Shiv), Shiv);
}

ItemImageData CrudeAxeImage
{
	shapeFile  = "hatchet";
	mountPoint = 0;

	weaponType = 0;
	reloadTime = 0;
	fireTime = GetDelay(CrudeAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData CrudeAxe
{
	heading = "bWeapons";
	description = "Crude Axe";
	className = "Weapon";
	shapeFile  = "hatchet";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = CrudeAxeImage;
	price = 0;
	showWeaponBar = true;
};
function CrudeAxeImage::onFire(%player, %slot)
{
	WoodAxeSwing(%player, GetRange(CrudeAxe), CrudeAxe);
}

//****************************************************************************************************
//   KNIFE
//****************************************************************************************************

ItemImageData KnifeImage
{
	shapeFile  = "dagger";
	mountPoint = 0;

	weaponType = 0;
	reloadTime = 0;
	fireTime = GetDelay(Knife);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData Knife
{
	heading = "bWeapons";
	description = "Knife";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = KnifeImage;
	price = 0;
	showWeaponBar = true;
};
function KnifeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Knife), Knife);
}

//****************************************************************************************************
//   DAGGER
//****************************************************************************************************

ItemImageData DaggerImage
{
	shapeFile  = "dagger";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Dagger);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData Dagger
{
	heading = "bWeapons";
	description = "Dagger";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = DaggerImage;
	price = 0;
	showWeaponBar = true;
};
function DaggerImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Dagger), Dagger);
}


ItemImageData MeteorDaggerImage
{
	shapeFile  = "dagger";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MeteorDagger);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData MeteorDagger
{
	heading = "bWeapons";
	description = "Meteor Dagger";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = MeteorDaggerImage;
	price = 0;
	showWeaponBar = true;
};
function MeteorDaggerImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(MeteorDagger), MeteorDagger);
}


ItemImageData SpellEffectAura1Image
{
	shapeFile  = "AURA_ENERGY"; //"AURA_FIRE_2";
	mountPoint = 4;
    mountOffset = {0.0, 0.0, 0.0};
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MeteorDagger);
	minEnergy = 0;
	maxEnergy = 0;

};
ItemData SpellEffectAura1
{
	heading = "bWeapons";
	description = "SpellEffectAura";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = SpellEffectAura1Image;
	price = 0;
	showWeaponBar = true;
};

ItemImageData SpellEffectAura2Image
{
	shapeFile  = "AURA_FIRE_2";
	mountPoint = 4;
    mountOffset = {0.0, 0.0, 0.0};
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MeteorDagger);
	minEnergy = 0;
	maxEnergy = 0;


};
ItemData SpellEffectAura2
{
	heading = "bWeapons";
	description = "SpellEffectAura";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = SpellEffectAura2Image;
	price = 0;
	showWeaponBar = true;
};

ItemImageData SpellEffectAura3Image
{
	shapeFile  = "AURA_ABSORB";
	mountPoint = 4;
    mountOffset = {0.0, 0.0, 0.0};
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MeteorDagger);
	minEnergy = 0;
	maxEnergy = 0;


};
ItemData SpellEffectAura3
{
	heading = "bWeapons";
	description = "SpellEffectAura";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = SpellEffectAura3Image;
	price = 0;
	showWeaponBar = true;
};

ItemImageData StaffPoleImage
{
	shapeFile  = "quarterstaff";
	mountPoint = 0;
    mountOffset = { 0, 0, 0 };
	//mountRotation = { 0, 1.01, 0};
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(LongStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	//sfxFire = SoundSwing3;
	//sfxActivate = AxeSlash2;
};
ItemData StaffPole
{
	heading = "bWeapons";
	description = "Long Staff";
	className = "Weapon";
	shapeFile  = "longstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = StaffPoleImage;
	price = 0;
	showWeaponBar = true;
};

$MageStaff[NoviceStaff,MaxMana] = 50;
$MageStaff[NoviceStaff,ManaCost] = 1;
$MageStaff[NoviceStaff,AttunementCost] = 10;
$MageStaff[NoviceStaff,AttunementTime] = 2;

ItemImageData NoviceStaffImage
{
	shapeFile  = "saphire";
	mountPoint = 0;
    mountOffset = { 0, -0.2, 0 };
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(NoviceStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = AxeSlash2;
};

ItemData NoviceStaff
{
    heading = "bWeapons";
	description = "Novice Staff";
	className = "Weapon";
	shapeFile  = "saphire";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = NoviceStaffImage;
	price = 0;
	showWeaponBar = true;
};

function NoviceStaff::onMount(%player,%item)
{
    %clientId = Player::getClient(%player);
    //if(fetchData(%clientId,"attunedWeapon") == %item)
    //{
    //    %clientId = Player::getClient(%player);
    //    %weapMana = fetchData(%clientId,"attunedWeaponMana");
    //    %maxMana = $MageStaff[%item,MaxMana];
    //    bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <f0>"@ %weapMana @"<f1>/<f0>"@%maxMana,2);
    //    
    //}
    Player::mountItem(%player,StaffPole,$ExtraImageSlot1);
}

function NoviceStaff::onUnmount(%player,%item)
{
    Player::unmountItem(%player,$ExtraImageSlot1);
}

function NoviceStaffImage::onFire(%player,%slot)
{
    MagesStaffAttack(%player,NoviceStaff,$MageStaff[NoviceStaff,ManaCost],SoundEnergyTurretFire,BlueStaffBolt,80*2);
}

$MageStaff[MagesStaff,MaxMana] = 500;
$MageStaff[MagesStaff,ManaCost] = 5;
$MageStaff[MagesStaff,AttunementCost] = 30;
$MageStaff[MagesStaff,AttunementTime] = 4;

ItemImageData MagesStaffImage
{
	shapeFile  = "emerald";
	mountPoint = 0;
    mountOffset = { 0, -0.2, 0 };
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MagesStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = AxeSlash2;
};

ItemData MagesStaff
{
    heading = "bWeapons";
	description = "Mage's Staff";
	className = "Weapon";
	shapeFile  = "emerald";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = MagesStaffImage;
	price = 0;
	showWeaponBar = true;
};

function MagesStaff::onMount(%player,%item)
{
    %clientId = Player::getClient(%player);
    if(fetchData(%clientId,"attunedWeapon") == %item)
    {
        %clientId = Player::getClient(%player);
        %weapMana = fetchData(%clientId,"attunedWeaponMana");
        %maxMana = $MageStaff[%item,MaxMana];
        bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <f0>"@ %weapMana @"<f1>/<f0>"@%maxMana,2);
        
    }
    Player::mountItem(%player,StaffPole,$ExtraImageSlot1);
}

function MagesStaff::onUnmount(%player,%item)
{
    Player::unmountItem(%player,$ExtraImageSlot1);
}

function MagesStaffImage::onFire(%player,%slot)
{
    MagesStaffAttack(%player,MagesStaff,$MageStaff[MagesStaff,ManaCost],SoundEnergyTurretFire,BlueStaffBolt,80*2);
}

$MageStaff[FireStaff,MaxMana] = 600;
$MageStaff[FireStaff,ManaCost] = 15;
$MageStaff[FireStaff,AttunementCost] = 45;
$MageStaff[FireStaff,AttunementTime] = 5;

ItemImageData FireStaffImage
{
	shapeFile  = "ruby";
	mountPoint = 0;
    mountOffset = { 0, -0.2, 0 };
	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(FireStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = AxeSlash2;
};

ItemData FireStaff
{
    heading = "bWeapons";
	description = "Fire Staff";
	className = "Weapon";
	shapeFile  = "ruby";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = FireStaffImage;
	price = 0;
	showWeaponBar = true;
};

function FireStaff::onMount(%player,%item)
{
    %clientId = Player::getClient(%player);
    
    Player::mountItem(%player,StaffPole,$ExtraImageSlot1);
}

function FireStaff::onUnmount(%player,%item)
{
    Player::unmountItem(%player,$ExtraImageSlot1);
}

function FireStaffImage::onFire(%player,%slot)
{
    MagesStaffAttack(%player,FireStaff,$MageStaff[FireStaff,ManaCost],LaunchFB,FireBallBolt,80*2);
}

$MageStaff[ThornStaff,MaxMana] = 100;
$MageStaff[ThornStaff,ManaCost] = 5;
$MageStaff[ThornStaff,AttunementCost] = 10;
$MageStaff[ThornStaff,AttunementTime] = 2;
$MageStaff[ThornStaff,Rate] = 1;

ItemImageData ThornStaffImage
{
	shapeFile = "mrtwig";
	mountPoint = 0;
    mountOffset = { 0, -0.6, 0 };
	mountRotation = { -1.57 ,0 ,0 };
	weaponType = 2;
	projectileType = ThornStaffBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
    lightRadius = 2;
	lightTime = 1;
	lightColor = { 0.25, 0.25, 0.85 };
    
	sfxFire = SoundELFIdle;//SoundRepairItem;
	sfxActivate = AxeSlash2;
};

ItemData ThornStaff
{
    heading = "bWeapons";
	description = "Staff of Thorns";
	className = "Weapon";
	shapeFile  = "mrtwig";
	hudIcon = "mrtwig";
	shadowDetailMask = 4;
	imageType = ThornStaffImage;
	price = 0;
	showWeaponBar = true;
};

function ThornStaff::onMount(%player,%item)
{
    %clientId = Player::getClient(%player);
    if(fetchData(%clientId,"attunedWeapon") == %item)
    {
        %weapMana = fetchData(%clientId,"attunedWeaponMana");
        %maxMana = $MageStaff[%item,MaxMana];
        bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <f0>"@ %weapMana @"<f1>/<f0>"@%maxMana,2);
        
    }
    //Player::mountItem(%player,StaffPole,$ExtraImageSlot1);
}

function ThornStaff::onUnmount(%player,%item)
{
    //Player::unmountItem(%player,$ExtraImageSlot1);
}

function ThornStaffImage::onActivate(%player,%slot)
{
    echo("ThornStaffImage::onActivate("@%player@","@%slot@")");
}
//function ThornStaffImage::onFire(%player,%slot)
//{
//    echo("ThornStaffImage::onFire("@%player@","@%slot@")");
//    %clientId = Player::getClient(%player);
//    if(%clientId == "")
//		%clientId = 0;
//
//	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
//	%time = getIntegerTime(true) >> 5;
//	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
//		return;
//	%clientId.lastFireTime = %time;
//	//=======================================================
//    
//    if(fetchData(%clientId,"attuningToWeapon"))
//    {
//        CancelAttunement(%clientId);
//        Player::trigger(%player,%slot,false);
//        return;
//    }
//    
//    if(fetchData(%clientId,"attunedWeapon") != %item)
//    {
//        Client::sendMessage(%clientId,$MsgRed,"You are not attuned to this staff!");
//        Player::trigger(%player,%slot,false);
//        return;
//    }
//    
//    %weapMana = fetchData(%clientId,"attunedWeaponMana");
//    
//    if(%weapMana < %manacost)
//    {
//        Player::trigger(%player,%slot,false);
//        Client::sendMessage(%clientId,$MsgRed,"Your staff is too low on mana. Use #recharge");
//    }
//    
//    if(%weapMana != "")
//    {
//        %item = Player::getMountedItem(%player,%slot);
//        %newMana = fetchData(%clientId,"attunedWeaponMana");
//        %maxMana = $MageStaff[%item,MaxMana];
//        bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <F0>"@ %newMana @"<F1>/<F0>"@%maxMana,1);
//    }
//}

//function ThornStaffBolt::onAcquire(%this, %player, %target)
//{
//    echo("ThornStaffBolt::onAcquire("@%this@","@%player@","@%target@")");
//	%client = Player::getClient(%player);
//	
//    %player.atkDisabled = false;
//    %weap = Player::getMountedItem(%player,$WeaponSlot);
//    %player.staffThing = %weap;
//    %player.staffTime = getSimTime() + $MageStaff[%weap,Rate];
//    %mana = fetchData(%client,"attunedWeaponMana");
//    %cost = $MageStaff[%weap,ManaCost];
//    
//    if(%mana < %cost)
//    {
//        Client::sendMessage(%client, $MsgRed, "Your staff is too low on mana. Use #recharge");
//        Player::trigger(%player, $WeaponSlot, false);
//        return;
//    }
//    
//	if(%target == %player) 
//	{
//		return;
//	}
//	else 
//	{
//		%player.staffTarget = %target;
//		if(getObjectType(%player.staffTarget) == "Player") 
//		{
//			%rclient = Player::getClient(%player.staffTarget);
//			%name = Client::getName(%rclient);
//		}
//		else 
//        {
//            %player.atkDisabled = true;
//            Player::trigger(%player,$WeaponSlot,false);
//            return;
//        }
//	}
//    
//	//HealCheckLoop(%client,%player,%weap,1);
//}

//function ThornStaffBolt::onRelease(%this, %player)
//{
//    echo("ThornStaffBolt::onRelease("@%this@","@%player@")");
//    %player.staffTime = "";
//    %player.staffThing = "";
//}

function ThornStaffBolt::damageTarget(%target, %timeSlice, %damPerSec, %enDrainPerSec, %pos, %vec, %mom, %shooterId)
{
    echo(%shooterId.stopDmgTgt);
    if(%shooterId.stopDmgTgt)
        return;
    
    %player = Client::getOwnedObject(%shooterId);
    
    if(fetchData(%shooterId,"attuningToWeapon"))
    {
        CancelAttunement(%shooterId);
        Player::trigger(%player,$WeaponSlot,false);
        %shooterId.stopDmgTgt = true;
        schedule(%shooterId@".stopDmgTgt = false;",0.5);
        return;
    }
    
    %weap = Player::getMountedItem(%player,$WeaponSlot);
    if(fetchData(%shooterId,"attunedWeapon") != %weap)
    {
        Client::sendMessage(%shooterId,$MsgRed,"You are not attuned to this staff!");
        Player::trigger(%player,$WeaponSlot,false);
        %shooterId.stopDmgTgt = true;
        schedule(%shooterId@".stopDmgTgt = false;",0.5);
        return;
    }
    
    %mana = fetchData(%shooterId,"attunedWeaponMana");
    if(%mana < $MageStaff[%weap,ManaCost])
    {
        Client::sendMessage(%shooterId,$MsgRed,"Your staff is too low on mana. Use #recharge");
        Player::trigger(%player,$WeaponSlot,false);
        return;
    }
    
    %player.staffTimeAccum += %timeSlice;
    if(%player.staffTimeAccum >= $MageStaff[%weap,Rate])
    {
        %player.staffTimeAccum = 0;
        storeData(%shooterId,"attunedWeaponMana",$MageStaff[%weap,ManaCost],"dec");
        Gamebase::virtual(%target,"onDamage",$StaffDamageType,1.0,"0 0 0","0 0 0","0 0 0","torso","",%shooterId,%weap);
        
        if(%mana != "")
        {
            %newMana = fetchData(%shooterId,"attunedWeaponMana");
            %maxMana = $MageStaff[%weap,MaxMana];
            bottomprint(%shooterId,"<jc><f1>"@RPGItem::getDesc(%weap) @" Mana: <F0>"@ %newMana @"<F1>/<F0>"@%maxMana,1);
        }
    }
}

//function ThornStaffBolt::checkDone(%this, %player)
//{
//    echo("ThornStaffBolt::checkDone("@%this@","@%player@")");
//	if(Player::isTriggered(%player,$WeaponSlot) && Player::getMountedItem(%player,$WeaponSlot) == %player.staffThing && %player.staffTarget != -1) 
//	{
//        %weap = %player.staffThing;
//        %client = Player::getClient(%player);
//        
//        %cost = $MageStaff[%weap,ManaCost];
//        
//        %object = %player.staffTarget;
//        
//        if(getSimTime() >= %player.staffTime)
//        {
//            %natCast = CalculatePlayerSkill(%client,$SkillNatureCasting);
//            %atkIdx = Word::FindWord($AccessoryVar[%weap, $SpecialVar],$SpecialVarATK)+1;
//            %value = getWord($AccessoryVar[%weap, $SpecialVar],%atkIdx);
//            //%amnt = %value + floor(%natCast/20);
//            //%rclient = Player::getClient(%object);
//            
//            //refreshHP(%rclient,%amnt/$TribesDamageToNumericDamage);
//            storeData(%client,"attunedWeaponMana",%cost,"dec");
//            
//            //Gamebase::virtual(%object,"onDamage",$StaffDamageType,%value,"0 0 0","0 0 0","0 0 0","torso","",%client,%weap);
//        }
//        
//        %mana = fetchData(%client,"attunedWeaponMana");
//        if(%mana < %cost)
//        {
//            Client::sendMessage(%client, $MsgRed, "Your staff is too low on mana. Use #recharge");
//            Player::trigger(%player, $WeaponSlot, false);
//            return;
//        }
//        
//        if(%mana != "")
//        {
//            %newMana = fetchData(%clientId,"attunedWeaponMana");
//            %maxMana = $MageStaff[%weap,MaxMana];
//            bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%weap) @" Mana: <F0>"@ %newMana @"<F1>/<F0>"@%maxMana,1);
//        }
//	}
//}


$MageStaff[HealerStaff,MaxMana] = 600;
$MageStaff[HealerStaff,ManaCost] = 15;
$MageStaff[HealerStaff,AttunementCost] = 45;
$MageStaff[HealerStaff,AttunementTime] = 5;
$MageStaff[HealerStaff,Rate] = 2;
ItemImageData HealerStaffImage
{
	shapeFile = "ruby";
	mountPoint = 0;
	weaponType = 2;
	projectileType = HealBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
	lightRadius = 1;
	lightTime = 1;
	lightColor = { 0.25, 1, 0.25 };

	sfxFire = DeActivateWA;//SoundRepairItem;
	sfxActivate = AxeSlash2;
};

ItemData HealerStaff
{
    heading = "bWeapons";
	description = "Healer Staff";
	className = "Weapon";
	shapeFile  = "ruby";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = HealerStaffImage;
	price = 0;
	showWeaponBar = true;
};

function HealerStaff::onMount(%player,%item)
{
    %clientId = Player::getClient(%player);
    if(fetchData(%clientId,"attunedWeapon") == %item)
    {
        %weapMana = fetchData(%clientId,"attunedWeaponMana");
        %maxMana = $MageStaff[%item,MaxMana];
        bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <f0>"@ %weapMana @"<f1>/<f0>"@%maxMana,2);
        
    }
    Player::mountItem(%player,StaffPole,$ExtraImageSlot1);
}

function HealerStaff::onUnmount(%player,%item)
{
    Player::unmountItem(%player,$ExtraImageSlot1);
}

function HealBolt::onAcquire(%this, %player, %target)
{
    echo("HealBolt::onAcquire("@%this@","@%player@","@%target@")");
	%client = Player::getClient(%player);
	%player.fixingDisabled = false;
	
    %weap = Player::getMountedItem(%player,$WeaponSlot);
    %player.healingThing = %weap;
    %player.healTime = getSimTime() + $MageStaff[%weap,Rate];
    %mana = fetchData(%client,"attunedWeaponMana");
    %cost = $MageStaff[%weap,ManaCost];
    
    if(%mana < %cost)
    {
        Client::sendMessage(%client, $MsgRed, "Your staff is too low on mana. Use #recharge");
        Player::trigger(%player, $WeaponSlot, false);
        return;
    }
    
	if(%target == %player) 
	{
		%player.repairTarget = -1;
        
        
		if(GameBase::getDamageLevel(%player) != 0) 
		{
			
            %player.repairTarget = %player;
            Client::sendMessage(%client, 0, "Heal self...");
			
		}
		else 
		{
			Client::sendMessage(%client,0,"Nothing in range");
			Player::trigger(%player, $WeaponSlot, false);
			return;
		}
	}
	else 
	{
		%player.repairTarget = %target;
		if(getObjectType(%player.repairTarget) == "Player") 
		{
			%rclient = Player::getClient(%player.repairTarget);
			%name = Client::getName(%rclient);
		}
		else 
        {%player.fixingDisabled = true;Player::trigger(%player,$WeaponSlot,false);return;}
        
		if(GameBase::getDamageLevel(%player.repairTarget) == 0) 
		{
			Client::sendMessage(%client,0,%name @ " is already healed.");
			Player::trigger(%player,$WeaponSlot,false);
			%player.repairTarget = -1;
			return;
		}
		if(getObjectType(%player.repairTarget) == "Player") 
		{
			Client::sendMessage(%rclient,0,"Being healed by " @ Client::getName(%client));
		}
		Client::sendMessage(%client,0,"Healing " @ %name @"...");
	}
    
	//HealCheckLoop(%client,%player,%weap,1);
}

//function HealCheckLoop(%clientId,%player,%item,%delay)
//{
//    %mana = fetchData(%clientId,"MANA");
//    %cost = $MageStaff[%item,ManaCost];
//    if(%mana < %cost)
//    {
//        
//    }
//    if(%player.repairTarget == -1)
//    {
//        
//    }
//    else
//    {
//    
//    }
//}
function HealBolt::onRelease(%this, %player)
{
    echo("HealBolt::onRelease("@%this@","@%player@")");
	%object = %player.repairTarget;
	if(%object != -1) 
	{
		%client = Player::getClient(%player);
		if(%object == %player) 
		{
			Client::sendMessage(%client,0,"Healing Stopped");
		}
		else 
		{
			if(GameBase::getDamageLevel(%object) == 0) 
			{
				Client::sendMessage(%client,0,"Healing Done");
			}
			else 
			{
				Client::sendMessage(%client,0,"Healing Stopped");
			}
		}
	}
    %player.healTime = "";
    %player.healingThing = "";
}

function HealBolt::checkDone(%this, %player)
{
    echo("HealBolt::checkDone("@%this@","@%player@")");
	if(Player::isTriggered(%player,$WeaponSlot) && Player::getMountedItem(%player,$WeaponSlot) == %player.healingThing && %player.repairTarget != -1) 
	{
        %weap = %player.healingThing;
        %client = Player::getClient(%player);
        
        %cost = $MageStaff[%weap,ManaCost];
        
        %object = %player.repairTarget;
        
        if(getSimTime() >= %player.healTime)
        {
            %defCast = CalculatePlayerSkill(%client,$SkillDefensiveCasting);
            %atkIdx = Word::FindWord($AccessoryVar[%weap, $SpecialVar],$SpecialVarATK)+1;
            %value = getWord($AccessoryVar[%weap, $SpecialVar],%atkIdx);
            %amnt = %value + floor(%defCast/20);
            %rclient = Player::getClient(%object);
            
            refreshHP(%rclient,-%amnt/$TribesDamageToNumericDamage);
            storeData(%client,"attunedWeaponMana",%cost,"dec");
            
            if(%rclient != %client)
            {
                Client::sendMessage(%rclient, $MsgWhite, "You healed for "@%amnt@" HP");
                Client::sendMessage(%client, $MsgWhite, "You healed "@ Client::getName(%rclient) @" for "@%amnt@" HP");
            }
            else
            {
                Client::sendMessage(%client, $MsgWhite, "You healed yourself for "@%amnt@" HP");
            }
            
            playSound(ActivateAR,Gamebase::getPosition(%rclient));
            bottomprint(%client,"<jc><f1>"@RPGItem::getDesc(%weap) @" Mana: <f0>"@ fetchData(%client,"attunedWeaponMana") @"<f1>/<f0>"@$MageStaff[%weap,MaxMana],2);
            %player.healTime = getSimTime() + $MageStaff[%weap,Rate];
        }
        
        %mana = fetchData(%client,"attunedWeaponMana");
        if(%mana < %cost)
        {
            Client::sendMessage(%client, $MsgRed, "Your staff is too low on mana. Use #recharge");
            Player::trigger(%player, $WeaponSlot, false);
            return;
        }

		if(%object == %player) 
		{
			if(GameBase::getDamageLevel(%player) == 0) 
			{
				Player::trigger(%player,$WeaponSlot,false);
				return;
			}
		}
		else 
		{
			if(GameBase::getDamageLevel(%object) == 0) 
			{
				Player::trigger(%player,$WeaponSlot,false);
				return;
			}
		}
	}
}

//function FireStaffImage::onFire(%player,%slot)
//{
//    MagesStaffAttack(%player,FireStaff,$MageStaff[FireStaff,ManaCost],LaunchFB,FireBallBolt,80*2);
//}

function MagesStaffAttack(%player,%item,%manacost,%sound,%projectile,%tgtRange)
{
    dbecho($dbechoMode, "MagesStaffAttack(" @ %player @ ", " @ %item @ ", "@ %projectile @")");

	%clientId = Player::getClient(%player);
	if(%clientId == "")
		%clientId = 0;

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastFireTime <= $fireTimeDelay)
		return;
	%clientId.lastFireTime = %time;
	//=======================================================
    
    if(fetchData(%clientId,"attuningToWeapon"))
    {
        CancelAttunement(%clientId);
        return;
    }
    
    if(fetchData(%clientId,"attunedWeapon") != %item)
    {
        Client::sendMessage(%clientId,$MsgRed,"You are not attuned to this staff!");
        return;
    }
    
    %weapMana = fetchData(%clientId,"attunedWeaponMana");
    
    if(%weapMana >= %manacost)
    {
        storeData(%clientId,"attunedWeaponMana",%manacost,"dec");
        if(%projectile != "")
        {
            %tgt = "";
            %tgtPos = "";
            if(%tgtRange != "")
            {
                $los::object = "";
                $los::position = "";
                if(Gamebase::getLOSInfo(%player,%tgtRange))
                {
                    %tgt = $los::object;
                    %tgtPos = $los::position;
                }
            }
            if(%sound != "")
                playSound(%sound,Gamebase::getPosition(%clientId));
            if(%tgtPos != "")
            {
                %pos = Word::getSubWord(Gamebase::getMuzzleTransform(%player),9,3);
                %dir = Vector::Normalize(Vector::sub(%tgtPos,%pos));
                %trans = "0 0 0 "@ %dir @" 0 0 0 "@ %pos;
            }
            else
                %trans = Gamebase::getMuzzleTransform(%player);
                
            Projectile::spawnProjectile(%projectile,%trans,%player,Item::getVelocity(%player),%tgt);
        }
    }
    else
        Client::sendMessage(%clientId,$MsgRed,"Your staff is too low on mana. Use #recharge");
    
    if(%weapMana != "")
    {
        %newMana = fetchData(%clientId,"attunedWeaponMana");
        %maxMana = $MageStaff[%item,MaxMana];
        bottomprint(%clientId,"<jc><f1>"@RPGItem::getDesc(%item) @" Mana: <F0>"@ %newMana @"<F1>/<F0>"@%maxMana,1);
    }
    
    PostAttack(%clientId,%item);
}

function CancelAttunement(%clientId)
{
    storeData(%clientId,"attuningToWeapon","");
    playSound(UnravelAM,Gamebase::getPosition(%clientId));
    Client::sendMessage(%clientId,$MsgWhite,"Attunement cancelled.");
}

function BeginAttuningWeapon(%clientId,%item)
{
    %mana = fetchData(%clientId,"MANA");
    if(%mana >= $MageStaff[%item,AttunementCost])
    {
        storeData(%clientId,"attuningToWeapon",true);
        %atw = fetchData(%clientId,"attunedWeapon");
        if(%atw != "")
            Client::sendMessage(%clientId,$MsgWhite,"Unattuning to "@RPGItem::getDesc(%atw)@" and attuning to "@RPGItem::getDesc(%item)@". (Attack to cancel)");
        else
            Client::sendMessage(%clientId,$MsgWhite,"Attuning to "@RPGItem::getDesc(%item)@". (Attack to cancel)");
            
        playSound(ActivateTR,Gamebase::getPosition(%clientId));
        schedule("FinishWeaponAttunement("@%clientId@","@%item@");",$MageStaff[%item,AttunementTime]);
        //refreshMANA(%clientId,$MageStaff[%item,AttunementCost]);
    }
    else
        Client::sendMessage(%clientId,$MsgRed,"Not enough mana to attune to "@RPGItem::getDesc(%item)@". ("@%mana@"/"@$MageStaff[%item,AttunementCost]@")");
}

function FinishWeaponAttunement(%clientId,%item)
{
    if(fetchData(%clientId,"attuningToWeapon"))
    {
        playSound(ActivateTD,Gamebase::getPosition(%clientId));
        Client::sendMessage(%clientId,$MsgBeige,"You attuned to "@RPGItem::getDesc(%item)@". Current Weapon Mana: "@$MageStaff[%item,AttunementCost]@"/"@$MageStaff[%item,MaxMana]);
        storeData(%clientId,"attunedWeapon",%item);
        storeData(%clientId,"attunedWeaponMana",$MageStaff[%item,AttunementCost]);
        storeData(%clientId,"attuningToWeapon","");
        refreshMANA(%clientId,$MageStaff[%item,AttunementCost]);
    }
}


ItemImageData FireChage1Image
{
	shapeFile = "shotgunbolt";
	mountPoint = 0;
    mountOffset = { 0, 0, 0 };
	mountRotation = { 0 ,0 ,0 };
	weaponType = 2;
	//projectileType = ThornStaffBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
    lightRadius = 2;
	lightTime = 1;
	lightColor = { 0.25, 0.25, 0.85 };
    
	sfxFire = SoundELFIdle;//SoundRepairItem;
	sfxActivate = ActivateBF;
};

ItemData FireChage1Item
{
    heading = "bWeapons";
	description = "Staff of Thorns";
	className = "Weapon";
	shapeFile  = "bullet";
	hudIcon = "mrtwig";
	shadowDetailMask = 4;
	imageType = FireChage1Image;
	price = 0;
	showWeaponBar = false;
};

ItemImageData FireChage2Image
{
	shapeFile = "fire_small";
	mountPoint = 0;
    mountOffset = { 0, 0, 0 };
	mountRotation = { 0,0 ,0 };
	weaponType = 2;
	//projectileType = ThornStaffBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
    lightRadius = 2;
	lightTime = 1;
	lightColor = { 0.25, 0.25, 0.85 };
    
	sfxFire = SoundELFIdle;//SoundRepairItem;
	sfxActivate = ActivateAB;
};

ItemData FireChage2Item
{
    heading = "bWeapons";
	description = "Staff of Thorns";
	className = "Weapon";
	shapeFile  = "bullet";
	hudIcon = "mrtwig";
	shadowDetailMask = 4;
	imageType = FireChage2Image;
	price = 0;
	showWeaponBar = false;
};


ItemImageData FireChage3Image
{
	shapeFile = "fire_medium";
	mountPoint = 0;
    mountOffset = { 0, 0, 0 };
	mountRotation = { 0 ,0 ,0 };
	weaponType = 2;
	//projectileType = ThornStaffBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
    lightRadius = 2;
	lightTime = 1;
	lightColor = { 0.25, 0.25, 0.85 };
    
	sfxFire = SoundELFIdle;//SoundRepairItem;
	sfxActivate = rocketExplosion;
};

ItemData FireChage3Item
{
    heading = "bWeapons";
	description = "Staff of Thorns";
	className = "Weapon";
	shapeFile  = "bullet";
	hudIcon = "mrtwig";
	shadowDetailMask = 4;
	imageType = FireChage3Image;
	price = 0;
	showWeaponBar = false;
};

ItemImageData ChargeMagicImage
{
	shapeFile = "bullet";
	mountPoint = 0;
    mountOffset = { 0, -0.6, 0 };
	mountRotation = { -1.57 ,0 ,0 };
	weaponType = 2;
	//projectileType = ThornStaffBolt;
	minEnergy = 0;
	maxEnergy = 0;
	lightType = 3;
    lightRadius = 2;
	lightTime = 1;
	lightColor = { 0.25, 0.25, 0.85 };
    
	sfxFire = SoundELFIdle;//SoundRepairItem;
	sfxActivate = SoundRepairItem;
};

ItemData ChargeMagicItem
{
    heading = "bWeapons";
	description = "Staff of Thorns";
	className = "Weapon";
	shapeFile  = "bullet";
	hudIcon = "mrtwig";
	shadowDetailMask = 4;
	imageType = ChargeMagicImage;
	price = 0;
	showWeaponBar = false;
};

function ChargeMagicImage::onActivate(%player,%slot)
{
    echo("ChargeMagicImage::onActivate("@%player@","@%slot@")");
    %player.chargeStartTime = getSimTime();
    %player.chargeStage = -1;
}

function ChargeMagicImage::onDeactivate(%player,%slot)
{
    echo("ChargeMagicImage::onDeactivate("@%player@","@%slot@")");
    %trans = Gamebase::getEyeTransform(%player);
    %vel = Item::getVelocity(%player);
    echo(%player.chargeStage);
    if(%player.chargeStage == 0)
    {
        Projectile::spawnProjectile(Firebolt,%trans,%player,%vel);
        playSound(HitPawnDT,Gamebase::getPosition(%player));
    }
    else if(%player.chargeStage == 1)
    {
        Projectile::spawnProjectile(Fireball,%trans,%player,%vel);
        playSound(ActivateAB,Gamebase::getPosition(%player));
    }
    else if(%player.chargeStage == 2)
    {
        Projectile::spawnProjectile(Melt,%trans,%player,%vel);
        playSound(LaunchFB,Gamebase::getPosition(%player));
    }
    
    Player::unmountItem(%player,7);
    %player.chargeStartTime = "";
    %player.chargeStage = "";
}

$Charge::spacerLen = 20;
$ChargeTime = 4;

function ChargeMagicImage::onUpdateFire(%player,%slot)
{
    echo("ChargeMagicImage::onUpdateFire("@%player@","@%slot@")");
    
    %clientId = Player::getClient(%player);
    %timeDiff = getSimTime() - %player.chargeStartTime;
    
    if(%timeDiff <= $ChargeTime)
    {
        echo(%timeDiff);
        %stage = floor(%timeDiff/2);
        if(%player.chargeStage != %stage)
        {
            %player.chargeStage = %stage;
            if(%stage == 0)
                Player::mountItem(%player,FireChage1Item,7);
            else if(%stage == 1)
            {
                Player::unmountItem(%player,7);
                Player::mountItem(%player,FireChage2Item,7);
            }
            
        }
        %msg = ChargeMagic::CreateBottomPrintMsg(%clientId,%timeDiff);
    }
    else
    {
        if(%player.chargeStage < 2)
        {
            %player.chargeStage = 2;
            Player::unmountItem(%player,7);
            Player::mountItem(%player,FireChage3Item,7);
        }
        %msg = "<jc>Charge:\n<f1>[====================]\n<f0>You are ready to cast!";
    }
    
    bottomprint(%clientId,%msg,1);
    
    //%player.chargeLastUpdate = getSimTime();
}

function ChargeMagic::CreateBottomPrintMsg(%clientId,%timeDiff)
{
    %mm = floor(%timeDiff* $Charge::spacerLen/$ChargeTime);
    %bmsg = "<jc>Charge: "@ floor(100*(%timeDiff/$ChargeTime)) @"%\n[<f1>";
    %msg = String::rpad(%bmsg,String::len(%bmsg) +%mm,"=");
    %bb = ceil($Charge::spacerLen - %mm);
    %msg = String::rpad(%msg,String::len(%msg)+%bb," ");
   // echo(%mm @" "@ %bb);
    %msg = %msg @ "<f0>]";
    return %msg;
}

//****************************************************************************************************
//   GLADIUS
//****************************************************************************************************

ItemImageData GladiusImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Gladius);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData Gladius
{
	heading = "bWeapons";
	description = "Gladius";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = GladiusImage;
	price = 0;
	showWeaponBar = true;
};
function GladiusImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Gladius), Gladius);
}

//****************************************************************************************************
//   SHORT SWORD
//****************************************************************************************************

ItemImageData ShortswordImage
{
	shapeFile  = "short_sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Shortsword);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing2;
	sfxActivate = AxeSlash2;
};
ItemData Shortsword
{
	heading = "bWeapons";
	description = "Short Sword";
	className = "Weapon";
	shapeFile  = "short_sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = ShortswordImage;
	price = 0;
	showWeaponBar = true;
};
function ShortswordImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Shortsword), Shortsword);
}

//****************************************************************************************************
//   BROAD SWORD
//****************************************************************************************************

ItemImageData BroadswordImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Broadsword);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData Broadsword
{
	heading = "bWeapons";
	description = "Broad Sword";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BroadswordImage;
	price = 0;
	showWeaponBar = true;
};
function BroadswordImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Broadsword), Broadsword);
}

//****************************************************************************************************
//   LONG SWORD
//****************************************************************************************************

ItemImageData LongswordImage
{
	shapeFile  = "long_sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Longsword);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData Longsword
{
	heading = "bWeapons";
	description = "Long Sword";
	className = "Weapon";
	shapeFile  = "long_sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = LongswordImage;
	price = 0;
	showWeaponBar = true;
};
function LongswordImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Longsword), Longsword);
}

//****************************************************************************************************
//   KELDRINITE LONG SWORD
//****************************************************************************************************

ItemImageData KeldriniteLSImage
{
	shapeFile  = "elfinblade";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(KeldriniteLS);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing2;
	sfxActivate = ActivateAS;
};
ItemData KeldriniteLS
{
	heading = "bWeapons";
	description = "Keldrinite Long Sword";
	className = "Weapon";
	shapeFile  = "elfinblade";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = KeldriniteLSImage;
	price = 0;
	showWeaponBar = true;
};
function KeldriniteLSImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(KeldriniteLS), KeldriniteLS);
}

//****************************************************************************************************
//   BASTARD SWORD
//****************************************************************************************************

ItemImageData BastardswordImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Bastardsword);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData Bastardsword
{
	heading = "bWeapons";
	description = "Bastard Sword";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BastardswordImage;
	price = 0;
	showWeaponBar = true;
};
function BastardswordImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Bastardsword), Bastardsword);
}

//****************************************************************************************************
//   RAPIER
//****************************************************************************************************

ItemImageData RapierImage
{
	shapeFile  = "katana";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Rapier);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData Rapier
{
	heading = "bWeapons";
	description = "Rapier";
	className = "Weapon";
	shapeFile  = "katana";
	hudIcon = "katana";
	shadowDetailMask = 4;
	imageType = RapierImage;
	price = 0;
	showWeaponBar = true;
};
function RapierImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Rapier), Rapier);
}

//****************************************************************************************************
//   CLAYMORE
//****************************************************************************************************

ItemImageData ClaymoreImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Claymore);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData Claymore
{
	heading = "bWeapons";
	description = "Claymore";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "katana";
	shadowDetailMask = 4;
	imageType = ClaymoreImage;
	price = 0;
	showWeaponBar = true;
};
function ClaymoreImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Claymore), Claymore);
}

//****************************************************************************************************
//   HATCHET
//****************************************************************************************************

ItemImageData HatchetImage
{
	shapeFile  = "hatchet";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Hatchet);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData Hatchet
{
	heading = "bWeapons";
	description = "Hatchet";
	className = "Weapon";
	shapeFile  = "hatchet";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = HatchetImage;
	price = 0;
	showWeaponBar = true;
};
function HatchetImage::onFire(%player, %slot)
{
	WoodAxeSwing(%player, GetRange(Hatchet), Hatchet);
}

//****************************************************************************************************
//   WAR AXE
//****************************************************************************************************

ItemImageData WarAxeImage
{
	shapeFile  = "axe";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(WarAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData WarAxe
{
	heading = "bWeapons";
	description = "War Axe";
	className = "Weapon";
	shapeFile  = "axe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = WarAxeImage;
	price = 0;
	showWeaponBar = true;
};
function WarAxeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(WarAxe), WarAxe);
}

ItemImageData MeteorAxeImage
{
	shapeFile  = "axe";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(MeteorAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData MeteorAxe
{
	heading = "bWeapons";
	description = "Meteor Axe";
	className = "Weapon";
	shapeFile  = "axe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = MeteorAxeImage;
	price = 0;
	showWeaponBar = true;
};
function MeteorAxeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(MeteorAxe), MeteorAxe);
}

//****************************************************************************************************
//   PICK AXE
//****************************************************************************************************

ItemImageData PickAxeImage
{
	shapeFile = "Pick";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(PickAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = CrossbowSwitch1;
};
ItemData PickAxe
{
	heading = "bWeapons";
	description = "Pick Axe";
	className = "Weapon";
	shapeFile = "Pick";
	hudIcon = "pick";
	shadowDetailMask = 4;
	imageType = PickAxeImage;
	price = 0;
	showWeaponBar = true;
};
function PickAxeImage::onFire(%player, %slot)
{
	PickAxeSwing(%player, GetRange(PickAxe), PickAxe);
}

//****************************************************************************************************
//   HAMMER PICK
//****************************************************************************************************

ItemImageData HammerPickImage
{
	shapeFile = "Pick";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(HammerPick);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = CrossbowSwitch1;
};
ItemData HammerPick
{
	heading = "bWeapons";
	description = "Hammer Pick";
	className = "Weapon";
	shapeFile = "Pick";
	hudIcon = "pick";
	shadowDetailMask = 4;
	imageType = HammerPickImage;
	price = 0;
	showWeaponBar = true;
};
function HammerPickImage::onFire(%player, %slot)
{
	PickAxeSwing(%player, GetRange(HammerPick), HammerPick);
}

//****************************************************************************************************
//   BATTLE AXE
//****************************************************************************************************

ItemImageData BattleAxeImage
{
	shapeFile  = "BattleAxe";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(BattleAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing7;
	sfxActivate = AxeSlash2;
};
ItemData BattleAxe
{
	heading = "bWeapons";
	description = "Battle Axe";
	className = "Weapon";
	shapeFile  = "BattleAxe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = BattleAxeImage;
	price = 0;
	showWeaponBar = true;
};
function BattleAxeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(BattleAxe), BattleAxe);
}

//****************************************************************************************************
//   HALBERD
//****************************************************************************************************

ItemImageData HalberdImage
{
	shapeFile  = "sword";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Halberd);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing7;
	sfxActivate = AxeSlash2;
};
ItemData Halberd
{
	heading = "bWeapons";
	description = "Halberd";
	className = "Weapon";
	shapeFile  = "sword";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = HalberdImage;
	price = 0;
	showWeaponBar = true;
};
function HalberdImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Halberd), Halberd);
}

//****************************************************************************************************
//   SPEAR
//****************************************************************************************************

ItemImageData SpearImage
{
	shapeFile  = "spear";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Spear);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData Spear
{
	heading = "bWeapons";
	description = "Spear";
	className = "Weapon";
	shapeFile  = "spear";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = SpearImage;
	price = 0;
	showWeaponBar = true;
};
function SpearImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Spear), Spear);
}

//****************************************************************************************************
//   AWL PIKE
//****************************************************************************************************

ItemImageData AwlPikeImage
{
	shapeFile  = "spear";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(AwlPike);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData AwlPike
{
	heading = "bWeapons";
	description = "Awl Pike";
	className = "Weapon";
	shapeFile  = "spear";
	hudIcon = "trident";
	shadowDetailMask = 4;
	imageType = AwlPikeImage;
	price = 0;
	showWeaponBar = true;
};
function AwlPikeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(AwlPike), AwlPike);
}

//****************************************************************************************************
//   TRIDENT
//****************************************************************************************************

ItemImageData TridentImage
{
	shapeFile  = "trident";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Trident);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData Trident
{
	heading = "bWeapons";
	description = "Trident";
	className = "Weapon";
	shapeFile  = "trident";
	hudIcon = "trident";
	shadowDetailMask = 4;
	imageType = TridentImage;
	price = 0;
	showWeaponBar = true;
};
function TridentImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Trident), Trident);
}

//****************************************************************************************************
//   CLUB
//****************************************************************************************************

ItemImageData ClubImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Club);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData Club
{
	heading = "bWeapons";
	description = "Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "club";
	shadowDetailMask = 4;
	imageType = ClubImage;
	price = 0;
	showWeaponBar = true;
};
function ClubImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Club), Club);
}

//****************************************************************************************************
//   SPIKED CLUB
//****************************************************************************************************

ItemImageData SpikedClubImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(SpikedClub);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData SpikedClub
{
	heading = "bWeapons";
	description = "Spiked Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "sclub";
	shadowDetailMask = 4;
	imageType = SpikedClubImage;
	price = 0;
	showWeaponBar = true;
};
function SpikedClubImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(SpikedClub), SpikedClub);
}

//****************************************************************************************************
//   MACE
//****************************************************************************************************

ItemImageData MaceImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(Mace);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData Mace
{
	heading = "bWeapons";
	description = "Mace";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "mace";
	shadowDetailMask = 4;
	imageType = MaceImage;
	price = 0;
	showWeaponBar = true;
};
function MaceImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(Mace), Mace);
}

//****************************************************************************************************
//   WAR HAMMER
//****************************************************************************************************

ItemImageData WarHammerImage
{
	shapeFile  = "hammer";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(WarHammer);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing6;
	sfxActivate = AxeSlash2;
};
ItemData WarHammer
{
	heading = "bWeapons";
	description = "War Hammer";
	className = "Weapon";
	shapeFile  = "hammer";
	hudIcon = "hammer";
	shadowDetailMask = 4;
	imageType = WarHammerImage;
	price = 0;
	showWeaponBar = true;
};
function WarHammerImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(WarHammer), WarHammer);
}

//****************************************************************************************************
//   WAR MAUL
//****************************************************************************************************

ItemImageData WarMaulImage
{
	shapeFile  = "hammer";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(WarMaul);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing7;
	sfxActivate = AxeSlash2;
};
ItemData WarMaul
{
	heading = "bWeapons";
	description = "War Maul";
	className = "Weapon";
	shapeFile  = "hammer";
	hudIcon = "hammer";
	shadowDetailMask = 4;
	imageType = WarMaulImage;
	price = 0;
	showWeaponBar = true;
};
function WarMaulImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(WarMaul), WarMaul);
}

//****************************************************************************************************
//   QUARTER STAFF
//****************************************************************************************************

ItemImageData QuarterStaffImage
{
	shapeFile  = "quarterstaff";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(QuarterStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData QuarterStaff
{
	heading = "bWeapons";
	description = "Quarter Staff";
	className = "Weapon";
	shapeFile  = "quarterstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = QuarterStaffImage;
	price = 0;
	showWeaponBar = true;
};
function QuarterStaffImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(QuarterStaff), QuarterStaff);
}

//****************************************************************************************************
//   LONG STAFF
//****************************************************************************************************

ItemImageData LongStaffImage
{
	shapeFile  = "longstaff";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(LongStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData LongStaff
{
	heading = "bWeapons";
	description = "Long Staff";
	className = "Weapon";
	shapeFile  = "longstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = LongStaffImage;
	price = 0;
	showWeaponBar = true;
};
function LongStaffImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(LongStaff), LongStaff);
}

//****************************************************************************************************
//   JUSTICE STAFF
//****************************************************************************************************

ItemImageData JusticeStaffImage
{
	shapeFile  = "longstaff";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(JusticeStaff);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing4;
	sfxActivate = AxeSlash2;
};
ItemData JusticeStaff
{
	heading = "bWeapons";
	description = "Justice Staff";
	className = "Weapon";
	shapeFile  = "quarterstaff";
	hudIcon = "spear";
	shadowDetailMask = 4;
	imageType = JusticeStaffImage;
	price = 0;
	showWeaponBar = true;
};
function JusticeStaffImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(JusticeStaff), JusticeStaff);
}

//****************************************************************************************************
//   BONE CLUB
//****************************************************************************************************

ItemImageData BoneClubImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(BoneClub);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData BoneClub
{
	heading = "bWeapons";
	description = "Bone Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "club";
	shadowDetailMask = 4;
	imageType = BoneClubImage;
	price = 0;
	showWeaponBar = true;
};
function BoneClubImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(BoneClub), BoneClub);
}

//****************************************************************************************************
//   SPIKED BONE CLUB
//****************************************************************************************************

ItemImageData SpikedBoneClubImage
{
	shapeFile  = "mace";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(SpikedBoneClub);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing5;
	sfxActivate = AxeSlash2;
};
ItemData SpikedBoneClub
{
	heading = "bWeapons";
	description = "Spiked Bone Club";
	className = "Weapon";
	shapeFile  = "mace";
	hudIcon = "sclub";
	shadowDetailMask = 4;
	imageType = SpikedBoneClubImage;
	price = 0;
	showWeaponBar = true;
};
function SpikedBoneClubImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(SpikedBoneClub), SpikedBoneClub);
}

//****************************************************************************************************
//   SLING
//****************************************************************************************************

ItemImageData SlingImage
{
	shapeFile = "crossbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(Sling);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData Sling
{
	description = "Sling";
	className = "Weapon";
	shapeFile = "crossbow";
	hudIcon = "grenade";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = SlingImage;
	price = 0;
	showWeaponBar = true;
};
function SlingImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 60;
	ProjectileAttack(%clientId, Sling, %vel);
}

//****************************************************************************************************
//   SHORT BOW
//****************************************************************************************************

ItemImageData ShortBowImage
{
	shapeFile = "longbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(ShortBow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData ShortBow
{
	description = "Short Bow";
	className = "Weapon";
	shapeFile = "longbow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = ShortBowImage;
	price = 0;
	showWeaponBar = true;
};
function ShortBowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, ShortBow, %vel);
}

//****************************************************************************************************
//   LONG BOW
//****************************************************************************************************

ItemImageData LongBowImage
{
	shapeFile = "longbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(LongBow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData LongBow
{
	description = "Long Bow";
	className = "Weapon";
	shapeFile = "longbow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = LongBowImage;
	price = 0;
	showWeaponBar = true;
};
function LongBowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, LongBow, %vel);
}

//****************************************************************************************************
//   ELVEN BOW
//****************************************************************************************************

ItemImageData ElvenBowImage
{
	shapeFile = "longbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(ElvenBow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData ElvenBow
{
	description = "Elven Bow";
	className = "Weapon";
	shapeFile = "longbow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = ElvenBowImage;
	price = 0;
	showWeaponBar = true;
};
function ElvenBowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, ElvenBow, %vel);
}

//****************************************************************************************************
//   COMPOSITE BOW
//****************************************************************************************************

ItemImageData CompositeBowImage
{
	shapeFile = "comp_bow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(CompositeBow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData CompositeBow
{
	description = "Composite Bow";
	className = "Weapon";
	shapeFile = "comp_bow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = CompositeBowImage;
	price = 0;
	showWeaponBar = true;
};
function CompositeBowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, CompositeBow, %vel);
}

//****************************************************************************************************
//   AEOLUS'S WING
//****************************************************************************************************

ItemImageData AeolusWingImage
{
	shapeFile = "comp_bow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(AeolusWing);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData AeolusWing
{
	description = "Aeolus's Wing";
	className = "Weapon";
	shapeFile = "comp_bow";
	hudIcon = "bow";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = AeolusWingImage;
	price = 0;
	showWeaponBar = true;
};
function AeolusWingImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, AeolusWing, %vel);
}

//****************************************************************************************************
//   LIGHT CROSSBOW
//****************************************************************************************************

ItemImageData LightCrossbowImage
{
	shapeFile = "crossbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(LightCrossbow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData LightCrossbow
{
	description = "Light Crossbow";
	className = "Weapon";
	shapeFile = "crossbow";
	hudIcon = "grenade";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = LightCrossbowImage;
	price = 0;
	showWeaponBar = true;
};
function LightCrossbowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, LightCrossbow, %vel);
}

//****************************************************************************************************
//   HEAVY CROSSBOW
//****************************************************************************************************

ItemImageData HeavyCrossbowImage
{
	shapeFile = "crossbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(HeavyCrossbow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData HeavyCrossbow
{
	description = "Heavy Crossbow";
	className = "Weapon";
	shapeFile = "crossbow";
	hudIcon = "grenade";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = HeavyCrossbowImage;
	price = 0;
	showWeaponBar = true;
};
function HeavyCrossbowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, HeavyCrossbow, %vel);
}

//****************************************************************************************************
//   REPEATING CROSSBOW
//****************************************************************************************************

ItemImageData RepeatingCrossbowImage
{
	shapeFile = "crossbow";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	ammoType = "";
	projectileType = NoProjectile;
	accuFire = false;
	reloadTime = 0;
	fireTime = GetDelay(RepeatingCrossbow);

	lightType = 3;  // Weapon Fire
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };

	sfxFire = CrossbowShoot1;
	sfxActivate = CrossbowSwitch1;
	sfxReload = NoSound;
};
ItemData RepeatingCrossbow
{
	description = "Repeating Crossbow";
	className = "Weapon";
	shapeFile = "crossbow";
	hudIcon = "grenade";
	heading = "bWeapons";
	shadowDetailMask = 4;
	imageType = RepeatingCrossbowImage;
	price = 0;
	showWeaponBar = true;
};
function RepeatingCrossbowImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);

	%vel = 100;
	ProjectileAttack(%clientId, RepeatingCrossbow, %vel);
}

//****************************************************************************************************
//   CASTING BLADE
//****************************************************************************************************

ItemImageData CastingBladeImage
{
	shapeFile  = "invisable"; //as opposed to "invisible"
	mountPoint = 0;

	weaponType = 0;
	reloadTime = 0;
	fireTime = GetDelay(CastingBlade);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = NoSound;
};
ItemData CastingBlade
{
	heading = "bWeapons";
	description = "Casting Blade";
	className = "Weapon";
	shapeFile  = "dagger";
	hudIcon = "dagger";
	shadowDetailMask = 4;
	imageType = CastingBladeImage;
	price = 0;
	showWeaponBar = true;
};
function CastingBladeImage::onFire(%player, %slot)
{
	%clientId = Player::getClient(%player);
	if(%clientId == "")
		%clientId = 0;

//	if(Player::isAIcontrolled(%clientId))
//	{
//		if(fetchData(%clientId, "HP") <= (fetchData(%clientId, "MaxHP")/3))
//		{
//			if( floor(getRandom() * 10) > 7 )
//				%doHealSpell = True;
//		}
//	}
//	if(%doHealSpell)
//		%index = GetBestSpell(%clientId, -1, True);
//	else

	%index = GetBestSpell(%clientId, 1, True);

	%length = $Spell::LOSrange[%index]-1;
		
	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length) && %index != -1)
	{
		%obj = getObjectType($los::object);
		if(%obj == "Player")
		{
			if(Player::isAiControlled(%clientId))
			{
				AI::newDirectiveRemove(fetchData(%clientId, "BotInfoAiName"), 99);
			}
			remoteSay(%clientId, 0, "#cast " @ $Spell::keyword[%index]);
			%hasCast = True;
		}
	}
	if(!%hasCast)
	{
		if(OddsAre(3))
			MeleeAttack(%player, GetRange(Hatchet), CastingBlade);	//mimic the hatchet range
	}
	%hasCast = "";
}

//====== "Projectiles" ======================================================

ItemData SmallRock
{
	description = "Small Rock";
	className = "Projectile";
	shapeFile = "little_rock";
	heading = "xAmmunition";
	shadowDetailMask = 4;
	price = 0;
};

ItemData BowArrow
{
    description = "Arrow";
	className = "Projectile";
	shapeFile = "tracer";
	heading = "xAmmunition";
	shadowDetailMask = 4;
	price = 0;
};

ItemData CrossbowBolt
{
    description = "Light Quarrel";
	className = "Projectile";
	shapeFile = "bullet";
	heading = "xAmmunition";
	shadowDetailMask = 4;
	price = 0;
};

//ItemData BasicArrow
//{
//	description = "Basic Arrow";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData SheafArrow
//{
//	description = "Sheaf Arrow";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData BladedArrow
//{
//	description = "Bladed Arrow";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData LightQuarrel
//{
//	description = "Light Quarrel";
//	className = "Projectile";
//	shapeFile = "bullet";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData HeavyQuarrel
//{
//	description = "Heavy Quarrel";
//	className = "Projectile";
//	shapeFile = "bullet";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData ShortQuarrel
//{
//	description = "Short Quarrel";
//	className = "Projectile";
//	shapeFile = "bullet";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData StoneFeather
//{
//	description = "Stone Feather";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData MetalFeather
//{
//	description = "Metal Feather";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData Talon
//{
//	description = "Talon";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};
//ItemData CeraphumsFeather
//{
//	description = "Ceraphum's Feather";
//	className = "Projectile";
//	shapeFile = "tracer";
//	heading = "xAmmunition";
//	shadowDetailMask = 4;
//	price = 0;
//};

//===========================================================================================
//===========================================================================================
//===========================================================================================
//====================================             ==========================================
//====================================   RUSTIES   ==========================================
//====================================             ==========================================
//===========================================================================================
//===========================================================================================
//===========================================================================================

//Notes on smithed items and rusties:
//-To determine the cost of a final combined item, add up all the costs of the materials
// involved and divide by $RustyCostAmp.

//---------------------------------
$SmithCombo[1] = "RHatchet 1";
$SmithCombo[2] = "RBroadSword 1";
$SmithCombo[3] = "RLongSword 1";
$SmithCombo[4] = "RClub 1";
$SmithCombo[5] = "RSpikedClub 1";
$SmithCombo[6] = "RKnife 1";
$SmithCombo[7] = "RDagger 1";
$SmithCombo[8] = "RShortSword 1";
$SmithCombo[9] = "RPickAxe 1";
$SmithCombo[10] = "RShortBow 1";
$SmithCombo[11] = "RLightCrossbow 1";
$SmithCombo[12] = "RWarAxe 1";
$SmithCombo[13] = "Keldrinite 1 LongSword 1";
$SmithCombo[14] = "ElvenBow 1 CompositeBow 1 Quartz 3";
$SmithCombo[15] = "SmallRock 1 Quartz 1";
$SmithCombo[16] = "Knife 1 Quartz 1";
$SmithCombo[17] = "Dagger 1 Quartz 1 Granite 2";
$SmithCombo[18] = "Dagger 2 Jade 2 Quartz 4";
$SmithCombo[19] = "Club 1 SkeletonBone 1 Granite 3";
$SmithCombo[20] = "SpikedClub 1 SkeletonBone 2 Granite 5";
$SmithCombo[21] = "LightRobe 1 ApprenticeRobe 1 EnchantedStone 5";
$SmithCombo[22] = "Keldrinite 2 FullPlateArmor 1 Gold 5 Emerald 5 Diamond 5 EnchantedStone 5";
$SmithCombo[23] = "DragonScale 5 Diamond 5 Ruby 3";
$SmithCombo[24] = "DragonScale 3 Ruby 2";
$SmithCombo[25] = "AdvisorRobe 1 Topaz 2 EnchantedStone 4";
$SmithCombo[26] = "LongStaff 1 Granite 4 Turquoise 2";

$SmithComboResult[1] = "Hatchet 1";
$SmithComboResult[2] = "BroadSword 1";
$SmithComboResult[3] = "LongSword 1";
$SmithComboResult[4] = "Club 1";
$SmithComboResult[5] = "SpikedClub 1";
$SmithComboResult[6] = "Knife 1";
$SmithComboResult[7] = "Dagger 1";
$SmithComboResult[8] = "ShortSword 1";
$SmithComboResult[9] = "PickAxe 1";
$SmithComboResult[10] = "ShortBow 1";
$SmithComboResult[11] = "LightCrossbow 1";
$SmithComboResult[12] = "WarAxe 1";
$SmithComboResult[13] = "KeldriniteLS 1";
$SmithComboResult[14] = "AeolusWing 1";
$SmithComboResult[15] = "StoneFeather 1";
$SmithComboResult[16] = "MetalFeather 1";
$SmithComboResult[17] = "Talon 1";
$SmithComboResult[18] = "CeraphumsFeather 1";
$SmithComboResult[19] = "BoneClub 1";
$SmithComboResult[20] = "SpikedBoneClub 1";
$SmithComboResult[21] = "FineRobe 1";
$SmithComboResult[22] = "KeldrinArmor 1";
$SmithComboResult[23] = "DragonMail 1";
$SmithComboResult[24] = "DragonShield 1";
$SmithComboResult[25] = "ElvenRobe 1";
$SmithComboResult[26] = "JusticeStaff 1";

//---------------------------------

$AccessoryVar[RHatchet, $AccessoryType] = $AccessoryVar[Hatchet, $AccessoryType];
$AccessoryVar[RBroadSword, $AccessoryType] = $AccessoryVar[BroadSword, $AccessoryType];
$AccessoryVar[RLongSword, $AccessoryType] = $AccessoryVar[LongSword, $AccessoryType];
$AccessoryVar[RClub, $AccessoryType] = $AccessoryVar[Club, $AccessoryType];
$AccessoryVar[RSpikedClub, $AccessoryType] = $AccessoryVar[SpikedClub, $AccessoryType];
$AccessoryVar[RKnife, $AccessoryType] = $AccessoryVar[Knife, $AccessoryType];
$AccessoryVar[RDagger, $AccessoryType] = $AccessoryVar[Dagger, $AccessoryType];
$AccessoryVar[RShortSword, $AccessoryType] = $AccessoryVar[ShortSword, $AccessoryType];
$AccessoryVar[RPickAxe, $AccessoryType] = $AccessoryVar[PickAxe, $AccessoryType];
$AccessoryVar[RShortBow, $AccessoryType] = $AccessoryVar[ShortBow, $AccessoryType];
$AccessoryVar[RLightCrossbow, $AccessoryType] = $AccessoryVar[LightCrossbow, $AccessoryType];
$AccessoryVar[RWarAxe, $AccessoryType] = $AccessoryVar[WarAxe, $AccessoryType];

$AccessoryVar[RHatchet, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[Hatchet, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RBroadSword, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[BroadSword, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RLongSword, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[LongSword, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RClub, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[Club, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RSpikedClub, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[SpikedClub, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RKnife, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[Knife, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RDagger, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[Dagger, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RShortSword, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[ShortSword, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RPickAxe, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[PickAxe, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RShortBow, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[ShortBow, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RLightCrossbow, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[LightCrossbow, $SpecialVar], 1) * $RustyDamageAmp);
$AccessoryVar[RWarAxe, $SpecialVar] = "6 " @ round(GetWord($AccessoryVar[WarAxe, $SpecialVar], 1) * $RustyDamageAmp);

$AccessoryVar[RHatchet, $Weight] = $AccessoryVar[Hatchet, $Weight] * $RustyWeightAmp;
$AccessoryVar[RBroadSword, $Weight] = $AccessoryVar[BroadSword, $Weight] * $RustyWeightAmp;
$AccessoryVar[RLongSword, $Weight] = $AccessoryVar[LongSword, $Weight] * $RustyWeightAmp;
$AccessoryVar[RClub, $Weight] = $AccessoryVar[Club, $Weight] * $RustyWeightAmp;
$AccessoryVar[RSpikedClub, $Weight] = $AccessoryVar[SpikedClub, $Weight] * $RustyWeightAmp;
$AccessoryVar[RKnife, $Weight] = $AccessoryVar[Knife, $Weight] * $RustyWeightAmp;
$AccessoryVar[RDagger, $Weight] = $AccessoryVar[Dagger, $Weight] * $RustyWeightAmp;
$AccessoryVar[RShortSword, $Weight] = $AccessoryVar[ShortSword, $Weight] * $RustyWeightAmp;
$AccessoryVar[RPickAxe, $Weight] = $AccessoryVar[PickAxe, $Weight] * $RustyWeightAmp;
$AccessoryVar[RShortBow, $Weight] = $AccessoryVar[ShortBow, $Weight] * $RustyWeightAmp;
$AccessoryVar[RLightCrossbow, $Weight] = $AccessoryVar[LightCrossbow, $Weight] * $RustyWeightAmp;
$AccessoryVar[RWarAxe, $Weight] = $AccessoryVar[WarAxe, $Weight] * $RustyWeightAmp;

$AccessoryVar[RHatchet, $MiscInfo] = "A rusty hatchet";
$AccessoryVar[RBroadSword, $MiscInfo] = "A rusty broad sword";
$AccessoryVar[RLongSword, $MiscInfo] = "A rusty long sword";
$AccessoryVar[RClub, $MiscInfo] = "A cracked club";
$AccessoryVar[RSpikedClub, $MiscInfo] = "A cracked spiked club";
$AccessoryVar[RKnife, $MiscInfo] = "A rusty knife";
$AccessoryVar[RDagger, $MiscInfo] = "A rusty dagger";
$AccessoryVar[RShortSword, $MiscInfo] = "A rusty short sword";
$AccessoryVar[RPickAxe, $MiscInfo] = "A rusty pick axe";
$AccessoryVar[RShortBow, $MiscInfo] = "A cracked short bow";
$AccessoryVar[RLightCrossbow, $MiscInfo] = "A cracked light crossbow";
$AccessoryVar[RWarAxe, $MiscInfo] = "A rusty war axe";

$SkillType[RHatchet] = $SkillSlashing;
$SkillType[RBroadSword] = $SkillSlashing;
$SkillType[RLongSword] = $SkillSlashing;
$SkillType[RClub] = $SkillBludgeoning;
$SkillType[RSpikedClub] = $SkillBludgeoning;
$SkillType[RKnife] = $SkillPiercing;
$SkillType[RDagger] = $SkillPiercing;
$SkillType[RShortSword] = $SkillPiercing;
$SkillType[RPickAxe] = $SkillPiercing;
$SkillType[RShortBow] = $SkillArchery;
$SkillType[RLightCrossbow] = $SkillArchery;
$SkillType[RWarAxe] = $SkillSlashing;

//****************************************************************************************************
// RUSTY KNIFE
//****************************************************************************************************

ItemImageData RKnifeImage
{
	shapeFile = "dagger";
	mountPoint = 0;

	weaponType = 0;
	reloadTime = 0;
	fireTime = GetDelay(RKnife);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing1;
	sfxActivate = AxeSlash2;
};
ItemData RKnife
{ 
	heading = "bWeapons"; 
	description = "Rusty Knife"; 
	className = "Weapon"; 
	shapeFile = "dagger"; 
	hudIcon = "dagger";
	shadowDetailMask = 4; 
	imageType = RKnifeImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RKnifeImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RKnife), RKnife); 
} 

//**************************************************************************************************** 
// RUSTY DAGGER 
//**************************************************************************************************** 

ItemImageData RDaggerImage 
{ 
	shapeFile = "dagger"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RDagger); 
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing1; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RDagger 
{ 
	heading = "bWeapons"; 
	description = "Rusty Dagger"; 
	className = "Weapon"; 
	shapeFile = "dagger"; 
	hudIcon = "dagger";
	shadowDetailMask = 4; 
	imageType = RDaggerImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RDaggerImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RDagger), RDagger); 
} 

//**************************************************************************************************** 
// RUSTY SHORTSWORD 
//**************************************************************************************************** 

ItemImageData RShortswordImage 
{ 
	shapeFile = "short_sword"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RShortsword);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing2; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RShortsword 
{ 
	heading = "bWeapons"; 
	description = "Rusty Short Sword"; 
	className = "Weapon"; 
	shapeFile = "short_sword"; 
	hudIcon = "blaster";
	shadowDetailMask = 4; 
	imageType = RShortswordImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RShortswordImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RShortsword), RShortsword); 
} 

//**************************************************************************************************** 
// RUSTY BROADSWORD 
//**************************************************************************************************** 

ItemImageData RBroadswordImage 
{ 
	shapeFile = "sword"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RBroadsword);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing5; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RBroadsword 
{ 
	heading = "bWeapons"; 
	description = "Rusty Broad Sword"; 
	className = "Weapon"; 
	shapeFile = "sword"; 
	hudIcon = "blaster";
	shadowDetailMask = 4; 
	imageType = RBroadswordImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RBroadswordImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RBroadsword), RBroadsword); 
} 

//**************************************************************************************************** 
// RUSTY LONGSWORD 
//**************************************************************************************************** 

ItemImageData RLongswordImage 
{ 
	shapeFile = "long_sword"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RLongsword);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing6; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RLongsword 
{ 
	heading = "bWeapons"; 
	description = "Rusty Long Sword"; 
	className = "Weapon"; 
	shapeFile = "long_sword"; 
	hudIcon = "blaster";
	shadowDetailMask = 4; 
	imageType = RLongswordImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RLongswordImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RLongsword), RLongsword); 
} 

//**************************************************************************************************** 
// RUSTY HATCHET 
//**************************************************************************************************** 

ItemImageData RHatchetImage 
{ 
	shapeFile = "axe"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RHatchet);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing1; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RHatchet 
{ 
	heading = "bWeapons"; 
	description = "Rusty Hatchet"; 
	className = "Weapon"; 
	shapeFile = "axe"; 
	hudIcon = "axe";
	shadowDetailMask = 4; 
	imageType = RHatchetImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RHatchetImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RHatchet), RHatchet); 
} 

//**************************************************************************************************** 
// RUSTY PICKAXE 
//**************************************************************************************************** 

ItemImageData RPickAxeImage 
{ 
	shapeFile = "Pick"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RPickAxe);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing1; 
	sfxActivate = CrossbowSwitch1; 
}; 
ItemData RPickAxe 
{ 
	heading = "bWeapons"; 
	description = "Rusty Pick Axe"; 
	className = "Weapon"; 
	shapeFile = "Pick"; 
	hudIcon = "pick";
	shadowDetailMask = 4; 
	imageType = RPickAxeImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RPickAxeImage::onfire(%player, %slot) 
{ 
	PickAxeSwing(%player, GetRange(RPickAxe), RPickAxe); 
} 

//**************************************************************************************************** 
// CRACKED CLUB 
//**************************************************************************************************** 

ItemImageData RClubImage 
{ 
	shapeFile = "mace"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RClub);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing5; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RClub 
{ 
	heading = "bWeapons"; 
	description = "Cracked Club"; 
	className = "Weapon"; 
	shapeFile = "mace"; 
	hudIcon = "club";
	shadowDetailMask = 4; 
	imageType = RClubImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RClubImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RClub), RClub); 
} 

//**************************************************************************************************** 
// CRACKED SPIKED CLUB 
//**************************************************************************************************** 

ItemImageData RSpikedClubImage 
{ 
	shapeFile = "mace"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	reloadTime = 0; 
	fireTime = GetDelay(RSpikedClub);
	minEnergy = 0; 
	maxEnergy = 0; 

	accuFire = true; 

	sfxFire = SoundSwing5; 
	sfxActivate = AxeSlash2; 
}; 
ItemData RSpikedClub 
{ 
	heading = "bWeapons"; 
	description = "Cracked Spiked Club"; 
	className = "Weapon"; 
	shapeFile = "mace"; 
	hudIcon = "sclub";
	shadowDetailMask = 4; 
	imageType = RSpikedClubImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RSpikedClubImage::onfire(%player, %slot) 
{ 
	MeleeAttack(%player, GetRange(RSpikedClub), RSpikedClub); 
} 

//**************************************************************************************************** 
// CRACKED SHORT BOW 
//**************************************************************************************************** 

ItemImageData RShortBowImage 
{ 
	shapeFile = "longbow"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	ammoType = ""; 
	projectileType = NoProjectile; 
	accuFire = false; 
	reloadTime = 0; 
	fireTime = GetDelay(RShortBow);

	lightType = 3; // Weapon Fire 
	lightRadius = 3; 
	lightTime = 1; 
	lightColor = { 0.6, 1, 1.0 }; 

	sfxFire = CrossbowShoot1; 
	sfxActivate = CrossbowSwitch1; 
	sfxReload = NoSound; 
}; 
ItemData RShortBow 
{ 
	description = "Cracked Short Bow"; 
	className = "Weapon"; 
	shapeFile = "longbow"; 
	hudIcon = "bow";
	heading = "bWeapons"; 
	shadowDetailMask = 4; 
	imageType = RShortBowImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RShortBowImage::onfire(%player, %slot) 
{ 
	%clientId = Player::getClient(%player); 

	%vel = 100; 
	ProjectileAttack(%clientId, RShortBow, %vel); 
} 

//**************************************************************************************************** 
// CRACKED LIGHT CROSSBOW 
//**************************************************************************************************** 

ItemImageData RLightCrossbowImage 
{ 
	shapeFile = "crossbow"; 
	mountPoint = 0; 

	weaponType = 0; // Single Shot 
	ammoType = ""; 
	projectileType = NoProjectile; 
	accuFire = false; 
	reloadTime = 0; 
	fireTime = GetDelay(RLightCrossbow);

	lightType = 3; // Weapon Fire 
	lightRadius = 3; 
	lightTime = 1; 
	lightColor = { 0.6, 1, 1.0 }; 

	sfxFire = CrossbowShoot1; 
	sfxActivate = CrossbowSwitch1; 
	sfxReload = NoSound; 
}; 
ItemData RLightCrossbow 
{ 
	description = "Cracked Light Crossbow"; 
	className = "Weapon"; 
	shapeFile = "crossbow"; 
	hudIcon = "grenade";
	heading = "bWeapons"; 
	shadowDetailMask = 4; 
	imageType = RLightCrossbowImage; 
	price = 0; 
	showWeaponBar = true; 
}; 
function RLightCrossbowImage::onfire(%player, %slot) 
{ 
	%clientId = Player::getClient(%player); 

	%vel = 100; 
	ProjectileAttack(%clientId, RLightCrossbow, %vel); 
}

//****************************************************************************************************
// RUSTY WARAXE
//****************************************************************************************************

ItemImageData RWarAxeImage
{
	shapeFile = "axe";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(RWarAxe);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = CrossbowSwitch1;
};
ItemData RWarAxe
{
	heading = "bWeapons";
	description = "Rusty War Axe";
	className = "Weapon";
	shapeFile = "axe";
	hudIcon = "axe";
	shadowDetailMask = 4;
	imageType = RWarAxeImage;
	price = 0;
	showWeaponBar = true;
};
function RWarAxeImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(RWarAxe), RWarAxe);
}

ItemImageData DragonFireCharge1Image
{
    shapeFile = "plasmaex";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = 2;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = NoSound;
};

ItemData DragonFireCharge1
{	
	description = "dd";
	className = "Weapon";
	shapeFile = "grenadeL";
	hudIcon = "grenade";
	heading = "zomg";
	shadowDetailMask = 4;
	imageType = DragonFireCharge1Image;
	price = 150;
	showWeaponBar = true;
};

ItemImageData DragonFireCharge2Image
{
    shapeFile = "fiery";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = 2;
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = NoSound;
	sfxActivate = NoSound;
};

ItemData DragonFireCharge2
{	
	description = "dd2";
	className = "Weapon";
	shapeFile = "grenadeL";
	hudIcon = "grenade";
	heading = "zomg";
	shadowDetailMask = 4;
	imageType = DragonFireCharge2Image;
	price = 150;
	showWeaponBar = true;
};

GrenadeData Chkn
{
   bulletShapeName    = "chickenarmor1.dts";
   explosionTag       = rocketExpBoom;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.3;
   mass               = 8.0;
   elasticity         = 0.1;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 85;
   damageType         = $NullDamageType;
   explosionRadius    = 10.0;
   kickBackStrength   = 5.0;
   maxLevelFlightDist = 75;
   totalTime          = 30.0;
   liveTime           = 2.0;
   projSpecialTime    = 0.01;

   inheritedVelocityScale = 0.5;
   smokeName              = "invisable.dts";
};

GrenadeData Eggie
{
   bulletShapeName    = "egg.dts";
   explosionTag       = debrisExpSmall;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.3;
   mass               = 8.0;
   elasticity         = 0.2;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 85;
   damageType         = $NullDamageType;
   explosionRadius    = 10.0;
   kickBackStrength   = 5.0;
   maxLevelFlightDist = 15;
   totalTime          = 30.0;
   liveTime           = 0.5;
   projSpecialTime    = 0.01;

   inheritedVelocityScale = 0.5;
   smokeName              = "invisable.dts";
};

ItemImageData ChickenLauncherImage 
{	
	shapeFile = "chickenarmor1";
	mountPoint = 0;
	weaponType = 0;
	//ammoType = GrenadeAmmo;
	//projectileType = BomberWarhead;
    mountOffset = { 0, 0.351, -0.1};
	accuFire = false;
	reloadTime = 0.8;
	fireTime = 0.5;
	lightType = 3;
	lightRadius = 3;
	lightTime = 1;
	lightColor = { 0.6, 1, 1.0 };
	sfxFire = SoundPickUpWeapon;	//SoundTurretDeploy;	//SoundFireGrenade;
	sfxActivate = SoundPickUpWeapon;
	sfxReload = SoundDryFire;
};

ItemData ChickenLauncher 
{	
	description = "ChickenLauncher";
	className = "Weapon";
	shapeFile = "grenadeL";
	hudIcon = "grenade";
	heading = "zomg";
	shadowDetailMask = 4;
	imageType = ChickenLauncherImage;
	price = 150;
	showWeaponBar = true;
};

$AccessoryVar[ChickenWeapon, $Weight] = 0.2;
$SkillRestriction[ChickenWeapon] = $SkillPiercing @ " 1";
$AccessoryVar[ChickenWeapon, $MiscInfo] = "A Chicken Beak";
$AccessoryVar[ChickenWeapon, $AccessoryType] = $ShortBladeAccessoryType;
$AccessoryVar[ChickenWeapon, $SpecialVar] = "6 114";
$SkillType[ChickenWeapon] = $SkillPiercing;

ItemImageData ChickenWeaponImage
{
	shapeFile  = "invisable";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = GetDelay(ChickenWeapon);
	minEnergy = 0;
	maxEnergy = 0;

	accuFire = true;

	sfxFire = SoundSwing3;
	sfxActivate = AxeSlash2;
};
ItemData ChickenWeapon
{
	heading = "bWeapons";
	description = "Chicken Peck";
	className = "Weapon";
	shapeFile  = "hammer";
	hudIcon = "hammer";
	shadowDetailMask = 4;
	imageType = ChickenWeaponImage;
	price = 0;
	showWeaponBar = true;
};
function ChickenWeaponImage::onFire(%player, %slot)
{
	MeleeAttack(%player, GetRange(ChickenWeapon), ChickenWeapon);
}
$ChickenSpawn = 15;
function Chkn::onRemove(%this)
{
    echo("Removing Chkn");
    // Projectile was already cleaned up, so we have to estimate its position
    %trkId = Projectile::getTrackId(%this);
    %pos = Projectile::PropagateTrack(%this,%trkId,0.5);
    %client = $Projectile::tracking[%this,%trkId,Owner];
    %player = Client::getOwnedObject(%client);
    %trans = "0 0 0 0 0 1 0 0 0 "@ %pos;
    for(%i = 0; %i < $ChickenSpawn; %i++)
    {
        %x = 15*getRandom();
        %y = 15*getRandom();
        %z = 10*getRandom();
        %vel = %x@" "@%y@" "@%z;
        %proj = Projectile::spawnProjectile("Eggie",%trans,%player,%vel);
        //$EggieNum[%proj] = %i;
        Projectile::startTracking(%client,%proj,0.2,3);
        //Projectile::TrackProjectile(%proj,0.2,%player);
    }
    
    Projectile::TrackCleanup(%this,%trkId);
}

function Eggie::onRemove(%this)
{
    %trkId = Projectile::getTrackId(%this);
    %pos = Projectile::PropagateTrack(%this,%trkId,0.5);
    %client = $Projectile::tracking[%this,%trkId,Owner];
    
    %rot = "0 0 "@ 2*$pi*getRandom();
    
    setAInumber(%newName, %n);
    %n = getAInumber();
    %aiName = "Chicken"@%n;
    echo("Create AI: "@ AI::otherCreate(%aiName,"Chicken",chickenarmor1,%pos,%rot));
    setAINumber(%aiName,%n);
    

    AI::setVar( %aiName,  iq,  100 );
    AI::setVar( %aiName,  attackMode, $AIattackMode);
    AI::setVar( %aiName,  pathType, $AI::defaultPathType);
    %aiCl = AI::getId(%aiName);
    
    echo("AI Client:" @%aiCL);
    storeData(%aiCl,"SpawnBotInfo","NotBlank");
    Gamebase::setTeam(%aiCl,5);
    AI::SetVar(%aiName, spotDist, 40);
    
    GiveThisStuff(%aiCl,"Dagger 1 EXP 20000");
    HardcodeAIskills(%aiCl);
    AI::SelectBestWeapon(%aiCl);
    AI::newDirectiveFollow(%aiName, %client, 0, 99);
    
    $AICount++;
    
    Projectile::TrackCleanup(%this,%trkId);
    //$EggieNum[%this] = "";
}

$AccessoryVar[ChickenLauncher, $MiscInfo] = "What it says on the tin";

function ChickenLauncherImage::onFire(%player, %slot)
{
    %clientId = Player::getClient(%player);
	%trans = GameBase::getMuzzleTransform(%player);
    
    %proj = Projectile::spawnProjectile("Chkn",%trans,%player,"0 0 0");
    Projectile::startTracking(%clientId,%proj,0.2,3);
    //Projectile::TrackProjectile(%proj,0.2,%clientId);
    Player::unmountItem(%player,%slot);
}

// For hotkey stuff

ItemData Blaster
{
   heading = "zOmg";
	description = "Blaster";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};

ItemData PlasmaGun
{
   heading = "zOmg";
	description = "Plasma Gun";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData Chaingun
{
   heading = "zOmg";
	description = "Chaingun";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData DiscLauncher
{
   heading = "zOmg";
	description = "Disc Launcher";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData GrenadeLauncher
{
   heading = "zOmg";
	description = "Grenade Launcher";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData LaserRifle
{
   heading = "zOmg";
	description = "Laser Rifle";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData ElfGun
{
   heading = "zOmg";
	description = "Elf Gun";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData Mortar
{
   heading = "zOmg";
	description = "Mortar";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};


ItemData TargetingLaser
{
   heading = "zOmg";
	description = "Targeting Laser";
	className = "Weapon";
   shapeFile  = "energygun";
	hudIcon = "blaster";
	shadowDetailMask = 4;
	imageType = BlasterImage;
	price = 0;
	showWeaponBar = true;
};