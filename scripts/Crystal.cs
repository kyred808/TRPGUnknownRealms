//Regular crystal
StaticShapeData Crystal
{
	shapeFile = "crystals2";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
	description = "Crystal";
};
function Crystal::onDamage()
{
}

MarkerData CrystalMarker {
	shapeFile = "dirArrows";
};

StaticShapeData OreCrystal
{
	shapeFile = "crystals";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
	description = "OreCrystal";
};

$OreCrystalData::defaultHP = 6;
$OreCrystalData::defaultHPVariance = 2;

function OreCrystal::onAdd(%this)
{
    if(%this.hp == "")
        %this.hp = $OreCrystalData::defaultHP;
    
}

function OreCrystal::onRemove(%this)
{
}

function OreCrystal::onDamage(%this,%type,%value,%pos,%vec,%mom,%vertPos,%rweapon,%object,%weapon,%preCalcMiss)
{
    if($AccessoryVar[RPGItem::ItemTagToLabel(%weapon), $AccessoryType] == $PickAxeAccessoryType)
    {
        %this.hp = %this.hp - 1;
        if(%this.hp < 1)
        {
            %pos = Gamebase::getPosition(%this);
            %above = Vector::add(%pos,Vector::getFromRot(Gamebase::getRotation(%this),2));
            GameBase::setDamageLevel(%this,1);
            
            if(%this.dropOnBreak != "")
            {
                if(%this.dropMin == "")
                    %min = 1;
                if(%this.dropMax == "")
                    %max = 1;

                %amt = getIntRandomMT(%min,%max);
                for(%i = 0; %i < %amt; %i++)
                {
                    %bits = newObject("", "Item", OreShape, 1, false);
                    %bits.itemObj = %this.dropOnBreak; //RPGItem::LabelToItemTag("TitaniteShard");
                    addToSet("MissionCleanup", %bits);
                    schedule("Item::Pop(" @ %bits @ ");", 500, %bits);
                    %rot = "0 0 "@getRandomMT()*2*$PI;
                    %vel = Vector::getFromRot(%rot,$MeteorBitsSpeed,$MeteorBitsZSpeed);
                    Gamebase::setPosition(%bits,%above);
                    Gamebase::setRotation(%bits,%rot);
                    Item::setVelocity(%bits,%vel);
                }
            }
            %realm = getWord(%this.id,0);
            %gid = getWord(%this.id,1);
            %id = getWord(%this.id,2);
            $RealmData[%realm,CrystalGroup,%gid,ActiveCrystalCnt]--;
            $RealmData[%realm,CrystalGroup,%gid,Crystal,%id,Active] = false;
        }
    }
}

//Regular crystal
StaticShapeData GemCrystal
{
	shapeFile = "crystals2";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
	description = "GemCrystal";
};

$GemCrystalData::MaxHP = 15;
$GemCrystalData::MinHP = 5;
$GemCrystalData::MinRespawnTime = 0.5*60;
$GemCrystalData::MaxRespawnTime = 2*60;
function GemCrystal::onAdd(%this)
{
    %this.hp = getIntRandomMT($GemCrystalData::MinHP,$GemCrystalData::MaxHP);
}

function GemCrystal::onRemove(%this)
{
}

