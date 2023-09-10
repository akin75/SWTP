/* created by: SWT-P_SS_23_akin75 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// Class <c>PerkUpgrade</c> defines the functionality of the Perk in game.
/// </summary>
public class PerkUpgrade : MonoBehaviour
{
    private GameObject player;
     private GameObject text;
    private Vector3 offset = new Vector3(0, -2.5f);
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private List<Perks> upgradesList; 
    public bool shopState = false;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform shopContent;
    private Weapon weapon;
    private PlayerSwitcher playerManager;
    private decimal constantMultiplier = 0.2m;
    private static PerkUpgrade _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }


    void Start()
    {
        playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        player = playerManager.GetCurrentPlayer();
        weapon = player.GetComponentInChildren<Weapon>();

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

    
    /// <summary>
    /// In the UI Perk upgrade player can choose an upgrade, depending on which perk they choose the status will be added to the player.
    /// </summary>
    /// <param name="upgrades">The perk to upgrade</param>

    public void BuyUpgrade(Perks upgrades)
    {
        ApplyUpgrade(upgrades);
    }

    /// <summary>
    /// Apply the upgrade to the chosen perk. Update the UI Perk upgrade
    /// </summary>
    /// <param name="upgrades">The chosen perk</param>
    private void ApplyUpgrade(Perks upgrades)
    {
        upgrades.perkLevel++;
        Invoke(upgrades.name + "Upgrade", 0f);
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Add max health to the player upon choosing this Upgrade
    /// </summary>
    private void MaxHealthUpgrade()
    {
        decimal multiplier = playerManager.playerClass.maxHealth * constantMultiplier;
        player.GetComponent<Player>().setMaxHealth((int)Math.Round(multiplier));
    }

    /// <summary>
    /// Add speed to the player upon choosing this Upgrade
    /// </summary>
    private void SpeedUpgrade()
    {
        var speed = player.GetComponent<PlayerController>().moveSpeed;
        decimal multiplier = (decimal)speed * constantMultiplier;
        player.GetComponent<PlayerController>().SetMoveSpeed((int)Math.Round(multiplier));
        playerManager.playerClass.SetMoveSpeed(player.GetComponent<PlayerController>().moveSpeed);
    }

    /// <summary>
    /// Add critical chance to the player upon choosing this Upgrade
    /// </summary>
    private void CriticalChanceUpgrade()
    {
        PlayerClass instance = playerManager.playerClass;
        instance.SetCritChance(instance.GetCritChance() + 0.05f);
    }

    /// <summary>
    /// Add critical damage to the player upon choosing this Upgrade
    /// </summary>
    private void CriticalDamageUpgrade()
    {
        PlayerClass instance = playerManager.playerClass;
        instance.SetCritDamage(instance.GetCritDamage() + 0.2f);
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