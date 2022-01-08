using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerBackButton : MonoBehaviour
{
	public Button backButton;
	public TwoPlayerPopUp popUp;

	void Start()
	{
		backButton = GetComponent<Button>();
		backButton.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		popUp.makeInactive();
	}
}
