using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHandler : MonoBehaviour
{

    public GameObject firstChest;

    
    void Start()
    {
        spawnChest();
    }

    private void spawnChest()
    {
        GameObject nc = Instantiate(firstChest) as GameObject;
        nc.transform.localPosition = new Vector2(Random.Range(-13f, 6f), Random.Range(2.5f, 5f));
    }

}
