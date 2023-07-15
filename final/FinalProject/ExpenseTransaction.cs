public class ExpenseTransaction : Transaction
{
  private string _merchant;
  public ExpenseTransaction(decimal amount, DateTime date, string description) : base(amount, date, description)
  {
  }

  public override void ProcessTransaction()
  {
    base.ProcessTransaction();
  }

}