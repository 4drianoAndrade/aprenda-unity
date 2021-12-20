using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour
{
    private GameController _GameController;

    [Header("Config. Player")]

    public float forceJump;
    private Rigidbody2D rbPlayer;
    public Transform groundCheck;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rbPlayer.AddForce(new Vector2(0, forceJump));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "collectable":
                _GameController.toScore(10);
                Destroy(collision.gameObject);
                break;

            case "obstacle":
                _GameController.changeScene("GameOver");
                break;
        }
    }
}
