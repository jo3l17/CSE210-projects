public class GoalManager
{
  private List<Goal> _goals;
  private int _score;
  public GoalManager() { }
  public void Start()
  {
    DisplayPlayerInfo();
    _goals = new List<Goal>();
    int option = DisplayMenu();
    while (option != 6)
    {
      switch (option)
      {
        case 1:
          CreateGoal();
          break;
        case 2:
          ListGoalDetails();
          break;
        case 3:
          SaveGoals();
          break;
        case 4:
          LoadGoals();
          break;
        case 5:
          RecordEvent();
          break;
      }
      DisplayPlayerInfo();
      option = DisplayMenu();
    }
  }
  public int TypeOfGoal()
  {
    Console.WriteLine("The types of Goals are:");
    ListGoalNames();
    Console.Write("What type of goal would you like to create? ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 4)
    {
      Console.WriteLine("Invalid option");
      Console.Write("What type of goal would you like to create? ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
  public int DisplayMenu()
  {
    Console.WriteLine("Menu options:");
    Console.WriteLine("1. Create New Goal");
    Console.WriteLine("2. List Goals");
    Console.WriteLine("3. Save Goals");
    Console.WriteLine("4. Load Goals");
    Console.WriteLine("5. Record Event");
    Console.WriteLine("6. Quit");
    Console.Write("Select a choice from the menu: ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 6)
    {
      Console.WriteLine("Invalid option");
      Console.Write("Select a choice from the menu: ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
  public void DisplayPlayerInfo()
  {
    Console.WriteLine($"You have {_score} points");
    Console.WriteLine();
  }
  public void ListGoalNames()
  {
    Console.WriteLine("1. Simple Goal");
    Console.WriteLine("2. Eternal Goal");
    Console.WriteLine("3. CheckList Goal");
    Console.WriteLine("4. Negative Goal");
  }
  public void ListGoalDetails()
  {
    if (_goals.Count() == 0)
    {
      Console.WriteLine("You don't have goals yet");
      return;
    }
    Console.WriteLine();
    int count = 1;
    foreach (Goal goal in _goals)
    {
      string goalDetails = $"{count}. [";
      goalDetails += goal.IsComplete() ? "X" : " ";
      goalDetails += $"] {goal.GetDetailsString()}";
      Console.WriteLine(goalDetails);
      count++;
    }
    Console.WriteLine();
  }
  public void CreateGoal()
  {
    int typeOfGoal = TypeOfGoal();
    Goal goalToAdd;
    Console.Write("What is the name of your goal? ");
    string goalName = Console.ReadLine();
    Console.Write("What is a short description of it? ");
    string goalDescription = Console.ReadLine();
    Console.Write("What is the amount of points associated to this goal? ");
    int goalPoints = int.Parse(Console.ReadLine());
    switch (typeOfGoal)
    {
      case 1:
        goalToAdd = new SimpleGoal(goalName, goalDescription, goalPoints);
        break;
      case 2:
        goalToAdd = new EternalGoal(goalName, goalDescription, goalPoints);
        break;
      case 3:
        Console.Write("How many times does this goal need to be accomplished for a bonus? ");
        int goalTarget = int.Parse(Console.ReadLine());
        Console.Write($"What is the bonus for acomplishing it {goalTarget} times? ");
        int goalBonus = int.Parse(Console.ReadLine());
        goalToAdd = new CheckListGoal(goalName, goalDescription, goalPoints, goalTarget, goalBonus);
        break;
      default:
        Console.WriteLine("Remember that this is a negative goal so it will reduce your points");
        goalToAdd = new NegativeGoal(goalName, goalDescription, -goalPoints);
        break;
    }
    _goals.Add(goalToAdd);
  }
  public void RecordEvent()
  {
    ListGoalDetails();
    Console.Write("What goal did you accomplish? ");
    int selectedIndex = int.Parse(Console.ReadLine());
    while (selectedIndex > _goals.Count() || selectedIndex < 1)
    {
      Console.WriteLine("Invalid option");
      Console.Write("What goal did you accomplish? ");
      selectedIndex = int.Parse(Console.ReadLine());
    }
    Goal selectedGoal = _goals[selectedIndex - 1];
    selectedGoal.RecordEvent();
    _score += selectedGoal.GetPoints();
    Console.WriteLine($"Your new score is {_score}");
    Console.WriteLine();
  }
  public void SaveGoals()
  {
    Console.Write("What is the filename for the goal file? ");
    string fileName = Console.ReadLine();
    using (StreamWriter outputFile = new StreamWriter(fileName))
    {
      outputFile.WriteLine(_score);
      foreach (Goal goal in _goals)
      {
        outputFile.WriteLine(goal.GetStringRepresentation());
      }
    }
  }
  public void LoadGoals()
  {
    Console.Write("What is the filename of the goal file? ");
    string fileName = Console.ReadLine();
    string[] lines = File.ReadAllLines(fileName);
    int entriesNumber = lines.Count();
    if (entriesNumber <= 0)
    {
      Console.WriteLine("The selected file doesn't have any goal");
      return;
    }
    _score = int.Parse(lines[0]);
    for (int i = 1; i < entriesNumber; i++)
    {
      Goal goal = CreateGoal(lines[i]);
      _goals.Add(goal);
    }
  }
  public Goal CreateGoal(string goalString)
  {
    string goalName = goalString.Split(":")[0];
    string[] goalDetails = goalString.Split(":")[1].Split(",");
    Goal goalToReturn;
    if (goalName == "SimpleGoal")
    {
      goalToReturn = new SimpleGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]), bool.Parse(goalDetails[3]));
    }
    else if (goalName == "EternalGoal")
    {
      goalToReturn = new EternalGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]));
    }
    else if (goalName == "NegativeGoal")
    {
      goalToReturn = new NegativeGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]));
    }
    else
    {
      goalToReturn = new CheckListGoal(
        goalDetails[0],
        goalDetails[1],
        int.Parse(goalDetails[2]),
        int.Parse(goalDetails[4]),
        int.Parse(goalDetails[3]),
        int.Parse(goalDetails[5])
      );
    }
    return goalToReturn;
  }
}