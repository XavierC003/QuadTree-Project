// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;

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
        try {
            Rectangle newRect = new Rectangle(x, y, width, height);
            Insert(root, newRect);
            Console.WriteLine($"Inserted rectangle at ({x}, {y}) with size {width}x{height}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Insertion Failed: {ex.Message}");
        }
    }

    // Recursive function to make sure the rectangle in the right node
    private void Insert(Node node, Rectangle rect)
    {
        try {
            LeafNode leaf = node as LeafNode ?? throw new Exception("Node is not a leaf node");
            if (leaf.Rectangles.Count < Max_Rectangles) {
                // Add rectangle if hit limit
                leaf.Rectangles.Add(rect);
            }
            else {
                //Split node when hit limit
                Split(leaf);
                Insert(root, rect);
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error during insertion: {ex.Message}");
        }
    }

    // Splits a leaf node into an internal node
    private void Split(LeafNode leaf)
    {
        InternalNode internalNode = new InternalNode {
            X = leaf.X,
            Y = leaf.Y,
            Width = leaf.Width,
            Height = leaf.Height
        };

        for (int i = 0; i < 4; i++)
        internalNode.Children[i] = new LeafNode { X = leaf.X, Y = leaf.Y, Width = leaf.Width / 2, Height = leaf.Height / 2};

        root = internalNode;
    }

    // Delete Rectangle at certain coordinates
    public void Delete(int x, int y)
    {
        try {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(r => r.X == x && r.Y == y) ?? throw new Exception($"Nothing to delete at ({x}, {y})");
            leaf.Rectangles.Remove(rect);
            Console.WriteLine($"Deleted rectangle at ({x}, {y})");
        }
        catch (Exception ex) {
            Console.WriteLine($"Deletion failed: {ex.Message}");
        }
    }

    // Finds a rectangle at certain coordinates
    public void Find(int x, int y)
    {
        try {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(rect => r.X == x && r.Y == y) ?? throw new Exception($"Nothing is at ({x}, {y})");
            Console.WriteLine($"Rectangle at ({x}, {y}): {rect.Width}x{rect.Height}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Find operation failed: {ex.Message}");
        }
    }

    // Dumps the entire quadtree
    public void Dump()
    {
        Console.WriteLine("Dumping quadtree...");
        try {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            foreach (var rect in leaf.Rectangles) {
                Console.WriteLine($"Rectangle at ({rect.X}, {rect.Y}): {rect.Wodth}x{rect.Height}");
            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    // Update the dimensions of rectangle
    public void Update(int x, int y, int width, int height) {
        try {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(r => r.X == x && r.Y == y) ?? throw new Exception($"Nothing to update at ({x}, {y})");
            rect.Width = width;
            rect.Height = height;
            Console.WriteLine($"Updated rectangle at ({x}, {y}) to size {width}x{height}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Update failed: {ex.Message}");
        }
    }
    // Read and process commands from a file
    public void ProcessComands(string filePath) {
        try {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Command file not found.");
            
            foreach (var line in File.ReadLines(filePath)){
                var parts = line.Split(' ');
                switch (parts[0]) {
                    case "Insert":
                        Insert(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                        break;
                    case "Delete":
                        Delete(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "Find":
                        Find(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "Update":
                        Update(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                        break;
                    case "Dump":
                        Dump();
                        break;
                    default:
                        Console.WriteLine("Error: Invalid command.");
                        break;
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error processing command file: {ex.Message}");
        }
    }
}

}

// Main program class

