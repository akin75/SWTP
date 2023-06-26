using NUnit.Framework;

[TestFixture]
public class KillCounterTests
{
    [Test]
    public void InitialKillCount_ShouldBeZero()
    {
        // Arrange
        KillCounter killCounter = new KillCounter();

        // Assert
        Assert.AreEqual(0, killCounter.GetKills());
    }

    [Test]
    public void IncreaseKillCount_ShouldIncrementByOne()
    {
        // Arrange
        KillCounter killCounter = new KillCounter();

        // Act
        killCounter.IncreaseKillCount();

        // Assert
        Assert.AreEqual(1, killCounter.GetKills());
    }
}





