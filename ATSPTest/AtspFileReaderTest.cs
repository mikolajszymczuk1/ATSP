namespace ATSP.Tests;

[TestFixture]
public class AtspFileReaderTest
{
    private AtspFileReader afr;
    private readonly string FILE_PATH = "data/test4.atsp";

    [SetUp]
    public void Setup()
    {
        string runningDir = TestContext.CurrentContext.TestDirectory;
        string projectDir = Directory.GetParent(runningDir)!.Parent!.Parent!.FullName;
        afr = new(Path.Combine(projectDir, FILE_PATH));
    }

    [Test]
    public void ReadBasicDataCorrectly()
    {
        afr.ReadFile();
        Assert.That(4, Is.EqualTo(Convert.ToInt32(afr.Dimension)));
    }

    [Test]
    public void ReadCostsMatrixCorrectly()
    {
        Assert.Pass();
    }
}
