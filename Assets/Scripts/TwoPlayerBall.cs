using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class TwoPlayerBall : MonoBehaviour
{
    public float randomYCoord;
    public float randXStart;
    public float randXEnd;
    public GameManager gm;
    public TwoPlayerCountdown countdown;
    protected float randomXCoord;
    protected Brick brickReference;
    protected Vector3 previousVelocity;
    protected Rigidbody2D rigidBody;
    protected bool inPlay;
    protected Renderer visual;
    protected Scene scene;
    protected float yValue;


    void Start()
    {
        getComponents();
        initializeCountdown();
    }

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
        countdown.activateCountdown();
    }

    void Update()
    {
        if (GameManager.instance.over)
            return;

        if (!inPlay)
        {
            // reset ball and start countdown
            transform.position = generateBallPosition();
            countdown.activateCountdown();
        }
        else
        {
            // if ball is going too slow, make sure it goes back to min speed
            if (rigidBody.velocity.y > -1 && rigidBody.velocity.y < 1)
                applyMinSpeed();
        }
        previousVelocity = rigidBody.velocity;
    }

    private float upOrDown()
    {
        // check if the ball is moving up or down
        if (rigidBody.velocity.y <= 0)
            return -1f;
        else
            return 1f;
    }

    private void applyMinSpeed()
    {
        // make the ball go minimum speed
        yValue = upOrDown();
        Vector2 minimumVelocity = new Vector2(0, yValue);
        rigidBody.AddForce(minimumVelocity);
    }

    public virtual void AutomaticLaunch()
    {
        Renderer visual = GetComponent<Renderer>();
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-260, 260), 170);
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }

    private Vector3 generateBallPosition()
    {
        randomXCoord = UnityEngine.Random.Range(randXStart, randXEnd);
        return new Vector3(randomXCoord, randomYCoord, 0);
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
        if (col.collider.tag == "PlayerBottom")
        {
            decrementLives();
        }
        else if (col.collider.tag == "MLBottom")
        {
            // the ball has reached the other side, so the player wins
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
