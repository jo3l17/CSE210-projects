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
          // Gets a random scripture from the file, and format it
          Book rndBook = books[rnd.Next(books.Count - 1)];
          Chapter rndChapter = rndBook.chapters[rnd.Next(rndBook.chapters.Count - 1)];
          Verse rndVerse = rndChapter.verses[rnd.Next(rndChapter.verses.Count - 1)];
          Reference rndReference = new Reference(rndBook.book, rndChapter.chapter, rndVerse.verse);
          Scripture rndScripture = new Scripture(rndReference, rndVerse.text);
          Console.Clear();
          Play(rndScripture);
          break;
        case 2:
          // Gets a reference from the book of Mormon I.E Alma 7:11-12, and then get it from the file and format.
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
          // Split the prompt from the user to find the book, chapter and verse(s) on the file
          string bookToFind = scripturePrompt.Split(" ")[0];
          string[] scriptureSplitted = scripturePrompt.Split(" ");
          // If this value is a number it means the book has a number at the begginning I.E "1 Nephi"
          bool isNumber = Regex.IsMatch(bookToFind, @"\d");
          if (isNumber)
          {
            bookToFind = $"{scriptureSplitted[0]} {scriptureSplitted[1]}";
          }
          string chapter = scripturePrompt.Split(bookToFind + " ")[1];
          int chapterNum = int.Parse(chapter.Split(":")[0]);
          int verse = 0;
          int endVerse = 0;
          // if there is a number after a ":" after the chapter that means it will look for specific verses
          bool hasVerses = chapter.Split(":").Count() > 1;
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
              Console.WriteLine("\nThe end verse needs to be greater than the initial verse\n");
              break;
            }
          }
          // Check that the book exists in the file
          int selectedBookPos = books.FindIndex(book => book.book == bookToFind);
          if (selectedBookPos < 0)
          {
            Console.WriteLine("\nThat book doesn't exists in the book of Mormon\n");
            break;
          }
          Book selectedBook = books[selectedBookPos];
          // Check that the chapter exists in the book
          bool chapterExists = selectedBook.chapters.Count() >= chapterNum;
          if (!chapterExists)
          {
            Console.WriteLine($"\nThat Chapter doesn't exists in {selectedBook.book} in the book of Mormon\n");
            break;
          }
          Chapter selectedChapter = selectedBook.chapters[chapterNum - 1];
          // Check that the verse exists in the Chapter
          bool verseExists = selectedChapter.verses.Count() >= verse && selectedChapter.verses.Count() >= endVerse;
          if (!verseExists)
          {
            Console.WriteLine($"\nThat or those Verses doesn't exists in {selectedBook.book} {selectedChapter.chapter} in the book of Mormon\n");
            break;
          }
          // Select a range of verses, in case no verses are given the whole chapter is selected
          List<Verse> selectedVerses = selectedChapter.verses.GetRange(0, selectedChapter.verses.Count());
          if (hasVerses)
          {
            selectedVerses = selectedChapter.verses.GetRange(verse - 1, endVerse != 0 ? endVerse - verse + 1 : 1);
          }
          // Format the cod
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
        Console.Clear();
        quit = true;
        break;
      }
      scripture.HideRandomWords(5);
      Console.Clear();
    }
  }
}

// The following classes are only to structure the JSON file
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