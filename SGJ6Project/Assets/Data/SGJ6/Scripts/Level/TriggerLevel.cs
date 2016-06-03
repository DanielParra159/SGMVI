using UnityEngine;

namespace SGJVI.Level {

	public class TriggerLevel : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            LevelManager.Instance.AdvanceLevel();
        }
	}
}