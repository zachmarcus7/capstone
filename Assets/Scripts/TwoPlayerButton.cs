using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerButton : MonoBehaviour
{
	public Button GameButton;
	public TwoPlayerPopUp PopUp;

	private void Start()
	{
		GameButton = GetComponent<Button>();
		GameButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		PopUp.MakeActive();
	}
}
