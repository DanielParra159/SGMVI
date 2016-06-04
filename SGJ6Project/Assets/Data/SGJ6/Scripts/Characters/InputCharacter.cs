using UnityEngine;

namespace SGJVI.Characters {

    [RequireComponent(typeof(Character))]
	public class InputCharacter : MonoBehaviour {

        private Character character;

		// Use this for initialization
		private void Awake () {
            character = gameObject.GetComponent<Character>();
		}

        public enum SwipeDirection
        {
            Up,
            Down,
            Right,
            Left
        }

        //inside class
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;

        void Update()
        {
#if UNITY_ANDROID
            SwipeTouch();
#else
            Swipe();
#endif
        }

        public void SwipeTouch()
        {
             if(Input.touches.Length > 0)
             {
                 Touch t = Input.GetTouch(0);
                 if(t.phase == TouchPhase.Began)
                 {
                      //save began touch 2d point
                     firstPressPos = new Vector2(t.position.x,t.position.y);
                 }
                 if(t.phase == TouchPhase.Ended)
                 {
                     //save ended touch 2d point
                     secondPressPos = new Vector2(t.position.x, t.position.y);

                     //create vector from the two points
                     currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                     //normalize the 2d vector
                     currentSwipe.Normalize();

                     CommonSwipe();
                 }
             }
        }

        public void Swipe()
        {
             if(Input.GetMouseButtonDown(0))
             {
                 //save began touch 2d point
                firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
             }
             if(Input.GetMouseButtonUp(0))
             {
                    //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
                    //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
           
                //normalize the 2d vector
                currentSwipe.Normalize();

                CommonSwipe();
            }
        }

        private void CommonSwipe()
        {

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                character.Jump();
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {

            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                character.ChangeDirection(SwipeDirection.Left);
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                character.ChangeDirection(SwipeDirection.Right);
            }
        }
	}
}