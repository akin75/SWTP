/* created by: SWT-P_SS_23_akin75 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>ObjectUpgrade</c> defines the functionality of object shop in the game
/// </summary>
public class ObjectUpgrade : MonoBehaviour
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
    private static ObjectUpgrade _instance;
    [SerializeField] private Sprite noEXPBar;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        text = Instantiate(upgradeTextPrefab, GameObject.Find("IconManager").transform);
        text.SetActive(false);
        foreach (Upgrades upgrades in upgradesList)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);
            upgrades.itemRef = item;
            var descriptionArea = item.transform.GetChild(0);
            descriptionArea.GetComponent<DescriptionHandler>().SetDescription(descriptionText);
            descriptionArea.GetComponent<DescriptionHandler>().SetUpgrades(upgrades);
            foreach (Transform child in descriptionArea.transform)
            {
                if (child.gameObject.name == "Name")
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = upgrades.name;
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
        }
        
    }

    

    /// <summary>
    /// In the UI shop player can buy an object, depending on which object they choose the object will be added.
    /// </summary>
    /// <param name="upgrades">The object to buy</param>

    public void BuyUpgrade(Upgrades upgrades)
    {
        var playerComponent = player.GetComponent<Player>();
        if (playerComponent.GetCoins() >= upgrades.cost && upgrades.quantity < 5)
        {
            playerComponent.setCurrency(-upgrades.cost);
            
            if (upgrades.weapon.gameObject.name == "Bait")
            {
                playerManager.GetComponent<ItemThrow>().baitCount++;
            }
            else if (upgrades.weapon.gameObject.name == "Mine")
            {
                playerManager.GetComponent<ItemThrow>().mineCount++;
            }
            upgrades.quantity++;
            ApplyUpgrade(upgrades);

        }
    }
    
    /// <summary>
    /// Apply the item to the chosen object. Update the UI Shop
    /// </summary>
    /// <param name="upgrades">The chosen object</param>

    private void ApplyUpgrade(Upgrades upgrades)
    {
        decimal multiplier = (upgrades.cost + 1m)  * 1.10m;
        upgrades.cost = (int)Math.Round(multiplier);
        var child = upgrades.itemRef.transform.GetChild(0);
        var childProgressBar = child.transform.GetChild(4);
        child.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Cost: " + upgrades.cost;
        if (upgrades.quantity <= 5)
        {
            childProgressBar.GetChild(upgrades.quantity).GetComponent<Image>().sprite = this.progressBar;
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
    /// Update the UI Shop if the Player interact with the shop
    /// </summary>
    private void UpdateShop()
    {
        foreach (Upgrades upgrades in upgradesList)
        {
            if (upgrades.weapon.gameObject.name == "Bait")
            {
                upgrades.quantity = playerManager.GetComponent<ItemThrow>().baitCount;
            } else if (upgrades.weapon.gameObject.name == "Mine" )
            {
                upgrades.quantity = playerManager.GetComponent<ItemThrow>().mineCount;
            }
            
            var area = upgrades.itemRef.transform.GetChild(0);
            var progress = area.transform.GetChild(4);
            var counter = 0;
            for (int i = 1; i < progress.childCount; i++)
            {
                if (progress.GetChild(i).GetComponent<Image>().sprite.name == "BAR Point 1" &&
                    i > upgrades.quantity)
                {
                    progress.GetChild(i).GetComponent<Image>().sprite = this.noEXPBar;
                }
                    
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

