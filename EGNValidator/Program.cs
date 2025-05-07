using ValidatorForEGN;

class Program
{
    static void Main(string[] args)
    {
        EGNValidator validator = new EGNValidator();

        DateTime date = DateTime.Parse(Console.ReadLine());
        string city = Console.ReadLine();
        bool isMale = (Console.ReadLine() == "male");

        foreach (var egn in validator.Generate(date, city, isMale))
            Console.WriteLine(egn);
    }
}