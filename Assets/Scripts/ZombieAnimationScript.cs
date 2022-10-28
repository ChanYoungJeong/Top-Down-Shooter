using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class ZombieAnimationScript : MonoBehaviour
{
    public Animator zombie_animator;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }


    // Update is called once per frame
    void Update()
    {
        Monster_AI c = GetComponentInChildren<Monster_AI>();

        if (c != null){
            if(c.follow == true){
                zombie_animator.SetBool("follow", true);
            }
            else {
                zombie_animator.SetBool("follow", false);
            }
        }

    }


}
