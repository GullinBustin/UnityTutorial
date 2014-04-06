using UnityEngine;
using System.Collections;

public class EnemyIA : MonoBehaviour {

	//Game

	public GameObject soldier;
	public GameObject archer;
	public GameObject guardian;
	
	
	
	public string enemy;
	
	private RaycastHit encuentro;
	private bool ishit = false;
	
	
	public int HP = 300;
	
	private GameObject myGM ;
	private int money;

	private int randsel=0;

	public GameObject End;

	void Start(){
		myGM = GameObject.Find("_"+transform.tag);
	}
	
	// Update is called once per frame
	void Update () {
		if (HP < 0) {
			End.SendMessage("Win",SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
		if(randsel == 0){
			randsel = (int) Random.Range(0,2.999999F) ;
		}
		if (myGM.GetComponent<GMPlayer>().Score >= 50 && randsel == 0){
			randsel=0;
			myGM.GetComponent<GMPlayer>().Score-=50;
			GameObject temp = Instantiate(soldier,new Vector3(transform.position.x + 5, 0.19F ,transform.position.z+5), Quaternion.identity) as GameObject;
			//temp.renderer.material.color = transform.renderer.material.color;
			temp.tag = transform.tag;
			temp.GetComponent<Guerrero>().enemy = enemy;
			if(transform.tag == "Enemy") temp.GetComponent<Guerrero>().AutoAtackRadio = Mathf.Infinity; 
			if(ishit) StartCoroutine(SendMeeting(temp));
			
		}
		if (myGM.GetComponent<GMPlayer>().Score >= 75 && randsel == 1){
			randsel=0;
			myGM.GetComponent<GMPlayer>().Score-=75;
			GameObject temp = Instantiate(archer,new Vector3(transform.position.x + 5, 1.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
			temp.renderer.material.color = transform.renderer.material.color;
			temp.tag = transform.tag;
			temp.GetComponent<Guerrero>().enemy = enemy;
			if(transform.tag == "Enemy") temp.GetComponent<Guerrero>().AutoAtackRadio = Mathf.Infinity; 
			if(ishit) StartCoroutine(SendMeeting(temp));
		}
		if (myGM.GetComponent<GMPlayer> ().Score >= 100 && randsel == 2) {
			randsel=0;
			myGM.GetComponent<GMPlayer> ().Score -= 100;
			GameObject temp = Instantiate (guardian, new Vector3 (transform.position.x + 5, 2.1F, transform.position.z + 5), Quaternion.identity) as GameObject;
			temp.renderer.material.color = transform.renderer.material.color;
			temp.tag = transform.tag;
			temp.GetComponent<Guerrero> ().enemy = enemy;
			if (transform.tag == "Enemy")
			temp.GetComponent<Guerrero> ().AutoAtackRadio = Mathf.Infinity; 
		}

	}

	void Damage (int hit){
		HP -= hit;
	}
	IEnumerator SendMeeting(GameObject temp) { 
		yield return new WaitForSeconds(0.1F); 
		temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
	}

}
