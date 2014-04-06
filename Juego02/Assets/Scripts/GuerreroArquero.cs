using UnityEngine;
using System.Collections;

public class GuerreroArquero : Guerrero {

	public GameObject arrow;

	public AudioClip Sound;

	// Use this for initialization
	public override void Attack (Transform enemy){
		audio.clip = Sound;
		if (!audio.isPlaying)
						audio.Play ();
		GameObject temp = Instantiate (arrow, transform.position, Quaternion.identity) as GameObject;
		temp.GetComponent<ArrowControl>().target = enemy;
		temp.GetComponent<ArrowControl>().damage = damage; 
	}
}
