using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlBarrel : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D barrelRb;
    private bool punctuated;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        barrelRb = GetComponent<Rigidbody2D>();
        barrelRb.velocity = new Vector2(_GameController.velocityObject, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _GameController.distanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    void LateUpdate()
    {
        if (punctuated == false && transform.position.x < _GameController.posXPlayer)
        {
            punctuated = true;
            _GameController.toScore(10);
        }
    }
}
