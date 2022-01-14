using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Paddle paddle;
    public int score;
    public Text scoresText;
    public Text livesText;
    public int lives;
    Scene scene;
    public bool over;
    private int winningScore;


    private void resetGame()
    {
        lives = 5;
        scoresText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        scene = SceneManager.GetActiveScene();
        over = false;
    }

    private void Start()
    {
        resetGame();

        // set up winning score for different modes
        if (scene.name == "Main")
            winningScore = 55;
        else
            winningScore = 43;
    }

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // make sure there's only one instance of the GameManager
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementPoints(int changeInScore)
    {
        score += changeInScore;
        scoresText.text = "Score: " + score.ToString();

        // check if player has won the game
        if (score == winningScore)
            PlayerWin();
    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();

        // check if player has lost all their lives
        if (lives < 1)
            PlayerDeath();
    }

    public void destroyPaddle()
	{
        over = true;
        GameObject paddleObject = GameObject.Find("Paddle");
        Destroy(paddleObject);
    }

    public void PlayerWin()
    {
        destroyPaddle();
        Pause.instance.ShowWinnerPopUp();
    }

    public void PlayerDeath()
    {
        destroyPaddle();
        Pause.instance.ShowEndPopUp();
    }

    public void DestroyCurrentGame()
    {
        Destroy(gameObject);
    }

}
