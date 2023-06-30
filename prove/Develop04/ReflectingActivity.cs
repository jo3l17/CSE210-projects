public class ReflectingActivity : Activity
{
  private List<Message> _prompts = new List<Message>();
  private List<Message> _questions = new List<Message>();

  public ReflectingActivity()
  {
    _name = "Reflecting";
    _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    _prompts.Add(new Message("Think of a time when you did something really difficult."));
    _prompts.Add(new Message("Think of a time when you stood up for someone else."));
    _prompts.Add(new Message("Think of a time when you helped someone in need."));
    _prompts.Add(new Message("Think of a time when youdid something truly selfless."));
    _questions.Add(new Message("Why was this experience meaningful to you?"));
    _questions.Add(new Message("Have you ever done anything like this before?"));
    _questions.Add(new Message("How did you get started?"));
    _questions.Add(new Message("How did you feel when it was complete?"));
    _questions.Add(new Message("What made this time different than other times when you were not as successful?"));
    _questions.Add(new Message("What is your favorite thing about this experience?"));
    _questions.Add(new Message("What could you learn from this experience that applies to other situations?"));
    _questions.Add(new Message("What did you learn about yourself through this experience?"));
    _questions.Add(new Message("How can you keep this experience in mind in the future?"));
  }
  public void Run()
  {
    DisplayStartingMessage();
    Console.WriteLine("Get ready...");
    LoadingBar(5);
    Console.WriteLine();
    DisplayPrompt();
    Console.WriteLine();
    Console.WriteLine("Now ponder on each of the following questions as the related to this experience");
    DisplayMessage("You may begin in ", 5);
    Console.Clear();
    DateTime startTime = DateTime.Now;
    DateTime futureTime = startTime.AddSeconds(GetDuration());
    DateTime currentTime = DateTime.Now;
    while (currentTime < futureTime)
    {
      DisplayQuestion();
      currentTime = DateTime.Now;
    }
    DisplayEndingMessage();
    Console.Clear();
  }

  public void DisplayPrompt()
  {
    Console.WriteLine("Consider the following prompt:");
    Console.WriteLine();
    Console.Write($"--- {GetRandomPrompt()} ---\n");
    Console.WriteLine();
    Console.WriteLine("When you have something in mind, press enter to continue.");
    Console.ReadLine();
  }

  public void DisplayQuestion()
  {
    Console.Write($"> {GetRandomQuestion()}");
    ShowSpinner(5);
    Console.Write("\n");
  }

  public string GetRandomPrompt()
  {
    Random rnd = new Random();
    int index = rnd.Next(_prompts.Count);
    return _prompts[index].getMessage();
  }

  public string GetRandomQuestion()
  {
    Random rnd = new Random();
    // get all the question that hasn't been used
    List<Message> unusedQuestions = _questions.Where(q => !q.isUsed()).ToList();
    // if all the questions have been used, reset them
    if (unusedQuestions.Count == 0)
    {
      foreach (Message q in _questions)
      {
        q.setUnused();
      }
      unusedQuestions = _questions.Where(q => !q.isUsed()).ToList();
    }
    // get a random question from the unused questions
    int index = rnd.Next(unusedQuestions.Count);
    return unusedQuestions[index].getMessage();
  }
}