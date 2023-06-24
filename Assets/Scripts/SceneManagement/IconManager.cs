using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IconManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image prefabImage;
    private Image uiUse;
    public Vector3 offset = new Vector3(0, 1);
    public GameObject iconManager;
    
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
