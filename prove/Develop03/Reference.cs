public class Reference
{
  private string _book;
  public int _chapter;
  public int _verse;
  public int _endVerse;
  public Reference(string book, int chapter)
  {
    _book = book;
    _chapter = chapter;
  }
  public Reference(string book, int chapter, int verse)
  {
    _book = book;
    _chapter = chapter;
    _verse = verse;
  }
  public Reference(string book, int chapter, int verse, int endVerse)
  {
    _book = book;
    _chapter = chapter;
    _verse = verse;
    _endVerse = endVerse;
  }
  public string GetDisplayText()
  {
    return $"{_book} {_chapter}" + (_verse != 0 ? $":{_verse}" : "") + (_endVerse != 0 ? $"-{_endVerse}" : "");
  }
}