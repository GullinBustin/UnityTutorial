using UnityEngine;
using System.Collections;

public class GMPlayer : MonoBehaviour {

	public int Score = 0;

	public int OffsetY = 10;
	public int SizeX = 100;
	public int SizeY = 50;
	public int Xposition =0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.Box (new Rect(Screen.width/2-SizeX/2+Xposition, OffsetY  , SizeX,SizeY), "Money: " + Score+ "\n" + transform.tag);

	}
}
