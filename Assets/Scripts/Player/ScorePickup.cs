using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            GameSession.Instance.AddPoints((int)amount);

            SpawnObject(other);
            Destroy(gameObject);
        }
    }
}
