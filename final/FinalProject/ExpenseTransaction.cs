public class ExpenseTransaction : Transaction
{
  private string _merchant;
  public ExpenseTransaction(string merchant, decimal amount, DateTime date, string description) : base(amount, date, description)
  {
    _merchant = merchant;
  }
  public override string Show()
  {
    return $"Expense: {_merchant} - {base.Show()}";
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_merchant}";
  }
}