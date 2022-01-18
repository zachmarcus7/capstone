using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoPlayerPopUp : MonoBehaviour
{
    private bool _active;
    public static TwoPlayerPopUp Instance;
    public GameObject PopUp;

    private void Start()
    {
        _active = false;
    }

    public void Update()
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
