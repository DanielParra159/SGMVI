using UnityEngine;

using SGJVI.Level;

namespace SGJVI.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
	public class Enemy : MonoBehaviour {

        public enum ENEMY_TYPES
        {
            Enemy,
            Ally
        }

        private Rigidbody2D myRigidbody = null;
        private Rigidbody2D MyRigidbody
        {
            get
            {
                if (myRigidbody == null)
                {
                    myRigidbody = gameObject.GetComponent<Rigidbody2D>();
                }
                return myRigidbody; 
            }
        }

        [SerializeField]
        private EnemyStats enemyStats;
		
		// Update is called once per frame
		private void Update () {
		
		}

        private void OnDisable()
        {
            myRigidbody.isKinematic = true;
        }

        private void OnEnable()
        {
            MyRigidbody.isKinematic = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ( ((1 << other.gameObject.layer) & Core.GameLayers.EnemiesMask) != 0 )
            {
                Debug.Log("Enemigo colisionando con jugador");
                LevelManager.Instance.CharacterCollideWithEnemy(enemyStats.EnemyType, enemyStats.NumLevelsToMove);
            }
        }
	}
}