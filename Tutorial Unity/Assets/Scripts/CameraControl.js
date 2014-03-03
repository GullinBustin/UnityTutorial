#pragma strict

var Target : Transform;
var distance = -10;
var lift = 1.5;

function Update (){
 	transform.position = Target.position + Vector3(0,lift,distance);
 	transform.LookAt(Target);
}