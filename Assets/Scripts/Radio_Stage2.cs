using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Radio_Stage2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool has_Screen;
    bool has_Battery;
    bool has_Password;
    private bool is_collide = false;
    public Image textboximage;
    GameObject hud;
    //public bool Radio_Fixed = false;

    private bool text1 = false;
    private bool text2 = false;
    private bool text3 = false;
    private bool text4 = false;
    private bool text0 = false;

    GameObject ItemManager;

    public Image monologueBox;

    [Range(0.5f, 30f)]
    public float timeBeforeHideMonologueBox = 5f;

    private static bool playIntroMonologue = true;
    private bool is_done;

    void Start()
    {
        ItemManager = GameObject.Find("ItemManager");
        hud = GameObject.Find("HUD").transform.Find("RadioTextBox").gameObject;
        textboximage.enabled = false;
        hud.transform.GetChild(0).gameObject.SetActive(false);
        hud.transform.GetChild(1).gameObject.SetActive(false);
        hud.transform.GetChild(2).gameObject.SetActive(false);
        hud.transform.GetChild(3).gameObject.SetActive(false);

        if (playIntroMonologue)
        {
            playIntroMonologue = false;

            // Hide monologue box after a few seconds
            Invoke("HideMonologueBox", timeBeforeHideMonologueBox);
        }
        else
        {
            HideMonologueBox();
        }
    }

    // Update is called once per frame
    void Update()
    {
                if (is_collide)
        {
            Time.timeScale = 0f;
            textboximage.enabled = true;
            //hud.transform.GetChild(0).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space) && text4 == true)
            {
                text4 = false;
                hud.transform.GetChild(0).gameObject.SetActive(false);
                hud.transform.GetChild(1).gameObject.SetActive(false);
                hud.transform.GetChild(2).gameObject.SetActive(false);
                hud.transform.GetChild(3).gameObject.SetActive(false);
                textboximage.enabled = false;
                Time.timeScale = 1f;
                is_collide = false;
                //StartCoroutine(Delay());
            }

            if (Input.GetKeyDown(KeyCode.Space) && text3 == true)
            {
                hud.transform.GetChild(2).gameObject.SetActive(false);
                hud.transform.GetChild(3).gameObject.SetActive(true);
                text4 = true;
                text3 = false;
                //StartCoroutine(Delay());
            }
            else if (Input.GetKeyDown(KeyCode.Space) && text2 == true)
            {
                text2 = false;
                text3 = true;
                hud.transform.GetChild(1).gameObject.SetActive(false);
                hud.transform.GetChild(2).gameObject.SetActive(true);
                //StartCoroutine(Delay());
            }
            else if (Input.GetKeyDown(KeyCode.Space) && text1 == true && text2 == false && text3 == false)
            {
                text1 = false;
                text2 = true;
                hud.transform.GetChild(0).gameObject.SetActive(false);
                hud.transform.GetChild(1).gameObject.SetActive(true);
                //StartCoroutine(Delay());
            }
            else if (Input.GetKeyDown(KeyCode.Space)&& text0 == false && text1 == false)
            {
                text1 = true;
                text0 = true;
                //StartCoroutine(Delay());
            }

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        Item_Manager Items = ItemManager.GetComponent<Item_Manager>();

        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            return;
        }

        if (collision.gameObject.CompareTag("Player") && Items.Has_Battery && Items.Has_Password && Items.Has_Screen && !is_done)
        {
            hud.transform.GetChild(0).gameObject.SetActive(true);
            is_collide = true;
            //You can add dalogue here
            Items.Radio_Fixed = true;
            is_done = true;
        }
    }

    private void HideMonologueBox()
    {
        if (monologueBox)
        {
            monologueBox.enabled = false;
            for (var i = 0; i < monologueBox.transform.childCount; ++i)
            {
                monologueBox.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
