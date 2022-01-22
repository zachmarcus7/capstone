namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is the main game manager for the ml agent in both
    /// one- and two-player modes. It manages the ai's score and lives,
    /// and also stops the game when the ai wins or loses.
    /// </summary>
    public class MLGameManager : MonoBehaviour
    {
        private Scene _scene;
        private int _winningScore;
        public static MLGameManager Instance;
        public int BricksBroken;
        public int Score;
        public int Lives;
        public bool Over;
        public Text ScoresText;
        public Text LivesText;

        private void Start()
        {
            ResetGame();

            // set up winning score for different modes                   
            if (_scene.name == "MLAgentScreen")
            {
                _winningScore = 10000000;                                              // CHANGED FOR TRAINING
            }
            else
            {
                _winningScore = 43;
            }
        }

        private void ResetGame()
        {
            Lives = 1000000;                                                      // CHANGED FOR TRAINING
            BricksBroken = 0;
            ScoresText.text = "Score: " + Score.ToString();
            LivesText.text = "Lives: " + Lives.ToString();
            _scene = SceneManager.GetActiveScene();
            Over = false;
        }

        private void Awake()
        {
            if (MLGameManager.Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            // make sure there's only one instance of the GameManager
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void IncrementPoints(int changeInScore)
        {
            Score += changeInScore;
            ScoresText.text = "Score: " + Score.ToString();
            BricksBroken++;

            // check if agent has won the game
            if (Score == _winningScore)
            {
                PlayerWin();
            }
        }

        public void DecrementLives()
        {
            Lives -= 1;
            LivesText.text = "Lives: " + Lives.ToString();

            // check if agent has lost all their lives
            if (Lives < 1)
            {
                PlayerDeath();
            }
        }

        public void PlayerWin()
        {
            // check if we're in 1 player or 2 player mode
            if (_scene.name == "MLAgentScreen")
            {
                Over = true;
                DestroyBallAndPaddle();
                Pause.Instance.ShowWinnerPopUp();
            }
            else
            {
                // activate the game over screen, indicating ml agent won                     
                Over = true;
                DestroyBallAndPaddle();
                Pause.Instance.ShowEndPopUp();
            }
        }

        public void PlayerDeath()
        {
            // check if we're in 1 player or 2 player mode
            if (_scene.name == "MLAgentScreen")
            {
                Over = true;
                DestroyBallAndPaddle();
                Pause.Instance.ShowEndPopUp();
            }
            else
            {
                Over = true;
                DestroyBallAndPaddle();
                Pause.Instance.ShowWinnerPopUp();
            }
        }

        public void DestroyBallAndPaddle()
        {
            GameObject paddleObject = GameObject.Find("Agent (Paddle)");
            GameObject ballObject = GameObject.Find("MLBall");
            Destroy(paddleObject);
            Destroy(ballObject);
        }

        public void DestroyCurrentGame()
        {
            Destroy(gameObject);
        }
    }
}