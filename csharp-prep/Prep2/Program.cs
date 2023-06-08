using System;

class Program
{
  static void Main(string[] args)
  {
    Console.Write("Enter your grade percentage: ");
    string grade = Console.ReadLine();
    int gradePercentage = int.Parse(grade);

    string letter = "";

    if (gradePercentage >= 90)
    {
      letter = "A";
    }
    else if (gradePercentage >= 80)
    {
      letter = "B";
    }
    else if (gradePercentage >= 70)
    {
      letter = "C";
    }
    else if (gradePercentage >= 60)
    {
      letter = "D";
    }
    else
    {
      letter = "F";
    }

    int lastDigit = gradePercentage % 10;
    string sign = "";

    if (letter == "A")
    {
      if (lastDigit < 3)
      {
        sign = "-";
      }
    }
    else if (letter != "F")
    {
      if (lastDigit >= 7)
      {
        sign = "+";
      }
      else if (lastDigit < 3)
      {
        sign = "-";
      }
    }

    Console.WriteLine($"Your grade is: {letter}{sign}");

    if (gradePercentage >= 70)
    {
      Console.WriteLine("Congratulations! You passed the class!");
    }
    else
    {
      Console.WriteLine("Sorry, you did not pass the class. Better luck next time!");
    }
  }
}