function GenerateAllArmorCosts()
{
	$ItemCost[PaddedArmor] = GenerateItemCost(PaddedArmor);
	$ItemCost[LeatherArmor] = GenerateItemCost(LeatherArmor);
	$ItemCost[StuddedLeather] = GenerateItemCost(StuddedLeather);
	$ItemCost[SpikedLeather] = GenerateItemCost(SpikedLeather);
	$ItemCost[HideArmor] = GenerateItemCost(HideArmor);
	$ItemCost[ScaleMail] = GenerateItemCost(ScaleMail);
	$ItemCost[BrigandineArmor] = GenerateItemCost(BrigandineArmor);
	$ItemCost[ChainMail] = GenerateItemCost(ChainMail);
	$ItemCost[RingMail] = GenerateItemCost(RingMail);
	$ItemCost[BandedMail] = GenerateItemCost(BandedMail);
	$ItemCost[SplintMail] = GenerateItemCost(SplintMail);
	$ItemCost[BronzePlateMail] = GenerateItemCost(BronzePlateMail);
	$ItemCost[PlateMail] = GenerateItemCost(PlateMail);
	$ItemCost[FieldPlateArmor] = GenerateItemCost(FieldPlateArmor);
	$ItemCost[FullPlateArmor] = GenerateItemCost(FullPlateArmor);
	$ItemCost[ApprenticeRobe] = GenerateItemCost(ApprenticeRobe);
	$ItemCost[LightRobe] = GenerateItemCost(LightRobe);
	$ItemCost[BloodRobe] = GenerateItemCost(BloodRobe);
	$ItemCost[AdvisorRobe] = GenerateItemCost(AdvisorRobe);
	$ItemCost[RobeOfVenjance] = GenerateItemCost(RobeOfVenjance);
	$ItemCost[PhensRobe] = GenerateItemCost(PhensRobe);
	$ItemCost[QuestMasterRobe] = 0;
	$ItemCost[CheetaursPaws] = GenerateItemCost(CheetaursPaws);
	$ItemCost[BootsOfGliding] = GenerateItemCost(BootsOfGliding);
	$ItemCost[WindWalkers] = GenerateItemCost(WindWalkers);
	$ItemCost[FineRobe] = GenerateItemCost(FineRobe);
	$ItemCost[ElvenRobe] = GenerateItemCost(ElvenRobe);
	$ItemCost[DragonMail] = GenerateItemCost(DragonMail);
	$ItemCost[KeldrinArmor] = GenerateItemCost(KeldrinArmor);
}

