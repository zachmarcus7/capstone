using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OnePlayerButton : MonoBehaviour
{
	private Button _onePlayerButton;

	private void Start()
	{
		_onePlayerButton = GetComponent<Button>();
		_onePlayerButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		SceneManager.LoadScene("Main");
	}
}
