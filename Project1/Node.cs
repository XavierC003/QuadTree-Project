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

    public abstract Node Insert(int x, int y, int width, int height); // Method to insert a rectangle into the node
    public abstract Node Delete(int x, int y); // Method to delete a rectangle from the node
    public abstract string Find(int x, int y); // Method to find a rectangle at a given coordinate
    public abstract Node Update(int x, int y, int newWidth, int newHeight); // Method to update a rectangle in the node
    public abstract void Dump(StringBuilder sb, int depth); // Method to dump the node's structure for debugging



}


