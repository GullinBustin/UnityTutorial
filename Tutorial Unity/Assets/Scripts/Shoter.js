#pragma strict

var obj : Transform;
var bullet : Component;
private var activate=true;

function Update () {
	if(activate){
		activate=false;
		huha();
	}
}

function huha(){
	yield WaitForSeconds(1.5);
	var dir : Vector3 = obj.transform.position-transform.position;
	var  effect = Instantiate(bullet,transform.position,transform.rotation);
	effect.rigidbody.velocity=(dir.normalized * 6);
	activate=true;
}