$RPGItem::affixLen = 2;
$RPGItem::AffixCount = 0;

//$ParseAffix[] gets used as a variable length return and parameter without strings

function RPGItem::addAffixType(%tag,%desc,%spec)
{
    $RPGItem::AffixType[$RPGItem::AffixCount] = %tag;
    $RPGItem::AffixIndex[%tag] = $RPGItem::AffixCount; //Might not be needed
    $RPGItem::AffixDesc[$RPGItem::AffixCount] = %desc;
    $RPGItem::AffixSpecialVar[$RPGItem::AffixCount] = %spec;
    $RPGItem::AffixCount++;
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
RPGItem::addAffixType("hr","HPRegen",$SpeicalVarHPRegen); //HPRegen
RPGItem::addAffixType("mr","MPRegen",$SpecialVarManaRegen); //MPRegen
RPGItem::addAffixType("ap","AMRP",$SpecialVarArmorPiercing); //Armor Pierce

$RPGItem::SpecialVarToAffix[$SpecialVarAMR] = "am";
$RPGItem::SpecialVarToAffix[$SpecialVarMDEF] = "md";
$RPGItem::SpecialVarToAffix[$SpecialVarHP] = "hp";
$RPGItem::SpecialVarToAffix[$SpecialVarMana] = "mp";
$RPGItem::SpecialVarToAffix[$SpecialVarATK] = "at";
$RPGItem::SpecialVarToAffix[$SpecialVarDEF] = "de";
$RPGItem::SpecialVarToAffix[$SpecialVarSPEED] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarMaxWeight] = "";
$RPGItem::SpecialVarToAffix[$SpeicalVarHPRegen] = "hr";
$RPGItem::SpecialVarToAffix[$SpecialVarManaRegen] = "mr";
$RPGItem::SpecialVarToAffix[$SpecialVarManaThief] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarManaHarvest] = "";
$RPGItem::SpecialVarToAffix[$SpecialVarArmorPiercing] = "ap";
$RPGItem::SpecialVarToAffix[$SpecialVarATKSpeed] = "sp";

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

//Unzip an itemTag into $ParseAffix[] array
function RPGItemAffix::ParseAffixes(%itemTag)
{
    deleteVariables("ParseAffix*");
    $ParseAffix["tagId"] = String::getWord(%itemTag,"_",0);
    for(%i = 1; (%w = String::getWord(%itemTag,"_",%i)) != "_"; %i++)
    {
        %affix = String::getSubStr(%w,0,$RPGItem::affixLen);
        %val = String::getSubStr(%w,$RPGItem::affixLen,9999);
        $ParseAffix[%affix] = %val;
    }
}

//Zip $ParseAffix[] array up into an itemTag
function RPGItemAffix::CreateTagFromParse()
{
    %newTag = $ParseAffix["tagId"];
    for(%i = 0; %i < $RPGItem::AffixCount; %i++)
    {
        %type = $RPGItem::AffixType[%i];
        if($ParseAffix[%type] != "" && $ParseAffix[%type] != 0)
        {
            %newTag = %newTag@"_"@%type@$ParseAffix[%type];
        }
    }
    return %newTag;
}

function RPGItem::ParseAffixData(%itemTag)
{
    if(!$RPGItem::Cached[%itemTag])
    {
        RPGItemAffix::ParseAffixes(%itemTag);
        
        for(%i = 0; %i < $RPGItem::AffixCount; %i++)
        {
            %type = $RPGItem::AffixType[%i];
            if($ParseAffix[%type] != "")
            {
                $RPGItemAffixCache[%itemTag,%type] = $ParseAffix[%type];
            }
        }
        
        $RPGItem::Cached[%itemTag] = true;
    }
}

//Transfer item cache into a $ParseAffix[] so we can manipulate the array and zip it up into a new tag.
function RPGItemAffix::ParseFromCache(%itemTag)
{
    if($RPGItem::Cached[%itemTag])
    {
        $ParseAffix["tagId"] = String::getWord(%itemTag,"_",0);
        for(%i = 0; %i < $RPGItem::AffixCount; %i++)
        {
            %type = $RPGItem::AffixType[%i];
            $ParseAffix[%type] = $RPGItemAffixCache[%itemTag,%type];
        }
    }
    else
        RPGItem::ParseAffixData(%itemTag);
}

