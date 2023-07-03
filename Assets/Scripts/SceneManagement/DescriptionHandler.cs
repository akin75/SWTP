using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public bool isHovered = false;
    private TextMeshProUGUI descriptionText;
    private Upgrades upgrades;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText != null && upgrades != null)
        {
            descriptionText.text = upgrades.description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null && upgrades != null)
        {
            descriptionText.text = "";
        }
    }

    public void SetDescription(TextMeshProUGUI description)
    {
        descriptionText = description;
    }
    
    public void SetUpgrades(Upgrades upgrades)
    {
        this.upgrades = upgrades;
    }
}
