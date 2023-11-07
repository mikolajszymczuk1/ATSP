namespace ATSP;

public class AtspDataGenerator
{
    public const int MIN = 1;
    public const int MAX = 1001;

    /// <summary>
    /// Method generates costs matrix with random data
    /// </summary>
    /// <param name="dimension">Dimension of matrix</param>
    /// <param name="random">Random object</param>
    /// <returns>
    /// Costs matrix with random data
    /// </returns>
    public static int[,] GenerateData(int dimension, Random random)
    {
        int [,] outputData = new int[dimension, dimension];

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (i == j)
                {
                    outputData[i, j] = 0;
                }
                else
                {
                    outputData[i, j] = random.Next(MIN, MAX);
                }
            }
        }

        return outputData;
    }
}
