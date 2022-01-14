using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MLGameManager : MonoBehaviour
{
    public static MLGameManager instance;
    public Paddle paddle;
    public int score;
    public Text scoresText;
    public Text livesText;
    public int lives;
    public int bricksBroken;
    Scene scene;
    public bool over;
    private int winningScore;


    private void resetGame()
	{
        lives = 5;
        bricksBroken = 0;
        scoresText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        scene = SceneManager.GetActiveScene();
        over = false;
    }

    private void Start()
    {
        resetGame();

        // set up winning score for different modes
        if (scene.name == "MLAgentScreen")
            winningScore = 55;
        else
            winningScore = 43;
    }

    private void Awake()
    {
        if (MLGameManager.instance != null)
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
        bricksBroken++;

        // check if agent has won the game
        if (score == winningScore)
            PlayerWin();

    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();

        // check if agent has lost all their lives
        if (lives < 1)
            PlayerDeath();
    }

    public void PlayerWin()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "MLAgentScreen")
        {
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowWinnerPopUp();
        }
        else
        {
            // activate the game over screen, indicating ml agent won                     
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowEndPopUp();
        }
    }

    public void PlayerDeath()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "MLAgentScreen")
        {
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowEndPopUp();
        }
        else
        {
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowWinnerPopUp();
        }

    }

    public void destroyBallAndPaddle()
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