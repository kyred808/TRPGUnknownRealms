//==================
// Health
//==================
function setHP(%clientId, %val)
{
	dbecho($dbechoMode, "setHP(" @ %clientId @ ", " @ %val @ ")");

	%armor = Player::getArmor(%clientId);

	if(%val < 0)
		%val = 0;
	if(%val == "")
		%val = fetchData(%clientId, "MaxHP");

	%a = %val * %armor.maxDamage;
	%b = %a / fetchData(%clientId, "MaxHP");
	%c = %armor.maxDamage - %b;

	if(%c < 0)
		%c = 0;
	else if(%c > %armor.maxDamage)
		%c = %armor.maxDamage;

	if(%c == %armor.maxDamage && !IsStillArenaFighting(%clientId))
	{
		storeData(%clientId, "LCK", 1, "dec");

		if(fetchData(%clientId, "LCK") >= 0)
		{
			Client::sendMessage(%clientId, $MsgRed, "You have permanently lost an LCK point!");

			if(fetchData(%clientId, "LCKconsequence") == "miss")
			{
				%c = GameBase::getDamageLevel(Client::getOwnedObject(%clientId));
				%val = -1;
			}
		}
	}

	GameBase::setDamageLevel(Client::getOwnedObject(%clientId), %c);

	return %val;
}

function ForceWakeUp(%clientId)
{
    %clientId.sleepMode = "";
    Client::setControlObject(%clientId, %clientId);
    refreshHPREGEN(%clientId);
    refreshStaminaREGEN(%clientId);
    %clientId.healTick = 0;
    Client::sendMessage(%clientId, $MsgWhite, "You awake.");
}

function HealHPCheck(%clientId)
{
    if(fetchData(%clientId, "HP") == fetchData(%clientId, "MaxHP") || fetchData(%clientId, "Stamina") == 0)
    {
        ForceWakeUp(%clientId);
    }
    else if(%clientId.sleepMode == 3)
    {
        if(%clientId.healTick > 10)
        {
            %clientId.healTick = 0;
            UseSkill(%clientId, $SkillHealing, True, True);
        }
        else
            %clientId.healTick++;
        schedule("HealHPCheck("@%clientId@");",0.3);
    }
}

function refreshHP(%clientId, %value)
{
	dbecho($dbechoMode, "refreshHP(" @ %clientId @ ", " @ %value @ ")");

	return setHP(%clientId, fetchData(%clientId, "HP") - round(%value * $TribesDamageToNumericDamage));
}
function refreshHPREGEN(%clientId,%zone)
{
	dbecho($dbechoMode, "refreshHPREGEN(" @ %clientId @ ")");

    // No natural passive regen
	//%a = CalculatePlayerSkill(%clientId, $SkillHealing) / 250000;
    
    %a = AddBonusStatePoints(%clientId, "HPRegen");
    
	if(%clientId.sleepMode == 1)
		%b = 0.0200;
	else if(%clientId.sleepMode == 2)
		%b = 0;
    else if(%clientId.sleepMode == 3)
        %b = 0.025 + CalculatePlayerSkill(%clientId, $SkillHealing) / 250000;
	else
		%b = 0;

    if($PlayersFastHealInProtectedZones && Zone::getType(%zone) == "PROTECTED")
    {
        if(!Player::isAIControlled(%clientId))
        {
            %b+=1;
        }
    }
	%c = AddPoints(%clientId, 10) / 2000;

	%r = %a + %b + %c;
    echo(%a @" "@ %b @" "@ %c);
    echo(%r);
	GameBase::setAutoRepairRate(Client::getOwnedObject(%clientId), %r);
}

//======================
// Stamina
//======================


function refreshStaminaREGEN(%clientId)
{
	dbecho($dbechoMode, "refreshStaminaREGEN(" @ %clientId @ ")");

	GameBase::setRechargeRate(Client::getOwnedObject(%clientId), calcRechargeRate(%clientId));
}

