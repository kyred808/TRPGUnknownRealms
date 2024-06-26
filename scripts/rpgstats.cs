function fetchData(%clientId, %type)
{
	dbecho($dbechoMode, "fetchData(" @ %clientId @ ", " @ %type @ ")");

	if(%type == "LVL")
	{
		%a = GetLevel(fetchData(%clientId, "EXP"), %clientId);
		return %a;
	}
    else if(%type == "HealBurstMax")
    {
        return Cap(1 + floor(CalculatePlayerSkill(%clientId, $SkillHealing) / 125),1,5);
    }
    else if(%type == "HealBurstCounterToNext")
    {
        %curAmt = fetchData(%clientId,"HealBurst");
        return (1+%curAmt)*1000;
    }
    else if(%type == "AMR")
    {
        //%a = AddPoints(%clientId, 1);
        %a = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarAMR);
        %b = AddBonusStatePoints(%clientId, "AMR");
        //%belt = BeltEquip::AddBonusStats(%clientId,"AMR");
        //%v = Cap(%a + %b + %belt,0,"inf");
        %v = Cap(%a + %b,0,"inf");
        
        return floor(%v);
    }
	else if(%type == "DEF")
	{
		//%a = AddPoints(%clientId, 7);
        %a = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarDEF);
		%b = AddBonusStatePoints(%clientId, "DEF");
        //%belt = BeltEquip::AddBonusStats(%clientId,"DEF");
		%c = (%a + %b); //+ %belt);
		%d = (fetchData(%clientId, "OverweightStep") * 7.0) / 100;
		%e = Cap(%c - (%c * %d), 0, "inf");
        
		return floor(%e);
	}
	else if(%type == "MDEF")
	{
		//%a = AddPoints(%clientId, 3);
        %a = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarMDEF);
		%b = AddBonusStatePoints(%clientId, "MDEF");
		%c = (%a + %b); //+ BeltEquip::AddBonusStats(%clientId,"MDEF");
		%d = (fetchData(%clientId, "OverweightStep") * 7.0) / 100;
		%e = Cap(%c - (%c * %d), 0, "inf");
        
		return floor(%e);
	}
	else if(%type == "ATK")
	{
        %weapTag = fetchData(%clientId,"EquippedWeapon");
		%weapon = RPGItem::ItemTagToLabel(%weapTag);
		if(%weapon != "")
		{
			%a = AddBonusStatePoints(%clientId, "ATK");
            %projAtk = 0;
			if(GetAccessoryVar(%weapon, $AccessoryType) == $RangedAccessoryType)
            {
				%rweapon = fetchData(%clientId, "LoadedProjectile " @ %weapTag);
                if(%rweapon != "")
                {
                    %rweaponLabel = RPGItem::ItemTagToLabel(%rweapon);
                    
                    %bb = GetWord(GetAccessoryVar(%rweaponLabel, $SpecialVar), 1);
                    %im = RPGItem::getAffixValue(%rweapon,"im");
                    if(%im != 0)
                        %bb += round(%bb * 0.1 * %im);
                    %projAtk = %bb;
                }
            }
            
            //Includes weapon ATK
            %equipAtkStats = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarATK);

            //This may need a refactor, as ATK might not be the first item
            %baseAtk = GetWord(GetAccessoryVar(%weapon, $SpecialVar), 1);
            %im = RPGItem::getAffixValue(%weapTag,"im");
            %affixBonus = 0;
            if(%im != 0)
            {
                %affixBonus = round(%baseAtk * 0.1 * %im);
                //%affixBonus = %affixBonus + RPGItem::getAffixValue(%weapon,$RPGItem::SpecialVarToAffix[$SpecialVarATK]);
            }
            //%b = %baseAtk + %extra;
            //echo("AB: "@ %affixBonus);
            //echo(%a);
            //echo(%equipAtkStats);
            %val = %a + %equipAtkStats + %projAtk + %affixBonus; //+ %c;

			return %val;
		}
		else
			return 0;
	}
	else if(%type == "MaxHP")
	{
		%a = $MinHP[fetchData(%clientId, "RACE")] + (CalculatePlayerSkill(%clientId, $SkillEndurance) * 0.6);
		//%b = AddPoints(%clientId, 4);
        %b = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarHP);
		%c = floor(fetchData(%clientId, "RemortStep") * (CalculatePlayerSkill(%clientId, $SkillEndurance) / 8));
		%d = fetchData(%clientId, "LVL");
		%e = AddBonusStatePoints(%clientId, "MaxHP");
        //%f = BeltEquip::AddBonusStats(%clientId,"MaxHP");
		return floor(%a + %b + %c + %d + %e);// + %f);
	}
	else if(%type == "HP")
	{
		%armor = Player::getArmor(%clientId);

		%c = %armor.maxDamage - GameBase::getDamageLevel(Client::getOwnedObject(%clientId));
		%a = %c * fetchData(%clientId, "MaxHP");
		%b = %a / %armor.maxDamage;

		return round(%b);
	}
	else if(%type == "MaxMANA")
	{
        //%lvl = fetchData(%clientId,"LVL");
        //%rl = fetchData(%clientId,"RemortStep");
        //%eng = floor( CalculatePlayerSkill(%clientId, $SkillEnergy) * $ManaEnergyFactor );
        ////%eqp = BeltEquip::AddBonusStats(%clientId,"MaxMANA");
        //%eqp = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarMana);
        //%extra = 0;
        //if(fetchData(%clientId,"Class") == "Mage")
        //    %extra = 15;
        //return 5*%lvl + 3*%rl + %eng + %eqp + %extra;
        
		%a = 8 + round( CalculatePlayerSkill(%clientId, $SkillEnergy) * (1/3) ) + floor(fetchData(%clientId,"LVL")*$ManaPerLevel);
		//%b = AddPoints(%clientId, 5);
		%c = AddBonusStatePoints(%clientId, "MaxMANA");
        %d = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarMana);
		return %a + %c + %d;
	}
	else if(%type == "MANA")
	{
        //return $ClientData[%clientId, %type];
		%armor = Player::getArmor(%clientId);
        
		%a = GameBase::getEnergy(Client::getOwnedObject(%clientId)) * fetchData(%clientId, "MaxMANA");
		%b = %a / %armor.maxEnergy;
        
		return round(%b);
	}
	else if(%type == "MaxWeight")
	{
		%a = 50 + CalculatePlayerSkill(%clientId, $SkillWeightCapacity);
		//%b = AddPoints(%clientId, 9);
        %b = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarMaxWeight);
		%c = AddBonusStatePoints(%clientId, "MaxWeight");
		return FixDecimals(%a + %b + %c);
	}
    else if(%type == "MANAThief")
    {
        %bonus = AddBonusStatePoints(%clientId, "MANAThief");
        //%equip = AddPoints(%clientId, $SpecialVarManaThief);
        %equip = RPGItem::GetPlayerEquipStats(%clientId,$SpecialVarManaThief);
        return %bonus + %equip;
    }
    else if(%type == "MANAHarvest")
    {
        %bonus = AddBonusStatePoints(%clientId, "MANAHarvest");
        %equip = AddPoints(%clientId, $SpecialVarManaHarvest);
        return %bonus + %equip;
    }
    else if(%type == "AMRP")
    {
        %equip = RPGItem::GetPlayerEquipStats(%clientId,%clientId, $SpecialVarArmorPiercing);
        %bonus = AddBonusStatePoints(%clientId, "AMRP");
        return %bonus + %equip;
    }
    else if(%type == "TrueShot")
    {
        //In case we want another method of giving trueshot
        %a = AddBonusStatePoints(%clientId, "TrueShot") > 0;
        return %a;
    }
    else if(%type == "Brace")
    {
        //In case we want another method of giving brace
        %a = AddBonusStatePoints(%clientId, "Brace");
        %ret = Cap(%a,0,100);
        return %ret;
    }
	else if(%type == "Weight")
	{
		return GetWeight(%clientId);
	}
	else if(%type == "RankPoints")
	{
		return Cap(floor($ClientData[%clientId, %type]), 0, "inf");
	}
	else if(%type == "OverweightStep")
	{
		return Cap(floor($ClientData[%clientId, %type]), 0, "inf");
	}
	else if(%type == "SlowdownHitFlag")
	{
		if(Player::isAiControlled(%clientId))
			return False;
		else
			return $ClientData[%clientId, %type];
	}
	else
		return $ClientData[%clientId, %type];

	return False;
}
function remotefetchData(%clientId, %type)
{
	dbecho($dbechoMode, "remotefetchData(" @ %clientId @ ", " @ %type @ ")");


	//rpgfetchdata specific vartypes
	if(%type == "zonedesc")
	{
		%r = fetchData(%clientId, "zone");
		%data = Zone::getDesc(%r);
	}
	else if(%type == "password")
	{
		return;
	}
	else if(%type == "servername")
	{
		%data = $Server::HostName;
	}
	else if(GetWord(%type, 0) == "skill" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = $PlayerSkill[%clientId, %s];
	}
	else if(GetWord(%type, 0) == "getbuycost" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = getBuyCost(%clientId, %s);
	}
	else if(GetWord(%type, 0) == "getsellcost" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = getSellCost(%clientId, %s);
	}
	else if(GetWord(%type, 0) == "skillcanuse" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = SkillCanUse(%clientId, %s);
	}
	else if(GetWord(%type, 0) == "spellcancast" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = SpellCanCast(%clientId, %s);
	}
	else if(GetWord(%type, 0) == "skillcancastnow" && (%s = GetWord(%type, 1)) != -1)
	{
		return;
		%data = SpellCanCastNow(%clientId, %s);
	}
	else if(%type == "RACE" || %type == "CLASS" || %type == "EXP" || %type == "LCK" || %type == "COINS" || %type == "MANA" || %type == "RemortStep" || %type == "bounty" || %type == "RankPoints" || %type == "MyHouse" || %type == "HP" || %type == "MaxHP" || %type == "BANK" || %type == "DEF" || %type == "MDEF" || %type == "SPcredits" || %type == "isMimic" || %type == "ATK" || %type == "MaxMANA" || %type == "MaxWeight" || %type == "LCKconsequence" || %type == "Weight" || %type == "LVL" || %type == "grouplist")
		%data = fetchData(%clientId, %type);
	else
	{
		%data = "omg!";
	}

	remoteEval(%clientId, SetRPGdata, %data, %type);
}

