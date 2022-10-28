using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb;
    //Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
    public Transform firePoint;

    public Vector2 movement;
    Vector2 mousePos;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        //movement
        
        if (movement.x == 0 && movement.y == 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            //rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
            rb.AddForce(movement.normalized * moveSpeed, ForceMode2D.Impulse);
        }
    }
}

