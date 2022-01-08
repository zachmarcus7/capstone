using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class Ball : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rigidBody;
    public Brick brickReference;
    public Vector3 previousVelocity;
    public bool inPlay;
    public float randomXCoord;
    public float randomYCoord;
    public float randXStart;
    public float randXEnd;
    public float randYStart;
    public float randYEnd;
    public GameManager gm;
    private Scene scene;
    public Countdown countdown;


    void Start()
    {
        // get necessary components
        scene = SceneManager.GetActiveScene();
        Renderer visual = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        // set up bricks for ball speed
        brickReference = new Brick();

        // start countdown to ball launch
        visual.enabled = !visual.enabled;
        Tuple<float, float> ballposition = generateBallPosition();
        transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
        countdown.activateCountdown();
    }

    void Update()
    {
        if (GameManager.instance.over)
		{
            return;
		}
        else
		{
            if (!inPlay)
            {
                Tuple<float, float> ballposition = generateBallPosition();
                transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
                countdown.activateCountdown();
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
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-200, 200), 200);  
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }

    private Tuple<float, float> generateBallPosition()
    {
        if (scene.name == "TwoPlayerScreen")
		{
            randomXCoord = UnityEngine.Random.Range(randXStart, randXEnd);
            randomYCoord = -0.9f;
            return new Tuple<float, float>(randomXCoord, randomYCoord);
        }
        else
		{
            randomXCoord = UnityEngine.Random.Range(5f, 10f);
            randomYCoord = 1.5f;
            return new Tuple<float, float>(randomXCoord, randomYCoord);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if ball was lost
        if (other.CompareTag("Bottom"))
        {   // decrement lives
            Renderer visual = GetComponent<Renderer>();
            gm.DecrementLives();
            rigidBody.velocity = Vector2.zero;
            inPlay = false;
            visual.enabled = !visual.enabled;

            // reset brick layers for speed
            brickReference.resetLayers();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // check if a brick was hit
        if (brickReference.colors.Contains(col.collider.tag))
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
            // increase the ball's speed by the layer's index multipled by 0.5
            rigidBody.velocity = direction * (speed + (colorIndex * 0.2f));
            brickReference.layerReached[colorIndex] = true;
        }
    }

    void TwoPlayerUpdate()
    {
        if (!inPlay)
        {
            Tuple<float, float> ballposition = generateBallPosition();
            transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
            AutomaticLaunch();
        }
        else
        {
            float yValue;
            if (rigidBody.velocity.y > -1 && rigidBody.velocity.y < 1)
            {
                if (rigidBody.velocity.y <= 0)
                {
                    yValue = -1f;
                }
                else
                {
                    yValue = 1f;
                }
                Vector2 minimumVelocity = new Vector2(0, yValue);
                rigidBody.AddForce(minimumVelocity);
            }
        }
        previousVelocity = rigidBody.velocity;
    }

}
