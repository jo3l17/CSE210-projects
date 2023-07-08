public class CheckListGoal : Goal
{
  private int _amountCompleted;
  private int _target;
  private int _bonus;
  public CheckListGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
  {
    _amountCompleted = 0;
    _target = target;
    _bonus = bonus;
  }
  public CheckListGoal(string name, string description, int points, int target, int bonus, int amountCompleted) : base(name, description, points)
  {
    _amountCompleted = amountCompleted;
    _target = target;
    _bonus = bonus;
  }
  public override bool IsComplete()
  {
    return _amountCompleted == _target;
  }
  public override void RecordEvent()
  {
    if (_amountCompleted < _target)
    {
      base.RecordEvent();
      _amountCompleted++;
      if (IsComplete())
      {
        AnimateStarsWithText($"Congratulations for completing your Goal: {_shortName}");
      }
    }
    else
    {
      Console.WriteLine($"Congratulations! You have earned {_points + _bonus} points!");
    }
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_bonus},{_target},{_amountCompleted}";
  }
  public override string GetDetailsString()
  {
    return $"{base.GetDetailsString()} -- Currently completed: {_amountCompleted}/{_target}";
  }
  void AnimateStarsWithText(string text)
  {
    Console.CursorVisible = false;
    Console.Clear();
    int centerX = Console.WindowWidth / 2;
    int centerY = Console.WindowHeight / 2;

    int startX = centerX - (text.Length / 2);
    int startY = centerY;

    Console.SetCursorPosition(startX, startY);
    Console.Write(text);

    Random random = new Random();
    int maxStars = (int)(Console.WindowWidth * Console.WindowHeight / 100);

    for (int i = 0; i < maxStars; i++)
    {
      int x = random.Next(Console.WindowWidth);
      int y = random.Next(Console.WindowHeight);

      if (!IsPositionOccupied(x, y, startX, startY, text.Length))
      {
        Console.SetCursorPosition(x, y);
        Console.Write("*");
      }
      else
      {
        i--;
      }

      Thread.Sleep(100);
    }

    Console.CursorVisible = true;
    Console.Clear();
  }

  bool IsPositionOccupied(int x, int y, int startX, int startY, int textLength)
  {
    if (x >= startX && x < startX + textLength && y == startY)
    {
      return true;
    }
    return false;
  }
}