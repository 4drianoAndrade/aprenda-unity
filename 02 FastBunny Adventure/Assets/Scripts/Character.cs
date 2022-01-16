using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    //private SpriteRenderer playerSr;

    public Transform groundCheck;

    private bool isGrounded;

    private int speedX;
    private float speedY;

    public float speed;
    public float jumpForce;
    public int extraJumps;
    private int exJump;

    public bool isLeft;

    public LayerMask whatIsGround;

    public GameObject carrotProjectilePrefab;

    public float shotForceBase;
    private float shotForce;
    public Transform weaponPosition;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        //playerSr = GetComponent<SpriteRenderer>();

        exJump = extraJumps;

        shotForce = shotForceBase;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
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

        if (isLeft == true && horizontal > 0)
        {
            flip();
        }

        if (isLeft == false && horizontal < 0)
        {
            flip();
        }

        speedY = playerRb.velocity.y;
        playerRb.velocity = new Vector2(horizontal * speed, speedY);

        if (isGrounded == true)
        {
            exJump = extraJumps;
        }

        if (Input.GetButtonDown("Jump") && exJump > 0)
        {
            jump();
            exJump--;
        }
        else if (Input.GetButtonDown("Jump") && exJump == 0 && isGrounded == true)
        {
            jump();
        }

        if (Input.GetButtonDown("Fire1") && _GameController.ammunition > 0)
        {
            shootCarrot();
        }
    }

    void LateUpdate()
    {
        playerAnimator.SetInteger("speedX", speedX);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    public void jump()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
        playerRb.AddForce(new Vector2(0, jumpForce));
    }

    // FUNÇÃO RESPONSÁVEL POR VIRAR A DIREÇÃO QUE O PERSONAGEM ESTÁ OLHANDO
    void flip()
    {
        isLeft = !isLeft;

        //playerSr.flipX = isLeft;

        float x = transform.localScale.x;
        x *= -1; // INVERTE O SINAL DO SCALE
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        shotForce *= -1;
    }

    void shootCarrot()
    {
        _GameController.manageAmmo(-1);

        GameObject temp = Instantiate(carrotProjectilePrefab);
        temp.transform.position = weaponPosition.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shotForce, 0);

        Destroy(temp, 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "collectable":

                Destroy(collision.gameObject);
                _GameController.manageAmmo(1);
                break;
        }
    }
}
