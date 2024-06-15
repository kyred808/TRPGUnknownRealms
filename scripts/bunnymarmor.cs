$mass[bunny] = 9.0;
$speed[bunny] = 16;
$jump[bunny] = 80;

PlayerData bunnymarmor1
{
   className = "Armor";
   shapeFile = "bunnymarmor1";
   damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
   flameShapeName = "";
   shieldShapeName = "shield";
   shadowDetailMask = 1;

   visibleToSensor = False;
	mapFilter = 0;
	mapIcon = "M_player";
   canCrouch = true;

   maxJetSideForceFactor = 1;
   maxJetForwardVelocity = 1.0;
   minJetEnergy = 60;
   jetForce = 1;
   jetEnergyDrain = 0.0;

	maxDamage = 1.0;
   maxForwardSpeed = $speed[bunny];
   maxBackwardSpeed = $speed[bunny] * 0.8;
   maxSideSpeed = $speed[bunny] * 0.5;

   groundForce = 50.00 * $mass[bunny];
   mass = $mass[bunny];
   groundTraction = 3.0;
	maxEnergy = 60;
   drag = 1.0;
   density = 1.2;

	minDamageSpeed = 20;
	damageScale = $damageScale;

   jumpImpulse = $jump[bunny];
   jumpSurfaceMinDot = $jumpSurfaceMinDot;

   // animation data:
   // animation name, one shot, direction
	// firstPerson, chaseCam, thirdPerson, signalThread
   // movement animations:
   animData[0]  = { "root", none, 1, true, true, true, false, 0 };
   animData[1]  = { "run", none, 1, true, false, true, false, 3 };//run, doesn't work
   animData[2]  = { "run", none, -1, true, false, true, false, 3 };//runback, doesn't work
   animData[3]  = { "left", none, 1, true, false, true, false, 3 };//side left, doesn't work
   animData[4]  = { "left", none, -1, true, false, true, false, 3 };//side left -1, doesn't work
   animData[5] = { "jump", none, 1, true, false, true, false, 3 };//jump stand, works
   animData[6] = { "jump", none, 1, true, false, true, false, 3 };//jump run, works
   animData[7] = { "root", none, 1, true, true, true, false, 3 };//crouch root
   animData[8] = { "root", none, 1, true, true, true, false, 3 };//crouch root
   animData[9] = { "root", none, -1, true, true, true, false, 3 };//crouch root -1
   animData[10] = { "root", none, 1, true, false, true, false, 3 };//crouch forward
   animData[11] = { "root", none, -1, true, false, true, false, 3 };//crouch forward -1
   animData[12] = { "root", none, 1, true, false, true, false, 3 };//crouch side left
   animData[13] = { "root", none, -1, true, false, true, false, 3 };//crouch side left -1
   animData[14]  = { "jet", none, 1, true, true, true, false, 3 };//fall, works
   animData[15]  = { "root", SoundLandOnGround, 1, true, false, false, false, 3 };//landing, works
   animData[16]  = { "root", SoundLandOnGround, 1, true, false, false, false, 3 };//landing, works
   animData[17]  = { "root", none, 1, true, false, false, false, 3 };//tumble loop
   animData[18]  = { "root", none, 1, true, false, false, false, 3 };//tumble end
   animData[19] = { "jet", none, 1, true, true, true, false, 3 };//root, flying works

   // misc. animations:
   animData[20] = { "root", none, 1, true, false, false, false, 0 };//die back
   animData[21] = { "root", none, 1, true, false, false, false, 3 };//throw
   animData[22] = { "root", none, 1, false, false, false, false, 3 };//flyer root
   animData[23] = { "root", none, 1, true, true, true, false, 3 };//apc root
   animData[24] = { "root", none, 1, false, false, false, false, 3 };//apc pilot
   
   // death animations:
   animData[25] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//crouch die
   animData[26] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die chest
   animData[27] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die head
   animData[28] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die  grab back
   animData[29] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die right side
   animData[30] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die left side
   animData[31] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die leg left
   animData[32] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die leg right
   animData[33] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die blown back
   animData[34] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die spin
   animData[35] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//root
   animData[36] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//root kneel
   animData[37] = { "dieleft", SoundPlayerDeath, 1, false, false, false, false, 4 };//die back

   // signal moves:
	animData[38] = { "root",  none, 1, true, false, false, false, 2 };//sign over here
   animData[39] = { "root", none, 1, true, false, false, false, 1 };//sign point
   animData[40] = { "root",none, 1, true, false, false, false, 2 };//sign retreat
   animData[41] = { "root", none, 1, true, false, false, true, 1 };//sign stop
   animData[42] = { "tailwag", none, 1, true, false, false, true, 1 }; //sign salut


    // celebration animations:
   animData[43] = { "dieleft", none, 1, true, false, false, true, 1 };//celebration 1
   animData[44] = { "tailwag", none, 1, true, false, false, true, 1 };//celebration 2
   animData[45] = { "backflip", none, 1, true, false, false, false, 2 };//celebration 3
 
    // taunt animations:
	animData[46] = { "root", none, 1, true, false, false, false, 2 };//taunt 1
	animData[47] = { "root", none, 1, true, false, false, false, 2 };//taunt 2
 
    // poses:
	animData[48] = { "root", none, 1, true, false, false, true, 1 };//pose kneel
	animData[49] = { "root", none, 1, true, false, false, true, 1 };//pose stand

	// Bonus wave
   animData[50] = { "root", none, 1, true, false, false, true, 1 };//wave

   jetSound = NoSound;
   rFootSounds = 
   {
     SoundLFootRSoft,
     SoundLFootRHard,
     SoundLFootRSoft,
     SoundLFootRHard,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRHard,
     SoundLFootRSnow,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRSoft,
     SoundLFootRSoft
  }; 
   lFootSounds =
   {
      SoundLFootLSoft,
      SoundLFootLHard,
      SoundLFootLSoft,
      SoundLFootLHard,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLHard,
      SoundLFootLSnow,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLSoft,
      SoundLFootLSoft
   };

   footPrints = { 0, 1 };

   boxWidth = 2;
   boxdepth = 2;
   boxNormalHeight = 3.3;
   boxCrouchHeight = 3.3;

   boxNormalHeadPercentage  = 0.83;
   boxNormalTorsoPercentage = 0.53;
   boxCrouchHeadPercentage  = 0.26;
   boxCrouchTorsoPercentage = 0.3333;

   boxHeadLeftPercentage  = 0;
   boxHeadRightPercentage = 1;
   boxHeadBackPercentage  = 0;
   boxHeadFrontPercentage = 1;
};