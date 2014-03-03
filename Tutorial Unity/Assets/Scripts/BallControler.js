#pragma strict

var VelRot = 100;
var jumpvel = 8;
private var fall=false;
private var unavez = true;

var hit1 : AudioClip;
var hit2 : AudioClip;
var hit3 : AudioClip;

function Update () {

 var rotation : float = Input.GetAxis("Horizontal") * VelRot;
 rotation *= Time.deltaTime;
 rigidbody.AddRelativeTorque (Vector3.back * rotation);

 if(Input.GetKeyDown(KeyCode.W) && fall == false){
 	rigidbody.velocity.y = jumpvel;
 	salto();
 }
}

function OnCollisionStay(){
	if(unavez){
		unavez=false;
		var ran = Random.Range(0,3);
		if(ran==0) audio.clip=hit1;
		if(ran==1) audio.clip=hit2;
		if(ran==2) audio.clip=hit3;
		audio.Play();
	}
	fall=false;
}

function salto (){
	yield WaitForSeconds(0.1);
	unavez=true;
	fall=true;
}