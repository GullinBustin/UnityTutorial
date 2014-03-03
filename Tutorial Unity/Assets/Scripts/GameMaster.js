#pragma strict

static var Score : int = 0;

var OffsetY = 40;
var SizeX = 40;
var SizeY = 40;

var endsong : AudioClip;
private var onetime = true;

function Start(){
	Score =0;
}

function Update () {

	if(Score == 5 && onetime){
		nextlevel();
	}

}

function OnGUI (){
	GUI.Box (new Rect(Screen.width/2-SizeX/2, OffsetY  , SizeX,SizeY), "Score: " + Score);	
}

function nextlevel (){
	onetime=false;
	audio.clip = endsong;
	audio.Play();
	yield WaitForSeconds(audio.clip.length);
	Application.LoadLevel("Tutorial02");
}