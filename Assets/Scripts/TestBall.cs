using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : Ball
{
    public override void AutomaticLaunch()
    {
        Renderer visual = GetComponent<Renderer>();
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-200, 200), 200);
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }
}
