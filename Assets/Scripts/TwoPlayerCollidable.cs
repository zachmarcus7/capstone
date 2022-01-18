using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoPlayerCollidable : MonoBehaviour
{
    private bool _hasBeenHit;

    private void Start()
    {
        _hasBeenHit = false;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // CHANGED FOR TRAINING
        /*
        
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
        */
        
    }
}

