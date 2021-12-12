using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlBridge : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D bridgeRb;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        bridgeRb = GetComponent<Rigidbody2D>();

        bridgeRb.velocity = new Vector2(_GameController.velocityObject, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _GameController.distanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
