using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TwoPlayerMLCountdown : MonoBehaviour
{
	private bool _active;
	private float _currentTime;
	public Text CountdownText;
	public TwoPlayerMLBall SelectedBall;
	public GameObject CountdownObject;

	private void Start()
	{
		_active = false;
		_currentTime = 3f;
	}

	private void Update()
	{
		// check if the countdown is active
		if (_active)
		{
			_currentTime -= 1 * Time.deltaTime;
			CountdownText.text = _currentTime.ToString("0") + "...";

			// check if we've hit zero
			if (_currentTime < 1)
			{
				// reset everything
				_active = false;
				CountdownObject.SetActive(false);
				_currentTime = 3f;

				// launch ball
				SelectedBall.AutomaticLaunch();
			}
		}
	}

	public void ActivateCountdown()
	{
		_active = true;
		CountdownObject.SetActive(true);
	}
}
