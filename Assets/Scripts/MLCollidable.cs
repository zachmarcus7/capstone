using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCollidable : MonoBehaviour
{
    public bool hasBeenHit;


    void Start()
    {
        hasBeenHit = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        /*
        if (coll.gameObject.tag == "MLBall")                           
        {
            if (!hasBeenHit)                                             // CHANGED FOR TRAINING
            {
                hasBeenHit = true;

                // update player's points
                MLGameManager.instance.IncrementPoints(1);

                // remove the brick from the game
                Destroy(gameObject);
            }
        }
        */
    }
}
