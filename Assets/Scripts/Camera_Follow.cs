using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (this.gameObject == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {

        }
        else
        {
            Vector3 dir = player.transform.position - this.transform.position;
            Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
            this.transform.Translate(moveVector);
        }
    }
}
