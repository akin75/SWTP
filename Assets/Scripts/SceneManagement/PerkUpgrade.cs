using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PerkUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
     private GameObject player;
     private GameObject text;
    private Vector3 offset = new Vector3(0, -2.5f);
    public TextMeshProUGUI levelText;
    public GameObject shopUI;
    public List<Perks> upgradesList;
    public bool shopState = false;
    public GameObject shopItemPrefab;
    public Transform shopContent;
    private Weapon weapon;
    private PlayerSwitcher playerManager;
    private decimal constantMultiplier = 0.2m;
    void Start()
    {
        
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = playerManager.GetCurrentPlayer();
        weapon = player.GetComponentInChildren<Weapon>();
        //Debug.Log($"Test: {weapon.GetLevel()}");

        foreach (Perks upgrades in upgradesList)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);
            upgrades.itemRef = item;
            var imageItem = item.transform.GetChild(0);
            foreach (Transform child in imageItem)
            {
                if (child.gameObject.name == "PerkName")
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = upgrades.name + " Level: " + upgrades.perkLevel;
                }
                if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrades.image;
                }
                if (child.gameObject.name == "PerkUpgrade")
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = $"{upgrades.description}" ; // Needs to be done with a better UI
                }
                
            }
            
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyUpgrade(upgrades);
            });
        }
        
    }

    

    public void BuyUpgrade(Perks upgrades)
    {
        ApplyUpgrade(upgrades);
    }

    private void ApplyUpgrade(Perks upgrades)
    {
        upgrades.perkLevel++;
        Invoke(upgrades.name + "Upgrade", 0f);
        //Temporarily
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void MaxHealthUpgrade()
    {
        decimal multiplier = playerManager.playerClass.maxHealth * constantMultiplier;
        player.GetComponent<Player>().setMaxHealth((int)Math.Round(multiplier));
    }

    private void SpeedUpgrade()
    {
        var speed = player.GetComponent<PlayerController>().moveSpeed;
        decimal multiplier = (decimal)speed * constantMultiplier;
        player.GetComponent<PlayerController>().SetMoveSpeed((int)Math.Round(multiplier));
        playerManager.playerClass.SetMoveSpeed(player.GetComponent<PlayerController>().moveSpeed);
    }

    // Update is called once per frame


    private void UpdateShop()
    {
        return;
    }
    private void OnGUI()
    {
        if (player != null)
        {
            levelText.text = "Level: " + playerManager.playerClass.GetLevel();
        }
    }

    private void Update()
    {
        if (playerManager.playerClass.hasLeveledUp())
        {
            shopUI.SetActive(true);
            Time.timeScale = 0;
        }
        
        if (player != null)
        {
            player = playerManager.GetCurrentPlayer();
        }
        
    }
    

}

[System.Serializable]
public class Perks
{
    public string name;
    public int cost;
    public Sprite image;
    [HideInInspector] public GameObject itemRef;
    [HideInInspector] public Object toUpgrade;
    public bool isUniquePerk;
    public string description;
    public int perkLevel;
}