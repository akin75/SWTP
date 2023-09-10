using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CheckIfPlayerSwitcherLoads()
    {

        SceneManager.LoadScene(2, LoadSceneMode.Single);
        yield return new WaitForSeconds(2f);
        Assert.AreNotEqual(null,GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>());
    }
    
    
    [UnityTest]
    public IEnumerator CheckIfPlayerCanShoot()
    {

        SceneManager.LoadScene(2, LoadSceneMode.Single);
        yield return new WaitForSeconds(3f);
        var playerObject = GameObject.FindWithTag("Player");
        playerObject.GetComponentInChildren<Weapon>().Shoot();
        yield return new WaitForSeconds(0.7f);
        Assert.AreNotEqual(null, GameObject.Find("Bullet(Clone)"));
    }
    
    [UnityTest]
    public IEnumerator CheckIfPlayerTakeDamage()
    {

        SceneManager.LoadScene(2, LoadSceneMode.Single);
        yield return new WaitForSeconds(3f);
        var playerObject = GameObject.FindWithTag("Player");
        var previousHealth = playerObject.GetComponent<Player>().GetCurrentHealth();
        playerObject.GetComponent<Player>().TakeDamage(10);
        yield return new WaitForSeconds(0.7f);
        Assert.AreNotEqual(previousHealth, playerObject.GetComponent<Player>().GetCurrentHealth());
    }
    
}
