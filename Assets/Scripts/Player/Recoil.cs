using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private GameObject lArmU;
    private GameObject lArmL;
    private GameObject rArmL;
    private GameObject rArmU;
    public GameObject weapon1;
    public GameObject weapon2;
    public float recoilDistance = 5;
    public float recoilDuration = 0.05f;
    
    private Vector3 lArmUOriginalPosition;
    private Vector3 lArmLOriginalPosition;
    private Vector3 rArmLOriginalPosition;
    private Vector3 rArmUOriginalPosition;
    private Vector3 weapon1OriginalPosition;
    private Vector3 weapon2OriginalPosition;

    void Start()
    {
        // Definiere die privaten Gameobjects
        lArmU = transform.Find("LeftArmUpper").gameObject;
        lArmL = transform.Find("LeftArmLower").gameObject;
        rArmL = transform.Find("RightArmLower").gameObject;
        rArmU = transform.Find("RightArmUpper").gameObject;

        // Speichere die ursprünglichen Positionen der GameObjects
        lArmUOriginalPosition = lArmU.transform.localPosition;
        lArmLOriginalPosition = lArmL.transform.localPosition;
        rArmLOriginalPosition = rArmL.transform.localPosition;
        rArmUOriginalPosition = rArmU.transform.localPosition;

        // Überprüfe, ob weapon1 vorhanden ist und speichere die ursprüngliche Position
        if (weapon1 != null)
            weapon1OriginalPosition = weapon1.transform.localPosition;

        // Überprüfe, ob weapon2 vorhanden ist und speichere die ursprüngliche Position
        if (weapon2 != null)
            weapon2OriginalPosition = weapon2.transform.localPosition;
    }

    
    private IEnumerator RecoilCoroutine()
    {
        Debug.Log("Start Coroutine");
        //float elapsedTime = 0f;
        //Vector3 recoilOffset = Vector3.back * recoilDistance;
        Vector3 recoilDirection = new Vector3(0, -1f, 0).normalized / 100;
        Vector3 recoilOffset = recoilDirection * recoilDistance;
        
        // Verschiebe die Gameobjects um recoilOffset nach hinten
        lArmU.transform.localPosition = lArmUOriginalPosition + recoilOffset;
        lArmL.transform.localPosition = lArmLOriginalPosition + recoilOffset + new Vector3(0,-0.02f,0);
        rArmL.transform.localPosition = rArmLOriginalPosition + recoilOffset + new Vector3(0,-0.02f,0);
        rArmU.transform.localPosition = rArmUOriginalPosition + recoilOffset;

        // Überprüfe, ob weapon1 vorhanden ist und verschiebe es ebenfalls
        if (weapon1 != null)
            weapon1.transform.localPosition = weapon1OriginalPosition + recoilOffset;
        
        // Überprüfe, ob weapon2 vorhanden ist und verschiebe es ebenfalls
        if (weapon2 != null)
            weapon2.transform.localPosition = weapon2OriginalPosition + recoilOffset;

        yield return new WaitForSeconds(recoilDuration);

        // Zurücksetzen der Position der Gameobjects auf ihre ursprünglichen Positionen
        lArmU.transform.localPosition = lArmUOriginalPosition;
        lArmL.transform.localPosition = lArmLOriginalPosition;
        rArmL.transform.localPosition = rArmLOriginalPosition;
        rArmU.transform.localPosition = rArmUOriginalPosition;

        // Überprüfe, ob weapon1 vorhanden ist und setze die Position zurück
        if (weapon1 != null)
            weapon1.transform.localPosition = weapon1OriginalPosition;

        // Überprüfe, ob weapon2 vorhanden ist und setze die Position zurück
        if (weapon2 != null)
            weapon2.transform.localPosition = weapon2OriginalPosition;
    }
    
    public void StartRecoil()
    {
        StartCoroutine("RecoilCoroutine");
    }
}
