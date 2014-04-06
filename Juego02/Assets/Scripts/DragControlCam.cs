using UnityEngine;
using System.Collections;

public class DragControlCam : MonoBehaviour {

	public string player;

	//public Texture rec;
	//private GameObject[] targets;
	private ArrayList targets;

	private bool SelectMode=false;

	private Vector2 IniPoint , FinPoint;

	private Vector2 WindPos1;
	private Vector2 WindPos2;

	void Start () {
		targets = new ArrayList();
	}

	void Update () {
		if (Input.GetKey("escape")) {
			Debug.Log("pressss");
			Application.Quit();
		}
		float velx = Input.GetAxis("Horizontal");
		float vely = Input.GetAxis("Vertical");
		if (Input.mousePosition.y < Screen.height * 0.1F) {
			vely=-0.3F;
		}
		if (Input.mousePosition.y > Screen.height * 0.8F && Input.mousePosition.y < Screen.height * 0.9F) {
			vely=0.3F;
		}
		if (Input.mousePosition.x < Screen.width * 0.1F && Input.mousePosition.y < Screen.height * 0.9F) {
			velx=-0.3F;
		}
		if (Input.mousePosition.x > Screen.width * 0.9F && Input.mousePosition.y < Screen.height * 0.9F) {
			velx=0.3F;
		}
		transform.position += new Vector3 (1 * velx, 0, 1 * vely);

		if (Input.GetMouseButtonDown (0) == true && Input.mousePosition.y < Screen.height * 0.9F) {
			foreach (Transform character in targets)
			{
				if(character != null){
					character.SendMessage("Deselected",SendMessageOptions.DontRequireReceiver);
				}
			}
			targets = new ArrayList();
			SelectMode=true;
			WindPos1=Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (WindPos1);
			RaycastHit hitPoint;
			Physics.Raycast (ray,out hitPoint, 100.0F);
			IniPoint = new Vector2 (hitPoint.point.x , hitPoint.point.z);
			if(hitPoint.transform != null && hitPoint.transform.tag == player ) targets.Add(hitPoint.transform);
		}
		if (Input.GetMouseButtonUp (0) == true && SelectMode) {
			SelectMode=false;
			WindPos2 = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (WindPos2);
			RaycastHit hitPoint;
			Physics.Raycast (ray,out hitPoint, 100.0F);
			FinPoint = new Vector2 (hitPoint.point.x , hitPoint.point.z);
			Vector2 maxvect= new Vector2 ( Mathf.Max(IniPoint.x, FinPoint.x) , Mathf.Max(IniPoint.y, FinPoint.y) );
			Vector2 minvect= new Vector2 ( Mathf.Min(IniPoint.x, FinPoint.x) , Mathf.Min(IniPoint.y, FinPoint.y) );
			GameObject[] AllObject = GameObject.FindGameObjectsWithTag(player); //FindObjectsOfType(typeof(GameObject)) as GameObject[];//

			for (int i=0; i<AllObject.Length ; i++){
				if(AllObject[i].transform.position.x > minvect.x && AllObject[i].transform.position.x < maxvect.x && AllObject[i].transform.position.z > minvect.y && AllObject[i].transform.position.z < maxvect.y){
					targets.Add(AllObject[i].transform);
				}
			}

			foreach (Transform character in targets)
			{
				character.SendMessage("Selected",SendMessageOptions.DontRequireReceiver);
			}

		}

		if (Input.GetMouseButtonDown (1) == true) {
			foreach (Transform character in targets)
			{
				if(character != null){
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hitPoint;
					if(Physics.Raycast (ray,out hitPoint, 100.0F)){
						character.SendMessage("Action", hitPoint ,SendMessageOptions.DontRequireReceiver);
					}
				}
			}		
		}
	}

	void OnGUI() {
		WindPos2 = Input.mousePosition;
		if (SelectMode) {
			GUI.Box (new Rect ( WindPos1.x, Screen.height -  WindPos1.y, WindPos2.x - WindPos1.x, -WindPos2.y + WindPos1.y), "");
		}
	}
}
