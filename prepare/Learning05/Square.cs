public class Square : Shape
{
  private double side;

  public Square(double side, string color) : base(color)
  {
    this.side = side;
  }

  public override double GetArea()
  {
    return side * side;
  }
}