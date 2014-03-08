using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {

	public float speed;
	public float damage;
	public Transform target;

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 vect = (target.transform.position - transform.position);
			if (vect.magnitude < 0.5) doDamage (target.transform);
			transform.position += vect.normalized * Time.deltaTime * speed;
		}
		//print (transform.position);
	}

	void doDamage(Transform enemy){
		enemy.SendMessage("Damage",damage,SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}

	void chooseTarget (Transform enemy){
		target = enemy;
	}
}
