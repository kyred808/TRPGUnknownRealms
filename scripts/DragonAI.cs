function Dragon::Periodic(%aiName)
{
    %aiId = AI::getId(%aiName);
    
    if(fetchData(%aiId, "dumbAIflag") || fetchData(%aiId, "frozen"))
		return;
        
    %aiTeam = GameBase::getTeam(%aiId);
	%aiPos = GameBase::getPosition(%aiId);
    //Hold still!
    AI::newDirectiveWaypoint(%aiName, %aiPos, 20);
   // Dragon::SelectMovement(%aiName);
}

$DragonMoveMode = 0; //Walking
$DragonMoveMode = 1; //Flying

//$DragonAttackMode = 0; // Wandering
//$DragonAttackMode = 1; // Regular walk
//$DragonAttackMode = 2; // Attacking a player
//$DragonAttackMode = 3; // Attack at a certain position
//$DragonAttackMode = 4; // Bot group formation - Not sure if will be used.

function Dragon::SelectMovement(%aiName)
{
    %aiId = AI::getId(%aiName);
    %dragonMode = fetchData(%aiId,"dragonMoveMode");
    
    %botAttackMode = fetchData(%aiId, "botAttackMode");
    if(%botAttackMode == 1)
    {
        %r = OddsAre($AImoveChance);
        if(%r && !fetchData(%aiId, "frozen"))
		{
			%s = RandomRaceSound(fetchData(%aiId, "RACE"), RandomWait);
			if(%s == "NoSound")
				PlaySound(SoundGrunt1, GameBase::getPosition(%aiId));
			else
				PlaySound(%s, GameBase::getPosition(%aiId));
			AI::moveToFurthest(%aiName);
		}
    }
}

$MaxPosFlyingZVel = 15;
$MaxNegFlyingZVel = -20;
$DragonFTPTolerance = 10;

// For now, limits fighing 1 dragon at a time, as damage would get weird
function setDragonFlag(%player,%flag,%time,%aiId)
{
    %cl = Player::getClient(%player);
    %prev = fetchData(%cl,"dragonAttack");
    storeData(%cl,"dragonAttack",%aiId);
    if(%prev == "")
        schedule("storeData("@%cl@",\"dragonAttack\",\"\");",%time,%cl);
}

function Dragon::toggleJetCheck(%aiId,%aiName)
{
    %pos = fetchData(%aiId,"targetPos");
    if(%pos != "")
    {
        %seq = fetchData(%aiId,"flightSequence");
        if(%seq == 1) // Fly to location
        {
            %aiPlayer = Client::getOwnedObject(%aiId);
            echo("Ai Player: "@%aiPlayer);
            %currentPos = Gamebase::getPosition(%aiId);
            %vel = Item::getVelocity(%aiPlayer);
            
            %speed = sqrt(Vector::dot(%vel,%vel)); //Dot product of something with itself is magnitude squared
            %heightCheck = getWord(%currentPos,2) < getWord(%pos,2);
            %zvel = getWord(%vel,2);
            %jet = false;
            if(%heightCheck)
            {
                if(%zvel < $MaxPosFlyingZVel)
                    %jet = true;
                else
                {
                    %jet = false;
                }
            }
            else
            {
                if(%zvel < $MaxNegFlyingZVel)
                    %jet = true;
            }
            
            echo("Is Jetting: "@%jet@ " Vel: "@ %vel @" Speed: "@ %speed);
            Player::setJet(%aiPlayer, %jet);
            
            %dist = Vector::getDistance(%currentPos,%pos);
            if(%dist < $DragonFTPTolerance)
            {
                storeData(%aiId,"flightSequence",2);
                Item::setVelocity(%aiPlayer,"0 0 0");
                Player::setGravity(%aiPlayer,0);
                Player::setJet(%aiPlayer, false);
                AI::SetVar(%aiName, spotDist, 1000);
                AI::newDirectiveRemove(%aiName, 99);
                
            }
            
        }
        else if(%seq == 2) // Look for target
        {
           // %check = fetchData(%aiId,"dragonAreaCheck");
           // if(%check == "")
           // {
           //     %set = newObject("set", SimSet);
           //     %n = containerBoxFillSet(%set, $SimPlayerObjectType, Gamebase::getPosition(%aiId), 1000, 1000, 1000, 0);
           //     Group::iterateRecursive(%set, setDragonFlag,"600",%aiId);
           //     deleteObject(%set);
           //     storeData(%aiId,"dragonAreaCheck",4);
           // }
           // else
           // {
           //     %check--;
           //     if(%check == 0)
           //         storeData(%aiId,"dragonAreaCheck","");
           //     else
           //         storeData(%aiId,"dragonAreaCheck",%check);
           // }
        
            echo("Seq 2");
            %cnt = fetchData(%aiId,"fireballShots");
            %lastFb = fetchData(%aiId,"lastFireball");
            %target = fetchData(%aiId,"targetFound");
            if(%target != "")
            {
                if(%cnt < 16)
                {
                    if(AI::TargetWithinFOV(%aiId,%target,deg2rad(45)))
                    {
                        if(%lastFb == "" || getSimTime() - %lastFb > 0.4)
                        {
                            storeData(%aiId,"lastFireball",getSimTime());
                            Dragon::launchFireball(%aiName,%target);
                            
                            storeData(%aiId,"fireballShots",%cnt+1);
                        }
                    }
                }
                else
                {
                    %aiPlayer = Client::getOwnedObject(%aiId);
                    //Charge up big shot
                    %charge = fetchData(%aiId,"bigShotCharge");
                    if(%charge == "")
                    {
                        Player::mountItem(%aiPlayer,DragonFireCharge1,0);
                    }
                    

                    if(%charge != "" && (%charge % 3 == 0 || ( %charge > 11 )))
                    {
                        //Create shockwave
                        %obj = newObject("","Mine","bombf");
                        addToSet("MissionCleanup", %obj);
                        %padd = "0 0 15";
                        %pos = Vector::add(Gamebase::getPosition(%aiId), %padd);
                        GameBase::setPosition(%obj, %pos);
                    }
                    
                    if(%charge == 10)
                    {
                        Player::mountItem(%aiPlayer,DragonFireCharge2,4);
                    }
                    
                    if(%charge > 15)
                    {
                        Dragon::launchDragonBlast(%aiName,%target);
                        storeData(%aiId,"bigShotCharge","");
                        Player::unmountItem(%aiPlayer,0);
                        Player::unmountItem(%aiPlayer,4);
                        storeData(%aiId,"fireballShots","");
                        storeData(%aiId,"lastFireball","");
                        storeData(%aiId,"flightSequence",3);
                        Player::setArmor(%aiPlayer,"DragonArmor");
                    }
                    else
                        storeData(%aiId,"bigShotCharge",1,"inc");
                }
                
            }
        }
        else if(%seq == 3)
        {
            storeData(%aiId,"flightSequence","");
            storeData(%aiId,"targetPos","");
            storeData(%aiId,"targetFound","");
            %aiPlayer = Client::getOwnedObject(%aiId);
            Player::setGravity(%aiPlayer,-20);
            //AI::SetVar(%aiName, spotDist, 1000);
        }
        
        
        if(%aiPlayer != -1 && %seq < 3)
            schedule("Dragon::toggleJetCheck("@%aiId@","@%aiName@");",0.5);
    }
}

