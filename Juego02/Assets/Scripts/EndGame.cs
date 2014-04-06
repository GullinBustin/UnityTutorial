using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization

	public AudioClip WinSound, LoseSound;
	public bool win;

	// Update is called once per frame
	void Win(){
		audio.clip = WinSound;
		audio.Play ();
		StartCoroutine (QuitGame());
	}

	void Lose(){
		audio.clip = LoseSound;
		audio.Play ();
		StartCoroutine (QuitGame());
	}

	IEnumerator QuitGame() { 
		yield return new WaitForSeconds(audio.clip.length); 
		Application.Quit ();
	}
}
