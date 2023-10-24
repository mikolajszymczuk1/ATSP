namespace ATSP;

class AtspDataGenerator
{
    private const int MIN = 1;
    private const int MAX = 1001;

    public static int[,] GenerateData(int dimension)
    {
        int [,] outputData = new int[dimension, dimension];
        Random random = new();

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
