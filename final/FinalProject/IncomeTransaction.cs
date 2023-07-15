public class IncomeTransaction : Transaction
{
  private string _source;
  public IncomeTransaction(decimal amount, DateTime date, string description) : base(amount, date, description)
  {
  }

  public override void ProcessTransaction()
  {
    base.ProcessTransaction();
  }

}