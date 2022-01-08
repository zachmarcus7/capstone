using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerHardButton : MonoBehaviour
{
	public Button hardButton;

	void Start()
	{
		hardButton = GetComponent<Button>();
		hardButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("TwoPlayerHard");
	}
}
