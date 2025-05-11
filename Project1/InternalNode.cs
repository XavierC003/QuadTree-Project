using System.Text;

namespace Project1
{
    /// <summary>
    /// An internal node in the quadtree that contains four child quadrants.
    /// </summary>
    public class InternalNode : Node
    {
        private Node[] children;

        /// <summary>
        /// Creates an internal node and initializes its four child nodes.
        /// </summary>
        public InternalNode(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            int halfW = width / 2;
            int halfH = height / 2;

            children = new Node[4];
            children[0] = new LeafNode(x, y, halfW, halfH);
            children[1] = new LeafNode(x + halfW, y, halfW, halfH);
            children[2] = new LeafNode(x, y + halfH, halfW, halfH);
            children[3] = new LeafNode(x + halfW, y + halfH, halfW, halfH);
        }

        private int GetQuadrant(int x, int y)
        {
            bool right = x >= X + Width / 2;
            bool top = y >= Y + Height / 2;
            if (top) return right ? 3 : 2;
            return right ? 1 : 0;
        }

        /// <summary>Inserts a rectangle by delegating to the appropriate child quadrant.</summary>
        public override Node Insert(int x, int y, int width, int height)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Insert(x, y, width, height);
            return this;
        }

        /// <summary>Deletes a rectangle by locating the correct child.</summary>
        public override Node Delete(int x, int y)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Delete(x, y);
            return this;
        }

        /// <summary>Finds a rectangle by routing to the appropriate quadrant.</summary>
        public override string Find(int x, int y)
        {
            int index = GetQuadrant(x, y);
            return children[index].Find(x, y);
        }

        /// <summary>Updates a rectangle by delegating to the correct child.</summary>
        public override Node Update(int x, int y, int newW, int newH)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Update(x, y, newW, newH);
            return this;
        }

        /// <summary>Dumps the current node and all child subtrees recursively.</summary>
        public override void Dump(StringBuilder sb, int depth)
        {
            sb.AppendLine(new string('\t', depth) + $"Node at {X},{Y}: {Width}x{Height}");
            foreach (var child in children)
            {
                child.Dump(sb, depth + 1);
            }
        }
    }
}
