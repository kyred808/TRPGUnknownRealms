
deleteVariables("Ability::*");

// Abilities
$SkillRestriction["manaflare"] = $MinLevel @" 5 C Mage";
$SkillRestriction["magebolt"] = $MinLevel @" 15 C Mage"; //G Wizard";


$SkillRestriction["rage"] = $MinLevel @" 5 G Fighter";
$SkillRestriction["bladebolt"] = $MinLevel @" 15 C Fighter";
$SkillRestriction["secondwind"] = $MinLevel @" 30 C Fighter";

$Ability::keyword[0] = "magebolt";
$Ability::index[magebolt] = 0;
$Ability::baseCooldown[0] = 15;
$Ability::SoundId[0] = UnravelAM;
$Ability::cost[0,Mana] = 20;
$Ability::cost[0,Stam] = 5;
$Ability::cost[0,Item] = "";
$SkillType[magebolt] = $SkillOffensiveCasting;

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

$Ability::keyword[3] = "rage";
$Ability::index[rage] = 3;
$Ability::baseCooldown[3] = 15;
$Ability::cooldownTicks[3] = 250;
$Ability::ticks[3] = 100;
$Ability::SoundId[3] = debrisSmallExplosion;
$Ability::cost[3,Mana] = 20;
$Ability::cost[3,Stam] = 20;
$Ability::cost[3,Item] = "";

$Ability::keyword[4] = "manaflare";
$Ability::index[manaflare] = 4;
$Ability::baseCooldown[4] = 15;
$Ability::cooldownTicks[4] = 300;
$Ability::ticks[4] = 60;
$Ability::SoundId[4] = ActivateTD;
$Ability::cost[4,Mana] = -20;
$Ability::cost[4,Stam] = 40;
$Ability::cost[4,Item] = "";
$SkillType[manaflare] = $SkillOffensiveCasting;

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
                    if(AddBonusStatePoints(%clientId, "SecondWindCD") == 0)
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
                    if(AddBonusStatePoints(%clientId, "RageCD") == 0)
                    {
                        UpdateBonusState(%clientId,"RageCD 1",$Ability::cooldownTicks[3]);
                        playSound($Ability::SoundId[$Ability::index[rage]],Gamebase::getPosition(%clientId));
                        Ability::DoRageBurst(%clientId);
                        Ability::DoAbilityCost(%clientId,$Ability::index[rage]);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"RageCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 4)
                {
                    if(AddBonusStatePoints(%clientId, "ManaFlareCD") == 0)
                    {
                        UpdateBonusState(%clientId,"ManaFlareCD 1",$Ability::cooldownTicks[4]);
                        playSound($Ability::SoundId[$Ability::index[manaflare]],Gamebase::getPosition(%clientId));
                        Ability::DoManaFlare(%clientId);
                        Ability::DoAbilityCost(%clientId,$Ability::index[manaflare]);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"ManaFlareCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
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
$Ability::RageRange = 15;
$Ability::RageBurstForce = 300;
function Ability::DoRageBurst(%clientId)
{
    %set = newObject("set", SimSet);
    %range = 2*$Ability::RageRange;
    UpdateBonusState(%clientId, "ATK 20", $Ability::ticks[3]);
    %force = $Ability::RageBurstForce + fetchData(%clientId,"ATK");
    %n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %range, %range, %range, 0);
    
    Group::iterateRecursive(%set, Ability::onBurstHit, %clientId,3,$SlamDamageType,%force,Player::getMountedItem(%clientId,$WeaponSlot));
    deleteObject(%set);
}

$Ability::MFRange = 15;
$Ability::MFForce = 450;
function Ability::DoManaFlare(%clientId)
{
    %set = newObject("set", SimSet);
    %range = 2*$Ability::MFRange;
    %n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %range, %range, %range, 0);
    UpdateBonusState(%clientId, "SDM 20", $Ability::ticks[4]);
    Group::iterateRecursive(%set, Ability::onBurstHit,%clientId,4,$SpellDamageType,$Ability::MFForce,$Ability::keyword[4],CalculatePlayerSkill(%clientId,$SkillOffensiveCasting)/50);
    deleteObject(%set);
}

function Ability::onBurstHit(%player,%clientId,%idx,%dmgType,%forceScale,%weap,%dmg)
{
    if(Player::getClient(%player) != %clientId)
    {
        %clPos = Gamebase::getPosition(%clientId);
        %plPos = Vector::add(Gamebase::getPosition(%player),"0 0 3"); //So there's an upward factor
        %dir = Vector::Normalize(Vector::sub(%plPs,%clPos));
        Gamebase::virtual(%player,"onDamage",%dmgType,%dmg,"0 0 0","0 0 0",ScaleVector(%dir,%forceScale),"torso","",%clientId,%weap);
        if(%idx == $Ability::index[rage])
        {
            UpdateBonusState(Player::getClient(%player), "DEF -150", $Ability::ticks[%idx]);
        }
        else if(%idx == $Ability::index[manaflare])
        {
            UpdateBonusState(Player::getClient(%player), "MDEF -150", $Ability::ticks[%idx]);
        }
    }
}


