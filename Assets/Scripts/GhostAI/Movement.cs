using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public float speedMax = 2;
    public float accelerationMax = 2;
    public float turnRate = 90;
    public bool orientToMovement = true; 

    public virtual Vector3 Velocity { get; set; }
    public virtual Vector3 Acceleration { get; set; }
    public virtual Vector3 Direction { get { return Velocity.normalized; } }

    public abstract void MoveTowards(Vector3 target);
    public abstract void ApplyForce(Vector3 force);
    public abstract void Stop();
    public abstract void Resume();
}
