using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemCollectSfx;
    public GameObject medikitCollectSfx;
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
                    default:
                        break;
                }
        }
    } 
}
