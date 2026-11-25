using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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

    private float moveInputX;
    private float moveInputY;

    [Header("Attack")]
    [SerializeField]
    private GameObject attackCollider; //Provisional

    [Header("Other")]
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
        if (moveInputX > 0)
        { 
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }



    //METODOS DE INPUTS
    public void OnMove(InputValue value)
    {
        moveInputX = value.Get<Vector2>().x;
        moveInputY = value.Get<Vector2>().y;
    }





    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Attack");
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        attackCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f); // Duración del ataque
        attackCollider.SetActive(false);
    }
}




























