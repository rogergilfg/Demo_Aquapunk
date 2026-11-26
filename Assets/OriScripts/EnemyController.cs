using UnityEngine;
using System.Collections;
using Unity.Hierarchy;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;
    private bool death;

    [Header("Movement")]
    [SerializeField]
    private float speed;
    private Vector2 playerPos;
    private Vector2 selfPos;
    [SerializeField]
    private float stopingDistance;
    [SerializeField]
    private float updateInterval = 0.3f;

    [Header("Combat")]
    [SerializeField] private float attackTime; //Duración de la animación de ataque
    [SerializeField] private GameObject attackCollider;

    [Header("Other")]
    private Rigidbody2D rb;
    private GameObject player;
    private bool playerDetected;
    private bool attacking;
    private bool coroutineStarted;
    private float distanceToPlayer;
    private LevelManager lM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lM = FindAnyObjectByType<LevelManager>();
        attackCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            return;
        }

        if(playerPos != null)
        {
            if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            distanceToPlayer = (selfPos - playerPos).magnitude;

            if (distanceToPlayer <= stopingDistance)
            {
                rb.linearVelocity = new Vector2(0, 0);
                StopCoroutine(FollowingPlayerCoroutine());
                coroutineStarted = false;

                if(lM.enemiesAttacking < lM.maxEnemiesAttacking && attacking == false)
                {
                    
                }

                return;
            }

            if (distanceToPlayer > stopingDistance && coroutineStarted == false)
            {
                StartCoroutine(FollowingPlayerCoroutine());
                coroutineStarted = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (death)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected");
            player = other.gameObject;
            DetectPlayer();
            playerDetected = true;
        }
    }

    private void DetectPlayer()
    {
        if (playerDetected == false)
        {
            StartCoroutine(FollowingPlayerCoroutine());
        }
    }

    /*private void FollowPlayer()
    {
        if (distanceToPlayer > stopingDistance)
        {
            Vector2 direction = (playerPos - selfPos).normalized;
            rb.linearVelocity = direction * speed;
        }
    }*/

    private IEnumerator FollowingPlayerCoroutine()
    {
        var wait = new WaitForSeconds(updateInterval);

        while (true)
        {
            if (!attacking)
            {
                playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
                selfPos = new Vector2(transform.position.x, transform.position.y);

                Vector2 toPlayer = (Vector2)player.transform.position - rb.position;
                float distance = toPlayer.magnitude; 

                if (distance <= stopingDistance)
                {
                    rb.linearVelocity = Vector2.zero;
                }
                else
                {
                    // Velocidad máxima para no pasarse de la distancia antes del próximo tick
                    float arriveSpeed = (distance - stopingDistance) / updateInterval;
                    rb.linearVelocity = toPlayer.normalized * Mathf.Min(speed, arriveSpeed);
                }
            }

            yield return wait;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        attacking = true;
        lM.enemiesAttacking += 1;
        AttackCollider(); //Se tendria que activar la animación
        yield return new WaitForSeconds(attackTime);
        AttackCollider(); //Se tendría que quitar cuando esté puesta la animación
        attacking = false;
        lM.enemiesAttacking -= 1;
    }

    public void AttackCollider() //Se tendría que llamar en Animation Event
    {
        if(attackCollider.activeSelf == false)
        {
            attackCollider.SetActive(true);
        }
        else
        {
            attackCollider.SetActive(false);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        if (currentHealth < 0)
        {
            //Animación de muelto
            Debug.Log("enemigo muelto");

        }
        else
        {
            //Animación de hit
            Debug.Log("enemigo golpeado");
        }
    }

    private void Die()
    {
        death = true;
        GetComponent<Collider2D>().isTrigger = true;
    }






    /* IEnumerator FollowingPlayerCoroutine()
     {
         while (true)
         {
             if (playerPos != null && attacking == false)
             {
                 selfPos = new Vector2(transform.position.x, transform.position.y);
                 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

                 FollowPlayer();
             }
             yield return new WaitForSeconds(0.3f);
         }
     }*/

}






























    //N <3