function storeData(%clientId, %type, %amt, %special)
{
	dbecho($dbechoMode, "storeData(" @ %clientId @ ", " @ %type @ ", " @ %amt @ ", " @ %special @ ")");

	if(%type == "HP")
	{
		setHP(%clientId, %amt);
	}
	else if(%type == "MANA")
	{
        %newVal = 0;
		if(%special == "inc")
			%newVal = $ClientData[%clientId, %type] + %amt;
		else if(%special == "dec")
			%newVal = $ClientData[%clientId, %type] - %amt;
        else
            %newVal = %amt;
            
        $ClientData[%clientId, %type] = Cap(%newVal,0,fetchData(%clientId,"MaxMANA"));
	}
    else if(%type == "COINS")
    {
        if(%special == "inc")
        {
			$ClientData[%clientId, "COINS"] += %amt;
            $ClientData[%clientId, "totalWeight"] += %amt * $coinweight;
            storeData(%clientId,"refreshWeight",1,"inc");
        }
		else if(%special == "dec")
        {
			$ClientData[%clientId, "COINS"] -= %amt;
            $ClientData[%clientId, "totalWeight"] -= %amt * $coinweight;
            storeData(%clientId,"refreshWeight",1,"inc");
        }
        else
        {
            %prev = $ClientData[%clientId, "COINS"];
            %diff = %amt - %prev;
			$ClientData[%clientId, "COINS"] = %amt;
            $ClientData[%clientId, "totalWeight"] += %diff * $coinweight;
            storeData(%clientId,"refreshWeight",1,"inc");
        }
    }
    else if(%type == "HealBurstCounter")
    {
        %tnb = fetchData(%clientId,"HealBurstCounterToNext");
        %max = fetchData(%clientId,"HealBurstMax");
        if(fetchData(%clientId,"HealBurst") < %max)
        {
            if(%special == "inc")
                $ClientData[%clientId, %type] += %amt;
            else if(%special == "dec")
                $ClientData[%clientId, %type] -= %amt;
            else
                $ClientData[%clientId, %type] = %amt;
            
            %new = $ClientData[%clientId, %type];
            
            if(%new >= %tnb)
            {
                storeData(%clientId,"HealBurst",1,"inc");
                %n = fetchData(%clientId,"HealBurst");
                Client::sendMessage(%clientId,$MsgWhite,"You gained a heal burst! ("@%n@"/"@%max@")~wUnravelAM.wav");
                if(%n < %max)
                {
                    %cnt = %new - %tnb;
                    //This could recurse, if %cnt is high enough
                    storeData(%clientId,"HealBurstCounter",%cnt);
                }
                else
                    $ClientData[%clientId, %type] = 0;
                
                
            }
        }
    }
    else if(%type == "refreshWeight") //Prevent weight from drifting due to floating point error
    {
        if(Player::isAIControlled(%clientId))
            return; //Dont' are about weight drift on bots
        if(%special == "inc")
			$ClientData[%clientId, "refreshWeight"] += %amt;
        
        if($ClientData[%clientId, "refreshWeight"] > $RecalcuatePlayerWeightCounterMax)
        {
            $ClientData[%clientId, "totalWeight"] = WeightRecalculate(%clientId);
            $ClientData[%clientId, "refreshWeight"] = 0;
        }
    }
	else if(%type == "MaxHP" || %type == "MaxMANA" ||%type == "MaxWeight" || %type == "Weight")
	{
		echo("Invalid call to storeData for " @ %type @ " : Can't manually set this variable.");
	}
	else
	{
		if(%special == "inc")
			$ClientData[%clientId, %type] += %amt;
		else if(%special == "dec")
			$ClientData[%clientId, %type] -= %amt;
		else if(%special == "strinc")
			$ClientData[%clientId, %type] = $ClientData[%clientId, %type] @ %amt;
		else
			$ClientData[%clientId, %type] = %amt;

		if(GetWord(%special, 1) == "cap")
			$ClientData[%clientId, %type] = Cap($ClientData[%clientId, %type], GetWord(%special, 2), GetWord(%special, 3));
	}
}

