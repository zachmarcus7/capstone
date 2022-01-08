using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class openingMenuBall : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rigidBody;
    public Vector3 previousVelocity;
    public bool inPlay;
    public float randomXCoord;
    public float randomYCoord;
    public float randXStart;
    public float randXEnd;
    public float randYStart;
    public float randYEnd;


    void Start()
    {
        Renderer visual = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        visual.enabled = !visual.enabled;
        AutomaticLaunch();
    }


    void Update()
    {
        if (inPlay)
        {
            int yValue = 1;
            if (rigidBody.velocity.magnitude < 5)
            {
                Vector2 minimumVelocity = new Vector2(0, yValue);
                rigidBody.velocity += minimumVelocity;
            }
        }
        previousVelocity = rigidBody.velocity;
    }


    private void AutomaticLaunch()
    {
        Renderer visual = GetComponent<Renderer>();
        Vector2 direction = new Vector2(200, 15);  
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }

}
