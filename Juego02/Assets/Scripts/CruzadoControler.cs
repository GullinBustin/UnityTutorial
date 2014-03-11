using UnityEngine;
using System.Collections;

public class CruzadoControler : MonoBehaviour {

	public string enemy;

	public int HP = 100;

	public float speed = 4;

	public int damage = 100;

	[Range(0.1F,10F)]public float reload = 3F;
	private float isReload; 
	[Range(0.1F,20F)]public float damageDistance = 0.1F;
	public bool isArcher = false;
	public GameObject arrow;

	public int value = 50;

	private Vector2 point;

	private Transform target;

	private CharacterController me = null;
	private GameObject enemyGM ;

	private bool control = false;

	private GameObject closest;
	private float findtime;
	private bool objective = false;
	// Use this for initialization
	void Start () {
		isReload = reload;
		me = GetComponent<CharacterController>();
		enemyGM = GameObject.Find("_"+enemy);
		if(!control)point= new Vector2(transform.position.x, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(HP<=0){

			enemyGM.GetComponent<GMPlayer>().Score += value;
			Destroy(gameObject);
		}
		if (findtime > 1F && objective==false) {
			target = FindClosestEnemy ();
			findtime = 0;
		} else {
			findtime += Time.deltaTime;
		}
		Vector2 vect;
		if (target == null) {
			vect = (point - new Vector2 (transform.position.x, transform.position.z));
			transform.LookAt (transform.position + new Vector3(vect.x,0,vect.y) );
			//animation.Play("camina2");
		} else {
			point= new Vector2(target.transform.position.x,target.transform.position.z);

			vect = (new Vector2 (target.transform.position.x,target.transform.position.z) - new Vector2 (transform.position.x, transform.position.z));
			transform.LookAt (transform.position + new Vector3(vect.x,0,vect.y) );
			if(IsEnemy (vect)){
				//animation.Play("ataque2");
				if(isReload >= reload){
					Fight(target);
					isReload=0;
					//print (Time.time);
				}
				vect=new Vector2(0,0);

			}
			//animation.Play("camina2");
			if(isReload < reload){
				isReload += Time.deltaTime;
			}
		}
		if(vect.magnitude < 0.1) {
			vect=new Vector2(0,0);
			objective=false;
			if(target == null)animation.Play("stop2");
			else animation.Play("ataque2");
		}else animation.Play("camina2"); 

		me.Move(new Vector3(vect.x,0,vect.y).normalized * Time.deltaTime * speed);
	}

	
	bool IsEnemy (Vector2 vect){ //Check if we are on the ground. Return true if we are else return null.
		RaycastHit hitinfo;
		//print (collider.bounds.extents.z);
		if (Physics.Raycast (new Vector3(transform.position.x , 1 ,transform.position.z), new Vector3 (vect.x, 0, vect.y),out hitinfo, collider.bounds.extents.z + damageDistance)) {
			if(hitinfo.transform == target){
				return true;
			}
		}
		return false;
	}

	Transform FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(enemy);
		if (gos.Length != 0) {
			//GameObject closest;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
						closest = go;
						distance = curDistance;
				}
			}
			return (closest.transform);
		} else {
			return null;		
		}
	}

	/*void NewEnemy (Transform enemy){
		if(enemy.tag == "Player"){
			target=enemy;
		}
	}*/

	void Fight(Transform enemy){
		if (! isArcher)	enemy.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
		else {
			GameObject temp = Instantiate (arrow, transform.position, Quaternion.identity) as GameObject;
			//temp.renderer.material.color = new Color(1,0,0);
			//temp.transform.SendMessage ("chooseTarget", enemy, SendMessageOptions.DontRequireReceiver);
			temp.GetComponent<ArrowControl>().target = enemy;
			temp.GetComponent<ArrowControl>().damage = damage;
		}
	}

	void Action (RaycastHit rayhit){
		objective = true;
		if (rayhit.transform.tag == enemy) {
			target = rayhit.transform;		
		} else {
			target = null;
			point = new Vector2(rayhit.point.x, rayhit.point.z);
			control=true;
		}
	}

	void Selected (){
		//renderer.material.shader = Shader.Find("Self-Illumin/Diffuse");	
	}

	void Deselected (){
		//renderer.material.shader = Shader.Find("Diffuse");
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


