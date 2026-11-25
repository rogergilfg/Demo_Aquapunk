using UnityEngine;
using System.Collections;

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

    [Header("Other")]
    private Rigidbody2D rb;
    private GameObject player;
    private bool attacking;

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

            StartCoroutine(FollowingPlayerCoroutine());
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (playerPos - selfPos).normalized;
        rb.linearVelocity = direction * speed;
    }

    IEnumerator FollowingPlayerCoroutine()
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
    }

}






























    //N <3