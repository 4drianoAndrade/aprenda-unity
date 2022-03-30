using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D playerRb;
    public float speed;
    public Transform weaponPosition;
    public float shotSpeed;
    public int idBullet;
    public tagBullets tagShot;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        _GameController._PlayerController = this;
        _GameController.isPlayerAlive = true;

        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector2(horizontal * speed, vertical * speed);

        if (Input.GetButtonDown("Fire1"))
        {
            shot();
        }
    }

    void shot()
    {
        GameObject temp = Instantiate(_GameController.bullet[idBullet]);

        temp.transform.tag = _GameController.applyTag(tagShot);

        temp.transform.position = weaponPosition.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enemyShot":
                _GameController.playerHit();
                Destroy(collision.gameObject);
                break;
        }
    }
}
