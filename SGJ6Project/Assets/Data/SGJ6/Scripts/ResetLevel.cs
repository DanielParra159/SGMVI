using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGJVI {

	public class ResetLevel : MonoBehaviour {

		// Use this for initialization
		private void Start () {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
		}
		
	}
}