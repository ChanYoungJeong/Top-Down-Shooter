using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string Scene_To_Load = "DemoLevel1";

    public void StartGame()
    {
        SceneManager.LoadScene(Scene_To_Load);
    }

}
