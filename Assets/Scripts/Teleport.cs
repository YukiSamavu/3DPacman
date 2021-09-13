using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");

        var tempDest = Utilities.Wrap(other.gameObject.transform.position, transform.position, destination.transform.position);
        
        other.gameObject.transform.SetPositionAndRotation(tempDest, other.gameObject.transform.rotation);
        //other.gameObject.transform.SetPositionAndRotation(destination.position, other.gameObject.transform.rotation);
    }
}
