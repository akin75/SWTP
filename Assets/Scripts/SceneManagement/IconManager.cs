/* created by: SWT-P_SS_23_akin75 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class <c>IconManager</c> shows the Icon in the Game
/// </summary>
public class IconManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image prefabImage;
    private Image uiUse;
    [SerializeField] private Vector3 offset = new Vector3(0, 1);
    [SerializeField] private GameObject iconManager;
    
    void Start()
    {
        uiUse = Instantiate(prefabImage, iconManager.transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
