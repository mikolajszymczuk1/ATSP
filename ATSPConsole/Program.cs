namespace ATSP;

class Program
{
    private const int DEFAULT_SIZE = 4;

    static void PrintMenu()
    {
        Console.WriteLine("====== MENU ======");
        Console.WriteLine("1) Load data from file");
        Console.WriteLine("2) Generate random data");
        Console.WriteLine("3) Show loaded data");
        Console.WriteLine("4) Run program");
        Console.WriteLine("5) Exit program");
    }

    static void PrintData(int[,] dataToPrint) {
        for (int i = 0; i < dataToPrint.GetLength(0); i++)
        {
            for (int j = 0; j < dataToPrint.GetLength(1); j++)
            {
                Console.Write(dataToPrint[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static void Main()
    {
        string? selectedOption = "";
        int size = DEFAULT_SIZE;
        int[,] data = AtspDataGenerator.GenerateData(DEFAULT_SIZE);

        while (selectedOption != "5")
        {
            PrintMenu();
            Console.Write("Your option: ");
            selectedOption = Console.ReadLine();
            Console.Clear();

            switch (selectedOption)
            {
                case "1":
                    Console.Write("Write path to file: ");
                    string? filePath = Console.ReadLine();
                    AtspFileReader afr = new(filePath!);
                    Console.WriteLine(afr.Dimension);
                    data = afr.ReadFile();
                    size = Convert.ToInt32(afr.Dimension);

                    break;

                case "2":
                    Console.Write("Write matrix size: ");
                    size = Convert.ToInt32(Console.ReadLine());
                    data = AtspDataGenerator.GenerateData(size);
                    break;

                case "3":
                    PrintData(data);
                    break;

                case "4":
                    Console.WriteLine("Program output: ");

                    Atsp atsp = new(size, data);
                    Console.WriteLine("Total time: " + atsp.Solve() + "ms");
                    Console.WriteLine("Best path: " + string.Join(" -> ", atsp.BestTour));
                    Console.WriteLine("Path cost: " + atsp.BestTourLength);
                    break;

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
