$TerrainOffset = "-5120 -3072 0";
$MapExtent[0] = 6144;
$MapExtent[1] = 6144;


$MeteorMinimumTime = 25*60;//25 mins
$MeteorTimeVariance = 12*60;//+/-6mins

$MeteorStrikeRadius = 750;

$MeteorNextTime = "";

$MaxMeteorsAtOnce = 30;
$MaxMeteorCrystals = 35;
$MeteorCrystalCount = 0;
$MeteorCrystalLightTimeInSeconds = 120;

$MeteorCrystalData::MaxTicks = 2160; //3 hours in 5s ticks
$MeteorCrystalData::HitMax = 8;

//3 to 5 mins in 5 sec ticks
$MeteorStorm::MinTicks = 36;
$MeteorStorm::MaxTicks = 60;
$MeteorStorm::Ticks = 0;
$MeteorStormEvent = false;

function Mission::init()
{
	dbecho($dbechoMode, "Mission::init()");

	if($displayPingAndPL)
		setClientScoreHeading("Name\t\x50Zone\t\xBFLVL\t\xDFPing\t\xFFPL");
	else
		setClientScoreHeading("Name\t\x50Zone\t\xB2LVL\t\xD2Class\t\xFFPL");

	if(!$NoSpawn)
		AI::setupAI();

	//schedule("echo(\".--==< RecursiveWorld STARTED >==--.\");RecursiveWorld(1);", 60);
    ClearAllMeteorData();
    ClearAllMeteorCrystals();
    
	echo(".--==< RecursiveWorld STARTED >==--.");
	RecursiveWorld(5);
	RecursiveZone(2);
    
	if($phantomremoteevalfix == "")
	{
	echo("No hack fix set, using default.");
	$phantomremoteevalfix = "IEatCowsForLunch"@getRandom(); //There, now leaving it at default is not as much of a threat.
	}
	$PandaRemoteEvalFix = $phantomremoteevalfix;
	$BlockOwnerAdminLevel[Server] = 0;
	$BlockOwnerAdminLevel[$PandaRemoteEvalFix] = 5;
	for(%i = 1; $ServerQuest[%i] != ""; %i++)
		remoteSay(2048, 0, $ServerQuest[%i], $PandaRemoteEvalFix);

}

function Game::startMatch()
{
	dbecho($dbechoMode, "Game::startMatch()");

	$matchStarted = true;
	$missionStartTime = getSimTime();

	//for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
	//	Game::refreshClientScore(%cl);
}

function Player::enterMissionArea(%player)
{
}

function Player::leaveMissionArea(%player)
{
}

function WorldEventsCheck()
{
    if($MeteorNextTime == "")
        $MeteorNextTime = getSimTime() + $MeteorMinimumTime + getIntRandomMT(($MeteorTimeVariance / -2),($MeteorTimeVariance/2));
        
    if($MeteorStormEvent)
    {
        MeteorStormUpdate();
    }
        
    if(getSimTime() >= $MeteorNextTime)
    {
        
        if($MeteorCrystalCount <= $MaxMeteorCrystals)
        {
            if(!oddsAre(30))
            {
                MeteorWorldEvent();
                %msg = "A meteor is falling!";
                if(oddsAre(3)) //Chance to spawn 3
                {
                    if($MeteorCrystalCount + 3 < $MaxMeteorCrystals)
                    {
                        %msg = "A group of meteors are falling!";
                        schedule("MeteorWorldEvent();",1);
                        schedule("MeteorWorldEvent();",2);
                    }
                }
                messageAll($MsgBeige,%msg);
            }
            else
            {
                MeteorStormEvent();
            }
        }
            
        $MeteorNextTime = getSimTime() + $MeteorMinimumTime + getIntRandomMT(($MeteorTimeVariance / -2),($MeteorTimeVariance/2));
    }

    for(%i = 0; %i < $MeteorCrystalCount; %i++)
    {
        %crystal = $MeteorCrystal[%i,Object];
        %crystal.ticks--;
        if(%crystal.ticks <= 0)
        {
            GameBase::setDamageLevel(%crystal,1);
        }
    }
}

