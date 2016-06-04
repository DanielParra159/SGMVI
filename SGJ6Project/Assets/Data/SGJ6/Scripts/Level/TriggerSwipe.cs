using UnityEngine;

namespace SGJVI.Level
{
    [RequireComponent(typeof(BoxCollider2D))]
	public class TriggerSwipe : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameLogic.GameLogic.Instance.PlayerOnTouchSwipe();
            this.gameObject.SetActive(false);
        }
	}
}