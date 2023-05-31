using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public enum ItemType
    {
        coin,
        heal
    };
    public ItemType itemType;
    public int value;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
                Destroy(gameObject);
                switch (itemType)
                {
                    case ItemType.coin:
                        trigger.gameObject.GetComponent<Player>().setCurrency(value);
                        break;
                    case ItemType.heal:
                        trigger.gameObject.GetComponent<Player>().TakeDamage(-value);
                        break;
                    default:
                        break;
                }
        }
    } 
}
