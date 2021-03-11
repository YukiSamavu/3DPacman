using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : SearchNode
{
    RandomNode nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        Agent agent = other.GetComponent<Agent>();
        if (agent != null)
        {
            SearchPath searchPath = agent.GetComponent<SearchPath>();

            if (searchPath != null)
            {
                do
                {
                    searchPath.Node = SearchNode.GetRandomSearchNode();
                }
                while (searchPath.Node == this);
            }
        }
    }
}
