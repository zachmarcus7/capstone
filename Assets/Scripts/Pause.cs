namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is used with the pause menu for
    /// stopping and resuming the game.
    /// </summary>
    public class Pause : MonoBehaviour
    {
        private Scene _scene;
        private GameObject _gameOverHeader;
        private GameObject _winnerHeader;
        public static Pause Instance;
        public static bool Active;
        public GameObject Menu;

        private void Start()
        {
            Active = false;
            Instance = this;
            _scene = SceneManager.GetActiveScene();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Active)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            Menu.SetActive(false);
            Time.timeScale = 1f;
            Active = false;
        }

        public void PauseGame()
        {
            Menu.SetActive(true);

            // make it so neither game over header appears during a normal pause
            _gameOverHeader = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;
            _gameOverHeader.SetActive(false);
            _winnerHeader = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
            _winnerHeader.SetActive(false);

            // make it so nothing can move
            Time.timeScale = 0f;
            Active = true;
        }

        public void ShowEndPopUp()
        {
            Menu.SetActive(true);

            // make it so only red game over header shows
            _winnerHeader = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
            _winnerHeader.SetActive(false);

            // make it so nothing can move
            Time.timeScale = 0f;
            Active = true;
        }

        public void ShowWinnerPopUp()
        {
            Menu.SetActive(true);

            // make it so only green game over header shows
            _gameOverHeader = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;
            _gameOverHeader.SetActive(false);

            // make it so nothing can move
            Time.timeScale = 0f;
            Active = true;
        }

        public void LoadMain()
        {
            if (_scene.name == "Main")
            {
                ResumeGame();
                GameManager.Instance.DestroyCurrentGame();
                SceneManager.LoadScene("OpeningMenu");
            }
            else if (_scene.name == "MLAgentScreen")
            {
                ResumeGame();
                MLGameManager.Instance.DestroyCurrentGame();
                SceneManager.LoadScene("OpeningMenu");
            }
            else
            {
                ResumeGame();
                GameManager.Instance.DestroyCurrentGame();
                MLGameManager.Instance.DestroyCurrentGame();
                SceneManager.LoadScene("OpeningMenu");
            } 
        }

	    public void ExitGame()
        {
            Debug.Log("Exit Game pressed");
        }
    }
}