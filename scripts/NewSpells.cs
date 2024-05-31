
//Cantrip Spells use only Stamina
$SpellTypeCantrip = 0;
//Magic spells can use magic and stamina
$SpellTypeMagic = 1;

$SpellCastTypeProjectile = 0; //Spells that simply shoot a single projectile
$SpellCastTypeBomb = 1; //Spells that drop an explosive (old style spells)
$SpellCastTypeSelf = 2; //Spells that effect just the caster. Heal or buffs.
$SpellCastTypeTarget = 3; //Spell that effect the target.
$SpellCastTypeSelfOrLOS = 4; //Spells that effect yourself or your target.
$SpellCastTypeScripted = 5; //Spells with custom scripted effects. "Other"
$SpellCastTypePlayerCharge = 6;
//Example: Samples of what fields each spell type needs.  Not an exhaustive list.
//Base types
//$Spell::keyword[0] = "spellkeyword";
//$Spell::index[spellkeyword] = 0;
//$Spell::name[0] = "Spell Name";
//$Spell::delay[0] = 1.5;
//$Spell::recoveryTime[0] = 2.625;
//$Spell::canMove[0] = true;
//$Spell::graceDistance[0] = 2; //Only relevant if canMove is false
//$Spell::aiRefVal[0] = 55; //Optional value for AI. Priority
//
////Common Effects and Sound
//$Spell::castingAura[0] = SpellEffectAura2;
//$Spell::startSound[0] = ActivateBF;
//$Spell::endSound[0] = NoSound; //Can also be blank for no sound.  End sound plays when the spell is cast
//
//// Cantrips cost only stamina, improve over time with higher skill
//$Spell::type[0] = $SpellTypeCantrip;
//$Spell::baseStamina[0] = 30;
//$Spell::minStamina[0] = 1;
//
//// Magic costs mana.  Can also cost stamina
//$Spell::type[0] = $SpellTypeMagic;
//$Spell::baseStamina[0] = 2;
//$Spell::mana[0] = 1;
//
//$Spell::castType[0] = $SpellCastTypeProjectile;
//$Spell::projectileData[0] = Thorn; //Projectile data object
//
//$Spell::castType[0] = $SpellCastTypeBomb;
//$Spell::damageValue[0] = "20";
//$Spell::LOSrange[0] = 80;
//
//$Spell::castType[0] = $SpellCastTypeSelf;
//$Spell::effectValue[0] = 20; //Can mean multiple things.  WIP?
//$Spell::effectBonus[0] = "ATK 30"; //Bonus effect
//
//$Spell::castType[0] = $SpellCastTypeTarget;
//$Spell::damageValue[0] = "20";
//$Spell::effectValue[0] = 20;
//$Spell::effectBonus[0] = "ATK 30";
//$Spell::LOSrange[0] = 80;
//$Spell::targetAllies[0] = true;
//
//$Spell::castType[0] = $SpellCastTypeSelfOrLOS;
//$Spell::effectValue[0] = 20; //Can mean multiple things.  WIP?
//$Spell::effectBonus[0] = "ATK 30"; //Bonus effect
//$Spell::LOSrange[0] = 80;
//$Spell::targetAllies[0] = true;
//$Spell::groupListCheck[0] = False;
//
//$Spell::castType[0] = $$SpellCastTypeScripted; //Whatever the spell needs.
//$Spell::forceLOSCheck[0] = true; //Force an LOS check
//$Spell::LOSrange[0] = 80;

$Spell::keyword[1] = "thorn";
$Spell::index[thorn] = 1;
$Spell::name[1] = "Thorn";
$Spell::description[1] = "Casts thorn.";
$Spell::damageValue[1] = 20;
$Spell::type[1] = $SpellTypeCantrip;
$Spell::baseStamina[1] = 5;
$Spell::minStamina[1] = 0.5;
$Spell::staminaFalloffFactor[1] = 1.5; //How many times the minimum SkillRestriction until you cast a min stamina
$Spell::delay[1] = 0.1;
$Spell::recoveryTime[1] = 2.625;
$Spell::canMove[1] = true;
$Spell::LOSrange[1] = 80;
$Spell::castType[1] = $SpellCastTypeProjectile;
$Spell::projectileData[1] = Thorn;
$Spell::startSound[1] = ActivateFK;
$Spell::overrideEndSound[1] = true; //Optional if false
$Spell::aiRefVal[1] = 20;
$Spell::useSkillOnCast[1] = false;
$SkillType[thorn] = $SkillNatureCasting;
$SkillRestriction[thorn] = $SkillNatureCasting @ " 15";

$Spell::keyword[2] = "firebolt";
$Spell::index[firebolt] = 2;
$Spell::name[2] = "Firebolt";
$Spell::damageValue[2] = 40; //Means nothing, except for info for projectile spells
$Spell::description[2] = "Shoot a mote of fire.";
$Spell::type[2] = $SpellTypeCantrip;
$Spell::baseStamina[2] = 25;
$Spell::minStamina[2] = 5;
$Spell::manaCost[2] = 2;
$Spell::staminaFalloffFactor[2] = 4;
$Spell::delay[2] = 1;
$Spell::LOSrange[2] = 80;
$Spell::recoveryTime[2] = 2.625;
$Spell::canMove[2] = true;
$Spell::castType[2] = $SpellCastTypeProjectile;
$Spell::projectileData[2] = Firebolt;//Thorn;
$Spell::startSound[2] = ActivateAB;
$Spell::castSound[2] = HitPawnDT;
$Spell::overrideEndSound[2] = true;
$Spell::aiRefVal[2] = 20;
$Spell::useSkillOnCast[2] = false;
$SkillType[firebolt] = $SkillOffensiveCasting;
$SkillRestriction[firebolt] = $SkillOffensiveCasting @ " 20";

$Spell::keyword[3] = "icespike";
$Spell::index[icespike] = 3;
$Spell::name[3] = "Icespike";
$Spell::damageValue[3] = 35;
$Spell::description[3] = "Casts icespike.";
$Spell::type[3] = $SpellTypeCantrip;
$Spell::baseStamina[3] = 15;
$Spell::minStamina[3] = 1;
$Spell::staminaFalloffFactor[3] = 1.5;
$Spell::delay[3] = 0.1;
$Spell::recoveryTime[3] = 2.625;
$Spell::canMove[3] = true;
$Spell::castType[3] = $SpellCastTypeProjectile;
$Spell::projectileData[3] = icespike;//Thorn;
$Spell::startSound[3] = ActivateFK;
$Spell::overrideEndSound[3] = true;
$Spell::aiRefVal[3] = 20;
$Spell::useSkillOnCast[3] = false;
$SkillType[icespike] = $SkillOffensiveCasting;
$SkillRestriction[icespike] = $SkillOffensiveCasting @ " 45";
$Spell::itemReq[3] = "scrolloficemagic 1";

$Spell::keyword[4] = "cloud";
$Spell::index[cloud] = 4;
$Spell::name[4] = "Cloud Attack";
$Spell::damageValue[4] = 85;
$Spell::description[4] = "Casts an explosive.";
$Spell::type[4] = $SpellTypeCantrip;
$Spell::baseStamina[4] = 20;
$Spell::minStamina[4] = 5;
$Spell::staminaFalloffFactor[4] = 1.5;
$Spell::delay[4] = 1.5;
$Spell::recoveryTime[4] = 2.625;
$Spell::canMove[4] = false;
$Spell::graceDistance[4] = 2;
$Spell::castType[4] = $SpellCastTypeProjectile;
$Spell::projectileData[4] = cloud;//Thorn;
$Spell::startSound[4] = ActivateBF;
$Spell::castSound[4] = LaunchFB;
$Spell::overrideEndSound[4] = true;
$Spell::aiRefVal[4] = 20;
$Spell::useSkillOnCast[4] = false;
$SkillType[cloud] = $SkillOffensiveCasting;
$SkillRestriction[cloud] = $SkillOffensiveCasting @ " 145";

$Spell::keyword[5] = "melt";
$Spell::index[melt] = 5;
$Spell::damageValue[5] = 140;
$Spell::name[5] = "Melt Bomb Attack";
$Spell::description[5] = "Casts an explosive.";
$Spell::type[5] = $SpellTypeCantrip;
$Spell::baseStamina[5] = 40;
$Spell::minStamina[5] = 15;
$Spell::staminaFalloffFactor[5] = 1.5;
$Spell::delay[5] = 1.5;
$Spell::recoveryTime[5] = 2.625;
$Spell::canMove[5] = false;
$Spell::graceDistance[5] = 2;
$Spell::castType[5] = $SpellCastTypeProjectile;
$Spell::projectileData[5] = melt;//Thorn;
$Spell::startSound[5] = ActivateBF;
$Spell::castSound[5] = ActivateAB;
$Spell::overrideEndSound[5] = true;
$Spell::aiRefVal[5] = 20;
$Spell::useSkillOnCast[5] = false;
$SkillType[melt] = $SkillOffensiveCasting;
$SkillRestriction[melt] = $SkillOffensiveCasting @ " 220";

$Spell::keyword[6] = "fireblast";
$Spell::index[fireblast] = 6;
$Spell::damageValue[6] = 240;
$Spell::name[6] = "Fire Blast";
$Spell::description[6] = "Launch an explosive fireball.  Can move slightly while charging.";
$Spell::type[6] = $SpellTypeMagic;
$Spell::manaCost[6] = 80;
$Spell::baseStamina[6] = 15;
$Spell::delay[6] = 8;
$Spell::recoveryTime[6] = 20;
$Spell::auraEffect[6] = SpellEffectAura2;
$Spell::startSound[6] = LaunchLS;
$Spell::castSound[6] = Explode3FW;
$Spell::canMove[6] = false;
$Spell::graceDistance[6] = 10;
$Spell::castType[6] = $SpellCastTypeScripted;
$Spell::overrideEndSound[6] = true;
$Spell::aiRefVal[6] = -20;
$Spell::useSkillOnCast[6] = false;
$SkillType[fireblast] = $SkillOffensiveCasting;
$SkillRestriction[fireblast] = $SkillOffensiveCasting @ " 450";

$Spell::keyword[7] = "teleport";
$Spell::index[teleport] = 7;
$Spell::name[7] = "Teleport";
$Spell::description[7] = "Teleports you near a zone";
$Spell::type[7] = $SpellTypeCantrip;
$Spell::baseStamina[7] = 80;
$Spell::minStamina[7] = 15;
$Spell::manaCost[7] = 5;
$Spell::staminaFalloffFactor[7] = 3;
$Spell::delay[7] = 8;
$Spell::recoveryTime[7] = 23;
$Spell::auraEffect[7] = SpellEffectAura1;
$Spell::startSound[7] = Portal11;
$Spell::endSound[7] = ActivateCH;
$Spell::canMove[7] = false;
$Spell::graceDistance[7] = 2;
$Spell::castType[7] = $SpellCastTypeScripted;
$Spell::overrideEndSound[7] = true;
$Spell::aiRefVal[7] = 0;
$Spell::useSkillOnCast[7] = true;
$SkillType[teleport] = $SkillNatureCasting;
$SkillRestriction[teleport] = $SkillNatureCasting @ " 60";

