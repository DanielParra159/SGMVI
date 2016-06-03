using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Common.Utils;

namespace SGJVI.Level {

	public class LevelManager : MonoBehaviour
    {

        #region Singelton
        private static LevelManager instance = null;
        public static LevelManager Instance
        {
            get { return instance; }
        }
        #endregion

        [SerializeField]
        private float levelTransitionDuration = 2.0f;
        [SerializeField]
        private GameObject initialLevel;
        private GameObject initialLevelInstance;
        [SerializeField]
        private GameObject [] levels;
        private List<GameObject> currentLevels = new List<GameObject>(3);
        private Transform rootLevel;
        private Transform auxRootLevel;
        private int levelsOutScreen = 0;
        private int lastRandom = -1;

        [SerializeField]
        private int levelHeight = 900;

        private Character character;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                StaticRandom.Init(1);

                for (int i = 0; i < levels.Length; ++i)
                {
                    levels[i].SetActive(false);
                    levels[i].CreatePool(true,1);
                }
                initialLevel.CreatePool(true, 1);
                rootLevel = new GameObject("rootLevel").transform;
                auxRootLevel = new GameObject("auxRootLevel").transform;
            }
            else
            {
                Debug.LogWarning("Se está creando una segunda instancia de LevelManager");
                Destroy(gameObject);
            }
        }

		// Use this for initialization
		private void Start () {
            if (instance == this)
            {
                ResetLevel();


                Invoke("AdvanceLevel", 1.0f);
                Invoke("AdvanceLevel", 4.0f);
            }
		}

        private void ResetLevel()
        {
            initialLevelInstance = initialLevel.SpawnPool(new Vector3(0, 0, 0));
            initialLevelInstance.transform.SetParent(rootLevel);
            currentLevels.Add(initialLevelInstance);

            int auxRandom = StaticRandom.RandomRange(0, levels.Length);
            lastRandom = auxRandom;
            GameObject auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, -levelHeight / 100, 0));
            currentLevels.Add(auxGameObject);
            auxGameObject.transform.SetParent(rootLevel);
            auxGameObject.SetActive(true);
            while (lastRandom == auxRandom)
            {
                auxRandom = StaticRandom.RandomRange(0, levels.Length);
            }
            auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, (-2 * levelHeight) / 100, 0));
            currentLevels.Add(auxGameObject);
            auxGameObject.transform.SetParent(rootLevel);
            auxGameObject.SetActive(true);
            lastRandom = auxRandom;
        }

        public void AdvanceLevel()
        {
            for (int i = 0; i < currentLevels.Count; ++i)
            {
                currentLevels[i].GetComponent<Level>().DisableLevel();
            }
            rootLevel.DOMoveY(9, levelTransitionDuration).OnComplete(NotyfyAllLevelsTransitionComplete);
        }

        private void NotyfyAllLevelsTransitionComplete()
        {
            ++levelsOutScreen;
            for (int i = 0; i < currentLevels.Count; ++i)
            {
                currentLevels[i].GetComponent<Level>().EnableLevel();
                currentLevels[i].transform.SetParent(auxRootLevel);
            }

            rootLevel.transform.position = Vector3.zero;

            for (int i = 0; i < currentLevels.Count; ++i)
            {
                currentLevels[i].GetComponent<Level>().EnableLevel();
                currentLevels[i].transform.SetParent(rootLevel);
            }

            if (levelsOutScreen > 1)
            {
                --levelsOutScreen;
                currentLevels[0].RecyclePool();
                currentLevels.RemoveAt(0);

                int auxRandom = lastRandom;
                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
                GameObject auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, -levelHeight / 100, 0));
                currentLevels.Add(auxGameObject);
                auxGameObject.transform.SetParent(rootLevel);
                auxGameObject.SetActive(true);
                lastRandom = auxRandom;
                Invoke("AdvanceLevel", 4.0f);
            }
        }

        public void CharacterCollideWithEnemy(Enemies.Enemy.ENEMY_TYPES enemyType, int levelsToMove)
        {
            if (enemyType == Enemies.Enemy.ENEMY_TYPES.Ally)
            {
                levelsToMove = -levelsToMove;
            }
        }
	}
}