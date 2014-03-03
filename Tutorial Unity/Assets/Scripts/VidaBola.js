#pragma strict

var maxcaida = -10;
private var isrest = false;

var gameover : AudioClip;

function Update () {

	if(transform.position.y <= maxcaida){
		
		if(isrest == false){
		restartlevel();
		}
		
	}

}

function restartlevel (){
	isrest=true;
	audio.clip = gameover;
	audio.Play();
	yield WaitForSeconds (audio.clip.length);
	Application.LoadLevel("Tutorial");
}