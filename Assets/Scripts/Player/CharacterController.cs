using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireForce = 40f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Camera cam;
    public PauseMenu pm;
    public float timeBetweenShots = 0.02f;
    public float maxDeviation = 10f;
    public static int damage = 20;
    private float timeSinceLastShot = 0f;
    
    private Rigidbody2D rb;
    private float nextFireTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Aim();
        Move();
        
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && timeSinceLastShot >= timeBetweenShots)
        {
            Shoot(); 
            timeSinceLastShot = 0f;

        } 
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }

    void Aim()
    {
       // if(!pm.gameIsPaused){
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - rb.position).normalized;
        transform.up = direction;
       // }
    }

    void Move()
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

    void Shoot()
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        nextFireTime = Time.time + timeBetweenShots;

        float deviationAngle = Random.Range(-maxDeviation, maxDeviation);
        Vector2 bulletDirection = Quaternion.Euler(0f, 0f, deviationAngle) * transform.up;

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.transform.right = bulletDirection;
        newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * fireForce, ForceMode2D.Impulse);
    }
}
