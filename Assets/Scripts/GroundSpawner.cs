using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> groundTiles;
    int groundTilesSum = 15;
    Vector3 nextSpawnPoint;
    //Start is called before the first frame update
    void Start()
    {
        SpawnTiles();
    }

    public void SpawnTiles()
    {
        for (int i = 0; i < groundTilesSum; i++)
        {
            if (i < 3)
            {
                // First 3 tile without stuff on it
                spawnTIle(0);
            }
            else
            {
                spawnTIle();
            }
        }
    }

    public void setNextSpawnPoint(Vector3 nextSpawnPoint)
    {
       this.nextSpawnPoint = nextSpawnPoint;
    }

    public void spawnTIle(int tileIndex = -1)
    {
        GameObject objectFromArray;
        if (tileIndex != -1)
        {
            //First object is empty floor
            objectFromArray = groundTiles[tileIndex];
        }
        else
        {
            int random = Random.Range(1, groundTiles.Count);
            objectFromArray = groundTiles[random];
        }
        GameObject createdFloor = Instantiate(objectFromArray, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = createdFloor.transform.GetChild(1).transform.position;
    }
}
