using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tagBullets
{
    Player, Enemy
}

public class GameController : MonoBehaviour
{
    public PlayerController _PlayerController;

    public GameObject playerPrefab;
    public int extraLife;
    public Transform playerSpawnPosition;

    public float delayPlayerSpawn;
    public float invincibleTime;

    [Header("Movement Limits")]
    public Transform upperLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    public Transform finalPosStage;
    public Transform scenery;
    public float stageSpeed;

    [Header("Prefabs")]
    public GameObject[] bullet;
    public GameObject explosionPrefab;

    public bool isPlayerAlive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive == true)
        {
            limitPlayerMovement();
        }
    }

    void LateUpdate()
    {
        scenery.position = Vector3.MoveTowards(scenery.position, new Vector3(scenery.position.x, finalPosStage.position.y, 0), stageSpeed * Time.deltaTime);
    }

    void limitPlayerMovement()
    {
        float posY = _PlayerController.transform.position.y;
        float posX = _PlayerController.transform.position.x;

        if (posY > upperLimit.position.y)
        {
            _PlayerController.transform.position = new Vector2(posX, upperLimit.position.y);
        }
        else if (posY < bottomLimit.position.y)
        {
            _PlayerController.transform.position = new Vector2(posX, bottomLimit.position.y);
        }

        if (posX > rightLimit.position.x)
        {
            _PlayerController.transform.position = new Vector2(rightLimit.position.x, posY);
        }
        else if (posX < leftLimit.position.x)
        {
            _PlayerController.transform.position = new Vector2(leftLimit.position.x, posY);
        }
    }

    public string applyTag(tagBullets tag)
    {
        string goBack = null;

        switch (tag)
        {
            case tagBullets.Player:
                goBack = "playerShot";
                break;

            case tagBullets.Enemy:
                goBack = "enemyShot";
                break;
        }

        return goBack;
    }

    public void playerHit() // Função ao tomar tiro inimigo
    {
        isPlayerAlive = false;
        Destroy(_PlayerController.gameObject);
        GameObject temp = Instantiate(explosionPrefab, _PlayerController.transform.position, explosionPrefab.transform.localRotation);
        extraLife -= 1;

        if (extraLife >= 0)
        {
            StartCoroutine("playerSpawn");
        }
        else
        {
            print("GAME OVER");
        }
    }

    IEnumerator playerSpawn()
    {
        yield return new WaitForSeconds(delayPlayerSpawn);
        GameObject temp = Instantiate(playerPrefab, playerSpawnPosition.position, playerSpawnPosition.localRotation);
        yield return new WaitForEndOfFrame();
        _PlayerController.StartCoroutine("Invincible");
    }
}
