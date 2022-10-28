using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Trigger : MonoBehaviour
{
    public bool Start_Stage3 = false;
    private bool is_collide = false;
    private bool select_yes = false;
    private bool select_no = false;
    GameObject hud;
    
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("HUD").transform.Find("Stage3_TextBox").gameObject;
        //hud.transform.GetChild(0).gameObject.SetActive(false);
        hud.transform.GetChild(1).gameObject.SetActive(false);
        hud.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (select_yes && Input.anyKeyDown)
        {
            Start_Stage3 = true;
            is_collide = false;
            hud.SetActive(false);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
        else if (select_no && Input.anyKeyDown)
        {
            is_collide = false;
            hud.transform.GetChild(0).gameObject.SetActive(true);
            hud.transform.GetChild(2).gameObject.SetActive(false);
            hud.SetActive(false);
            Time.timeScale = 1f;
            is_collide = false;
            select_no = false;
        }

        if (is_collide)
        {
            Time.timeScale = 0f;
            hud.SetActive(true);
            //hud.transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hud.transform.GetChild(0).gameObject.SetActive(false);
                hud.transform.GetChild(1).gameObject.SetActive(true);
                select_yes = true;
                //StartCoroutine(Delay());
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                hud.transform.GetChild(0).gameObject.SetActive(false);
                hud.transform.GetChild(2).gameObject.SetActive(true);
                //StartCoroutine(Delay());
                select_no = true;
            }
        }

        
        //hud.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_collide = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.1f);
    }
}
