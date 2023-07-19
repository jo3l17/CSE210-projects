public class FileManager
{
  public static List<User> GetAllUsers()
  {
    List<User> users = new();
    string[] lines = File.ReadAllLines("users.txt");
    foreach (string line in lines)
    {
      string[] data = line.Split(',');
      User user = new(data[0], data[1], data[2], data[3]);
      users.Add(user);
    }
    return users;
  }

  public static void SaveAllUsers(List<User> users)
  {
    string[] lines = new string[users.Count];
    for (int i = 0; i < users.Count; i++)
    {
      lines[i] = users[i].GetStringRepresentation();
    }
    File.WriteAllLines("users.txt", lines);
  }
  public static List<Budget> GetUserBudgets(User user)
  {
    List<Budget> budgets = new();
    if (!File.Exists($"Budgets/{user.GetId()}.txt"))
    {
      File.Create($"Budgets/{user.GetId()}.txt").Close();
    }
    string[] lines = File.ReadAllLines($"Budgets/{user.GetId()}.txt");
    foreach (string line in lines)
    {
      string budgetType = line.Split(':')[0];
      string[] budgetDetails = line.Split(':')[1].Split(',');
      Budget budgetToAdd;
      if (budgetType == "MonthlyBudget")
      {
        budgetToAdd = new MonthlyBudget(
          int.Parse(budgetDetails[3]),
          budgetDetails[1],
          decimal.Parse(budgetDetails[2]),
          budgetDetails[0]
          );
      }
      else
      {
        budgetToAdd = new YearlyBudget(
                  int.Parse(budgetDetails[3]),
                  budgetDetails[1],
                  decimal.Parse(budgetDetails[2]),
                  budgetDetails[0]
                  );
      }
      budgets.Add(budgetToAdd);
    }
    foreach (Budget budget in budgets)
    {
      budget.SetTransactions(GetBudgetTransactions(budget));
    }
    return budgets;
  }
  public static void SaveUserBudgets(User user)
  {
    List<Budget> budgets = user.GetBudgets();
    string[] lines = new string[budgets.Count];
    for (int i = 0; i < budgets.Count; i++)
    {
      lines[i] = budgets[i].GetStringRepresentation();
    }
    File.WriteAllLines($"Budgets/{user.GetId()}.txt", lines);
  }
  public static void WriteBudgetTransactions(Budget budget)
  {
    List<Transaction> transactions = budget.GetTransactions();
    string[] lines = new string[transactions.Count];
    for (int i = 0; i < transactions.Count; i++)
    {
      lines[i] = transactions[i].GetStringRepresentation();
    }
    File.WriteAllLines($"Transactions/{budget.GetId()}.txt", lines);
  }
  public static List<Transaction> GetBudgetTransactions(Budget budget)
  {
    List<Transaction> transactions = new();
    if (!File.Exists($"Transactions/{budget.GetId()}.txt"))
    {
      File.Create($"Transactions/{budget.GetId()}.txt").Close();
    }
    string[] lines = File.ReadAllLines($"Transactions/{budget.GetId()}.txt");
    foreach (string line in lines)
    {
      Transaction transaction = CreateTransaction(line);
      transactions.Add(transaction);
    }
    return transactions;
  }
  public static Transaction CreateTransaction(string transactionString)
  {
    string transactionType = transactionString.Split(':')[0];
    string[] transactionDetails = transactionString.Split(':')[1].Split(',');
    Transaction transactionToReturn;
    if (transactionType == "ExpenseTransaction")
    {
      transactionToReturn = new ExpenseTransaction(transactionDetails[3], decimal.Parse(transactionDetails[0]), DateTime.Parse(transactionDetails[1]), transactionDetails[2]);
    }
    else
    {
      transactionToReturn = new IncomeTransaction(transactionDetails[3], decimal.Parse(transactionDetails[0]), DateTime.Parse(transactionDetails[1]), transactionDetails[2]);
    }
    return transactionToReturn;
  }
  public static void WriteReportToFile(string filename, Budget budget)
  {
    string[] lines = new string[6 + budget.GetTransactions().Count];
    lines[0] = $"Budget: {budget.GetTitle()}";
    lines[1] = $"Limit: {budget.GetLimit()}";
    lines[2] = $"Percentage completed: {budget.GetPercentageCompleted()}";
    // progress bar
    string thirdLine = "[";
    for (int i = 0; i < 10; i++)
    {
      if (i < budget.GetPercentageCompleted() / 10)
      {
        thirdLine += "=";
      }
      else
      {
        thirdLine += "-";
      }
    }
    thirdLine += "]";
    lines[3] = thirdLine;
    lines[4] = "Transactions:";
    for (int i = 0; i < budget.GetTransactions().Count; i++)
    {
      lines[i + 5] = budget.GetTransactions()[i].Show();
    }
    lines[lines.Length - 1] = "-------------------------";
    File.WriteAllLines($"Reports/{filename}.txt", lines);
    Console.WriteLine($"Report saved to Reports/{filename}.txt");
    Console.WriteLine("returning to menu...");
    ShowSpinner(3);
  }
  public static void ShowSpinner(int seconds)
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
}