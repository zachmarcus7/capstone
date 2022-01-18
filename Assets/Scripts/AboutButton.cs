using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AboutButton : MonoBehaviour
{
  public Button GameButton;
  public AboutPopUp PopUp;

  private void Start()
  {
    GameButton = GetComponent<Button>();
    GameButton.onClick.AddListener(OnClick);
  }

  public void OnClick()
  {
    PopUp.MakeActive();
  }
}
