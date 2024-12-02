using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

[System.Serializable]
public struct TacticalQuality
{
    public float Value;
    public float Weight;

}
public class Node : MonoBehaviour
{
    public bool Reacheable = true;
    public int ID;
    public List<TacticalQuality> EnemyQualities;
    public List<TacticalQuality> CharacterQualities;
    public List<Node> Neighbors;
    public bool IsTactical;
    

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
