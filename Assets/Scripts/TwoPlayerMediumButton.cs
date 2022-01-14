using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerMediumButton : MonoBehaviour
{
	public Button mediumButton;


	void Start()
	{
		mediumButton = GetComponent<Button>();
		mediumButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("TwoPlayerMedium");
	}

}
