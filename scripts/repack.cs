$repackver = 14;

//Plasmatic echo
function pecho(%m)
{
	//$console::printlevel = 1;
	echo(String::getSubStr(%m, 0, 250));
	//$console::printlevel = 0;		
}



function rp_include(%file){
	if(!$fileLoaded[%file])
		exec(%file);
	$fileLoaded[%file] = True;
}

function remoteRSound(%server, %val)
{//By phantom - beatme101.com, tribesrpg.org
	//This function is useful for disabling or resetting sound.
	//It can be useful for any server to induce silence.

	//Send a value of 1 to turn off sound.
	//Send a value of 2 to turn sound back on.
	//Send a value of 3 to turn sound off, then back on (to end currently playing sounds such as music).

	if(%server != 2048)
		return;

	if(%val & 1)
	{
		sfxclose();
	}

	if(%val & 2)
	{
		sfxopen();
	}
}

function sendControl(%val, %mod)
{//By phantom - beatme101.com, tribesrpg.org
	remoteEval(2048,RawKey,%val, %mod);
}
function remoteRepackIdent(%server, %val)
{
	if(%val && %server == 2048)
		remoteeval(%server, RepackConfirm, 14);
}
function remoteRepackKeyOverride(%server, %val)
{//By phantom - beatme101.com, tribesrpg.org
	if(%server == 2048){
		if(%val)
			$repackKeyOverride = True;
		else
			$repackKeyOverride = "";
	}
}

//function String::len(%string)
//{
//	%chunk = 10;
//	%length = 0;
//
//	for(%i = 0; String::getSubStr(%string, %i, 1) != ""; %i += %chunk)
//		%length += %chunk;
//	%length -= %chunk;
//
//	%checkstr = String::getSubStr(%string, %length, 99999);
//	for(%k = 0; String::getSubStr(%checkstr, %k, 1) != ""; %k++)
//		%length++;
//
//	if(%length == -%chunk)
//		%length = 0;
//
//	return %length;
//}

//=====================================================
//Buffered Center Print
//Written by Bovidi
//-Added in repack 9
//This function is designed for low speed input, to
//generate long messages all at once.
//Message is sent 255 chars at a time, and displays only
//when the last message has been recieved.
//Despite the name, this function can print to any
//location on the screen (top, bottom, center).
//Do not use this at the same time as the other buffered print by Bovidi.
//This function is left for legacy support, do not use it if you have a choice.
//=====================================================
function remoteBufferedCenterPrint(%server, %string, %timeout, %location) {

	if(%server != 2048) {
		return false;
	}

	if(%timeout > 0) {
		$RPG::bufferedCenterPrintID = -123123123;//Avoid conflict with the other buffered function
		//Begin the string
		$RPG::bufferedTextOverflow = false;
		$RPG::bufferedTextLength = 0;
		$RPG::bufferedText = "";
		$RPG::bufferedTextTimeout = %timeout;
	}

	if($RPG::bufferedTextOverflow)
		return;

	$RPG::bufferedText = $RPG::bufferedText @ %string;
	$RPG::bufferedTextLength = $RPG::bufferedTextLength + String::Len(%string);

	if(%timeout == -1 || $RPG::bufferedTextLength > 1500) {
		//Final piece of text
		$centerPrintId++;
		Client::centerPrint($RPG::bufferedText, %location);
		if($RPG::bufferedTextTimeout)
			schedule("clearCenterPrint(" @ $centerPrintId @ ");", $RPG::bufferedTextTimeout);

		if($RPG::bufferedTextLength > 1500) {
			//Overflow detected
			$RPG::bufferedTextOverflow = true;
		}
	}
}