//WIP
$Spell::keyword[8] = "heal";
$Spell::index[heal] = 8;
$Spell::name[8] = "Heal Self";
$Spell::description[8] = "Heals the caster.";
$Spell::type[8] = $SpellTypeCantrip;
$Spell::baseStamina[8] = 20;
$Spell::minStamina[8] = 5;
$Spell::staminaFalloffFactor[8] = 3;
$Spell::delay[8] = 1.5;
$Spell::recoveryTime[8] = 2.25;
$Spell::canMove[8] = false;
$Spell::graceDistance[8] = 2;
$Spell::castType[8] = $SpellCastTypeSelf;
$Spell::effectVars[8] = "HP 6"; //Testing
$Spell::startSound[8] = DeActivateWA;
$Spell::endSound[8] = ActivateAR;
$Spell::groupListCheck[8] = false;
$Spell::aiRefVal[8] = -6;
$Spell::useSkillOnCast[8] = true;
$SkillType[heal] = $SkillDefensiveCasting;
$SkillRestriction[heal] = $SkillDefensiveCasting @ " 10";

$Spell::keyword[9] = "shield";
$Spell::index[shield] = 9;
$Spell::name[9] = "Shield Self";
$Spell::description[9] = "A magical shield adds 50 DEF to the caster.";
$Spell::type[9] = $SpellTypeCantrip;
$Spell::baseStamina[9] = 30;
$Spell::minStamina[9] = 15;
$Spell::staminaFalloffFactor[9] = 3;
$Spell::delay[9] = 2.0;
$Spell::recoveryTime[9] = 8;
$Spell::castType[9] = $SpellCastTypeSelf;
$Spell::effectVars[9] = "BONUS DEF 50 150";//5 minutes
$Spell::startSound[9] = ActivateTR;
//$Spell::castSound[9] = ActivateTD;
$Spell::endSound[9] = ActivateTD;
$Spell::groupListCheck[9] = false;
$Spell::refVal[9] = -10;
$Spell::canMove[9] = false;
$Spell::graceDistance[9] = 2;
$Spell::useSkillOnCast[9] = true;
$SkillType[shield] = $SkillDefensiveCasting;
$SkillRestriction[shield] = $SkillDefensiveCasting @ " 20";

$Spell::keyword[10] = "advshield1";
$Spell::index[advshield1] = 10;
$Spell::name[10] = "Shield Self Or Other (1st)";
$Spell::description[10] = "A magical shield that adds 80 DEF to the caster or target in LOS.";
$Spell::message[10] = "Shielding %s";
$Spell::type[10] = $SpellTypeCantrip;
$Spell::baseStamina[10] = 35;
$Spell::minStamina[10] = 20;
$Spell::staminaFalloffFactor[10] = 3;
$Spell::delay[10] = 2.0;
$Spell::recoveryTime[10] = 10;
$Spell::castType[10] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[10] = 80;
$Spell::effectVars[10] = "BONUS DEF 80 165";//5 minutes
$Spell::startSound[10] = ActivateTR;
$Spell::endSound[10] = ActivateTD;
$Spell::teamTargetOnly[10] = true;
$Spell::groupListCheck[10] = false;
$Spell::refVal[10] = -11;
$Spell::canMove[10] = false;
$Spell::graceDistance[10] = 2;
$Spell::useSkillOnCast[10] = true;
$SkillType[advshield1] = $SkillDefensiveCasting;
$SkillRestriction[advshield1] = $SkillDefensiveCasting @ " 60";

$Spell::keyword[11] = "advheal1";
$Spell::index[advheal1] = 11;
$Spell::name[11] = "Heal Self or Other (1st)";
$Spell::description[11] = "Heals the caster or someone in the LOS.";
$Spell::type[11] = $SpellTypeCantrip;
$Spell::baseStamina[11] = 35;
$Spell::minStamina[11] = 8;
$Spell::staminaFalloffFactor[11] = 3;
$Spell::delay[11] = 1.5;
$Spell::recoveryTime[11] = 3.25;
$Spell::canMove[11] = false;
$Spell::graceDistance[11] = 2;
$Spell::castType[11] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[11] = 80;
$Spell::effectVars[11] = "HP 10"; //Testing
$Spell::startSound[11] = DeActivateWA;
$Spell::endSound[11] = ActivateAR;
$Spell::teamTargetOnly[11] = true;
//$Spell::teamTargetOnly[11] = true;
$Spell::groupListCheck[11] = false;
$Spell::aiRefVal[11] = -10;
$Spell::useSkillOnCast[11] = true;
$SkillType[advheal1] = $SkillDefensiveCasting;
$SkillRestriction[advheal1] = $SkillDefensiveCasting @ " 80";

$Spell::keyword[12] = "advheal2";
$Spell::index[advheal2] = 12;
$Spell::name[12] = "Heal Self Or Other (2nd)";
$Spell::description[12] = "Heals the caster or someone in the LOS.";
$Spell::type[12] = $SpellTypeCantrip;
$Spell::baseStamina[12] = 40;
$Spell::minStamina[12] = 10;
$Spell::staminaFalloffFactor[12] = 3;
$Spell::delay[12] = 1.5;
$Spell::recoveryTime[12] = 4.0;
$Spell::canMove[12] = false;
$Spell::graceDistance[12] = 2;
$Spell::castType[12] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[12] = 80;
$Spell::effectVars[12] = "HP 15"; //Testing
$Spell::startSound[12] = DeActivateWA;
$Spell::endSound[12] = ActivateAR;
$Spell::teamTargetOnly[12] = true;
$Spell::groupListCheck[12] = false;
$Spell::aiRefVal[12] = -15;
$Spell::useSkillOnCast[12] = true;
$SkillType[advheal2] = $SkillDefensiveCasting;
$SkillRestriction[advheal2] = $SkillDefensiveCasting @ " 110";

$Spell::keyword[13] = "advheal3";
$Spell::index[advheal3] = 13;
$Spell::name[13] = "Heal Self Or Other (3rd)";
$Spell::description[13] = "Heals the caster or someone in the LOS.";
$Spell::type[13] = $SpellTypeCantrip;
$Spell::baseStamina[13] = 50;
$Spell::minStamina[13] = 15;
$Spell::staminaFalloffFactor[13] = 3;
$Spell::delay[13] = 1.5;
$Spell::recoveryTime[13] = 4.75;
$Spell::canMove[13] = false;
$Spell::graceDistance[13] = 2;
$Spell::castType[13] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[13] = 80;
$Spell::effectVars[13] = "HP 25"; //Testing
$Spell::startSound[13] = DeActivateWA;
$Spell::endSound[13] = ActivateAR;
$Spell::teamTargetOnly[13] = true;
$Spell::groupListCheck[13] = false;
$Spell::aiRefVal[13] = -15;
$Spell::useSkillOnCast[13] = true;
$SkillType[advheal3] = $SkillDefensiveCasting;
$SkillRestriction[advheal3] = $SkillDefensiveCasting @ " 200";

$Spell::keyword[14] = "advheal4";
$Spell::index[advheal4] = 14;
$Spell::name[14] = "Heal Self Or Other (4th)";
$Spell::description[14] = "Heals the caster or someone in the LOS.";
$Spell::type[14] = $SpellTypeCantrip;
$Spell::baseStamina[14] = 50;
$Spell::minStamina[14] = 18;
$Spell::staminaFalloffFactor[14] = 3.5;
$Spell::delay[14] = 1.5;
$Spell::recoveryTime[14] = 5;
$Spell::canMove[14] = false;
$Spell::graceDistance[14] = 2;
$Spell::castType[14] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[14] = 80;
$Spell::effectVars[14] = "HP 35"; //Testing
$Spell::startSound[14] = DeActivateWA;
$Spell::endSound[14] = ActivateAR;
$Spell::teamTargetOnly[14] = true;
$Spell::groupListCheck[14] = false;
$Spell::aiRefVal[14] = -35;
$Spell::useSkillOnCast[14] = true;
$SkillType[advheal4] = $SkillDefensiveCasting;
$SkillRestriction[advheal4] = $SkillDefensiveCasting @ " 320";

$Spell::keyword[15] = "advshield2";
$Spell::index[advshield2] = 15;
$Spell::name[15] = "Shield Self Or Other (2nd)";
$Spell::description[15] = "A magical shield that adds 70 DEF and 50 MDEF to the caster or target in LOS.";
$Spell::message[15] = "Shielding %s";
$Spell::type[15] = $SpellTypeCantrip;
$Spell::baseStamina[15] = 40;
$Spell::minStamina[15] = 12;
$Spell::staminaFalloffFactor[15] = 3;
$Spell::delay[15] = 2.0;
$Spell::recoveryTime[15] = 12;
$Spell::castType[15] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[15] = 80;
$Spell::effectVars[15] = "BONUS DEF 70 190,BONUS MDEF 50 190";//6:20 minutes
$Spell::startSound[15] = ActivateTR;
$Spell::endSound[15] = ActivateTD;
$Spell::teamTargetOnly[15] = true;
$Spell::groupListCheck[15] = false;
$Spell::refVal[15] = -12;
$Spell::canMove[15] = false;
$Spell::graceDistance[15] = 2;
$Spell::useSkillOnCast[15] = true;
$SkillType[advshield2] = $SkillDefensiveCasting;
$SkillRestriction[advshield2] = $SkillDefensiveCasting @ " 140";

$Spell::keyword[16] = "advshield3";
$Spell::index[advshield3] = 16;
$Spell::name[16] = "Shield Self Or Other (3rd)";
$Spell::description[16] = "A magical shield that adds 120 DEF and 80 MDEF to the caster or target in LOS.";
$Spell::message[16] = "Shielding %s";
$Spell::type[16] = $SpellTypeCantrip;
$Spell::baseStamina[16] = 60;
$Spell::minStamina[16] = 25;
$Spell::staminaFalloffFactor[16] = 3;
$Spell::delay[16] = 2.0;
$Spell::recoveryTime[16] = 14;
$Spell::castType[16] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[16] = 80;
$Spell::effectVars[16] = "BONUS DEF 120 218,BONUS MDEF 80 218";//7:16 minutes
$Spell::startSound[16] = ActivateTR;
$Spell::endSound[16] = ActivateTD;
$Spell::teamTargetOnly[16] = true;
$Spell::groupListCheck[16] = false;
$Spell::refVal[16] = -12;
$Spell::canMove[16] = false;
$Spell::graceDistance[16] = 2;
$Spell::useSkillOnCast[16] = true;
$SkillType[advshield3] = $SkillDefensiveCasting;
$SkillRestriction[advshield3] = $SkillDefensiveCasting @ " 290";

