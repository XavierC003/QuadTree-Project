// Class representing a rectangle with position and dimensions
public class Rectangle
{
    public int X { get; set; } // X=coordinate of the bottom left 
    public int Y { get; set; } // Y-Coordinate of the bottom left
    public int Width { get; set; } // Width of the rectangle
    public int Height { get; set; } // Height of the rectangle

    // Constructor to initialize a rectangle with given values
    public Rectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

}
