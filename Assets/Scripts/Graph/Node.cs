using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Priority_Queue;

public class Node : MonoBehaviour
{
    public bool Reacheable = true;
    public int ID;

    public List<Node> Neighbors;

}

public class NodeRecord: FastPriorityQueueNode
{
    public Node Node;
    public float CostSoFar;

    public NodeRecord(Node node)
    {
        Node = node;
    }
}
