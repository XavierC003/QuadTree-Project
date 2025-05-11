namespace Project1;

using System;
using System.Collections.Generic;
using System.Text;

// Leaf node class representing a node that holds rectangles.
    public class LeafNode : Node
    {
        private const int Capacity = 5;
        private List<Rectangle> rectangles;


        /// <summary>
        /// Initializes a new instance of a LeafNode with spatial boundaries.
        /// </summary>
        public LeafNode(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            rectangles = new List<Rectangle>();
        }

        /// <summary>
        /// Inserts a rectangle into the leaf node. Splits to InternalNode if over capacity.
        /// </summary>
        /// <returns>The original or new InternalNode after split</returns>
        /// <exception cref="Exception">Thrown if a rectangle already exists at the location</exception>
        public override Node Insert(int x, int y, int width, int height)
        {
            foreach (var rect in rectangles)
            {
                if (rect.X == x && rect.Y == y)
                    throw new Exception("You can not double insert at a position.");
            }

            rectangles.Add(new Rectangle(x, y, width, height));

            if (rectangles.Count > Capacity)
            {
                var internalNode = new InternalNode(X, Y, Width, Height);
                foreach (var r in rectangles)
                {
                    internalNode.Insert(r.X, r.Y, r.Width, r.Height);
                }
                return internalNode;
            }

            return this;
        }

        /// <summary>
        /// Deletes a rectangle at the specified location.
        /// </summary>
        /// <exception cref="Exception">Thrown if no rectangle found</exception>

        public override Node Delete(int x, int y)
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rectangles[i].X == x && rectangles[i].Y == y)
                {
                    rectangles.RemoveAt(i);
                    return this;
                }
            }

            throw new Exception($"Nothing to delete at {x}, {y}.");
        }

        /// <summary>
        /// Finds a rectangle at the specified coordinates.
        /// </summary>
        /// <returns>Formatted string with rectangle info</returns>
        /// <exception cref="Exception">Thrown if not found</exception>
        public override string Find(int x, int y)
        {
            foreach (var rect in rectangles)
            {
                if (rect.X == x && rect.Y == y)
                    return $"Rectangle at {x}, {y}: {rect.Width}x{rect.Height}";
            }

            throw new Exception($"Nothing is at {x}, {y}.");
        }


        /// <summary>
        /// Updates the width and height of a rectangle at a given location.
        /// </summary>
        /// <exception cref="Exception">Thrown if not found</exception>
        public override Node Update(int x, int y, int newW, int newH)
        {
            foreach (var rect in rectangles)
            {
                if (rect.X == x && rect.Y == y)
                {
                    rect.Width = newW;
                    rect.Height = newH;
                    return this;
                }
            }

            throw new Exception($"Nothing to update at {x}, {y}.");
        }

        /// <summary>
        /// Prints this node and all rectangles inside it to the StringBuilder.
        /// </summary>
        public override void Dump(StringBuilder sb, int depth)
        {
            sb.AppendLine(new string('\t', depth) + $"Node at {X},{Y}: {Width}x{Height}");
            foreach (var r in rectangles)
            {
                sb.AppendLine(new string('\t', depth + 1) + $"Rectangle at {r.X}, {r.Y}: {r.Width}x{r.Height}");
            }
        }
    }
