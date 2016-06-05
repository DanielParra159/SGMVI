using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGJVI {

	public class EndIntro : MonoBehaviour {

        public GameObject frame1;
		// Use this for initialization
		private void Start () {
		    Invoke("frame2", 4.0f);
		}
		

        public void frame2()
        {
            frame1.SetActive(false);
            Invoke("EndIntro1", 4.0f);
        }

        public void EndIntro1()
        {
            SceneManager.LoadScene(1);
        }
	}
}