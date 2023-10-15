using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask bossLayer;

    float speed = 5f;
    float moveHorizontal;
    float jumpPower = 8f;
    float attackRange = 1.5f;
    int attackDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        //Jump higher when pressing jump button longer & jump lower by tapping jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, bossLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Boss boss = enemy.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);
            }
        }
    }

    void FixedUpdate() => rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

    bool Grounded() => Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
}
