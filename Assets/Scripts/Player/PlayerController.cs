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
        stepSfx = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCameraObject != null)
        {
            cam = mainCameraObject.GetComponent<Camera>();
        }

        Move();
        Aim(cam);
    }

    /// <summary>
    /// handles player movement
    /// </summary>
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
            stepSfx.mute = false;
        }
    }
    
    /// <summary>
    /// aims when mouse is being moved
    /// </summary>
    /// <param name="camInput">mouse movement</param>
    void Aim(Camera camInput)
    {
        if(!pm.IsPaused() && cam != null){
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - rb.position).normalized;
            transform.up = direction;
        }
    }

    /// <summary>
    /// sets move speed of player 
    /// </summary>
    /// <param name="value">move speed</param>
    public void SetMoveSpeed(int value)
    {
        moveSpeed = moveSpeed + value;
    }
}
