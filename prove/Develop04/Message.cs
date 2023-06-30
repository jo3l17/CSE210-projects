public class Message
{
  private string _message;
  private bool _used = false;
  public Message(string message)
  {
    _message = message;
  }
  public string getMessage()
  {
    this._used = true;
    return _message;
  }
  public bool isUsed()
  {
    return _used;
  }
  public void setUnused()
  {
    _used = false;
  }
}