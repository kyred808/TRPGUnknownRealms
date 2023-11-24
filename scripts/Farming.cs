$AccessoryVar[Grain, $MiscInfo] = "A handful of common seeds that can be cultivated and used to make bread. Can be planted with #plant";
$AccessoryVar[GobbieBerry, $MiscInfo] = "A handful of sour berries commonly loved by goblins. Can be planted with #plant";
$AccessoryVar[RedBerry, $MiscInfo] = "A handful of red berries with stamina restoring properties. Can be planted with #plant";
$AccessoryVar[Yuccavera, $MiscInfo] = "A plant with medicinal properties. Can be planted with #plant";
$AccessoryVar[TreeFruit, $MiscInfo] = "A hard shelled fruit that grows on trees. Can be planted with #plant";
$AccessoryVar[Strawberry, $MiscInfo] = "A handful of sweet stawberries. Can be planted with #plant";
$AccessoryVar[Nixphyllum, $MiscInfo] = "A leafy plant with poisonous properties. Can be planted with #plant";
$AccessoryVar[DeezNuts, $MiscInfo] = "Rare mysterious nuts of strange origin that grow in pairs. Contain magical properties. Can be planted with #plant";
$AccessoryVar[Lunabrosia, $MiscInfo] = "A rare magical plant that glows in moonlight. Can be planted with #plant";

// Not a typical plant
$SkillRestriction["#plant treefruit"] = $SkillFarming @" 5";

$SkillRestriction["#plant grain"] = $SkillFarming @" 5";
$SkillRestriction["#plant gobbieberry"] = $SkillFarming @" 30";
$SkillRestriction["#plant redberry"] = $SkillFarming @" 120";
$SkillRestriction["#plant yuccavera"] = $SkillFarming @" 350";
$SkillRestriction["#plant strawberry"] = $SkillFarming @" 500";
$SkillRestriction["#plant nixphyllum"] =  $SkillFarming @" 700";
$SkillRestriction["#plant deeznuts"] = $SkillFarming @" 950 R 1";
$SkillRestriction["#plant lunabrosia"] = $SkillFarming @" 1200 R 2";

$FarmingItems = "grain,treefruit,gobbieberry,yuccavera,strawberry,nixphyllum,deeznuts,lunabrosia";

$Farming::PlantShape[Grain] = PlantedPlantTwo;
$Farming::PlantShape[GobbieBerry] = PlantedPlantTwo;
$Farming::PlantShape[RedBerry] = PlantedPlantTwo;
$Farming::PlantShape[Yuccavera] = PlantedPlantTwo;
$Farming::PlantShape[Treefruit] = PlantedTreeShape;
$Farming::PlantShape[Strawberry] = PlantedPlantOne;
$Farming::PlantShape[Nixphyllum] = PlantedPlantOne;
$Farming::PlantShape[DeezNuts] = PlantedPlantOne;
$Farming::PlantShape[Lunabrosia] = PlantedPlantOne;

$Farming::MaxHarvestAmnt = 5;

$Farming::PlantBaseHarvestAmnt[Grain] = 3;
$Farming::PlantBaseHarvestAmnt[GobbieBerry] = 2;
$Farming::PlantBaseHarvestAmnt[RedBerry] = 2;
$Farming::PlantBaseHarvestAmnt[Yuccavera] = 2;
$Farming::PlantBaseHarvestAmnt[Treefruit] = 2;
$Farming::PlantBaseHarvestAmnt[Strawberry] = 2;
$Farming::PlantBaseHarvestAmnt[Nixphyllum] = 2;
$Farming::PlantBaseHarvestAmnt[DeezNuts] = 2;
$Farming::PlantBaseHarvestAmnt[Lunabrosia] = 2;

$Farming::BaseGrowTime[Grain] = 120;
$Farming::BaseGrowTime[GobbieBerry] = 150;
$Farming::BaseGrowTime[RedBerry] = 200;
$Farming::BaseGrowTime[Yuccavera] = 200;
$Farming::BaseGrowTime[Treefruit] = 300;
$Farming::BaseGrowTime[Strawberry] = 300;
$Farming::BaseGrowTime[Nixphyllum] = 300;
$Farming::BaseGrowTime[DeezNuts] = 500;
$Farming::BaseGrowTime[Lunabrosia] = 800;

