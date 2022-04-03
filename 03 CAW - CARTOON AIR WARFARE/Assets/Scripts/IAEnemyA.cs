using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    Above, Below, Left, Right
}

public class IAEnemyA : MonoBehaviour
{
    private GameController _GameController;
    public direction directionMovement;

    public float movementSpeed;
    public float curveStartingPoint;
    private bool isCurve;
    public float curveAngle;
    public float increment;
    private float incremented;
    private float zRotation;

    public int idBullet;
    public tagBullets tagShot;

    public Transform gun;
    public float shotSpeed;

    public float delayShot;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        zRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        curveControl();
    }

    void shoot()
    {
        GameObject temp = Instantiate(_GameController.bullet[idBullet], gun.position, transform.localRotation);
        temp.transform.tag = _GameController.applyTag(tagShot);
        temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * shotSpeed;
    }

    void OnBecameVisible()
    {
        StartCoroutine("shotControl");
    }

    IEnumerator shotControl()
    {
        yield return new WaitForSeconds(delayShot);
        shoot();
        StartCoroutine("shotControl");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "playerShot":
                GameObject temp = Instantiate(_GameController.explosionPrefab, transform.position, _GameController.explosionPrefab.transform.localRotation);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }

    void curveControl()
    {
        if (transform.position.y <= curveStartingPoint && isCurve == false && directionMovement == direction.Below)
        {
            isCurve = true;
        }

        if (transform.position.y >= curveStartingPoint && isCurve == false && directionMovement == direction.Above)
        {
            isCurve = true;
        }

        if (transform.position.x <= curveStartingPoint && isCurve == false && directionMovement == direction.Left)
        {
            isCurve = true;
        }

        if (transform.position.x >= curveStartingPoint && isCurve == false && directionMovement == direction.Right)
        {
            isCurve = true;
        }

        if (isCurve == true && incremented < curveAngle)
        {
            zRotation += increment;
            transform.rotation = Quaternion.Euler(0, 0, zRotation);

            if (increment < 0)
            {
                incremented += (increment * -1);
            }
            else
            {
                incremented += increment;
            }
        }

        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
    }
}
