namespace ATSP;

class Program
{
    private const int DEFAULT_SIZE = 4;

    static void Main()
    {
        string? selectedOption = "";
        int size = DEFAULT_SIZE;
        int[,] data = AtspDataGenerator.GenerateData(DEFAULT_SIZE);

        while (selectedOption != "5")
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
                    data = AtspDataGenerator.GenerateData(size);
                    break;

                // Option 3
                case "3":
                    PrintHelper.PrintData(data);
                    break;

                // Option 4
                case "4":
                    Console.WriteLine("Program output: ");

                    Atsp atsp = new(size, data);
                    Console.WriteLine("Total time: " + atsp.Solve() + "ms");
                    Console.WriteLine("Best path: " + string.Join(" -> ", atsp.BestTour));
                    Console.WriteLine("Path cost: " + atsp.BestTourLength);
                    break;

                // Option 5
                case "5":
                    Console.WriteLine("Exit the program :)");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
