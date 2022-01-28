using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float speed;

    public GameObject bulletPrefab;
    public Transform weaponPosition;

    public float shotSpeed;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject temp = Instantiate(bulletPrefab);
        temp.transform.position = weaponPosition.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
    }
}
