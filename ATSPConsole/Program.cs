namespace ATSP;

class Program
{
    private const int DEFAULT_SIZE = 4;

    static void Main()
    {
        Atsp atsp;
        string? selectedOption = "";
        int size = DEFAULT_SIZE;
        int[,] data = AtspDataGenerator.GenerateData(DEFAULT_SIZE, new Random());

        while (selectedOption != "6")
        {
            PrintHelper.PrintMenu();
            Console.Write("Your option: ");
            selectedOption = Console.ReadLine();
            Console.Clear();

            switch (selectedOption)
            {
                // Option 1
                case "1":
                    Console.Write("Write path to file: ");
                    string? filePath = Console.ReadLine();
                    AtspFileReader afr = new(filePath!);
                    Console.WriteLine(afr.Dimension);
                    data = afr.ReadFile();
                    size = Convert.ToInt32(afr.Dimension);

                    break;

                // Option 2
                case "2":
                    Console.Write("Write matrix size: ");
                    size = Convert.ToInt32(Console.ReadLine());
                    data = AtspDataGenerator.GenerateData(size, new Random());
                    break;

                // Option 3
                case "3":
                    PrintHelper.PrintData(data);
                    break;

                // Option 4
                case "4":
                    Console.WriteLine("Brute Force method");
                    Console.WriteLine("Program output: ");

                    atsp = new(size, data);
                    Console.WriteLine("Total time: " + atsp.SolveBF() + "ms");
                    Console.WriteLine("Best path: " + string.Join(" -> ", atsp.BestTour));
                    Console.WriteLine("Path cost: " + atsp.BestTourLength);
                    break;

                // Option 5
                case "5":
                    Console.WriteLine("Dynamic Programming method");
                    Console.WriteLine("Program output: ");

                    atsp = new(size, data);
                    Console.WriteLine("Total time: " + atsp.SolveDP() + "ms");
                    Console.WriteLine("Best path: " + string.Join(" -> ", atsp.BestTour));
                    Console.WriteLine("Path cost: " + atsp.BestTourLength);
                    break;

                // Option 6
                case "6":
                    Console.WriteLine("Exit the program :)");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
