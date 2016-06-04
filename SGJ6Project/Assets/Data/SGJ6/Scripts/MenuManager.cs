using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public void BeginButton () {

		Debug.Log ("Begin Game");

		BeginGame ();

	}

	public void QuitButton () {

		Debug.Log ("Quit game");

		Application.Quit ();

	}

	void BeginGame () {

		Debug.Log ("Start Game");

	}

}
