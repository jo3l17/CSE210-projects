public abstract class Goal
{
  protected string _shortName;
  protected string _description;
  protected int _points;

  public Goal(string name, string description, int points)
  {
    _shortName = name;
    _description = description;
    _points = points;
  }
  public int GetPoints()
  {
    return _points;
  }
  public virtual string GetDetailsString()
  {
    return $"{_shortName} ({_description})";
  }
  public virtual void RecordEvent()
  {
    Console.WriteLine($"Congratulations! You have earned {_points} points!");
  }
  public abstract bool IsComplete();
  public virtual string GetStringRepresentation()
  {
    return $"{GetType().Name}:{_shortName},{_description},{_points}";
  }
}