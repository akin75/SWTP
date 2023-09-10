using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using TMPro;
[TestFixture]
public class KillCounterTests
{
    [Test]
    public void InitialKillCount_ShouldBeZero()
    {
        // Arrange
        var killCounterObject = new GameObject();
        var killCounter = killCounterObject.AddComponent<KillCounter>();

        var textPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/SceneManager/Kills.prefab");

        var sObject = new SerializedObject(killCounter);
        sObject.FindProperty("killCounterText").objectReferenceValue = textPrefab;
        sObject.ApplyModifiedProperties();
        // Assert
        Assert.AreEqual(0, killCounter.GetKills());
    }

    [Test]
    public void IncreaseKillCount_ShouldIncrementByOne()
    {
        // Arrange
        var killCounterObject = new GameObject();
        var killCounter = killCounterObject.AddComponent<KillCounter>();

        var textPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/SceneManager/Kills.prefab");

        var sObject = new SerializedObject(killCounter);
        sObject.FindProperty("killCounterText").objectReferenceValue = textPrefab;
        sObject.ApplyModifiedProperties();
        killCounter.IncreaseKillCount();

        // Assert
        Assert.AreEqual(1, killCounter.GetKills());
    }
}





