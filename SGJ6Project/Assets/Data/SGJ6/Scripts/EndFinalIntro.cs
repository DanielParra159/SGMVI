using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGJVI {

	public class EndFinalIntro : MonoBehaviour {

		// Use this for initialization
		private void Start () {
            Invoke("End", 4.0f);
		}
		
		// Update is called once per frame
		private void Update () {
		
		}

        private void End()
        {
            SceneManager.LoadScene(0);
        }
	}
}