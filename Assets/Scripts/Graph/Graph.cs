using System.Collections.Generic;
using Extensions;
using Priority_Queue;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public static Graph Instance { get; private set; }  
    public TArray<Node> nodes;
    public int SizeX;
    public int SizeY;
    public int Offset = 2;
    public float BoundaryX => (transform.position.x + SizeX/2)*Offset;
    public float BoundaryY => (transform.position.z + SizeY/2)*Offset;
    public int ElementsPerRow => SizeX/Offset - 1;
    public int ElementsPerCol => SizeY/Offset - 1;
    public GameObject NodePrefab;
    public float TransitionCost = 1;
    private Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
        }
    }

    [ContextMenu("CreateGraph")]
    public void CreateGraph()
    {
        Debug.Log(BoundaryX);
        nodes = new Node[SizeX,SizeY];
        float y = BoundaryY;
        int index = 0;

        for(int i = 0; i < SizeY;i++)
        {
            float x = -BoundaryX;
            for(int j = 0; j< SizeX ;j++)
            {
                Vector3 currentPos = new Vector3(x, 0 , y);
                GameObject nodeObj = Instantiate(NodePrefab, currentPos, Quaternion.identity,transform);
                nodeObj.name = "node" + index;
                Node node = nodeObj.GetComponent<Node>();
                node.ID= index;

                nodes[i,j] = node;
                
                index++;
                x += Offset;
            }
            y -= Offset;
        }

        for(int i = 0; i < SizeX;i++)
        {
            for(int j = 0; j< SizeY ;j++)
            {
                CheckNeighbors(i,j, nodes[i,j]);

            }

        }
    }

    private void CheckNeighbors(int x, int y, Node node)
    {
        int top = x - 1;
        int bot = x + 1;
        int right = y + 1;
        int left = y - 1;

        if(top > -1){
            // Vecino superior
            node.Neighbors.Add(nodes[top,y]);
        }
        if(top > -1 && left > -1){
            // Vecino diagonal superior izq
            node.Neighbors.Add(nodes[top,left]);
        }
        if(left > -1){
            // Vecino izq
            node.Neighbors.Add(nodes[x,left]);
        }
        if(bot < SizeY && left > -1){
            // Vecino diagonal inferior izq
            node.Neighbors.Add(nodes[bot,left]);
        }
        if(bot < SizeY){
            // Vecino inferior
            node.Neighbors.Add(nodes[bot,y]);
        }
        if(bot < SizeY && right < SizeX){
            // Vecino diagonal inferior der
            node.Neighbors.Add(nodes[bot,right]);
        }
        if(right < SizeX){
            // Vecino der
            node.Neighbors.Add(nodes[x,right]);
        }  
        if(top > -1 && right < SizeX){
            // Vecino diagonal superior der
            node.Neighbors.Add(nodes[top,right]);
        }
    
    }

    public float Heuristic(Node a, Node b)
    {
        float DifX = Mathf.Abs(a.transform.position.x - b.transform.position.x);
        float DifY = Mathf.Abs(a.transform.position.z - b.transform.position.z);

        return DifX + DifY;
    }

    private List<Transform> MakePath(Node start, Node end)
    {
        List<Transform> path = new List<Transform>();
        Node current = end;

        while(current != start)
        {
            path.Add(current.transform);
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
 
    }

    public List<Transform> AStar(Node start, Node end)
    {
        cameFrom.Clear();
        NodeRecord startRecord = new NodeRecord(start);
        startRecord.CostSoFar = 0;
        Dictionary<Node,NodeRecord> visited = new Dictionary<Node,NodeRecord>();


        FastPriorityQueue<NodeRecord> open = new FastPriorityQueue<NodeRecord>(SizeX*SizeY);
        open.Enqueue(startRecord,0);
        visited[start] = startRecord;
        cameFrom[start] = start;

        while(open.Count > 0)
        {
            NodeRecord current = open.Dequeue();
            if(current.Equals(end))
            {
                break;
            }

            foreach(Node next in current.Node.Neighbors)
            {
                if(!next.Reacheable)
                {
                    continue;
                }

                float newCost = current.CostSoFar + TransitionCost;
                if(!visited.ContainsKey(next))
                {
                    visited[next] = new NodeRecord(next);
                    visited[next].CostSoFar = float.MaxValue;
                }
                if(newCost < visited[next].CostSoFar)
                {
                    visited[next].CostSoFar = newCost;
                    float priority = newCost + Heuristic(next,end);

                    if(open.Contains(visited[next])){
                        open.UpdatePriority(visited[next],priority);
                    }else
                    {
                        open.Enqueue(visited[next],priority);
                    }

                    cameFrom[next] = current.Node; 
                }
            }
        }

        return MakePath(start, end);

    }

    public Node MapToNode(Vector3 position)
    {
        float worldSizeX = SizeX*Offset;
        float x = (position.x + worldSizeX/2)/worldSizeX;
        x = Mathf.Abs(x);

        int j = Mathf.RoundToInt((SizeX-1)*x);

        float worldSizeY = SizeY*Offset;
        float y = (position.z + worldSizeY/2)/worldSizeY;
        y = 1 - Mathf.Abs(y);

        int i = Mathf.RoundToInt((SizeY-1)*y);

        Debug.Log("i = " + i + " j = " + j);
        return nodes[i,j];
    }

}
