using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerEasyButton : MonoBehaviour
{
	public Button EasyButton;

	private void Start()
	{
		EasyButton = GetComponent<Button>();
		EasyButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		SceneManager.LoadScene("TwoPlayerEasy");
	}
}
