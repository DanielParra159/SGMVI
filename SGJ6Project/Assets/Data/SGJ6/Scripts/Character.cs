using UnityEngine;
using System.Collections;
using SGJVI.Level;

namespace SGJVI.Characters
{
    public class Character : MonoBehaviour
    {




		void Start()
        {
            LevelManager.Instance.MainCharacter = this;
        }
			
        void Update()
        {

        }

        public void ChangeDirection(InputCharacter.SwipeDirection dir)
        {
            Debug.Log("ChangeDirection " + dir);
        }

        public void Jump()
        {
            Debug.Log("Jump");
        }
    }
}