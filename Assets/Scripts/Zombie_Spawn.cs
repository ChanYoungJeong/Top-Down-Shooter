using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Spawn : MonoBehaviour
{
    public GameObject Zombie_Prefab;
    GameObject Timer_Canvas;
    Monster_AI Zombie_AI;
    Timer Get_Time;

    public float Spawn_Rate_Min = 0.5f;
    public float Spawn_Rate_Max = 3f;

    private float Spawn_Rate;
    private float Time_After_Spawn;
    private float Check_Time;
    private float Stage_Time;

    private float Get_Zombie_Type;
    private float Zombie_Random_Scale;

    GameObject Trigger;
    Stage3_Trigger S3_Trigger;

    // Start is called before the first frame update
    void Start()
    {
        Timer_Canvas = GameObject.Find("TimerCanvas");
        Get_Time = Timer_Canvas.GetComponent<Timer>();
        Spawn_Rate = Random.Range(Spawn_Rate_Max, Spawn_Rate_Min);

        Trigger = GameObject.Find("Start_Timer_Trigger");
        S3_Trigger = Trigger.GetComponent<Stage3_Trigger>();

    }

    // Update is called once per frame
    void Update()
    {
        if (S3_Trigger.Start_Stage3 == true)
        {
            Time_After_Spawn += Time.deltaTime;
        }
        Stage_Time = Get_Time.Time_Max;
        Spawn_Rate_Max =  Mathf.Pow(7 , Get_Time.time_current / Stage_Time) - 0.8f;
        Spawn_Rate_Min =  Mathf.Pow(5 , Get_Time.time_current / Stage_Time) - 0.8f;
        Check_Time = Get_Time.time_current;

        //Debug.Log(Spawn_Rate_Max);
        if (Time_After_Spawn >= Spawn_Rate && Check_Time != 0)
        {
            Time_After_Spawn = 0f;

            GameObject zombie = Instantiate(Zombie_Prefab, transform.position, transform.rotation);
            Zombie_AI = zombie.GetComponentInChildren<Monster_AI>();
            Zombie_AI.follow = true;
            Zombie_AI.Target_Body = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

            Get_Zombie_Type = Random.Range(1f, 0f);
            Zombie_Random_Scale = Random.Range(0.7f, 0.95f);
            
            zombie.transform.localScale = new Vector3(Zombie_Random_Scale, Zombie_Random_Scale, 0);
            if(Get_Zombie_Type > 0.8)
            {
                zombie.tag = "TypeB";
            }
            Spawn_Rate = Random.Range(Spawn_Rate_Max, Spawn_Rate_Min);
        }
    }
}
