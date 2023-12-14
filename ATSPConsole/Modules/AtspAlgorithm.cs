namespace ATSP;

public abstract class AtspAlgorithm
{
    protected AtspGraph _graph;
    protected int _startVertex;
    protected bool IsBenchmark;

    protected AtspAlgorithm(AtspGraph graph, int startVertex)
    {
        _graph = graph;
        _startVertex = startVertex;
    }

    protected AtspAlgorithm()
    {
        IsBenchmark = true;
    }

    public int StartVertex
    {
        get => _startVertex;
        set => _startVertex = value;
    }


    public AtspGraph Graph
    {
        get => _graph;
        set => _graph = value;
    }

    public abstract void Start();
}