//=====================================================
//Buffered Console Print
//Written by phantom - tribesrpg.org
//This function is designed for high frequency messages,
//such as damage messages in battles.
//It can handle 255 chars per input string, and has a
//high number of supported lines.
//Messages are sent and handled similarly to the Tribes
//chat hud, except you should end each line here with \n.
//Changing the value of %max will only have an effect
//on subsequent messages.
//======================================================
function remoteBufferedConsolePrint(%server, %string, %timeout, %location, %max) {
	if(%server != 2048){
		return;
	}
	%max = floor(%max);
	$ConsolePrintText[$cprintnum++] = %string;
	for(%i = 1; $ConsolePrintText[%i] != ""; %i++)
			%msg = %msg @ $ConsolePrintText[%i];
	$centerPrintId++;
	Client::centerPrint(%msg, %location);
	schedule("clearCenterPrint(" @ $centerPrintId @ ");", %timeout);

	if(String::len(%msg) > 1500){//overflowing
		for(%i = 1; $ConsolePrintText[%i] != ""; %i++)
			$ConsolePrintText[%i-1] = $ConsolePrintText[%i];
		$cprintnum--;
		$ConsolePrintText[%i] = "";
		$ConsolePrintText[0] = "";
	}
	//for(%n=1;$cprintnum >= %max;%n++){
	if($cprintnum >= %max){
		for(%i = 1; $ConsolePrintText[%i] != ""; %i++){
			if(%i <= %max)
				$ConsolePrintText[%i-1] = $ConsolePrintText[%i];
			else
				$ConsolePrintText[%i] = "";
		}
		$cprintnum = %max-1;
		$ConsolePrintText[0] = "";
	}
}

function remoteClearBufferedConsole(%server){
//Written by phantom - tribesrpg.org
	if(%server != 2048){
		return;
	}
	for(%i = 1; $ConsolePrintText[%i] != ""; %i++)
		$ConsolePrintText[%i] = "";
	$cprintnum = "";
}


$RPG::bufferedCenterPrintID = -123123123;

//=====================================================
//Buffered Center Print 2
//Written by Bovidi (bug-fixed by phantom)
//-Added in repack 16
//This function is designed for high speed input, to
//generate long messages all at once.
//Message is sent 255 chars at a time, and displays only
//when the last message has been recieved.
//Despite the name, this function can print to any
//location on the screen (top, bottom, center).
//Do not use this at the same time as the other buffered print by Bovidi.
//======================================================
function remoteBufferedCenterPrint2(%server, %string, %timeout, %location, %index, %id) {
	if(%server != 2048)
		return false;
	//Timeout is 0 - N, so piece together the message


	//New message has arrived, toss out the previous message
	if($RPG::bufferedCenterPrintID != %id)
	{
		//New id get rid of everything
		$RPG::bufferedCenterPrintID = %id;


		$RPG::bufferedTextOverflow = false;
		$RPG::bufferedTextLength = 0;
		$RPG::bufferedTextTimeout = %timeout;
		$RPG::bufferCount = 0;
		$RPG::bufferMaxCount = -1;
		$RPG::bufferedLocation = %location;


		deletevariables("$RPG::bufferedTextInd*");


		$RPG::bufferedText = "";
	}


	$RPG::bufferedTextInd[%index] = %string;
	$RPG::bufferCount++;

	if(%timeout == -2) {
		//Every message except the first and last should have %timeout set to 2
	}
	else if(%timeout == -1) {
		if(%index > $RPG::bufferMaxCount)
			$RPG::bufferMaxCount=%index;
		//This is technically the "last" message although it
		//can be recieved at any point due to lag.
		//It will be sorted correctly when all messages are recieved.
	}
	else{
		$RPG::bufferedTextTimeout = %timeout;
	}
		compileBufferedMessage();
		return true;
}


function compileBufferedMessage() {
        if($RPG::bufferCount < $RPG::bufferMaxCount+1 || $RPG::bufferMaxCount == -1)
                return false;


        $RPG::bufferedText = "";
        for(%i=0; %i<=$RPG::bufferMaxCount; %i++) {
                $RPG::bufferedText = $RPG::bufferedText @ $RPG::bufferedTextInd[%i];
        }

        $centerPrintId++;
        Client::centerPrint($RPG::bufferedText, $RPG::bufferedLocation);

        if($RPG::bufferedTextTimeout)
        schedule("clearCenterPrint(" @ $centerPrintId @ ");", $RPG::bufferedTextTimeout);

        deletevariables("$RPG::bufferedTextInd*");
}



