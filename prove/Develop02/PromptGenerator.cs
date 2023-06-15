public class PromptGenerator
{
  private static List<string> _prompts = new List<string>()
  {
    "What was a memorable moment or conversation I had today?",
    "What is something new that I learned or discovered today?",
    "Reflect on a challenge or obstacle I faced today and how I handled it.",
    "Reflecting on the day, what was a moment that made me feel grateful?",
    "What is something I learned about myself or others today?",
  };

  public static string GetRandomPrompt()
  {
    Random random = new Random();
    return _prompts[random.Next(_prompts.Count)];
  }
}