$Farming::PlantLockTime = 3600;

// No trapping players in plants.  (Or also anvils)
function Farming::PlayerSpawnProtection(%pos)
{
    %set = newObject("set", SimSet);
    %n = containerBoxFillSet(%set, $StaticObjectType | $SimInteriorObjectType, %pos, 3, 3, 3, 0);
    echo("Spawn Area Check: "@ %n);
    %found = false;
    for(%i = 0; %i < %n; %i++)
    {
        %obj = Group::getObject(%set,%i);
        if(clipTrailingNumbers(Object::getName(%obj)) == "anvil")
        {
            %found = true;
            break;
            //deleteObject(%obj);
        }
        else
        {
            %type = GameBase::getDataName(%obj);
            if(%type == "PlantedSeed")
            {
                %found = true;
                //$Farming::GrowingTime[%obj] = "";
                //deleteObject(%obj);
                break;
            }
            else if(%type == "PlantedTreeShape" || %type == "PlantedPlantOne" || %type == "PlantedPlantTwo")
            {
                %found = true;
                //$Farming::PlantLock[%obj] = "";
                //deleteObject(%obj);
                break;
            }
        }
    }
    
    deleteObject(%set);
    if(%found)
        return Vector::add(%pos,"0 0 5");
    else
        return %pos;
}

// Assumes a LOS check already happened
function Farming::PlantCrop(%clientId,%crop)
{
    Client::SendMessage(%clientId,$MsgWhite,"You planted a "@ $beltitem[%crop, "Name"] @" plant.~wPku_hlth.wav");
    
    %skillRestrict = $SkillRestriction["#plant "@%crop];
    %skillReq = getWord(%skillRestrict,Word::findWord(%skillRestrict,$SkillFarming)+1);
    %pskill = CalculatePlayerSkill(%clientId,$SkillFarming);
    %mod = (80 + 40*getRandom())/100; //Random 80-120%
    %time = Cap($Farming::BaseGrowTime[%crop]*((1.2*%skillReq)/%pskill)*%mod,$Farming::BaseGrowTime[%crop]/2,"inf");
    echo("Grow Time: "@ %time @" "@%mod);
    %skillDiff = (%pskill - %skillReq);
    %amnt = Cap(floor(getRandomMT()*($Farming::PlantBaseHarvestAmnt[%crop]+floor(%skillDiff/(1.3*%skillReq)*getRandomMT()))),1,$Farming::MaxHarvestAmnt);
    echo(%amnt);
    %rot = Vector::add(Vector::getRotation($los::normal),"1.57 0 0");
    Farming::PlantSeedObj(%crop,"PlantedSeed",$los::position,%rot,%time,%amnt,Client::getName(%clientId));
}

function Farming::PlantSeedObj(%cropType,%seedObjType,%pos,%rot,%growTime,%amnt,%ownerName)
{
    %set = nameToId("MissionCleanup\\PlantedCrops");
    if(%set == -1)
    {
        %set = newObject("PlantedCrops",SimGroup);
        addToSet(nameToId("MissionCleanup"),%set);
    }
    
    %obj = newObject("Plant "@%cropType@" "@%ownerName,StaticShape,%seedObjType,true);
    addToSet(%set,%obj);
    Gamebase::setPosition(%obj,%pos);
    Gamebase::setRotation(%obj,%rot);
    schedule("Farming::GrowPant("@%obj@","@%ownerName@");",%growTime,%obj);
    $Farming::GrowingTime[%obj] = getSimTime() @ "," @ %growTime;
    $Farming::SeedHarvestAmnt[%obj] = %amnt;
}


function Farming::GrowPant(%object,%ownerName)
{
    
    $Farming::GrowingTime[%object] = "";
    %type = getWord(Object::getName(%object),1);
    
    %pos = Gamebase::getPosition(%object);
    %rot = Vector::add(Gamebase::getRotation(%object),"-1.57 0 0");
    Farming::PlantCropObj(%type,$Farming::PlantShape[%type],%pos,%rot,$Farming::SeedHarvestAmnt[%object],$Farming::PlantLockTime,%ownerName);
    $Farming::SeedHarvestAmnt[%object] = "";
    deleteObject(%object);
}

