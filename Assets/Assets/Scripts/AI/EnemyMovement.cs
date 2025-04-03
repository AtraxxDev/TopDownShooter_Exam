using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed)
    {
        moveSpeed = speed;
    }

    public void Move(Vector3 direction)
    {
        rb.linearVelocity = direction * moveSpeed;  // Movimiento normal
        transform.forward = direction;
      
    }

  
}
