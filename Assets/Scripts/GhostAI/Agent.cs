using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public Perception perception;
    public Movement movement;
    public Animator animator;
}
