public class BreathingActivity : Activity
{
  public BreathingActivity(){
    _name = "Breathing";
    _description = "This activity will help you retax by walking your through breathing in and out simety. Clear your mind and focus on your breathing.";
  }
  public void Run()
  {
    DisplayStartingMessage();
    Console.WriteLine("Get ready...");
    LoadingBar(5);
    Console.WriteLine();
    DateTime startTime = DateTime.Now;
    DateTime futureTime = startTime.AddSeconds(GetDuration());
    DateTime currentTime = DateTime.Now;
    int breathIncount = 4;
    int holdBreathCount = 7;
    int breathOutcount = 8;
    while (currentTime < futureTime)
    {
      DisplayMessage("Breathe in...", breathIncount);
      DisplayMessage("Hold breath...", holdBreathCount);
      DisplayMessage("Breathe out...", breathOutcount);
      Console.WriteLine();
      currentTime = DateTime.Now;
    }
    DisplayEndingMessage();
    Console.Clear();
  }
}