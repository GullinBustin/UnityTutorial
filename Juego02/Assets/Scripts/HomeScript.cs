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

	public GameObject End;

	public Material HomeSelect , HomeDeselect ;

	void Start(){
		myGM = GameObject.Find("_"+transform.tag);

	}

	void Update (){
		if (HP < 0) {
			End.SendMessage("Lose",SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}

	void OnGUI() {
		//money = myGM.GetComponent<GMPlayer>().Score;
		if (make) {

				if (myGM.GetComponent<GMPlayer>().Score >= 50 && GUI.Button (new Rect (10, 10, 70, 30), "Soldier")){
						myGM.GetComponent<GMPlayer>().Score-=50;
						GameObject temp = Instantiate(soldier,new Vector3(transform.position.x + 5, 0.19F ,transform.position.z+5), Quaternion.identity) as GameObject;
						//temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<Guerrero>().enemy = enemy;
						//if(transform.tag == "Enemy") temp.GetComponent<Guerrero>().AutoAtackRadio = Mathf.Infinity; 
						if(ishit) StartCoroutine(SendMeeting(temp));
						
				}
				if (myGM.GetComponent<GMPlayer>().Score >= 75 && GUI.Button (new Rect (90, 10, 70, 30), "Archer")){
						myGM.GetComponent<GMPlayer>().Score-=75;
						GameObject temp = Instantiate(archer,new Vector3(transform.position.x + 5, 1.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<Guerrero>().enemy = enemy;
						//if(transform.tag == "Enemy") temp.GetComponent<Guerrero>().AutoAtackRadio = Mathf.Infinity; 
						if(ishit) StartCoroutine(SendMeeting(temp));
				}
				if (myGM.GetComponent<GMPlayer>().Score >= 100 && GUI.Button (new Rect (170, 10, 70, 30), "Guardian")){
						myGM.GetComponent<GMPlayer>().Score-=100;
						GameObject temp = Instantiate(guardian,new Vector3(transform.position.x + 5, 2.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<Guerrero>().enemy = enemy;
						//if(transform.tag == "Enemy") temp.GetComponent<Guerrero>().AutoAtackRadio = Mathf.Infinity; 
						if(ishit) StartCoroutine(SendMeeting(temp));//temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				
		}
	}
	
	void Selected (){
		renderer.material = HomeSelect;	
		make = true;
	}

	void Deselected(){
		renderer.material = HomeDeselect;	
		make = false;
	}

	void Action (RaycastHit rayhit){
		ishit = true;
		encuentro = rayhit;
	}

	void Damage (int hit){
		HP -= hit;
	}
	IEnumerator SendMeeting(GameObject temp) { 
		yield return new WaitForSeconds(0.1F); 
		temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
	}


}
