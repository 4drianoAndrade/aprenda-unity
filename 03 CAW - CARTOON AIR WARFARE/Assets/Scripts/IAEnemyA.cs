using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    Above, Below, Left, Right
}

public class IAEnemyA : MonoBehaviour
{
    public direction directionMovement;

    public float movementSpeed;
    public float curveStartingPoint;
    private bool isCurve;
    public float curveAngle;
    public float increment;
    private float incremented;
    private float zRotation;

    public GameObject prefabShot;
    public Transform gun;
    public float shotSpeed;

    public float delayShot;

    // Start is called before the first frame update
    void Start()
    {
        zRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        curveControl();
    }

    void shoot()
    {
        GameObject temp = Instantiate(prefabShot, gun.position, transform.localRotation);
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
