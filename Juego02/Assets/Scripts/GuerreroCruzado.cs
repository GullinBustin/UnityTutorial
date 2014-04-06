using UnityEngine;
using System.Collections;

public class GuerreroCruzado : Guerrero {

	// Use this for initialization

	public AudioClip Sound1, Sound2, Sound3;

	private AudioClip randsound;

	public override void camina (){
		//base.camina ();
		animation.Play ("camina2"); 
	}

	public override void para(){
		//base.para ();
		animation.Play ("stop2"); 

	}

	public override void ataca (){
		//base.ataca ();
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
		animation.Play ("ataque2"); 

	}
}
