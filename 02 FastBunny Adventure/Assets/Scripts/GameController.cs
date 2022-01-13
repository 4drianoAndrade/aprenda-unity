using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Character _playercontroller;

    public float posY;

    // Start is called before the first frame update
    void Start()
    {
        _playercontroller = FindObjectOfType(typeof(Character)) as Character;
    }

    void LateUpdate()
    {
        posY = _playercontroller.transform.position.y;
    }
}
