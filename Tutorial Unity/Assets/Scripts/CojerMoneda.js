#pragma strict

var CoinEffect : Transform;
var valor=1;
function OnTriggerEnter (info : Collider) {
	
	if(info.tag == "Player"){
		GameMaster.Score += valor;
		var  effect = Instantiate(CoinEffect,transform.position,transform.rotation);
		Destroy (effect.gameObject , 3);
		Destroy(gameObject);
	}
}