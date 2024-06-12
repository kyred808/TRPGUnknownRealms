$RPGItem::AffixCount = 0;
deleteVariables("RPGItemAffix::modifier*");

function RPGItemAffix::defineEquipModifier(%name,%modStr,%class)
{
    if($RPGItemAffix::modifier[%class,Count] == "")
        $RPGItemAffix::modifier[%class,Count] = 0;
        
    %index = $RPGItemAffix::modifier[%class,Count];
    
    $RPGItemAffix::modifier[%class,%index,Name] = %name;
    $RPGItemAffix::modifier[%class,NameToIndex,%name] = %index;
    
    for(%i = 0; (%mod = String::getWord(%modStr,",",%i)) != ","; %i++)
    {
        %w1 = getWord(%mod,0);
        %w2 = getWord(%mod,1);
        $RPGItemAffix::modifier[%class,%index,ModList] = $RPGItemAffix::modifier[%class,%index,ModList] @ %w1 @" ";
        if($RPGItem::AffixIndex[%w1] != "") //It's an affix modifier
        {
            $RPGItemAffix::modifier[%class,%index,%w1] = %w2;
        }
        else
        {
            //Other modifiers go here
        }
    }
    
    $RPGItemAffix::modifier[%class,Count]++;
}


//"am 5,at 10,at 20%"
//"am 5" grants a flat +5 to the AMR stat.  "at 10" grants a flat +10 to the ATK stat.
//"at 20%" means it looks at the item's native ATK stat and gives +20% given the affix bonus
//Note: attack speed already works on a percentage


function RPGItemAffix::ApplyEquipModifiersToItem(%itemTag,%name)
{
    RPGItemAffix::ParseFromCache(%itemTag);
    %class = RPGItem::GetItemGroupFromTag(%itemTag);
    %index = $RPGItemAffix::modifier[%class,NameToIndex,%name];
    for(%i = 0; %i < getWordCount($RPGItemAffix::modifier[%class,%index,ModList]); %i++)
    {
        %mod = getWord($RPGItemAffix::modifier[%class,%index,ModList],%i);
        //echo("MOD: "@ %mod);
        %val = $RPGItemAffix::modifier[%class,%index,%mod];
        //echo("VAL: "@ %val);
        if(String::right(%val,1) == "%")
        {
            %spec = $RPGItem::AffixSpecialVar[$RPGItem::AffixIndex[%mod]];
            %itemLabel = RPGItem::ItemTagToLabel(%itemTag);
            %vars = GetAccessoryVar(%itemLabel, $SpecialVar);
            //echo("VV: "@%vars);
       
            %k = Word::FindWord(%vars,%spec);
            //echo("K: "@ %k);
            %amt = getWord(%vars,%k+1);
            if(%mod == "va")
                %amt = GetItemCost(RPGItem::ItemTagToLabel(%itemTag),%itemTag);
            %amt = %amt * (1+%val/100) - %amt;
            $ParseAffix[%mod] += round(%amt);
        }
        else
        {
            $ParseAffix[%mod] += %val;
        }
    }
    
    $ParseAffix["pr"] = %name;
    
    return RPGItemAffix::CreateTagFromParse();
}

RPGItem::addAffixType("im","Rank"); //Improvement
RPGItem::addAffixType("sd","SpellDmg"); //Spell Damage
RPGItem::addAffixType("we","Weight"); //Weight
RPGItem::addAffixType("va","Value"); //Value
RPGItem::addAffixType("pr"); //Prefix
RPGItem::addAffixType("nn"); //Name

//Special Var related affixes
RPGItem::addAffixType("am","AMR",$SpecialVarAMR); //Armor (AMR)
RPGItem::addAffixType("md","MDEF",$SpecialVarMDEF); //MDEF
RPGItem::addAffixType("at","ATK",$SpecialVarATK); //Attack
RPGItem::addAffixType("de","DEF",$SpecialVarDEF); //Defense
RPGItem::addAffixType("sp","ATKSPD",$SpecialVarATKSpeed); //atk speed Debating this one as spec var
RPGItem::addAffixType("hp","HP",$SpecialVarHP); //MaxHP
RPGItem::addAffixType("mp","MP",$SpecialVarMana); //MaxMP
RPGItem::addAffixType("hr","HPRegen",$SpecialVarHPRegen); //HPRegen
RPGItem::addAffixType("mr","MPRegen",$SpecialVarManaRegen); //MPRegen

