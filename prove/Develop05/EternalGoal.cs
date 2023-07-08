public class EternalGoal : Goal
{
  public EternalGoal(string name, string description, int points) : base(name, description, points) { }
  public override bool IsComplete()
  {
    return false;
  }
  public override void RecordEvent()
  {
    base.RecordEvent();
  }
}