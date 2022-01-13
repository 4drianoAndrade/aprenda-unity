/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actions
{
    JUMP
}

public class BunnyController : MonoBehaviour
{
    private GameController _GameController;
    private GameObject[] _gameObject;
    private Animator playerAnimator;

    [Header("Config. Player")]

    public float forceJump;
    private Rigidbody2D rbPlayer;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    public Transform groundCheck;
    private bool isGrounded;
    private MoveOffset[] movement;
    private bool oneJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        rbPlayer = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = _GameController.moves;
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rbPlayer.AddForce(new Vector2(0, forceJump));
            _GameController.sfxSource.PlayOneShot(_GameController.sfxJump);
        }

        _gameObject = GameObject.FindGameObjectsWithTag("scenario");
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
                playerAnimator.SetBool("isDeath", true);

                foreach (MoveOffset moves in movement)
                {
                    moves.enabled = false;
                }

                foreach (GameObject gObjects in _gameObject)
                {
                    gObjects.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                }

                _GameController.sfxStage.Stop();
                rbPlayer.constraints = RigidbodyConstraints2D.None;

                if (oneJump == false)
                {
                    _GameController.sfxSource.PlayOneShot(_GameController.sfxDamage);
                    rbPlayer.AddForce(new Vector2(0, forceJump));
                    _GameController.sfxSource.PlayOneShot(_GameController.sfxDeath);
                    oneJump = true;
                }

                boxCollider.isTrigger = true;
                circleCollider.isTrigger = true;
                Invoke("sceneGameOver", 2.5f);
                break;

            case "triggerObstacle":
                forceJump = 600;
                rbPlayer.AddForce(new Vector2(0, forceJump));
                wait(2f);
                collision.gameObject.tag = "obstacle";
                OnTriggerEnter2D(collision);
                break;

            case "endOfScene":
                _GameController.changeScene("Stages");
                break;
        }
    }

    void sceneGameOver()
    {
        _GameController.changeScene("GameOver");
    }

    void wait(float time)
    {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
    }

    private void JUMP()
    {
        if (isGrounded == true)
        {
            rbPlayer.AddForce(new Vector2(0, forceJump));
            _GameController.sfxSource.PlayOneShot(_GameController.sfxJump);
        }
    }
}
*/