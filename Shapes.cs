namespace CleanCodeTest;

public class Shape_Base
{
    public virtual float Area() => 0;
    public virtual int CornerCount() => 0;
}

public class Square(float side) : Shape_Base
{

    public override float Area()
    {
        return side * side;
    }

    public override int CornerCount()
    {
        return 4;
    }
}

public class Rectangle(float Width, float Height) : Shape_Base
{
    public override float Area()
    {
        return Width * Height;
    }

    public override int CornerCount()
    {
        return 4;
    }
}

public class Triangle(float Base, float Height) : Shape_Base
{
    public override float Area()
    {
        return (float)(0.5 * Base * Height);
    }

    public override int CornerCount()
    {
        return 3;
    }
}

public class Circle(float Radius) : Shape_Base
{
    public override float Area()
    {
        return (float)(Math.PI * Radius * Radius);
    }

    public override int CornerCount()
    {
        return 0;
    }
}

public enum ShapeType
{
    Square = 0,
    Rectangle = 1,
    Triangle = 2,
    Circle = 3
}

public class ShapeUnion
{
    public ShapeType Type { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}
