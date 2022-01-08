using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerButton : MonoBehaviour
{
	public Button twoPlayerButton;
	public TwoPlayerPopUp popUp;

	void Start()
	{
		twoPlayerButton = GetComponent<Button>();
		twoPlayerButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		popUp.makeActive();
	}
}
