using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        playerMovement.RotateTowardsMouse(inputHandler.MousePosition);

    }
    private void FixedUpdate()
    {
        playerMovement.Move(inputHandler.MoveInput);
    }

   
}
