using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    public GameObject hud;
    GameObject gameover;

    public float maxHealth = 30f;

    private float health = 1f;  // Cannot set to maxHealth here, gets updated to maxHealth in Start()
    private bool isDead = false;

    public bool is_knockback;         //Made by ChanYoung

    public GameObject[] itemsToDropOnDeath;

    public Shooting shootingRef = null;

    [Range(0f, 1f)]
    public float[] itemDropChances;

    // Start is called before the first frame update
    private void Start()
    {
        // Only Player should have a PersistentManager
        var persistentComp = GetComponent<PersistentManager>();
        if(persistentComp)
        {
            if(PersistentManager.isInitialized)
            {
                health = PersistentManager.health;
                Debug.Log($"For {gameObject.ToString()} health is {health}");
            }
            else
            {
                health = maxHealth;
            }

            UpdateHUD();
        }
        else
        {
            health = maxHealth;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    // Returns true if damage was applied, false otherwise
    public bool ApplyDamage(float damageAmount)
    {
        if (damageAmount <= 0 || isDead)
        {
            return false;
        }

        // Apply damage
        health = Mathf.Clamp(health - damageAmount, 0f, health);
        Debug.Log($"Applying {damageAmount} damage to {this.gameObject.name}, current health = {health}");

        UpdateHUD();
        
        if (health == 0)
        {
            Die();
            if (this.gameObject.tag == "Player")
            {
                Time.timeScale = 0f;
                hud.transform.Find("GameOver").gameObject.SetActive(true);
            }
        }

        return true;
    }
    public bool ApplyHealing(float healAmount)
    {
        if (healAmount <= 0 || isDead)
        {
            return false;
        }

        // Apply damage
        health = Mathf.Clamp(health + healAmount, health, maxHealth);
        Debug.Log($"Healing {this.gameObject.name} by {healAmount}, current health = {health}");

        UpdateHUD();

        return true;
    }

    // Returns true if knockback was applied, false otherwise
    public bool ApplyKnockback(Vector2 direction, float knockbackStrength)
    {
        if (isDead)
        {
            return false;
        }

        Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(knockbackStrength * direction, ForceMode2D.Impulse);
        is_knockback = true;


        return true;
    }

    private void Die()
    {
        Debug.Log($"{this.gameObject.name} has died!");
        isDead = true;

        if (itemsToDropOnDeath.Length > 0)
        {
            if (gameObject)
            {
                // Drop item
                var roll = Random.value;
                for(var i = 0; i < itemDropChances.Length; ++i)
                {
                    if(i > 0)
                    {
                        itemDropChances[i] += itemDropChances[i - 1];
                    }

                    //Debug.Log($"roll = {roll}, itemDropChances[i] = {itemDropChances[i]}");

                    // Note: breaking out of this loop if this is true
                    if(roll <= itemDropChances[i])
                    {
                        if(itemsToDropOnDeath[i])
                        {
                            Instantiate(itemsToDropOnDeath[i], gameObject.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Debug.LogError($"itemsToDropOnDeath[{i}] is null!");
                        }

                        break;
                        
                    }
                }
            }
        }

        Destroy(gameObject);
    }

    private void UpdateHUD()
    {
        if (hud)
        {
            var heartComp = hud.GetComponent<UI_HUD>();
            heartComp.UpdateHealthHearts();
        }
        else
        {
            if (gameObject.CompareTag("Player"))
            {
                Debug.LogWarning("HUD object not set in Player!");
            }
        }
    }

}
