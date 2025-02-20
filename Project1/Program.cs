// See https://aka.ms/new-console-template for more information
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

// Quadtree class representing the spatial data structure
public class Quadtree
{
    // Root node of the quadtree
    privatw Node root; // Root node of the quadtree

    // Constructor initializing the quadtree with a root leaf node
    public Quadtree()
    {
        root = new LeafNode {X = -50, Y = -50, Width = 100, Height = 100};
    }
}

// Program class test code

class Program 
{
    static void Main()
    {
        Console.WriteLine("Creating a new rectangle...");

        //rectangle instance
        Rectangle rect = new Rectangle(10,20,30,40);

        //Displaying rectangle properties
        Console.WriteLine($"Rectangle created at ({rect.X}, {rect.Y}) with width {rect.Width}, and height {rect.Height}");

        // Leaf node adding rectangle
        LeafNode leaf = new LeafNode { X = 0, Y =0, Width = 100, Height = 100};
        leaf.Rectangles.Add(rect);

        Console.WriteLine("Added rectangle to LeafNode. ");
        Console.WriteLine($"LeafNode contains {leaf.Rectangles.Count} rectangle(s).");

        //Internal Node
        InternalNode internalNode = new InternalNode { X = 0, Y = 0, Width = 200, Height = 200};
        internalNode.Children[0] = leaf;

        Console.WriteLine("InternalNode created and assigned a Leafnode as a child.");
    }
}

