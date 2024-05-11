
deleteVariables("Ability::*");

// Abilities
$SkillRestriction["manaflare"] = $MinLevel @" 5 C Mage";
$SkillRestriction["magebolt"] = $MinLevel @" 15 C Mage"; //G Wizard";

$SkillRestriction["brace"] = $MinLevel @" 10 C Paladin";

$SkillRestriction["rage"] = $MinLevel @" 5 C Fighter";
$SkillRestriction["bladebolt"] = $MinLevel @" 15 G Warrior";
$SkillRestriction["secondwind"] = $MinLevel @" 30 C Fighter";
$SkillRestriction["catsfeet"] = $MinLevel @" 5 G Rogue";
$SkillRestriction["doublestrike"] = $MinLevel @" 10 G Rogue";
$SkillRestriction["fade"] = $MinLevel @" 15 G Rogue";
$SkillRestriction["trueshot"] = $MinLevel @" 5 C Ranger";

$SkillRestriction["heavystrike"] = $MinLevel @" 5";
$SkillRestriction["empower"] = $MinLevel @" 1";

$Ability::keyword[0] = "magebolt";
$Ability::name[0] = "Mage Bolt";
$Ability::description[0] = "A quick casting bolt of arcane energy.";
$Ability::index[magebolt] = 0;
$Ability::cooldownTime[0] = 3;
$Ability::SoundId[0] = UnravelAM;
$Ability::cost[0,Mana] = 20;
$Ability::cost[0,TP] = 15;
$Ability::cost[0,Item] = "";
$SkillType[magebolt] = $SkillOffensiveCasting;

$Ability::keyword[1] = "bladebolt";
$Ability::index[bladebolt] = 1;
$Ability::name[1] = "Blade Bolt";
$Ability::description[1] = "Fires a bolt of energy from your weapon that does 75% ATK of your equipped weapon.";
$Ability::cooldownTime[1] = 3;
$Ability::SoundId[1] = LaunchFB;
$Ability::cost[1,Mana] = 0;
$Ability::cost[1,TP] = 20;
$Ability::cost[1,Item] = "";

$Ability::keyword[2] = "secondwind";
$Ability::index[secondwind] = 2;
$Ability::name[2] = "Second Wind";
$Ability::description[2] = "Gather up a surge of energy, restoring 50% health.";
$Ability::cooldownTicks[2] = 250;
$Ability::SoundId[2] = ActivateTD;
$Ability::cost[2,Mana] = 5;
$Ability::cost[2,TP] = 75;
$Ability::cost[2,Item] = "";

$Ability::keyword[3] = "rage";
$Ability::index[rage] = 3;
$Ability::name[3] = "Rage";
$Ability::description[3] = "Let out a burst of rage, knocking your enemies back. Raises your ATK by 5+LVL and lowers the DEF of enemies hit by the blast by 150.";
$Ability::cooldownTicks[3] = 250;
$Ability::ticks[3] = 100;
$Ability::SoundId[3] = debrisSmallExplosion;
$Ability::cost[3,Mana] = 15;
$Ability::cost[3,TP] = 90;
$Ability::cost[3,Item] = "";

$Ability::keyword[4] = "manaflare";
$Ability::index[manaflare] = 4;
$Ability::name[4] = "Mana Flare";
$Ability::description[4] = "Burst forth with arcane energy, knocking your enemies back.  Raises all your spell damage by 20 ATK and lowers teh MDEF of enemies hit by 150.  Restores 100 Mana.";
$Ability::cooldownTicks[4] = 300;
$Ability::ticks[4] = 60;
$Ability::SoundId[4] = ActivateTD;
$Ability::cost[4,Mana] = -100;
$Ability::cost[4,TP] = 60;
$Ability::cost[4,Item] = "";
$SkillType[manaflare] = $SkillOffensiveCasting;

$Ability::keyword[5] = "trueshot";
$Ability::index[trueshot] = 5;
$Ability::name[5] = "True Shot";
$Ability::description[5] = "For 2 mins, your shots are launched at very high speed.  Your attacks also land with 15% armor piercing.";
$Ability::cooldownTicks[5] = 300;
$Ability::ticks[5] = 60; // 2 mins
$Ability::SoundId[5] = ActivateTD;
$Ability::cost[5,Mana] = 15;
$Ability::cost[5,TP] = 30;
$Ability::cost[5,Item] = "";

