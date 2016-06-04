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
		private float direction, lastDirection, breakBlockTime;
		[SerializeField]
		[Range (0.01f, 0.5f)]
		private float timeBetweemTouch = 0;
		private bool breakBlock;


		private void Awake()
		{
			
			m_Character = GetComponent<PlatformerCharacter2D>();
			direction = -1.0f;
			lastDirection = direction;

		}

		void Start()
        {
			
            LevelManager.Instance.MainCharacter = this;

        }
			

		private void FixedUpdate()
		{
			
			m_Character.Move(direction, breakBlock, m_Jump);
			m_Jump = false;

			if (breakBlock == true && (breakBlockTime + timeBetweemTouch) < Time.time) {

				breakBlock = false;
				direction = lastDirection;

			}

            Vector2 beginRay = transform.position + new Vector3(0.0f,0.5f,0.0f);
            Vector2 endRay = new Vector2(direction, 0);

            RaycastHit2D impact = Physics2D.Raycast(beginRay, endRay, 0.5f, SGJVI.Core.GameLayers.WallMask);

            if (impact.collider != null)
            {
                direction *= -1;
            }

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


			Vector2 beginRay = transform.position;
			Vector2 endRay = new Vector2 (0, -1);

			RaycastHit2D impact = Physics2D.Raycast (beginRay, endRay, 1.0f, SGJVI.Core.GameLayers.BreakableMask);

			if (impact.collider != null) {

				Debug.Log ("Impact");
				breakBlock = true;
				lastDirection = direction;
				direction = 0;
				breakBlockTime = Time.time;

				Breakable breakable = impact.collider.GetComponent<Breakable> ();

				breakable.Hit ();

			}




        }
    }
}