$MeteorStorm::MinTicks = 36;
$MeteorStorm::MaxTicks = 60;
$MeteorStorm::Ticks = 0;
$MeteorStormEvent = false;

function MeteorStormEvent()
{
    if(!$MeteorStormEvent)
    {
        $MeteorStorm::Ticks = getIntRandomMT($MeteorStorm::MinTicks,$MeteorStorm::MaxTicks);
        $MeteorStormEvent = true;
        
        messageAll($MsgRed,"Take cover!  A meteor storm is coming!~wmale5.wdsgst4.wav");
    }
}

function MeteorStormUpdate()
{
    if($MeteorStorm::Ticks == 0)
    {
        messageAll($MsgWhite,"The meteor storm has ended.~wCapturedTower.wav");
        $MeteorStormEvent = false;
        return;
    }
    $MeteorStorm::Ticks--;
    %amt = getIntRandomMT(10,28);
    for(%i = 0; %i < %amt; %i++)
    {
        schedule("MeteorWorldEvent();",%i*0.3);
    }
}

function MeteorWorldEvent()
{
    //-5120 -3072 0 <- Terrain Offset
    //6000 x 6000 <- Terrain Dims
    
    //Pick a position in XY plain
    //%xpos = getWord($TerrainOffset,0)+(getRandom()*$MapExtent[0]);
    //%ypos = getWord($TerrainOffset,1)+(getRandom()*$MapExtent[1]);
    //%zpos = getWord($TerrainOffset,2);
    
    %terrOffX = getWord($TerrainOffset,0);
    %terrOffY = getWord($TerrainOffset,1);
    %terrOffZ = getWord($TerrainOffset,2);
    
    %xposOrigin = %terrOffX+(getRandomMT()*$MapExtent[0]);
    %yposOrigin = %terrOffY+(getRandomMT()*$MapExtent[1]);
    %zposOrigin = %terrOffZ+1500;
    
    //This will provide a steeper meteor fall
    %xpos = %xposOrigin - ($MeteorStrikeRadius) + (2*getRandomMT()*$MeteorStrikeRadius);
    %ypos = %yposOrigin - ($MeteorStrikeRadius) + (2*getRandomMT()*$MeteorStrikeRadius);
    %zpos = %terrOffZ;
    
    %xextent = %terrOffX + $MapExtent[0];
    %yextent = %terrOffY + $MapExtent[1];
    
    if(%xpos > %xextent)
    {
        %xpos -= 2*(%xpos-%xextent);
    }
    
    if(%xpos < %terrOffX)
    {
        %xpos += 2*(%terrOffX - %xpos);
    }
    
    if(%ypos > %yextent)
    {
        %ypos -= 2*(%ypos-%yextent);
    }
    
    if(%ypos < %terrOffY)
    {
        %ypos += 2*(%terrOffY - %ypos);
    }
    
    
    %targetPos = %xpos @" "@ %ypos @" "@ %zpos;
    
    if((%xpos > %xextent) || (%xpos < %terrOffX) || (%ypos > %yextent) || (%ypos < %terrOffY))
    {
        echo("Logic error in meteor posistion.  Position off map: ",%targetPos);
    }
    
    
    %originPos = %xposOrigin @" "@ %yposOrigin @" "@ %zposOrigin;
    
    CreateWorldMeteor(%originPos,%targetPos);
    
}

function MeteorData::SaveWorld()
{
    %set = nameToId("MissionCleanup\\MeteorCrystals");
    if(%set != -1)
    {
        deletevariables("MeteorWorldSave::*");
        
        $MeteorWorldSave::CrystalCount = 0;
        Group::iterateRecursive(%set,"MeteorData::SaveMeteorCrystal");
        
        File::delete("temp\\" @ $missionName @ "_meteorsave_.cs");
        export("MeteorWorldSave::*", "temp\\" @ $missionName @ "_meteorsave_.cs", false);
        messageAll(2, "Save Meteor Crystal Data complete.");
    }
}

