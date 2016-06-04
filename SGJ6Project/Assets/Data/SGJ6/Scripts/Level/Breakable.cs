using UnityEngine;

namespace SGJVI.Level {

	public class Breakable : MonoBehaviour {

        [SerializeField]
        private int maxLife = 1;
        public int CurrentLife
        {
            get { return maxLife; }
        }

        public bool Hit()
        {
            --maxLife;
            if (maxLife == 0)
            {
                //Break
                gameObject.SetActive(false);
                //TODO:
            }
            return (maxLife == 0);
        }

	}
}