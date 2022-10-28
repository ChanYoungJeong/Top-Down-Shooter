using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public static bool isInitialized = false;  // When true, game already started

    public static float health;

    public static int total_Pistol_Ammo_num;
    public static int total_Rifle_Ammo_num;
    public static int total_Shotgun_Ammo_num;

    public static int currentAmmo_Pistol;
    public static int currentAmmo_Rifle;
    public static int currentAmmo_Shotgun;

    public static WeaponType weapontype;

    public static string entryLevel;

    // Start is called before the first frame update
    void Start()
    {
        if(!isInitialized && gameObject)
        {
            var combatComp = gameObject.GetComponent<CombatComponent>();
            if(combatComp)
            {
                health = combatComp.maxHealth;
            }

            isInitialized = true;
        }
    }
}