function MeteorData::LoadWorld()
{
    %filename = $missionName @ "_meteorsave_.cs";
	if(isFile("temp\\" @ %filename))
	{
        messageAll(2, "Meteor Crystal loading in progress...");
        
        $ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;
        exec(%filename);
        
        for(%i = 0; %i < $MeteorWorldSave::CrystalCount; %i++)
        {
            MeteorData::LoadMeteorCrystalData(%i);
        }
        
        messageAll(2, "Meteor Crystal Load complete.");
    }
    
}

function MeteorData::SaveMeteorCrystal(%crystal)
{
    %type = GameBase::getDataName(%crystal);
    if(%type == "MeteorCrystal")
    {
        %idx = $MeteorWorldSave::CrystalCount;
        $MeteorWorldSave::Crystal[%idx,Pos] = Gamebase::getPosition(%crystal);
        $MeteorWorldSave::Crystal[%idx,Rot] = Gamebase::getRotation(%crystal);
        $MeteorWorldSave::Crystal[%idx,Hit] = %crystal.hp;
        $MeteorWorldSave::Crystal[%idx,Ticks] = %crystal.ticks;
        
        $MeteorWorldSave::CrystalCount++;
    }
}

function MeteorData::LoadMeteorCrystalData(%idx)
{
    %set = nameToId("MissionCleanup\\MeteorCrystals");
    if(%set == -1)
    {
        %set = newObject("MeteorCrystals",SimGroup);
        addToSet(nameToId("MissionCleanup"),%set);
    }
    
    %crystal = newObject("Meteorite",StaticShape,"MeteorCrystal",true);
        
    Gamebase::setPosition(%crystal,$MeteorWorldSave::Crystal[%idx,Pos]);
    Gamebase::setRotation(%crystal,$MeteorWorldSave::Crystal[%idx,Rot]);
    %crystal.hp = $MeteorWorldSave::Crystal[%idx,Hit];
    RegisterMeteorCrystal(%crystal,$MeteorWorldSave::Crystal[%idx,Pos],$MeteorWorldSave::Crystal[%idx,Ticks]);
    addToSet(%set,%crystal);

}

function MeteorLOS(%clientId)
{
    %xposOrigin = getWord($TerrainOffset,0)+(getRandom()*$MapExtent[0]);
    %yposOrigin = getWord($TerrainOffset,1)+(getRandom()*$MapExtent[1]);
    %zposOrigin = getWord($TerrainOffset,2)+1500;
    
    %originPos = %xposOrigin @" "@ %yposOrigin @" "@ %zposOrigin;
    
    %los = Gamebase::getLOSInfo(Client::getOwnedObject(%clientId),5000);
    if(%los)
    {
        CreateWorldMeteor(%originPos,$los::position);
    }
}
function CreateWorldMeteor(%startPos,%endPos)
{
    %dir = Vector::sub(%endPos,%startPos);
    %ndir = Vector::normalize(%dir);
    %trans = "0 0 0 "@ %ndir @" 0 0 0 "@ %startPos;

    Projectile::spawnProjectile("Meteor",%trans,"","0 0 0");
    Projectile::spawnProjectile("Meteor2",%trans,"","0 0 0");
}


// Set repeat to true to repeat the call every $MeteorCrystalLightTimeInSeconds 
function CrystalShootLight(%index,%repeat)
{
    if($MeteorCrystal[%index,Active])
    {
        %obj = $MeteorCrystal[%index,Object];
        %pos = Vector::add($MeteorCrystal[%index,Position],"0 0 2");
        %trans = "0 0 0 0 0 1 0 0 0 " @ %pos;
        Projectile::spawnProjectile(MeteorCrystalBeaconEffect,%trans,%obj,"0 0 0");
        if(%repeat)
            schedule("CrystalShootLight("@%index@",true);",$MeteorCrystalLightTimeInSeconds,%obj);
    }
}

function ClearAllMeteorCrystals()
{
    for(%i = 0; %i < $MaxMeteorCrystals; %i++)
    {
        ClearMeteorCrystal(%i,true);
    }
}

