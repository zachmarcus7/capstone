using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class MenuBall : MonoBehaviour
{
    private Renderer _visual;
    private bool _inPlay;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _visual = GetComponent<Renderer>();
        _visual.enabled = !_visual.enabled;
        AutomaticLaunch();
    }

    private void Update()
    {
        if (_inPlay)
        {
            int yValue = 1;
            if (_rigidBody.velocity.magnitude < 5)
            {
                Vector2 minimumVelocity = new Vector2(0, yValue);
                _rigidBody.velocity += minimumVelocity;
            }
        }
    }

    private void AutomaticLaunch()
    {
        Vector2 direction = new Vector2(200, 15);
        _rigidBody.AddForce(direction);
        _inPlay = true;
        _visual.enabled = true;
    }
}
