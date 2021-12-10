using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float velocityMovement;

    public float limitMaxY;
    public float limitMinY;
    public float limitMaxX;
    public float limitMinX;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float posY = transform.position.y;
        float posX = transform.position.x;

        playerRb.velocity = new Vector2(horizontal * velocityMovement, vertical * velocityMovement);

        // Verifica o limite X
        if (transform.position.x > limitMaxX)
        {
            posX = limitMaxX;
        }
        else if (transform.position.x < limitMinX)
        {
            posX = limitMinX;
        }

        // Verifica o limite Y
        if (transform.position.y > limitMaxY)
        {
            posY = limitMaxY;
        }
        else if (transform.position.y < limitMinY)
        {
            posY = limitMinY;
        }

        transform.position = new Vector2(posX, posY);
    }
}
