using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TwoPlayerMediumButton : MonoBehaviour
{
	private Button _mediumButton;

	private void Start()
	{
		_mediumButton = GetComponent<Button>();
		_mediumButton.onClick.AddListener(OnClick);
	}

	public void OnClick()
	{
		SceneManager.LoadScene("TwoPlayerMedium");
	}
}
