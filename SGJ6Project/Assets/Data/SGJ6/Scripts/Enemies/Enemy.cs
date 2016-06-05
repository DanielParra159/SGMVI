using UnityEngine;

using SGJVI.Level;
using UnityStandardAssets._2D;
using SGJVI.Characters;

namespace SGJVI.Enemies
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
	public class Enemy : MonoBehaviour {

        public enum ENEMY_TYPES
        {
            Enemy,
            Ally
        }

        public enum ENEMY_DIRECTION
        {
            Left,
            Right
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
        private int numLevelsToMove = 0;
 
        [SerializeField]
        private Enemy.ENEMY_TYPES enemyType = 0;

        [SerializeField]
        private Enemy.ENEMY_DIRECTION enemyDir = 0;
        private float direction;

        private BoxCollider2D myBoxCollider;
        private PlatformerCharacter2D m_Character;

        [SerializeField]
        private float energy;
		
        private void Awake()
        {
            BoxCollider2D[] boxes = gameObject.GetComponents<BoxCollider2D>();
            for (int i = 0; i < boxes.Length; ++i)
            {
                if (boxes[i].isTrigger)
                {
                    myBoxCollider = boxes[i];
                }
            }

            m_Character = GetComponent<PlatformerCharacter2D>();
            direction = -1.0f;
        }

        private void FixedUpdate()
        {
            m_Character.Move(direction, false, false);
        }

        private void OnDisable()
        {
            myRigidbody.isKinematic = true;
            myBoxCollider.isTrigger = false;
        }

        private void OnEnable()
        {
            MyRigidbody.isKinematic = false;
            myBoxCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if ( ((1 << other.gameObject.layer) & Core.GameLayers.PlayerMask) != 0 )
            {
                gameObject.SetActive(false);
                CharacterHUD.Instance.addTime(-energy);
                return;
                Debug.Log("Enemigo colisionando con jugador");
				AudioManager.Instance.PlaySoundUp ();
                myBoxCollider.isTrigger = false; 
                LevelManager.Instance.CharacterCollideWithEnemy(enemyType, numLevelsToMove);
            } 
            else if (((1 << other.gameObject.layer) & Core.GameLayers.InvisibleEnemyWall) != 0)
            {
                direction *= -1;
            }
        }
	}
}