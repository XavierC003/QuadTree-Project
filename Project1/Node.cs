namespace Project1;

using System;
using System.Collections.Generic;

// Base class for quadtree nodes. Stores all the common properties
public abstract class Node
{
    public int X { get; set; }  // X-coordinate of the node
    public int Y { get; set; }  // Y-coordinate of the node
    public int Width { get; set; }  // Width of the node's area
    public int Height { get; set; }  // Height of the node's area
}


