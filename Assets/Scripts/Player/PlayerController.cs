using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera cam;
    public PauseMenu pm;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }
    
    void Aim()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - rb.position).normalized;
        transform.up = direction;
    }

    public void SetMoveSpeed(int value)
    {
        moveSpeed = moveSpeed + value;
    }
}
