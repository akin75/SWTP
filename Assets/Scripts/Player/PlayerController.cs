using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Camera cam;
    public PauseMenu pm;
    private AudioSource stepSfx;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        stepSfx = gameObject.GetComponent<AudioSource>();
        Debug.Log(stepSfx);
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
            stepSfx.mute = true;
        }
        else
        {
            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            rb.velocity = direction * moveSpeed;
            //stepSfx.loop = true;
            stepSfx.mute = false;
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
