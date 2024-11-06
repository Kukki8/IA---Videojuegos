using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 Location;
    private bool m_visited = false;
    public bool Reacheable = true;
    public int ID;

    public List<Node> Neighbors;

}
