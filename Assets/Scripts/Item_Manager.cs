using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    public bool Has_CardKey;
    public bool Has_Screen;
    public bool Has_Battery;
    public bool Has_Password;
    public bool Radio_Fixed;
    public bool is_basement;
    // Start is called before the first frame update
    void Start()
    {
        
        Has_CardKey = false;
        Has_Screen = false;
        Has_Battery = false;
        Has_Password = false;
        Radio_Fixed = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }


}
