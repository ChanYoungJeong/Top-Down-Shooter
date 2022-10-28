using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var objs = FindObjectsOfType<DontDestroyObject>();
        if (objs.Length > 6)
        {
            Debug.Log(gameObject.name + " Destroyed");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(objs.Length);
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame

}
