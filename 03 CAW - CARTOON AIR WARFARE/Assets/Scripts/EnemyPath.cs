using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform enemyShip;
    public Transform[] checkPoints;

    public float movementSpeed;
    public float delayStopped;

    private int checkPointId;
    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("startMovement");
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            enemyShip.localPosition = Vector3.MoveTowards(enemyShip.localPosition, checkPoints[checkPointId].position, movementSpeed * Time.deltaTime);

            if (enemyShip.localPosition == checkPoints[checkPointId].position)
            {
                move = false;
                StartCoroutine("startMovement");
            }
        }
    }

    IEnumerator startMovement()
    {
        checkPointId++;

        if (checkPointId >= checkPoints.Length)
        {
            checkPointId = 0;
        }

        yield return new WaitForSeconds(delayStopped);
        move = true;
    }
}
