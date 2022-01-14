using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
	public Button backButton;
	public AboutPopUp popUp;


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