function MenuSP(%clientId, %page)
{
	dbecho($dbechoMode, "MenuSP(" @ %clientId @ ", " @ %page @ ")");

	Client::buildMenu(%clientId, "You have " @ fetchData(%clientId, "SPcredits") @ " SP credits", "sp", true);

	%clientId.bulkNum = "";

	%l = 6;
	%ns = GetNumSkills();
	%np = floor(%ns / %l);
	
	%lb = (%page * %l) - (%l-1);
	%ub = %lb + (%l-1);
	if(%ub > %ns)
		%ub = %ns;

	for(%i = %lb; %i <= %ub; %i++)
    {
        %bonus = RPGItem::GetPlayerEquipStats(%clientId,"SKILL"@%i);//BeltEquip::AddBonusStats(%clientId,"SKILL"@%i);
        if(%bonus > 0)
            Client::addMenuItem(%clientId, %cnt++ @ "(" @ GetPlayerSkill(%clientId, %i) @ "+"@ %bonus @") " @ $SkillDesc[%i], %i @ " " @ %page);
        else
            Client::addMenuItem(%clientId, %cnt++ @ "(" @ GetPlayerSkill(%clientId, %i) @ ") " @ $SkillDesc[%i], %i @ " " @ %page);
    }

	if(%page == 1)
	{
		Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1);
		Client::addMenuItem(%clientId, "xDone", "done");
	}
	else if(%page == %np+1)
	{
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1);
		Client::addMenuItem(%clientId, "xDone", "done");
	}
	else
	{
		Client::addMenuItem(%clientId, "nNext >>", "page " @ %page+1);
		Client::addMenuItem(%clientId, "p<< Prev", "page " @ %page-1);
	}

	return;
}
function processMenusp(%clientId, %opt)
{
	dbecho($dbechoMode, "processMenusp(" @ %clientId @ ", " @ %opt @ ")");

	%o = GetWord(%opt, 0);
	%p = GetWord(%opt, 1);

	if(fetchData(%clientId, "SPcredits") > 0 && %o != "page" && %o != "done")
	{
        %spCredits = fetchData(%clientId, "SPcredits");
        %limit = CalculateSPToCurrentUpperBound(%clientId,%o);
        if(%limit > 0)
        {
            %echo = true;
            if(%clientId.bulkNum < 1 || %clientId.bulkNum == "")
            {
                %clientId.bulkNum = 1;
                %echo = false;
            }
            
            if(%clientId.bulkNum > %spCredits)
                %clientId.bulkNum = %spCredits;
                
            if(%clientId.bulkNum > %limit)
                %clientId.bulkNum = %limit;
            
            if(AddSkillPoint(%clientId, %o, %clientId.bulkNum,true))
                storeData(%clientId, "SPcredits", %clientId.bulkNum, "dec");
            
            if(%echo)
                Client::SendMessage(%clientId,$MsgWhite,"You spent "@ %clientId.bulkNum @" SP on "@$SkillDesc[%o]);
            
            RefreshAll(%clientId,false);
        }
		//if(%clientId.bulkNum > 30 && !(%clientId.adminLevel >= 1) )
		//	%clientId.bulkNum = 30;
        //
        //  
		//for(%i = 1; %i <= %clientId.bulkNum; %i++)
		//{
		//	if(fetchData(%clientId, "SPcredits") > 0)
		//	{
		//		if(AddSkillPoint(%clientId, %o))
		//			storeData(%clientId, "SPcredits", 1, "dec");
		//		else
		//			break;
		//	}
		//	else
		//		break;
		//}

		//RefreshAll(%clientId);
	}

	if(%o != "done")
		MenuSP(%clientId, %p);
}
function processMenunull(%clientId, %opt)
{
	return;
}

