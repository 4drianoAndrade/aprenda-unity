using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tagBullets
{
    Player, Enemy
}

public enum gameState
{
    Intro, Gameplay
}

public class GameController : MonoBehaviour
{
    public PlayerController _PlayerController;
    public gameState currentState;
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

    [Header("Intro Configuration")]
    public float shipInitialSize;
    public float originalSize;
    public Transform shipStartingPosition;
    public Transform takeoffPosition;
    public float takeoffSpeed;
    private float currentSpeed;
    private bool isTakeOff;
    public Color initialSmokeColor;
    public Color finalSmokeColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("introStage");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive == true)
        {
            limitPlayerMovement();
        }

        if (isTakeOff == true && currentState == gameState.Intro)
        {
            _PlayerController.transform.position = Vector3.MoveTowards(_PlayerController.transform.position, takeoffPosition.position, currentSpeed * Time.deltaTime);

            if (_PlayerController.transform.position == takeoffPosition.position)
            {
                StartCoroutine("moveUp");
                currentState = gameState.Gameplay;
            }

            _PlayerController.smokeSR.color = Color.Lerp(initialSmokeColor, finalSmokeColor, 0.1f);
        }
    }

    void LateUpdate()
    {
        if (currentState == gameState.Gameplay)
        {
            scenery.position = Vector3.MoveTowards(scenery.position, new Vector3(scenery.position.x, finalPosStage.position.y, 0), stageSpeed * Time.deltaTime);
        }
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

    IEnumerator introStage()
    {
        _PlayerController.smokeSR.color = initialSmokeColor;
        _PlayerController.shadow.SetActive(false);
        _PlayerController.transform.localScale = new Vector3(shipInitialSize, shipInitialSize, shipInitialSize);
        _PlayerController.transform.position = shipStartingPosition.position;

        yield return new WaitForSeconds(2);
        isTakeOff = true;

        for (currentSpeed = 0; currentSpeed < takeoffSpeed; currentSpeed += 0.2f)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator moveUp()
    {
        _PlayerController.shadow.SetActive(true);

        for (float scale = shipInitialSize; scale < originalSize; scale += 0.025f)
        {
            _PlayerController.transform.localScale = new Vector3(scale, scale, scale);
            _PlayerController.shadow.transform.localScale = new Vector3(scale, scale, scale);
            _PlayerController.smokeSR.color = Color.Lerp(_PlayerController.smokeSR.color, finalSmokeColor, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
