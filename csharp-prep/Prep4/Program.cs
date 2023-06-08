using System;

class Program
{
  static void Main(string[] args)
  {
    List<int> numbers = new List<int>();
    Console.WriteLine("Enter a list of numbers, type 0 when finished.");
    int number;
    do
    {
      Console.Write("Enter number: ");
      string userInput = Console.ReadLine();
      number = Convert.ToInt32(userInput);
      if (number != 0)
      {
        numbers.Add(number);
      }
    }
    while (number != 0);

    int sum = numbers.Sum();
    double average = numbers.Average();
    int maxNumber = numbers.Max();

    Console.WriteLine($"The sum is: {sum}");
    Console.WriteLine($"The average is: {average}");
    Console.WriteLine($"The largest number is: {maxNumber}");

    List<int> positiveNumbers = numbers.Where(x => x > 0).ToList();
    int smallestPositiveNumber = positiveNumbers.Min();

    Console.WriteLine($"The smallest positive number is: {smallestPositiveNumber}");

    List<int> sortedNumbers = numbers.OrderBy(x => x).ToList();

    Console.WriteLine("The sorted list is:");
    foreach (int num in sortedNumbers)
    {
      Console.WriteLine(num);
    }
  }
}