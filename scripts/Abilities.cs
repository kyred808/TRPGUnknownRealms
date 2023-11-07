
deleteVariables("Ability::*");

// Abilities
$SkillRestriction["magebolt"] = $MinLevel @" 15 C Mage"; //G Wizard";

$SkillRestriction["slam"] = $MinLevel @" 20 G Warrior";
$SkillRestriction["bladebolt"] = $MinLevel @" 15 C Fighter";
$SkillRestriction["secondwind"] = $MinLevel @" 30 C Fighter";


$Ability::keyword[0] = "magebolt";
$Ability::index[magebolt] = 0;
$Ability::baseCooldown[0] = 15;
$Ability::SoundId[0] = UnravelAM;
$Ability::cost[0,Mana] = 20;
$Ability::cost[0,Stam] = 5;
$Ability::cost[0,Item] = "";

$Ability::keyword[1] = "bladebolt";
$Ability::index[bladebolt] = 1;
$Ability::baseCooldown[1] = 15;
$Ability::SoundId[1] = LaunchFB;
$Ability::cost[1,Mana] = 15;
$Ability::cost[1,Stam] = 0;
$Ability::cost[1,Item] = "";

$Ability::keyword[2] = "secondwind";
$Ability::index[secondwind] = 2;
$Ability::baseCooldown[2] = 15;
$Ability::SoundId[2] = ActivateTD;
$Ability::cost[2,Mana] = 30;
$Ability::cost[2,Stam] = -100;
$Ability::cost[2,Item] = "";

$Ability::keyword[3] = "slam";
$Ability::index[slam] = 3;
$Ability::baseCooldown[3] = 15;
$Ability::SoundId[3] = ActivateAB;
$Ability::cost[3,Mana] = 5;
$Ability::cost[3,Stam] = 20;
$Ability::cost[3,Item] = "";

//$AbilityCost["#bladebolt"] = "Mana 15";


