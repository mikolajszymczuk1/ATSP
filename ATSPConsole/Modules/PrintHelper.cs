namespace ATSP;

class PrintHelper
{
    /// <summary>
    /// Method to print simple menu for user
    /// </summary>
    public static void PrintMenu()
    {
        Console.WriteLine("====== MENU ======");
        Console.WriteLine("1) Load data from file");
        Console.WriteLine("2) Generate random data");
        Console.WriteLine("3) Show loaded data");
        Console.WriteLine("4) Run program (BF)");
        Console.WriteLine("5) Run program (DP)");
        Console.WriteLine("6) Run program (TS)");
        Console.WriteLine("7) Exit program");
    }

    /// <summary>
    /// Method to print data for user
    /// </summary>
    /// <param name="dataToPrint">Data to print</param>
    public static void PrintData(int[,] dataToPrint) {
        for (int i = 0; i < dataToPrint.GetLength(0); i++)
        {
            for (int j = 0; j < dataToPrint.GetLength(1); j++)
            {
                Console.Write(dataToPrint[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }
}
