using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutButton : MonoBehaviour
{
	public Button aboutButton;
	public AboutPopUp popUp;

	void Start()
	{
		aboutButton = GetComponent<Button>();
		aboutButton.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		popUp.makeActive();
	}
}
