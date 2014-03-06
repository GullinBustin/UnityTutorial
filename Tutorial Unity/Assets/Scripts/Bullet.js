#pragma strict

function Start () {
 Destroy(gameObject,10);
}


function OnTriggerEnter (info : Collider) {
	if(info.tag == "Enemy" || info.tag == "Money")
	{
	}
	else Destroy(gameObject);
}