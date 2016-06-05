using UnityEngine;

namespace SGJVI.Level {

    [RequireComponent(typeof(BoxCollider2D))]
	public class TriggerLevel : MonoBehaviour {

        private BoxCollider2D myBoxCollider;

        private void Awake()
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnDisable()
        {
            myBoxCollider.isTrigger = false;
        }

        private void OnEnable()
        {
            myBoxCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & Core.GameLayers.PlayerMask) != 0)
            {
                Debug.Log("Jugador colisionando con TriggerLevel");
				AudioManager.Instance.PlaySoundDown ();
                myBoxCollider.isTrigger = false; 
                LevelManager.Instance.AdvanceLevel();
            }
        }
	}
}