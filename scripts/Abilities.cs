
deleteVariables("Ability::*");

// Abilities
$SkillRestriction["manaflare"] = $MinLevel @" 5 C Mage";
$SkillRestriction["magebolt"] = $MinLevel @" 15 C Mage"; //G Wizard";


$SkillRestriction["rage"] = $MinLevel @" 5 C Fighter";
$SkillRestriction["bladebolt"] = $MinLevel @" 15 C Fighter";
$SkillRestriction["secondwind"] = $MinLevel @" 30 C Fighter";

$SkillRestriction["fade"] = $MinLevel @" 15 G Rogue";
$SkillRestriction["trueshot"] = $MinLevel @" 5 C Ranger";

$Ability::keyword[0] = "magebolt";
$Ability::name[0] = "Mage Bolt";
$Ability::description[0] = "A quick casting bolt of arcane energy.";
$Ability::index[magebolt] = 0;
$Ability::baseCooldown[0] = 15;
$Ability::SoundId[0] = UnravelAM;
$Ability::cost[0,Mana] = 20;
$Ability::cost[0,Stam] = 5;
$Ability::cost[0,Item] = "";
$SkillType[magebolt] = $SkillOffensiveCasting;

$Ability::keyword[1] = "bladebolt";
$Ability::index[bladebolt] = 1;
$Ability::name[1] = "Blade Bolt";
$Ability::description[1] = "Fires a bolt of energy from your weapon that does 75% ATK of your equipped weapon.";
$Ability::baseCooldown[1] = 15;
$Ability::SoundId[1] = LaunchFB;
$Ability::cost[1,Mana] = 15;
$Ability::cost[1,Stam] = 0;
$Ability::cost[1,Item] = "";

$Ability::keyword[2] = "secondwind";
$Ability::index[secondwind] = 2;
$Ability::name[2] = "Second Wind";
$Ability::description[2] = "Gather up a surge of energy, restoring 100 stamina.";
$Ability::baseCooldown[2] = 15;
$Ability::SoundId[2] = ActivateTD;
$Ability::cost[2,Mana] = 30;
$Ability::cost[2,Stam] = -100;
$Ability::cost[2,Item] = "";

$Ability::keyword[3] = "rage";
$Ability::index[rage] = 3;
$Ability::name[3] = "Rage";
$Ability::description[3] = "Let out a burst of rage, knocking your enemies back. Raises your ATK by 20 and lowers the DEF of enemies hit by 150.";
$Ability::baseCooldown[3] = 15;
$Ability::cooldownTicks[3] = 250;
$Ability::ticks[3] = 100;
$Ability::SoundId[3] = debrisSmallExplosion;
$Ability::cost[3,Mana] = 20;
$Ability::cost[3,Stam] = 20;
$Ability::cost[3,Item] = "";

$Ability::keyword[4] = "manaflare";
$Ability::index[manaflare] = 4;
$Ability::name[4] = "Mana Flare";
$Ability::description[4] = "Burst forth with arcane energy, knocking your enemies back.  Raises all your spell damage by 20 ATK and lowers teh MDEF of enemies hit by 150.";
$Ability::baseCooldown[4] = 15;
$Ability::cooldownTicks[4] = 300;
$Ability::ticks[4] = 60;
$Ability::SoundId[4] = ActivateTD;
$Ability::cost[4,Mana] = -20;
$Ability::cost[4,Stam] = 40;
$Ability::cost[4,Item] = "";
$SkillType[manaflare] = $SkillOffensiveCasting;

$Ability::keyword[5] = "trueshot";
$Ability::index[trueshot] = 5;
$Ability::name[5] = "Mana Flare";
$Ability::description[5] = "For 2 mins, your shots are launched at very high speed.  Your attacks also land with 15% armor piercing.";
$Ability::baseCooldown[5] = 15;
$Ability::cooldownTicks[5] = 300;
$Ability::ticks[5] = 60; // 2 mins
$Ability::SoundId[5] = ActivateTD;
$Ability::cost[5,Mana] = 15;
$Ability::cost[5,Stam] = 5;
$Ability::cost[5,Item] = "";

