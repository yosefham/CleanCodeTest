using BenchmarkDotNet.Attributes;

namespace CleanCodeTest;

// [MemoryDiagnoser]
public class Benchs
{
    private const int MAX_SHAPES = 5_000_000;
    private ShapeUnion[] ShapeUnions = [];
    private Shape_Base[] Shapes = [];

    [GlobalSetup]
    public void SetUpData()
    {
        Shapes = Enumerable.Range(1, MAX_SHAPES).Select(x =>
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

        ShapeUnions = Enumerable.Range(1, MAX_SHAPES).Select(x =>
            new ShapeUnion
            {
                Type = (ShapeType)(Random.Shared.Next() % 4),
                Width = Random.Shared.Next(100),
                Height = Random.Shared.Next(100)
            }
        ).ToArray();

    }

    [Benchmark(Baseline = true)]
    public float TotalAreaVTBL()
    {
        float accum = 0.0f;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex++)
        {
            accum += Shapes[shapeIndex].Area();
        }
        return accum;
    }

    [Benchmark]
    public float TotalAreaVTBL4()
    {
        float accum0 = 0.0f;
        float accum1 = 0.0f;
        float accum2 = 0.0f;
        float accum3 = 0.0f;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex += 4)
        {
            accum0 += Shapes[shapeIndex].Area();
            accum1 += Shapes[shapeIndex + 1].Area();
            accum2 += Shapes[shapeIndex + 3].Area();
            accum3 += Shapes[shapeIndex + 3].Area();
        }
        float accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }





    [Benchmark]
    public float TotalAreaSwitch()
    {
        float accum = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex++)
        {
            accum += GetAreaSwitch(ShapeUnions[shapeIndex]);
        }
        return accum;
    }

    [Benchmark]
    public float TotalAreaSwitch4()
    {

        float accum0 = 0.0f;
        float accum1 = 0.0f;
        float accum2 = 0.0f;
        float accum3 = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex += 4)
        {
            accum0 += GetAreaSwitch(ShapeUnions[shapeIndex]);
            accum1 += GetAreaSwitch(ShapeUnions[shapeIndex + 1]);
            accum2 += GetAreaSwitch(ShapeUnions[shapeIndex + 3]);
            accum3 += GetAreaSwitch(ShapeUnions[shapeIndex + 3]);
        }
        float accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }

    public static float GetAreaSwitch(ShapeUnion shape)
    {
        float result = shape.Type switch
        {
            ShapeType.Square => shape.Width * shape.Width,
            ShapeType.Rectangle => shape.Width * shape.Height,
            ShapeType.Triangle => 0.5f * shape.Width * shape.Height,
            ShapeType.Circle => (float)Math.PI * shape.Width * shape.Width,
            _ => 0
        };
        return result;
    }






    [Benchmark]
    public float TotalAreaTable()
    {
        float accum = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex++)
        {
            accum += GetAreaTable(ShapeUnions[shapeIndex]);
        }
        return accum;
    }

    [Benchmark]
    public float TotalAreaTable4()
    {

        float accum0 = 0.0f;
        float accum1 = 0.0f;
        float accum2 = 0.0f;
        float accum3 = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex += 4)
        {
            accum0 += GetAreaTable(ShapeUnions[shapeIndex]);
            accum1 += GetAreaTable(ShapeUnions[shapeIndex + 1]);
            accum2 += GetAreaTable(ShapeUnions[shapeIndex + 3]);
            accum3 += GetAreaTable(ShapeUnions[shapeIndex + 3]);
        }
        float accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }


    private static readonly float[] CTable = [1.0f, 1.0f, 0.5f, (float)Math.PI];
    public static float GetAreaTable(ShapeUnion shape)
    {
        return CTable[(int)shape.Type] * shape.Width * shape.Height;
    }




    [Benchmark]
    public float TotalCornerAreaVTBL()
    {
        float accum = 0.0f;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex++)
        {
            accum += 1.0f / (1.0f + Shapes[shapeIndex].CornerCount()) * Shapes[shapeIndex].Area();
        }
        return accum;
    }

    [Benchmark]
    public float TotalCornerAreaVTBL4()
    {
        float accum0 = 0.0f;
        float accum1 = 0.0f;
        float accum2 = 0.0f;
        float accum3 = 0.0f;
        for (int shapeIndex = 0; shapeIndex < Shapes.Length; shapeIndex += 4)
        {
            accum0 += 1.0f / (1.0f + Shapes[shapeIndex].CornerCount()) * Shapes[shapeIndex].Area();
            accum1 += 1.0f / (1.0f + Shapes[shapeIndex + 1].CornerCount()) * Shapes[shapeIndex].Area();
            accum2 += 1.0f / (1.0f + Shapes[shapeIndex + 2].CornerCount()) * Shapes[shapeIndex].Area();
            accum3 += 1.0f / (1.0f + Shapes[shapeIndex + 3].CornerCount()) * Shapes[shapeIndex].Area();
        }
        float accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }




    [Benchmark]
    public float TotalCornerAreaTable()
    {
        float accum = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex++)
        {
            accum += GetCornerAreaTable(ShapeUnions[shapeIndex]);
        }
        return accum;
    }

    [Benchmark]
    public float TotalCornerAreaTable4()
    {

        float accum0 = 0.0f;
        float accum1 = 0.0f;
        float accum2 = 0.0f;
        float accum3 = 0.0f;
        for (int shapeIndex = 0; shapeIndex < ShapeUnions.Length; shapeIndex += 4)
        {
            accum0 += GetCornerAreaTable(ShapeUnions[shapeIndex]);
            accum1 += GetCornerAreaTable(ShapeUnions[shapeIndex + 1]);
            accum2 += GetCornerAreaTable(ShapeUnions[shapeIndex + 3]);
            accum3 += GetCornerAreaTable(ShapeUnions[shapeIndex + 3]);
        }
        float accum = accum0 + accum1 + accum2 + accum3;
        return accum;
    }



    private static readonly float[] CornerCount = [1.0f / (1.0f + 4.0f), 1.0f / (1.0f + 4.0f), 0.5f / (1.0f + 3.0f), (float)Math.PI];
    public static float GetCornerAreaTable(ShapeUnion shape)
    {
        return CornerCount[(int)shape.Type] * shape.Width * shape.Height;

    }

}
