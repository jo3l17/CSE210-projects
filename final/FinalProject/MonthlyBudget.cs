public class MonthlyBudget : Budget
{
  private int _month;
  private List<string> _months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "November", "December" };
  public MonthlyBudget(int month, string name, decimal limit, string id = "") : base(name, limit, id)
  {
    _month = month;
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_month}";
  }
  public override string GetTitle()
  {
    return $"{_name} for {_months[_month - 1]}";
  }
}