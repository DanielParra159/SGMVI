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
        private GameObject [] levels;
        private List<GameObject> currentLevels = new List<GameObject>(3);
        private GameObject rootLevel;

        [SerializeField]
        private int levelHeight = 900;

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
                rootLevel = new GameObject("rootLevel");
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
                int auxRandom = StaticRandom.RandomRange(0, levels.Length);
                int lastRandom = auxRandom;
                GameObject auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, 0, 0));
                currentLevels.Add(auxGameObject);
                auxGameObject.transform.SetParent(rootLevel.transform);
                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
                auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, -levelHeight / 100, 0));
                currentLevels.Add(auxGameObject);
                auxGameObject.transform.SetParent(rootLevel.transform);
                lastRandom = auxRandom;

                while (lastRandom == auxRandom)
                {
                    auxRandom = StaticRandom.RandomRange(0, levels.Length);
                }
                auxGameObject = levels[auxRandom].SpawnPool(new Vector3(0, (-2 * levelHeight) / 100, 0));
                currentLevels.Add(auxGameObject);
                auxGameObject.transform.SetParent(rootLevel.transform);
            }
		}
		
		// Update is called once per frame
		private void Update () {
		
		}

        public void AdvanceLevel()
        {
            for (int i = 0; i < currentLevels.Count; ++i)
            {
                currentLevels[i].GetComponent<Level>().DisableLevel();
                currentLevels[i].transform.DOMoveY(currentLevels[i].transform.position.y + 9, levelTransitionDuration).OnComplete(NotyfyAllLevelsTransitionComplete);
            }
        }

        private void NotyfyAllLevelsTransitionComplete()
        {

        }
	}
}