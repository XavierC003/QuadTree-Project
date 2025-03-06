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
        InternalNode? internalNode = null;
        try
        {
            LeafNode leaf = node as LeafNode ?? throw new Exception("Node is not a leaf node");
            try {
                leaf.Rectangles.Add(rect);
                return;
            }
            catch {
                Split(leaf);
            }
        }
        catch
        {
            internalNode = node as InternalNode ?? throw new Exception("Node is not an internal node");
        }

        if (internalNode == null) {
            throw new Exception("Internal node is null.");
        }

        try {
            bool right = rect.X >= internalNode!.X + internalNode.Width / 2;
            bool bottom = rect.Y >= internalNode.Y + internalNode.Height / 2;

            int index = (right ? 1 : 0) + (bottom ? 2 : 0);

            Insert(internalNode.Children[index], rect);
        }

        catch (Exception ex) {
            Console.WriteLine($"Insert failed: {ex.Message}");
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

        if (root == leaf) {
            root = internalNode;
        }
        
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
        Dump(root, 0); // Start dumping from the root with depth 0
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while dumping quadtree: {ex.Message}");
    }

    // Nested recursive function with try-catch for safety
    void Dump(Node node, int depth)
    {
        try
        {
            if (node == null)
            {
                Console.WriteLine("Error: Encountered a null node.");
                return;
            }

            string indent = new string(' ', depth * 4); // Indentation for readability

            if (node is LeafNode leaf)
            {
                Console.WriteLine($"{indent}LeafNode - ({leaf.X}, {leaf.Y}) - {leaf.Width}x{leaf.Height}");
                
                foreach (var rect in leaf.Rectangles)
                {
                    try
                    {
                        Console.WriteLine($"{indent}  rectangle - ({rect.X}, {rect.Y}) - {rect.Width}x{rect.Height}");
                    }
                    catch (Exception rectEx)
                    {
                        Console.WriteLine($"{indent}  Error while printing rectangle: {rectEx.Message}");
                    }
                }
            }
            else if (node is InternalNode internalNode)
            {
                Console.WriteLine($"{indent}InternalNode - ({internalNode.X}, {internalNode.Y}) - {internalNode.Width}x{internalNode.Height}");

                foreach (var child in internalNode.Children)
                {
                    try
                    {
                        if (child != null)
                        {
                            Dump(child, depth + 1); // Recursive call for child nodes
                        }
                    }
                    catch (Exception childEx)
                    {
                        Console.WriteLine($"{indent}  Error while dumping child node: {childEx.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"{indent}Error: Unknown node type.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing node at depth {depth}: {ex.Message}");
        }
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

                Console.WriteLine($"Processing line: '{cleanLine}'");
                var parts = cleanLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue; // Skip empty lines

                Console.WriteLine($"Parsed parts: {string.Join(", ", parts)}");
                try{
                    switch (parts[0].ToLower()) // Ensure commands are case-insensitive
                    {
                        case "insert":
                        if (parts.Length != 5) throw new Exception("Insert requires 4 arguments.");
                        if (!int.TryParse(parts[1], out int ix) || !int.TryParse(parts[2], out int iy) ||
                            !int.TryParse(parts[3], out int iw) || !int.TryParse(parts[4], out int ih))
                            throw new FormatException("Insert command contains invalid number format.");
                        Insert(ix, iy, iw, ih);
                        break;

                        case "delete":
                        if (parts.Length != 3) throw new Exception("Delete requires 2 arguments.");
                        if (!int.TryParse(parts[1], out int dx) || !int.TryParse(parts[2], out int dy))
                            throw new FormatException("Delete command contains invalid number format.");
                        Delete(dx, dy);
                        break;

                    case "find":
                        if (parts.Length != 3) throw new Exception("Find requires 2 arguments.");
                        if (!int.TryParse(parts[1], out int fx) || !int.TryParse(parts[2], out int fy))
                            throw new FormatException("Find command contains invalid number format.");
                        Find(fx, fy);
                        break;

                    case "update":
                        if (parts.Length != 5) throw new Exception("Update requires 4 arguments.");
                        if (!int.TryParse(parts[1], out int ux) || !int.TryParse(parts[2], out int uy) ||
                            !int.TryParse(parts[3], out int uw) || !int.TryParse(parts[4], out int uh))
                            throw new FormatException("Update command contains invalid number format.");
                        Update(ux, uy, uw, uh);
                        break;

                    case "dump":
                        if (parts.Length != 1) throw new Exception("Dump takes no arguments.");
                        Dump();
                        break;

                    default:
                        throw new Exception($"Unknown command: {parts[0]}");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Error: {fe.Message}");
                Environment.Exit(1); // Required exit on invalid number formats
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1); // Required exit on parsing or unknown command errors
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing command file: {ex.Message}");
        Environment.Exit(1);
    }
}

}
