namespace Project1;

using System;
using System.Collections.Generic;
using System.Text;

// Leaf node class representing a node that holds rectangles.
    public class LeafNode : Node
    {
        private const int Capacity = 5;
        private List<Rectangle> rectangles;

        public LeafNode(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            rectangles = new List<Rectangle>();
        }

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

        public override string Find(int x, int y)
        {
            foreach (var rect in rectangles)
            {
                if (rect.X == x && rect.Y == y)
                    return $"Rectangle at {x}, {y}: {rect.Width}x{rect.Height}";
            }

            throw new Exception($"Nothing is at {x}, {y}.");
        }

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

        public override void Dump(StringBuilder sb, int depth)
        {
            sb.AppendLine(new string('\t', depth) + $"Node at {X},{Y}: {Width}x{Height}");
            foreach (var r in rectangles)
            {
                sb.AppendLine(new string('\t', depth + 1) + $"Rectangle at {r.X}, {r.Y}: {r.Width}x{r.Height}");
            }
        }
    }
