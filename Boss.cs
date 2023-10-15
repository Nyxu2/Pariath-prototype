using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;
    bool playerInRoom = false;
    
    [SerializeField] Rigidbody2D bossrb;
    [SerializeField] Transform playerT;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float knockBack = 50f;
    [SerializeField] float attackRange = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bossrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRoom)
        {
            Move();

            Attack(GameObject.FindWithTag("Player"));
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(GameObject Player)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, attackRange);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Rigidbody2D playerrb = Player.GetComponent<Rigidbody2D>();
            if (playerrb != null)
            {
                Vector3 knockbackDirection = Player.transform.position - transform.position;
                knockbackDirection.Normalize();
                playerrb.AddForce(-knockbackDirection * knockBack);
            }
        }
    }

    void Move()
    {
        Vector2 directionToPlayer = (playerT.position - transform.position).normalized;
        bossrb.velocity = directionToPlayer * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            playerInRoom = true;
            playerT = c.transform;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
       if (c.CompareTag("Player"))
        {
            playerInRoom = false;
            playerT = null;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
