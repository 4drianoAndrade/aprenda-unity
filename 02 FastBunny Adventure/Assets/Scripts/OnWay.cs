using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWay : MonoBehaviour
{
    private GameController _GameController;
    public Transform surface;
    public BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        if (surface.position.y < _GameController.posY)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
}
