using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    
    public Camera cam;

    
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        
        
        // Raycast (fehlerhaft: Ray nur bis zur Maus)
        /*
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Enemy") && Input.GetMouseButton(0))
        {
            Debug.Log("hit");
            Destroy(hit.collider.gameObject);
        }
            */
    }
        


    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
