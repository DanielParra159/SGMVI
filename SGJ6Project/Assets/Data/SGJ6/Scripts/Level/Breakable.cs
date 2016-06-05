using SGJVI.Characters;
using UnityEngine;

namespace SGJVI.Level {

	public class Breakable : MonoBehaviour {

        [SerializeField]
        private int maxLife = 1;
        public int CurrentLife
        {
            get { return maxLife; }
        }

        [SerializeField]
        private float energyToAdd;

        public bool Hit()
        {
            --maxLife;
            if (maxLife == 0)
            {
                //Break
                gameObject.SetActive(false);
                CharacterHUD.Instance.addTime(energyToAdd);
                //TODO:
            }
            return (maxLife == 0);
        }

	}
}