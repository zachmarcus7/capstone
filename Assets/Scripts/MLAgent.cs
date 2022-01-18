namespace MLBreakout
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Unity.MLAgents;
    using Unity.MLAgents.Actuators;
    using Unity.MLAgents.Sensors;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// This is the script for the one-player paddle ai.
    /// It uses reinforcement learning to train the agent.
    /// </summary>
    public class MLAgent : Agent
    {
        private Vector3 _previousBallLocation;
        private Vector3 _changeInBallLocation;
        private Scene _scene;
        public static MLAgent Instance;
        public GameObject Target;
        public float MoveSpeed;
        public float RightScreenEdge;
        public float LeftScreenEdge;
        public float PreviousPaddlePosition;
        public float Input;
        public int PreviousBricksBroken;
        public int PreviousLives;

        private void Awake()
        {
            PreviousBricksBroken = 0;
            PreviousLives = 5;
            MoveSpeed = 5f;
            Instance = this;
            _scene = SceneManager.GetActiveScene();
        }

        private void Update()
        {
            if (transform.localPosition.x < LeftScreenEdge)
            {
                transform.localPosition = new Vector3(LeftScreenEdge, transform.localPosition.y, 0);
            }
            if (transform.localPosition.x > RightScreenEdge)
            {
                transform.localPosition = new Vector3(RightScreenEdge, transform.localPosition.y, 0);
            }

            // check if any bricks have been broken
            if (MLGameManager.Instance.BricksBroken != PreviousBricksBroken)
            {
                //AddReward(+1f);
                PreviousBricksBroken = MLGameManager.Instance.BricksBroken;
            }

            // check if any lives have been lost
            if (MLGameManager.Instance.Lives != PreviousLives)
            {
                //AddReward(-0.5f);
                PreviousLives = MLGameManager.Instance.Lives;
                EndEpisode();
            }
            PreviousPaddlePosition = transform.position.x;
        }

        public override void OnEpisodeBegin()
        {
            transform.localPosition = new Vector3(4f, 0.6854997f, 0f);
            Target.transform.localPosition = new Vector3(Random.Range(1f, 9f), Random.Range(3.25f, 3.75f), 0);
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            _changeInBallLocation = Target.transform.localPosition - _previousBallLocation;
            _previousBallLocation = Target.transform.localPosition;

            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(Target.transform.localPosition);

            // add observation for distance
            sensor.AddObservation(Vector3.Distance(this.transform.localPosition, Target.transform.localPosition));

            // add observation for ball's direction
            sensor.AddObservation(_changeInBallLocation);

            // add observation for paddle distance to screen edges
            sensor.AddObservation(transform.localPosition.x - LeftScreenEdge);
            sensor.AddObservation(RightScreenEdge - transform.localPosition.x);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            // here we're getting which one of the 3 actions was sent to the action buffer
            // then changing the x position depending on what the action was
            int moveX = 0;
            int action = actions.DiscreteActions[0];

            switch (action)
            {
                case 0:
                    moveX = -1;
                    break;
                case 1:
                    moveX = 0;
                    break;
                case 2:
                    moveX = 1;
                    break;
            }

            transform.localPosition += new Vector3(moveX * 3f, 0, 0) * Time.deltaTime * MoveSpeed;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "MLBall")
            {
                AddReward(+1.5f);
                Debug.Log("+1.5 for hitting ball");
            }
        }
    }
}