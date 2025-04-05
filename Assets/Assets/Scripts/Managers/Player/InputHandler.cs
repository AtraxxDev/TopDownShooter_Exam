using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosition { get; private set; }

    public bool isFiring { get; private set; }
    public bool isFiringMissile { get; private set; }  // Nueva propiedad para los misiles

    private Player_Actions playerActions;

    private void Awake()
    {
        playerActions = new Player_Actions();
        playerActions.Player.Enable();

        playerActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        playerActions.Player.Look.performed += ctx => MousePosition = ctx.ReadValue<Vector2>();

        playerActions.Player.FireBullets.performed += ctz => isFiring = true;
        playerActions.Player.FireBullets.canceled += ctz => isFiring = false;

        playerActions.Player.FireMissile.performed += ctz => isFiringMissile = true;  // Nueva acción para disparar misiles
        playerActions.Player.FireMissile.canceled += ctz => isFiringMissile = false; // Cuando se suelta la tecla
    }
}