$Spell::keyword[17] = "advshield4";
$Spell::index[advshield4] = 17;
$Spell::name[17] = "Shield Self Or Other (4th)";
$Spell::description[17] = "A magical shield that adds 170 MDEF to the caster or target in LOS.";
$Spell::message[17] = "Shielding %s";
$Spell::type[17] = $SpellTypeCantrip;
$Spell::baseStamina[17] = 60;
$Spell::minStamina[17] = 25;
$Spell::staminaFalloffFactor[17] = 3;
$Spell::delay[17] = 2.0;
$Spell::recoveryTime[17] = 16;
$Spell::castType[17] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[17] = 80;
$Spell::effectVars[17] = "BONUS MDEF 170 255";//8:30 minutes
$Spell::startSound[17] = ActivateTR;
$Spell::endSound[17] = ActivateTD;
$Spell::teamTargetOnly[17] = true;
$Spell::groupListCheck[17] = false;
$Spell::refVal[17] = -14;
$Spell::canMove[17] = false;
$Spell::graceDistance[17] = 2;
$Spell::useSkillOnCast[17] = true;
$SkillType[advshield4] = $SkillDefensiveCasting;
$SkillRestriction[advshield4] = $SkillDefensiveCasting @ " 420";

$Spell::keyword[18] = "shell";
$Spell::index[shell] = 18;
$Spell::name[18] = "Hardend Shell";
$Spell::description[18] = "A magic shell covers the caster or their target, granting AMR 1 and DEF 150";
$Spell::message[18] = "Shelling %s";
$Spell::type[18] = $SpellTypeMagic;
$Spell::baseStamina[18] = 15;
$Spell::manaCost[18] = 20;
$Spell::delay[18] = 4.0;
$Spell::recoveryTime[18] = 30;
$Spell::castType[18] = $SpellCastTypeScripted;
$Spell::LOSrange[18] = 80;
$Spell::effectVars[18] = "BONUS AMR 1 150,BONUS DEF 150 150,BONUS SHELL 1 150";//5 minutes
$Spell::auraEffect[18] = SpellEffectAura2;
$Spell::startSound[18] = ActivateTR;
$Spell::endSound[18] = ActivateTD;
$Spell::teamTargetOnly[18] = true;
$Spell::groupListCheck[18] = false;
$Spell::refVal[18] = -14;
$Spell::canMove[18] = false;
$Spell::graceDistance[18] = 2;
$Spell::useSkillOnCast[18] = true;
$SkillType[shell] = $SkillDefensiveCasting;
$SkillRestriction[shell] = $SkillDefensiveCasting @ " 150";

$Spell::keyword[19] = "shell2";
$Spell::index[shell2] = 19;
$Spell::name[19] = "Hardend Shell (2nd)";
$Spell::description[19] = "A magic shell covers the caster or their target, granting AMR 4 and DEF 150";
$Spell::message[19] = "Shelling %s";
$Spell::type[19] = $SpellTypeMagic;
$Spell::baseStamina[19] = 15;
$Spell::manaCost[19] = 40;
$Spell::delay[19] = 4.0;
$Spell::recoveryTime[19] = 30;
$Spell::castType[19] = $SpellCastTypeScripted;
$Spell::LOSrange[19] = 80;
$Spell::effectVars[19] = "BONUS AMR 4 150,BONUS DEF 150 150,BONUS SHELL 1 150";//5 minutes
$Spell::auraEffect[19] = SpellEffectAura2;
$Spell::startSound[19] = ActivateTR;
$Spell::endSound[19] = ActivateTD;
$Spell::teamTargetOnly[19] = true;
$Spell::groupListCheck[19] = false;
$Spell::refVal[19] = -14;
$Spell::canMove[19] = false;
$Spell::graceDistance[19] = 2;
$Spell::useSkillOnCast[19] = true;
$SkillType[shell2] = $SkillDefensiveCasting;
$SkillRestriction[shell2] = $SkillDefensiveCasting @ " 300";

$Spell::keyword[20] = "transport";
$Spell::index[transport] = 20;
$Spell::name[20] = "Transport";
$Spell::description[20] = "Transports to a specific zone";
$Spell::type[20] = $SpellTypeMagic;
$Spell::baseStamina[20] = 15;
$Spell::manaCost[20] = 25;
$Spell::delay[20] = 8;
$Spell::recoveryTime[20] = 20;
$Spell::auraEffect[20] = SpellEffectAura1;
$Spell::startSound[20] = RespawnB;
$Spell::endSound[20] = ActivateCH;
$Spell::canMove[20] = false;
$Spell::graceDistance[20] = 2;
$Spell::castType[20] = $SpellCastTypeScripted;
$Spell::overrideEndSound[20] = false;
$Spell::aiRefVal[20] = 0;
$Spell::useSkillOnCast[20] = true;
$SkillType[transport] = $SkillNatureCasting;
$SkillRestriction[transport] = $SkillNatureCasting @ " 200";

//Grouplist test needs testing
$Spell::keyword[21] = "advtransport";
$Spell::index[advtransport] = 21;
$Spell::name[21] = "Advanced Transport";
$Spell::description[21] = "Transports self OR person in line-of-sight to a specific zone.  Faster cast speed but at higher mana cost.";
$Spell::type[21] = $SpellTypeMagic;
$Spell::baseStamina[21] = 15;
$Spell::manaCost[21] = 35;
$Spell::delay[21] = 3.5;
$Spell::recoveryTime[21] = 27;
$Spell::LOSrange[21] = 500;
$Spell::auraEffect[21] = SpellEffectAura1;
$Spell::startSound[21] = RespawnB;
$Spell::endSound[21] = ActivateCH;
$Spell::canMove[21] = false;
$Spell::graceDistance[21] = 2;
$Spell::castType[21] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[21] = true; //Needed to be able to access los::object on scripted spells
$Spell::overrideEndSound[21] = false;
$Spell::groupListCheck[21] = true;
$Spell::teamTargetOnly[21] = true;
$Spell::aiRefVal[21] = 0;
$Spell::useSkillOnCast[21] = true;
$SkillType[advtransport] = $SkillNatureCasting;
$SkillRestriction[advtransport] = $SkillNatureCasting @ " 350";

$Spell::keyword[22] = "translocate";
$Spell::index[translocate] = 22;
$Spell::name[22] = "Translocate";
$Spell::description[22] = "Teleport to a position in your line of sight or swap places with your traget.";
$Spell::type[22] = $SpellTypeMagic;
$Spell::baseStamina[22] = 15;
$Spell::manaCost[22] = 20;
$Spell::delay[22] = 10;
$Spell::recoveryTime[22] = 15;
$Spell::LOSrange[22] = 250;
$Spell::auraEffect[22] = SpellEffectAura1;
$Spell::startSound[22] = RespawnB;
$Spell::endSound[22] = UnravelAM;
$Spell::canMove[22] = false;
$Spell::graceDistance[22] = 2;
$Spell::castType[22] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[22] = true; //Needed to be able to access los::object on scripted spells
$Spell::overrideEndSound[22] = false;
$Spell::groupListCheck[22] = false;
$Spell::aiRefVal[22] = 0;
$Spell::useSkillOnCast[22] = true;
$SkillType[translocate] = $SkillNatureCasting;
$SkillRestriction[translocate] = $SkillNatureCasting @ " 100";

$Spell::keyword[23] = "fasttranslocate";
$Spell::index[fasttranslocate] = 23;
$Spell::name[23] = "Fast Translocate";
$Spell::description[23] = "Translocate spell that casts quicker and can be used while moving, but at a much heavier mana cost.";
$Spell::type[23] = $SpellTypeMagic;
$Spell::baseStamina[23] = 15;
$Spell::manaCost[23] = 100;
$Spell::delay[23] = 0.2;
$Spell::recoveryTime[23] = 35;
$Spell::LOSrange[23] = 250;
$Spell::auraEffect[23] = SpellEffectAura1;
$Spell::startSound[23] = UnravelAM;
$Spell::endSound[23] = UnravelAM;
$Spell::canMove[23] = true;
$Spell::castType[23] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[23] = true; //Needed to be able to access los::object on scripted spells
$Spell::overrideEndSound[23] = false;
$Spell::groupListCheck[23] = false;
$Spell::aiRefVal[23] = 0;
$Spell::useSkillOnCast[23] = true;
$SkillType[fasttranslocate] = $SkillNatureCasting;
$SkillRestriction[fasttranslocate] = $SkillNatureCasting @ " 500";

$Spell::keyword[24] = "advtranslocate";
$Spell::index[advtranslocate] = 24;
$Spell::name[24] = "Advanced Translocate";
$Spell::description[24] = "Translocation spell with a much longer distance. Can also move while casting.";
$Spell::type[24] = $SpellTypeMagic;
$Spell::baseStamina[24] = 20;
$Spell::manaCost[24] = 60;
$Spell::delay[24] = 10;
$Spell::recoveryTime[24] = 15;
$Spell::LOSrange[24] = 1200;
$Spell::auraEffect[24] = SpellEffectAura1;
$Spell::startSound[24] = RespawnB;
$Spell::endSound[24] = UnravelAM;
$Spell::canMove[24] = true;
$Spell::castType[24] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[24] = true; //Needed to be able to access los::object on scripted spells
$Spell::overrideEndSound[24] = false;
$Spell::groupListCheck[24] = false;
$Spell::aiRefVal[24] = 0;
$Spell::useSkillOnCast[24] = true;
$SkillType[advtranslocate] = $SkillNatureCasting;
$SkillRestriction[advtranslocate] = $SkillNatureCasting @ " 750";

$Spell::keyword[25] = "arcshot";
$Spell::index[arcshot] = 25;
$Spell::name[25] = "Arc Shot";
$Spell::damageValue[25] = 20;
$Spell::description[25] = "Shoots a small electric bolt.";
$Spell::type[25] = $SpellTypeCantrip;
$Spell::baseStamina[25] = 10;
$Spell::minStamina[25] = 2;
$Spell::LOSrange[25] = 80;
$Spell::staminaFalloffFactor[25] = 3;
$Spell::delay[25] = 0.1;
$Spell::recoveryTime[25] = 1.625;
$Spell::canMove[25] = true;
$Spell::castType[25] = $SpellCastTypeProjectile;
$Spell::projectileData[25] = ManaBoltProj;
$Spell::startSound[25] = ActivateFK;
$Spell::overrideEndSound[25] = true; //Optional if false
$Spell::aiRefVal[25] = 15;
$Spell::useSkillOnCast[25] = false;
$SkillType[arcshot] = $SkillOffensiveCasting;
$SkillRestriction[arcshot] = $SkillOffensiveCasting @ " 15";

$Spell::keyword[26] = "soften";
$Spell::index[soften] = 26;
$Spell::name[26] = "Soften";
$Spell::description[26] = "Softens the target's armor, lowering their DEF by 80 and AMR by 2 for 90s.";
$Spell::message[26] = "Softening %s";
$Spell::type[26] = $SpellTypeCantrip;
$Spell::baseStamina[26] = 35;
$Spell::minStamina[26] = 5;
$Spell::staminaFalloffFactor[26] = 5;
$Spell::delay[26] = 2.0;
$Spell::recoveryTime[26] = 3;
$Spell::castType[26] = $SpellCastTypeTarget;
$Spell::LOSrange[26] = 80;
$Spell::effectVars[26] = "BONUS DEF -80 45,BONUS ARM -2 45";
$Spell::startSound[26] = ActivateTR;
$Spell::endSound[26] = ActivateTD;
$Spell::groupListCheck[26] = false;
$Spell::refVal[26] = -12;
$Spell::canMove[26] = false;
$Spell::graceDistance[26] = 2;
$Spell::useSkillOnCast[26] = true;
$SkillType[soften] = $SkillNatureCasting;
$SkillRestriction[soften] = $SkillNatureCasting @ " 30";