function MenuGroup(%clientId)
{
	dbecho($dbechoMode, "MenuGroup(" @ %clientId @ ")");

	Client::buildMenu(%clientId, "Pick a group:", "pickgroup", true);
	Client::addMenuItem(%clientId, "1Priest", 1);
	Client::addMenuItem(%clientId, "2Rogue", 2);
	Client::addMenuItem(%clientId, "3Warrior", 3);
	Client::addMenuItem(%clientId, "4Wizard", 4);

	return;
}
function processMenupickgroup(%clientId, %opt)
{
	dbecho($dbechoMode, "processMenupickgroup(" @ %clientId @ ", " @ %opt @ ")");

	if(%opt == 1)
		storeData(%clientId, "GROUP", "Priest");
	else if(%opt == 2)
		storeData(%clientId, "GROUP", "Rogue");
	else if(%opt == 3)
		storeData(%clientId, "GROUP", "Warrior");
	else if(%opt == 4)
		storeData(%clientId, "GROUP", "Wizard");

	%clientId.choosingGroup = "";
	%clientId.choosingClass = True;

	MenuClass(%clientId);
}

function MenuClass(%clientId)
{
	dbecho($dbechoMode, "MenuClass(" @ %clientId @ ")");

	Client::buildMenu(%clientId, "Pick a class:", "pickclass", true);

	%op = 0;
	for(%i = 1; $ClassName[%i, 0] != ""; %i++)
	{
		if(String::ICompare(fetchData(%clientId, "GROUP"), $ClassGroup[$ClassName[%i, 0]]) == 0)
		{
			%op++;
			Client::addMenuItem(%clientId, %op @ $ClassName[%i, 0], %op);
		}
	}
	Client::addMenuItem(%clientId, "x<-- BACK", "back");


	return;
}
function processMenupickclass(%clientId, %opt)
{
	dbecho($dbechoMode, "processMenupickclass(" @ %clientId @ ", " @ %opt @ ")");

	if(%opt == "back")
	{
		%clientId.choosingClass = "";
		%clientId.choosingGroup = True;
		storeData(%clientId, "GROUP", "");

		MenuGroup(%clientId);
		return;
	}

	%op = 0;
	for(%i = 1; $ClassName[%i, 0] != ""; %i++)
	{
		if(String::ICompare(fetchData(%clientId, "GROUP"), $ClassGroup[$ClassName[%i, 0]]) == 0)
		{
			%op++;
			if(%op == %opt)
				storeData(%clientId, "CLASS", $ClassName[%i, 0]);
		}
	}
    
    SetAllSkills(%clientId, 0);
        //add $autoStartupSP for each skill
	for(%i = 1; %i <= GetNumSkills(); %i++)
		AddSkillPoint(%clientId, %i, $autoStartupSP);
    

    storeData(%clientId,"tempPrimarySkills","");
    storeData(%clientId,"tempSecondarySkills","");
    MenuPickSkillBonus(%clientId,fetchData(%clientId,"CLASS"),0,1);
}

