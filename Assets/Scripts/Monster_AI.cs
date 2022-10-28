using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Monster_AI : MonoBehaviour
{ 
    
    public float Current_Speed = 2f;
    public float Normal_Speed = 1f;
    public float contact_Distance = 0.1f;
    bool check_hit;
    bool knockback_delay = false;
    
    //Variable for follow
    float X_range = 0;
    float Y_range = 0;
    Vector3 dir;
    Vector3 moveVelocity = Vector3.zero;
    //Varibale for Dash
    float dashSpeed = 30;
    float dashTime = 0.1f;
    bool is_dash = true;

    CircleCollider2D Detecting_Area;
    Rigidbody2D Rigid_Body;
    Transform Target_Transform;
    Vector3 endPosition;
    public bool follow = false;

    GameObject parentObject;
    public Rigidbody2D Target_Body;
    SpriteRenderer SR;

    //Sounde Detect
    SoundDetect SD;

    void Start()
    {
        SD = gameObject.transform.parent.GetComponentInChildren<SoundDetect>();

        Detecting_Area = GetComponent<CircleCollider2D>();
        Rigid_Body = GetComponent<Rigidbody2D>();
        Target_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        parentObject = gameObject.transform.parent.gameObject;
        if (transform.parent.tag == "TypeB")
        {
            SR = parentObject.GetComponent<SpriteRenderer>();
            SR.material.color = Color.red;
            this.transform.localScale = new Vector3(15,15,0);
        }

        StartCoroutine(Movement_Delay());

    }

    public IEnumerator Movement_Delay()
    {
        //Debug.Log("Start Couroutine : " + Time.time);

        X_range = 0;
        Y_range = 0;
        float Delay_Time = Random.Range(1f, 1.5f);

        yield return new WaitForSeconds(Delay_Time);
        //Debug.Log("Waiting Time : " + Delay_Time);

        float Movement_Time = Random.Range(2f, 4f);
        X_range = Random.Range(-1f, 1f);
        Y_range = Random.Range(-1f, 1f);

        dir = new Vector3(X_range, Y_range);

        yield return new WaitForSeconds(Movement_Time);
        //Debug.Log("Moving Time : " + Movement_Time);
        StartCoroutine(Movement_Delay());
    }
    
    IEnumerator KnockBack_Delay()
    {
        follow = false;
        knockback_delay = true;
        yield return new WaitForSeconds(0.15f);
        follow = true;
        parentObject.GetComponent<CombatComponent>().is_knockback = false;
        //Follow_Target();
    }
    

    private void FixedUpdate()
    {
        Move_Byself();  
        
        transform.position = transform.parent.position;
    }


    private void Follow_Target()
    {  
            Rigid_Body = parentObject.GetComponent<Rigidbody2D>();

    if (Vector3.Distance(Rigid_Body.position, Target_Body.position) > contact_Distance && follow)
        {
            //look at Target direction
            
                Vector3 dir = Target_Body.position - Rigid_Body.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Rigid_Body.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //Follow the target
                Vector3 position = Vector3.MoveTowards(Rigid_Body.position, Target_Body.position, Current_Speed * Time.deltaTime);
            if (is_dash == false || transform.parent.tag != "TypeB")
            {
                Rigid_Body.MovePosition(position);
            }
            else if (transform.parent.tag == "TypeB" && is_dash == true)
            { 
                StartCoroutine(DashCoroutine(Rigid_Body, position));
                is_dash = false;
                StartCoroutine("Dash_Delay");
            }
        }
    else
        {
            Rigid_Body.velocity = Vector3.zero;
        }

    }
    private IEnumerator DashCoroutine(Rigidbody2D rigid, Vector3 position)
    {
        float startTime = Time.time;
        is_dash = true;
        while (Time.time < startTime + dashTime)
        {
             //Debug.Log("대시" + startTime);
             Current_Speed = dashSpeed;
             yield return null;
        }
        Current_Speed = 2f;
        //Debug.Log("이건 왜 안함?" + startTime);
        
        
       
    }

    private IEnumerator Dash_Delay()
    {
        //Debug.Log(is_dash);
        yield return new WaitForSeconds(1.5f);
        is_dash = true;
    }

    /*
    private bool check_hit()
    {
        if(parentObject.GetComponent<CombatComponent>().start_follow == true)
        {
            Target_Body =
        }
    }
    */

    private void Move_Byself()
    {
        if (SD != null && SD.detect_sound == true)
        {
            Target_Body = SD.Target_Body;
            follow = true;
  
        }

        //parent object
        if (follow == false && knockback_delay == false)
        {
            parentObject.GetComponent<CombatComponent>().is_knockback = false;
            moveVelocity = new Vector3(X_range, Y_range);
        
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.parent.rotation = Quaternion.Euler(0, 0, angle);
            transform.parent.position += moveVelocity * Normal_Speed * Time.deltaTime;

        }
        else
        {
            if (Rigid_Body != null && Target_Body != null)
            { 
                if (parentObject.GetComponent<CombatComponent>().is_knockback == false)
                {
                    Follow_Target();
                }
                else
                {
                    StartCoroutine("KnockBack_Delay");
                }
            }
          
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            follow = true;
            StopCoroutine(Movement_Delay());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Target_Body = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Movement_Delay");
            follow = false;
        }
        */
    }
}
