namespace Project1;

using Systen;
using System.Collections.Generic;

// Base class for quadtree nodes. Stores all the common properties
public abstract class Node
{
    public int x { get; set; }  // X-coordinate of the node
    public int y { get; set: }  // Y-coordinate of the node
    public int Width { get; set; }  // Width of the node's area
    public int Height { get; set; }  // Height of the node's area
}

// Leaf node class representing a node that holds rectangles.
public class LeafNode : Node
{
    // List of rectangles contained in the leaf node
    public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
    
}

