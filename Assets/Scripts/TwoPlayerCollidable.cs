using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerCollidable : MonoBehaviour
{
    public bool hasBeenHit;

    void Start()
    {
        hasBeenHit = false;
    }

    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasBeenHit)
            hasBeenHit = true;

        // update either the player's points or ml agent's points
        if (coll.gameObject.tag == "Ball")
            GameManager.instance.IncrementPoints(1);
        else
            MLGameManager.instance.IncrementPoints(1);

        // remove the brick from the game
        Destroy(gameObject);
    }
}

