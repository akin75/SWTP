using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class MenuTest
{
    [Test]
    public void PausedTime0()
    {
        // Arrange
        PauseMenu pm = new PauseMenu();
        pm.Pause();

        // Assert
        Assert.AreEqual(0, Time.captureFramerate);
    }

    [Test]
    public void NotPausedAfterResume()
    {
        // Arrange
        PauseMenu pm = new PauseMenu();
        pm.Pause();
        pm.Resume();
        
        // Assert
        Assert.AreEqual(false, pm.IsPaused());
    }
}





