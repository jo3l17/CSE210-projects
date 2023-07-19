public class User
{
  private string _name;
  private string _email;
  private string _password;
  private List<Budget> _budgets;
  private List<Transaction> _transactions;
  private string _id;
  public User(string name, string email, string password, string id = "")
  {
    _name = name;
    _email = email;
    _password = password;
    _budgets = new List<Budget>();
    _budgets = new List<Budget>();
    _transactions = new List<Transaction>();
    _id = id == "" ? Guid.NewGuid().ToString() : id;
  }
  public static User Login(List<User> users, string email, string password)
  {
    foreach (User user in users)
    {
      if (user._email == email && user._password == password)
      {
        return user;
      }
    }
    return null;
  }
  public string GetName()
  {
    return _name;
  }
  public static string GetPassword()
  {
    Console.Write("Enter password: ");
    string pass = string.Empty;
    ConsoleKey key;
    do
    {
      var keyInfo = Console.ReadKey(intercept: true);
      key = keyInfo.Key;
      if (key == ConsoleKey.Backspace && pass.Length > 0)
      {
        Console.Write("\b \b");
        pass = pass[0..^1];
      }
      else if (!char.IsControl(keyInfo.KeyChar))
      {
        Console.Write("*");
        pass += keyInfo.KeyChar;
      }
    } while (key != ConsoleKey.Enter);
    Console.WriteLine();
    return pass;
  }
  public string GetStringRepresentation()
  {
    return $"{_name},{_email},{_password},{_id}";
  }
  public string GetId()
  {
    return _id;
  }
  public void AddBudget(Budget budget)
  {
    _budgets.Add(budget);
  }
  public void SetBudgets(List<Budget> budgets)
  {
    _budgets = budgets;
  }
  public List<Budget> GetBudgets()
  {
    return _budgets;
  }
}