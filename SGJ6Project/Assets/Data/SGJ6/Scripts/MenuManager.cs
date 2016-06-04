using UnityEngine;
using System.Collections;
using SGJVI.GameLogic;

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

        GameLogic.Instance.StartGame();

        gameObject.SetActive(false);

	}

}
