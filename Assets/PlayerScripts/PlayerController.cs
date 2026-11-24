using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentAq;    //Aq = mana, lo he puesto por aquatista
    [SerializeField]
    private float maxAq;
    

    [Header("Movement")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private float moveInput;
    private bool isGrounded;

    [Header("Components")]
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //player mira a donde camina
        if (moveInput > 0)
        { 
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }


    //Inputs metodos
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }



    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }


}
