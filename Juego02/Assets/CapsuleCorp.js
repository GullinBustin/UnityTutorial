#pragma strict

var hitpoints = 100;

private var p : Vector2;

var life = 100;

private var target : Component;

function Start(){
	p=Vector2(transform.position.x, transform.position.z);
}

function Update () {
	if(life<0){
	Destroy(gameObject);
	}
	var vect : Vector2;
	if(target != null){
		p=Vector2(target.transform.position.x,target.transform.position.z);
		vect = Vector2(target.transform.position.x,target.transform.position.z) - Vector2(transform.position.x, transform.position.z);
	}else{
		vect = p - Vector2(transform.position.x, transform.position.z);
	}
	if(vect.magnitude < 0.1) {
		vect=Vector2(0,0);
	}
	rigidbody.velocity =Vector3(vect.normalized.x,0,vect.normalized.y) * 4;
}

function OnCollisionStay(info : Collision){
	if(target != null) {
		if(info.transform == target){
		
			Fight(target);
		}
	}
}

function NewPoint (point : Vector2){
	target = null;
	p=point;
}

function NewEnemy ( enemy : Component ){
	if(enemy.tag == "Player"){
		target=enemy;
	}
}

function Fight(enemy : Component){
	enemy.SendMessage("Damage",hitpoints,SendMessageOptions.DontRequireReceiver);
}

function Damage (points : int){
	life-=points;
}