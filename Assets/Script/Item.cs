using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int spawnCount;
    //public GameObject item;
    public int cubeScore;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        cubeScore = 20;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            GameObject.Find("Spawn").GetComponent<ItemSpawn>().spawnCount -= 1;
            GameObject.Find("Spawn").GetComponent<ItemSpawn>().EventWaveitem();
            Destroy(gameObject);
        }

        //if (other.CompareTag("Player"))
        //{
        //    other.GetComponent<ItemAdd>().soresum(cubeScore);
        //}

        
        
    }
        
}
