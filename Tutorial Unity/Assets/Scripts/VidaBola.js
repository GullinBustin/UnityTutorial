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

function OnTriggerEnter (info : Collider) {
	
	if(info.tag == "Enemy" && !isrest){
		restartlevel();
		rigidbody.velocity.y=8;
		collider.enabled=false;
		animation.Play();
		//transform.localScale= Vector3(0.1,0.1,0.1);
		
 	}
}

function restartlevel (){
	isrest=true;
	audio.clip = gameover;
	audio.Play();
	yield WaitForSeconds (audio.clip.length);
	Application.LoadLevel("Tutorial");
}

