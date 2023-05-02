using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Weapon weapon;
    public Camera cam;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput != Vector2.zero)
        {
            rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.deltaTime);
        }

        weapon.transform.position = rb.transform.position;
        // Richte das Waffenobjekt auf den Mauszeiger aus
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        weapon.Aim(mousePosition);
        
        Debug.Log("Mausposition" + mousePosition);
    }
}