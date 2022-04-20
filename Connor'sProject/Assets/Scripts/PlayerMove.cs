using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private LayerMask jumpGround;
    private BoxCollider2D coll;
    private float Jumpforce = 4f;
    public int points = 0;
    private enum movement { idle, right, jump };

    private void Start()
    {
        Debug.Log("Tis Working");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }


    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpforce);
        }

        Animations();
    }
    private void Animations()
        {
            movement state;
            if (dirX > 0f)
            {
                state = movement.right;
                sprite.flipX = false;
            }
            else if (dirX < 0)
            {
                state = movement.right;
                sprite.flipX = true;
            }
            else
            {
                state = movement.idle;
            }
            if (rb.velocity.y > 0.1f)
            {
                state = movement.jump;
            }
            else if (rb.velocity.y < -0.1f)
            {
                state = movement.idle;
            }

            anim.SetInteger("state", (int)state);
        }
}
