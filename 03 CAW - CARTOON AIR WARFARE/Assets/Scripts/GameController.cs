using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController _playerController;

    [Header("Movement Limits")]
    public Transform upperLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    [Header("Camera Side Limit")]
    public Camera cam;
    public Transform leftCameraLimit;
    public Transform rightCameraLimit;
    public float sideCameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        limitPlayerMovement();
    }

    void LateUpdate()
    {
        controlCameraPosition();
    }

    void controlCameraPosition()
    {
        if (cam.transform.position.x > leftCameraLimit.position.x && cam.transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (cam.transform.position.x <= leftCameraLimit.position.x && _playerController.transform.position.x > leftCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (cam.transform.position.x >= rightCameraLimit.position.x && _playerController.transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
    }

    void moveCamera()
    {
        Vector3 cameraTargetPosition = new Vector3(_playerController.transform.position.x, cam.transform.position.y, -10);
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraTargetPosition, sideCameraSpeed * Time.deltaTime);
    }

    void limitPlayerMovement()
    {
        float posY = _playerController.transform.position.y;
        float posX = _playerController.transform.position.x;

        if (posY > upperLimit.position.y)
        {
            _playerController.transform.position = new Vector2(posX, upperLimit.position.y);
        }
        else if (posY < bottomLimit.position.y)
        {
            _playerController.transform.position = new Vector2(posX, bottomLimit.position.y);
        }

        if (posX > rightLimit.position.x)
        {
            _playerController.transform.position = new Vector2(rightLimit.position.x, posY);
        }
        else if (posX < leftLimit.position.x)
        {
            _playerController.transform.position = new Vector2(leftLimit.position.x, posY);
        }
    }
}
