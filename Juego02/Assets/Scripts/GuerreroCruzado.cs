using UnityEngine;
using System.Collections;

public class GuerreroCruzado : Guerrero {

	// Use this for initialization

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
		animation.Play ("ataque2"); 

	}
}