$Spell::keyword[27] = "haste";
$Spell::index[haste] = 27;
$Spell::name[27] = "Enchant Haste";
$Spell::description[27] = "Increases movement speed for 240s. Cannot fly while hasted, but stacks with CheetaursPaws. Use #unhaste to stop early.";
$Spell::type[27] = $SpellTypeMagic;
$Spell::baseStamina[27] = 25;
$Spell::manaCost[27] = 25;
$Spell::delay[27] = 4;
$Spell::recoveryTime[27] = 15;
$Spell::castType[27] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[27] = true; //Needed to be able to access los::object on scripted spells
$Spell::LOSrange[27] = 80;
$Spell::ticks[27] = 120;
$Spell::startSound[27] = ActivateTR;
$Spell::endSound[27] = UnravelAM;
$Spell::groupListCheck[27] = False;
$Spell::teamTargetOnly[27] = true;
$Spell::aiRefVal[27] = 0;
$Spell::useSkillOnCast[27] = true;
$Spell::canMove[27] = true;
$SkillType[haste] = $SkillNatureCasting;
$SkillRestriction[haste] = $SkillNatureCasting @ " 50";

$Spell::keyword[28] = "repent";
$Spell::index[repent] = 28;
$Spell::name[28] = "Repent";
$Spell::description[28] = "Casts a holy explosion. Spell tracks the target.";
$Spell::type[28] = $SpellTypeMagic;
$Spell::baseStamina[28] = 15;
$Spell::manaCost[28] = 10;
$Spell::delay[28] = 1.5;
$Spell::recoveryTime[28] = 4.65;
$Spell::castType[28] = $SpellCastTypeBomb;
$Spell::bombData[28] = Bomb2;
$Spell::radius[28] = 15;
$Spell::damageValue[28] = 100;
$Spell::trackTarget[28] = true;
$Spell::LOSrange[28] = 80;
$Spell::startSound[28] = DeActivateWA;
$Spell::endSound[28] = Reflected;
$Spell::groupListCheck[28] = False;
$Spell::aiRefVal[28] = 30;
$Spell::useSkillOnCast[28] = false;
$Spell::canMove[28] = false;
$Spell::graceDistance[28] = 2;
$SkillType[repent] = $SkillDefensiveCasting;
$SkillRestriction[repent] = $SkillDefensiveCasting @ " 50";

$Spell::keyword[29] = "smite";
$Spell::index[smite] = 29;
$Spell::name[29] = "Smite";
$Spell::description[29] = "Casts a large holy explosion. Spell tracks the target.";
$Spell::type[29] = $SpellTypeMagic;
$Spell::baseStamina[29] = 15;
$Spell::manaCost[29] = 30;
$Spell::delay[29] = 2.625;
$Spell::recoveryTime[29] = 10;
$Spell::castType[29] = $SpellCastTypeBomb;
$Spell::bombData[29] = Bomb1;
$Spell::radius[29] = 25;
$Spell::damageValue[29] = 275;
$Spell::LOSrange[29] = 80;
$Spell::startSound[29] = SpellCastSnd;
$Spell::endSound[29] = holysmite;
$Spell::groupListCheck[29] = False;
$Spell::aiRefVal[29] = 45;
$Spell::useSkillOnCast[29] = false;
$Spell::canMove[29] = false;
$Spell::graceDistance[29] = 2;
$SkillType[smite] = $SkillDefensiveCasting;
$SkillRestriction[smite] = $SkillDefensiveCasting @ " 300";

$Spell::keyword[30] = "wrath";
$Spell::index[wrath] = 30;
$Spell::name[30] = "Enchant Wrath";
$Spell::message[15] = "Casting Wrath on %s";
$Spell::description[30] = "Boost you or your target's ATK by +15 for 2 minutes";
$Spell::type[30] = $SpellTypeMagic;
$Spell::baseStamina[30] = 15;
$Spell::manaCost[30] = 20;
$Spell::delay[30] = 2.625;
$Spell::recoveryTime[30] = 5;
$Spell::castType[30] = $SpellCastTypeSelfOrLOS;
$Spell::LOSrange[30] = 80;
$Spell::effectVars[30] = "BONUS ATK 15 60";//2 minutes
$Spell::startSound[30] = DeActivateWA;
$Spell::endSound[30] = ActivateTD;
$Spell::groupListCheck[30] = False;
$Spell::aiRefVal[30] = 45;
$Spell::useSkillOnCast[30] = true;
$Spell::canMove[30] = false;
$Spell::graceDistance[30] = 2;
$SkillType[wrath] = $SkillDefensiveCasting;
$SkillRestriction[wrath] = $SkillDefensiveCasting @ " 140";

$Spell::keyword[31] = "breeze";
$Spell::index[breeze] = 31;
$Spell::name[31] = "Healing Breeze";
$Spell::description[31] = "Lightly heal yourself and all allies around you for 15 HP.";
$Spell::damageValue[31] = -15;
$Spell::type[31] = $SpellTypeMagic;
$Spell::baseStamina[31] = 15;
$Spell::manaCost[31] = 20;
$Spell::delay[31] = 1.5;
$Spell::recoveryTime[31] = 4.25;
$Spell::castType[31] = $SpellCastTypeScripted;
$Spell::radius[31] = 25;
$Spell::startSound[31] = DeActivateWA;
$Spell::endSound[31] = ActivateAR;
$Spell::groupListCheck[31] = False;
$Spell::aiRefVal[31] = 45;
$Spell::useSkillOnCast[31] = true;
$Spell::canMove[31] = false;
$Spell::graceDistance[31] = 2;
$SkillType[breeze] = $SkillNatureCasting;
$SkillRestriction[breeze] = $SkillNatureCasting @ " 120";

$Spell::keyword[32] = "ironfist";
$Spell::index[ironfist] = 32;
$Spell::name[32] = "Ironfist";
$Spell::description[32] = "Casts ironfist. Lowers opponent DEF by 80 and ATK by 10 for 60 seconds";
$Spell::damageValue[32] = 128;
$Spell::type[32] = $SpellTypeMagic;
$Spell::baseStamina[32] = 5;
$Spell::manaCost[32] = 5;
$Spell::castType[32] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[32] = true;
$Spell::LOSrange[32] = 80;
$Spell::delay[32] = 0.1;
$Spell::recoveryTime[32] = 13.5;
$Spell::radius[32] = 7;
$Spell::effectVars[32] = "BONUS DEF -80 30,BONUS ATK -10 30";
$Spell::startSound[32] = UnravelAM;
$Spell::endSound[32] = NoSound;
$Spell::groupListCheck[32] = False;
$Spell::aiRefVal[32] = 128;
$Spell::canMove[32] = true;
$SkillType[ironfist] = $SkillOffensiveCasting;
$SkillRestriction[ironfist] = $SkillOffensiveCasting @ " 110";

$Spell::keyword[33] = "icestorm";
$Spell::index[icestorm] = 33;
$Spell::name[33] = "Icestorm";
$Spell::description[33] = "Fire a volley of ice blasts.";
$Spell::damageValue[33] = 45;
$Spell::type[33] = $SpellTypeMagic;
$Spell::baseStamina[33] = 15;
$Spell::manaCost[33] = 6;
$Spell::castType[33] = $SpellCastTypeScripted;
$Spell::delay[33] = 1;
$Spell::recoveryTime[33] = 2.25;
$Spell::startSound[33] = ImpactTR;
$Spell::endSound[33] = Reflected;
$Spell::groupListCheck[33] = False;
$Spell::aiRefVal[33] = 45;
$Spell::canMove[33] = false;
$Spell::graceDistance[33] = 2;
$SkillType[icestorm] = $SkillNatureCasting;
$SkillRestriction[icestorm] = $SkillNatureCasting @ " 85";

$Spell::keyword[34] = "soften2";
$Spell::index[soften2] = 34;
$Spell::name[34] = "Soften 2";
$Spell::description[34] = "Softens the target's armor, lowering their DEF by 150 and AMR by 3 for 130s.";
$Spell::message[34] = "Softening %s";
$Spell::type[34] = $SpellTypeMagic;
$Spell::baseStamina[34] = 15;
$Spell::manaCost[34] = 10;
$Spell::delay[34] = 2.0;
$Spell::recoveryTime[34] = 4.25;
$Spell::castType[34] = $SpellCastTypeTarget;
$Spell::LOSrange[34] = 80;
$Spell::effectVars[34] = "BONUS DEF -150 65,BONUS ARM -3 65";
$Spell::startSound[34] = ActivateTR;
$Spell::endSound[34] = ActivateTD;
$Spell::groupListCheck[34] = false;
$Spell::refVal[34] = -12;
$Spell::canMove[34] = false;
$Spell::graceDistance[34] = 2;
$Spell::useSkillOnCast[34] = true;
$SkillType[soften2] = $SkillNatureCasting;
$SkillRestriction[soften2] = $SkillNatureCasting @ " 170";

$Spell::keyword[35] = "dimensionrift";
$Spell::index[dimensionrift] = 35;
$Spell::name[35] = "Dimension Rift";
$Spell::description[35] = "Casts Dimension Rift.";
$Spell::delay[35] = 9.5;
$Spell::recoveryTime[35] = 11.25;
$Spell::damageValue[35] = 320;
$Spell::type[35] = $SpellTypeMagic;
$Spell::baseStamina[35] = 20;
$Spell::manaCost[35] = 40;
$Spell::castType[35] = $SpellCastTypeScripted;
$Spell::auraEffect[35] = SpellEffectAura1;
$Spell::startSound[35] = LaunchLS;
$Spell::endSound[35] = Explode3FW;
$Spell::groupListCheck[35] = False;
$Spell::aiRefVal[35] = 320;
$Spell::canMove[35] = false;
$Spell::graceDistance[35] = 2;
$SkillType[dimensionrift] = $SkillOffensiveCasting;
$SkillRestriction[dimensionrift] = $SkillOffensiveCasting @ " 750";

$Spell::keyword[36] = "beam";
$Spell::index[beam] = 36;
$Spell::name[36] = "Beam";
$Spell::description[36] = "Light gathers into a concentrated beam and causes intense damage to the target.";
$Spell::delay[36] = 0.0;
$Spell::recoveryTime[36] = 15.0;
$Spell::damageValue[36] = 180;
$Spell::type[36] = $SpellTypeMagic;
$Spell::baseStamina[36] = 2;
$Spell::manaCost[36] = 30;
$Spell::castType[36] = $SpellCastTypeScripted;
$Spell::forceLOSCheck[36] = true;
$Spell::LOSrange[36] = 1000;
$Spell::startSound[36] = HitLevelDT;
$Spell::endSound[36] = HitBF;
$Spell::groupListCheck[36] = False;
$Spell::aiRefVal[36] = 180;
$Spell::canMove[36] = true;
$SkillType[beam] = $SkillOffensiveCasting;
$SkillRestriction[beam] = $SkillOffensiveCasting @ " 520";

