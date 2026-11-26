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

    [Header("Movement")]
    [SerializeField]
    private float speed;
    private Vector2 playerPos;
    private Vector2 selfPos;
    [SerializeField]
    private float stopingDistance;
    [SerializeField]
    private float updateInterval = 0.3f;

    [Header("Other")]
    private Rigidbody2D rb;
    private GameObject player;
    private bool attacking;
    private bool coroutineStarted;
    private float distanceToPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
                //StopCoroutine(FollowingPlayerCoroutine());
                coroutineStarted = false;
                return;
            }

            if (distanceToPlayer > stopingDistance && coroutineStarted == false)
            {
                //StartCoroutine(FollowingPlayerCoroutine());
                coroutineStarted = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected");
            player = other.gameObject;
            DetectPlayer();


        }
    }

    private void DetectPlayer()
    {
        if (player != null)
        {
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            selfPos = new Vector2(transform.position.x, transform.position.y);

            //StartCoroutine(FollowingPlayerCoroutine());
        }
    }

    private void FollowPlayer()
    {
 
        if (distanceToPlayer > stopingDistance)
        {
            Vector2 direction = (playerPos - selfPos).normalized;
            rb.linearVelocity = direction * speed;
        }

    }

    /*private IEnumerator FollowingPlayerCoroutine()
    {
        var wait = new WaitForSeconds(updateInterval);

        while (true)
        {
            if (!attacking)
            {
                Vector2 toPlayer = (Vector2)player.position - rb.position;
                float distance = toPlayer.magnitude;

                // Girar hacia el player
                transform.localScale = new Vector3(toPlayer.x >= 0 ? 1 : -1, 1, 1);

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
    }*/








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