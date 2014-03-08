using UnityEngine;
using System.Collections;

public class HomeScript : MonoBehaviour {
	//Button

	public Texture btnTexture1;
	public Texture btnTexture0;


	//Game

	public GameObject soldier;
	public GameObject archer;
	public GameObject guardian;

	

	public string enemy;

	private bool make=false;
	private RaycastHit encuentro;
	private bool ishit = false;


	public int HP = 300;

	void Update (){
		if (HP < 0)	Destroy (gameObject);
	}

	void OnGUI() {
		if (make) {

				if (GUI.Button (new Rect (10, 10, 70, 30), "Soldier")){
						GameObject temp = Instantiate(soldier,new Vector3(transform.position.x + 5, 1.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<MinionControler>().enemy = enemy;
						if(ishit) temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				if (GUI.Button (new Rect (90, 10, 70, 30), "Archer")){
						GameObject temp = Instantiate(archer,new Vector3(transform.position.x + 5, 1.1F ,transform.position.z+5), Quaternion.identity) as GameObject;
						temp.renderer.material.color = transform.renderer.material.color;
						temp.tag = transform.tag;
						temp.GetComponent<MinionControler>().enemy = enemy;
						if(ishit) temp.transform.SendMessage("Action",encuentro,SendMessageOptions.DontRequireReceiver);
				}
				if (GUI.Button (new Rect (170, 10, 70, 30), "Guardian")){
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
