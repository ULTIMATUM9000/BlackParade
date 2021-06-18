using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Player player;
    public GameObject enemy;
    public GameObject enemySpawnPoint;
    int enemyAmount = 10;
    bool isSpawning = false;
    public List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < enemyAmount; x++) //enemyAmount equals amount of enemies that can be out at a time for each spawner
        {
            GameObject obj = Instantiate(enemy);
            obj.SetActive(false);
            enemyList.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning == false && player.PersonCarrying)// need to put an && personPickedUp == true
        {
            StartCoroutine("spawner");
        }
    }

    IEnumerator spawner()
    {
        isSpawning = true;
        for (int i = 0; i < enemyAmount; i++)
        {

            enemyList[i].transform.position = enemySpawnPoint.transform.position;
            enemyList[i].transform.eulerAngles = new Vector3(0, 0, 0); //rotation of the enemy ignore this
            enemyList[i].SetActive(true);
            yield return new WaitForSeconds(2.00f);
        }
        isSpawning = false;
    }
}
