using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class TwoPlayerBall : MonoBehaviour
{
    private bool _inPlay;
    private float _randomXCoord;
    private float _yValue;
    private Brick _brickReference;
    private Scene _scene;
    private Vector3 _previousVelocity;
    private Renderer _visual;
    private Rigidbody2D _rigidBody;
    public float RandomYCoord;
    public float RandXStart;
    public float RandXEnd;
    public GameManager Manager;
    public TwoPlayerCountdown Countdown;

    private void Start()
    {
        GetComponents();
        InitializeCountdown();
    }

    private void GetComponents()
    {
        _scene = SceneManager.GetActiveScene();
        _visual = GetComponent<Renderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _brickReference = new Brick();
    }

    private void InitializeCountdown()
    {
        _visual.enabled = !_visual.enabled;
        transform.position = GenerateBallPosition();
        Countdown.ActivateCountdown();
    }

    private void Update()
    {
        if (GameManager.Instance.Over)
        {
            return;
        }

        if (!_inPlay)
        {
            // reset ball and start countdown
            transform.position = GenerateBallPosition();
            Countdown.ActivateCountdown();
        }
        else
        {
            // if ball is going too slow, make sure it goes back to min speed
            if (_rigidBody.velocity.y > -1 && _rigidBody.velocity.y < 1)
            {
                ApplyMinSpeed();
            }
        }
        _previousVelocity = _rigidBody.velocity;
    }

    private float UpOrDown()
    {
        // check if the ball is moving up or down
        if (_rigidBody.velocity.y <= 0)
        {
            return -1f;
        }
        else
        {
            return 1f;
        }
    }

    private void ApplyMinSpeed()
    {
        // make the ball go minimum speed
        _yValue = UpOrDown();
        Vector2 minimumVelocity = new Vector2(0, _yValue);
        _rigidBody.AddForce(minimumVelocity);
    }

    public void AutomaticLaunch()
    {
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-260, 260), 170);
        _rigidBody.AddForce(direction);
        _inPlay = true;
        _visual.enabled = true;
    }

    private Vector3 GenerateBallPosition()
    {
        _randomXCoord = UnityEngine.Random.Range(RandXStart, RandXEnd);
        return new Vector3(_randomXCoord, RandomYCoord, 0);
    }

    void DecrementLives()
    {
        Manager.DecrementLives();
        _rigidBody.velocity = Vector2.zero;
        _inPlay = false;
        _visual.enabled = !_visual.enabled;

        // reset brick layers for speed
        _brickReference.ResetLayers();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // check if ml bottom was hit
        if (col.collider.tag == "PlayerBottom")
        {
            DecrementLives();
        }
        else if (col.collider.tag == "MLBottom")
        {
            // the ball has reached the other side, so the player wins
            Manager.PlayerWin();
        }
        // check if a brick was hit
        else if (_brickReference.Colors.Contains(col.collider.tag))
        {
            int index = Array.IndexOf(_brickReference.Colors, col.collider.tag);
            Bounce(col.contacts[0].normal, index);
        }
        else
        {
            Bounce(col.contacts[0].normal, -1);
        }
    }

    private void Bounce(Vector3 collisionNormal, int colorIndex)
    {
        var speed = _previousVelocity.magnitude;
        var direction = Vector3.Reflect(_previousVelocity.normalized, collisionNormal);

        // check if not bouncing off of a brick, or if the ball has already reached the layer
        if ((colorIndex == -1) || (_brickReference.LayerReached[colorIndex] == true))
        {
            _rigidBody.velocity = direction * speed;
        }
        else
        {
            // increase the ball's speed by the layer's index multipled by 0.2
            _rigidBody.velocity = direction * (speed + (colorIndex * 0.2f));
            _brickReference.LayerReached[colorIndex] = true;
        }
    }
}