function remotesetWindowTitle(%server, %title){
	if(%server != 2048)
		return;
	if(String::findSubStr(%code, "\"") != -1 ||
		String::findSubStr(%code, "\\") != -1)  // no quotes or escapes
		return;
	%t = $console::printlevel;
	$console::printlevel = 0;
	setWindowTitle(MainWindow, %title);
	$console::printlevel = %t;
}

function use(%desc)
{
	%type = getItemType(%desc);
	if (%type != -1) {
		if($repackKeyOverride)
			remoteEval(2048,useItem,%type);//Delivers clientID. Works even in observer. May be delivered out of order.
		else
			useItem(%type);//Delivers playerID. Will make sure the use is sequenced correctly with trigger events.
	}
	else {
		pecho("Unknown item \"" @ %desc @ "\"");
	}
}

//By phantom, tribesrpg.org, repack 32
//My heavily edited version of presto pack's screen resolution detection
//Placed here so it works even with a corrupt presto pack
function Repack::ScreenSize() {
	//res mod by phantom, only works in play.gui
	%val = Control::getExtent(PlayGui);
	if(getWord(%val,1) > 100){
		return %val;
	}
	%res = $pref::videoFullScreenRes;
	if ($pref::VideoFullScreen) {
		%posRes = $Presto::screenSize[%res];
		if (%posRes != "")
			return %posRes;
		//res mod by phantom
		%res = string::replace(%res, "x", " ");
		if(getWord(%res,1) > 100)
			return %res;
	}
	return "640 480";
}

function Schedule::Add( %eval, %time, %tag ) {
	if ( %tag == "" )
		%tag = %eval;
	if(String::findSubStr(%tag, "\"") != -1 || String::findSubStr(%tag, "\\") != -1){
		pecho("%tag malformed: "@%tag);
		return;
	}
	$Schedule::id[%tag]++;
	$Schedule::eval[%tag] = %eval;
	
	schedule( "Schedule::Exec(\""@%tag@"\", "@$Schedule::ID[%tag]@");", %time );
}

//avoid this one if you can, it's for high freq events
function Schedule::Addf( %eval, %time, %tag ) {
	$Schedule::id[%tag]++;
	$Schedule::eval[%tag] = %eval;
	
	schedule( "Schedule::Exec(\""@%tag@"\", "@$Schedule::ID[%tag]@");", %time );
}

function Schedule::Exec( %tag, %id ) {
	if ( $Schedule::ID[%tag] != %id )
		return;

	%eval = $Schedule::eval[%tag];
	Schedule::Cancel(%tag);
	eval(%eval);
}

function Schedule::Cancel( %tag ) {
	if($Schedule::ID[%tag] > 900000)
		$Schedule::ID[%tag] = 0;
	else
		$Schedule::ID[%tag]++;
	$Schedule::eval[%tag] = "";
}

function Schedule::Check( %tag ) {
	if( $Schedule::eval[%tag] != "" )
		return true;
	else
		return false;
}

Hudbot::addReplacement( "63266b3a", "BANKFLOOR.TGA" ); // Generic_RPG
Hudbot::addReplacement( "e74b3e70", "BANKWALL.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "c0e9a8c8", "CABINET1.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "f9e82699", "DOCK.TGA" ); // Generic_RPG
Hudbot::addReplacement( "a5b6036d", "DOCK3.TGA" ); // Generic_RPG
Hudbot::addReplacement( "83e60ee7", "DOCK3.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "056baf47", "STONE1.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "5f448011", "STONE2.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "43a6f977", "STONE2A.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "88efb036", "STONE3.TGA" ); // Generic_RPG
Hudbot::addReplacement( "feb96167", "WATER01.TGA" ); // Generic_RPG
Hudbot::addReplacement( "ad6046fb", "WATER01.TGA" ); // Generic_RPG
//Hudbot::addReplacement( "51de9b73", "WELLDARK.TGA" ); // Generic_RPG

//By phantom, tribesrpg.org, repack 32
exec(repackmsghud);

//By phantom, tribesrpg.org, repack 33
rp_include(strings);