#pragma strict

var CoinEffect : Transform;
var valor=1;
function OnTriggerEnter (info : Collider) {
	
	if(info.tag == "Player"){
		GameMaster02.Score += valor;
		var  effect = Instantiate(CoinEffect,transform.position,transform.rotation);
		Destroy (effect.gameObject , 3);
		Destroy(gameObject);
	}
}