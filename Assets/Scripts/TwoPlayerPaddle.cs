using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoPlayerPaddle : MonoBehaviour
{
    private Vector3 _lastMousePosition;
    private float _speed;
    public float RightScreenEdge;
    public float LeftScreenEdge;

    private void start()
    {
        _speed = 15;
    }

    private void Update()
    {
        // for pausing
        if (Pause.Active)
        {
            return;
        }

        // check if mouse has moved
        if (Input.mousePosition != _lastMousePosition)
        {
            _lastMousePosition = Input.mousePosition;
            OnMouseMovement();
        }
        else
        {
            OnMouseNotMoving();
        }

        // check if paddle has hit an edge
        if (transform.localPosition.x < LeftScreenEdge)
        {
            transform.localPosition = new Vector3(LeftScreenEdge, 0.68f, 0);
        }
        if (transform.localPosition.x > RightScreenEdge)
        {
            transform.localPosition = new Vector3(RightScreenEdge, 0.68f, 0);
        }
    }

    private void OnMouseMovement()
    {
        Camera cam = Camera.main;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 paddlePosition = new Vector3(mousePosition.x, transform.position.y, 0);
        transform.position = paddlePosition;
    }

    private void OnMouseNotMoving()
    {
        float xVal = Input.GetAxisRaw("Horizontal");
        Vector3 moveDelta = new Vector3(xVal, 0, 0);
        transform.Translate(moveDelta.x * Time.deltaTime * _speed, 0, 0);
    }
}
