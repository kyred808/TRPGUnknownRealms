//======================================================================
// Bonus States are special bonuses for a certain player that last a
// certain amount of ticks.  A tick is decreased every 2 seconds by
// the zone check.
//======================================================================

$maxBonusStates = 50;

$BonusStateDesc["ATK"] = "Attack";
$BonusStateDesc["DEF"] = "Defense";
$BonusStateDesc["MDEF"] = "Magic Defense";
$BonusStateDesc["AMRP"] = "Armor Piercing";
$BonusStateDesc["AMR"] = "Armor";
$BonusStateDesc["SDM"] = "Spell Dmg";

$BonusStateNegative["SecondWindCD"] = true;
$BonusStateNegative["RageCD"] = true;
$BonusStateNegative["ManaFlareCD"] = true;

function DecreaseBonusStateTicks(%clientId, %b)
{
	if(%b != "")
	{
		//Decrease specified tick for the player
		$BonusStateCnt[%clientId, %b]--;

		if($BonusStateCnt[%clientId, %b] <= 0)
		{
			$BonusStateCnt[%clientId, %b] = "";
            if(Word::FindWord($BonusState[%clientId, %i],"FoodCoolDown") != -1)
                Client::sendMessage(%clientId,$MsgBeige,"You are able to eat again.");
			$BonusState[%clientId, %b] = "";
			playSound(BonusStateExpire, GameBase::getPosition(%clientId));
            refreshHPREGEN(%clientId);
            refreshStaminaREGEN(%clientId);
            refreshAll(%clientId,false);
		}
	}
	else
	{
		%totalbcnt = 0;
		%truebcnt = 0;

		//Decrease all ticks for that player
		for(%i = 1; %i <= $maxBonusStates; %i++)
		{
			if($BonusStateCnt[%clientId, %i] > 0)
			{
				$BonusStateCnt[%clientId, %i]--;

				if($BonusStateCnt[%clientId, %i] <= 0)
				{
					$BonusStateCnt[%clientId, %i] = "";
                    if(Word::FindWord($BonusState[%clientId, %i],"FoodCoolDown") != -1)
                        Client::sendMessage(%clientId,$MsgBeige,"You are able to eat again.");
					$BonusState[%clientId, %i] = "";
					playSound(BonusStateExpire, GameBase::getPosition(%clientId));
                    refreshHPREGEN(%clientId);
                    refreshStaminaREGEN(%clientId);
                    refreshAll(%clientId,false);
				}
				else
				{
					%totalbcnt++;
					if($BonusState[%clientId, %i] != "Jail" && $BonusState[%clientId, %i] != "Theft")
						%truebcnt++;
				}
			}
		}

		if(%truebcnt > 0)
			storeData(%clientId, "isBonused", True);
		else
			storeData(%clientId, "isBonused", "");
			
	}
}

function GetAllBonusStatesTogether(%clientId)
{
    %statCnt = 0;
    for(%i = 1; %i <= $maxBonusStates; %i++)
	{
        if($BonusStateCnt[%clientId, %i] > 0)
		{
            %stat = String::getWord($BonusState[%clientId, %i]," ",0);
            echo(%stat);
            if(%stat != " ")
            {
                %amt = String::getWord($BonusState[%clientId, %i]," ",1);
                if(%bonus[%stat] == "")
                {
                    %bonus[%stat] = %amt;
                    %bonusStat[%statCnt] = %stat;
                    %statCnt++;
                }
                else
                {
                    %bonus[%stat] += %amt;
                }
            }
        }
    }
    
    %msg = "";
    if(%statCnt > 0)
    {
        for(%i = 0; %i < %statCnt; %i++)
        {
            %stat = %bonusStat[%i];
            if(!$BonusStateNegative[%stat])
                %msg = %msg @ %stat @ " " @ %bonus[%stat] @",";
        }
        return %msg;
    }
    return -1;
}

function GetBonusStateTicks(%clientId,%filter)
{
    %add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++)
	{
        if($BonusStateCnt[%clientId, %i] > 0)
		{
            %substr = $BonusState[%clientId, %i];
            %wx = Word::FindWord(%substr,%filter);
            if(%wx != -1)
            {
                return $BonusStateCnt[%clientId, %i];
            }
        }
    }
}

function AddBonusStatePoints(%clientId, %filter)
{
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++)
	{
		if($BonusStateCnt[%clientId, %i] > 0)
		{
            %substr = $BonusState[%clientId, %i];
            %wx = Word::FindWord(%substr,%filter);
            while(%wx != -1)
            {
                %add += getWord(%substr,%wx+1);
                
                %substr = Word::getSubWord(%substr,%wx+2,9999);
                %wx = Word::FindWord(%substr,%filter);
            }
        
			//for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2)
			//{
			//	%p2 = GetWord($BonusState[%clientId, %i], %z+1);
			//	if(String::ICompare(%p1, %filter) == 0)
			//	{
			//		//same filter
			//		%add += %p2;
			//	}
			//}
		}
	}

	return %add;
}

function UpdateBonusState(%clientId, %type, %ticks)
{
    echo("Bonus: "@%type@" "@ %ticks);
	//look thru the current bonus states and attempt to update
	%flag = False;
	for(%i = 1; %i <= $maxBonusStates; %i++)
	{
		if($BonusStateCnt[%clientId, %i] > 0)
		{
			if(String::ICompare($BonusState[%clientId, %i], %type) == 0)
			{
				$BonusStateCnt[%clientId, %i] = %ticks;
				%flag = True;
			}
		}
	}

	if(!%flag)
	{
		//couldn't find a current entry to update, so make a new entry
		for(%i = 1; %i <= $maxBonusStates; %i++)
		{
			if( !($BonusStateCnt[%clientId, %i] > 0) )
			{
				$BonusState[%clientId, %i] = %type;
				$BonusStateCnt[%clientId, %i] = %ticks;

				return True;
			}
		}
	}

	return %flag;
}