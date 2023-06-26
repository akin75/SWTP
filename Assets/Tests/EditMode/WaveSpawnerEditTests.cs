using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WaveSpawnerEditTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TimeBetweenWaves()
    {
        // Use the Assert class to test conditions
        var gameObject = new GameObject();
        var ws = gameObject.AddComponent<WaveSpawner>();
        Assert.IsTrue(ws.timeBetweenWaves > 0);
    }
}
