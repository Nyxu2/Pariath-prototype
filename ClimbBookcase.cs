using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbBookcase : MonoBehaviour
{
    float vertical;
    float speed = 8f;
    bool bookcase;
    bool climbing;

    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (bookcase && Mathf.Abs(vertical) > 0f)
        {
            climbing = true;
        }
    }

    void FixedUpdate()
    {
        if (climbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bookcase"))
        {
            bookcase = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Bookcase"))
        {
            bookcase = false;
            climbing = false;
        }
    }
}
