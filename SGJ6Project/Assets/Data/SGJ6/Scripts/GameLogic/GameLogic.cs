using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGJVI.GameLogic {

	public class GameLogic : MonoBehaviour {

        #region Singelton
        private static GameLogic instance = null;
        public static GameLogic Instance
        {
            get { return instance; }
        }
        #endregion

        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject levelManager;
		[SerializeField]
		private GameObject audioManager;
		[SerializeField]
        private GameObject rootCanvas;
        private GameObject gameOverCanvas;

        [SerializeField]
        private GameObject touchTutorial;
        [SerializeField]
        private GameObject swipeTutorial;

        private bool touchState = false;
        private bool swipeState = false;

        //inside class
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
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

		// Use this for initialization
		private void Start () {
            if (instance == this)
            {
                Time.timeScale = 0;
                rootCanvas = Instantiate(rootCanvas);
                player = Instantiate(player);
                levelManager = Instantiate(levelManager);
				audioManager = Instantiate (audioManager);
                gameOverCanvas = rootCanvas.GetComponentInChildren<GameOverMenu>().gameObject;
                gameOverCanvas.SetActive(false);
            }
		}
		
		// Update is called once per frame
		private void Update () {
		    if (touchState)
            {
                if (Input.touches.Length > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Began)
                    {
                        Time.timeScale = 1;
                        touchState = false;
                        touchTutorial.SetActive(false);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Time.timeScale = 1;
                    touchState = false;
                    touchTutorial.SetActive(false);
                }
            } 
            else if (swipeState)
            {
                if (Input.touches.Length > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Began)
                    {
                        //save began touch 2d point
                        firstPressPos = new Vector2(t.position.x, t.position.y);
                    }
                    if (t.phase == TouchPhase.Ended)
                    {
                        //save ended touch 2d point
                        secondPressPos = new Vector2(t.position.x, t.position.y);

                        //create vector from the two points
                        currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                        //normalize the 2d vector
                        currentSwipe.Normalize();

                        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            Time.timeScale = 1;
                            swipeState = false;
                            swipeTutorial.SetActive(false);
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //save began touch 2d point
                        firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        //save ended touch 2d point
                        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                        //create vector from the two points
                        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                        //normalize the 2d vector
                        currentSwipe.Normalize();

                        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            Time.timeScale = 1;
                            swipeState = false;
                            swipeTutorial.SetActive(false);
                        }
                    }
                }
            }
		}

        public void PlayerOnTouchTrigger()
        {
            Time.timeScale = 0;
            touchState = true;
            touchTutorial = Instantiate(touchTutorial);
        }

        public void PlayerOnTouchSwipe()
        {
            Time.timeScale = 0;
            swipeState = true;
            swipeTutorial = Instantiate(swipeTutorial);
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }

        public void EndGame()
        {
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.UnloadScene(0);
        }
	}
}