using System;

class Program
{
  static void Main(string[] args)
  {
    bool playAgain = true;

    while (playAgain)
    {
      Random random = new Random();
      int magicNumber = random.Next(1, 101);
      int guessCount = 0;
      int guess = 0;

      Console.WriteLine("Guess My Number Game!");
      Console.WriteLine("---------------------");

      while (guess != magicNumber)
      {
        Console.Write("What is your guess? ");
        guess = Convert.ToInt32(Console.ReadLine());
        guessCount++;
        if (guess < magicNumber)
        {
          Console.WriteLine("Higher");
        }
        else if (guess > magicNumber)
        {
          Console.WriteLine("Lower");
        }
        else
        {
          Console.WriteLine("You guessed it!");
        }
      }

      Console.WriteLine("Congratulations! You guessed the number in " + guessCount + " attempts.");

      Console.Write("Do you want to play again? (yes/no): ");
      string playAgainInput = Console.ReadLine();

      playAgain = (playAgainInput == "yes");
      Console.WriteLine();
    }
  }
}