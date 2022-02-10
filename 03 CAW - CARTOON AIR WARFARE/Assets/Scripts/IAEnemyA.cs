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

    // Start is called before the first frame update
    void Start()
    {
        zRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= curveStartingPoint && isCurve == false)
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
