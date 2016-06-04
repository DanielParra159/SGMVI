using UnityEngine;
using System.Collections;
using SGJVI.Level;

namespace SGJVI.Characters
{
    public class Character : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            LevelManager.Instance.MainCharacter = this;
        }

        // Update is called once per frame
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