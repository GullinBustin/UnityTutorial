using UnityEngine;
using System.Collections;

public class Guerrero : MonoBehaviour {

	public int HP;

	public int damage;
	
	//public string team;

	public string enemy;

	public GameObject isSelect;
	public float SelectHeight;

	public float AutoAtackRadio;

	private Transform target;

	private Vector2 point;

	private GameObject SelectObject; 

	[Range(0.1F,10F)]public float reload;

	private float ReloadTime=0;

	[Range(0.1F,20F)]public float damageDistance;
	
	public int price;

	private NavMeshAgent navigate;

	private int objective=0;
	private GameObject closest;

	// Use this for initialization
	public virtual void Start () {
		navigate = GetComponent<NavMeshAgent> ();
		point = PosicionPlano (transform.position);
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//Debug.Log (point);
		if (HP <= 0) {
			GameObject.Find("_"+enemy).GetComponent<GMPlayer>().Score += price;
			Destroy(gameObject);
		} else {
			if(objective == 0){
				target = FindClosestEnemy ();
			}
			if (target != null) {
				point = calcularPunto(target);//PosicionPlano(target.position);
				navigate.SetDestination(new Vector3 (point.x,0, point.y));
				if(IsEnemy(point-PosicionPlano(transform.position))){
					ataca();
					transform.LookAt(new Vector3(point.x, transform.position.y, point.y));
					navigate.Stop();
					point = calcularPunto(transform);
					if(ReloadTime <= 0){
						Attack(target);
						ReloadTime=reload;
					}else{
						ReloadTime-=Time.deltaTime;
					}
				}else{
					camina ();
				}
			}else{
				//navigate.Resume();
				if((point-PosicionPlano(transform.position)).magnitude <= navigate.stoppingDistance){
					objective=0;
					para();
				}else{
					camina ();
					navigate.SetDestination(new Vector3 (point.x,0, point.y));

				}
			}
		}
	}


	public virtual void Attack (Transform enemy){
		enemy.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Selected (){
		SelectObject = Instantiate (isSelect, transform.position + new Vector3(0, SelectHeight , 0), isSelect.transform.rotation) as GameObject;
		SelectObject.transform.parent = transform;
	}

	public virtual void Deselected (){
		Destroy (SelectObject);
	}

	public virtual void Damage (int hit){
		HP -= hit;
	}

	public virtual void Action (RaycastHit rayhit){
		//Debug.Log("se ha llamado");
		if (rayhit.transform.tag == enemy) {
			target = rayhit.transform;
			navigate.SetDestination(rayhit.transform.position);
			objective=2;
		} else {
			target = null;
			point = PosicionPlano(rayhit.point);
			navigate.SetDestination(new Vector3(point.x,0,point.y)); //(new Vector3 (rayhit.point.x, transform.position.y, rayhit.point.z));
			objective=1;
		}
	}

	bool IsEnemy (Vector2 vect){ 
		RaycastHit hitinfo;
		//print (collider.bounds.extents.z);
		if (Physics.Raycast (new Vector3(transform.position.x , 1 ,transform.position.z), new Vector3 (vect.x, 0, vect.y),out hitinfo, collider.bounds.extents.z + damageDistance + navigate.stoppingDistance)) {
			if(hitinfo.transform == target){
				return true;
			}
		}
		return false;
	}

	Vector2 PosicionPlano(Vector3 V){
		return new Vector2 (V.x, V.z);
	}

	Vector2 calcularPunto(Transform t){ //hacer mas bonito, mirar que sea mas o menos eficiente
		Vector2 sup = new Vector2 (t.collider.bounds.extents.x, t.collider.bounds.extents.z);
		Vector2 normvect = new Vector2 (transform.position.x - t.position.x, transform.position.z - t.position.z).normalized;
		Vector2 sol = PosicionPlano (t.position) + Vector2.Scale (normvect, sup);

		//Debug.Log (PosicionPlano(t.position) + "     " + sol);

		return sol;
	}

	Transform FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(enemy);
		if (gos.Length != 0) {

			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.magnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
			//Debug.Log(distance);
			if(distance < AutoAtackRadio) return closest.transform;
			else return null;
		} else {
			return null;		
		}
	}

	//Estas funiones son un parche
	public virtual void ataca(){
	}
	public virtual void para(){
	}
	public virtual void camina(){
	}

}