//$Spell::keyword[1] = "firebomb";
//$Spell::index[firebomb] = 1;
//$Spell::name[1] = "Fire Bomb From Hell";
//$Spell::description[1] = "Casts an explosive.";
//$Spell::delay[1] = 1.5;
//$Spell::recoveryTime[1] = 2.625;
//$Spell::radius[1] = 10;
//$Spell::damageValue[1] = "55";
//$Spell::LOSrange[1] = 80;
//$Spell::manaCost[1] = 5;
//$Spell::auraEffect[1] = SpellEffectAura2;
//$Spell::startSound[1] = ActivateBF;
//$Spell::endSound[1] = NoSound;
//$Spell::endSoundLoc[1] = $SpellEndSoundLocationPlayer;
//$Spell::groupListCheck[1] = False;
//$Spell::refVal[1] = 55;
//$Spell::graceDistance[1] = 2;
//$Spell::effectType[1] = $SpellTypeProjectile;
//$Spell::projectileData[1] = Firebomb;
//$SkillType[firebomb] = $SkillOffensiveCasting;

$Spell::keyword[37] = "fireball";
$Spell::index[fireball] = 37;
$Spell::name[37] = "Fireball";
$Spell::type[37] = $SpellTypeCantrip;
$Spell::spellImage[37,0] = InvisShape;
$Spell::delay[37] = 1;
$Spell::baseStamina[37] = 15;
$Spell::minStamina[37] = 5;
$Spell::staminaFalloffFactor[37] = 1.5;
$Spell::chargeTime[37] = 1.5;
$Spell::castType[37] = $SpellCastTypePlayerCharge;
$Spell::projectileData[37] = fireball;
$Spell::startSound[37] = ActivateTR;
$Spell::chargeSound[37] = ActivateBF;
$Spell::castSound[37] = LaunchFB;
$Spell::aiRefVal[37] = 0;
$SkillType[fireball] = $SkillOffensiveCasting;
$SkillRestriction[fireball] = $SkillOffensiveCasting @ " 20";

function NewSpell::BeginCastSpell(%clientId, %keyword)
{
    dbecho($dbechoMode, "BeginCastSpell(" @ %clientId @ ", " @ %keyword @ ")");

	%w1 = GetWord(%keyword, 0);
	%w2 = String::getSubStr(%keyword, String::len(%w1)+1, 99999);
    
    %i = $Spell::index[%w1];
    
    if(%i != "")
    {
        if(Spell::CheckSpellRequirements(%clientId,%i))
        {
            %type = $Spell::type[%i];
            
            
        }
        else
            Client::sendMessage(%clientId, $MsgWhite, $Spell::FailCastReason);
    }
    else
        Client::sendMessage(%clientId, $MsgWhite, "This spell seems unfamiliar to you.");
}

function Spell::BeginCastSpell(%clientId, %keyword)
{
	dbecho($dbechoMode, "BeginCastSpell(" @ %clientId @ ", " @ %keyword @ ")");

	%w1 = GetWord(%keyword, 0);
	%w2 = String::getSubStr(%keyword, String::len(%w1)+1, 99999);

	//for(%i = 1; $Spell::keyword[%i] != ""; %i++)
	//{
    %i = $Spell::index[%w1];
    
    if(%i != "")
    {
        if(Spell::CheckSpellRequirements(%clientId,%i))
		{
            %type = $Spell::type[%i];
            %stamina = $Spell::baseStamina[%i];
            if(%type == $SpellTypeCantrip)
            {
                %stamina = Spell::CalculateCantripStamina(%clientId,%i);
            }
            
            if(%i == $Spell::index[teleport])
            {
                if( !(String::ICompare(%w2, "town") == 0 || String::ICompare(%w2, "dungeon") == 0))
                {
                    Client::sendMessage(%clientId, $MsgWhite, "Please specify either \"town\" or \"dungeon\" (eg. #cast teleport town)");
                    return false;
                }
            }
            
            if(%i == $Spell::index[transport] || %i == $Spell::index[advtransport])
            {   
                %zz = GetZoneByKeywords(%clientId, %w2, 3);
                if(%w2 == "" || %zz == false)
                {
                    Client::sendMessage(%clientId, $MsgWhite, "Please specify a valid zone name. Can use partial names if they are specific enough.");
                    return false;
                }
                else
                    %extraText = " to "@ Zone::getDesc(%zz);
            }
            if(fetchData(%clientId, "Stamina") >= %stamina)
            {
                %mcost = $Spell::manaCost[%i];
                if(%mcost == "")
                    %mcost = 0;
                %mana = fetchData(%clientId, "MANA");
                if(%mana >= %mcost)
                {
                    %player = Client::getOwnedObject(%clientId);
                    %castType = $Spell::castType[%i];
                    %losSpell = (%castType == $SpellCastTypeBomb || %castType == $SpellCastTypeTarget || %castType == $SpellCastTypeSelfOrLOS || $Spell::forceLOSCheck[%i]);
                    if(%losSpell && GameBase::getLOSinfo(%player, $Spell::LOSrange[%i]))
                    {
                        %lospos = $los::position;
                        %losobj = $los::object;
                        %norm = $los::normal;
                        
                        if(getObjectType(%losobj) == "Player")
                        {
                            %bombType = %castType == $SpellCastTypeBomb;
                            if(!%bombType || (%bombType && $Spell::trackTarget[%i]))
                            {
                                %cl = Player::getClient(%losobj);
                                if($Spell::teamTargetOnly[%i])
                                {
                                    if(Gamebase::getTeam(%castObj) == Gamebase::getTeam(%clientId))
                                        %extraText = %extraText @" on "@ Client::getName(Player::getClient(%losobj));
                                }
                                else
                                    %extraText = %extraText @" on "@ Client::getName(Player::getClient(%losobj));
                            }
                        }
                    }
                    else
                    {
                        if(%i == $Spell::index[translocate] || %i == $Spell::index[advtranslocate] || %i == $Spell::index[fasttranslocate])
                        {
                            Client::sendMessage(%clientId, $MsgWhite, "Destination is too far to translocate to.");
                            return false;
                        }
                        %lospos = "";
                        %losobj = 0;
                        %norm = "";
                    }
                    
                    %msg =  "Casting " @ $Spell::name[%i];
                    if(%extraText != "")
                        %msg = %msg @ %extraText;
                    Client::sendMessage(%clientId, $MsgBeige,%msg @".");
                    
                    storeData(%clientId, "SpellCastStep", 1);
                    
                    %tempStamCost = floor(%stamina / 2);
                    //refreshStamina(%clientId, %tempStamCost);
                    if(%mcost > 0)
                    {
                        if(%mcost % 2 == 0)
                            refreshMana(%clientId,%mcost/2);
                        else
                            refreshMana(%clientId,floor(%mcost/2));
                    }
                    playSound($Spell::startSound[%i], GameBase::getPosition(%clientId));

                    %skt = $SkillType[$Spell::keyword[%i]];
                    %sk1 = CalculatePlayerSkill(%clientId, %skt);
                    %gsa = GetSkillAmount($Spell::keyword[%i], %skt);
                    %sk2 = %sk1 - %gsa;
                    %sk = Cap(%sk2, 0, "inf");
                    %rt = $Spell::recoveryTime[%i];
                    %a = %rt / 2;
                    %b = (1000 - %sk) / 1000;
                    %c = %b * %a;
                    %recovTime = $Spell::delay[%i] + Cap(%a + %c, %a, %rt);	//recovery time is never smaller than half of the original and never bigger than the original.
                    
                    if($Spell::auraEffect[%i] != "")
                        Player::mountItem(%player,$Spell::auraEffect[%i],$SpellAuraSlot);
                    
                    if(%clientId.isAtRest == 1)
                    {
                        %clientId.isAtRest = 0;
                        refreshStaminaREGEN(%clientId);
                    }
                    %clientId.isAtRestCounter = 0;

                    
                    if($Spell::castType[%i] == $SpellCastTypePlayerCharge)
                    {
                        Spell::StartPlayerChargeCast(%clientId,%i);
                    }
                    else
                    {
                        schedule("%retval=Spell::DoCastSpell(" @ %clientId @ ", " @ %i @ ", \"" @ GameBase::getPosition(%clientId) @ "\", \"" @ %lospos @ "\", \"" @ %losobj @ "\", \"" @ %w2 @ "\", \"" @%norm@ "\"); if(%retval){refreshStamina(" @ %clientId @ ", " @ %tempStamCost @ ");}", $Spell::delay[%i]);
                        schedule("storeData(" @ %clientId @ ", \"SpellCastStep\", \"\");sendDoneRecovMsg(" @ %clientId @ ");", %recovTime);
                    }
                    return true;
                }
                else
                    Client::sendMessage(%clientId, $MsgWhite, "Insufficient mana to cast this spell. ("@ %mana @"/"@ %mcost @")");
            }
            else
				Client::sendMessage(%clientId, $MsgWhite, "Insufficient stamina to cast this spell. ("@ fetchData(%clientId, "Stamina") @"/"@ %stamina @")");
        }
        else
            Client::sendMessage(%clientId, $MsgWhite, $Spell::FailCastReason);
    }
    else
        Client::sendMessage(%clientId, $MsgWhite, "This spell seems unfamiliar to you.");
        
    return false;
}

function Spell::StartPlayerChargeCast(%clientId,%i)
{
    %weap = fetchData(%clientId,"EquippedWeapon");
    if(%weap != "")
    {
        RPGItem::unequipItem(%clientId,%weap,false);
    }
    storeData(%clientId,"SpellCastStep","");
    Player::unmountItem(%clientId,$BaseWeaponSlot);
    Player::mountItem(%clientId,ChargeMagicItem,$BaseWeaponSlot);
    storeData(%clientId,"EquippedSpell",%i);
}

function Spell::CastChargedMagic(%clientId,%index,%timeDiff)
{
    
    %chargeTime = $Spell::chargeTime[%index];
    if(%timeDiff >= %chargeTime)
    {
        %casterPos = Gamebase::getPosition(%clientId);
        
        if($Spell::manaCost[%index] > 0)
        {
            %mcost = $Spell::manaCost[%index];
            if(fetchData(%clientId,"MANA") >= %mcost)
                refreshMANA(%clientId, %mcost);
            else
            {
                Client::sendMessage(%clientId, $MsgRed, "You lost too much mana before you could finish the spell.~wUnravelAM.wav");
                return false;
            }
        }
        %stamina = Spell::CalculateCantripStamina(%clientId,%index,1);
        echo(%stamina);
        if(fetchData(%clientId, "Stamina") >= %stamina)
        {
            //refreshStamina(%clientId,%stamina);
        }
        else
        {
            Client::sendMessage(%clientId, $MsgRed, "You lost too much stamina before you could finish the spell.~wUnravelAM.wav");
            return false;
        }
        %player = Client::getOwnedObject(%clientId);
        if(%index == 37)
        {
            %trans = Gamebase::getMuzzleTransform(%player);
            %vel = Item::getVelocity(%player);
            Projectile::spawnProjectile(Fireball,%trans,%player,%vel);
        }
        
        if($Spell::castSound[%index] != "")
            playSound($Spell::castSound[%index],%casterPos);
    }
}

