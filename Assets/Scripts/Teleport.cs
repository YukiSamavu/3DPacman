using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");
        other.gameObject.transform.SetPositionAndRotation(destination.transform.position, other.gameObject.transform.rotation);
        
        //other.gameObject.transform.SetPositionAndRotation(destination.position, other.gameObject.transform.rotation);
    }
}
