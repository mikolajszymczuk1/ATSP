using System.Diagnostics;

namespace ATSP;

public class Atsp
{
    private readonly int[,] _costMatrix;
    private readonly bool[] _visited;
    private readonly int _numCities;
    private List<int> _bestPath;
    private int _bestPathLength;

    public Atsp(int numCities, int[,] costMatrix)
    {
        _costMatrix = costMatrix;
        _numCities = numCities;
        _bestPath = new List<int>();
        _bestPathLength = int.MaxValue;
        _visited = new bool[numCities];
    }

    public List<int> BestTour => _bestPath;
    public int BestTourLength => _bestPathLength;

    /// <summary>
    /// Main solving method to calculate and find shortest path
    /// </summary>
    /// <returns>
    /// Total time that solving process took
    /// </returns>
    public double Solve()
    {
        List<int> currentPath = new() { 0 };
        _visited[0] = true;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        FindShortestPath(currentPath, 1, 0);
        stopwatch.Stop();

        double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;

        _visited[0] = false;

        return elapsedTime;
    }

    /// <summary>
    /// Method finds shortest path with brute force method
    /// </summary>
    /// <param name="currentPath">Current path</param>
    /// <param name="depth">Depth</param>
    /// <param name="currentLength">Current length</param>
    private void FindShortestPath(List<int> currentPath, int depth, int currentLength)
    {
        if (depth == _numCities)
        {
            int lastCity = currentPath.Last();
            int length = currentLength + _costMatrix[lastCity, 0];

            if (length < _bestPathLength)
            {
                _bestPath = new List<int>(currentPath);
                _bestPathLength = length;
            }
        }
        else
        {
            for (int i = 0; i < _numCities; i++)
            {
                if (!_visited[i])
                {
                    int lastCity = currentPath.Last();
                    int newLength = currentLength + _costMatrix[lastCity, i];

                    if (newLength < _bestPathLength)
                    {
                        currentPath.Add(i);
                        _visited[i] = true;

                        FindShortestPath(currentPath, depth + 1, newLength);

                        _visited[i] = false;
                        currentPath.RemoveAt(currentPath.Count - 1);
                    }
                }
            }
        }
    }
}
