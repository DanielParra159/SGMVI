using UnityEngine;

using SGJVI.Level;

namespace SGJVI.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
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

        private Animator myAnimator = null;

        private ENEMY_DIRECTION currentDir;

        [SerializeField]
        private EnemyStats enemyStats;
        private BoxCollider2D myBoxCollider;
		
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
            myAnimator = gameObject.GetComponent<Animator>();
            currentDir = enemyStats.EnemyDir;
            myAnimator.SetBool("Mirror", ((currentDir == ENEMY_DIRECTION.Left) ? false : true));
        }

		// Update is called once per frame
		private void Update () {
		
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
                Debug.Log("Enemigo colisionando con jugador");
                myBoxCollider.isTrigger = false; 
                LevelManager.Instance.CharacterCollideWithEnemy(enemyStats.EnemyType, enemyStats.NumLevelsToMove);
            } 
            else if (((1 << other.gameObject.layer) & Core.GameLayers.InvisibleEnemyWall) != 0)
            {
                //Camiamos de dirección
                currentDir = currentDir == ENEMY_DIRECTION.Left ? ENEMY_DIRECTION.Right : ENEMY_DIRECTION.Left;
                myAnimator.SetBool("Mirror", ((currentDir == ENEMY_DIRECTION.Left) ? false : true));
            }
        }
	}
}