$Ability::keyword[6] = "fade";
$Ability::index[fade] = 6;
$Ability::name[6] = "Fade Slash";
$Ability::description[6] = "You slash your opponent and leap backwards into the shadows, disappearing and confusing your attackers.";
$Ability::baseCooldown[6] = 15;
$Ability::cooldownTicks[6] = 300;
$Ability::ticks[6] = 60; // 2 mins
$Ability::SoundId[6] = ActivateTD;
$Ability::cost[6,Mana] = 30;
$Ability::cost[6,Stam] = 10;
$Ability::cost[6,Item] = "";
$FadeNoUnhideTime = 10;
//$AbilityCost["#bladebolt"] = "Mana 15";

$FadeAttackJumpImpulse = -120;
$FadeAttackJumpForce = 20;
$FadeAttackTargetImpulseScale = -1.5;
$FadeAttackConfuseRange = 15;

$Ability::RageRange = 15;
$Ability::RageBurstForce = 300;

$Ability::MFRange = 15;
$Ability::MFForce = 450;

//Slam disabled for now
$SlamSpeedFactor = 200;

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
                else if(%idx == 5)
                {
                    if(AddBonusStatePoints(%clientId, "TrueShotCD") == 0)
                    {
                        UpdateBonusState(%clientId,"TrueShotCD 1",$Ability::cooldownTicks[5]);
                        playSound($Ability::SoundId[$Ability::index[trueshot]],Gamebase::getPosition(%clientId));
                        UpdateBonusState(%clientId,"TrueShot 1",$Ability::ticks[5]);
                        UpdateBonusState(%clientId,"AMRP 15",$Ability::ticks[5]);
                        Ability::DoAbilityCost(%clientId,$Ability::index[trueshot]);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"TrueShotCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 6)
                {
                    Ability::DoFadeAttack(%clientId);
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

function Ability::DoFadeAttack(%clientId)
{
    %timeLeft = fetchData(%clientId,"blockFade");
    if(%timeLeft == "")
    {
        if(fetchData(%clientId, "invisible"))
        {
            Client::sendMessage(%clientId, $MsgWhite, "You are already invisible.");
            return;
        }
        %player = Client::getOwnedObject(%clientId);
        $los::object = "";
        %los = Gamebase::getLOSInfo(%player,4);
        %weapon = Player::getMountedItem(%clientId,$WeaponSlot);
        if(%weapon != "")
        {
            if(%los && getObjectType($los::object) == "Player")
            {
                GameBase::virtual($los::object, "onDamage", "", 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, %weapon);
                %imp = Vector::getFromRot(Gamebase::getRotation(%clientId),$FadeAttackJumpImpulse,$FadeAttackJumpForce);
                Player::applyImpulse(%player,%imp);
                Player::applyImpulse($los::object,ScaleVector(%imp,$FadeAttackTargetImpulseScale));
                GameBase::startFadeOut(%clientId);
                Ability::DoAbilityCost(%clientId,$Ability::index[fade]);
				storeData(%clientId, "invisible", True);
                %delay = $FadeNoUnhideTime;
                %grace = Cap(CalculatePlayerSkill(%clientId, $SkillHiding) / 10, 5, 100);                
                schedule("WalkSlowInvisLoop("@%clientId@",5,"@%grace@");",%delay);
                
                %timeLeft = fetchData(%clientId,"blockFade",true);
                schedule("storeData("@%clientId@",\"blockFade\",\"\");",120);
                
                %range = 2*$FadeAttackConfuseRange;
                containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %range, %range, %range, 0);
                Group::iterateRecursive(%set, Ability::FadeConfusion, %clientId);
                
                Client::sendMessage(%clientId, $MsgWhite, "You fade away in the shadows. Your movement will not unhide you for "@ %delay @"s.");
            }
            else
                Client::sendMessage(%clientId, $MsgWhite, "No target is in range.");
        }
        else
            Client::sendMessage(%clientId, $MsgWhite, "You need a weapon equipped.");
    }
    else
    {
        %time = Number::Beautify(%timeLeft - getSimTime(),0,2);
        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%time@"s)");
    }
}

function Ability::FadeConfusion(%player,%clientId)
{
    %tgtClient = Player::getClient(%player);
    if(RPG::isAIControlled(%tgtClient) && !fetchData(%tgtClient,"customAiFlag"))
    {
        %aiTag = fetchData(%tgtClient,"BotInfoAiName");
        
        if(AI::GetTarget(%aiTag) == %clientId)
        {
            AI::SetScriptedTargets(%aiTag);
            schedule("AI::SetAutomaticTargets("@%aiTag@");",1);
        }
    }
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
    // Refactor to allow damaging in pvp
    if(Gamebase::getTeam(%player) != Gamebase::getTeam(%clientId))
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


