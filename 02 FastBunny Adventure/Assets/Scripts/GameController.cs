using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Config. Ground")]

    public float velocityGround;
    public float distanceDestroy;
    public float sizeGround;
    public GameObject[] groundPrefab;
    public MoveOffset[] moves;

    [Header("Globals")]

    public int score;
    public Text txtScore;
    public int count = 0;
    public SceneManager sceneManager;

    [Header("FX Sound")]

    public AudioSource sfxStage;
    public AudioSource sfxSource;
    public AudioClip sfxJump;
    public AudioClip sfxPoints;
    public AudioClip sfxDamage;
    public AudioClip sfxDeath;
    public AudioClip sfxBoardSpin;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toScore(int qttPoints)
    {
        score += qttPoints;
        txtScore.text = "Score: " + score.ToString();
        sfxSource.PlayOneShot(sfxPoints);
    }

    public void changeScene(string sceneDestiny)
    {
        SceneManager.LoadScene(sceneDestiny);
    }
}
