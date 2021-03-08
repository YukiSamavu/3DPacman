using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : Pickup
{
    [Range(1, 120)] public int colorChangeRate = 1;
    public Material material;

    private int currentFrame = 0;

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

    private void Update()
    {
        if (material == null) return;

        currentFrame++;
        if (currentFrame == colorChangeRate)
        {
            currentFrame = 0;

            int r = Random.Range(0, 256);
            int g = Random.Range(0, 256);
            int b = Random.Range(0, 256);

            material.color = new Color(r, g, b);
        }
    }
}
