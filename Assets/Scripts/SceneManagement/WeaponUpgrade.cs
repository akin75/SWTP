/* created by: SWT-P_SS_23_akin75 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>WeaponUpgrade</c> defines the functionality of the Weapon Shop. Player can apply upgrade to its weapon.
/// </summary>
public class WeaponUpgrade : MonoBehaviour
{
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
    private static WeaponUpgrade _instance;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        text = Instantiate(upgradeTextPrefab, GameObject.Find("IconManager").transform);
        text.SetActive(false);
        weapon = player.GetComponentInChildren<Weapon>();
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

    
    /// <summary>
    /// In the UI shop player can buy an upgrade, depending on which weapon they choose the weapon will be upgraded.
    /// </summary>
    /// <param name="upgrades">The weapon to upgrade</param>

    public void BuyUpgrade(Upgrades upgrades)
    {
        var playerComponent = player.GetComponent<Player>();
        if (playerComponent.GetCoins() >= upgrades.cost)
        {
            playerComponent.setCurrency(-upgrades.cost);
            upgrades.weapon.GetComponentInChildren<Weapon>().AddLevel(1);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetDamage(10);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetTimeBetweenShots(0.005f);
            ApplyUpgrade(upgrades);

        }
    }
    
    /// <summary>
    /// Apply the upgrade to the chosen upgrade. Update the UI Shop
    /// </summary>
    /// <param name="upgrades">The chosen upgrade</param>

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

    /// <summary>
    /// Update the shop if Player interacts with the shop
    /// </summary>

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
