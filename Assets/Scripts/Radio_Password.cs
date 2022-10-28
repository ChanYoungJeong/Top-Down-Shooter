using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio_Password : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject ItemManager;
    Item_Manager Item_Component;
    private bool is_collide = false;
    public Image textboximage;
    GameObject hud;

    // Start is called before the first frame update
    void Awake()
    {
        ItemManager = GameObject.Find("ItemManager");
        Item_Component = ItemManager.GetComponent<Item_Manager>();
        hud = GameObject.Find("HUD").transform.Find("PasswordTextBox").gameObject;
        textboximage.enabled = false;
        hud.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_collide)
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
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Only Player can pick up items
        if (collision.gameObject.name != "Player")
        {
            //Debug.Log(Items);
            return;
        }
        else if (collision.gameObject.name == "Player")
        {
            is_collide = true;
            Item_Component.Has_Password = true;
        }
    }
}
