using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGJVI {

	public class LevelComplete : MonoBehaviour {

		// Use this for initialization
		private void Start () {
		
		}
		
		// Update is called once per frame
		private void Update () {
		
		}

        private void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(3);
        }
	}
}