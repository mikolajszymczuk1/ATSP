namespace ATSP;

class AtspFileReader
{
    private string _astpFilePath = "";
    private string? _name = "";
    private string? _fileType = "";
    private string? _comment = "";
    private string? _dimension = "";
    private string? _edgeWeightType = "";
    private string? _edgeWeightFormat = "";

    public AtspFileReader(string astpFilePath)
    {
        _astpFilePath = astpFilePath;
    }

    public string? Name => _name;
    public string? FileType => _fileType;
    public string? Comment => _comment;
    public string? Dimension => _dimension;
    public string? EdgeWeightType => _edgeWeightType;
    public string? EdgeWeightFormat => _edgeWeightFormat;

    /// <summary>
    /// Method read .astp file and get all needed information from it
    /// </summary>
    /// <returns>
    /// Costs matrix
    /// </returns>
    public int[,] ReadFile()
    {
        int[,] costMatrix = {};
        List<string> elements = new();

        try
        {
            using StreamReader reader = new(_astpFilePath);

            // Load base data
            _name = GetValueFromFileLine(reader.ReadLine());
            _fileType = GetValueFromFileLine(reader.ReadLine());
            _comment = GetValueFromFileLine(reader.ReadLine());
            _dimension = GetValueFromFileLine(reader.ReadLine());
            _edgeWeightType = GetValueFromFileLine(reader.ReadLine());
            _edgeWeightFormat = GetValueFromFileLine(reader.ReadLine());
            reader.ReadLine(); // Read last unused section in file [ EDGE_WEIGHT_SECTION ]

            // Load matrix
            int lineCount = 0;
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCount++;
                string[] wordsInLine = line.Split(new char[] { ' ', '\t', '.', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in wordsInLine)
                {
                    elements.Add(word);
                }
            }

            // Generate matrix
            int row = 0;
            int col = 0;
            costMatrix = new int[Convert.ToInt32(_dimension), Convert.ToInt32(_dimension)];
            foreach (string el in elements)
            {
                if (el == "EOF") break;
                costMatrix[row, col] = Convert.ToInt32(el);
                col++;

                if (col == Convert.ToInt32(_dimension))
                {
                    col = 0;
                    row++;
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        return costMatrix;
    }

    /// <summary>
    /// Method takes string in notation --> KEY: VALUE and returns <c>VALUE<c>
    /// </summary>
    /// <param name="lineRead">String line to get value from</param>
    /// <returns>
    ///
    /// </returns>
    private static string GetValueFromFileLine(string? lineRead)
    {
        if (lineRead == null) return "";
        string[] parts = lineRead.Trim().Split(':');
        return parts[1].Trim(); // Return second part as value
    }
}
