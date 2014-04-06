using UnityEngine;
using System.Collections;

public class GuerreroGuardian : Guerrero {

	public AudioClip Sound1, Sound2, Sound3;

	private AudioClip randsound;
	// Use this for initialization
	public override void Attack (Transform enemy){
		base.Attack (enemy);
		float rand = Random.Range (0, 3F);
		
		if (rand < 1) {
			randsound=Sound1;
		}
		if (rand <= 2 && rand >=1) {
			randsound=Sound2;
		}
		if (rand > 2) {
			randsound=Sound3;
		}
		if (!audio.isPlaying) {
			audio.clip = randsound;
			audio.Play();
		}
	}
}
