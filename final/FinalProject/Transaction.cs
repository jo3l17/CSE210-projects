public class Transaction
{
  protected decimal _amount;
  protected DateTime _date;
  protected string _description;

  public Transaction(decimal amount, DateTime date, string description)
  {
    this._amount = amount;
    this._date = date;
    this._description = description;
  }
  public virtual decimal GetAmount()
  {
    return _amount;
  }
  public virtual string Show()
  {
    return $"{_amount} - {_date.ToShortDateString()} - {_description}";
  }
  public virtual string GetStringRepresentation()
  {
    return $"{GetType().Name}:{_amount},{_date.ToShortDateString()},{_description}";
  }
}