using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Test_PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    public bool canMove; //añadir al PlayerController real

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;      // vista top-down, sin gravedad
        rb.freezeRotation = true;  // evita que gire al chocar
        canMove = true;
    }

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        moveInput = new Vector2(
            (kb.dKey.isPressed ? 1 : 0) - (kb.aKey.isPressed ? 1 : 0),
            (kb.wKey.isPressed ? 1 : 0) - (kb.sKey.isPressed ? 1 : 0)
        ).normalized; // normalizado para que la diagonal no sea más rápida
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = moveInput * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
