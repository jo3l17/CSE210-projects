using System.IO;

class Program
{
  static void Main(string[] args)
  {
    bool exit = false;
    string filename = "journal.txt";
    Journal journal = new Journal();
    while (!exit)
    {
      int option = DisplayMenu();
      switch (option)
      {
        case 1:
          string prompt = PromptGenerator.GetRandomPrompt();
          Console.WriteLine(prompt);
          Console.Write("> ");
          string inputText = Console.ReadLine();
          Entry newEntry = new Entry();
          DateTime theCurrentTime = DateTime.Now;
          newEntry._date = theCurrentTime.ToShortDateString();
          newEntry._promptText = prompt;
          newEntry._entryText = inputText;
          journal.AddEntry(newEntry);
          break;
        case 2:
          journal.DisplayAll();
          break;
        case 3:
          Console.WriteLine("What is the filename?");
          filename = Console.ReadLine();
          string[] lines = File.ReadAllLines(filename);
          int entriesNumber = lines.Count() / 3;
          journal = new Journal();
          for (int i = 0; i < entriesNumber; i++)
          {
            int entryIndex = i * 3;
            string entryDate = lines[entryIndex].Split(": ")[1];
            string entryPrompt = lines[entryIndex + 1].Split(": ")[1];
            string entryText = lines[entryIndex + 2].Split(": ")[1];
            Entry inputEntry = new Entry();
            inputEntry._date = entryDate;
            inputEntry._promptText = entryPrompt;
            inputEntry._entryText = entryText;
            journal.AddEntry(inputEntry);
          }
          break;
        case 4:
          Console.WriteLine("What is the filename?");
          filename = Console.ReadLine();
          using (StreamWriter outputFile = new StreamWriter(filename))
          {
            foreach (Entry entry in journal._entries)
            {
              outputFile.WriteLine($"Date: {entry._date}");
              outputFile.WriteLine($"Prompt: {entry._promptText}");
              outputFile.WriteLine($"Entry: {entry._entryText}");
            }
          }
          break;
        case 5:
          exit = true;
          break;
      }
    }
  }

  public static int DisplayMenu()
  {
    Console.WriteLine("Please select one of the following choices");
    Console.WriteLine("1. Write");
    Console.WriteLine("2. Display");
    Console.WriteLine("3. Load");
    Console.WriteLine("4. Save");
    Console.WriteLine("5. Quit");
    Console.Write("What would you like to do? ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 5)
    {
      Console.WriteLine("Invalid option");
      Console.Write("What would you like to do? ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
}