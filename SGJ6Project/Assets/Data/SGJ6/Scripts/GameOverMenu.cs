using UnityEngine;

namespace SGJVI {

	public class GameOverMenu : MonoBehaviour {

        public void OnReset()
        {
            GameLogic.GameLogic.Instance.ResetGame();
        }
		
	}
}