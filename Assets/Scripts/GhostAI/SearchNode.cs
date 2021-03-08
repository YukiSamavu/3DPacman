using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SearchNode : MonoBehaviour
{
    public static SearchNode GetRandomSearchNode()
    {
        SearchNode[] searchNodes = FindObjectsOfType<SearchNode>();
        if (searchNodes.Length > 0)
        {
            return searchNodes[Random.Range(0, searchNodes.Length)];
        }
        else
        {
            return null;
        }
    }
}
