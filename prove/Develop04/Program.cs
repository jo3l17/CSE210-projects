using System;

class Program
{
  static void Main(string[] args)
  {
    int option = DisplayMenu();
    while (option != 4)
    {
      switch (option)
      {
        case 1:
          BreathingActivity breathingActivity = new BreathingActivity();
          breathingActivity.Run();
          break;
        case 2:
          ReflectingActivity reflectingActivity = new ReflectingActivity();
          reflectingActivity.Run();
          break;
        case 3:
          ListingActivity listingActivity = new ListingActivity();
          listingActivity.Run();
          break;
      }
      option = DisplayMenu();
    }
    Console.WriteLine("Quitting");
  }

  static int DisplayMenu()
  {
    Console.WriteLine("Menu options:");
    Console.WriteLine("1. Start Breathing Activity");
    Console.WriteLine("2. Start Reflecting Activity");
    Console.WriteLine("3. Start Listing Activity");
    Console.WriteLine("4. Quit");
    Console.Write("Select a choice from the menu: ");
    int option = int.Parse(Console.ReadLine());
    while (option < 1 || option > 4)
    {
      Console.WriteLine("Invalid option");
      Console.Write("Select a choice from the menu: ");
      option = int.Parse(Console.ReadLine());
    }
    return option;
  }
}