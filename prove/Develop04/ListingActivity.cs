public class ListingActivity : Activity
{
  private int _count;
  private List<string> _prompts = new List<string>();
  public ListingActivity()
  {
    _count = 0;
    _name = "Listing";
    _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This witt help you recognize the power you have and how you can use it in other aspects of your Life.";
    _prompts.Add("Who are people that you appreciate?");
    _prompts.Add("What are personal strengths of yours?");
    _prompts.Add("Who are people that you have helped this week?");
    _prompts.Add("When have you felt the Holy Ghost this month?");
    _prompts.Add("Who are some of your personal heroes?");
  }
  public void GetRandomPrompt()
  {
    Random rnd = new Random();
    int index = rnd.Next(_prompts.Count);
    Console.WriteLine($"--- {_prompts[index]} ---");
  }
  public List<string> GetListFromUser()
  {
    DateTime startTime = DateTime.Now;
    DateTime futureTime = startTime.AddSeconds(GetDuration());
    DateTime currentTime = DateTime.Now;
    List<string> list = new List<string>();
    while (currentTime < futureTime)
    {
      Console.Write("> ");
      string input = Console.ReadLine();
      list.Add(input);
      _count++;
      currentTime = DateTime.Now;
    }
    return list;
  }
  public void Run()
  {
    DisplayStartingMessage();
    Console.WriteLine("Get ready...");
    LoadingBar(5);
    Console.WriteLine("List as many responses you can to the following prompt:");
    GetRandomPrompt();
    DisplayMessage("You may begin in:", 5);
    Console.WriteLine();
    List<string> list = GetListFromUser();
    Console.WriteLine($"You listed {_count} items.");
    Console.WriteLine("Your list:");
    foreach (string item in list)
    {
      Console.WriteLine(item);
    }
    Console.WriteLine();
    Console.WriteLine("Well done!");
    ShowSpinner(5);
  }
}