function GemCrystal::onDamage(%this,%type,%value,%pos,%vec,%mom,%vertPos,%rweapon,%object,%weapon,%preCalcMiss)
{
    if($AccessoryVar[RPGItem::ItemTagToLabel(%weapon), $AccessoryType] == $PickAxeAccessoryType)
    {
        %this.hp = %this.hp - 1;
        if(%this.hp < 1)
        {
            %pos = Gamebase::getPosition(%this);
            %above = Vector::add(%pos,Vector::getFromRot(Gamebase::getRotation(%this),2));
            GameBase::setDamageLevel(%this,1);
            %amt = getIntRandomMT(1,4);
            //echo("SpawnAmt: "@ %amt);
            for(%i = 0; %i < %amt; %i++)
            {
                %bits = newObject("", "Item", OreShape, 1, false);
                %bits.itemObj = RPGItem::LabelToItemTag("TitaniteShard");
                addToSet("MissionCleanup", %bits);
                schedule("Item::Pop(" @ %bits @ ");", 500, %bits);
                %rot = "0 0 "@getRandomMT()*2*$PI;
                %vel = Vector::getFromRot(%rot,$MeteorBitsSpeed,$MeteorBitsZSpeed);
                Gamebase::setPosition(%bits,%above);
                Gamebase::setRotation(%bits,%rot);
                Item::setVelocity(%bits,%vel);
            }
            %realm = getWord(%this.id,0);
            %id = getWord(%this.id,1);
            $RealmData[%realm,CrystalSpawned,%id] = false;
            $RealmData[%realm,CrystalRespawnTime,%id] = getSimTime() + getIntRandomMT($GemCrystalData::MinRespawnTime,$GemCrystalData::MaxRespawnTime);
        }
    }
}

//Empty crystal
StaticShapeData EmptyCrystal
{
	shapeFile = "crystals";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
      description = "Empty Crystal";
};

//Meteor crystal
StaticShapeData MeteorCrystal
{
	shapeFile = "crystals5";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
	description = "Crystal";
};

function MeteorCrystal::onAdd(%this)
{
    %this.hp = $MeteorCrystalData::HitMax;
}

function MeteorCrystal::onRemove(%this)
{
    //%this.hp = 5;
    %index = FindMeteorCrystalIndex(%this);
    ClearMeteorCrystal(%index);
}

$MeteorBitsSpeed = 10;
$MeteorBitsZSpeed = 5;
function MeteorCrystal::onDamage(%this,%type,%value,%pos,%vec,%mom,%vertPos,%rweapon,%object,%weapon,%preCalcMiss)
{
    if($AccessoryVar[RPGItem::ItemTagToLabel(%weapon), $AccessoryType] == $PickAxeAccessoryType)
    {
        //if( floor(getRandom() * 10) > 5)
        //{
            %this.hp = %this.hp - 1;
            
        //}
        echo(%this.hp);
        if(%this.hp < 1)
        {
            %pos = Gamebase::getPosition(%this);
            %above = Vector::add(%pos,Vector::getFromRot(Gamebase::getRotation(%this),2));
            GameBase::setDamageLevel(%this,1);
            %amt = getIntRandomMT(3,6);
            for(%i = 0; %i < %amt; %i++)
            {
                %odds = OddsAre(5);
                %shape = "";
                %loot = "";
                if(%odds)
                {
                    %shape = "MeteorBitsRed";
                    %loot = RPGItem::LabelToItemTag("MeteorCore");
                }
                else
                {
                    %shape = "MeteorBits";
                    %loot = RPGItem::LabelToItemTag("MeteorChunk");
                }
                   
                %bits = newObject("", "Item", %shape, 1, false);
                %bits.itemObj = %loot;
                addToSet("MissionCleanup", %bits);
                schedule("Item::Pop(" @ %bits @ ");", 500, %bits);
                %rot = "0 0 "@getRandomMT()*2*$PI;
                %vel = Vector::getFromRot(%rot,$MeteorBitsSpeed,$MeteorBitsZSpeed);
                Gamebase::setPosition(%bits,%above);
                Gamebase::setRotation(%bits,%rot);
                Item::setVelocity(%bits,%vel);
            }
            //GameBase::throw(%arrow, Client::getOwnedObject(%clientId), %vel, false);
            //deleteObject(%this);
        }
    }
}

function InitCrystals()
{
	dbecho($dbechoMode, "InitCrystals()");

	%group = nameToID("MissionGroup\\Crystals");

	if(%group != -1)
	{
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++)
		{
			%this = Group::getObject(%group, %i);
			%info = Object::getName(%this);

			if(%info != "")
			{
				%cnt = 0;
				for(%z = 0; (%p1 = GetWord(%info, %z)) != -1; %z+=2)
				{
					%p2 = GetWord(%info, %z+1);
					%this.bonus[%cnt++] = %p2;
				}
			}
		}
	}
}