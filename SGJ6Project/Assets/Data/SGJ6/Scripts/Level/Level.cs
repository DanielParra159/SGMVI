using SGJVI.Enemies;
using UnityEngine;

namespace SGJVI.Level
{

	public sealed class Level : MonoBehaviour {

        [SerializeField]
        private Enemy[] enemies;

        private void Awake()
        {
            DisableLevel();
        }

        public void EnableLevel()
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].enabled = true;
            }
        }

        public void DisableLevel()
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].enabled = false;
            }
        }
	}
}