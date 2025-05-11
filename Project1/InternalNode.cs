namespace Project1

using System.Text;


{
    public class InternalNode : Node
    {
        private Node[] children;

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

        public override Node Insert(int x, int y, int width, int height)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Insert(x, y, width, height);
            return this;
        }

        public override Node Delete(int x, int y)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Delete(x, y);
            return this;
        }

        public override string Find(int x, int y)
        {
            int index = GetQuadrant(x, y);
            return children[index].Find(x, y);
        }

        public override Node Update(int x, int y, int newW, int newH)
        {
            int index = GetQuadrant(x, y);
            children[index] = children[index].Update(x, y, newW, newH);
            return this;
        }

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