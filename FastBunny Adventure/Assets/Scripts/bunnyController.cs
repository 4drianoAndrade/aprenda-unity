using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour
{
    [Header("Config. Player")]

    public float forceJump;

    private Rigidbody2D rbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rbPlayer.AddForce(new Vector2(0, forceJump));
        }
    }
}
