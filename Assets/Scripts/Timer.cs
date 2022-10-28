using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text text_Timer;
    public float time_current;
    public float Time_Max = 15f;
    public Image Timer_Image;
    private bool isEnded;
    GameObject hud;
    UI_HUD UIhud;
    //GameObject 

    GameObject Trigger;
    Stage3_Trigger S3_Trigger;

    GameObject BGM_Player;
    AudioSource backmusic;
    // Start is called before the first frame update
    void Start()
    {
        Timer_Image.enabled = false;
        text_Timer.enabled = false;
        BGM_Player = GameObject.Find("BGM_Stage3");
        backmusic = BGM_Player.GetComponent<AudioSource>();

        //backmusic.Pause();

        Trigger = GameObject.Find("Start_Timer_Trigger");
        S3_Trigger = Trigger.GetComponent<Stage3_Trigger>();
        hud = GameObject.Find("HUD");
        UIhud = hud.GetComponent<UI_HUD>();
        Reset_Timer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnded)
            return;

        if (S3_Trigger.Start_Stage3 == true)
        {
            text_Timer.enabled = true;
            Timer_Image.enabled = true;
            Check_Timer();    
        }

        if(Trigger)
        {
            Debug.Log(Trigger);
            backmusic.Pause();
        }
        else
        {
            if (backmusic.isPlaying) return;
            else
            backmusic.Play();
        }
            

    }

    private void Check_Timer()
    {

        if (0 < time_current)
        {
            time_current -= Time.deltaTime;
            text_Timer.text = $"{time_current:N1}";
            //Debug.Log(time_current);
        }
        else if (!isEnded)
        {
            End_Timer();
           
        }


    }

    private void End_Timer()
    {
        Debug.Log("End");
        time_current = 0;
        text_Timer.text = $"{time_current:N1}";
        isEnded = true;

        StartCoroutine(End_Delay());
    }


    private void Reset_Timer()
    {
        time_current = Time_Max;
        text_Timer.text = $"{time_current:N1}";
        isEnded = false;
        Debug.Log("Start");
    }

    IEnumerator End_Delay()
    {
        hud.transform.Find("Panel").gameObject.SetActive(true);
        UIhud.StartCoroutine(UIhud.FadeCoroutine());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinLevel");
    }
}
