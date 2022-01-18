using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayerBackButton : MonoBehaviour
{
	public Button BackButton;
	public TwoPlayerPopUp PopUp;

	private void Start()
	{
		BackButton = GetComponent<Button>();
		BackButton.onClick.AddListener(OnClick);
	}

	public void OnClick()
	{
		PopUp.MakeInactive();
	}
}
