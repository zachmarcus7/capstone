namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// This is used with the bricks in two-player order to
    /// increment the player's or ai's points and get rid of 
    /// a brick when it's struck.
    /// </summary>
    public class TwoPlayerCollidable : MonoBehaviour
    {
        private bool _hasBeenHit;

        private void Start()
        {
            _hasBeenHit = false;
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (!_hasBeenHit)
            {
                _hasBeenHit = true; 
            }

            // update either the player's points or ml agent's points
            if (coll.gameObject.tag == "Ball")
            {
                GameManager.Instance.IncrementPoints(1);
            }
            else
            {
                MLGameManager.Instance.IncrementPoints(1);
            }

            // remove the brick from the game
            Destroy(gameObject);
        }
    }
}