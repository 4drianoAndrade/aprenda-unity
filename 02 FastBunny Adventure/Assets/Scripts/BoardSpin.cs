using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpin : MonoBehaviour
{
    private GameController _GameController;
    SpriteRenderer spriteRenderer;
    private bool oneSpin = false;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                spriteRenderer.flipX = true;

                if (oneSpin == false)
                {
                    //_GameController.sfxSource.PlayOneShot(_GameController.sfxBoardSpin);
                    oneSpin = true;
                }

                break;
        }
    }
}