$AccessoryVar[PaddedArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[LeatherArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[StuddedLeather, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[SpikedLeather, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[HideArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[ScaleMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[BrigandineArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[ChainMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[RingMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[BandedMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[SplintMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[BronzePlateMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[PlateMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[FieldPlateArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[FullPlateArmor, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[ApprenticeRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[LightRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[BloodRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[AdvisorRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[RobeOfVenjance, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[PhensRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[QuestMasterRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[FineRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[ElvenRobe, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[DragonMail, $AccessoryType] = $BodyAccessoryType;
$AccessoryVar[KeldrinArmor, $AccessoryType] = $BodyAccessoryType;

$AccessoryVar[PaddedArmor, $SpecialVar] = "1 1 7 30 4 5";
$AccessoryVar[LeatherArmor, $SpecialVar] = "1 2 7 80 4 7";
$AccessoryVar[StuddedLeather, $SpecialVar] = "1 3 7 130 4 9";
$AccessoryVar[SpikedLeather, $SpecialVar] = "1 5 7 180 4 13";
$AccessoryVar[HideArmor, $SpecialVar] = "1 5 7 230 4 18";
$AccessoryVar[ScaleMail, $SpecialVar] = "1 7 7 280 4 22";
$AccessoryVar[BrigandineArmor, $SpecialVar] = "1 8 7 330 4 28";
$AccessoryVar[ChainMail, $SpecialVar] = "1 8 7 380 4 33";
$AccessoryVar[RingMail, $SpecialVar] = "1 8 7 430 4 38";
$AccessoryVar[BandedMail, $SpecialVar] = "1 10 7 480 4 43";
$AccessoryVar[SplintMail, $SpecialVar] = "1 10 7 530 4 47";
$AccessoryVar[BronzePlateMail, $SpecialVar] = "1 15 7 580 4 53";
$AccessoryVar[PlateMail, $SpecialVar] = "1 25 7 630 4 58";
$AccessoryVar[FieldPlateArmor, $SpecialVar] = "1 30 7 680 4 64";
$AccessoryVar[DragonMail, $SpecialVar] = "1 30 7 730 4 70";
$AccessoryVar[FullPlateArmor, $SpecialVar] = "1 50 7 780 4 75";
$AccessoryVar[KeldrinArmor, $SpecialVar] = "1 60 7 880 3 350 4 110";
$AccessoryVar[ApprenticeRobe, $SpecialVar] = "3 40 4 1";
$AccessoryVar[LightRobe, $SpecialVar] = "3 100 4 2";
$AccessoryVar[FineRobe, $SpecialVar] = "3 200 4 5";
$AccessoryVar[BloodRobe, $SpecialVar] = "3 320 4 7";
$AccessoryVar[AdvisorRobe, $SpecialVar] = "3 400 4 8";
$AccessoryVar[ElvenRobe, $SpecialVar] = "3 480 4 11";
$AccessoryVar[RobeOfVenjance, $SpecialVar] = "3 600 4 15";
$AccessoryVar[PhensRobe, $SpecialVar] = "3 750 4 18";
$AccessoryVar[QuestMasterRobe, $SpecialVar] = "3 1000 4 1000 5 1000 6 1000 7 1000 10 1000 11 1000";

$AccessoryVar[PaddedArmor, $Weight] = 10;
$AccessoryVar[LeatherArmor, $Weight] = 15;
$AccessoryVar[StuddedLeather, $Weight] = 25;
$AccessoryVar[SpikedLeather, $Weight] = 25;
$AccessoryVar[HideArmor, $Weight] = 30;
$AccessoryVar[ScaleMail, $Weight] = 40;
$AccessoryVar[BrigandineArmor, $Weight] = 35;
$AccessoryVar[ChainMail, $Weight] = 40;
$AccessoryVar[RingMail, $Weight] = 30;
$AccessoryVar[BandedMail, $Weight] = 35;
$AccessoryVar[SplintMail, $Weight] = 40;
$AccessoryVar[BronzePlateMail, $Weight] = 45;
$AccessoryVar[PlateMail, $Weight] = 50;
$AccessoryVar[FieldPlateArmor, $Weight] = 60;
$AccessoryVar[FullPlateArmor, $Weight] = 70;
$AccessoryVar[ApprenticeRobe, $Weight] = 20;
$AccessoryVar[LightRobe, $Weight] = 19;
$AccessoryVar[BloodRobe, $Weight] = 18;
$AccessoryVar[AdvisorRobe, $Weight] = 17;
$AccessoryVar[RobeOfVenjance, $Weight] = 16;
$AccessoryVar[PhensRobe, $Weight] = 15;
$AccessoryVar[QuestMasterRobe, $Weight] = 14;
$AccessoryVar[FineRobe, $Weight] = 17;
$AccessoryVar[ElvenRobe, $Weight] = 10;
$AccessoryVar[DragonMail, $Weight] = 35;
$AccessoryVar[KeldrinArmor, $Weight] = 105;

$AccessoryVar[PaddedArmor, $MiscInfo] = "Padded Armor";
$AccessoryVar[LeatherArmor, $MiscInfo] = "Leather Armor";
$AccessoryVar[StuddedLeather, $MiscInfo] = "Studded Leather Armor";
$AccessoryVar[SpikedLeather, $MiscInfo] = "Spiked Leather Armor";
$AccessoryVar[HideArmor, $MiscInfo] = "Hide Armor";
$AccessoryVar[ScaleMail, $MiscInfo] = "Scale Mail";
$AccessoryVar[BrigandineArmor, $MiscInfo] = "Brigandine Armor";
$AccessoryVar[ChainMail, $MiscInfo] = "Chain Mail";
$AccessoryVar[RingMail, $MiscInfo] = "Ring Mail";
$AccessoryVar[BandedMail, $MiscInfo] = "Banded Mail";
$AccessoryVar[SplintMail, $MiscInfo] = "Splint Mail";
$AccessoryVar[BronzePlateMail, $MiscInfo] = "Bronze Plate Mail";
$AccessoryVar[PlateMail, $MiscInfo] = "Plate Mail";
$AccessoryVar[FieldPlateArmor, $MiscInfo] = "Field Plate Armor";
$AccessoryVar[FullPlateArmor, $MiscInfo] = "Full Plate Armor";
$AccessoryVar[ApprenticeRobe, $MiscInfo] = "Apprentice Robe";
$AccessoryVar[LightRobe, $MiscInfo] = "Light Robe";
$AccessoryVar[BloodRobe, $MiscInfo] = "Blood Robe";
$AccessoryVar[AdvisorRobe, $MiscInfo] = "A robe worn by the elite mages of the realm";
$AccessoryVar[RobeOfVenjance, $MiscInfo] = "The powerful mage Venjance's robe";
$AccessoryVar[PhensRobe, $MiscInfo] = "The godly mage Phen's robe";
$AccessoryVar[QuestMasterRobe, $MiscInfo] = "<f2>Quest Master Robe";
$AccessoryVar[FineRobe, $MiscInfo] = "A fine robe";
$AccessoryVar[ElvenRobe, $MiscInfo] = "An elven robe";
$AccessoryVar[DragonMail, $MiscInfo] = "A dragon scale mail";
$AccessoryVar[KeldrinArmor, $MiscInfo] = "Keldrinite-plated armor";

$ArmorSkin[MaleHuman,PaddedArmor] = "rpgpadded";
$ArmorSkin[MaleHuman,LeatherArmor] = "rpgleather";
$ArmorSkin[MaleHuman,StuddedLeather] = "rpgstudleather";
$ArmorSkin[MaleHuman,SpikedLeather] = "rpgspiked";
$ArmorSkin[MaleHuman,HideArmor] = "rpghide";
$ArmorSkin[MaleHuman,ScaleMail] = "rpgscalemail";
$ArmorSkin[MaleHuman,BrigandineArmor] = "rpgbrigandine";
$ArmorSkin[MaleHuman,ChainMail] = "rpgchainmail";
$ArmorSkin[MaleHuman,RingMail] = "rpgringmail";
$ArmorSkin[MaleHuman,BandedMail] = "rpgbandedmail";
$ArmorSkin[MaleHuman,SplintMail] = "rpgsplintmail";
$ArmorSkin[MaleHuman,BronzePlateMail] = "rpgbronzeplate";
$ArmorSkin[MaleHuman,PlateMail] = "rpgplatemail";
$ArmorSkin[MaleHuman,FieldPlateArmor] = "rpgfieldplate";
$ArmorSkin[MaleHuman,FullPlateArmor] = "rpgfullplate";
$ArmorSkin[MaleHuman,ApprenticeRobe] = "robepink";
$ArmorSkin[MaleHuman,LightRobe] = "robepurple";
$ArmorSkin[MaleHuman,BloodRobe] = "robered";
$ArmorSkin[MaleHuman,AdvisorRobe] = "robeblue";
$ArmorSkin[MaleHuman,RobeOfVenjance] = "robeblack";
$ArmorSkin[MaleHuman,PhensRobe] = "robewhite";
$ArmorSkin[MaleHuman,QuestMasterRobe] = "robeorange";
$ArmorSkin[MaleHuman,FineRobe] = "robebrown";
$ArmorSkin[MaleHuman,ElvenRobe] = "robegreen";
$ArmorSkin[MaleHuman,DragonMail] = "rpghuman6";
$ArmorSkin[MaleHuman,KeldrinArmor] = "rpgfullplate";

$ArmorSkin[FemaleHuman,PaddedArmor] = "rpgpadded";
$ArmorSkin[FemaleHuman,LeatherArmor] = "rpgleather";
$ArmorSkin[FemaleHuman,StuddedLeather] = "rpgstudleather";
$ArmorSkin[FemaleHuman,SpikedLeather] = "rpgspiked";
$ArmorSkin[FemaleHuman,HideArmor] = "rpghide";
$ArmorSkin[FemaleHuman,ScaleMail] = "rpgscalemail";
$ArmorSkin[FemaleHuman,BrigandineArmor] = "rpgbrigandine";
$ArmorSkin[FemaleHuman,ChainMail] = "rpgchainmail";
$ArmorSkin[FemaleHuman,RingMail] = "rpgringmail";
$ArmorSkin[FemaleHuman,BandedMail] = "rpgbandedmail";
$ArmorSkin[FemaleHuman,SplintMail] = "rpgsplintmail";
$ArmorSkin[FemaleHuman,BronzePlateMail] = "rpgbronzeplate";
$ArmorSkin[FemaleHuman,PlateMail] = "rpgplatemail";
$ArmorSkin[FemaleHuman,FieldPlateArmor] = "rpgfieldplate";
$ArmorSkin[FemaleHuman,FullPlateArmor] = "rpgfullplate";
$ArmorSkin[FemaleHuman,ApprenticeRobe] = "robepink";
$ArmorSkin[FemaleHuman,LightRobe] = "robepurple";
$ArmorSkin[FemaleHuman,BloodRobe] = "robered";
$ArmorSkin[FemaleHuman,AdvisorRobe] = "robeblue";
$ArmorSkin[FemaleHuman,RobeOfVenjance] = "robeblack";
$ArmorSkin[FemaleHuman,PhensRobe] = "robewhite";
$ArmorSkin[FemaleHuman,QuestMasterRobe] = "robeorange";
$ArmorSkin[FemaleHuman,FineRobe] = "robebrown";
$ArmorSkin[FemaleHuman,ElvenRobe] = "robegreen";
$ArmorSkin[FemaleHuman,DragonMail] = "rpghuman6";
$ArmorSkin[FemaleHuman,KeldrinArmor] = "rpgfullplate";

$ArmorSkin[MaleKijin,PaddedArmor] = "rpgorcpadded";
$ArmorSkin[MaleKijin,LeatherArmor] = "rpgorcleather";
$ArmorSkin[MaleKijin,StuddedLeather] = "rpgorcstudleather";
$ArmorSkin[MaleKijin,SpikedLeather] = "rpgorcspiked";
$ArmorSkin[MaleKijin,HideArmor] = "rpgorchide";
$ArmorSkin[MaleKijin,ScaleMail] = "rpgorcscalemail";
$ArmorSkin[MaleKijin,BrigandineArmor] = "rpgorcbrigandine";
$ArmorSkin[MaleKijin,ChainMail] = "rpgorcchainmail";
$ArmorSkin[MaleKijin,RingMail] = "rpgorcringmail";
$ArmorSkin[MaleKijin,BandedMail] = "rpgorcbandedmail";
$ArmorSkin[MaleKijin,SplintMail] = "rpgorcsplintmail";
$ArmorSkin[MaleKijin,BronzePlateMail] = "rpgorcbronzeplate";
$ArmorSkin[MaleKijin,PlateMail] = "rpgorcplatemail";
$ArmorSkin[MaleKijin,FieldPlateArmor] = "rpgorcfieldplate";
$ArmorSkin[MaleKijin,FullPlateArmor] = "rpgorcfullplate";
$ArmorSkin[MaleKijin,ApprenticeRobe] = "rpgorcrobepink";
$ArmorSkin[MaleKijin,LightRobe] = "rpgorcrobepurple";
$ArmorSkin[MaleKijin,BloodRobe] = "rpgorcrobered";
$ArmorSkin[MaleKijin,AdvisorRobe] = "rpgorcrobeblue";
$ArmorSkin[MaleKijin,RobeOfVenjance] = "rpgorcrobeblack";
$ArmorSkin[MaleKijin,PhensRobe] = "rpgorcrobewhite";
$ArmorSkin[MaleKijin,QuestMasterRobe] = "rpgorcrobeorange";
$ArmorSkin[MaleKijin,FineRobe] = "rpgorcrobebrown";
$ArmorSkin[MaleKijin,ElvenRobe] = "rpgorcrobegreen";
$ArmorSkin[MaleKijin,DragonMail] = "rpgorchuman6";
$ArmorSkin[MaleKijin,KeldrinArmor] = "rpgorcfullplate";

$ArmorSkin[FemaleKijin,PaddedArmor] = "rpgorcpadded";
$ArmorSkin[FemaleKijin,LeatherArmor] = "rpgorcleather";
$ArmorSkin[FemaleKijin,StuddedLeather] = "rpgorcstudleather";
$ArmorSkin[FemaleKijin,SpikedLeather] = "rpgorcspiked";
$ArmorSkin[FemaleKijin,HideArmor] = "rpgorchide";
$ArmorSkin[FemaleKijin,ScaleMail] = "rpgorcscalemail";
$ArmorSkin[FemaleKijin,BrigandineArmor] = "rpgorcbrigandine";
$ArmorSkin[FemaleKijin,ChainMail] = "rpgorcchainmail";
$ArmorSkin[FemaleKijin,RingMail] = "rpgorcringmail";
$ArmorSkin[FemaleKijin,BandedMail] = "rpgorcbandedmail";
$ArmorSkin[FemaleKijin,SplintMail] = "rpgorcsplintmail";
$ArmorSkin[FemaleKijin,BronzePlateMail] = "rpgorcbronzeplate";
$ArmorSkin[FemaleKijin,PlateMail] = "rpgorcplatemail";
$ArmorSkin[FemaleKijin,FieldPlateArmor] = "rpgorcfieldplate";
$ArmorSkin[FemaleKijin,FullPlateArmor] = "rpgorcfullplate";
$ArmorSkin[FemaleKijin,ApprenticeRobe] = "rpgorcrobepink";
$ArmorSkin[FemaleKijin,LightRobe] = "rpgorcrobepurple";
$ArmorSkin[FemaleKijin,BloodRobe] = "rpgorcrobered";
$ArmorSkin[FemaleKijin,AdvisorRobe] = "rpgorcrobeblue";
$ArmorSkin[FemaleKijin,RobeOfVenjance] = "rpgorcrobeblack";
$ArmorSkin[FemaleKijin,PhensRobe] = "rpgorcrobewhite";
$ArmorSkin[FemaleKijin,QuestMasterRobe] = "rpgorcrobeorange";
$ArmorSkin[FemaleKijin,FineRobe] = "rpgorcrobebrown";
$ArmorSkin[FemaleKijin,ElvenRobe] = "rpgorcrobegreen";
$ArmorSkin[FemaleKijin,DragonMail] = "rpgorchuman6";
$ArmorSkin[FemaleKijin,KeldrinArmor] = "rpgorcfullplate";

//the way it works is:
// $RACE[%clientId] @ $ArmorPlayerModel[WhateverArmor]
$ArmorPlayerModel[PaddedArmor] = "";
$ArmorPlayerModel[LeatherArmor] = "";
$ArmorPlayerModel[StuddedLeather] = "";
$ArmorPlayerModel[SpikedLeather] = "";
$ArmorPlayerModel[HideArmor] = "";
$ArmorPlayerModel[ScaleMail] = "";
$ArmorPlayerModel[BrigandineArmor] = "";
$ArmorPlayerModel[ChainMail] = "";
$ArmorPlayerModel[RingMail] = "";
$ArmorPlayerModel[BandedMail] = "";
$ArmorPlayerModel[SplintMail] = "";
$ArmorPlayerModel[BronzePlateMail] = "";
$ArmorPlayerModel[PlateMail] = "";
$ArmorPlayerModel[FieldPlateArmor] = "";
$ArmorPlayerModel[FullPlateArmor] = "";
$ArmorPlayerModel[ApprenticeRobe] = "Robed";
$ArmorPlayerModel[LightRobe] = "Robed";
$ArmorPlayerModel[BloodRobe] = "Robed";
$ArmorPlayerModel[AdvisorRobe] = "Robed";
$ArmorPlayerModel[RobeOfVenjance] = "Robed";
$ArmorPlayerModel[PhensRobe] = "Robed";
$ArmorPlayerModel[QuestMasterRobe] = "Robed";
$ArmorPlayerModel[FineRobe] = "Robed";
$ArmorPlayerModel[ElvenRobe] = "Robed";
$ArmorPlayerModel[DragonMail] = "";
$ArmorPlayerModel[KeldrinArmor] = "";

$ArmorHitSound[PaddedArmor] = SoundHitLeather;
$ArmorHitSound[LeatherArmor] = SoundHitLeather;
$ArmorHitSound[StuddedLeather] = SoundHitLeather;
$ArmorHitSound[SpikedLeather] = SoundHitLeather;
$ArmorHitSound[HideArmor] = SoundHitLeather;
$ArmorHitSound[ScaleMail] = SoundHitChain;
$ArmorHitSound[BrigandineArmor] = SoundHitChain;
$ArmorHitSound[ChainMail] = SoundHitChain;
$ArmorHitSound[RingMail] = SoundHitChain;
$ArmorHitSound[BandedMail] = SoundHitChain;
$ArmorHitSound[SplintMail] = SoundHitChain;
$ArmorHitSound[BronzePlateMail] = SoundHitPlate;
$ArmorHitSound[PlateMail] = SoundHitPlate;
$ArmorHitSound[FieldPlateArmor] = SoundHitPlate;
$ArmorHitSound[FullPlateArmor] = SoundHitPlate;
$ArmorHitSound[ApprenticeRobe] = SoundHitFlesh;
$ArmorHitSound[LightRobe] = SoundHitFlesh;
$ArmorHitSound[BloodRobe] = SoundHitFlesh;
$ArmorHitSound[AdvisorRobe] = SoundHitFlesh;
$ArmorHitSound[RobeOfVenjance] = SoundHitFlesh;
$ArmorHitSound[PhensRobe] = SoundHitFlesh;
$ArmorHitSound[QuestMasterRobe] = SoundHitFlesh;
$ArmorHitSound[FineRobe] = SoundHitFlesh;
$ArmorHitSound[ElvenRobe] = SoundHitFlesh;
$ArmorHitSound[DragonMail] = SoundHitChain;
$ArmorHitSound[KeldrinArmor] = SoundHitPlate;

//this list is used to make things easy when cycling between armors
$ArmorList[1] = "PaddedArmor";
$ArmorList[2] = "LeatherArmor";
$ArmorList[3] = "StuddedLeather";
$ArmorList[4] = "SpikedLeather";
$ArmorList[5] = "HideArmor";
$ArmorList[6] = "ScaleMail";
$ArmorList[7] = "BrigandineArmor";
$ArmorList[8] = "ChainMail";
$ArmorList[9] = "RingMail";
$ArmorList[10] = "BandedMail";
$ArmorList[11] = "SplintMail";
$ArmorList[12] = "BronzePlateMail";
$ArmorList[13] = "PlateMail";
$ArmorList[14] = "FieldPlateArmor";
$ArmorList[15] = "FullPlateArmor";
$ArmorList[16] = "ApprenticeRobe";
$ArmorList[17] = "LightRobe";
$ArmorList[18] = "BloodRobe";
$ArmorList[19] = "AdvisorRobe";
$ArmorList[20] = "RobeOfVenjance";
$ArmorList[21] = "PhensRobe";
$ArmorList[22] = "QuestMasterRobe";
$ArmorList[23] = "FineRobe";
$ArmorList[24] = "ElvenRobe";
$ArmorList[25] = "DragonMail";
$ArmorList[26] = "KeldrinArmor";

//============================================================================
ItemData PaddedArmor
{
	description = "Padded Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData PaddedArmor0
{
	description = "Padded Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData LeatherArmor
{
	description = "Leather Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData LeatherArmor0
{
	description = "Leather Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData StuddedLeather
{
	description = "Studded Leather Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData StuddedLeather0
{
	description = "Studded Leather Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData SpikedLeather
{
	description = "Spiked Leather Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData SpikedLeather0
{
	description = "Spiked Leather Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData HideArmor
{
	description = "Hide Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData HideArmor0
{
	description = "Hide Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData ScaleMail
{
	description = "Scale Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData ScaleMail0
{
	description = "Scale Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData BrigandineArmor
{
	description = "Brigandine Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData BrigandineArmor0
{
	description = "Brigandine Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData ChainMail
{
	description = "Chain Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData ChainMail0
{
	description = "Chain Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData RingMail
{
	description = "Ring Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData RingMail0
{
	description = "Ring Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData BandedMail
{
	description = "Banded Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData BandedMail0
{
	description = "Banded Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData SplintMail
{
	description = "Splint Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData SplintMail0
{
	description = "Splint Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData DragonMail
{
	description = "Dragon Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData DragonMail0
{
	description = "Dragon Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData BronzePlateMail
{
	description = "Bronze Plate Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData BronzePlateMail0
{
	description = "Bronze Plate Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData PlateMail
{
	description = "Plate Mail";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData PlateMail0
{
	description = "Plate Mail";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData FieldPlateArmor
{
	description = "Field Plate Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData FieldPlateArmor0
{
	description = "Field Plate Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData FullPlateArmor
{
	description = "Full Plate Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData FullPlateArmor0
{
	description = "Full Plate Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData KeldrinArmor
{
	description = "Keldrin Armor";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData KeldrinArmor0
{
	description = "Keldrin Armor";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData ApprenticeRobe
{
	description = "Apprentice Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData ApprenticeRobe0
{
	description = "Apprentice Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData LightRobe
{
	description = "Light Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData LightRobe0
{
	description = "Light Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData FineRobe
{
	description = "Fine Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData FineRobe0
{
	description = "Fine Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData BloodRobe
{
	description = "Blood Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData BloodRobe0
{
	description = "Blood Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData AdvisorRobe
{
	description = "Advisor Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData AdvisorRobe0
{
	description = "Advisor Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData ElvenRobe
{
	description = "Elven Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData ElvenRobe0
{
	description = "Elven Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData RobeOfVenjance
{
	description = "Robe Of Venjance";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData RobeOfVenjance0
{
	description = "Robe Of Venjance";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData PhensRobe
{
	description = "Phen's Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData PhensRobe0
{
	description = "Phen's Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};

//============================================================================
ItemData QuestMasterRobe
{
	description = "Quest Master Robe";
	className = "Accessory";
	shapeFile = "discammo";

	heading = "eMiscellany";
	price = 0;
};
ItemData QuestMasterRobe0
{
	description = "Quest Master Robe";
	className = "Equipped";
	shapeFile = "discammo";

	heading = "aArmor";
};