function Spell::CalculateCantripStamina(%clientId,%i,%mult)
{
    if(Player::isAiControlled(%clientId))
    {
        return 0;
    }
    if(%mult == "")
        %mult = 1;
        
    %kw = $Spell::keyword[%i];
    %skillType = $SkillType[%kw];
    %skill = CalculatePlayerSkill(%clientId, %skillType);
    
    %minSkill = 1;
    %wx = Word::FindWord($SkillRestriction[%kw],%skillType);
    if(%wx != -1)
        %minSkill = Cap(getWord($SkillRestriction[%kw],%wx+1),1,"inf");
        
    %base = $Spell::baseStamina[%i];
    %weightFactor = $Spell::minStamina[%i]-%base;
    %growthFactor = $Spell::staminaFalloffFactor[%i]-1;
    
    %m = %weightFactor/(%minSkill*%growthFactor);
    %b = %base-%weightFactor/%growthFactor;
    %stamCost = Cap(%m*%skill+%b,$Spell::minStamina[%i],%base);
    %stamCost *= %mult;
    
    return %stamCost;
}

function Spell::DoCastSpell(%clientId, %index, %oldpos, %castPos, %castObj, %w2, %castPosNorm)
{
    dbecho($dbechoMode, "Spell::DoCastSpell(" @ %clientId @ ", " @ %index @ ", " @ %oldpos @ ", " @ %castPos @ ", " @ %castObj @ ", " @ %w2 @ ")");
    echo("Spell::DoCastSpell(" @ %clientId @ ", " @ %index @ ", " @ %oldpos @ ", " @ %castPos @ ", " @ %castObj @ ", " @ %w2 @ ")");
    %player = Client::getOwnedObject(%clientId);
    %casterPos = GameBase::getPosition(%clientId);
    if($Spell::auraEffect[%index] != "")
        Player::unmountItem(%player,$SpellAuraSlot);
    
    
    if(!$Spell::canMove[%index] && Vector::getDistance(%oldpos,%casterPos) > $Spell::graceDistance[%index])
    {
        Client::sendMessage(%clientId, $MsgBeige, "Your casting was interrupted.");
		storeData(%clientId, "SpellCastStep", 2);
		return False;
    }
    
    if($Spell::teamTargetOnly[%index])
    {
        if(Gamebase::getTeam(%castObj) != Gamebase::getTeam(%clientId))
        {
            %castObj = 0; //Cannot target enemies
        }
    }
    
    if($Spell::groupListCheck[%index])
	{
        %cl = Player::getClient(%castObj);
        if( !(IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%cl)) && IsInCommaList(fetchData(%cl, "grouplist"), Client::getName(%clientId))) && %cl != %clientId && %cl != -1)
		{
			Client::sendMessage(%clientId, $MsgBeige, "You are not part of the target's group.");
			storeData(%clientId, "SpellCastStep", 2);

			return false;
		}
    }
    
    if($Spell::trackTarget[%index])
        %castPos = Gamebase::getPosition(%castObj);
    
    //WIP
    //if($Spell::targetAllies[%index])
    //{
    
    //}
    
    //Do mana cost...
    if($Spell::manaCost[%index] > 0)
    {
        %mcost = $Spell::manaCost[%index];
        if(%mcost % 2 == 0)
            %mcost = %mcost/2;
        else
            %mcost = ceil(%mcost/2); //Take the ceil here, to make up the odd half.
            
        if(fetchData(%clientId,"MANA") >= %mcost)
            refreshMANA(%clientId, %mcost);
        else
        {
            Client::sendMessage(%clientId, $MsgRed, "You lost too much mana before you could finish the spell.~wUnravelAM.wav");
            return false;
        }
    }
    
    %overrideEndSound = false;
    %castType = $Spell::castType[%index];
    if(%castType == "")
        %castType = $SpellCastTypeScripted;
        
    if(%castType == $SpellCastTypeProjectile)
    {
        %trans = GameBase::getEyeTransform(%clientId);
        %vel = Item::getVelocity(%clientId);
        %eyePos = Word::getSubWord(%trans,9,3);
        %dir = Word::getSubWord(%trans,3,3);
        
        // This gets around close range collision issues
        %offsetProj = Vector::add(%eyePos,ScaleVector(%dir,1));
        
        Projectile::spawnProjectile($Spell::projectileData[%index],Word::getSubWord(%trans,0,9) @" "@ %offsetProj,%player,%vel);
        
        if($Spell::castSound[%index] != "")
            playSound($Spell::castSound[%index],%casterPos);
        %overrideEndSound = $Spell::overrideEndSound[%index];
		%returnFlag = true;
    }
    else if(%castType == $SpellCastTypeSelf)
    {
        echo("Self cast");
        Spell::ApplyEffectVars(%clientId,%clientId,%index);
        %castPos = GameBase::getPosition(%clientId);
        %overrideEndSound = $Spell::overrideEndSound[%index];
		%returnFlag = true;
    }
    else if(%castType == $SpellCastTypeTarget)
    {
        if(getObjectType(%castObj) == "Player")
        {
            %tgtCl = Player::getClient(%castObj);
            %msg = $Spell::message[%index];
            if(%msg != "")
            {
                if(Word::FindWord(%msg,"%s") != -1)
                {
                    %msg = String::replace(%msg,"%s",Client::getName(%tgtCl));
                }
                Client::sendMessage(%clientId, $MsgBeige, %msg);
            }
            Client::sendMessage(%tgtCl, $MsgBeige, Client::getName(%clientId) @ " is casting " @ $Spell::name[%index] @ " on you.");
            
            Spell::ApplyEffectVars(%clientId,Player::getClient(%castObj),%index);
            %overrideEndSound = $Spell::overrideEndSound[%index];
            %returnFlag = true;
        }
        else
        {
            Client::sendMessage(%clientId, $MsgRed,"Could not find a target.");
            %overrideEndSound = false;
            %returnFlag = false;
        }
    }
    else if(%castType == $SpellCastTypeBomb)
    {
        if(%castPos != "")
		{
			CreateAndDetBomb(%clientId, $Spell::BombData[%index], %castPos, true, %index);

			%overrideEndSound = True;
			%returnFlag = True;
		}
		else
		{
			Client::sendMessage(%clientId, $MsgBeige, "Could not find a target.");

			%returnFlag = False;
		}
    }
    else if(%castType == $SpellCastTypeSelfOrLOS)
    {
        %objtype = getObjectType(%castObj);
        if(%castObj == 0 || %objtype != "Player")
        {
            Spell::ApplyEffectVars(%clientId,%clientId,%index);
            %castPos = GameBase::getPosition(%clientId);
            %overrideEndSound = $Spell::overrideEndSound[%index];
            Client::sendMessage(%clientId, $MsgBeige, "Received "@ $Spell::name[%index]);
            %returnFlag = true;
        }
        else if(getObjectType(%castObj) == "Player")
        {
            %tgtCl = Player::getClient(%castObj);
            %msg = $Spell::message[%index];
            if(%msg != "")
            {
                if(Word::FindWord(%msg,"%s") != -1)
                {
                    %msg = String::replace(%msg,"%s",Client::getName(%tgtCl));
                }
                Client::sendMessage(%clientId, $MsgBeige, %msg);
            }
            Client::sendMessage(%tgtCl, $MsgBeige, Client::getName(%clientId) @ " is casting " @ $Spell::name[%index] @ " on you.");
            Spell::ApplyEffectVars(%clientId,%tgtCl,%index);
            %overrideEndSound = $Spell::overrideEndSound[%index];
            %returnFlag = true;
        }
    }
    else if(%castType == $SpellCastTypeScripted)
    {
        if(%index == $Spell::index[fireblast])
        {
            %trans = GameBase::getEyeTransform(%clientId);
            %vel = Item::getVelocity(%player);
            %eyePos = Word::getSubWord(%trans,9,3);
            %dir = Word::getSubWord(%trans,3,3);
            
            // This gets around close range collision issues
            %offsetProj = Vector::add(%eyePos,ScaleVector(%dir,0.3));
            %tt = Word::getSubWord(%trans,0,9) @" "@ %offsetProj;
            Projectile::spawnProjectile(Fireblast,%tt,%player,%vel);
            schedule("Projectile::spawnProjectile(FireblastInvis,\""@%tt@"\","@%player@",\""@%vel@"\");",0.1);
            
            if($Spell::castSound[%index] != "")
                playSound($Spell::castSound[%index],%casterPos);
            %overrideEndSound = true;
            %returnFlag = true;
        }
        else if(%index == $Spell::index[teleport])
        {
            //teleport zone spell

            %zoneId = GetNearestZone(%clientId, %w2, 3);
            
            if(%zoneId != False)
            {
                echo(%zoneId," "@Zone::getDesc(%zoneId));
                Client::sendMessage(%clientId, $MsgBeige, "Teleporting near " @ Zone::getDesc(%zoneId));

                //teleport

                %mpos = Zone::getMarker(%zoneId);
                if(!fetchData(%clientId, "invisible"))
                    GameBase::startFadeIn(%clientId);

                GameBase::setPosition(%clientId, %mpos);
                CheckAndBootFromArena(%clientId);
                NullItemList(%clientId, Lore, $MsgRed, "You lost all %1s you were carrying when you teleported.");

                Player::setDamageFlash(%clientId, 0.7);
                %extraDelay = 0.22;	//sometimes the endSound doesn't get played unless there is sufficient delay

                %castPos = SetOnGround(%clientId, 500);
                //%castPos = %newpos;

                %returnFlag = True;
            }
            else
            {
                Client::sendMessage(%clientId, $MsgBeige, "Teleportation failed.");
                %returnFlag = false;
            }
        }
        else if(%index == 18)
        {
            if(%castObj == 0)
            {
                Spell::ApplyEffectVars(%clientId,%clientId,%index);
                Player::mountItem(%clientId,SpellEffectAura3,$SpellAuraSlot+3);
                %castPos = GameBase::getPosition(%clientId);
                %overrideEndSound = $Spell::overrideEndSound[%index];
                %returnFlag = true;
            }
            else if(getObjectType(%castObj) == "Player")
            {
                %tgtCl = Player::getClient(%castObj);
                %msg = $Spell::message[%index];
                if(%msg != "")
                {
                    if(Word::FindWord(%msg,"%s") != -1)
                    {
                        %msg = String::replace(%msg,"%s",Client::getName(%tgtCl));
                    }
                    Client::sendMessage(%clientId, $MsgBeige, %msg);
                }
                Client::sendMessage(%id, $MsgBeige, Client::getName(%clientId) @ " is casting " @ $Spell::name[%index] @ " on you.");
                Spell::ApplyEffectVars(%clientId,%tgtCl,%index);
                Player::mountItem(%tgtCl,SpellEffectAura3,$SpellAuraSlot+3);
                %overrideEndSound = $Spell::overrideEndSound[%index];
                %returnFlag = true;
            }
        }
        else if(%index == $Spell::index[transport])
        {
            //Transport zone spell

            %zoneId = GetZoneByKeywords(%clientId, %w2, 3);

            if(%zoneId != false)
            {
                Client::sendMessage(%clientId, $MsgBeige, "Transporting to " @ Zone::getDesc(%zoneId));

                //teleport

                %system = Object::getName(%zoneId);
                %type = GetWord(%system, 0);
                %desc = String::getSubStr(%system, String::len(%type)+1, 9999);

                %castPos = TeleportToMarker(%clientId, "Realm0\\Zones\\" @ %system @ "\\DropPoints", False, True);
                CheckAndBootFromArena(%clientId);
                NullItemList(%clientId, Lore, $MsgRed, "You lost all %1s you were carrying when you teleported.");

                if(!fetchData(%clientId, "invisible"))
                    GameBase::startFadeIn(%clientId);
                
                Player::setDamageFlash(%clientId, 0.7);
                %extraDelay = 0.22;	//sometimes the endSound doesn't get played unless there is sufficient delay

                %returnFlag = True;
            }
            else
            {
                Client::sendMessage(%clientId, $MsgBeige, "Transportation failed.");
                %returnFlag = False;
            }
        }
        else if(%index == $Spell::index[advtransport])
        {
            //Advanced Transport zone spell

            %zoneId = GetZoneByKeywords(%clientId, %w2, 3);

            if(%zoneId != False)
            {
                if(getObjectType(%castObj) == "Player")
                    %id = Player::getClient(%castObj);
                else
                    %id = %clientId;

                Client::sendMessage(%clientId, $MsgBeige, "Transporting to " @ Zone::getDesc(%zoneId));
                if(%clientId != %id)
                    Client::sendMessage(%id, $MsgBeige, "You are being transported to " @ Zone::getDesc(%zoneId));

                //teleport

                %system = Object::getName(%zoneId);
                %type = GetWord(%system, 0);
                %desc = String::getSubStr(%system, String::len(%type)+1, 9999);
                
                %castPos = TeleportToMarker(%id, "Realm0\\Zones\\" @ %system @ "\\DropPoints", False, True);
                CheckAndBootFromArena(%id);
                NullItemList(%clientId, Lore, $MsgRed, "You lost all %1s you were carrying when you teleported.");

                if(!fetchData(%id, "invisible"))
                    GameBase::startFadeIn(%id);

                Player::setDamageFlash(%id, 0.7);
                %extraDelay = 0.22;	//sometimes the endSound doesn't get played unless there is sufficient delay

                %returnFlag = True;
            }
            else
            {
                Client::sendMessage(%clientId, $MsgBeige, "Transportation failed.");
                %returnFlag = False;
            }
        }
        else if(%index == $Spell::index[translocate] || %index == $Spell::index[fasttranslocate] || %index == $Spell::index[advtranslocate])
        {
            if(getObjectType(%castObj) == "Player")
            {
                %cl = Player::getClient(%castObj);
                if(Zone::getType(fetchData(%cl,"zone")) != "PROTECTED")
                {
                    %dest = Gamebase::getPosition(%castObj);
                    //%casterPos
                    playSound($Spell::endSound[%index],%casterPos);
                    Gamebase::setPosition(%castObj,%casterPos);
                    Gamebase::setPosition(%clientId,%castPos);
                    Item::setVelocity(%castObj,"0 0 0");
                    Item::setVelocity(%player,"0 0 0");
                    
                    
                    Client::sendMessage(%clientId,$MsgWhite,"You swapped places with "@Client::getName(%cl)@".");
                    Client::sendMessage(%clientId,$MsgWhite,Client::getName(%clientId)@" swapped places with you.");
                    %returnFlag = True;
                }
                else
                {
                    Client::sendMessage(%clientId,$MsgWhite,Client::getName(%cl)@" is in a PROTECTED zone.");
                    refreshMana(%clientId,$Spell::manaCost[%index]); //Refund used mana.
                    %returnFlag = false;
                }
            }
            else if(%castPos != "")
            {
            
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
                Client::sendMessage(%clientId, $MsgBeige, "Teleported to location.");
                Gamebase::setPosition(%clientId,%dest);
                %returnFlag = True;
            }
            else
            {
                //Normally we should not hit this condition
                Client::sendMessage(%clientId, $MsgBeige, "Invalid teleport location.");
                %returnFlag = false;
            }
        }
        else if(%index == $Spell::index[haste])
        {
            if(getObjectType(%castObj) == "Player" && !Player::isAiControlled(%clientId))
            {
                %id = Player::getClient(%castObj);
                Client::sendMessage(%clientId, $MsgBeige, "Enchanting " @ Client::getName(%id));
                Client::sendMessage(%id, $MsgBeige, Client::getName(%clientId) @ " is casting " @ $Spell::name[%index] @ " on you.");
            }
            else
            {
                %id = %clientId;
                %castPos = %casterPos;
            }
            if(AddBonusStatePoints(%id, "HasteCD") == 0)
            {
                UpdateBonusState(%id, "SPD 1", $Spell::ticks[%index]);
                UpdateBonusState(%id, "HasteCD 1", $Spell::ticks[%index] + 90);
                refreshAll(%id,false);
            }
            else
            {
                %ticks = GetBonusStateTicks(%id,"HasteCD 1");
                Client::sendMessage(%id, $MsgRed, "You are too exhausted to be hasted. ("@ %ticks*2 @"s)");
                %overrideEndSound = true;
                %returnFlag = false;
            }
            
            %overrideEndSound = false;
            %returnFlag = true;
        }
        else if(%index == $Spell::index[breeze])
        {
            %b = $Spell::radius[%index] * 2;
            %set = newObject("set", SimSet);
            %n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %b, %b, %b, 0);

            Group::iterateRecursive(%set, DoBoxFunction, %clientId, %index, %w2);
            deleteObject(%set);

            %overrideEndSound = True;

            %returnFlag = True;
        }
        else if(%index == $Spell::index[ironfist])
        {
            if(%castPos != "")
            {
                %minrad = 0;
                %maxrad = $Spell::radius[%index];
                for(%i = 0; %i <= 8; %i++)
                {
                    %tempPos = RandomPositionXY(%minrad, %maxrad);

                    %xPos = GetWord(%tempPos, 0) + GetWord(%castPos, 0);
                    %yPos = GetWord(%tempPos, 1) + GetWord(%castPos, 1);
                    %zPos = GetWord(%castPos, 2) + (%i / 3);
            
                    %newPos = %xPos @ " " @ %yPos @ " " @ %zPos;

                    schedule("CreateAndDetBomb(" @ %clientId @ ", \"Bomb12\", \"" @ %newPos @ "\", False, " @ %index @ ");", %i / 24, %player);
                }
                CreateAndDetBomb(%clientId, "Bomb12", %castPos, True, %index);

                %overrideEndSound = True;
                %returnFlag = True;
            }
            else
            {
                Client::sendMessage(%clientId, $MsgBeige, "Could not find a target.");
                %returnFlag = False;
            }
        }
        else if(%index == $Spell::index[icestorm])
        {
            %vel = Item::getVelocity(%player);
            for(%i = 0; %i <= 6; %i++)
            {
                schedule("%trans = GameBase::getEyeTransform(" @ %clientId @ "); Projectile::spawnProjectile(\"IceStorm\", %trans, \"" @ %player @ "\", \"" @ %vel @ "\");", %i / 7, %player);
            }

            %overrideEndSound = True;
            %returnFlag = True;
        }
        else if(%index == $Spell::index[beam])
        {
            if(getObjectType(%castObj) == "Player")
                %id = Player::getClient(%castObj);

            %trans = GameBase::getEyeTransform(%clientId);
            %p = Projectile::spawnProjectile("sniperLaser", %trans, %player, "0 0 0", 1.0);

            %mom1 = Vector::getFromRot( GameBase::getRotation(%clientId), -60, 1 );
            Player::applyImpulse(%clientId, %mom1);

            %r = $Spell::damageValue[%index];
        
            if(%id != "")
            {
                //%miss = CalcSpellMiss(%clientId, %id, %index);

                SpellDamage(%clientId, %id, %r, %index);
                %mom2 = Vector::getFromRot( GameBase::getRotation(%clientId), 50, 1 );
                Player::applyImpulse(%id, %mom2);
            }

            %castPos = GameBase::getPosition(%clientId);

            %returnFlag = True;
        }
        else if(%index == $Spell::index[dimensionrift])
        {
            %trans = GameBase::getEyeTransform(%clientId);
            %vel = Item::getVelocity(%player);
            Projectile::spawnProjectile("DimensionRift",%trans,%player,%vel);
            Projectile::spawnProjectile("DimensionRift2",%trans,%player,%vel);
            schedule("Projectile::spawnProjectile(\"DimensionRift3\",\""@%trans@"\",\""@%player@"\",\""@%vel@"\");",0.6,%player);
            schedule("Projectile::spawnProjectile(\"DimensionRift4\",\""@%trans@"\",\""@%player@"\",\""@%vel@"\");",1.2,%player);
            %overrideEndSound = True;
            %returnFlag = True;
        }
    }
    
    Player::setAnimation(%clientId, 39);
	if(!%overrideEndSound && $Spell::endSound[%index] != "")
	{
		if(%extraDelay == "")
			playSound($Spell::endSound[%index], %castPos);
		else
			schedule("playSound(" @ $Spell::endSound[%index] @ ", \"" @ %castPos @ "\");", %extraDelay);
	}
    
    //==================================================================

	%skilltype = $SkillType[$Spell::keyword[%index]];
    storeData(%clientId, "SpellCastStep", 2);
	if(%returnFlag)
	{
		if($Spell::useSkillOnCast[%index])
			UseSkill(%clientId, %skilltype, True, True);
		UseSkill(%clientId, $SkillEnergy, True, True);
	}
	else
	{
		UseSkill(%clientId, %skilltype, False, True);
		UseSkill(%clientId, $SkillEnergy, False, True);
	}
    
    return %returnFlag;
}

