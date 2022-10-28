using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_HUD : MonoBehaviour
{
    public GameObject player;
    public GameObject[] hearts;
    public GameObject ammoText;   // Text representing ammo amount

    public Image monologueBox;
    public Image panel;
    [Range(0.5f, 30f)]
    public float timeBeforeHideMonologueBox = 5f;

    private static bool playIntroMonologue = true;

    private void Awake()
    {
        transform.Find("GameOver").gameObject.SetActive(false);
        transform.Find("Stage3_TextBox").gameObject.SetActive(false);
        transform.Find("Panel").gameObject.SetActive(false);
        
    }
    private void Start()
    {

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


        if(GameObject.Find("Complete"))
        {
            transform.Find("GameOver").gameObject.SetActive(true);
        }


        // Hide infinity symbol by default
        //infinitySymbol.SetActive(false);

    }

    public void UpdateHealthHearts()
    {
        if (player)
        {
            var combatComp = player.GetComponent<CombatComponent>();
            if (combatComp)
            {
                float healthVal = combatComp.GetHealth();
                for (var i = 0; i < hearts.Length; ++i)
                {
                    var imageComp = hearts[i].GetComponent<Image>();

                    if (healthVal > (i + 1) * (0.33f * combatComp.maxHealth)) // Display full heart
                    {
                        imageComp.fillAmount = 1f;
                    }
                    else if (healthVal > (i + 1) * (0.33f * combatComp.maxHealth) - (0.33f * combatComp.maxHealth * 0.5f)) // Display half heart
                    {
                        imageComp.fillAmount = 0.5f;
                    }
                    else // Hide heart
                    {
                        imageComp.fillAmount = 0f;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Player object not set in HUD!");
        }
    }

    public void UpdateAmmoAmount(int ammoAmount)
    {
        var textComp = ammoText.GetComponent<Text>();
        if (textComp)
        {
            textComp.text = ammoAmount.ToString();
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

    public void Go_Menu()
    {
        var objs = FindObjectsOfType<DontDestroyObject>();
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i].transform.gameObject);
            //Debug.Log(objs[i].name);
        }

        SceneManager.LoadScene("Start Menu");
    }
    public void Exit_Game()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while(fadeCount < 2.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            panel.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
