using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public int Maxcount;
    public int spawnCount;
    public GameObject item;
    int rangeX;
    int rangeZ;
    //public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        Maxcount = 10;
        spawnCount = 0;
    }

    public void EventWaveitem()
    {
        for(int i= spawnCount; spawnCount < Maxcount; i++)
        {
            rangeX = Random.Range(-50, 50);
            rangeZ = Random.Range(-50, 50);
            Instantiate(item, new Vector3(rangeX, 0, rangeZ), Quaternion.identity);
            spawnCount++;
        }
    }
}
