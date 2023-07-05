using System;

class Program
{
  static void Main(string[] args)
  {
    List<Shape> shapes = new List<Shape>();
    shapes.Add(new Square(5, "green"));
    shapes.Add(new Circle(5, "blue"));
    shapes.Add(new Rectangle(4, 5, "red"));
    foreach (Shape shape in shapes)
    {
      string color = shape.GetColor();
      double area = shape.GetArea();

      Console.WriteLine($"The {color} {shape.GetType().Name} has an area of {area}");
    }
  }
}