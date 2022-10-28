using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyAnimation : MonoBehaviour
{
    public Animator animatorr;
    Vector2 movementt;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement c = GetComponentInParent<Player_Movement>();
        if (c != null){
            movementt = c.movement;
        }


        if (movementt.sqrMagnitude >= 0.1){
            animatorr.SetFloat("Speed", 1);
        }
        else if(movementt.sqrMagnitude < 0.1){
            animatorr.SetFloat("Speed", 0);
        }
    }

}
