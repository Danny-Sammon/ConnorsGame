using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    private SpriteRenderer sp;
    //private Scene scene;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpGround;
    

    private float dirX = 0f;
    [SerializeField] private float speed = 7f;
    private float Jumpforce = 8f;

    private enum movement { idle, right, jump, fall };
    


    void Start()
    {
        //scene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
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

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }

    private void Animations()
    {
        movement state;
        if (dirX > 0f)
        {
            state = movement.right;
            sp.flipX = false;
        }
        else if (dirX < 0)
        {
            state = movement.right;
            sp.flipX = true;
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
            state = movement.fall;
        }
         anim.SetInteger("state", (int) state);
    }
   
}
