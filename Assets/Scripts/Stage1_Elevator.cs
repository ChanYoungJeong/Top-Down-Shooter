using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1_Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    bool has_key;
    GameObject ItemManager;
    GameObject hud;
    Item_Manager Items;

    public Image textboximage;
    private bool is_collide = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_collide = true;
        }
    }

    void Start()
    {
        ItemManager = GameObject.Find("ItemManager");
        Items = ItemManager.GetComponent<Item_Manager>();
        hud = GameObject.Find("HUD").transform.Find("TextBox").gameObject;
        textboximage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_collide && !Items.Has_CardKey)
        {
            Time.timeScale = 0f;
            textboximage.enabled = true;
            hud.transform.GetChild(0).gameObject.SetActive(true);
            //hud.transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hud.transform.GetChild(0).gameObject.SetActive(false);
                //StartCoroutine(Delay());
                textboximage.enabled = false;
                Time.timeScale = 1f;
                is_collide = false;
            }
        }
       
    }


    /*
    private void OnTriggerStay2D(Collider2D collision)
    {

        Item_Manager Items = ItemManager.GetComponent<Item_Manager>();

        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            return;
        }

        if (collision.gameObject.CompareTag("Player") && Items.Has_CardKey)
        {
            SceneManager.LoadScene("Basement1");
        }

    }
    */

    // IEnumerator ActivateElevatorText()
    // {
    //     // suspend execution for 5 seconds
    //     hud.transform.GetChild(0).gameObject.SetActive(true);
    //             textboximage.enabled = true;

    //             yield return new WaitForSeconds(5);
    //             hud.transform.GetChild(0).gameObject.SetActive(false);
    //             textboximage.enabled = false;
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Item_Manager Items = ItemManager.GetComponent<Item_Manager>();
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         if (collision.gameObject.CompareTag("Player") && !Items.Has_CardKey)
    //         {
    //             StartCoroutine("ActivateElevatorText");
    //         }
    //     }
    // }


}
