public class IncomeTransaction : Transaction
{
  private string _source;
  public IncomeTransaction(string source, decimal amount, DateTime date, string description) : base(amount, date, description)
  {
    _source = source;
  }
  public override string Show()
  {
    return $"Income: {_source} - {base.Show()}";
  }
  public override decimal GetAmount()
  {
    return -base.GetAmount();
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_source}";
  }
}