$Ability::keyword[6] = "fade";
$Ability::index[fade] = 6;
$Ability::name[6] = "Fade Slash";
$Ability::description[6] = "You slash your opponent and leap backwards into the shadows, disappearing and confusing your attackers.";
$Ability::cooldownTime[6] = 120;
$Ability::ticks[6] = 60; // 2 mins
$Ability::SoundId[6] = ActivateTD;
$Ability::cost[6,Mana] = 30;
$Ability::cost[6,TP] = 30;
$Ability::cost[6,Item] = "";

$Ability::keyword[7] = "brace";
$Ability::index[brace] = 7;
$Ability::name[7] = "Brace";
$Ability::description[7] = "For 2mins, become a nearly immovable wall.  Raises DEF by 200+LVL and AMR by 5. Reduces impulses by 50%";
$Ability::cooldownTicks[7] = 300;
$Ability::ticks[7] = 60; // 2 mins
$Ability::SoundId[7] = ActivateTD;
$Ability::cost[7,Mana] = 30;
$Ability::cost[7,TP] = 30;
$Ability::cost[7,Item] = "";

$Ability::keyword[8] = "doublestrike";
$Ability::index[doublestrike] = 8;
$Ability::name[8] = "Double Strike";
$Ability::description[8] = "Hit twice for 30s. Stamina cost of attacks is also doubled to match. Only works with melee weapons.";
$Ability::cooldownTicks[8] = 60;
$Ability::ticks[8] = 15; // 30s
$Ability::SoundId[8] = ActivateTD;
$Ability::cost[8,Mana] = 5;
$Ability::cost[8,TP] = 45;
$Ability::cost[8,Item] = "";

$Ability::keyword[9] = "catsfeet";
$Ability::index[catsfeet] = 9;
$Ability::name[9] = "Cat's Feet";
$Ability::description[9] = "Negate fall damage for 1 minute.";
$Ability::cooldownTicks[9] = 300;
$Ability::ticks[9] = 30; // 60s
$Ability::SoundId[9] = ActivateTD;
$Ability::cost[9,Mana] = 25;
$Ability::cost[9,TP] = 0;
$Ability::cost[9,Item] = "";

$Ability::keyword[10] = "heavystrike";
$Ability::index[heavystrike] = 10;
$Ability::name[10] = "Heavy Strike";
$Ability::description[10] = "Your next swing with a melee attack swings hard. Hit 50% harder and reduce the target's AMR by 5 for 20 seconds.";
$Ability::cooldownTicks[10] = 30;
$Ability::ticks[10] = 10; // 20s
$Ability::SoundId[10] = ActivateTD;
$Ability::cost[10,Mana] = 0;
$Ability::cost[10,TP] = 20;
$Ability::cost[10,Item] = "";

$Ability::keyword[11] = "empower";
$Ability::index[empower] = 11;
$Ability::name[11] = "Empower";
$Ability::description[11] = "Your next time dealing damage will hit with +15 ATK and +2 Min Damage";
$Ability::cooldownTicks[11] = 1;
$Ability::ticks[11] = 10; // 20s
$Ability::SoundId[11] = ActivateTD;
$Ability::cost[11,Mana] = 0;
$Ability::cost[11,TP] = 15;
$Ability::cost[11,Item] = "";

$Ability::heavyStrikeForce = 150;
$HeavyStrikeUpForce = 20;

$FadeAttackJumpImpulse = 275;
$FadeAttackJumpForce = 75;
$FadeAttackTargetImpulseScale = -1.5;
$FadeAttackConfuseRange = 40;

$FadeNoUnhideTime = 10; // Seconds

$TrueShotMaxRange = 1000;

$Ability::RageRange = 15;
$Ability::RageBurstForce = 300;

