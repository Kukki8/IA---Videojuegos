using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public List<Node> nodes;
    public int SizeX;
    public int SizeY;
    public int Offset = 2;
    public float BoundaryX => transform.position.x + SizeX/2;
    public float BoundaryY => transform.position.z + SizeY/2;
    public int ElementsPerRow => SizeX/Offset - 1;
    public int ElementsPerCol => SizeY/Offset - 1;
    public GameObject NodePrefab;

    [ContextMenu("CreateGraph")]
    public void CreateGraph()
    {
        nodes.Clear();
        int index = 0;

        int x = (int)-BoundaryX + Offset;
        while(x < ElementsPerRow){
            int y = (int)-BoundaryY + Offset;
            while(y < ElementsPerCol){
                Vector3 currentPos = new Vector3(x, 0 , y);
                GameObject nodeObj = Instantiate(NodePrefab, currentPos, Quaternion.identity,transform);
                nodeObj.name = "node" + index;
                Node node = nodeObj.GetComponent<Node>();
                node.ID= index;
                nodes.Add(node);
                index++;
                y+= Offset;
            }

            x+= Offset;

        }

        foreach(Node node in nodes){
            CheckNeighbors(node);
        }
    }

    private void CheckNeighbors(Node node)
    {
        int top = node.ID - ElementsPerRow;
        int bot = node.ID + ElementsPerRow;
        int right = node.ID + 1;
        int left = node.ID - 1;
        int upperLeft = node.ID - ElementsPerRow - 1;
        int upperRight = node.ID - ElementsPerRow + 1;
        int lowerLeft = node.ID + ElementsPerRow - 1;
        int lowerRight = node.ID + ElementsPerRow + 1;


        Debug.Log("Chequeando nodo: "+ node.ID);

        if(top > -1){
            // Vecino superior
            node.Neighbors.Add(GetNode(top));
        }
        if((upperLeft % ElementsPerRow < ElementsPerRow - 1 && upperLeft > 0) || upperLeft == 0){
            // Vecino diagonal superior izq
            node.Neighbors.Add(GetNode(upperLeft));
        }
        if((left % ElementsPerRow < ElementsPerRow - 1 && left > 0 )|| left == 0){
            // Vecino izq
            node.Neighbors.Add(GetNode(left));
        }
        if(lowerLeft % ElementsPerRow < ElementsPerRow - 1 && lowerLeft < ElementsPerRow*ElementsPerCol){
            // Vecino diagonal inferior izq
            node.Neighbors.Add(GetNode(lowerLeft));
        }
        if(bot < ElementsPerRow * ElementsPerCol){
            // Vecino inferior
            node.Neighbors.Add(GetNode(bot));
        }
        if(lowerRight % ElementsPerRow > 0 && lowerRight < ElementsPerRow*ElementsPerCol){
            // Vecino diagonal inferior der
            node.Neighbors.Add(GetNode(lowerRight));
        }
        if(right % ElementsPerRow > 0){
            // Vecino der
            node.Neighbors.Add(GetNode(right));
        }  
        if(upperRight % ElementsPerRow > 0){
            // Vecino diagonal superior der
            node.Neighbors.Add(GetNode(upperRight));
        }
    
    }

    private Node GetNode(int index)
    {

        foreach(Node n in nodes){
            if(n.ID == index ){
                return n;
            }

        }
        return null;
    }

}