function Spell::ApplyEffectVars(%casterClient,%targetClient,%index)
{
    %effectVars = $Spell::effectVars[%index];
    if(%effectVars == "")
    {
        echo("ERROR: Spell has no effectVars");
        return;
    }
    
    for(%i = 0; (%w = String::getWord(%effectVars,",",%i) ) != ","; %i++)
    {
        %effect = getWord(%w,0);
        if(%effect == "HP")
        {
            %amnt = getWord(%w,1) / $TribesDamageToNumericDamage;
            refreshHP(%targetClient,-%amnt);
        }
        else if(%effect == "Stam")
        {
            %amnt = getWord(%w,1);
            refreshStamina(%targetClient,-%amnt);
        }
        else if(%effect == "BONUS")
        {
            %type = getWord(%w,1) @" "@getWord(%w,2);//Word::getSubWord(%w,1,2); //BonusName Amnt
            %ticks = getWord(%w,3);
            UpdateBonusState(%targetClient, %type, %ticks);
        }
    }
}

function Spell::CheckSpellRequirements(%clientId,%index)
{
    $Spell::FailCastReason = "";
    if(SkillCanUse(%clientId, $Spell::keyword[%index]))
    {
        //if($Spell::itemReq[%index] != "")
        //{
        //    //RPGItem::getItemCount(%clientId,%item)
        //}
        //else
        //    return true;
        return true;
    }
    else
    {
        $Spell::FailCastReason = "You can't cast this spell because you lack the necessary skills. ("@ WhatSkills($Spell::keyword[%index]) @")";
        return false;
    }
}

