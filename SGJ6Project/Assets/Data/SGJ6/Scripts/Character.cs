using UnityEngine;
using System.Collections;
using SGJVI.Level;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

namespace SGJVI.Characters

{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class Character : MonoBehaviour
    {

		private PlatformerCharacter2D m_Character;
		private bool m_Jump;
		private float direction, lastDirection;
		private bool crouch;


		private void Awake()
		{
			m_Character = GetComponent<PlatformerCharacter2D>();
			direction = -1.0f;
		}

		void Start()
        {
            LevelManager.Instance.MainCharacter = this;
        }
			

		private void FixedUpdate()
		{
			// Read the inputs.


			// Pass all parameters to the character control script.
			m_Character.Move(direction, crouch, m_Jump);
			m_Jump = false;
			crouch = false;
		}

        public void ChangeDirection(InputCharacter.SwipeDirection dir)
        {
            Debug.Log("ChangeDirection " + dir);

			if (dir == InputCharacter.SwipeDirection.Left) {

				direction = -1.0f;

			} else {

				direction = 1.0f;

			}


        }

        public void Jump()
        {
            Debug.Log("Jump");
			m_Jump = true;

        }

        public void BreakBlock()
        {
            Debug.Log("BreakBlock");
			crouch = true;
			lastDirection = direction;
			direction = 0;
        }
    }
}