function Farming::PlantCropObj(%cropType,%cropObjType,%pos,%rot,%amnt,%lockTime,%ownerName)
{
    %set = nameToId("MissionCleanup\\PlantedCrops");
    if(%set == -1)
    {
        %set = newObject("PlantedCrops",SimGroup);
        addToSet(nameToId("MissionCleanup"),%set);
    }
    
    %name = %cropType;
    if(%ownerName != "")
        %name = %name @","@%ownerName;
    %nobj = newObject(%name,StaticShape,%cropObjType,true);
    Gamebase::setPosition(%nobj,%pos);
    Gamebase::setRotation(%nobj,%rot);
    addToSet(%set,%nobj);
    $Farming::harvestAmnt[%nobj] = %amnt;
    if(%lockTime != "")
        $Farming::PlantLock[%nobj] = %ownerName@","@getSimTime()@","@%lockTime;
    
}

function Farming::MassHarvest(%obj,%clientId)
{
    if($massharvestcnt < 15)
        schedule("Farming::HarvestPlantObject("@%obj@","@%clientId@");",0.2*$massharvestcnt,%obj);
    $massharvestcnt++;
}

function Farming::HarvestPlantObject(%obj,%clientId)
{
    %type = GameBase::getDataName(%obj);
    if (%type == "PlantedPlantOne" || %type == "PlantedPlantTwo")
    {
        %objName = Object::getName(%obj);
        %cropType = String::getWord(%objName,",",0);
        if(Word::findWord($FarmingItems,%cropType,",") != -1)
        {
            %skillRestrict = $SkillRestriction["#plant "@%cropType];
            %skillReq = getWord(%skillRestrict,Word::findWord(%skillRestrict,$SkillFarming)+1);
            %pskill = CalculatePlayerSkill(%clientId,$SkillFarming);
            if (%pskill >= %skillReq || %clientId.adminlevel > 4)
            {
                %amnt = $Farming::harvestAmnt[%obj];
                if(%amnt > 0)
                {
                    Client::SendMessage(%clientId,0,"You harvested " @ %amnt @ " " @ %cropType @ ".");
                    GiveThisStuff(%clientId,%cropType@" "@%amnt);
                    //Need to rework so the difficulty gets harder if you are harvesting easy plants
                    UseSkill(%clientId, $SkillFarming, True, True, 8/%amnt);
                }
                else
                {
                    Client::SendMessage(%clientId,$MsgWhite,"You fail to find anything.");
                    UseSkill(%clientId, $SkillFarming, True, True, 35);
                }
                Farming::deleteCrop(%obj);
            }
        }
    }
}

function Farming::deleteCrop(%cropObj)
{
    $Farming::harvestAmnt[%cropObj] = "";
    $Farming::PlantLock[%cropObj] = "";
    deleteObject(%cropObj);
}

function Farming::LoadWorld()
{
    %filename = $missionName @ "_farmingsave_.cs";
	if(isFile("temp\\" @ %filename))
	{
        messageAll(2, "Player Farm loading in progress...");
        
        $ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;
        exec(%filename);
        
        for(%i = 0; %i < $FarmingWorldSave::seedCount; %i++)
        {
            Farming::LoadPlantedSeed(%i);
        }
        
        for(%i = 0; %i < $FarmingWorldSave::cropCount; %i++)
        {
            Farming::LoadPlantedCrop(%i);
        }
        
        echo("Farm Load complete.");
        messageAll(2, "Player Farm loading complete.");
    }
    
}

function Farming::SaveWorld()
{
    %set = nameToId("MissionCleanup\\PlantedCrops");
    if(%set != -1)
    {
        deletevariables("FarmingWorldSave::*");
        $FarmingWorldSave::seedCount = 0;
        $FarmingWorldSave::cropCount = 0;
        Group::iterateRecursive(%set,"Farming::SavePlantData");
        
        File::delete("temp\\" @ $missionName @ "_farmingsave_.cs");
        export("FarmingWorldSave::*", "temp\\" @ $missionName @ "_farmingsave_.cs", false);
        messageAll(2, "Save Player Farm Data complete.");
    }
    
    
}

