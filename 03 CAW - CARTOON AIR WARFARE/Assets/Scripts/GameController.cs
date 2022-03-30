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
    public Transform playerSpawn;

    [Header("Movement Limits")]
    public Transform upperLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    [Header("Camera Side Limit")]
    public Camera cam;
    public Transform cameraFinalPosition;
    public float phaseSpeed;
    public Transform leftCameraLimit;
    public Transform rightCameraLimit;
    public float sideCameraSpeed;

    [Header("Prefabs")]
    public GameObject[] bullet;
    public GameObject explosionPrefab;

    public bool isPlayerAlive;

    // Start is called before the first frame update
    void Start()
    {
        //_PlayerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
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
        //cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(cam.transform.position.x, cameraFinalPosition.position.y, -10), phaseSpeed * Time.deltaTime);
        //controlCameraPosition();
    }

    void controlCameraPosition()
    {
        if (cam.transform.position.x > leftCameraLimit.position.x && cam.transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (cam.transform.position.x <= leftCameraLimit.position.x && _PlayerController.transform.position.x > leftCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (cam.transform.position.x >= rightCameraLimit.position.x && _PlayerController.transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
    }

    void moveCamera()
    {
        Vector3 cameraTargetPosition = new Vector3(_PlayerController.transform.position.x, cam.transform.position.y, -10);
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraTargetPosition, sideCameraSpeed * Time.deltaTime);
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
            Instantiate(playerPrefab, playerSpawn.position, playerSpawn.localRotation);
        }
        else
        {
            print("GAME OVER");
        }
    }
}
