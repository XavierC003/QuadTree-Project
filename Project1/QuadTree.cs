namespace Project1;

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
        root = new LeafNode { X = -50, Y = -50, Width = 100, Height = 100 };
    }

    // Insert Rectangle
    public void Insert(int x, int y, int width, int height)
    {
        try
        {
            Rectangle newRect = new Rectangle(x, y, width, height);
            Insert(root, newRect);
            Console.WriteLine($"Inserted rectangle at ({x}, {y}) with size {width}x{height}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Insertion Failed: {ex.Message}");
        }
    }

    // Recursive function to make sure the rectangle in the right node
    private void Insert(Node node, Rectangle rect)
    {
        try
        {
            LeafNode leaf = node as LeafNode ?? throw new Exception("Node is not a leaf node");
            if (leaf.Rectangles.Count < Max_Rectangles)
            {
                // Add rectangle if hit limit
                leaf.Rectangles.Add(rect);
            }
            else
            {
                //Split node when hit limit
                Split(leaf);
                Insert(leaf, rect);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during insertion: {ex.Message}");
        }
    }

    // Splits a leaf node into an internal node
    private void Split(LeafNode leaf)
    {
        InternalNode internalNode = new InternalNode
        {
            X = leaf.X,
            Y = leaf.Y,
            Width = leaf.Width,
            Height = leaf.Height
        };

        for (int i = 0; i < 4; i++) {
            internalNode.Children[i] = new LeafNode { X = leaf.X, Y = leaf.Y, Width = leaf.Width / 2, Height = leaf.Height / 2 };
        }

        foreach (var rect in leaf.Rectangles){
            Insert(internalNode, rect);
        }
        leaf = internalNode;
    }

    // Delete Rectangle at certain coordinates
    public void Delete(int x, int y)
    {
        try
        {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(r => r.X == x && r.Y == y) ?? throw new Exception($"Nothing to delete at ({x}, {y})");
            leaf.Rectangles.Remove(rect);
            Console.WriteLine($"Deleted rectangle at ({x}, {y})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deletion failed: {ex.Message}");
        }
    }

    // Finds a rectangle at certain coordinates
    public void Find(int x, int y)
    {
        try
        {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(r => r.X == x && r.Y == y) ?? throw new Exception($"Nothing is at ({x}, {y})");
            Console.WriteLine($"Rectangle at ({x}, {y}): {rect.Width}x{rect.Height}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Find operation failed: {ex.Message}");
        }
    }

    // Dumps the entire quadtree
    public void Dump()
    {
        Console.WriteLine("Dumping quadtree...");
        try
        {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            foreach (var rect in leaf.Rectangles)
            {
                Console.WriteLine($"Rectangle at ({rect.X}, {rect.Y}): {rect.Width}x{rect.Height}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Update the dimensions of rectangle
    public void Update(int x, int y, int width, int height)
    {
        try
        {
            LeafNode leaf = root as LeafNode ?? throw new Exception("Root is not a leaf node");
            var rect = leaf.Rectangles.Find(r => r.X == x && r.Y == y) ?? throw new Exception($"Nothing to update at ({x}, {y})");
            rect.Width = width;
            rect.Height = height;
            Console.WriteLine($"Updated rectangle at ({x}, {y}) to size {width}x{height}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update failed: {ex.Message}");
        }
    }
    // Read and process commands from a file
    public void ProcessCommands(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Command file not found.");

            foreach (var line in File.ReadLines(filePath))
            {
                //Remove  any extra spaces
                string cleanLine = line.Trim();
                //Remove semicolon
                if (cleanLine.EndsWith(";")) {
                    cleanLine = cleanLine.Substring(0, cleanLine.Length - 1);
                }

                Console.WriteLine($"Processing line: '{cleanLine}");
                var parts = cleanLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue; // Skip empty lines

                Console.WriteLine($"Parsed parts: {string.Join(", ", parts)}");

                switch (parts[0])
                {
                    case "insert":
                        Insert(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                        break;
                    case "delete":
                        Delete(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "find":
                        Find(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "update":
                        Update(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                        break;
                    case "dump":
                        Dump();
                        break;
                    default:
                        Console.WriteLine("Error: Invalid command.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing command file: {ex.Message}");
        }
    }
}
