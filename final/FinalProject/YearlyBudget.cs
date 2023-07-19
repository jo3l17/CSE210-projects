public class YearlyBudget : Budget
{
  private int _year;
  public YearlyBudget(int year, string name, decimal limit, string id = "") : base(name, limit, id)
  {
    _year = year;
  }
  public override string GetStringRepresentation()
  {
    return $"{base.GetStringRepresentation()},{_year}";
  }
  public override string GetTitle()
  {
    return $"{_name} for Year {_year}";
  }
}