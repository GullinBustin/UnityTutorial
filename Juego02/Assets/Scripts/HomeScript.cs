using UnityEngine;
using System.Collections;

public class HomeScript : MonoBehaviour {

	//Game

	public GameObject soldier;
	public GameObject archer;
	public GameObject guardian;

	

	public string enemy;

	private bool make=false;
	private RaycastHit encuentro;
	private bool ishit = false;


	public int HP = 300;

	private GameObject myGM ;
	private int money;
	void Start(){
		myGM = GameObject.Find("_"+transform.tag);
	}

	void Update (){
		if (HP < 0)	Destroy (gameObject);
	}

	void OnGUI() {
		//money = myGM.GetComponent<GMPlayer>().Score;
		if (make) {

				if (myGM.GetComponent<GMPlayer>().Score >= 50 && GUI.Button (new Rect (10, 10, 70, 30), "Soldier")){
						myGM.GetComponent<GMPlayer>().Score-=50;
						GameObject temp = Instantiate(soldier,new Vector3(transform.position.x + 5, 0.19F ,transform.position.z+5), Quaternion.identity) as GameObject;
						//temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<CruzadoControler>().enemy = enemy;
						if(ishit) temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				if (myGM.GetComponent<GMPlayer>().Score >= 75 && GUI.Button (new Rect (90, 10, 70, 30), "Archer")){
						myGM.GetComponent<GMPlayer>().Score-=75;
						GameObject temp = Instantiate(archer,new Vector3(transform.position.x + 5, 1.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<MinionControler>().enemy = enemy;
						if(ishit) temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				if (myGM.GetComponent<GMPlayer>().Score >= 100 && GUI.Button (new Rect (170, 10, 70, 30), "Guardian")){
						myGM.GetComponent<GMPlayer>().Score-=100;
						GameObject temp = Instantiate(guardian,new Vector3(transform.position.x + 5, 2.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<MinionControler>().enemy = enemy;
						if(ishit) temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				
		}
	}
	
	void Selected (){
		renderer.material.shader = Shader.Find("Self-Illumin/Diffuse");	
		make = true;
	}

	void Deselected(){
		renderer.material.shader = Shader.Find("Diffuse");	
		make = false;
	}

	void Action (RaycastHit rayhit){
		ishit = true;
		encuentro = rayhit;
	}

	void Damage (int hit){
		HP -= hit;
	}


}
