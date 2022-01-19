namespace MLBreakout
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// This is used for the ball in both the one-
    /// player and two-player mode for the ml agent.
    /// </summary>
    public class TwoPlayerMLBall : MonoBehaviour
    {
        private Rigidbody2D _rigidBody;
        private Vector3 _previousVelocity;
        private Brick _brickReference;
        private bool _inPlay;
        private int _startDirection;
        private Renderer _visual;
        private Scene _scene;
        public MLGameManager Manager;
        public TwoPlayerMLCountdown Countdown;
        public float RandomXCoord;
        public float RandomYCoord;

        private void Start()
        {
            GetComponents();
            SetBallDirection();
            InitializeCountdown();
        }

        // this gets all the required components to launch the ball
        private void GetComponents()
        {
            _scene = SceneManager.GetActiveScene();
            _visual = GetComponent<Renderer>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _brickReference = new Brick();
        }

        // this sets the y axis startDirection depending on the scene
        private void SetBallDirection()
        {
            if (_scene.name == "MLAgentScreen")
            {
                _startDirection = 280;  // 260
            }
            else if (_scene.name == "TwoPlayerHard")
            {
                _startDirection = 330;  // 300
            }
            else if (_scene.name == "TwoPlayerMedium")
            {
                _startDirection = 200;
            }
            else
            {
                _startDirection = 170;
            }
        }

        private void InitializeCountdown()
        {
            // start countdown to ball launch
            _visual.enabled = !_visual.enabled;
            transform.position = GenerateBallPosition();
            //Countdown.activateCountdown();                                   // CHANGED FOR TRAINING
            AutomaticLaunch();
        }

        private void Update()
        {
            if (MLGameManager.Instance.Over)
            {
                return;
            }
            else
            {
                if (!_inPlay)
                {
                    transform.position = GenerateBallPosition();
                    //countdown.activateCountdown();
                    AutomaticLaunch();                                       // CHANGED FOR TRAINING
                }
                else
                {
                    float yValue;
                    if (_rigidBody.velocity.y > -1 && _rigidBody.velocity.y < 1)
                    {
                        // check if the ball is moving up or down
                        if (_rigidBody.velocity.y <= 0)
                            yValue = -1f;
                        else
                            yValue = 1f;

                        // make the ball go the minimum speed
                        Vector2 minimumVelocity = new Vector2(0, yValue);
                        _rigidBody.AddForce(minimumVelocity);
                    }
                }
                _previousVelocity = _rigidBody.velocity;
            }

        }

        public void AutomaticLaunch()
        {
            Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-260, 260), _startDirection);
            _rigidBody.AddForce(direction);
            _inPlay = true;
            _visual.enabled = true;
        }

        private Vector3 GenerateBallPosition()
        {
            if (_scene.name == "MLAgentScreen")
            {
                RandomXCoord = UnityEngine.Random.Range(2f, 9f);
                RandomYCoord = 2f;
                return new Vector3(RandomXCoord, RandomYCoord, 0);
            }
            else
            {
                RandomXCoord = UnityEngine.Random.Range(2f, 9f);
                RandomYCoord = 6.5f;
                return new Vector3(RandomXCoord, RandomYCoord, 0);
            }
        }

        private void DecrementLives()
        {
            Manager.DecrementLives();
            _rigidBody.velocity = Vector2.zero;
            _inPlay = false;
            _visual.enabled = !_visual.enabled;

            // reset brick layers for speed
            _brickReference.ResetLayers();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            // check if ml bottom was hit
            if (col.collider.tag == "MLBottom")
            {
                DecrementLives();
            }
            else if (col.collider.tag == "PlayerBottom")
            {
                // the ball has reached the other side, so the ml agent wins
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
}