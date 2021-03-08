using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float amount;
    public GameObject spawnObject;

    public abstract void OnTriggerEnter(Collider other);

    public void SpawnObject(Collider other)
    {
        if (spawnObject != null)
        {
            GameObject gameObject = Instantiate(spawnObject, other.transform.position, other.transform.rotation, other.transform);
            Destroy(gameObject, 2);
        }
    }
}