function Player::useAbility(%clientId,%ability)
{
    %idx = $Ability::index[%ability];
    if(%idx != "")
    {
        if(SkillCanUse(%clientId, %ability))
        {
            if(Ability::CheckCost(%clientId,%idx))
            {
                if(%idx == 0)
                {
                    Ability::DoMageBolt(%clientId);
                }
                else if(%idx == 1)
                {
                    Ability::DoBladeBolt(%clientId);
                }
                else if(%idx == 2)
                {
                    if(AddBonusStatePoints(%clientId, "SecondWindCD 1") == 0)
                    {
                        UpdateBonusState(%clientId,"SecondWindCD 1",250);
                        playSound($Ability::SoundId[$Ability::index[secondwind]],Gamebase::getPosition(%clientId));
                        Client::sendMessage(%clientId, $MsgWhite, "You regain your energy!");
                        Ability::DoAbilityCost(%clientId,$Ability::index[secondwind]);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"SecondWindCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 3)
                {
                    Ability::DoSlam(%clientId);
                }
            }
            else
                Client::sendMessage(%clientId, $MsgWhite, $AbilityFailReason);
        }
        else
            Client::sendMessage(%clientId, $MsgRed, "You lack the requirements to use that ability.");
    }
    else
        Client::sendMessage(%clientId, $MsgWhite, "Ability "@%cropped@" does not exist.");
}

function Ability::DoAbilityCost(%clientId,%idx)
{
    if($Ability::cost[%idx,Mana])
        refreshMANA(%clientId,$Ability::cost[%idx,Mana]);

    if($Ability::cost[%idx,Stam])
        refreshStamina(%clientId,$Ability::cost[%idx,Stam]);
        
    if($Ability::cost[%idx,Item])
        TakeThisStuff(%clientId,$Ability::cost[%idx,Item]);
}

function Ability::CheckCost(%clientId,%idx)
{
    $AbilityFailReason = "";
    %flag = true;
    if($Ability::cost[%idx,Mana])
    {
        %mana = fetchData(%clientId,"MANA");
        if(%mana < $Ability::cost[%idx,Mana])
            %flag = false;
    }
    
    if(!%flag)
    {
        $AbilityFailReason = "You do not have enough mana ("@$Ability::cost[%idx,Mana]@")";
        return false;
    }
    
    if($Ability::cost[%idx,Stam])
    {
        %stam = fetchData(%clientId,"Stamina");
        if(%stam < $Ability::cost[%idx,Stam])
            %flag = false;
    }
    
    if(!%flag)
    {
        $AbilityFailReason = "You do not have enough stamina ("@$Ability::cost[%idx,Stam]@")";
        return false;
    }
    
    if($Ability::cost[%idx,Item])
    {
        if(!HasThisStuff(%clientId,$Ability::cost[%idx,Item]))
            %flag = false;
    }
    
    if(!%flag)
    {
        $AbilityFailReason = "You lack the necessary items ("@$Ability::cost[%idx,Item]@")";
        return false;
    }
    
    return true;
}

function Ability::DoMageBolt(%clientId)
{
    %timeLeft = fetchData(%clientId,"blockMageBolt");
    if(%timeLeft == "")
    {
        %trans = Gamebase::getEyeTransform(%clientId);
        %player = Client::getOwnedObject(%clientId);
        %vel = Item::getVelocity(%player);
        Ability::DoAbilityCost(%clientId,$Ability::index[magebolt]);
        
        playSound($Ability::SoundId[$Ability::index[magebolt]],Gamebase::getPosition(%clientId));
        
        //Projectile::spawnProjectile(MageBoltMain,%trans,%player,%vel);
        Projectile::spawnProjectile(MageBoltTail,%trans,%player,%vel);
        
        storeData(%clientId,"blockMageBolt",getSimTime() + 3);
        schedule("storeData("@%clientId@",\"blockMageBolt\",\"\");",3);
    }
    else
    {
        %time = Number::Beautify(%timeLeft - getSimTime(),0,2);
        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%time@"s)");
    }
}

$SlamSpeedFactor = 200;

function Ability::DoSlam(%clientId)
{
    %timeLeft = fetchData(%clientId,"blockSlam");
    if(%timeLeft == "")
    {
        %player = Client::getOwnedObject(%clientId);
        %trans = Gamebase::getEyeTransform(%clientId);
        
        %dir = Word::getSubWord(%trans,3,3);
        %imp = ScaleVector(%dir,$SlamSpeedFactor*CalculatePlayerSkill(%clientid,$SkillEndurance)/50);
        Ability::DoAbilityCost(%clientId,$Ability::index[slam]);
        playSound($Ability::SoundId[$Ability::index[slam]],Gamebase::getPosition(%clientId));
        storeData(%clientId,"blockSlam",getSimTime() + 15);
        schedule("storeData("@%clientId@",\"blockSlam\",\"\");",15);
        Player::applyImpulse(%player, %imp);
        storeData(%clientId,"doingSlam",true);
        schedule("storeData("@%clientId@",\"doingSlam\",\"\");",3);
    }
    else
    {
        %time = Number::Beautify(%timeLeft - getSimTime(),0,2);
        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%time@"s)");
    }
}

function Ability::DoBladeBolt(%clientId)
{
    %timeLeft = fetchData(%clientId,"blockBladeBolt");
    if(%timeLeft == "")
    {
        %player = Client::getOwnedObject(%clientId);
        if(Player::getMountedItem(%player,$WeaponSlot) != "")
        {
            %trans = Gamebase::getEyeTransform(%clientId);
        
            %vel = Item::getVelocity(%player);
            Ability::DoAbilityCost(%clientId,$Ability::index[bladebolt]);
            
            playSound($Ability::SoundId[$Ability::index[bladebolt]],Gamebase::getPosition(%clientId));
            
            Projectile::spawnProjectile(BladeBoltTail,%trans,%player,%vel);
            
            storeData(%clientId,"blockBladeBolt",getSimTime() + 3);
            schedule("storeData("@%clientId@",\"blockBladeBolt\",\"\");",3);
        }
        else
            Client::sendMessage(%clientId, $MsgWhite, "You need to have a weapon equipped.");
        
    }
    else
    {
        %time = Number::Beautify(%timeLeft - getSimTime(),0,2);
        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%time@"s)");
    }
}