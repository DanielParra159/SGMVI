using UnityEngine;

namespace SGJVI.Level {

    [RequireComponent(typeof(BoxCollider2D))]
	public class TriggerLevel : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & Core.GameLayers.PlayerMask) != 0)
            {
                Debug.Log("Jugador colisionando con TriggerLevel");
                LevelManager.Instance.AdvanceLevel();
            }
        }
	}
}