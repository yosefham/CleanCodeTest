namespace CleanCodeTest;

public abstract class Shape_Base
{
    public abstract double Area();
}

public class Square(double side) : Shape_Base
{

    public override double Area()
    {
        return side * side;
    }
}

public class Rectangle(double Width, double Height) : Shape_Base
{
    public override double Area()
    {
        return Width * Height;
    }
}

public class Triangle(double Base, double Height) : Shape_Base
{
    public override double Area()
    {
        return (double)(0.5 * Base * Height);
    }
}

public class Circle(double Radius) : Shape_Base
{
    public override double Area()
    {
        return (double)(Math.PI * Radius * Radius);
    }
}
