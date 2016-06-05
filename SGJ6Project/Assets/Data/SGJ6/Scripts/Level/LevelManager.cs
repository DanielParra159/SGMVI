using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Common.Utils;
using SGJVI.Characters;

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
        private float normalLevelTransitionDuration = 2.0f;
        [SerializeField]
        private float fastLevelTransitionDuration = 1.0f;
        private float currentLevelTransitionDuration;
        [SerializeField]
        private GameObject initialLevel;
        private GameObject initialLevelInstance;
        [SerializeField]
        private GameObject secondLevel;
        private GameObject secondLevelInstance;
        [SerializeField]
        private GameObject [] levels;
        //private List<GameObject> currentLevels = new List<GameObject>(3);
        private const int MAX_PREVIOUS_LEVELS = 10;
        private List<GameObject> previousLevels = new List<GameObject>(MAX_PREVIOUS_LEVELS);
        private int levelIndex = 0;
        private Transform rootLevel;
        private Transform auxRootLevel;
        private int levelsOutScreen = 0;
        private int lastRandom = -1;

        [SerializeField]
        private int levelHeight = 900;

        private Character character;
        public Character MainCharacter
        {
            set {
                character = value;
                character.transform.SetParent(rootLevel);
            }
        }

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
                secondLevel.CreatePool(true, 1);
                rootLevel = new GameObject("rootLevel").transform;
                auxRootLevel = new GameObject("auxRootLevel").transform;
            }
            else
            {
                Debug.LogWarning("Se está creando una segunda instancia de LevelManager");
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

		// Use this for initialization
		private void Start () {
            if (instance == this)
            {
                ResetLevel();


                //Invoke("AdvanceLevel", 1.0f);
                //Invoke("AdvanceLevel", 4.0f);
            }
		}

        private void ResetLevel()
        {
            currentLevelTransitionDuration = normalLevelTransitionDuration;
            levelIndex = 0;

            for (int i = 0; i < previousLevels.Count; ++i)
            {
                if (previousLevels[i].activeSelf)
                {
                    previousLevels[i].RecyclePool();
                }
            }
            previousLevels.Clear();

            initialLevelInstance = initialLevel.SpawnPool(new Vector3(0, 0, 0));
            initialLevelInstance.transform.SetParent(rootLevel);
            initialLevelInstance.SetActive(true);
            previousLevels.Add(initialLevelInstance);

            secondLevelInstance = secondLevel.SpawnPool(new Vector3(0, -9, 0));
            secondLevelInstance.transform.SetParent(rootLevel);
            secondLevelInstance.SetActive(true);
            previousLevels.Add(secondLevelInstance);

            int auxRandom = StaticRandom.RandomRange(0, levels.Length);
            for (int i = 0; i < 8; ++i)
            {
                lastRandom = auxRandom;
                GameObject auxGameObject = levels[auxRandom].SpawnPool();
                auxGameObject.SetActive(false);
                previousLevels.Add(auxGameObject);
                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
            }
            for (int i = 2; i < 4; ++i)
            {
                ++levelIndex;
                previousLevels[i].transform.transform.position = new Vector3(0, (-i*levelHeight) / 100, 0);
                previousLevels[i].transform.SetParent(rootLevel);
                previousLevels[i].SetActive(true);
            }

            /*initialLevelInstance = initialLevel.SpawnPool(new Vector3(0, 0, 0));
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
            lastRandom = auxRandom;*/
        }

        public void BackLevel()
        {
            Debug.Log("retrocedemos un nivel");
            for (int i = 0; i < levelIndex; ++i)
            {
                if (previousLevels[i].activeSelf)
                    previousLevels[i].GetComponent<Level>().DisableLevel();
            }
            rootLevel.DOMoveY(-9, currentLevelTransitionDuration).OnComplete(NotyfyBackLevelTransitionComplete);
        }

        private void NotyfyBackLevelTransitionComplete()
        {
            for (int i = 0; i < previousLevels.Count; ++i)
            {
                if (previousLevels[i].activeSelf)
                {
                    previousLevels[i].GetComponent<Level>().EnableLevel();
                    previousLevels[i].transform.SetParent(auxRootLevel);
                }
            }
            character.transform.SetParent(auxRootLevel);
            --levelIndex;
            rootLevel.transform.position = Vector3.zero;

            for (int i = 0; i < previousLevels.Count; ++i)
            {
                if (previousLevels[i].activeSelf)
                {
                    previousLevels[i].GetComponent<Level>().EnableLevel();
                    previousLevels[i].transform.SetParent(rootLevel);
                }
            }
            character.transform.SetParent(rootLevel);
        }

        public void AdvanceLevel()
        {
            Debug.Log("avanzamos un nivel");
            for (int i = 0; i < levelIndex; ++i)
            {
                if (previousLevels[i].activeSelf)
                    previousLevels[i].GetComponent<Level>().DisableLevel();
            }
            rootLevel.DOMoveY(9, currentLevelTransitionDuration).OnComplete(NotyfyAdvanceLevelTransitionComplete);
        }

        private void NotyfyAdvanceLevelTransitionComplete()
        {
            ++levelsOutScreen;
            for (int i = 0; i < previousLevels.Count; ++i)
            {
                if (previousLevels[i].activeSelf)
                {
                    previousLevels[i].GetComponent<Level>().EnableLevel();
                    previousLevels[i].transform.SetParent(auxRootLevel);
                }
            }
            character.transform.SetParent(auxRootLevel);

            rootLevel.transform.position = Vector3.zero;

            for (int i = 0; i < previousLevels.Count; ++i)
            {
                if (previousLevels[i].activeSelf)
                {
                    previousLevels[i].GetComponent<Level>().EnableLevel();
                    previousLevels[i].transform.SetParent(rootLevel);
                }
            }
            character.transform.SetParent(rootLevel);
            ++levelIndex; 
            previousLevels[levelIndex].GetComponent<Level>().LevelComplete();
            
            if (levelIndex > MAX_PREVIOUS_LEVELS)
            {
                Debug.Log("previousLevels completa");
                previousLevels[0].RecyclePool();
                previousLevels.RemoveAt(0);
                --levelIndex;
            }

            GameObject auxGameObject;
            if (levelIndex == MAX_PREVIOUS_LEVELS)
            {
                int auxRandom = lastRandom;
                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
                auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, (-2*levelHeight) / 100, 0));
                previousLevels.Add(auxGameObject);
                lastRandom = auxRandom;
            }
            else
            {
                previousLevels[levelIndex].transform.position = new Vector3(0, (-2*levelHeight) / 100, 0);
            }

            previousLevels[levelIndex].transform.SetParent(rootLevel);
            previousLevels[levelIndex].SetActive(true);

            /*if (levelsOutScreen > 1)
            {
                Debug.Log("apagamos el nivel de arriba");
                --levelsOutScreen;
                //previousLevels[levelIndex - 5].RecyclePool();

                int auxRandom = lastRandom;
                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
                GameObject auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, -levelHeight / 100, 0));
                previousLevels.Add(auxGameObject);
                auxGameObject.transform.SetParent(rootLevel);
                auxGameObject.SetActive(true);
                lastRandom = auxRandom;
                //Invoke("AdvanceLevel", 4.0f);
            }*/
        }

        public void CharacterCollideWithEnemy(Enemies.Enemy.ENEMY_TYPES enemyType, int levelsToMove)
        {
            if (enemyType == Enemies.Enemy.ENEMY_TYPES.Ally)
            {
                currentLevelTransitionDuration = fastLevelTransitionDuration;
                while (levelsToMove > 0)
                {
                    --levelsToMove;
                    AdvanceLevel();
                }
                currentLevelTransitionDuration = normalLevelTransitionDuration;
            }
            else
            {
                currentLevelTransitionDuration = fastLevelTransitionDuration;
                while (levelsToMove > 0)
                {
                    --levelsToMove;
                    BackLevel();
                }
                Debug.Log(levelIndex);
                character.transform.DOMove(previousLevels[levelIndex - 3].GetComponent<Level>().GetRandomTriggerLevelPosition()+ new Vector3(0,-2,0), currentLevelTransitionDuration);
                currentLevelTransitionDuration = normalLevelTransitionDuration;
            }
        }
	}
}