using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarGrid))]
public class Astar : MonoBehaviour
{
    private AStarGrid grid;

    private void Awake()
    {
        grid = this.GetComponent<AStarGrid>();
    }

    public void FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(endPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].FCost<currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
                GetFinalPath(startNode, targetNode);

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.isWall || closedList.Contains(neighbour))
                    continue;
                int moveCost = currentNode.gCost + getManhattanDistance(currentNode, neighbour);

                if (moveCost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = moveCost;
                    neighbour.hCost = getManhattanDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openList.Contains(neighbour))
                        openList.Add(neighbour);
                }
            }
        }
    }

    private void GetFinalPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.path = path;
        print(path.Count);
    }

    private int getManhattanDistance(Node a, Node b)
    {
        int ix = Mathf.Abs(a.gridX - b.gridX);
        int iy = Mathf.Abs(a.gridY - b.gridY);
        return ix + iy;
    }

}