//Debug function
function PrintAffixCache(%itemTag)
{
    if($RPGItem::Cached[%itemTag])
    {
        for(%i = 0; %i < $RPGItem::AffixCount; %i++)
        {
            %type = $RPGItem::AffixType[%i];
            if($RPGItemAffixCache[%itemTag,%type] == "")
                %val = "N/A";
            else
                %val = $RPGItemAffixCache[%itemTag,%type];
            //echo(%itemTag @" Affix: "@ %type @" - "@ %val);
        }
    }
    else
        echo("No Affix Cache for "@ %itemTag);
}

//Manipulates $ParseAffix[] globals and constructs new tag from them
//%itemTag - current full item tag
//%affixType - affix without the numbers.  Ex: at im 
//%affixNum - number to be attached to affix
//%special - "inc" or "dec".  If blank, then replace
function RPGItem::setItemAffix(%itemTag,%affixType,%amt,%special)
{
    //Future consideration.  Allow for bulk processing
    if($RPGItem::AffixIndex[%affixType] != "")
    {
        RPGItemAffix::ParseFromCache(%itemTag);
        if(%special == "")
            $ParseAffix[%affixType] = %amt;
        else if(%special == "inc")
            $ParseAffix[%affixType] += %amt;
        else if(%special == "dec")
            $ParseAffix[%affixType] -= %amt;
            
        return RPGItemAffix::CreateTagFromParse();
    }
    else
        echo("ERROR: Unsupported Affix Type: "@ %affixType);
}

function RPGItem::getImprovementLevel(%itemTag)
{
    return RPGItem::getAffixValue(%itemTag,"im");
    //RPGItem::ParseData(%itemTag);
    //return $RPGItemAffix::improvementLevel[%itemTag];
}

function RPGItem::getAffixValue(%itemTag,%affixType)
{
    RPGItem::ParseAffixData(%itemTag);
    %value = $RPGItemAffixCache[%itemTag,%affixType];
    if(%value == "")
        return 0;
    else
        return %value;
}

function RPGItem::hasAffixes(%itemTag)
{
    return String::findSubStr(%itemTag,"_") != -1;
}

//=========================
//No longer used functions
//=========================

//I'm not sure if this string manipulation method is faster or not.
//But using $ParseAffix globals was way easier to code and uses simpler loops
function RPGItem::setItemAffixOld(%itemTag,%affixType,%amt,%special)
{
    %tag = "";
    %bFound = false;
    for(%i = 0; (%w = String::getWord(%itemTag,"_",%i)) != "_"; %i++)
    {
        if(%i == 0) //id is always first
        {
            %tag = %w;
            continue;
        }

        //echo(Math::isInteger(String::right(%w,String::len(%w)-%affixLen)));
        if(String::findSubStr(%w,%affixType) == 0) // && Math::isInteger(String::right(%w,String::len(%w)-%affixLen)))
        {
            %bFound = true;
            if(%special == "")
                %currentAffixString = 0;
            else
                %currentAffixString = String::getSubStr(%w,$RPGItem::affixLen,9999);

            %new = RPGItem::UpdateAffixValue(%currentAffixString,%amt,%special);
            
            if(%new != "")
                %tag = %tag@"_"@%affixType@%new;
          
        }
        else
            %tag = %tag @"_"@ %w; //Might cause problems?  Keep testing
    }
    
    if(!%bFound)
    {
		//echo("Not found! "@ %amt);
        %new = RPGItem::UpdateAffixValue(0,%amt,%special);
		//echo("New: "@ %new);
        if(%new != "")
            return %tag @"_"@ %affixType @%new;
    }
    
    return %tag;
}

function RPGItem::UpdateAffixValue(%current,%amt,%special)
{
    if(%special == "")
    {
        %current = %amt;
    }
    else if(%special == "inc")
    {
        %current += %amt;
    }
    else if(%special == "dec")
    {
        %current -= %amt;
    }

    if(%current != 0 || !Math::isInteger(%current) )
        return %current;
    else
        return "";
}

function RPGItem::getAffixValueOld(%itemTag,%affixType)
{
    %aff = String::getSubStr(%itemTag,String::len(String::copyUntil(%itemTag,"_"))+1,9999);
    %idx = String::findSubStr(%aff,%affixType);
    if(%idx != -1)
    {
        %imm = String::getSubStr(%aff,%idx+$RPGItem::affixLen,9999);
        return String::copyUntil(%imm,"_");
    }
    else
        return 0;
}