$Ability::MFRange = 15;
$Ability::MFForce = 220;

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
                        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[secondwind]]);
                        UpdateBonusState(%clientId,"SecondWindCD 1",250);
                        playSound($Ability::SoundId[$Ability::index[secondwind]],Gamebase::getPosition(%clientId));
                        Client::sendMessage(%clientId, $MsgWhite, "You regain your health!");
                        Ability::DoAbilityCost(%clientId,$Ability::index[secondwind]);
                        %max = fetchData(%clientId, "MaxHP");
                        %amt = floor(%max / 2);
                        setHP(%clientId,fetchData(%clientId,"HP")+%amt);
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
                        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[trueshot]]);
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
                else if(%idx == 7)
                {
                    if(AddBonusStatePoints(%clientId, "BraceCD") == 0)
                    {
                        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[brace]]);
                        UpdateBonusState(%clientId,"BraceCD 1",$Ability::cooldownTicks[%idx]);
                        UpdateBonusState(%clientId,"DEF "@200 + fetchData(%clientId,"LVL"),$Ability::ticks[%idx]);
                        UpdateBonusState(%clientId,"AMR 5",$Ability::ticks[%idx]);
                        UpdateBonusState(%clientId,"Brace 50",$Ability::ticks[%idx]);
                        playSound($Ability::SoundId[%idx],Gamebase::getPosition(%clientId));
                        Ability::DoAbilityCost(%clientId,$Ability::index[brace]);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"BraceCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 8)
                {
                    if(AddBonusStatePoints(%clientId, "DoubleStrikeCD") == 0)
                    {
                        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[DoubleStrike]]);
                        UpdateBonusState(%clientId,"DoubleStrikeCD 1",$Ability::cooldownTicks[%idx]);
                        storeData(%clientId,"DoubleStrikeFlag",true);
                        storeData(%clientId,"DoubleStrikeTimeout",getSimTime()+(2*$Ability::ticks[%idx])); //More reliable than a scheduled clear.
                        playSound($Ability::SoundId[%idx],Gamebase::getPosition(%clientId));
                        Ability::DoAbilityCost(%clientId,%idx);
                        schedule("Client::sendMessage("@%clientId@","@$MsgRed@", \"Double Strike has worn off.\");",2*$Ability::ticks[%idx],%clientId);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"DoubleStrikeCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 9)
                {
                    if(AddBonusStatePoints(%clientId, "CatsFeetCD") == 0)
                    {
                        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[catsfeet]]);
                        UpdateBonusState(%clientId,"CatsFeetCD 1",$Ability::cooldownTicks[%idx]);
                        storeData(%clientId,"CatsFeetFlag",true);
                        storeData(%clientId,"CatsFeetTimeout",getSimTime()+(2*$Ability::ticks[%idx]));
                        playSound($Ability::SoundId[%idx],Gamebase::getPosition(%clientId));
                        Ability::DoAbilityCost(%clientId,%idx);
                        schedule("Client::sendMessage("@%clientId@","@$MsgRed@", \"Cat's Feet has worn off.\");",2*$Ability::ticks[%idx],%clientId);
                    }
                    else
                    {
                        %ticks = GetBonusStateTicks(%clientId,"CatsFeetCD 1");
                        Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                    }
                }
                else if(%idx == 10)
                {
                    if(fetchData(%clientId,"HeavyStrikeFlag") == "")
                    {
                        if(AddBonusStatePoints(%clientId, "HeavyStrikeCD") == 0)
                        {
                            Client::sendMessage(%clientId, $MsgBeige,"Next Swing "@ $Ability::name[$Ability::index[heavystrike]]);
                            UpdateBonusState(%clientId,"HeavyStrikeCD 1",$Ability::cooldownTicks[%idx]);
                            storeData(%clientId,"HeavyStrikeFlag",true);
                            playSound($Ability::SoundId[%idx],Gamebase::getPosition(%clientId));
                            Ability::DoAbilityCost(%clientId,%idx);
                        }
                        else
                        {
                            %ticks = GetBonusStateTicks(%clientId,"HeavyStrikeCD 1");
                            Client::sendMessage(%clientId, $MsgWhite, "That ability is still on cooldown. ("@%ticks*2@"s)");
                        }
                    }
                    else
                        Client::sendMessage(%clientId, $MsgWhite, "You are already prepared to Heavy Strike.");
                }
                else if(%idx == 11)
                {
                    if(fetchData(%clientId,"EmpowerFlag") == "")
                    {
                        Client::sendMessage(%clientId, $MsgBeige,"Next attack is Empowered!");
                        storeData(%clientId,"EmpowerFlag",true);
                        playSound($Ability::SoundId[%idx],Gamebase::getPosition(%clientId));
                        Ability::DoAbilityCost(%clientId,%idx);
                    }
                    else
                        Client::sendMessage(%clientId, $MsgWhite, "You are already Empowered.");
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

    //if($Ability::cost[%idx,Stam])
    //    refreshStamina(%clientId,$Ability::cost[%idx,Stam]);
    
    if($Ability::cost[%idx,TP])
        useTP(%clientId,$Ability::cost[%idx,TP]);
    
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
    
    if($Ability::cost[%idx,TP])
    {
        %stam = fetchData(%clientId,"TP");
        if(%stam < $Ability::cost[%idx,TP])
            %flag = false;
    }
    
    if(!%flag)
    {
        $AbilityFailReason = "You do not have enough TP ("@$Ability::cost[%idx,TP]@")";
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
                Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[fade]]);
                GameBase::virtual($los::object, "onDamage", "", 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, %weapon);
                %imp = Vector::getFromRot(Gamebase::getRotation(%clientId),$FadeAttackJumpImpulse,$FadeAttackJumpForce);
                //Player::applyImpulse(%player,%imp);
                //Player::applyImpulse($los::object,ScaleVector(%imp,$FadeAttackTargetImpulseScale));
                Player::applyImpulse($los::object,%imp);
                GameBase::startFadeOut(%clientId);
                Ability::DoAbilityCost(%clientId,$Ability::index[fade]);
				storeData(%clientId, "invisible", True);
                %delay = $FadeNoUnhideTime;
                %grace = Cap(CalculatePlayerSkill(%clientId, $SkillHiding) / 10, 5, 100);                
                schedule("WalkSlowInvisLoop("@%clientId@",5,"@%grace@");",%delay);
                
                %timeLeft = fetchData(%clientId,"blockFade");
                schedule("storeData("@%clientId@",\"blockFade\",\"\");",$Ability::cooldownTime[$Ability::index[fade]]);
                
                %range = 2*$FadeAttackConfuseRange;
                %set = newObject("set", SimSet);
                containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %range, %range, %range, 0);
                Group::iterateRecursive(%set, Ability::FadeConfusion, %clientId);
                deleteObject(%set);
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
            
        echo("CONFUSE! "@ %aiTag);
        AI::SetScriptedTargets(%aiTag);
        AI::setVar(%aiTag, SpotDist, 0);
        schedule("AI::SetAutomaticTargets("@%aiTag@");",1,%player);
        schedule("AI::SetSpotDist("@AI::getID(%aiTag)@");",$FadeNoUnhideTime,%player);
    }
}

