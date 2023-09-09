/* created by: SWT-P_SS_23_akin75 */
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
    /// <summary>
    /// Set the description text and update the UI
    /// </summary>
    /// <param name="description">Description to set</param>
    public void SetDescription(TextMeshProUGUI description)
    {
        descriptionText = description;
    }
    
    /// <summary>
    /// Set to the current upgrade
    /// </summary>
    /// <param name="upgrades">Upgrade to set</param>
    
    public void SetUpgrades(Upgrades upgrades)
    {
        this.upgrades = upgrades;
    }
}
