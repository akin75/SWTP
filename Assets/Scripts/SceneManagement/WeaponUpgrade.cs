using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public GameObject upgradeTextPrefab;
    private GameObject text;
    private Vector3 offset = new Vector3(0, -2.5f);
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI descriptionText;
    public GameObject shopUI;
    public List<Upgrades> upgradesList;
    public bool shopState = false;
    public GameObject shopItemPrefab;
    public Transform shopContent;
    private Weapon weapon;
    private PlayerSwitcher playerManager;
    public Sprite progressBar;
    private bool inRadius = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        text = Instantiate(upgradeTextPrefab, GameObject.Find("IconManager").transform);
        text.SetActive(false);
        weapon = player.GetComponentInChildren<Weapon>();
        Debug.Log($"Test: {weapon.GetLevel()}");

        foreach (Upgrades upgrades in upgradesList)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);
            upgrades.itemRef = item;
            var weaponArea = item.transform.GetChild(0);
            weaponArea.GetComponent<DescriptionHandler>().SetDescription(descriptionText);
            weaponArea.GetComponent<DescriptionHandler>().SetUpgrades(upgrades);
            foreach (Transform child in weaponArea.transform)
            {
                if (child.gameObject.name == "Level")
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = upgrades.name + " Level: " + upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel();
                }
                if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrades.image;
                }
                if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = "Cost: " + upgrades.cost ;
                }
            }
            
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyUpgrade(upgrades);
            });
            item.GetComponent<Button>().interactable = false;
        }
        
    }

    

    public void BuyUpgrade(Upgrades upgrades)
    {
        var playerComponent = player.GetComponent<Player>();
        if (playerComponent.GetCoins() >= upgrades.cost)
        {
            //Debug.Log("Test");
            playerComponent.setCurrency(-upgrades.cost);
            upgrades.weapon.GetComponentInChildren<Weapon>().AddLevel(1);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetDamage(10);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetTimeBetweenShots(0.005f);
            Debug.Log($"Weapon Level {upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel()}");
            ApplyUpgrade(upgrades);

        }
    }

    private void ApplyUpgrade(Upgrades upgrades)
    {
        decimal multiplier = (upgrades.cost + 1m)  * 1.10m;
        upgrades.cost = (int)Math.Round(multiplier);
        var child = upgrades.itemRef.transform.GetChild(0);
        var childProgressBar = child.transform.GetChild(3);
        child.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = upgrades.name + " Level: " + upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel();
        child.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Cost: " + upgrades.cost;
        foreach (Transform progressBar in childProgressBar.transform)
        {
            if (progressBar.gameObject.name == $"Bar lvl {upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel()}")
            {
                progressBar.gameObject.GetComponent<Image>().sprite = this.progressBar;
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            UpdateShop();
            text.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);
            inRadius = true;

        }
        
    }


    private void UpdateShop()
    {
        foreach (Upgrades upgrades in upgradesList)
        {
            if (playerManager.playerClass.GetLevel() >= upgrades.levelToBuy)
            {
                upgrades.itemRef.GetComponent<Button>().interactable = true;
            }
        }
    }
    private void OnGUI()
    {
        if (player != null)
        {
            coinText.text = " " + playerManager.playerClass.GetCurrency();
            levelText.text = "Level:  " + playerManager.playerClass.GetLevel();
        }
    }

    private void Update()
    {
        if (inRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!shopUI.activeSelf)
                {
                    shopUI.SetActive(true);
                    shopState = true;
                }
                else
                {
                    shopUI.SetActive(false);
                    shopState = false;
                }
                
                
            }
        }

        if (player != null)
        {
            player = playerManager.GetCurrentPlayer();
            weapon = player.GetComponentInChildren<Weapon>();
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
            shopUI.SetActive(false);
            shopState = false;
            inRadius = false;
        }
    }

   
}


[System.Serializable]
public class Upgrades
{
    public string name;
    public int cost;
    public Sprite image;
    [HideInInspector] public GameObject itemRef;
    public GameObject weapon;
    public int levelToBuy;
    public int quantity;
    public string description;
    
}
