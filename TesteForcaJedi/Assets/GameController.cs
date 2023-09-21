using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> SpawnerList = new List<GameObject>();
    public GameObject enemy;
    
    void Update()
    {
        SpawnerEnemy();
    }

    public void SpawnerEnemy()
    {
        int SpawnerEscolhido = Random.Range(0, SpawnerList.Count);
        Instantiate(enemy, SpawnerList[SpawnerEscolhido].transform.position, SpawnerList[SpawnerEscolhido].transform.rotation);

        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            SpawnerList.Clear();
        }

    }


}
