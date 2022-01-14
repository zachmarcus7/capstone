using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerPopUp : MonoBehaviour
{
    public static TwoPlayerPopUp instance;
    public static bool active;
    public GameObject popUp;


    void Start()
    {
        active = false;
    }

    public void Update()
    {
        if (active)
            popUp.SetActive(true);
        else
            popUp.SetActive(false);
    }

    public void makeActive()
    {
        active = true;
    }

    public void makeInactive()
    {
        active = false;
    }

}
