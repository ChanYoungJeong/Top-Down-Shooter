using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    bool has_key;
    GameObject ItemManager;
    void Start()
    {
        ItemManager = GameObject.Find("ItemManager");
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        Item_Manager Items = ItemManager.GetComponent<Item_Manager>();

        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            return;
        }

        if (collision.gameObject.CompareTag("Player") && Items.Radio_Fixed)
        {
            SceneManager.LoadScene("roof");
        }
    }
}
