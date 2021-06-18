using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMech : MonoBehaviour
{
	List<GameObject> damnedList = new List<GameObject>();
    [SerializeField] GameObject damned1;
    [SerializeField] GameObject damned2;
    [SerializeField] GameObject damned3;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] GameObject StartPoint;
    [SerializeField] GameObject EndPoint;
    
    float respawnTime = 10.0f;

    void Start()
    {
    	damnedList.Add(damned1);
        damnedList.Add(damned2);
        damnedList.Add(damned3);
    	StartPoint.transform.position = new Vector2(StartPoint.transform.position.x, SpawnPoint.transform.position.y);
    	EndPoint.transform.position = new Vector2(EndPoint.transform.position.x, SpawnPoint.transform.position.y);
        StartCoroutine(Spawn());
    }

    void Deploy()
    {
    	//Gotta change the spawn //Datu ObjectPooling
        int prefabIndex = UnityEngine.Random.Range(0,3);
        GameObject a = Instantiate(damnedList[prefabIndex]) as GameObject; //temporary
        a.transform.position = new Vector2(Random.Range(StartPoint.transform.position.x, EndPoint.transform.position.x), SpawnPoint.transform.position.y);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            Deploy();
        }
    }
}