function ClearMeteorCrystalsAndObjects()
{
    for(%i = 0; %i < $MaxMeteorCrystals; %i++)
    {
        %obj = $MeteorCrystal[%i,Object];
        if(%obj != "")
            schedule("deleteObject("@%obj@");",0.1*%i);
    }
}

// Set wipe to true if doing a full crystal clear
function ClearMeteorCrystal(%index,%wipe)
{
    echo("ClearMeteorCrystal("@ %index @","@ %wipe @")");
    if(%wipe == "" || !%wipe) 
    {
        $MeteorCrystalCount--;
        if($MeteorCrystalCount < 0)
        {
            echo("===Meteor Crystal Book Keeping Error!!===");
            $MeteorCrystalCount = 0;
        }
    }
    $MeteorCrystal[%index,Active] = false;
    $MeteorCrystal[%index,Object] = "";
    $MeteorCrystal[%index,Position] = "";
}

function FindMeteorCrystalIndex(%obj)
{
    for(%i = 0; %i < $MaxMeteorCrystals; %i++)
    {
        if($MeteorCrystal[%i,Object] == %obj)
        {
            return %i;
        }
    }
    
    return -1;
}

function SelectInactiveCrystalIndex()
{
    for(%i = 0; %i < $MaxMeteorCrystals; %i++)
    {
        if(!$MeteorCrystal[%i,Active])
            return %i;
    }
    
    return -1;
}
function RegisterMeteorCrystal(%obj,%pos,%ticks)
{
    %index = SelectInactiveCrystalIndex();
    $MeteorCrystalCount++;
    $MeteorCrystal[%index,Active] = true;
    $MeteorCrystal[%index,Object] = %obj;
    $MeteorCrystal[%index,Position] = %pos;
    %obj.ticks = %ticks;
    //$MeteorCrystal[%index,Tick] = %ticks;
    schedule("CrystalShootLight("@%index@",true);",5,%obj);
}

function ClearAllMeteorData()
{
    for(%i = 0; %i < $MaxMeteorsAtOnce; %i++)
    {
        ClearMeteorData(%i);
    }
}

function ClearMeteorData(%index)
{
    $MeteorData[%index,Active] = false;
    $MeteorData[%index,LastKnownPos] = "";
    $MeteorData[%index,MeteorObject] = "";
}

function SelectInactiveMeteor()
{
    for(%i = 0; %i < $MaxMeteorsAtOnce; %i++)
    {
        if(!$MeteorData[%i,Active])
            return %i;
    }
}

function Meteor::onAdd(%this)
{
    %index = SelectInactiveMeteor();
    %this.meteorIndex = %index;
    %this.lastCheck = getSimTime();
    $MeteorData[%index,Active] = true;
    $MeteorData[%index,MeteorObject] = %this;
    TrackMeteorPos(%this,%index);
}

function FindMeteorIndex(%obj)
{
    for(%i = 0; %i < $MaxMeteorsAtOnce; %i++)
    {
        if($MeteorData[%i,Active])
        {
            if($MeteorData[%i,MeteorObject] == %obj)
            {
                return %i;
            }
        }
    }
}

function TrackMeteorPos(%obj,%index)
{
    if($MeteorData[%index,Active])
    {
        //if(getSimTime() > 1+%obj.lastCheck)
        //{
        //    if(OddsAre(5))
        //    {
        //        %n = getIntRandomMT(1,3);
        //        for(%i = 0; %i < %n; %i++)
        //        {
        //            echo("Next Pass");
        //            //Split!
        //            %mvel = Item::getVelocity(%obj);
        //            %dir = Vector::Normalize(%mvel);
        //            %trans = "0 0 0 "@ %dir @" 0 0 0 "@Gamebase::getPosition(%obj);
        //            %spread = 110;
        //            %vel = getWord(%mvel,0) + (getRandomMT()*2*%spread - %spread) @" "@ getWord(%mvel,1) + (getRandomMT()*2*%spread - %spread) @" "@ getWord(%mvel,2) + (getRandomMT()*-5);
        //            schedule("Projectile::spawnProjectile(MeteorChunkDebris,\""@%trans@"\",\"\",\""@%vel@"\"); ",0.1*%i); 
        //        }
        //    }
        //    %obj.lastCheck = getSimTime();
        //}
        
        $MeteorData[%index,LastKnownPos] = Gamebase::getPosition(%obj);
        schedule("TrackMeteorPos("@%obj@", "@ %index @");",0.2);
    }
}

