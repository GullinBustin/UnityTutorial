using UnityEngine;
using System.Collections;

public class MinionControler : MonoBehaviour {
	
	public string enemy;

	public int HP = 100;

	public float speed = 4;

	public int damage = 100;

	[Range(0.1F,100F)]public float damageDistance = 0.1F;

	public int value = 50;

	private Vector2 point;

	private Transform target;

	private CharacterController me = null;
	private GameObject enemyGM ;

	private bool control = false;
	// Use this for initialization
	void Start () {
		me = GetComponent<CharacterController>();
		enemyGM = GameObject.Find("_"+enemy);
		if(!control)point= new Vector2(transform.position.x, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(HP<0){

			enemyGM.GetComponent<GMPlayer>().Score += value;
			Destroy(gameObject);
		}
		Vector2 vect;
		if (target == null) {
			vect = (point - new Vector2 (transform.position.x, transform.position.z));
		} else {
			point= new Vector2(target.transform.position.x,target.transform.position.z);

			vect = (new Vector2 (target.transform.position.x,target.transform.position.z) - new Vector2 (transform.position.x, transform.position.z));
			if(IsEnemy (vect)){
				Fight(target);
				vect=new Vector2(0,0);

			}
		}
		if(vect.magnitude < 0.1) {
			vect=new Vector2(0,0);
		}

		me.Move(new Vector3(vect.x,0,vect.y).normalized * Time.deltaTime * speed);
	}

	
	bool IsEnemy (Vector2 vect){ //Check if we are on the ground. Return true if we are else return null.
		RaycastHit hitinfo;
		if (Physics.Raycast (transform.position, new Vector3 (vect.x, 0, vect.y),out hitinfo, collider.bounds.extents.y + 0.1F)) {
			if(hitinfo.transform == target){
				return true;
			}
		}
		return false;
	}

	/*void NewEnemy (Transform enemy){
		if(enemy.tag == "Player"){
			target=enemy;
		}
	}*/

	void Fight(Transform enemy){
		enemy.SendMessage("Damage",damage,SendMessageOptions.DontRequireReceiver);
	}

	void Action (RaycastHit rayhit){
		if (rayhit.transform.tag == enemy) {
			target = rayhit.transform;		
		} else {
			target = null;
			point = new Vector2(rayhit.point.x, rayhit.point.z);
			control=true;
		}
	}

	void Selected (){
		renderer.material.shader = Shader.Find("Self-Illumin/Diffuse");	
	}

	void Deselected (){
		renderer.material.shader = Shader.Find("Diffuse");
	}

	void NewPoint (Vector2 p){
		target = null;
		point = p;
	}

	void Damage (int hit){
		HP -= hit;
	}

	void ChangeShader(Shader type){
		renderer.material.shader = type;
	}
}


