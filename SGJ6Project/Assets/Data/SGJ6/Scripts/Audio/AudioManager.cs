using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	#region Singelton
	private static AudioManager instance = null;
	public static AudioManager Instance
	{
		get { return instance; }
	}
	#endregion

	private AudioSource audioSource;

	[SerializeField]
	private AudioClip jump;

	[SerializeField]
	private AudioClip up;

	[SerializeField]
	private AudioClip down;

	[SerializeField]
	private AudioClip breakBlock;

	private void Awake () {

		if (instance == null)
		{
			
			instance = this;

		}
		else
		{
			
			Debug.LogWarning("Se está creando una segunda instancia de LevelManager");
			Destroy(gameObject);

		}

		audioSource = GetComponent<AudioSource> ();

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

	public void PlaySoundBreakBlock () {

		audioSource.PlayOneShot (breakBlock);

	}


}
