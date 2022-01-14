using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPopUp : MonoBehaviour
{
    public static AboutPopUp instance;
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
