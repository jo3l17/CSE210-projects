public class Activity
{
  protected string _name;
  protected string _description;
  protected int _duration;

  public Activity()
  {
    _name = "Default Activity";
    _description = "Default Description";
    _duration = 0;
  }
  public Activity(string name, string description)
  {
    _name = name;
    _description = description;
  }

  public void DisplayStartingMessage()
  {
    Console.Clear();
    Console.WriteLine($"Welcome to the {_name} Activity\n");
    Console.WriteLine($"{_description}\n");
    SetDuration();
    Console.Clear();
  }

  public void SetDuration()
  {
    Console.Write("How long, in seconds, would you like for your session? ");
    _duration = int.Parse(Console.ReadLine());
  }
  protected int GetDuration()
  {
    return _duration;
  }

  public void DisplayEndingMessage()
  {
    Console.WriteLine("Well done!\n");
    Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity");
    ShowSpinner(3);
  }

  public void ShowSpinner(int seconds)
  {
    int delay = 200;
    int spinnerIndex = 0;
    int iterations = seconds * 1000 / delay;

    for (int i = 0; i < iterations; i++)
    {
      switch (spinnerIndex % 4)
      {
        case 0:
          Console.Write("/");
          break;
        case 1:
          Console.Write("-");
          break;
        case 2:
          Console.Write("\\");
          break;
        case 3:
          Console.Write("|");
          break;
      }

      spinnerIndex++;
      Thread.Sleep(delay);
      Console.Write("\b \b");
    }

    Console.Write(" ");
    Console.WriteLine();
  }
  public void LoadingBar(int seconds)
  {
    int delay = 200;
    int iterations = seconds * 1000 / delay;
    int percent = 0;
    int percentIncrement = 100 / iterations;
    for (int i = 0; i <= iterations; i++)
    {
      Console.Write("[");
      for (int j = 0; j < percent; j++)
      {
        Console.Write("=");
      }
      if (percent < 100)
      {
        Console.Write(">");
      }
      for (int j = percent; j < 99; j++)
      {
        Console.Write(" ");
      }
      Console.Write("]");
      percent += percentIncrement;
      Thread.Sleep(delay);
      if (percent <= 100)
      {
        for (int j = 0; j < 103; j++)
        {
          Console.Write("\b \b");
        }
      }
    }
    Console.WriteLine();
  }
  public void DisplayMessage(string message, int pauseSeconds)
  {
    Console.Write(message);
    ShowCountdown(pauseSeconds);
  }

  public void ShowCountdown(int seconds)
  {
    for (int i = seconds; i > 0; i--)
    {
      Console.Write($"{i}");
      Thread.Sleep(1000);
      Console.Write("\b \b");
    }
    Console.WriteLine();
  }
}