function Farming::SavePlantData(%plant)
{
    %type = GameBase::getDataName(%plant);
    
    if(%type == "PlantedSeed")
    {
        %plantingTime = String::getWord($Farming::GrowingTime[%plant],",",0);
        %growTime = String::getWord($Farming::GrowingTime[%plant],",",1);
        %diff = (%plantingTime + %growTime) - getSimTime();
        %name = Object::getName(%plant);
        %plantType = getWord(%name,1);
        %owner = getWord(%name,2);
        if(%diff < %growTime)
        {
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,ObjType] = %type;
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,Type] = %plantType;
            if(%owner != -1)
                $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,Owner] = %owner;
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,TimeRemaing] = %diff;
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,Pos] = Gamebase::getPosition(%plant);
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,Rot] = Gamebase::getRotation(%plant);
            $FarmingWorldSave::seed[$FarmingWorldSave::seedCount,HarvestAmnt] = $Farming::SeedHarvestAmnt[%plant];
            $FarmingWorldSave::seedCount++;
        }
        else
        {
            echo("A plant was not cleaned up right...or the save had bad timing");
        }
    }
    else if(%type == "PlantedTreeShape" || %type == "PlantedPlantOne" || %type == "PlantedPlantTwo")
    {
        %name = Object::getName(%plant);
        %plantType = String::getWord(%name,",",0);
        %owner = String::getWord(%name,",",1);
        
        $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,ObjType] = %type;
        $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Type] = %plantType;
        if(%owner != -1)
            $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Owner] = %owner;
        $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Pos] = Gamebase::getPosition(%plant);
        $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Rot] = Gamebase::getRotation(%plant);
        $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,HarvestAmnt] = $Farming::harvestAmnt[%plant];
        %lock = $Farming::PlantLock[%plant];
        if(%lock != "")
        {
            %lockingTime = String::getWord(%lock,",",1);
            %maxTime = String::getWord(%lock,",",2);
            %diff = (%maxTime + %lockingTime) - getSimTime();
            if(%diff < %maxTime)
            {
                %owner = String::getWord(%lock,",",0);
                $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Lock,Owner] = %owner;
                $FarmingWorldSave::crop[$FarmingWorldSave::cropCount,Lock,TimeRemaining] = %diff;
                
            }
            else
                $Farming::PlantLock[%plant] = ""; //Clear the lock, as it should have been clear.
        }
        $FarmingWorldSave::cropCount++;
    }
}

function Farming::LoadPlantedCrop(%index)
{
    %type = $FarmingWorldSave::crop[%index,Type];
    %objType = $FarmingWorldSave::crop[%index,ObjType];
    %pos = $FarmingWorldSave::crop[%index,Pos];
    %rot = $FarmingWorldSave::crop[%index,Rot];
    %lockTime = $FarmingWorldSave::crop[%index,Lock,TimeRemaining];
    %ownerName = $FarmingWorldSave::crop[%index,Lock,Owner];
    %amnt = $FarmingWorldSave::crop[%index,HarvestAmnt];
    
    Farming::PlantCropObj(%type,%objType,%pos,%rot,%amnt,%lockTime,%ownerName);
}

function Farming::LoadPlantedSeed(%index)
{
    %cropType = $FarmingWorldSave::seed[%index,Type];
    %objType = $FarmingWorldSave::seed[%index,ObjType];
    %pos = $FarmingWorldSave::seed[%index,Pos];
    %rot = $FarmingWorldSave::seed[%index,Rot];
    %time = $FarmingWorldSave::seed[%index,TimeRemaing];
    %owner = $FarmingWorldSave::seed[%index,Owner];
    %amnt = $FarmingWorldSave::seed[%index,HarvestAmnt];
    
    Farming::PlantSeedObj(%cropType,%objType,%pos,%rot,%time,%amnt,%owner);
}