function Ability::DoMageBolt(%clientId)
{
    %timeLeft = fetchData(%clientId,"blockMageBolt");
    if(%timeLeft == "")
    {
        Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[magebolt]]);
        %trans = Gamebase::getEyeTransform(%clientId);
        %player = Client::getOwnedObject(%clientId);
        %vel = Item::getVelocity(%player);
        Ability::DoAbilityCost(%clientId,$Ability::index[magebolt]);
        
        playSound($Ability::SoundId[$Ability::index[magebolt]],Gamebase::getPosition(%clientId));
        
        //Projectile::spawnProjectile(MageBoltMain,%trans,%player,%vel);
        Projectile::spawnProjectile(MageBoltTail,%trans,%player,%vel);
        
        storeData(%clientId,"blockMageBolt",getSimTime() + $Ability::cooldownTime[$Ability::index[magebolt]]);
        schedule("storeData("@%clientId@",\"blockMageBolt\",\"\");",$Ability::cooldownTime[$Ability::index[magebolt]]);
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
            Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[bladebolt]]);
            %trans = Gamebase::getEyeTransform(%clientId);
        
            %vel = Item::getVelocity(%player);
            Ability::DoAbilityCost(%clientId,$Ability::index[bladebolt]);
            
            playSound($Ability::SoundId[$Ability::index[bladebolt]],Gamebase::getPosition(%clientId));
            
            Projectile::spawnProjectile(BladeBoltTail,%trans,%player,%vel);
            
            storeData(%clientId,"blockBladeBolt",getSimTime() + $Ability::cooldownTime[$Ability::index[bladebolt]]);
            schedule("storeData("@%clientId@",\"blockBladeBolt\",\"\");",$Ability::cooldownTime[$Ability::index[bladebolt]]);
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
    Client::sendMessage(%clientId, $MsgBeige,"Using "@ $Ability::name[$Ability::index[rage]]);
    %set = newObject("set", SimSet);
    %range = 2*$Ability::RageRange;
    UpdateBonusState(%clientId, "ATK "@5+fetchData(%clientId,"LVL"), $Ability::ticks[3]);
    %force = $Ability::RageBurstForce + fetchData(%clientId,"ATK");
    %n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %range, %range, %range, 0);
    
    Group::iterateRecursive(%set, Ability::onBurstHit, %clientId,3,$SlamDamageType,%force,Player::getMountedItem(%clientId,$WeaponSlot));
    deleteObject(%set);
}


function Ability::DoManaFlare(%clientId)
{
    Client::sendMessage(%clientId, $MsgBeige, $Ability::name[$Ability::index[manaflare]]);
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


