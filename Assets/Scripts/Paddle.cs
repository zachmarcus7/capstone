namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// This is used with the paddle in the one-player
    /// regular mode, allowing the user to control it.
    /// </summary>
    public class Paddle : MonoBehaviour
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
                OnMouseMoving();
            }
            else
            {
                OnMouseNotMoving();
            }

            // check if paddle has hit an edge
            if (transform.localPosition.x < LeftScreenEdge)
            {
                transform.localPosition = new Vector3(LeftScreenEdge, transform.position.y, 0);
            }
            if (transform.localPosition.x > RightScreenEdge)
            {
                transform.localPosition = new Vector3(RightScreenEdge, transform.position.y, 0);
            }
        }

        private void OnMouseMoving()
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
}