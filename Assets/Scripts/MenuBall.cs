namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// This is for the ball that launches and bounces
    /// around the screen in the opening menu.
    /// </summary>
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
                // this makes sure the ball stays at a minimum speed
                int yValue = 1;
                if (_rigidBody.velocity.magnitude < 5)
                {
                    Vector2 minimumVelocity = new Vector2(0, yValue);
                    _rigidBody.velocity += minimumVelocity;
                }
            }
        }

        // this launches the ball whenever the opening menu starts
        private void AutomaticLaunch()
        {
            Vector2 direction = new Vector2(200, 15);
            _rigidBody.AddForce(direction);
            _inPlay = true;
            _visual.enabled = true;
        }
    }
}