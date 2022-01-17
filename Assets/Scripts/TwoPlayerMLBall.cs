using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class TwoPlayerMLBall : MonoBehaviour
{
    public float speed = 1f;
    public Brick brickReference;
    public float randomXCoord;
    public float randomYCoord;
    public MLGameManager gm;
    public TwoPlayerMLCountdown countdown;
    private Rigidbody2D rigidBody;
    private Vector3 previousVelocity;
    private bool inPlay;
    private int startDirection;
    private Renderer visual;
    private Scene scene;


    private void getComponents()
    {
        // get necessary components
        scene = SceneManager.GetActiveScene();
        visual = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        brickReference = new Brick();
    }

    private void initializeCountdown()
    {
        // start countdown to ball launch
        visual.enabled = !visual.enabled;
        transform.position = generateBallPosition();
        //countdown.activateCountdown();                                   // CHANGED FOR TRAINING
        AutomaticLaunch();
    }

    void Start()
    {
        getComponents();

        // set startDirection depending on scene
        if (scene.name == "MLAgentScreen")
            startDirection = 200;
        else
            startDirection = -200;    // CHANGED FOR TRAINING

        initializeCountdown();
    }

    void Update()
    {
        if (MLGameManager.instance.over)
        {
            return;
        }
        else
        {
            if (!inPlay)
            {
                transform.position = generateBallPosition();
                //countdown.activateCountdown();
                AutomaticLaunch();                                       // CHANGED FOR TRAINING
            }
            else
            {
                float yValue;
                if (rigidBody.velocity.y > -1 && rigidBody.velocity.y < 1)
                {
                    // check if the ball is moving up or down
                    if (rigidBody.velocity.y <= 0)
                        yValue = -1f;
                    else
                        yValue = 1f;

                    // make the ball go the minimum speed
                    Vector2 minimumVelocity = new Vector2(0, yValue);
                    rigidBody.AddForce(minimumVelocity);
                }
            }
            previousVelocity = rigidBody.velocity;
        }

    }

    public void AutomaticLaunch()
    {
        Renderer visual = GetComponent<Renderer>();
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-200, 200), startDirection);
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }

    private Vector3 generateBallPosition()
    {
        if (scene.name == "MLAgentScreen")
		{
            randomXCoord = UnityEngine.Random.Range(2f, 9f);
            randomYCoord = 2f;
            return new Vector3(randomXCoord, randomYCoord, 0);
        }
		else
		{
            randomXCoord = UnityEngine.Random.Range(2f, 9f);
            randomYCoord = 6.5f;
            return new Vector3(randomXCoord, randomYCoord, 0);
        }
    }

    void decrementLives()
    {
        Renderer visual = GetComponent<Renderer>();
        gm.DecrementLives();
        rigidBody.velocity = Vector2.zero;
        inPlay = false;
        visual.enabled = !visual.enabled;

        // reset brick layers for speed
        brickReference.resetLayers();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // check if ml bottom was hit
        if (col.collider.tag == "MLBottom")
		{
            decrementLives();
		}
        else if (col.collider.tag == "PlayerBottom")
		{
            // the ball has reached the other side, so the ml agent wins
            gm.PlayerWin();
		}
        // check if a brick was hit
        else if (brickReference.colors.Contains(col.collider.tag))
        {
            int index = Array.IndexOf(brickReference.colors, col.collider.tag);
            Bounce(col.contacts[0].normal, index);
        }
        else
        {
            Bounce(col.contacts[0].normal, -1);
        }
    }

    private void Bounce(Vector3 collisionNormal, int colorIndex)
    {
        var speed = previousVelocity.magnitude;
        var direction = Vector3.Reflect(previousVelocity.normalized, collisionNormal);

        // check if not bouncing off of a brick, or if the ball has already reached the layer
        if ((colorIndex == -1) || (brickReference.layerReached[colorIndex] == true))
        {
            rigidBody.velocity = direction * speed;
        }
        else
        {
            // increase the ball's speed by the layer's index multipled by 0.2
            rigidBody.velocity = direction * (speed + (colorIndex * 0.2f));
            brickReference.layerReached[colorIndex] = true;
        }
    }
}

