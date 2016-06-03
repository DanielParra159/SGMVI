﻿using SGJVI.Enemies;
using UnityEngine;

namespace SGJVI.Level
{

	public sealed class Level : MonoBehaviour {

        [SerializeField]
        private Enemy[] enemies;

        [SerializeField]
        private TriggerLevel[] triggersLevel;

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

        public Vector3 GetRandomTriggerLevelPosition()
        {
            return triggersLevel[Random.Range(0, triggersLevel.Length)].transform.position;
        }
	}
}