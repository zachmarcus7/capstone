using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;


public class TwoPlayerMLAgent : Agent
{
	private float _moveSpeed;
	private float _previousPaddlePosition;
	private Vector3 _previousBallLocation;
	private Vector3 _changeInBallLocation;
	public static TwoPlayerMLAgent Instance;
	public GameObject Target;
	public int PreviousBricksBroken;
	public int PreviousLives;
	public float RightScreenEdge;
	public float LeftScreenEdge;
	public float Input;

	private void Awake()
	{
		_moveSpeed = 5f;
		PreviousLives = 5;
		PreviousBricksBroken = 0;
		Instance = this;
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
		_previousPaddlePosition = transform.position.x;
	}

	public override void OnEpisodeBegin()
	{
		transform.localPosition = new Vector3(5f, 7.6f, 0f);
		Target.transform.localPosition = new Vector3(Random.Range(1f, 8f), 6f, 0);
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

		transform.localPosition += new Vector3(moveX * 3f, 0, 0) * Time.deltaTime * _moveSpeed;   
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "MLBall")
		{
			AddReward(+1f);
		}
	}
}
