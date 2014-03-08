#pragma strict

private var target : RaycastHit;

private var shader1 : Shader; 
private var shader2 : Shader; 

function Start (){
	shader1 = Shader.Find("Diffuse"); 
	shader2 = Shader.Find("Self-Illumin/Diffuse"); 
}

function Update () {
	var varx = Input.GetAxis("Horizontal");
	var vary = Input.GetAxis("Vertical");
	transform.position += Vector3(1 * varx,0,1* vary) ;
	
	
	var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	
	if(Input.GetMouseButtonDown(0)==true && Input.mousePosition.y < Screen.height * 0.9F){
		
		if(target.transform != null) target.transform.SendMessage("Deselected",SendMessageOptions.DontRequireReceiver );
		
		Physics.Raycast (ray, target, 100.0);
		
		if(target.transform != null) target.transform.SendMessage("Selected",SendMessageOptions.DontRequireReceiver );
		
	}
	if(Input.GetMouseButtonDown(1)==true){
		
		var hit : RaycastHit;
		
		if (Physics.Raycast (ray, hit, 100.0)) {
			target.transform.SendMessage("Action",hit,SendMessageOptions.DontRequireReceiver);
			
			/*if(hit.transform.tag == "Player"){
				
				target.transform.SendMessage("NewEnemy",hit.transform,SendMessageOptions.DontRequireReceiver );
			
			}else{
				
				target.transform.SendMessage("NewPoint", Vector2(hit.point.x,hit.point.z),SendMessageOptions.DontRequireReceiver );
			}*/
		}
	}
}