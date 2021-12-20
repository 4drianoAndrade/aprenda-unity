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

    [Header("Globals")]

    public int score;
    //public Text txtScore;

    //[Header("FX Sound")]

    //public AudioSource fxSource;
    //public AudioClip fxPoints;

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
        //txtScore.text = "Score: " + score.ToString();
        //fxSource.PlayOneShot(fxPoints);
    }

    public void changeScene(string sceneDestiny)
    {
        SceneManager.LoadScene(sceneDestiny);
    }
}
