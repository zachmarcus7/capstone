namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is the main game manager for the player in both
    /// one- and two-player modes. It manages the player's score and lives,
    /// and also stops the game when the player wins or loses.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private int _winningScore;
        private Scene _scene;
        public static GameManager Instance;
        public int Score;
        public int Lives;
        public bool Over;
        public Text ScoresText;
        public Text LivesText;
        public Paddle SelectedPaddle;

        private void Start()
        {
            ResetGame();

            // set up winning score for different modes
            if (_scene.name == "Main")
            {
                _winningScore = 55;
            }
            else
            {
                _winningScore = 43;
            }
        }

        private void ResetGame()
        {
            Lives = 100000;                                                // CHANGED FOR TRAINING
            Over = false;
            ScoresText.text = "Score: " + Score.ToString();
            LivesText.text = "Lives: " + Lives.ToString();
            _scene = SceneManager.GetActiveScene();
        }

        private void Awake()
        {
            if (GameManager.Instance != null)
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

            // check if player has won the game
            if (Score == _winningScore)
                PlayerWin();
        }

        public void DecrementLives()
        {
            Lives -= 1;
            LivesText.text = "Lives: " + Lives.ToString();

            // check if player has lost all their lives
            if (Lives < 1)
                PlayerDeath();
        }

        public void DestroyPaddle()
        {
            Over = true;
            GameObject paddleObject = GameObject.Find("Paddle");
            Destroy(paddleObject);
        }

        public void PlayerWin()
        {
            DestroyPaddle();
            Pause.Instance.ShowWinnerPopUp();
        }

        public void PlayerDeath()
        {
            DestroyPaddle();
            Pause.Instance.ShowEndPopUp();
        }

        public void DestroyCurrentGame()
        {
            Destroy(gameObject);
        }
    }
}