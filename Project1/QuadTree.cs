namespace Project1;

using System;
using System.Text;


// Quadtree class representing the spatial data structure
public class Quadtree
    {
        private Node root;

        public Quadtree()
        {
            root = new LeafNode(-50, -50, 100, 100);
        }

        public void Insert(int x, int y, int width, int height)
        {
            root = root.Insert(x, y, width, height);
        }

        public void Delete(int x, int y)
        {
            root = root.Delete(x, y);
        }

        public void Find(int x, int y)
        {
            Console.WriteLine(root.Find(x, y));
        }

        public void Update(int x, int y, int width, int height)
        {
            root = root.Update(x, y, width, height);
        }

        public void Dump()
        {
            var sb = new StringBuilder();
            root.Dump(sb, 0);
            Console.Write(sb.ToString());
        }
    }

