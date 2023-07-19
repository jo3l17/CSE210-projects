public class InputValidator
{
  public static string NoCommas(string message)
  {
    Console.Write(message);
    string output = Console.ReadLine();
    while (output.Contains(","))
    {
      Console.WriteLine("No commas allowed!");
      Console.Write(message);
      output = Console.ReadLine();
    }
    return output;
  }
}