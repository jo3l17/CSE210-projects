public class Journal
{
  public List<Entry> _entries = new List<Entry>();
  public void AddEntry(Entry newEntry)
  {
    _entries.Add(newEntry);
  }
  public void DisplayAll()
  {
    Console.WriteLine("Jornal entries: ");
    Console.WriteLine("----------------");
    foreach (Entry entry in _entries)
    {
      entry.Display();
      Console.WriteLine();
    }
  }
}