function MenuPickSkillBonus(%clientId,%class,%num,%page)
{
    %primary = false;
    //echo(%num);
    if(%num < $SkillBoostNumPrimary)
    {
        Client::buildMenu(%clientId, "Pick "@ $SkillBoostNumPrimary @" Primary Skills ("@ $SkillBoostNumPrimary - %num @")", "PickSkillBonus", true);
        %primary = true;
    }
    else
        Client::buildMenu(%clientId, "Pick "@ $SkillBoostMax - $SkillBoostNumPrimary @" Secondary Skills ("@ $SkillBoostMax - %num @")", "PickSkillBonus", true);
        
    %l = 6;
	%ns = GetNumSkills();
	%np = floor(%ns / %l);
	
	%lb = (%page * %l) - (%l-1);
	%ub = %lb + (%l-1);
	if(%ub > %ns)
		%ub = %ns;
    
    %prim = fetchData(%clientId,"tempPrimarySkills");
    %sec = fetchData(%clientId,"tempSecondarySkills");
    
    //echo("Primary: "@ %prim);
    //echo("Secondary: "@ %sec);
	for(%i = %lb; %i <= %ub; %i++)
    {
        if(Word::findWord(%prim,%i) != -1)
        {
            %c = GetPlayerSkill(%clientId, %i) + ($SkillMultiplier[%class,%i]*($SkillPrimaryBonus-1));
            %d = round(%c * 10);
            %e = (%d / 10) * 1.000001;
            Client::addMenuItem(%clientId, %cnt++ @ "**(" @ %e @ ") " @ $SkillDesc[%i],"primary "@ %i @ " " @ %class @ " " @ %num @ " " @ %page);
        }
        else if(Word::findWord(%sec,%i) != -1)
        {
            %c = GetPlayerSkill(%clientId, %i) + ($SkillMultiplier[%class,%i]*($SkillSecondaryBonus-1));
            %d = round(%c * 10);
            %e = (%d / 10) * 1.000001;
            Client::addMenuItem(%clientId, %cnt++ @ "*(" @ %e @ ") " @ $SkillDesc[%i],"secondary "@ %i @ " " @ %class @ " " @ %num @ " " @ %page);
        }
        else
            Client::addMenuItem(%clientId, %cnt++ @ "(" @ GetPlayerSkill(%clientId, %i) @ ") " @ $SkillDesc[%i],"select "@ %i @ " " @ %class @ " " @ %num @ " " @ %page);
    }

	if(%page == 1)
	{
		Client::addMenuItem(%clientId, "nNext >>", "page filler "@ %class @" "@ %num @" "@ %page+1);
		Client::addMenuItem(%clientId, "b<--Back", "back");
	}
	else if(%page == %np+1)
	{
		Client::addMenuItem(%clientId, "p<< Prev", "page filler "@ %class @" "@ %num @" "@ %page-1);
		Client::addMenuItem(%clientId, "b<--Back", "back");
	}
	else
	{
		Client::addMenuItem(%clientId, "nNext >>", "page filler "@ %class @" "@ %num @" "@ %page+1);
		Client::addMenuItem(%clientId, "p<< Prev", "page filler "@ %class @" "@ %num @" "@ %page-1);
	}
}

