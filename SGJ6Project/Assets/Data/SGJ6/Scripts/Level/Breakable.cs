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

        [SerializeField]
        private GameObject breakObject;

        public bool Hit()
        {
            --maxLife;
            if (maxLife == 0)
            {
                //Break
                gameObject.SetActive(false);
                CharacterHUD.Instance.addTime(energyToAdd);
                if (breakObject != null)
                {
                    breakObject = (GameObject)Instantiate(breakObject, transform.position, Quaternion.identity);
                    Destroy(breakObject, 0.5f);
                }
                //TODO:
            }
            return (maxLife == 0);
        }

	}
}