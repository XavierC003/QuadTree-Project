namespace Project1;

using System;
using System.Text;


// Quadtree class representing the spatial data structure
public class Quadtree
    {
        private Node root;


        /// <summary>
        /// Initializes the quadtree with a 100x100 root leaf node.
        /// </summary>
        public Quadtree()
        {
            root = new LeafNode(-50, -50, 100, 100);
        }

        /// <summary>
        /// Inserts a rectangle into the quadtree.
        /// </summary>
        /// <param name="x">X coordinate of the rectangle</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Insert(int x, int y, int width, int height)
        {
            root = root.Insert(x, y, width, height);
        }

        /// <summary>
        /// Deletes a rectangle at the specified coordinates.
        /// </summary>
        public void Delete(int x, int y)
        {
            root = root.Delete(x, y);
        }


        /// <summary>
        /// Finds and displays a rectangle at the specified location.
        /// </summary>

        public void Find(int x, int y)
        {
            Console.WriteLine(root.Find(x, y));
        }


        /// <summary>
        /// Updates the dimensions of a rectangle at the given coordinates.
        /// </summary>
        public void Update(int x, int y, int width, int height)
        {
            root = root.Update(x, y, width, height);
        }

        /// <summary>
        /// Prints the entire quadtree hierarchy.
        /// </summary>

        public void Dump()
        {
            var sb = new StringBuilder();
            root.Dump(sb, 0);
            Console.Write(sb.ToString());
        }
    }

