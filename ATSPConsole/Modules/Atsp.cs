using System.Diagnostics;

namespace ATSP;

public class Atsp
{
    private readonly int[,] _costMatrix;
    private readonly bool[] _visited;
    private readonly int _numCities;
    private List<int> _bestPath;
    private int _bestPathLength;
    private int[,] _dpTable; // Dynamic programming table

    public Atsp(int numCities, int[,] costMatrix)
    {
        _costMatrix = costMatrix;
        _numCities = numCities;
        _bestPath = new List<int>();
        _bestPathLength = int.MaxValue;
        _visited = new bool[numCities];
        _dpTable = new int[numCities, 1 << numCities];
    }

    public List<int> BestTour => _bestPath;
    public int BestTourLength => _bestPathLength;

    /// <summary>
    /// Main solving method to calculate and find the shortest path using the brute-force approach.
    /// </summary>
    /// <returns>Total time that the solving process took in milliseconds.</returns>
    public double SolveBF()
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
    /// Solves problem (ATSP) using dynamic programming.
    /// </summary>
    /// <returns>Total time that the solving process took in milliseconds.</returns>
    public double SolveDP()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        // Initialize DP table
        for (int i = 0; i < _numCities; i++)
        {
            for (int j = 0; j < (1 << _numCities); j++)
            {
                _dpTable[i, j] = int.MaxValue;
            }
        }

        int finalState = (1 << _numCities) - 1; // Final state where all cities are visited
        _bestPathLength = FindShortestPathDP(0, 1, finalState);

        // Reconstruct the best path
        ReconstructPath(0, 1, finalState);
        stopwatch.Stop();

        double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;

        return elapsedTime;
    }

    /// <summary>
    /// Method to find the shortest path with a brute-force method.
    /// </summary>
    /// <param name="currentPath">The current path being explored.</param>
    /// <param name="depth">The depth of the search tree.</param>
    /// <param name="currentLength">The length of the current path.</param>
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

    /// <summary>
    /// Helper method to find the shortest path using Dynamic Programming.
    /// </summary>
    /// <param name="currentCity">The current city being visited.</param>
    /// <param name="mask">A bitmask representing visited cities.</param>
    /// <param name="finalState">The bitmask representing the final state where all cities are visited.</param>
    /// <returns>The length of the shortest path from the current city.</returns>
    private int FindShortestPathDP(int currentCity, int mask, int finalState)
    {
        if (mask == finalState)
        {
            return _costMatrix[currentCity, 0];
        }

        if (_dpTable[currentCity, mask] != int.MaxValue)
        {
            return _dpTable[currentCity, mask];
        }

        for (int nextCity = 0; nextCity < _numCities; nextCity++)
        {
            if ((mask & (1 << nextCity)) == 0)
            {
                int newMask = mask | (1 << nextCity);
                int cost = _costMatrix[currentCity, nextCity] + FindShortestPathDP(nextCity, newMask, finalState);

                if (cost < _dpTable[currentCity, mask])
                {
                    _dpTable[currentCity, mask] = cost;
                }
            }
        }

        return _dpTable[currentCity, mask];
    }

    /// <summary>
    /// Helper method to reconstruct the best path after Dynamic Programming calculation.
    /// </summary>
    /// <param name="currentCity">The current city being visited.</param>
    /// <param name="mask">A bitmask representing visited cities.</param>
    /// <param name="finalState">The bitmask representing the final state where all cities are visited.</param>
    private void ReconstructPath(int currentCity, int mask, int finalState)
    {
        if (mask == finalState)
        {
            _bestPath.Add(0); // Add the starting city to complete the tour
            return;
        }

        for (int nextCity = 0; nextCity < _numCities; nextCity++)
        {
            if ((mask & (1 << nextCity)) == 0)
            {
                int newMask = mask | (1 << nextCity);
                int cost = _dpTable[currentCity, mask] - _costMatrix[currentCity, nextCity];

                if (cost == _dpTable[nextCity, newMask])
                {
                    _bestPath.Add(nextCity);
                    ReconstructPath(nextCity, newMask, finalState);
                    break;
                }
            }
        }
    }
}
