using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AboutPopUp : MonoBehaviour
{
  private static bool _active;
  public AboutPopUp Instance;
  public GameObject PopUp;

  private void Start()
  {
    _active = false;
  }

  private void Update()
  {
    if (_active)
    {
      PopUp.SetActive(true);
    }
    else
    {
      PopUp.SetActive(false);
    }
}

  public void MakeActive()
  {
    _active = true;
  }

  public void MakeInactive()
  {
    _active = false;
  }
}
