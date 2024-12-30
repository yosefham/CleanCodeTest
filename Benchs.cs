using BenchmarkDotNet.Attributes;

namespace CleanCodeTest;

[MemoryDiagnoser]
public class Benchs
{

    private readonly Shape_Base[] Shapes = Enumerable.Range(1, 1_000_000).Select(x =>
    {
        var rand = Random.Shared.Next() % 4;
        Shape_Base shape = rand switch
        {
            0 => new Circle(Random.Shared.Next(100)),
            1 => new Square(Random.Shared.Next(100)),
            2 => new Rectangle(Random.Shared.Next(100), Random.Shared.Next(100)),
            3 => new Triangle(Random.Shared.Next(100), Random.Shared.Next(100)),
            _ => throw new IndexOutOfRangeException()
        };
        return shape;
    }).ToArray();

    [Benchmark(Baseline = true)]
    public double TotalAreaVTBL()
    {
        double accum = 0.0;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex++)
        {
            accum += Shapes[shapeIndex].Area();
        }
        return accum;
    }

    [Benchmark]
    public double TotalAreaVTBL4()
    {
        double accum0 = 0.0;
        double accum1 = 0.0;
        double accum2 = 0.0;
        double accum3 = 0.0;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex += 4)
        {
            accum0 += Shapes[shapeIndex].Area();
            accum1 += Shapes[shapeIndex + 1].Area();
            accum2 += Shapes[shapeIndex + 3].Area();
            accum3 += Shapes[shapeIndex + 3].Area();
        }
        double accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }
}