function calcRechargeRate(%clientId)
{
    %a = (CalculatePlayerSkill(%clientId, $SkillEnergy) / 6500) + (CalculatePlayerSkill(%clientId, $SkillEndurance) / 6500);

    %a = %a + AddBonusStatePoints(%clientId, "StamRegen");
    
    if(%clientId.isAtRest && %clientId.sleepMode == "")
        %a = %a + 0.3;
    
	if(%clientId.sleepMode == 1) //Sleep
		%b = 1.0 + %a;
	else if(%clientId.sleepMode == 2) //Rest
		%b = 2.25 + %a;
    else if(%clientId.sleepMode == 3) //Heal
        %b = -2 + %a;
	else
		%b = %a;

	%c = AddPoints(%clientId, 11) / 800;

	%r = %b + %c;
    
    return %r;
}

function setStamina(%clientId,%val)
{
    dbecho($dbechoMode, "setStamina(" @ %clientId @ ", " @ %val @ ")");

    %armor = Player::getArmor(%clientId);
    %max = fetchData(%clientId, "MaxStam");
	if(%val == "")
		%val = %max;

	%a = %val * %armor.maxEnergy;
	%b = %a / %max;

	if(%b < 0)
		%b = 0;
	else if(%b > %armor.maxEnergy)
		%b = %armor.maxEnergy;

	GameBase::setEnergy(Client::getOwnedObject(%clientId), %b);
}

function refreshStamina(%clientId,%value)
{
    dbecho($dbechoMode, "refreshStamina(" @ %clientId @ ", " @ %value @ ")");
    setStamina(%clientId, (fetchData(%clientId, "Stamina") - %value));
}

function WeaponStamina(%clientId,%weapon)
{
    dbecho($dbechoMode, "WeaponStamina(" @ %clientId @ ", " @ %weapon @ ")");
    if(Player::isAiControlled(%clientId))
    {
        return;
    }
    
    %skillType = $SkillType[%weapon];
    %skill = CalculatePlayerSkill(%clientId, %skillType);
    
    if(%clientId.isAtRest == 1)
    {
        %clientId.isAtRest = 0;
        refreshStaminaREGEN(%clientId);
    }
    %clientId.isAtRestCounter = 0;
    
    %minSkill = 1;
    %wx = Word::FindWord($SkillRestriction[%weapon],%skillType);
    if(%wx != -1)
        %minSkill = Cap(getWord($SkillRestriction[%weapon],%wx+1),1,"inf");
        
    %stamCost = Cap(GetAccessoryVar(%weapon, $Weight) + (%minSkill-%skill)/(1.5*%minSkill),0.5,"inf");
    //echo(%stamCost);
    refreshStamina(%clientId,%stamCost);
}

//======================
// MANA
//======================

function setMANA(%clientId, %val)
{
	dbecho($dbechoMode, "setMANA(" @ %clientId @ ", " @ %val @ ")");

    storeData(%clientId,"MANA",%val);
    
	//%armor = Player::getArmor(%clientId);
    //
	//if(%val == "")
	//	%val = fetchData(%clientId, "MaxMANA");
    //
	//%a = %val * %armor.maxEnergy;
	//%b = %a / fetchData(%clientId, "MaxMANA");
    //
	//if(%b < 0)
	//	%b = 0;
	//else if(%b > %armor.maxEnergy)
	//	%b = %armor.maxEnergy;
    //
	//GameBase::setEnergy(Client::getOwnedObject(%clientId), %b);
}

function refreshMANA(%clientId, %value)
{
	dbecho($dbechoMode, "refreshMANA(" @ %clientId @ ", " @ %value @ ")");
	setMANA(%clientId, (fetchData(%clientId, "MANA") - %value));
}

function ManaRegenTick(%clientId)
{
    dbecho($dbechoMode, "ManaRegenTick(" @ %clientId @ ")");
    if(%clientId.manaRegenTick == "")
        %clientId.manaRegenTick = 1;
    else
        %clientId.manaRegenTick++;
        
    if(%clientId.manaRegenTick >= 3)
    {
        %val = 1;
        %b = BeltEquip::AddBonusStats(%clientId,"MANARegen");
        %c = AddBonusStatePoints(%clientId, "MANARegen");
        %val = %val + %b + %c;
        refreshMANA(%clientId, -1 * %val);
        %clientId.manaRegenTick = 0;
    }
}