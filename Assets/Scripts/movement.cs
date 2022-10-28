using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //Vector2 movement;
    public Animator ani;
    Vector2 movementt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement c = GetComponentInParent<Player_Movement>();
        if (c != null){
            movementt = c.movement;
        }
        // ani.SetFloat("Speed", movement.sqrMagnitude);
        ani.SetFloat("Speed", movementt.sqrMagnitude);
    }
}
