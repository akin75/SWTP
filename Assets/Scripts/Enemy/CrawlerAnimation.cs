using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Referenz auf den SpriteRenderer des GameObjects

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer-Komponente abrufen
        InvokeRepeating("FlipSprite", 1f, 0.2f); // Starte die Flipp-Funktion mit einer Wiederholungsrate von 1 Sekunde
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX; // Flipping des Sprites in horizontaler Richtung (X-Achse)
    }
}