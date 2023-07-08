public class NegativeGoal : Goal
{
  public NegativeGoal(string name, string description, int points) : base(name, description, points) { }
  public override bool IsComplete()
  {
    return false;
  }
  public override void RecordEvent()
  {
    Console.WriteLine();
    Console.WriteLine($"You have lost {Math.Abs(_points)} points :(");
  }
}