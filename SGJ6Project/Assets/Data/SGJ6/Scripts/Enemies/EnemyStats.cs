using UnityEngine;

namespace SGJVI.Enemies {

    [CreateAssetMenu(fileName = "Enemy", menuName = "SGJVI/Enemy", order = 1)]
	public class EnemyStats : ScriptableObject {

        [SerializeField]
        private int numLevelsToMove = 0;
        public int NumLevelsToMove
        {
            get { return numLevelsToMove; }
        }

        [SerializeField]
        private int moveSpeed = 0;
        public int MoveSpeed
        {
            get { return moveSpeed; }
        }

        [SerializeField]
        private Enemy.ENEMY_TYPES enemyType = 0;
        public Enemy.ENEMY_TYPES EnemyType
        {
            get { return enemyType; }
        }

        [SerializeField]
        private Enemy.ENEMY_DIRECTION enemyDir = 0;
        public Enemy.ENEMY_DIRECTION EnemyDir
        {
            get { return enemyDir; }
        }

	}
}