function processMenuPickSkillBonus(%clientId, %opt)
{
    %option = getWord(%opt,0);
    %skill = getWord(%opt,1);
    %class = getWord(%opt,2);
    %num = getWord(%opt,3);
    %page = getWord(%opt,4);
    
    %prim = fetchData(%clientId,"tempPrimarySkills");
    %sec = fetchData(%clientId,"tempSecondarySkills");
    
    if(%option == "select")
    {
        if(%num < $SkillBoostNumPrimary)
            storeData(%clientId,"tempPrimarySkills",%skill@" ","strinc");
        else
            storeData(%clientId,"tempSecondarySkills",%skill@" ","strinc");
            
        %num++;
        if(%num < $SkillBoostMax)
        {
            MenuPickSkillBonus(%clientId,%class,%num,%page);
        }
        else
            CreateNewPlayer(%clientId);
    }
    else if(%option == "primary") //Deselect primary skill
    {
        storeData(%clientId,"tempPrimarySkills",String::RemoveWords(%prim,%skill));
        if(%sec != "")
        {
            storeData(%clientId,"tempSecondarySkills","");
            %num = $SkillBoostNumPrimary - 1; //Can make this assumption, as primary had to be full to have any secondaries
            Client::sendMessage(%clientId,$MsgRed,"Secondary Skills Cleared.~wError_Message.wav");
        }
        else
            %num--;
        MenuPickSkillBonus(%clientId,%class,%num,%page);
    }
    else if(%option == "secondary") //Deselect secondary skill
    {
        storeData(%clientId,"tempSecondarySkills",String::RemoveWords(%sec,%skill));
        MenuPickSkillBonus(%clientId,%class,%num-1,%page);
    }
    else if(%option == "page")
    {
        MenuPickSkillBonus(%clientId,%class,%num,%page);
    }
    else if(%option == "back")
    {
        storeData(%clientId,"tempPrimarySkills","");
        storeData(%clientId,"tempSecondarySkills","");
        storeData(%clientId,"CLASS","");
        SetAllSkills(%clientId, 0);
        MenuClass(%clientId);
    }
}

function CreateNewPlayer(%clientId)
{
    	//let the player enter the world
	%clientId.choosingClass = "";
    %clientId.newPlayer = true;
    if($RealmData::RealmIdToLabel[0] != "")
        storeData(%clientId,"Realm",$RealmData::RealmIdToLabel[0]);
        
    echo("New Player Dropping in Realm: "@fetchData(%clientId,"Realm"));
    %class = fetchData(%clientId,"CLASS");
    storeData(%clientId, "spawnStuff", $ClassSpawnStuff[%class],"strinc");
	Game::playerSpawn(%clientId, false);

	//######### set a few start-up variables ########
	storeData(%clientId, "COINS", GetRoll($initcoins[fetchData(%clientId, "GROUP")]));

	
	//###############################################
    
    
    %prim = fetchData(%clientId,"tempPrimarySkills");
    %sec = fetchData(%clientId,"tempSecondarySkills");
    for(%i = 0; %i < getWordCount(%prim); %i++)
    {
        %skill = getWord(%prim,%i);
        %c = GetPlayerSkill(%clientId, %skill) + ($SkillMultiplier[%class,%skill]*($SkillPrimaryBonus-1));
        %d = round(%c * 10);
        %e = (%d / 10) * 1.000001;
        echo("Primary: "@$SkillDesc[%skill]@ " "@ GetPlayerSkill(%clientId, %skill) @" -> "@ %e);
        $PlayerSkill[%clientId, %skill] = %e;
    }
    
    for(%i = 0; %i < getWordCount(%sec); %i++)
    {
        %skill = getWord(%sec,%i);
        %c = GetPlayerSkill(%clientId, %skill) + ($SkillMultiplier[%class,%skill]*($SkillSecondaryBonus-1));
        %d = round(%c * 10);
        %e = (%d / 10) * 1.000001;
        $PlayerSkill[%clientId, %skill] = %e;
    }
    
    %clientId.newPlayer = false;
    schedule("Client::sendMessage("@%clientId@",0,\"Talk to the man with HI\");",1);

	centerprint(%clientId, "<f1>Server powered by the RPG MOD version " @ $rpgver @ "<f0>\n\n" @ $loginMsg, 15);
}

function OldGetLevel(%ex, %clientId)
{
	dbecho($dbechoMode, "GetLevel(" @ %ex @ ", " @ %clientId @ ")");

	%m = GetEXPmultiplier(%clientId);

	if(%m != 0)
	{
		%a = (  (-500 * %m) + FixDecimals(sqrt( (250000 * %m * %m) + (2000 * %m * %ex) ))  ) / (1000 * %m);
		%b = floor(%a) + 1;
	}

	return %b;
}
function OldGetExp(%level, %clientId)
{
	dbecho($dbechoMode, "GetExp(" @ %level @ ", " @ %clientId @ ")");

	%m = GetEXPmultiplier(%clientId);

	%level--;
	%a = (500 * %level) + (500 * %level * %level);
	%b = floor( (%a * %m) + 0.2);

	return %b;
}

function GetLevel(%ex, %clientId)
{
	dbecho($dbechoMode, "GetLevel(" @ %ex @ ", " @ %clientId @ ")");

	%n = 1000;
	%b = floor(%ex / %n) + 1;

	return %b;
}
function GetExp(%level, %clientId)
{
	dbecho($dbechoMode, "GetExp(" @ %level @ ", " @ %clientId @ ")");

	%n = 1000;
	%b = (%level - 1) * %n;

	return %b;
}

