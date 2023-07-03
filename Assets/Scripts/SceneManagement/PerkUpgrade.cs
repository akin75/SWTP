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
    public GameObject upgradeTextPrefab;
    private GameObject text;
    private Vector3 offset = new Vector3(0, -2.5f);
    public TextMeshProUGUI coinText;
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
        text = Instantiate(upgradeTextPrefab, GameObject.Find("IconManager").transform);
        text.SetActive(false);
        weapon = player.GetComponentInChildren<Weapon>();
        //Debug.Log($"Test: {weapon.GetLevel()}");

        foreach (Perks upgrades in upgradesList)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);
            upgrades.itemRef = item;
            foreach (Transform child in item.transform)
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
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = $"{upgrades.name}      " + $"{upgrades.perkLevel}  -->  {upgrades.perkLevel + 1}" ; // Needs to be done with a better UI
                }
                if (child.gameObject.name == "PerkCost")
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

    

    public void BuyUpgrade(Perks upgrades)
    {
        var playerComponent = player.GetComponent<Player>();
        if (playerComponent.GetCoins() >= upgrades.cost)
        {
            Debug.Log("Test");
            playerComponent.setCurrency(-upgrades.cost);
            ApplyUpgrade(upgrades);

        }
    }

    private void ApplyUpgrade(Perks upgrades)
    {
        decimal multiplier = (upgrades.cost + 1m)  * 1.10m;
        upgrades.cost = (int)Math.Round(multiplier);
        upgrades.perkLevel++;
        upgrades.itemRef.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{upgrades.name}      " + $"{upgrades.perkLevel}  -->  {upgrades.perkLevel + 1}" ;
        upgrades.itemRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Cost:   " + $"{upgrades.cost}" ;
        Invoke(upgrades.name + "Upgrade", 0f);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            text.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);
            shopState = true;
            UpdateShop();
        }
        
    }


    private void UpdateShop()
    {
        foreach (Perks upgrades in upgradesList)
        {
            if (playerManager.playerClass.GetLevel() >= upgrades.levelToBuy)
            {
                upgrades.itemRef.GetComponent<Button>().interactable = true;
                upgrades.itemRef.transform.GetChild(1).GetComponent<Image>().color = new Color(46, 32, 32, 0.4f);
            }
        }
    }
    private void OnGUI()
    {
        if (player != null)
        {
            coinText.text = "Coins: " + player.GetComponent<Player>().GetCoins();
            levelText.text = "Level: " + playerManager.playerClass.GetLevel();
        }
    }

    private void Update()
    {
        if (shopState && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(true);
            
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
    public int levelToBuy;
    public int perkLevel;
}