using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {

	public float speed;
	public float damage;
	public Transform target;

	public AudioClip Sound1, Sound2, Sound3;

	private AudioClip randsound;
	private bool exist=true;

	// Update is called once per frame
	void Update () {

		if (target != null) {
			if(exist){
				Vector3 vect = (target.transform.position - transform.position);
				if (vect.magnitude < 0.5)
						doDamage (target.transform);
				transform.position += vect.normalized * Time.deltaTime * speed;
			}
		} else {
			Destroy(gameObject);
		}

		//print (transform.position);
	}

	void doDamage(Transform enemy){
		enemy.SendMessage("Damage",damage,SendMessageOptions.DontRequireReceiver);
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
		exist = false;

		//audio.clip = randsound;
		//audio.Play ((ulong)randsound.length);
		//Destroy (gameObject);
		StartCoroutine( DestroyMe (randsound));
	}

	void chooseTarget (Transform enemy){
		target = enemy;
	}

	IEnumerator DestroyMe(AudioClip sound) { 
		MeshRenderer me = GetComponent<MeshRenderer> ();
		me.enabled = false;

		audio.clip = sound;
		audio.Play ();
		yield return new WaitForSeconds(sound.length); 
		Destroy (gameObject);
	}
}
