using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public string Scene_To_Load = "DemoLevel1";
    public static bool Game_Is_Paused = false;
    GameObject BGM_Player;
    GameObject ItemManager;
    GameObject Setting_Canvas;
    GameObject EventSystem;
    AudioSource backmusic;
    AudioMixer BGM_Mixer;
    GameObject Stage3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Stage3 = GameObject.Find("Tower");
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Game_Is_Paused)
            {
                close_Setting();
            }
            else
            {
                Open_Setting();
            }
        }
        
        if(Stage3)
        {
            
            backmusic.Stop();
        }
        else
        {
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.Play();
                //DontDestroyOnLoad(BGM_Player);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Scene_To_Load);
    }

    void Awake()
    {
        //DontDestroyOnLoad(this);
        BGM_Player = GameObject.Find("BGM_Player");
        backmusic = BGM_Player.GetComponent<AudioSource>(); //Save Back Ground Music


        //Item Manager
        ItemManager = GameObject.Find("ItemManager");
        DontDestroyOnLoad(ItemManager);

        //Settings
        Setting_Canvas = GameObject.Find("Setting Canvas");
        Setting_Canvas.SetActive(false);
        DontDestroyOnLoad(Setting_Canvas);

        //EventSystem
        EventSystem = GameObject.Find("EventSystem");
        DontDestroyOnLoad(EventSystem);
    }

    public void Open_Setting()
    {
        Setting_Canvas.SetActive(true);
        Time.timeScale = 0f;
        Game_Is_Paused = true;
    }

    public void close_Setting()
    {
        Setting_Canvas.SetActive(false);
        Time.timeScale = 1f;
        Game_Is_Paused = false;
    }

    public void Set_BGM_Volume(float volume)
    {
        backmusic.volume = volume;
    }
}
