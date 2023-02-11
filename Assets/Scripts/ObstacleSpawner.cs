using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacle;
    Vector3 spawnPos;
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos= transform.position;
        StartCoroutine("SpawnObstacles");
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    void Spawn()
    {
        int randObstacle = Random.Range(0, obstacle.Length);
        int randomSpot = Random.Range(0, 4); //0 && 2 == top  && 1 && 3 == bottom
        spawnPos = transform.position;

        if (randomSpot == 0 || randomSpot == 2)
        {
            Instantiate(obstacle[randObstacle], spawnPos, transform.rotation);
        }
        else
        {
            spawnPos.y = -transform.position.y;

            if(randObstacle== 1)
            {
                spawnPos.x += 1;
            }
            else if (randObstacle == 2)
            {
                spawnPos.x += 2;
            }

            GameObject obs = Instantiate(obstacle[randObstacle], spawnPos, transform.rotation);
            obs.transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            Spawn();
            GameManager.Instance.UpdateScore();
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
