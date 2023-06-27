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
    public GameObject shopUI;
    public List<Upgrades> upgradesList;
    public bool shopState = false;
    public GameObject shopItemPrefab;
    public Transform shopContent;
    private Weapon weapon;
    private PlayerSwitcher playerManager;
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
            foreach (Transform child in item.transform)
            {
                if (child.gameObject.name == "LevelText")
                {
                    child.gameObject.GetComponent<Text>().text = upgrades.name + " Level: " + upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel();
                }
                if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrades.image;
                }
                if (child.gameObject.name == "LevelTo")
                {
                    child.gameObject.GetComponent<Text>().text = "Level      " + $"{upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel()} -->  {(upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel() + 1)}";
                }
                if (child.gameObject.name == "DamageTo")
                {
                    child.gameObject.GetComponent<Text>().text += upgrades.weapon.GetComponentInChildren<Weapon>().damage + "  -->  " + (upgrades.weapon.GetComponentInChildren<Weapon>().damage + 10) ;
                }
                if (child.gameObject.name == "FireRateTo")
                {
                    child.gameObject.GetComponent<Text>().text += upgrades.weapon.GetComponentInChildren<Weapon>().timeBetweenShots + "  -->  " + (upgrades.weapon.GetComponentInChildren<Weapon>().timeBetweenShots - 0.005f);
                }
                if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = "Cost: " + upgrades.cost ;
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
            Debug.Log("Test");
            playerComponent.setCurrency(-upgrades.cost);
            upgrades.weapon.GetComponentInChildren<Weapon>().AddLevel(1);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetDamage(10);
            upgrades.weapon.GetComponentInChildren<Weapon>().SetTimeBetweenShots(0.005f);
            Debug.Log($"Weapon Upgrades: Damage {upgrades.weapon.GetComponentInChildren<Weapon>().damage}  Level {upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel()}");
            ApplyUpgrade(upgrades);

        }
    }

    private void ApplyUpgrade(Upgrades upgrades)
    {
        decimal multiplier = (upgrades.cost + 1m)  * 1.10m;
        upgrades.cost = (int)Math.Round(multiplier);
        upgrades.itemRef.transform.GetChild(4).GetComponent<Text>().text = upgrades.name + " Level: " + upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel();
        upgrades.itemRef.transform.GetChild(1).GetComponent<Text>().text = "Level      " + $"{upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel()} -->  {(upgrades.weapon.GetComponentInChildren<Weapon>().GetLevel() + 1)}";
        upgrades.itemRef.transform.GetChild(2).GetComponent<Text>().text = "Damage      " + upgrades.weapon.GetComponentInChildren<Weapon>().damage + "  -->  " + (upgrades.weapon.GetComponentInChildren<Weapon>().damage + 10) ;
        upgrades.itemRef.transform.GetChild(3).GetComponent<Text>().text = "Fire Rate      " + upgrades.weapon.GetComponentInChildren<Weapon>().timeBetweenShots + "  -->  " + (upgrades.weapon.GetComponentInChildren<Weapon>().timeBetweenShots - 0.005f); ;
        upgrades.itemRef.transform.GetChild(5).GetComponent<Text>().text = "Cost: " + upgrades.cost;
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
        foreach (Upgrades upgrades in upgradesList)
        {
            if (playerManager.playerClass.GetLevel() >= upgrades.levelToBuy)
            {
                upgrades.itemRef.GetComponent<Button>().interactable = true;
                upgrades.itemRef.transform.GetChild(6).GetComponent<Image>().color = new Color(46, 32, 32, 0);
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
            
            Debug.Log($"Weapon {weapon.gameObject.name}");
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
}
