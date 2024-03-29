using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class characterController2 : MonoBehaviour
{
    public float jumpForce = 150.0f;
    public float speed = 1.0f;
    private float moveDirection;

    private bool jump;
    private bool grounded = true;
    private bool moving;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>(); //caching animator
    }
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (rigidbody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        rigidbody2D.velocity = new Vector2(x:speed * moveDirection, rigidbody2D.velocity.y);
        if (jump == true) 
        { 
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, y:jumpForce);
            jump = false;
        }
}

    private void Update()
{
        if (grounded == true && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                spriteRenderer.flipX = true;
                anim.SetFloat(name:"speed", speed);
            } else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                spriteRenderer.flipX = false;
                anim.SetFloat(name: "speed", speed);
            }
        } else if (grounded == true)
        {
            moveDirection = 0.0f;
            anim.SetFloat(name: "speed", value:0.0f);
        }
        if (grounded==true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Zemin"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
        
    }
}

