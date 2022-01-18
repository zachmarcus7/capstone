using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MLPlayerButton : MonoBehaviour
{
	private Button _mlPlayerButton;

	private void Start()
	{
		_mlPlayerButton = GetComponent<Button>();
		_mlPlayerButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		SceneManager.LoadScene("MLAgentScreen");
	}
}
