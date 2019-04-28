using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public bool renderGizmos = false;

    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    private Transform startPos;
    private Node[,] grid;
    private List<Node> path;

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
        for (int j = 0; j < gridSizeY; j++)
        {
            for (int i = 0; i < gridSizeX; i++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool wall = Physics.CheckSphere(worldPoint, nodeRadius, wallMask);
                grid[j, i] = new Node(wall, worldPoint, i, j);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPos)
    {
        float xPoint = Mathf.Clamp01((worldPos.x + gridWorldSize.x) / gridWorldSize.x);
        float yPoint = Mathf.Clamp01((worldPos.y + gridWorldSize.y) / gridWorldSize.y);
    }

    private void OnDrawGizmos()
    {
        if (renderGizmos) { 
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    if (!n.isWall)
                        Gizmos.color = Color.green;
                    else
                        Gizmos.color = Color.red;

                    if (path != null)
                    {
                        Gizmos.color = Color.blue;
                    }

                    Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - distance));
                }
            }
        }
    }
}