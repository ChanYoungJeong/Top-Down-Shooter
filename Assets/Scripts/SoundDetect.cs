using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetect : MonoBehaviour
{
    Shooting c_Shooting;
    public bool detect_sound;
    public Rigidbody2D Target_Body;
    Rigidbody2D Rigid_Body2;
    CircleCollider2D Sound_Detect_Area;
    Monster_AI AI_script;
    private Coroutine AI_Coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        detect_sound = false;
        AI_script = gameObject.transform.parent.GetComponentInChildren<Monster_AI>(); ;
        c_Shooting = GameObject.Find("Firepoint").GetComponent<Shooting>();
        Rigid_Body2 = GetComponent<Rigidbody2D>();
        Sound_Detect_Area = GetComponent<CircleCollider2D>();
        AI_Coroutine = StartCoroutine(AI_script.Movement_Delay());
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player") && c_Shooting.shoot)
        {           
            if (Input.GetMouseButton(0))
            {
                detect_sound = true;
                Target_Body = collision.gameObject.GetComponent<Rigidbody2D>();
                if (AI_Coroutine != null)
                {
                    StopCoroutine(AI_script.Movement_Delay());
                }
            }
        }
   
    }
}
