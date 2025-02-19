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

// Leaf node class representing a node that holds rectangles.
public class LeafNode : Node
{
    // List of rectangles contained in the leaf node
    public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
    
}

// Internal node class representing a node that splits into four children
public class InternalNode : Node
{
    // Array of four child nodes
    public Node[] Children { get; set; } = new Node[4];
}

// Class representing a rectangle with position and dimensions
public class Rectangle{
    public int X { get; set; } // X=coordinate of the bottom left 
    public int Y { get; set; } // Y-Coordinate of the bottom left
    public int Width { get; set; } // Width of the rectangle
    public int Height { get; set; } // Height of the rectangle
    // Constructor to initialize a rectangle with given values
public Rectangle(int x, int y, int width, int height)
{
    X = x;
    Y = y;
    Width = width;
    Height = height;
}

}