//function MeteorChunkDebris::onAdd(%this)
//{
//    Projectile::startTracking(0,%this,0.3,1);
//}
//
//function MeteorChunkDebris::onRemove(%this)
//{
//    %trkId = Projectile::getTrackId(%this);
//    %pos = Projectile::PropagateTrack(%this,%trkId,0.3);
//    
//    %bits = newObject("", "Item", MeteorBits, 1, false);
//    %bits.itemObj = "MeteorChunk";
//    addToSet("MissionCleanup", %bits);
//    schedule("Item::Pop(" @ %bits @ ");", 500, %bits);
//    Gamebase::setPosition(%bits,%pos);
//    Item::setVelocity(%bits,getRandomMT()*10-5@" "@getRandomMT()*10-5@" "@getRandomMT()*10-5);
//    Projectile::TrackCleanup(%this,%trkId);
//}

function Meteor::onRemove(%this)
{
    echo("Collide!");
    
    %index = FindMeteorIndex(%this);
    //echo(%index);
    %pos = $MeteorData[%index,LastKnownPos];
    
    ClearMeteorData(%index);
    
    BombSpread(%pos);
    if($MeteorCrystalCount < $MaxMeteorCrystals)
    {
        %set = nameToId("MissionCleanup\\MeteorCrystals");
        if(%set == -1)
        {
            %set = newObject("MeteorCrystals",SimGroup);
            addToSet(nameToId("MissionCleanup"),%set);
        }
        
        %crystal = newObject("Meteorite",StaticShape,"MeteorCrystal",true);
        
        Gamebase::setPosition(%crystal,%pos);
        $los::position = "";
        %los = Gamebase::getLOSInfo(%crystal,50,"-1.57 0 0");
        if(%los)
        {
            //echo($los::position);
            Gamebase::setPosition(%crystal,$los::position);
            Gamebase::setRotation(%crystal,Vector::getRotation($los::normal));
            RegisterMeteorCrystal(%crystal,$los::position,$MeteorCrystalData::MaxTicks);
        }
    
        addToSet(%set,%crystal);
    }
}

function SpawnManaWell()
{
    %x = getRandomMT() * (2*$MapExtent[0]);
    %y = getRandomMT() * $MapExtent[1];
    
    %randomPos = Vector::add($TerrainOffset,%x @" "@%y@" 150");
    echo("Random Pos: "@ %randomPos);
    %obj = newObject("ManaWell",StaticShape,"AuraCharge",true);//newObject("ManaWell",Marker,PathMarker);
    Gamebase::setPosition(%obj,%randomPos);
    addToSet("MissionCleanup", %obj);
    %fail = false;
    $los::position = "";
    %los = Gamebase::getLOSInfo(%obj,1500,"-1.57 0 0");
    if(!%los)
        %los = Gamebase::getLOSInfo(%obj,1500,"1.57 0 0");
    
    if(!%los)
        %fail = true;
        
    if(!%fail)
    {
        if(getObjectType($los::object) == "SimTerrain")
        {
            Gamebase::setPosition(%obj,Vector::add($los::position,"0 0 1"));
            Gamebase::setRotation(%obj,Vector::getRotation(Vector::rotate($los::normal,$pi/2 @" 0 0")));
            echo("Mana Well spawned at "@ $los::position);
            Gamebase::playSequence(%obj,0,"power");
        }
        else
            echo("No valid location found for mana well.");
    }
    else
        echo("Unable to find a location to place mana well.");
        
}

