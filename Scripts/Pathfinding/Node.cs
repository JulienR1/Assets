﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int gridX, gridY;

    public bool isWall;
    public Vector3 position;

    public Node parent;

    public int gCost, hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node(bool isWall, Vector3 position, int gridX, int gridY)
    {
        this.isWall = isWall;
        this.position = position;
        this.gridX = gridX;
        this.gridY = gridY;
    }

}
