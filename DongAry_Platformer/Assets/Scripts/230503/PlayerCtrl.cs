using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GeneralAnimation
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StatSetting(100, 10, 7, 10);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Stats.MoveSpeed, rb.velocity.y, 0);
        if(Input.GetAxis("Horizontal") != 0)
        {
            StateUpdate(CharacterStates.Run);

            an.SetBool("run", true);
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateUpdate(CharacterStates.Jump);
            rb.velocity = Vector2.up * Stats.JumpForce;
            
        }
        if (Input.anyKey && rb.velocity == Vector2.zero)
        {
            StateUpdate(CharacterStates.Idle);
            
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            an.SetBool("jump", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            an.SetBool("jump", false);
        }
    }
}
