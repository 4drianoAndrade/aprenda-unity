using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGround : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D groundRb;
    private bool isInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        groundRb = GetComponent<Rigidbody2D>();
        groundRb.velocity = new Vector2(_GameController.velocityGround, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstantiated == false)
        {
            if (transform.position.x <= 0)
            {
                isInstantiated = true;

                int idPrefab = 0;
                int rand = Random.Range(0, 96);

                if (rand < 12)
                {
                    idPrefab = 0;
                }
                else if (rand < 24)
                {
                    idPrefab = 1;
                }
                else if (rand < 36)
                {
                    idPrefab = 2;
                }
                else if (rand < 48)
                {
                    idPrefab = 3;
                }
                else if (rand < 60)
                {
                    idPrefab = 4;
                }
                else if (rand < 72)
                {
                    idPrefab = 5;
                }
                else if (rand < 84)
                {
                    idPrefab = 6;
                }
                else
                {
                    idPrefab = 7;
                }

                _GameController.count++;

                if (_GameController.count == 10)
                {
                    idPrefab = 8;
                }

                GameObject temp = Instantiate(_GameController.groundPrefab[idPrefab]);

                float posX = transform.position.x + _GameController.sizeGround;
                float posY = transform.position.y;
                temp.transform.position = new Vector2(posX, posY);
            }
        }

        if (transform.position.x < _GameController.distanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
