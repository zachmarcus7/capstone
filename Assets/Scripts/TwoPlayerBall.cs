namespace MLBreakout
{
    using UnityEngine;
    using System.Linq;
    using System;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// This is used for the player's ball in two-player mode.
    /// It manages the ball's speed and what happens
    /// when collisions occur.
    /// </summary>
    public class TwoPlayerBall : MonoBehaviour
    {
        private bool _inPlay;
        private bool _firstServeCompleted;
        private float _randomXCoord;
        private float _yValue;
        private int _yStart;
        private int _xStart;
        private Brick _brickReference;
        private Vector3 _previousVelocity;
        private Renderer _visual;
        private Rigidbody2D _rigidBody;
        private Scene _scene;
        public float RandomYCoord;
        public float RandXStart;
        public float RandXEnd;
        public GameManager Manager;
        public TwoPlayerCountdown Countdown;

        private void Start()
        {
            _firstServeCompleted = false;
            GetComponents();
            SetBallDirection();
            Countdown.ActivateCountdown();

            // let the game know the first serve has been dealt
            _firstServeCompleted = true;
        }

        // this gets all the required components to launch the ball
        private void GetComponents()
        {
            _scene = SceneManager.GetActiveScene();
            _rigidBody = GetComponent<Rigidbody2D>();
            _brickReference = new Brick();
            transform.position = GenerateBallPosition();

            // make the ball disappear until the countdown ends
            _visual = GetComponent<Renderer>();
            _visual.enabled = !_visual.enabled;
        }

        // this sets the y axis startDirection depending on the scene
        private void SetBallDirection()
        {
            if (_scene.name == "Main")
            {
                _yStart = 260;
                _xStart = 260;
            }
            else if (_scene.name == "TwoPlayerHard")
            {
                _yStart = 260;
                _xStart = 260;
            }
            else if (_scene.name == "TwoPlayerMedium")
            {
                _yStart = 170;
                _xStart = 240;
            }
            else
            {
                _yStart = 120;
                _xStart = 200;
            }
        }

        private void Update()
        {
            if (GameManager.Instance.Over)
            {
                return;
            }

            // this is here to make sure the countdown doesn't get reactivated on the first serve
            if (!_firstServeCompleted)
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

        public int RandomDirection()
        {
            // randomly choose a number between -1 or 1
            // -1 means the ball will go left, 1 means it'll go right
            float randomChoice = UnityEngine.Random.Range(0f, 2f);
            if (randomChoice < 1f)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public void AutomaticLaunch()
        {
            // see if ball goes right or left
            int horizontal = RandomDirection();
            _xStart *= horizontal;

            // apply force to ball
            Vector2 direction = new Vector2(_xStart, _yStart);
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
}