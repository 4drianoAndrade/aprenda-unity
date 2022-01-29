using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] loot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "playerShot":

                Destroy(collision.gameObject);

                GameObject temp = Instantiate(explosionPrefab, transform.position, transform.localRotation);
                Destroy(temp.gameObject, 0.5f);

                spawnLoot();

                Destroy(this.gameObject);

                break;
        }
    }

    void spawnLoot()
    {
        int itemId = 0;
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            rand = Random.Range(0, 100);

            if (rand > 85)
            {
                itemId = 2; // CAIXA BOMBA
            }
            else if (rand > 50)
            {
                itemId = 1; // CAIXA SAUDE
            }
            else
            {
                itemId = 0; // CAIXA MOEDA
            }

            Instantiate(loot[itemId], transform.position, transform.localRotation);
        }
    }
}
