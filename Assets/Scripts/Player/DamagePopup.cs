using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMesh;
    private int _displayedDamage;
    public float moveYSpeed = 20;
    private float _disappearTimer;
    private float _disappearSpeed;
    private Color _textColor;

    // Neue Variablen für kritische Treffer
    [SerializeField] private Color critColor = Color.red;
    [SerializeField] private Vector3 critScale = new Vector3(1.5f, 1.5f, 1.5f);
    
    public void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        _disappearTimer -= Time.deltaTime; // Abnahme des Timers

        if (_disappearTimer <= 0)
        {
            _disappearSpeed = 3f;
            _textColor.a -= _disappearSpeed * Time.deltaTime; // Abnahme der Alpha-Komponente
            _textMesh.color = _textColor;

            if (_textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
/// <summary>
/// sets up the damage popup. handles the size and the color
/// </summary>
/// <param name="damage"></param>
/// <param name="isCrit"></param>
    public void Setup(int damage, bool isCrit)
    {
        _displayedDamage = damage;
        _textMesh.SetText(_displayedDamage.ToString());

        if (isCrit)
        {
            _textMesh.color = critColor;
            _textMesh.transform.localScale = critScale;
        }

        _textColor = _textMesh.color;
        _disappearTimer = 0.2f;
    }
}