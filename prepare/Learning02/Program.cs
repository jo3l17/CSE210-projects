using System;

class Program
{
  static void Main(string[] args)
  {
    Job j1 = new Job();
    j1._company = "Microsoft";
    j1._jobTitle = "Software Engineer";
    j1._startYear = 2019;
    j1._endYear = 2022;

    Job j2 = new Job();
    j2._company = "Apple";
    j2._jobTitle = "Manager";
    j2._startYear = 2022;
    j2._endYear = 2023;

    Resume r1 = new Resume();
    r1._name = "Allison Rose";

    r1._jobs.Add(j1);
    r1._jobs.Add(j2);

    r1.Display();
  }
}