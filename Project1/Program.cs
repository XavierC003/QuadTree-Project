// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace QuadtreeProject {
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

// Quadtree class representing the spatial data structure
public class Quadtree
{
    // Root node of the quadtree
    private Node root;
    // Max rectangles before splitting
    private const int Max_Rectangles = 5;

    // Constructor initializing the quadtree with a root leaf node
    public Quadtree()
    {
        root = new LeafNode {X = -50, Y = -50, Width = 100, Height = 100};
    }

    // Insert Rectangle
    public void Insert(int x, int y, int width, int height)
    {
        Console.WriteLine($"Inserting rectangle at ({x}, {y}) with size {width}x{height}");
    }

    // Delete Rectangle at certain coordinates
    public void Delete(int x, int y)
    {
        Rectangle newRect = new Rectangle(x, y, width, height);
        Insert(root, newRect);
    }

    // Finds a rectangle at certain coordinates
    public void Find(int x, int y)
    {
        Console.WriteLine($"Finding rectangle at ({x}, {y}) ");
    }

    // Dumps the entire quadtree
    public void Dump()
    {
        Console.WriteLine("Dumping quadtree...");
    }
}

// Main program class

