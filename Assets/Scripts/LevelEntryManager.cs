using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntryManager : MonoBehaviour
{
    public string sceneToLoad;
    public bool isWinCondition = false; // End level when this is true

 
    GameObject ItemManager;
    void Start()
    {
        ItemManager = GameObject.Find("ItemManager");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item_Manager Items = ItemManager.GetComponent<Item_Manager>();
        // Only player can interact
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if(isWinCondition)
        {
            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        // Store variables in PersistentManager
        var combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if(combatComp)
        {
            PersistentManager.health = combatComp.GetHealth();

            var shootingComp = combatComp.shootingRef;
            if (shootingComp)
            {
                PersistentManager.total_Pistol_Ammo_num = shootingComp.total_Pistol_Ammo_num;
                PersistentManager.total_Rifle_Ammo_num = shootingComp.total_Rifle_Ammo_num;
                PersistentManager.total_Shotgun_Ammo_num = shootingComp.total_Shotgun_Ammo_num;

                PersistentManager.currentAmmo_Pistol = shootingComp.currentAmmo_Pistol;
                PersistentManager.currentAmmo_Rifle = shootingComp.currentAmmo_Rifle;
                PersistentManager.currentAmmo_Shotgun = shootingComp.currentAmmo_Shotgun;

                PersistentManager.weapontype = shootingComp.weapontype;
            }
        }

        var entryComp = collision.gameObject.GetComponent<EntryPoint>();
        if (entryComp)
        {
            PersistentManager.entryLevel = SceneManager.GetActiveScene().name;
        }

        
        if (collision.gameObject.CompareTag("Player") && Items.Has_CardKey && gameObject.name == "Elevator_Door1")
        {
            SceneManager.LoadScene("Basement1");
        }
        

        if (collision.gameObject.CompareTag("Player") && Items.Radio_Fixed)
        {
            SceneManager.LoadScene("roof");
        }

        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
