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
	yield WaitForSeconds(3);
	var dir : Vector3 = obj.transform.position-transform.position;
	var  effect = Instantiate(bullet,transform.position,transform.rotation);
	effect.rigidbody.velocity=(dir.normalized * 4);
	activate=true;
}