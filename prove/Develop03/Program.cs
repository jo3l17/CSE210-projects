using System.Text.Json;
using System.Text.RegularExpressions;
class Program
{
  static void Main(string[] args)
  {
    string filename = "BOM.json";
    string json = File.ReadAllText(filename);
    JSONBooks jsonBooks = JsonSerializer.Deserialize<JSONBooks>(json);
    List<Book> books = jsonBooks.books;
    Random rnd = new Random();
    bool exit = false;
    while (!exit)
    {
      int option = DisplayMenu();
      switch (option)
      {
        case 1:
          Book rndBook = books[rnd.Next(books.Count - 1)];
          Chapter rndChapter = rndBook.chapters[rnd.Next(rndBook.chapters.Count - 1)];
          Verse rndVerse = rndChapter.verses[rnd.Next(rndChapter.verses.Count - 1)];
          Reference rndReference = new Reference(rndBook.book, rndChapter.chapter, rndVerse.verse);
          Scripture rndScripture = new Scripture(rndReference, rndVerse.text);
          Console.Clear();
          Play(rndScripture);
          break;
        case 2:
          Console.WriteLine("type a reference to the book of mormon, I.E Alma 7:11-12, type quit to go back");
          string scripturePrompt = Console.ReadLine();
          string regex = "[0-9]?[ ]?[A-Za-z]+[ ][0-9]+:?[0-9]?[0-9]?-?[0-9]?";
          bool matchFound = Regex.IsMatch(scripturePrompt, regex);
          if (scripturePrompt == "quit")
          {
            break;
          }
          while (!matchFound)
          {
            Console.WriteLine("Try again, make sure your scrpiture has the following format: Alma 7:11-12");
            scripturePrompt = Console.ReadLine();
            matchFound = Regex.IsMatch(scripturePrompt, regex);
            if (scripturePrompt == "quit")
            {
              break;
            }
            break;
          }
          string bookToFind = scripturePrompt.Split(" ")[0];
          string[] scriptureSplitted = scripturePrompt.Split(" ");
          bool hasVerses = scripturePrompt.Split(":").Count() > 1;
          bool isNumber = Regex.IsMatch(bookToFind, @"\d");
          if (isNumber)
          {
            bookToFind = $"{scriptureSplitted[0]} {scriptureSplitted[1]}";
          }
          string chapter = scripturePrompt.Split(bookToFind + " ")[1];
          int chapterNum = int.Parse(chapter.Split(":")[0]);
          int verse = 0;
          int endVerse = 0;
          if (hasVerses)
          {
            string verses = scripturePrompt.Split(":")[1];
            bool hasMultipleVerses = verses.Split("-").Count() > 1;
            verse = int.Parse(verses.Split("-")[0]);
            if (hasMultipleVerses)
            {
              endVerse = int.Parse(verses.Split("-")[1]);
            }
            if (endVerse <= verse && endVerse != 0)
            {
              Console.WriteLine("The end verse has to be greater than the initial verse");
              break;
            }
          }
          int selectedBookPos = books.FindIndex(book => book.book == bookToFind);
          if (selectedBookPos < 0)
          {
            Console.WriteLine("That book doesn't exists in the book of Mormon");
            break;
          }
          Book selectedBook = books[selectedBookPos];
          bool chapterExists = selectedBook.chapters.Count() >= chapterNum;
          if (!chapterExists)
          {
            Console.WriteLine($"That Chapter doesn't exists in {selectedBook} in the book of Mormon");
            break;
          }
          Chapter selectedChapter = selectedBook.chapters[chapterNum - 1];
          bool verseExists = selectedChapter.verses.Count() >= verse && selectedChapter.verses.Count() >= endVerse;
          if (!verseExists)
          {
            Console.WriteLine($"That or those Verses doesn't exists in {selectedBook} {selectedChapter} in the book of Mormon");
            break;
          }
          List<Verse> selectedVerses = selectedChapter.verses.GetRange(0, selectedChapter.verses.Count());
          if (hasVerses)
          {
            selectedVerses = selectedChapter.verses.GetRange(verse - 1, endVerse != 0 ? endVerse - verse + 1 : 1);
          }
          string joinedVerses = string.Join(" ", selectedVerses.Select(verse => verse.text));
          Reference selectedReference = new Reference(selectedBook.book, selectedChapter.chapter, verse);
          if (endVerse != 0)
          {
            selectedReference = new Reference(selectedBook.book, selectedChapter.chapter, verse, endVerse);
          }
          Scripture selectedScripture = new Scripture(selectedReference, joinedVerses);
          Console.Clear();
          Play(selectedScripture);
          break;
        case 3:
          exit = true;
          break;
      }
    }
  }
  public static int DisplayMenu()
  {
    Console.Clear();
    Console.WriteLine("This app only supports Book of Mormon references for now, select an option");
    Console.WriteLine("1. Random Scripture");
    Console.WriteLine("2. Find Scripture");
    Console.WriteLine("3. Quit");
    Console.Write("What would you like to do? ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 3)
    {
      Console.WriteLine("Invalid option");
      Console.Write("What would you like to do? ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
  public static void Play(Scripture scripture)
  {
    bool quit = false;
    while (!quit)
    {
      Console.WriteLine(scripture.GetDisplayText());
      Console.WriteLine();
      Console.WriteLine("Press enter to continue or type 'quit' to finish:");
      string input = Console.ReadLine();
      if (input == "quit" || scripture.IsCompletelyHidden())
      {
        quit = true;
        break;
      }
      scripture.HideRandomWords(5);
      Console.Clear();
    }
  }
}

class JSONBooks
{
  public List<Book> books { get; set; }
}

class Book
{
  public string book { get; set; }
  public List<Chapter> chapters { get; set; }
}

class Chapter
{
  public int chapter { get; set; }
  public string reference { get; set; }
  public List<Verse> verses { get; set; }
}

class Verse
{
  public string reference { get; set; }
  public string text { get; set; }
  public int verse { get; set; }
}