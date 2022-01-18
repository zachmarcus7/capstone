using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TwoPlayerHardButton : MonoBehaviour
{
	private Button _hardButton;

	private void Start()
	{
		_hardButton = GetComponent<Button>();
		_hardButton.onClick.AddListener(OnClick);
	}

	public void OnClick()
	{
		SceneManager.LoadScene("TwoPlayerHard");
	}
}
