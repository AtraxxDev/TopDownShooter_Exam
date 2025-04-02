using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 inputDirection)
    {
        Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        rb.linearVelocity = moveDirection * moveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
    }

    public void RotateTowardsMouse(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 lookPoint = new Vector3(hitInfo.point.x, rb.transform.position.y, hitInfo.point.z);
            rb.transform.LookAt(lookPoint);
        }
    }


}
