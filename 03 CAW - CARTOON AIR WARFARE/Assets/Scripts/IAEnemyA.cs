using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemyA : MonoBehaviour
{
    public float movementSpeed;
    public float curveStartingPoint;
    private bool isCurve;
    public float curveAngle;
    public float increment;
    private float incremented;
    private float zRotation;
    public bool isPosX, isPosY;
    public bool isNegative;

    // Start is called before the first frame update
    void Start()
    {
        zRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= curveStartingPoint && isCurve == false && isPosY == true && isNegative == false)
        {
            isCurve = true;
        }

        if (transform.position.y >= curveStartingPoint && isCurve == false && isPosY == true && isNegative == true)
        {
            isCurve = true;
        }

        if (transform.position.x <= curveStartingPoint && isCurve == false && isPosX == true && isNegative == false)
        {
            isCurve = true;
        }

        if (transform.position.x >= curveStartingPoint && isCurve == false && isPosX == true && isNegative == true)
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
