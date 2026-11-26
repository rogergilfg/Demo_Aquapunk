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
    public bool canMove;
    

    [Header("Movement")]
    [SerializeField]
    private float speed;

    private float moveInputX;
    private float moveInputY;

    [Header("Attack")]
    [SerializeField]
    private GameObject attackCollider; //Provisional
    private int combo;
    private bool canAttack;

    [Header("Other")]
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
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
        if (canMove == true)
        {
            rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);
        }
        else
        { 
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }



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
            
            if (canAttack == true)
            {
                //other.GetComponent<EnemyController>().TakeDamage();
                StartCoroutine(AttackCoroutine());
                combo++;

                switch (combo)
                {
                  
                }
            }

        }
    }



    IEnumerator AttackCoroutine()
    {
        canAttack = false;
        attackCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f); 
        attackCollider.SetActive(false);
        canAttack = true;
    }
}




























