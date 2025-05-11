// Class representing a rectangle with position and dimensions
public class Rectangle
{
    public double X { get; set; } // X=coordinate of the bottom left 
    public double Y { get; set; } // Y-Coordinate of the bottom left
    public double Width { get; set; } // Width of the rectangle
    public double Height { get; set; } // Height of the rectangle

    // Constructor to initialize a rectangle with given values
    public Rectangle(double x, double y, double width, double height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

}
