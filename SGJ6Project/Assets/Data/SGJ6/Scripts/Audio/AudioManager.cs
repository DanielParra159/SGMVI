using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private AudioSource audioSource;

	public enum listaObjetos {coche, casa};


	[SerializeField]
	private AudioClip jump;

	[SerializeField]
	private AudioClip up;

	[SerializeField]
	private AudioClip down;

	[SerializeField]
	private AudioClip breakBlock;

	private void Awake () {

		audioSource = GetComponent<AudioSource> ();

		listaObjetos objetos = new listaObjetos { };

	}


	public void PlaySoundJump () {

		audioSource.PlayOneShot (jump);

	}

	public void PlaySoundUp () {

		audioSource.PlayOneShot (up);

	}

	public void PlaySoundDown () {

		audioSource.PlayOneShot (down);

	}

	public void PlaySoundBreackBlock () {

		audioSource.PlayOneShot (breakBlock);

	}


}
