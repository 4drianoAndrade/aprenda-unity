using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    public Transform groundCheck;

    private bool isGrounded;

    private int speedX;
    private float speedY;

    public float speed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            speedX = 1;
        }
        else
        {
            speedX = 0;
        }

        speedY = playerRb.velocity.y;
        playerRb.velocity = new Vector2(horizontal * speed, speedY);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void LateUpdate()
    {
        playerAnimator.SetInteger("speedX", speedX);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }
}
