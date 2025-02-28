namespace Project1;

// Leaf node class representing a node that holds rectangles.
public class LeafNode : Node
{
    // List of rectangles contained in the leaf node
    public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
}
