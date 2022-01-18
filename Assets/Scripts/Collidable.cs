namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// This is used with the bricks in order to
    /// increment the player's points and get rid of 
    /// a brick when it's struck.
    /// </summary>
    public class Collidable : MonoBehaviour
    {
        private bool _hasBeenHit;

        private void Start()
        {
            _hasBeenHit = false;
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Ball")
            {
                if (!_hasBeenHit)
                {
                    _hasBeenHit = true;

                    // update player's points
                    GameManager.Instance.IncrementPoints(1);

                    // remove the brick from the game
                    Destroy(gameObject);
                }
            }
        }
    }
}