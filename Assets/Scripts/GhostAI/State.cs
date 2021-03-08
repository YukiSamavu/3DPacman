using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void Enter(Agent owner);
    public abstract void Execute(Agent owner);
    public abstract void Exit(Agent owner);
}
