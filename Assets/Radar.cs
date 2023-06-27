using UnityEngine;

public class Radar : MonoBehaviour
{
    public float growthTime = 3f;

    private float currentRadius = 0f;
    private float targetRadius;
    private bool isGrowing = false;

    private void Start()
    {
        // Zielradius auf die Größe des Gameobjekts setzen
        targetRadius = transform.localScale.x;
    }

    private void Update()
    {
        if (isGrowing)
        {
            // Wachstum über die Zeit berechnen
            currentRadius += targetRadius / growthTime * Time.deltaTime;
            // Wenn der aktuelle Radius den Zielradius erreicht oder überschreitet, das Wachstum beenden
            if (currentRadius >= targetRadius)
            {
                currentRadius = targetRadius;
                isGrowing = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }

    public void StartGrowth()
    {
        isGrowing = true;
    }
}