using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemCollectSfx;
    public GameObject medikitCollectSfx;
    private AchievementManager _achievementManager;
    public enum ItemType
    {
        coin,
        heal,
        specialA,
        specialB
    };
    public ItemType itemType;
    public int value;

    private void Start()
    {
        _achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            Debug.Log("item collected");
            Destroy(gameObject);
            switch (itemType)
            {
                case ItemType.coin:
                    GameObject itemCollectSfx = Instantiate(this.itemCollectSfx, transform.position, Quaternion.identity);
                    trigger.gameObject.GetComponent<Player>().setCurrency(value);
                    break;
                case ItemType.heal:
                    GameObject medikitCollectSfx = Instantiate(this.medikitCollectSfx, transform.position, Quaternion.identity);
                    trigger.gameObject.GetComponent<Player>().heal(value);
                    break;
                case ItemType.specialA:
                    GameObject specialCollectSfxA = Instantiate(this.itemCollectSfx, transform.position, Quaternion.identity);
                    _achievementManager.SpecialItemPickedUp("A");
                    break;
                case ItemType.specialB:
                    GameObject specialCollectSfxB = Instantiate(this.itemCollectSfx, transform.position, Quaternion.identity);
                    _achievementManager.SpecialItemPickedUp("B");
                    break;
                default:
                    break;
            }
        }
    } 
}
