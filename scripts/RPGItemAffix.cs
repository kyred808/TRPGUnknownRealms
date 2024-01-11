$RPGItem::affixLen = 2;
$RPGItem::AffixCount = 0;
function RPGItem::addAffixType(%tag)
{
    $RPGItem::AffixType[$RPGItem::AffixCount] = %tag;
    $RPGItem::AffixCount++;
}

//%itemTag - current full item tag
//%affixType - affix without the numbers.  Ex: at im etc
//%affixNum - number to be attached to affix
//%special - "inc" or "dec".  If blank, then replace
function RPGItem::setItemAffix(%itemTag,%affixType,%amt,%special)
{
    %tag = "";
    %bFound = false;
    for(%i = 0; (%w = String::getWord(%itemTag,"_",%i)) != "_"; %i++)
    {
        if(%i == 0)
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
            %tag = %tag @"_"@ %w;
    }
    
    if(!%bFound)
    {
        %new = RPGItem::UpdateAffixValue(0,%amt,%special);
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
    
    if(%current != 0)
        return %current;
    else
        return "";
}

function RPGItem::ParseAffixes(%itemTag)
{
    for(%i = 1; (%w = String::getWord(%itemTag,"_",%i)) != "_"; %i++)
    {
        %affix = String::getSubStr(%w,0,$RPGItem::affixLen);
        %val = String::getSubStr(%w,$RPGItem::affixLen,9999);
        $ParseAffix[%affix] = %val;
    }
}

function RPGItem::getImprovementLevel(%itemTag)
{
    RPGItem::ParseData(%itemTag);
    return $RPGItemAffix::improvementLevel[%itemTag];
}

function RPGItem::ParseData(%itemTag)
{
    if(!$RPGItem::Cached[%itemTag])
    {
        deleteVariables("ParseAffix*");
        RPGItem::ParseAffixes(%itemTag);
        if($ParseAffix["im"] == "")
            $RPGItemAffix::improvementLevel[%itemTag] = 0;
        else
            $RPGItemAffix::improvementLevel[%itemTag] = $ParseAffix["im"];
        
        $RPGItem::Cached[%itemTag] = true;
    }
}

function RPGItem::getAffixValue(%itemTag,%affixType)
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