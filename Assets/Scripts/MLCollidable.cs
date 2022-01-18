using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCollidable : MonoBehaviour
{
    private bool _hasBeenHit;

    private void Start()
    {
        _hasBeenHit = false;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        /*
        if (coll.gameObject.tag == "MLBall")                           
        {
            if (!_hasBeenHit)                                             // CHANGED FOR TRAINING
            {
                _hasBeenHit = true;

                // update player's points
                MLGameManager.Instance.IncrementPoints(1);

                // remove the brick from the game
                Destroy(gameObject);
            }
        }
        */
    }
}
