#pragma strict

private var target : RaycastHit;

function Update () {
	var varx = Input.GetAxis("Horizontal");
	var vary = Input.GetAxis("Vertical");
	var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	transform.position += Vector3(1 * varx,0,1* vary) ;
	if(Input.GetMouseButtonDown(0)==true){
		if (Physics.Raycast (ray, target, 100.0)) {
			//target.transform.position -= Vector3(0,0,10);
			//Debug.DrawLine (ray.origin, hit.point);
		}
	}
	if(Input.GetMouseButtonDown(1)==true){
	var hit : RaycastHit;
		if (Physics.Raycast (ray, hit, 100.0)) {
			if(hit.transform.tag == "Player"){
				target.transform.SendMessage("NewEnemy",hit.transform,SendMessageOptions.DontRequireReceiver );
			}else{
				target.transform.SendMessage("NewPoint", Vector2(hit.point.x,hit.point.z),SendMessageOptions.DontRequireReceiver );
			}
		}
	}
}