function DistributeExpForKilling(%damagedClient)
{
	dbecho($dbechoMode2, "DistributeExpForKilling(" @ %damagedClient @ ")");

	%dname = Client::getName(%damagedClient);
	%dlvl = fetchData(%damagedClient, "LVL");

	%count = 0;

	//parse $damagedBy and create %finalDamagedBy
	%nameCount = 0;
	%listCount = 0;
	%total = 0;
	for(%i = 1; %i <= $maxDamagedBy; %i++)
	{
		if($damagedBy[%dname, %i] != "")
		{
			%listCount++;

			%n = GetWord($damagedBy[%dname, %i], 0);
			%d = GetWord($damagedBy[%dname, %i], 1);

			%flag = 0;
			for(%z = 1; %z <= %nameCount; %z++)
			{
				if(%finalDamagedBy[%z] == %n)
				{
					%flag = 1;
					%dCounter[%n] += %d;
				}
			}
			if(%flag == 0)
			{
				%nameCount++;
				%finalDamagedBy[%nameCount] = %n;
				%dCounter[%n] = %d;

				%p = IsInWhichParty(%n);
				if(%p != -1)
				{
					%id = GetWord(%p, 0);
					%inv = GetWord(%p, 1);
					if(%inv == -1)
					{
						%tmppartylist[%id] = %tmppartylist[%id] @ %n @ " ";
						if(String::findSubStr(%tmpl, %id @ " ") == -1)
							%tmpl = %tmpl @ %id @ " ";
					}
				}
			}
			%total += %d;
		}
	}

	//clear $damagedBy
	for(%i = 1; %i <= $maxDamagedBy; %i++)
		$damagedBy[%dname, %i] = "";

	//parse thru all tmppartylists and determine the number of same party members involved in exp split
	for(%w = 0; (%a = GetWord(%tmpl, %w)) != -1; %w++)
	{
		%n = CountObjInList(%tmppartylist[%a]);
		for(%ww = 0; (%aa = GetWord(%tmppartylist[%a], %ww)) != -1; %ww++)
			%partyFactor[%aa] = %n;
	}

	//distribute exp
	for(%i = 1; %i <= %nameCount; %i++)
	{
		if(%finalDamagedBy[%i] != "")
		{
			%listClientId = NEWgetClientByName(%finalDamagedBy[%i]);

			%slvl = fetchData(%listClientId, "LVL");

			if(RPG::isAiControlled(%damagedClient))
			{
				if(%slvl > $PlayerLevelLimitForExp)
					%value = 0;
				else
				{
					%f = (101 - %slvl) / 10;
					if(%f < 1) %f = 1;

					%a = (%dlvl - %slvl) + 8;
					%b = %a * %f;
					if(%b < 1) %b = 1;

					%z = %b * 0.10;
					%y = getRandom() * %z;
					%r = %y - (%z / 2);

					%c = %b + %r;

					%value = %c;
				}
			}
			else
			{
				%value = 0;
			}
            
			//rank point bonus
			if(fetchData(%listClientId, "MyHouse") != "")
			{
				%ph = Cap(GetRankBonus(%listClientId), 1.00, 3.00);
				%value = %value * %ph;
			}

			%perc = %dCounter[%finalDamagedBy[%i]] / %total;
			%final = Cap(round( %value * %perc ), "inf", 1000);

			//determine party exp
			%pf = %partyFactor[%finalDamagedBy[%i]];
			if(%pf != "" && %pf >= 2)
				%pvalue = round(%final * (1.0 + (%pf * 0.1)));
			else
				%pvalue = 0;

			storeData(%listClientId, "EXP", %final, "inc");
			if(%final > 0)
				Client::sendMessage(%listClientId, 0, %dname @ " has died and you gained " @ %final @ " experience!");
			else if(%final < 0)
				Client::sendMessage(%listClientId, 0, %dname @ " has died and you lost " @ -%final @ " experience.");
			else if(%final == 0)
				Client::sendMessage(%listClientId, 0, %dname @ " has died.");

			if(%pvalue != 0)
			{
				storeData(%listClientId, "EXP", %pvalue, "inc");
				Client::sendMessage(%listClientId, $MsgWhite, "You have gained " @ %pvalue @ " party experience!");
			}

			Game::refreshClientScore(%listClientId);
            
            if(fetchData(%TrueClientId,"HealBurst") < fetchData(%TrueClientId,"HealBurstMax"))
            {
                storeData(%listClientId,"HealBurstCounter",%final + %pvalue,"inc");
                //Client::sendMessage(%listClientId, $MsgWhite, "You have gained " @ %pvalue @ " party experience!");
            }
		}
	}
}

function StartStatSelection(%clientId)
{
	dbecho($dbechoMode, "StartStatSelection(" @ %clientId @ ")");

	%group = nameToId("MissionGroup\\ObserverDropPoints");
	%observerMarker = Group::getObject(%group, 0);
	
	Client::setControlObject(%clientId, Client::getObserverCamera(%clientId));
	Observer::setFlyMode(%clientId, GameBase::getPosition(%observerMarker), GameBase::getRotation(%observerMarker), false, true);

	storeData(%clientId, "SPcredits", $initSPcredits);

	MenuGroup(%clientId);
}

