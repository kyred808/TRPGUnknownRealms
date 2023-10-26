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

function refreshStaminaREGEN(%clientId)
{
	dbecho($dbechoMode, "refreshMANAREGEN(" @ %clientId @ ")");

	%a = (CalculatePlayerSkill(%clientId, $SkillEnergy) / 3250);
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

	GameBase::setRechargeRate(Client::getOwnedObject(%clientId), %r);
}

function UpdateManaRegen(%clientId)
{
    if(%clientId.manaRegenTick = "")
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

function setStamina(%clientId,%val)
{
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
    setStamina(%clientId, (fetchData(%clientId, "Stamina") - %value));
}
