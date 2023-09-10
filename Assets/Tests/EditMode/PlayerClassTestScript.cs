using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerClassTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerClassCanBeCreated()
    {
        PlayerClass player = new PlayerClass(100, new GameObject("Test"), 1f, new AnimationCurve());
        Assert.AreNotEqual(null, player);
    }

    [Test]
    public void PlayerClassHasHp()
    {
        PlayerClass player = new PlayerClass(100, new GameObject("Test"), 1f, new AnimationCurve());
        Assert.AreNotEqual(0, player.GetCurrentHealth());
    }


    [Test]
    public void PlayerClassCanLvlUp()
    {
        PlayerClass player = new PlayerClass(100, new GameObject("Test"), 1f, new AnimationCurve());
        player.SetLevelCurve(100);
        player.SetExpPoints(100);
        Assert.AreEqual(true, player.hasLeveledUp());
    }
}
