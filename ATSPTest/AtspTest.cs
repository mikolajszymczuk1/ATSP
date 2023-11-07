namespace ATSP.Tests;

public class AtspTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        int a = 4;
        int b = 4;
        Assert.That(a, Is.EqualTo(b));
    }
}
