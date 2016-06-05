using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace SGJVI.Characters {

    [RequireComponent(typeof(Slider))]
	public class CharacterHUD : MonoBehaviour {

        #region Singelton
        private static CharacterHUD instance = null;
        public static CharacterHUD Instance
        {
            get { return instance; }
        }
        #endregion

        private Slider slider;
        [SerializeField]
        private float energy = 10;
        private float currentEnergy = 10;
		// Use this for initialization
		private void Awake () {
            if (instance == null)
            {
                instance = this;
                slider = GetComponent<Slider>();
                currentEnergy = energy;
            }
            else
            {
                Debug.LogWarning("Se está creando una segunda instancia de GameLogic");
                Destroy(gameObject);
            }
            
		}

        private void OnDestroy()
        {
            instance = null;
        }
		
		// Update is called once per frame
		private void Update () {
            slider.value = currentEnergy / energy;

            currentEnergy -= Time.deltaTime;
            if (currentEnergy < 0.0f)
            {
                currentEnergy = 0.0f;
                //Perdemos
                GameLogic.GameLogic.Instance.EndGame();
            }
		}

        public void addTime(float timeToAdd)
        {
            currentEnergy += timeToAdd;
            slider.DOValue(currentEnergy / energy, 0.5f);
        }

	}
}