using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public bool hasBeenHit;

    void Start()
    {
        hasBeenHit = false;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            if (!hasBeenHit)
            {
                hasBeenHit = true;

                // update player's points
                GameManager.instance.IncrementPoints(1);

                // remove the brick from the game
                Destroy(gameObject);
            }
        }
    }



}
