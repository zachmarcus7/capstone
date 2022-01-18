using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackButton : MonoBehaviour
{
	public Button GameButton;
	public AboutPopUp PopUp;

	private void Start()
	{
		GameButton = GetComponent<Button>();
		GameButton.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		PopUp.MakeInactive();
	}
}
