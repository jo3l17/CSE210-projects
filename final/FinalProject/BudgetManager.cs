public class BudgetManager
{
  List<User> users;
  User user;
  List<Budget> budgets;
  Budget budget;
  bool loggedIn;
  public BudgetManager()
  {
    users = FileManager.GetAllUsers();
  }
  public void Start()
  {
    int option = DisplayMenu();
    while ((option != 3 && !loggedIn) || (option != 6 && loggedIn))
    {
      if (loggedIn)
      {
        switch (option)
        {
          case 1:
            CreateNewBudget();
            break;
          case 2:
            SelectBudget();
            AddTransaction();
            break;
          case 3:
            ShowBudgetTransactions();
            break;
          case 4:
            GenerateReport();
            break;
          case 5:
            Logout();
            break;
          case 6:
            Console.WriteLine("Exiting...");
            break;
        }
        FileManager.SaveUserBudgets(user);
      }
      else
      {
        switch (option)
        {
          case 1:
            Login();
            break;
          case 2:
            Register();
            break;
          case 3:
            Console.WriteLine("Exiting...");
            break;
        }
      }
      option = DisplayMenu();
    }
  }
  public void ShowBudgetTransactions()
  {
    if (budgets.Count == 0)
    {
      Console.WriteLine("No budgets found");
      return;
    }
    SelectBudget("Select a budget to view transactions: ");
    if (budget.GetTransactions().Count == 0)
    {
      Console.WriteLine("No transactions found");
      return;
    }
    int option;
    do
    {
      Console.Clear();
      foreach (Transaction transaction in budget.GetTransactions())
      {
        Console.WriteLine(transaction.Show());
      }
      Console.Write("Press 0 to return to main menu, or any key to select another budget: ");
      string optionString = Console.ReadLine();
      option = int.Parse(optionString == "" ? "1" : optionString);
      if(option != 0)
      {
        SelectBudget("Select a budget to view transactions: ");
      }
    } while (option != 0);
  }
  public void CreateNewBudget()
  {
    int type = BudgetType();
    string name = InputValidator.NoCommas("Enter budget name: ");
    Console.Write("Enter limit: ");
    decimal limit = decimal.Parse(Console.ReadLine());
    Budget budgetToCreate;
    if (type == 1)
    {
      Console.Write("Enter the number of the month: ");
      int month = int.Parse(Console.ReadLine());
      budgetToCreate = new MonthlyBudget(month, name, limit);
    }
    else
    {
      Console.Write("Enter the year: ");
      int year = int.Parse(Console.ReadLine());
      budgetToCreate = new YearlyBudget(year, name, limit);
    }
    budgets.Add(budgetToCreate);
    user.SetBudgets(budgets);
  }

  public static int BudgetType()
  {
    Console.WriteLine("Select budget type:");
    Console.WriteLine("1. Monthly");
    Console.WriteLine("2. Yearly");
    Console.Write("Enter option: ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 2)
    {
      Console.Write("Enter option: ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
  public void SelectBudget(string customMessage = "Select budget:")
  {
    if (budgets.Count == 0)
    {
      Console.WriteLine("No budgets found");
      return;
    }
    Console.WriteLine(customMessage);
    int i = ShowBudgets();
    Console.Write("Enter option: ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > i)
    {
      Console.Write("Enter option: ");
      option = int.Parse(Console.ReadLine());
    }
    budget = budgets[option - 1];
  }

  public void AddTransaction()
  {
    Console.WriteLine("Select transaction type:");
    Console.WriteLine("1. Expense");
    Console.WriteLine("2. Income");
    Console.Write("Enter option: ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 2)
    {
      Console.Write("Enter option: ");
      option = int.Parse(Console.ReadLine());
    }
    Transaction transaction;
    Console.Write("Enter amount: ");
    decimal amount = decimal.Parse(Console.ReadLine());
    Console.Write("Enter date (leave blank for today): ");
    string dateString = Console.ReadLine();
    DateTime date = DateTime.Parse(dateString == "" ? DateTime.Now.ToString() : dateString);
    string description = InputValidator.NoCommas("Enter description: ");
    if (option == 1)
    {
      string merchant = InputValidator.NoCommas("Enter expense merchant: ");
      transaction = new ExpenseTransaction(merchant, amount, date, description);
    }
    else
    {
      string source = InputValidator.NoCommas("Enter income source: ");
      transaction = new IncomeTransaction(source, amount, date, description);
    }
    budget.AddTransaction(transaction);
    FileManager.WriteBudgetTransactions(budget);
  }

  public int ShowBudgets()
  {
    int i = 1;
    foreach (Budget budget in budgets)
    {
      Console.WriteLine(i + ". " + budget.Show());
      i++;
    }
    return i;
  }
  public void GenerateReport()
  {
    SelectBudget("Select a budget to generate report: ");
    budget.GenerateReport();
  }
  public void GetUserBudgets()
  {
    budgets = FileManager.GetUserBudgets(user);
    user.SetBudgets(budgets);
  }
  public void Logout()
  {
    loggedIn = false;
    Console.WriteLine("Logged out");
  }
  public void Register()
  {
    string name = InputValidator.NoCommas("Enter name: ");
    string email = InputValidator.NoCommas("Enter email: ");
    string password = User.GetPassword();
    User regUser = new(name, email, password);
    users.Add(regUser);
    user = regUser;
    loggedIn = true;
    Console.WriteLine("Registration successful");
    FileManager.SaveAllUsers(users);
    budgets = new List<Budget>();
  }
  public void Login()
  {
    string email = InputValidator.NoCommas("Enter email: ");
    string password = User.GetPassword();
    user = User.Login(users, email, password);
    if (user != null)
    {
      Console.WriteLine("Login successful");
      loggedIn = true;
      GetUserBudgets();
    }
    else
    {
      Console.WriteLine("Login failed");
    }
  }
  public int DisplayMenu()
  {
    Console.Clear();
    Console.WriteLine();
    if (loggedIn)
    {
      Console.WriteLine("Logged in as " + user.GetName());
      Console.WriteLine("Menu options:");
      Console.WriteLine("1. Create New Budget");
      Console.WriteLine("2. Add Transaction");
      Console.WriteLine("3. Show Budgets");
      Console.WriteLine("4. Generate Report");
      Console.WriteLine("5. Logout.");
      Console.WriteLine("6. Exit");
    }
    else
    {
      Console.WriteLine("Menu options:");
      Console.WriteLine("1. Login");
      Console.WriteLine("2. Register");
      Console.WriteLine("3. Exit");
    }
    Console.Write("Select a choice from the menu: ");
    int option = int.Parse(Console.ReadLine());
    while ((loggedIn && (option < 1 || option > 6)) || (!loggedIn && (option < 1 || option > 3)))
    {
      Console.WriteLine("Invalid option");
      Console.Write("Select a choice from the menu: ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
}