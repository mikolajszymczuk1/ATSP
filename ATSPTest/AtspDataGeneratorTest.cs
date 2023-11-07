using Moq;

namespace ATSP.Tests;

[TestFixture]
public class AtspDataGeneratorTest
{
    [Test]
    public void GenerateMatrixWithExpectedDimension()
    {
        // Given
        int testDimension = 5;
        int expectedDimension = testDimension;

        // When
        int[,] data = AtspDataGenerator.GenerateData(testDimension, new Random());

        // Then
        Assert.That(expectedDimension, Is.EqualTo(data.GetLength(0)));
    }

    [Test]
    public void GenerateMatrixWithZerosOnDiagonal()
    {
        // Given
        int testDimension = 8;
        int expectedDiagonalValue = 0;

        // When
        int[,] data = AtspDataGenerator.GenerateData(testDimension, new Random());

        // Then
        for (int i = 0; i < testDimension; i++)
        {
            Assert.That(expectedDiagonalValue, Is.EqualTo(data[i, i]));
        }
    }

    [Test]
    public void GenerateSpecificMatrixCorrectly()
    {
        // Given
        int testDimension = 3;
        int[,] expectedData = {
            {0, 5, 1},
            {6, 0, 10},
            {10, 5, 0},
        };

        Mock<Random> mockRandom = new();
        mockRandom.SetupSequence(r => r.Next(AtspDataGenerator.MIN, AtspDataGenerator.MAX))
            .Returns(5).Returns(1).Returns(6).Returns(10).Returns(10).Returns(5);

        // When
        int[,] data = AtspDataGenerator.GenerateData(testDimension, mockRandom.Object);

        // Then
        Assert.That(expectedData, Is.EqualTo(data));
    }
}
