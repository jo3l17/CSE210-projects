public class Budget
{
  protected string _name;
  protected decimal _limit;
  protected List<Transaction> _transactions;
  protected string _id;

  public Budget(string name, decimal limit, string id = "")
  {
    _name = name;
    _limit = limit;
    _transactions = new List<Transaction>();
    _id = id == "" ? Guid.NewGuid().ToString() : id;
  }
  public string GetId()
  {
    return _id;
  }
  public virtual string GetStringRepresentation()
  {
    return $"{this.GetType().Name}:{_id},{_name},{_limit}";
  }
  public decimal GetPercentageCompleted()
  {
    decimal completed = 0;
    foreach (Transaction transaction in _transactions)
    {
      completed += transaction.GetAmount();
    }
    return completed / _limit * 100;
  }
  public decimal GetLimit()
  {
    return _limit;
  }
  public virtual string GetTitle()
  {
    return "";
  }
  public string Show()
  {
    return $"{_name} - {_limit} - {GetPercentageCompleted()}% completed";
  }
  public void SetTransactions(List<Transaction> transactions)
  {
    _transactions = transactions;
  }
  public void AddTransaction(Transaction transaction)
  {
    _transactions.Add(transaction);
  }
  public List<Transaction> GetTransactions()
  {
    return _transactions;
  }
  public void GenerateReport()
  {
    Console.WriteLine($"Budget: {GetTitle()}");
    Console.WriteLine($"Limit: {_limit}");
    Console.WriteLine($"Percentage completed: {GetPercentageCompleted()}");
    Console.Write("[");
    int progress = (int)GetPercentageCompleted() / 10;
    for (int i = 0; i < 10; i++)
    {
      if (i < progress)
      {
        Console.Write("=");
      }
      else
      {
        Console.Write("-");
      }
    }
    Console.Write("]\n");
    Console.WriteLine("Transactions:");
    foreach (Transaction transaction in _transactions)
    {
      Console.WriteLine(transaction.Show());
    }
    Console.WriteLine();
    Console.WriteLine("Do you want to export this budget to a file? (y/n)");
    string answer = Console.ReadLine();
    if (answer == "y")
    {
      Console.WriteLine("What is the name of the file?");
      string fileName = Console.ReadLine();
      FileManager.WriteReportToFile(fileName, this);
    }
  }
}