RPGItem::addAffixType("vs","VITScale",$SpecialVarVITScaling);
RPGItem::addAffixType("ms","MNDScale",$SpecialVarMNDScaling);
RPGItem::addAffixType("ss","STRScale",$SpecialVarSTRScaling);
RPGItem::addAffixType("ds","DEXScale",$SpecialVarDEXScaling);
RPGItem::addAffixType("is","INTScale",$SpecialVarINTScaling);
RPGItem::addAffixType("fs","FAIScale",$SpecialVarFAIScaling);

RPGItem::addAffixType("vt","VIT",$SpecialVarVIT);
RPGItem::addAffixType("mn","MND",$SpecialVarMND);
RPGItem::addAffixType("sr","STR",$SpecialVarSTR);
RPGItem::addAffixType("dx","DEX",$SpecialVarDEX);
RPGItem::addAffixType("it","INT",$SpecialVarINT);
RPGItem::addAffixType("fa","FAI",$SpecialVarFAI);

RPGItem::addAfficType("ba","BARRIER",$SpecialVarBAR);

$RPGItem::SpecialVarToAffix[$SpecialVarAMR] = "am";
$RPGItem::SpecialVarToAffix[$SpecialVarMDEF] = "md";
$RPGItem::SpecialVarToAffix[$SpecialVarHP] = "hp";
$RPGItem::SpecialVarToAffix[$SpecialVarMana] = "mp";
$RPGItem::SpecialVarToAffix[$SpecialVarATK] = "at";
$RPGItem::SpecialVarToAffix[$SpecialVarDEF] = "de";
$RPGItem::SpecialVarToAffix[$SpecialVarSPEED] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarMaxWeight] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarHPRegen] = "hr";
$RPGItem::SpecialVarToAffix[$SpecialVarManaRegen] = "mr";
$RPGItem::SpecialVarToAffix[$SpecialVarManaThief] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarManaHarvest] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarArmorPiercing] = "ap";
$RPGItem::SpecialVarToAffix[$SpecialVarATKSpeed] = "sp";

$RPGItem::SpecialVarToAffix[$SpecialVarVITScaling] = "vs";
$RPGItem::SpecialVarToAffix[$SpecialVarMNDScaling] = "ms";
$RPGItem::SpecialVarToAffix[$SpecialVarSTRScaling] = "ss";
$RPGItem::SpecialVarToAffix[$SpecialVarDEXScaling] = "ds";
$RPGItem::SpecialVarToAffix[$SpecialVarINTScaling] = "is";
$RPGItem::SpecialVarToAffix[$SpecialVarFAIScaling] = "fs";

$RPGItem::SpecialVarToAffix[$SpecialVarVIT] = "vt";
$RPGItem::SpecialVarToAffix[$SpecialVarMND] = "mn";
$RPGItem::SpecialVarToAffix[$SpecialVarSTR] = "sr";
$RPGItem::SpecialVarToAffix[$SpecialVarDEX] = "dx";
$RPGItem::SpecialVarToAffix[$SpecialVarINT] = "it";
$RPGItem::SpecialVarToAffix[$SpecialVarFAI] = "fa";

$RPGItem::SpecialVarToAffix[$SpecialVarBAR] = "ba";

RPGItemAffix::defineEquipModifier("Fast","sp 40,at -15%,va 10%",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Hard","de 50,va 5%",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Broken","at -40%,va -80%",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Vengeful","at 20%,va 25%,de -100,sp 25",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Warding","md 100,va 12%,ba 15",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Sharp","at 10,va 10%",$RPGItem::WeaponClass);
RPGItemAffix::defineEquipModifier("Gilded","va 25%",$RPGItem::WeaponClass);
//RPGItemAffix::defineEquipModifier("Old","va -70%,at -30%,we 50%,sp -50",$RPGItem::WeaponClass);

function GetAffixBonusText(%itemTag)
{
    RPGItemAffix::ParseFromCache(%itemTag);
    %txt = "";
    for(%i = 0; %i < $RPGItem::AffixCount; %i++)
    {
        %type = $RPGItem::AffixType[%i];
        if($ParseAffix[%type] != "" && $ParseAffix[%type] != 0 && !(%type == "pr" || %type == "nn" || %type == "im"))
        {
            %txt = %txt @"<f0>"@$RPGITem::AffixDesc[%i]@": ";
            %val = $ParseAffix[%type];
            %txtVal = %val;
            if(%type == "sp")
                %txtVal = %txtVal @"%";
            if(%val > 0)
                %txt  = %txt @"<f1>+"@%txtVal@" ";
            else
                %txt = %txt @"<f1>"@%txtVal@" ";
        }
    }
    return %txt;
}