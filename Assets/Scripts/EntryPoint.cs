using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var entryPoints = GameObject.FindGameObjectsWithTag("EntryPoint");
        foreach(var entryPoint in entryPoints)
        {
            var entryData = entryPoint.GetComponent<EntryPointData>();
            if(entryData && PersistentManager.entryLevel == entryData.entryLevel)
            {
                gameObject.transform.position = entryPoint.transform.position;
                break;
            }
        }
    }
}
