using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerMLCountdown : MonoBehaviour
{
	public static bool active = false;
	public float currentTime = 0f;
	public float startingTime = 3f;
	public Text countdownText;
	public TwoPlayerMLBall ball;
	public GameObject countdown;

	void Start()
	{
		currentTime = startingTime;
	}

	void Update()
	{
		// check if the countdown is active
		if (active)
		{
			currentTime -= 1 * Time.deltaTime;
			countdownText.text = currentTime.ToString("0") + "...";

			// check if we've hit zero
			if (currentTime < 1)
			{
				// reset everything
				active = false;
				countdown.SetActive(false);
				currentTime = 3f;

				// launch ball
				ball.AutomaticLaunch();
			}
		}
	}

	public void activateCountdown()
	{
		active = true;
		countdown.SetActive(true);
	}
}