function Game::refreshClientScore(%clientId)
{
	dbecho($dbechoMode2, "Game::refreshClientScore(" @ %clientId @ ")");

	if(fetchData(%clientId, "HasLoadedAndSpawned"))
	{
		if(GetLevel(fetchData(%clientId, "EXP"), %clientId) != fetchData(%clientId, "templvl") && fetchData(%clientId, "HasLoadedAndSpawned") && fetchData(%clientId, "templvl") != "")
		{
			//client has leveled up
			%lvls = (GetLevel(fetchData(%clientId, "EXP"), %clientId) - fetchData(%clientId, "templvl"));
            
			storeData(%clientId, "SPcredits", (%lvls * $SPgainedPerLevel), "inc");

			if(%lvls > 0)
			{
				if(%lvls == 1)
					Client::sendMessage(%clientId,0,"You have gained a level!");		
				else
					Client::sendMessage(%clientId,0,"You have gained " @ %lvls @ " levels!");
				Client::sendMessage(%clientId,0,"Welcome to level " @ fetchData(%clientId, "LVL"));
				PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
                
                //Refresh health and mana!
                setHP(%clientId);
                setMana(%clientId);
                //setStamina(%clientId);
                
			}
			else if(%lvls < 0)
			{
				if(%lvls == -1)
					Client::sendMessage(%clientId,0,"You have lost a level...");		
				else
					Client::sendMessage(%clientId,0,"You have lost " @ -%lvls @ " levels...");
				Client::sendMessage(%clientId,0,"You are now level " @ fetchData(%clientId, "LVL"));
			}
            
            RefreshEquipment(%clientId);
		}
		storeData(%clientId, "templvl", GetLevel(fetchData(%clientId, "EXP"), %clientId));

		%lvl = GetLevel(fetchData(%clientId, "EXP"), %clientId);
		%rcheck = $ClassName[1, fetchData(%clientId, "RemortStep")+1];
		%cr = fetchData(%clientId, "currentlyRemorting");
		%maxlvl = 125 + fetchData(%clientId, "RemortStep");
		if(%lvl >= %maxlvl && %rcheck != "" && !%cr && !Player::isAiControlled(%clientId))
		{
			//FORCE REMORT!!!

			storeData(%clientId, "currentlyRemorting", True);

			for(%i = 1; %i <= 20; %i++)
			{
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"Bomb7\", GameBase::getPosition(" @ %clientId @ "), False, 19);", %i * 3, %clientId);
			}

			schedule("DoRemort(" @ %clientId @ ");", 60, %clientId);
		}
	}
    
    %clZone = "";
    if(fetchData(%clientId, "invisible"))
        %clZone = fetchData(%clientId,"lastScentZone");
    else
        %clZone = fetchData(%clientId, "zone");
	%z = Zone::getDesc(%clZone);
    
	if(%z == -1)
		%z = "unknown";

	if($displayPingAndPL)
		Client::setScore(%clientId, "%n\t" @ %z @ "\t  " @ fetchData(%clientId, "LVL") @ "\t%p\t%l", fetchData(%clientId, "LVL"));
	else
	{
            Client::setScore(%clientId, "%n\t" @ %z @ "\t  " @ fetchData(%clientId, "LVL") @ "\t" @ getFinalCLASS(%clientId) @ "\t%l", fetchData(%clientId, "LVL"));
	}
}

function DoRemort(%clientId)
{
	dbecho($dbechoMode, "DoRemort(" @ %clientId @ ")");

	storeData(%clientId, "RemortStep", 1, "inc");

	storeData(%clientId, "EXP", 0);
	storeData(%clientId, "templvl", 1);
	storeData(%clientId, "LCK", $initLCK, "inc");
	storeData(%clientId, "SPcredits", $initSPcredits, "inc");
	storeData(%clientId, "currentlyRemorting", "");

	//skill variables
	%cnt = 0;
	for(%i = 1; %i <= GetNumSkills(); %i++)
	{
		$PlayerSkill[%clientId, %i] = 0;
		$SkillCounter[%clientId, %i] = 0;
	}
	for(%i = 1; %i <= GetNumSkills(); %i++)
		AddSkillPoint(%clientId, %i, $autoStartupSP);

	UnequipMountedStuff(%clientId);
    BeltEquip::UnequipAll(%clientId);
	
	Player::setDamageFlash(%clientId, 1.0);
	Item::setVelocity(%clientId, "0 0 0");
	%pos = TeleportToMarker(%clientId, "Teams/team0/DropPoints", 0, 0);

	playSound(RespawnC, GameBase::getPosition(%clientId));
	
	RefreshAll(%clientId,true);

	Client::sendMessage(%clientId, $MsgBeige, "Welcome to Remort Level " @ fetchData(%clientId, "RemortStep") @ "!");

	return %pos;
}

function GetRankBonus(%clientId)
{
	dbecho($dbechoMode, "GetRankBonus(" @ %clientId @ ")");

	return 1 + ( fetchData(%clientId, "RankPoints") / 100 );
}