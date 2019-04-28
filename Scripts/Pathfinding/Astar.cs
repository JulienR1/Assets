using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Astar : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = this.GetComponent<Grid>();
    }

    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.NodeFromWorldPosition(startPos);
    }

}
