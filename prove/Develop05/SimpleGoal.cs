public class SimpleGoal : Goal
{
  private bool _isComplete = false;
  public SimpleGoal(string name, string description, int points) : base(name, description, points) { }
  public SimpleGoal(string name, string description, int points, bool isComplete) : base(name, description, points)
  {
    _isComplete = isComplete;
  }
  public override bool IsComplete()
  {
    return _isComplete;
  }
  public override void RecordEvent()
  {
    base.RecordEvent();
    _isComplete = true;
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_isComplete}";
  }
}