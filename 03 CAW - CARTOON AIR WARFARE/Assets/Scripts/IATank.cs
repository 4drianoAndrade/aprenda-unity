using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATank : MonoBehaviour
{
    private GameController _GameController;
    public int idBullet;
    public tagBullets tagShot;
    public Transform gun;
    public float shotSpeed;
    public float delayShot;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameVisible()
    {
        StartCoroutine("shotControl");
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    IEnumerator shotControl()
    {
        yield return new WaitForSeconds(delayShot);
        shoot();
        StartCoroutine("shotControl");
    }

    void shoot()
    {
        if (_GameController.isPlayerAlive == true)
        {
            gun.up = _GameController._PlayerController.transform.position - transform.position;
            GameObject temp = Instantiate(_GameController.bullet[idBullet], gun.position, gun.localRotation);
            temp.transform.tag = _GameController.applyTag(tagShot);
            temp.GetComponent<Rigidbody2D>().velocity = gun.up * shotSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "playerShot":
                GameObject temp = Instantiate(_GameController.explosionPrefab, transform.position, _GameController.explosionPrefab.transform.localRotation);
                temp.transform.parent = _GameController.scenery;

                _GameController.addScore(points);

                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
}