function TranslateEffectVars(%effectVars)
{
    %str = "";
    for(%i = 0; (%w = String::getWord(%effectVars,",",%i) ) != ","; %i++)
    {
        %effect = getWord(%w,0);
        if(%effect == "HP")
        {
            %amnt = getWord(%w,1);
            if(%amnt > 0)
                %amnt = "+"@%amnt;
            %str = %str @"HP "@ %amnt @",";
        }
        else if(%effect == "Stam")
        {
            %amnt = getWord(%w,1);
            if(%amnt > 0)
                %amnt = "+"@%amnt;
            %str = %str @"STAM "@ %amnt @",";
        }
        else if(%effect == "BONUS")
        {
            %type = getWord(%w,1);
            if($BonusStateNegative[%type])
                continue;
            //%type = getWord(%w,1) @" "@getWord(%w,2);//Word::getSubWord(%w,1,2); //BonusName Amnt
            %ticks = getWord(%w,3);
            
            %str = %str @getWord(%w,1)@": "@ getWord(%w,2) @" ("@2*%ticks@"s),";
        }
    }
    return %str;
}

function Spell::WhatIsSpell(%clientId,%keyword)
{
    %si = $Spell::index[%keyword];
	if(%si != "")
	{
		%desc = $Spell::name[%si];
        %reqs = WhatSkills(%keyword);
		%nfo = $Spell::description[%si];
        %type = $Spell::type[%si];
		%atkinfo = $Spell::damageValue[%si];
		%sd = $Spell::delay[%si];
		%sr = $Spell::recoveryTime[%si];
        
        if(%type == $SpellTypeCantrip)
        {
            %stam = Cap(Spell::CalculateCantripStamina(%clientId,%si),0,$Spell::baseStamina[%si]);
            %minStam = $Spell::minStamina[%si];
            %sm = $Spell::manaCost[%si];
        }
        else
        {
            %stam = $Spell::baseStamina[%si];
            %sm = $Spell::manaCost[%si];
        }
        %effects = TranslateEffectVars($Spell::effectVars[%si]);
	}
    %typeStr = "";
    if(%type == $SpellTypeCantrip)
        %typeStr = "Cantrip";
    else if(%type == $SpellTypeMagic)
        %typeStr = "Magic";
    %msg = "";
	%msg = %msg @ "<f1>" @ %desc @" - <f0>"@%typeStr@"<f1>\n";
    %msg = %msg @ "\nSkill Type: " @ $SkillDesc[$SkillType[%keyword]];
    if(%atkinfo != "")
        %msg = %msg @ "\nATK: " @ %atkinfo;
    %msg = %msg @ "\nRestrictions: " @ %reqs;
    if(%effects != "")
        %msg = %msg @ "\nBonuses: "@ %effects;
    %msg = %msg @ "\nDelay: " @ %sd @ " sec";
    %msg = %msg @ "\nRecovery: " @ %sr @ " sec";
    %msg = %msg @ "\nStamina: "@ %stam;
    if(%minStam && (%stam > %minStam))
        %msg = %msg @ "\nMinStam: "@ %minStam;
    if(%sm)
        %msg = %msg @ "\nMana: " @ %sm;
	%msg = %msg @ "\n\n<f0>" @ %nfo;
    return %msg;
}

//=========================
//Bomb spell functions
//=========================

function CreateAndDetBomb(%clientId, %b, %castPos, %doDamage, %index)
{
	dbecho($dbechoMode, "CreateAndDetBomb(" @ %clientId @ ", " @ %b @ ", " @ %castPos @ ", " @ %index @ ")");

	%player = Client::getOwnedObject(%clientId);

	%bomb = newObject("", "Mine", %b);

	addToSet("MissionCleanup", %bomb);

	GameBase::Throw(%bomb, %player, 0, false);
	GameBase::setPosition(%bomb, %castPos);
    
	if(%doDamage)
		SpellRadiusDamage(%clientId, %castPos, %index);

    if(%index != -1)
        playSound($Spell::endSound[%index], %castPos);
}

function SpellDamage(%clientId, %targetId, %damageValue, %index)
{
	dbecho($dbechoMode, "SpellDamage(" @ %clientId @ ", " @ %targetId @ ", " @ %damageValue @ ", " @ %index @ ")");

    if(%index == $Spell::index[ironfist])
    {
        Spell::ApplyEffectVars(%clientId,%targetId,%index);
    }
	GameBase::virtual(%targetId, "onDamage", $SpellDamageType, %damageValue, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, $Spell::keyword[%index]);
}

function SpellRadiusDamage(%clientId, %pos, %index)
{
	dbecho($dbechoMode, "SpellRadiusDamage(" @ %clientId @ ", " @ %pos @ ", " @ %index @ ")");

	%b = $Spell::radius[%index] * 2;
	%set = newObject("set", SimSet);
	%n = containerBoxFillSet(%set, $SimPlayerObjectType, %pos, %b, %b, %b, 0);

	Group::iterateRecursive(%set, DoSpellDamage, %clientId, %pos, %index);
	deleteObject(%set);
}
function DoSpellDamage(%object, %clientId, %pos, %index)
{
	dbecho($dbechoMode, "DoSpellDamage(" @ %object @ ", " @ %clientId @ ", " @ %pos @ ", " @ %index @ ")");

	%id = Player::getClient(%object);

	%percMin = 5;
	%percMax = 100;

	%dist = Vector::getDistance(%pos, GameBase::getPosition(%id));

	if(%dist <= $Spell::radius[%index])
	{
		%newDamage = SpellCalcRadiusDamage(%dist, $Spell::radius[%index], $Spell::damageValue[%index], %percMin, %percMax);
		SpellDamage(%clientId, %id, %newDamage, %index);
	}
}

function SpellCalcRadiusDamage(%dist, %radius, %dmg, %percMin, %percMax)
{
	dbecho($dbechoMode, "SpellCalcRadiusDamage(" @ %dist @ ", " @ %radius @ ", " @ %dmg @ ", " @ %percMin @ ", " @ %percMax @ ")");

	%newdmg = %dmg - (%dist * (%dmg / %radius));

	%p = (%newdmg * 100) / %dmg;

	if(%p < %percMin)
		%p = %percMin;
	else if(%p > %percMax)
		%p = %percMax;

	%newdmg = (%p * %dmg) / 100;

	return %newdmg;
}
//=========================
//END Bomb spell functions
//=========================

function DoBoxFunction(%object, %clientId, %index, %extra)
{
	dbecho($dbechoMode, "DoBoxFunction(" @ %object @ ", " @ %clientId @ ", " @ %index @ ", " @ %extra @ ")");
    %id = Player::getClient(%object);

	if(%index == $Spell::index[breeze])
	{
        if(GameBase::getTeam(%clientId) == GameBase::getTeam(%id))
		{
			Client::sendMessage(%clientId, $MsgBeige, "Healing Breeze on " @ Client::getName(%id));
			if(%clientId != %id)
				Client::sendMessage(%id, $MsgBeige, "You are being healed by " @ Client::getName(%clientId));

			%r = $Spell::damageValue[%index] / $TribesDamageToNumericDamage;
			refreshHP(%id, %r);

			%castPos = GameBase::getPosition(%id);

			CreateAndDetBomb(%clientId, "Bomb10", %castPos, False, %index);
			playSound($Spell::endSound[%index], %castPos);
		}
    }
}

function GetBestSpell(%clientId, %type, %semiRandomSpell)
{
	dbecho($dbechoMode, "GetBestSpell(" @ %clientId @ ", " @ %type @ ", " @ %semiRandomSpell @ ")");

	%wdelay = 10;	//weights
	%wrecov = 0.5;

	%bestSpell = -1;
	%backupSpell = "";
	%highest = 0.1;

	for(%i = 1; $Spell::keyword[%i] != ""; %i++)
	{
		if(SkillCanUse(%clientId, $Spell::keyword[%i]))
		{
			if(fetchData(%clientId, "MANA") >= $Spell::manaCost[%i])
			{
				%d = ( ($Spell::delay[%i] / %wdelay) + ($Spell::recoveryTime[%i] / %wrecov) );
				%x = (100 / %d) * $Spell::aiRefVal[%i];
				%v =  %x * %type;

				if(%semiRandomSpell)
				{
					%r = getRandom() * 100;
					%rr = getRandom() * 100;
				}
				else
				{
					%r = 1;
					%rr = 0;
				}

				if(%v > %highest)
				{
					if(%r > %rr)
					{
						%bestSpell = %i;
						%highest = %v;
					}
					else
						%backupSpell = %i;
				}
			}
		}
	}
	if(%bestSpell == -1 && %backupSpell != "")
		%bestSpell = %backupSpell;

	return %bestSpell;
}

function sendDoneRecovMsg(%clientId)
{
	//this function is here just to make the schedule command where this is called easier to read
	Client::sendMessage(%clientId, $MsgBeige, "You are ready to cast.");
}

//For remote functions - need revising
function SpellCanCast(%clientId, %keyword)
{
	dbecho($dbechoMode, "SpellCanCast(" @ %clientId @ ", " @ %keyword @ ")");

	for(%i = 1; $Spell::keyword[%i] != ""; %i++)
	{
		if(String::ICompare($Spell::keyword[%i], %keyword) == 0)
		{
			if(SkillCanUse(%clientId, $Spell::keyword[%i]))
			{
				if(fetchData(%clientId, "MaxMANA") >= $Spell::manaCost[%i])
					return True;
			}
		}
	}
	return False;
}
function SpellCanCastNow(%clientId, %keyword)
{
	dbecho($dbechoMode, "SpellCanCastNow(" @ %clientId @ ", " @ %keyword @ ")");

	for(%i = 1; $Spell::keyword[%i] != ""; %i++)
	{
		if(String::ICompare($Spell::keyword[%i], %keyword) == 0)
		{
			if(SkillCanUse(%clientId, $Spell::keyword[%i]))
			{
				if(fetchData(%clientId, "MANA") >= $Spell::manaCost[%i])
					return True;
			}
		}
	}
	return False;
}