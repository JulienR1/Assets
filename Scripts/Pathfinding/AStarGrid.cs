using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    private Transform startPos;
    private Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool wall = !Physics.CheckSphere(worldPoint, nodeRadius, wallMask);
                grid[i, j] = new Node(wall, worldPoint, i, j);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        float xPoint = Mathf.Clamp01((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float yPoint = Mathf.Clamp01((worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node currentNode)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = currentNode.gridX + x;
                int checkY = currentNode.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.isWall) ? Color.white : Color.blue;
                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }
}