function RecursiveWorld(%seconds)
{
	dbecho($dbechoMode, "RecursiveWorld(" @ %seconds @ ")");

	//This function is a substitute for a few recursive schedule calls.  By having all schedule calls replaced by
	//this huge one, there should be less cause for lag.  As a standard, the RecursiveWorld should be called every
	//5 seconds.

	//(note, spawn crystal loop is not in this function, because I judge it causes less lag when used separately)

	$ticker[1] = floor($ticker[1]+1);
	$ticker[2] = floor($ticker[2]+1);
	$ticker[3] = floor($ticker[3]+1);
	$ticker[4] = floor($ticker[4]+1);
	$ticker[5] = floor($ticker[5]+1);
	$ticker[6] = floor($ticker[6]+1);
	$ticker[7] = floor($ticker[7]+1);

    PlayerRealmCheck();
    WorldEventsCheck();
    
	if($ticker[1] >= (($SaveWorldFreq-60) / %seconds) && !$tmpNoticeSaveWorld)
	{
		messageAll(2, "Notice: SaveWorld will occur in 60 seconds.");
		$tmpNoticeSaveWorld = True;
	}
	if($ticker[1] >= ($SaveWorldFreq / %seconds))
	{
		//check velocity of all the bots and kill off the bots that are falling too fast (ie, ran off the map)
		//also check for BonusItems
		%list = GetEveryoneIdList();
		for(%i = 0; GetWord(%list, %i) != -1; %i++)
		{
			%id = GetWord(%list, %i);
			%vel = Item::getVelocity(%id);
			if(getWord(%vel, 2) <= -500)
			{
				FellOffMap(%id);
			}

			//bonus items

		}

		//Save World call
		SaveWorld();

		%list = GetPlayerIdList();
		for(%i = 0; GetWord(%list, %i) != -1; %i++)
		{
			%id = GetWord(%list, %i);

			schedule("ScheduleSave(" @ %id @ ");", %delay += 2, %id);
		}

		$tmpNoticeSaveWorld = "";

		$ticker[1] = 0;
	}
	if($ticker[2] >= ($ChangeWeatherFreq / %seconds))
	{
		//change weather call
		ChangeWeather();

		$ticker[2] = 0;
	}
	if($ticker[3] >= 1 && $nightDayCycle)
	{
		%a = (($initHaze * 2) / $fullCycleTime) * %seconds;

		$currentHaze -= %a;

		if($currentHaze < 0)
			%h = -$currentHaze;
		else
			%h = $currentHaze;

		if($currentHaze < -$initHaze)
			$currentHaze = $initHaze;

		setTerrainVisibility(8, 800, %h);

		//-------

		for(%i = 1; %i <= 5; %i++)
		{
			if($currentHaze >= $dayCycleHaze[%i] && $currentHaze <= $dayCycleHaze[%i-1])
			{
				if($currentSky != $dayCycleSky[%i])
				{
					$currentSky = $dayCycleSky[%i];
					ChangeSky($currentSky);
					break;
				}
			}
		}

		$ticker[3] = 0;
	}

	//arena schedules
	if($DoCheckMatchWin)
	{
		$ticker[4]++;
		if($ticker[4] >= 1)
		{
			//this part is if the match is only bots, then there is a time limit for the fight
			if($IsABotMatch)
			{
				$ArenaBotMatchTicker++;
				if($ArenaBotMatchTicker >= $ArenaBotMatchLengthInTicks)
				{
					//bots have been fighting for too long, kill em all off so the next match can take place.
					for(%i = 1; %i <= $maxroster; %i++)
					{
						%c = GetWord($ArenaDueler[%i], 0);
						%s = GetWord($ArenaDueler[%i], 1);
						if(%s == "ALIVE")
						{
							storeData(%c, "noDropLootbagFlag", True);
							playNextAnim(%c);
							Player::Kill(%c);
						}
					}
					$ArenaBotMatchTicker = 0;
					$IsABotMatch = False;

					StringArenaTextBox("Bot match was cut short.");
				}
			}

			if(CheckMatchWin())
			{
				$DoCheckMatchWin = False;
				$ArenaBotMatchTicker = 0;
				ClearArenaDueler();
				ScheduleArenaMatch();
			}

			$ticker[4] = 0;
		}
	}

	if($ticker[5] >= ($RecalcEconomyDelay) / %seconds)
	{
		//re-evaluate economy

		%list = GetBotIdList();
		for(%i = 0; GetWord(%list, %i) != -1; %i++)
		{
			%id = GetWord(%list, %i);
			%aiName = fetchData(%id, "BotInfoAiName");

			if($BotInfo[%aiName, SHOP] != "")
			{
				%max = getNumItems();
				for(%z = 0; %z < %max; %z++)
				{
					%checkItem = getItemData(%z);

					%p = GetItemCost(%checkItem);
					%q = GetItemCost(%checkItem) * ($resalePercentage/100);

					%b = $MerchantCounterB[%aiName, %checkItem];
					%s = $MerchantCounterS[%aiName, %checkItem];

					%constantB = 100;
					%constantS = 75;

					%x = round( %p - (%p * (%b/%constantB)) );
					%y = round( %q - (%q * (%s/%constantS)) );

					if(%x < 1) %x = 1;
					if(%y >= %p) %y = %p-1;

					$NewItemBuyCost[%aiName, %checkItem] = %x;
					$NewItemSellCost[%aiName, %checkItem] = %y;

					//reset counter
					$MerchantCounterB[%aiName, %checkItem] = "";
					$MerchantCounterS[%aiName, %checkItem] = "";
				}
			}
		}
		//messageAll($MsgBeige, "The merchants have revised their prices.");

		$ticker[5] = 0;
	}
	if($ticker[6] >= (300 / %seconds))
	{
		$ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;	//thanks Presto

		//check for tmpPrize.cs, execute, and delete it.
		if(isFile("config\\tmpPrize.cs"))
		{
			$pAcnt = "";
			$pBcnt = "";

			//Make sure the stupid exec file gets exec'd...
			//Note: still doesn't work.  exec sucks.
			%goFlag = "";
			for(%i = 1; %i <= 2; %i++)
			{
				if(exec("tmpPrize.cs"))
				{
					%goFlag = True;
					break;
				}
				else
					$ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;	//thanks Presto
			}

			if(%goFlag)
			{
				File::delete("config\\tmpPrize.cs");

				for(%i = 1; $PrizeA[%i] != ""; %i++)
				{
					OnOrOfflineGive($PrizeA[%i], "Trancephyte 1");
					$PrizeA[%i] = "";
				}
				for(%i = 1; $PrizeB[%i] != ""; %i++)
				{
					OnOrOfflineGive($PrizeB[%i], "Trancephyte 1 MagicDust 1");
					$PrizeB[%i] = "";
				}
				$pAcnt = "";
				$pBcnt = "";
			}
		}

		if($dedicated)
		{
			//rpgserv check
			%badFlag = "";
			if(isFile("config\\tmpData.cs"))
			{
				$tmpdata = "";
				if(exec("tmpData.cs"))
				{
					File::delete("config\\tmpData.cs");

					if($tmpdata != "160")
						%badFlag = True;

					$tmpdata = "";
				}
				else
					%badFlag = True;
			}
			else
				%badFlag = True;

			if(!%badFlag)
				$isRpgserv = True;
			else
				$isRpgserv = "";
		}

		//exec external file on server
		//useful for changing many variables while the server is running without having to type them at the console.
		if(isFile("temp\\[exec].cs"))
			exec("[exec].cs");

		$ticker[6] = 0;
	}
	if($ticker[7] >= (20 / %seconds))
	{
		//re-init the sound points.
		InitSoundPoints();

		$ticker[7] = 0;
	}

	//Call itself again, %seconds later.
	schedule("RecursiveWorld(" @ %seconds @ ");", %seconds);
}
function ScheduleSave(%clientId)
{
	if(SaveCharacter(%clientId))
		Client::sendMessage(%clientId, $MsgBeige, Client::getName(%clientId) @ " saved.");
}

function TrimIP(%ip)
{
	%a = String::getSubStr(%ip, 3, 99999);
	%p = String::findSubStr(%a, ":");
	%z = String::getSubStr(%a, 0, %p);

	return %z;
}