using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPath : MonoBehaviour
{
    public SearchNode initialNode;
    public SearchNode Node { get; set; }

    void Start()
    {
        Node = (initialNode == null) ? SearchNode.GetRandomSearchNode() : initialNode;       
    }

    public void Move(Movement movement)
    {
        if (Node != null)
        {
            movement.MoveTowards(Node.transform.position);
        }
    }
}