function Dragon::launchFireball(%aiName,%target)
{
    %aiId = AI::getId(%aiName);
    %trans = Gamebase::getEyeTransform(%aiId);
    %targetPos = Gamebase::getPosition(%target);
    %eyePos = Word::getSubWord(%trans,9,3);
    %atkDir = Vector::Normalize(Vector::sub(%targetPos,%eyePos));
    
    %ntrans = "0 0 0 "@ %atkDir @" 0 0 0 "@ %eyePos;
    
    %aiPlayer = Client::getOwnedObject(%aiId);
    Projectile::spawnProjectile(DragonFireball,%ntrans,%aiPlayer,"0 0 0");
    playSound(ActivateAB,Gamebase::getPosition(%aiId));
}

function Dragon::launchDragonBlast(%aiName,%target)
{
    %aiId = AI::getId(%aiName);
    %trans = Gamebase::getEyeTransform(%aiId);
    %targetPos = Gamebase::getPosition(%target);
    %eyePos = Word::getSubWord(%trans,9,3);
    %atkDir = Vector::Normalize(Vector::sub(%targetPos,%eyePos));
    
    %ntrans = "0 0 0 "@ %atkDir @" 0 0 0 "@ %eyePos;
    
    %aiPlayer = Client::getOwnedObject(%aiId);
    %proj = Projectile::spawnProjectile(DragonBlast,%ntrans,%aiPlayer,"0 0 0");
    Projectile::TrackProjectile(%proj,0.2,%aiId);
    playSound(debrisLargeExplosion,Gamebase::getPosition(%aiId));
}

function DragonBlast::onRemove(%this)
{
    %pos = Projectile::PropagateTrack(%this,0.2);
    BombSpread(%pos);
}

function Dragon::FlyToPosition(%aiName,%pos)
{
    %aiId = AI::getId(%aiName);
    
    %currentPos = Gamebase::getPosition(%aiId);
    storeData(%aiId,"targetPos",%pos);
    storeData(%aiId,"flightSequence",1);
    Player::setArmor(%aiId,"FlyingDragonArmor");
    AI::newDirectiveRemove(%aiName, 99);
    AI::newDirectiveRemove(%aiName, 20);
    Dragon::toggleJetCheck(%aiId,%aiName);
    AI::newDirectiveWaypoint(%aiName, %pos, 99);
}