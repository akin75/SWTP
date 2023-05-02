using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D rb;

    public void Aim(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }
}