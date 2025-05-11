using System;
using System.Text;
using System.Collections.Generic;

namespace Project1
{

// Base class for quadtree nodes. Stores all the common properties
public abstract class Node
{
    public double X { get; set; }  // X-coordinate of the node
    public double Y { get; set; }  // Y-coordinate of the node
    public double Width { get; set; }  // Width of the node's area
    public double Height { get; set; }  // Height of the node's area

        /// <summary>
        /// Inserts a rectangle into the node.
        /// </summary>
        /// <param name="x">X position of rectangle</param>
        /// <param name="y">Y position of rectangle</param>
        /// <param name="width">Width of rectangle</param>
        /// <param name="height">Height of rectangle</param>
        /// <returns>Updated node</returns>
        /// <exception cref="Exception">Thrown if insertion fails</exception>
    public abstract Node Insert(double x, double y, double width, double height); // Method to insert a rectangle into the node

            /// <summary>
        /// Deletes a rectangle at the given coordinates.
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns>Updated node</returns>
        /// <exception cref="Exception">If rectangle is not found</exception>
    public abstract Node Delete(double x, double y); // Method to delete a rectangle from the node

    /// <summary>
        /// Finds a rectangle at the given position.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>A string description of the rectangle</returns>
        /// <exception cref="Exception">If no rectangle is found</exception>
    public abstract string Find(double x, double y); // Method to find a rectangle at a given coordinate

      /// <summary>
        /// Updates an existing rectangle's size.
        /// </summary>
        /// <param name="x">X coordinate of the rectangle</param>
        /// <param name="y">Y coordinate of the rectangle</param>
        /// <param name="newWidth">New width</param>
        /// <param name="newHeight">New height</param>
        /// <returns>Updated node</returns>
        /// <exception cref="Exception">If no rectangle is found to update</exception>
    public abstract Node Update(double x, double y, double newWidth, double newHeight); // Method to update a rectangle in the node

     /// <summary>
        /// Appends the structure of this node to the string builder for printing.
        /// </summary>
        /// <param name="sb">StringBuilder to write output</param>
        /// <param name="depth">Current depth in the tree</param>
    public abstract void Dump(StringBuilder sb, int depth); // Method to dump the node's structure for debugging

}
}


