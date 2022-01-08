using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerEasyButton : MonoBehaviour
{
	public Button easyButton;

	void Start()
	{
		easyButton = GetComponent<Button>();
		easyButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("TwoPlayerEasy");
	}
}
