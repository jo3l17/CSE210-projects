public class Transaction
{
  protected decimal _amount { get; }
  protected DateTime _date { get; }
  protected string _description { get; }

  public Transaction(decimal amount, DateTime date, string description)
  {
    this._amount = amount;
    this._date = date;
    this._description = description;
  }

  public virtual void ProcessTransaction() { }
}