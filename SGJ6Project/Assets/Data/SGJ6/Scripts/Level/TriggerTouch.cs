using SGJVI.GameLogic;
using UnityEngine;

namespace SGJVI.Level {

    [RequireComponent(typeof(BoxCollider2D))]
	public class TriggerTouch : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameLogic.GameLogic.Instance.PlayerOnTouchTrigger();
            this.gameObject.SetActive(false);
        }
	}
}