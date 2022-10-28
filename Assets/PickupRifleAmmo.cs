using UnityEngine;

public class PickupRifleAmmo : MonoBehaviour
{
    public int addRifleAmmo = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only Player can pick up items
        if(!(collision.gameObject.CompareTag("Player")))
        {
            return;
        }

        CombatComponent combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if (combatComp)
        {
            var shootingComp = combatComp.shootingRef;
            if(shootingComp)
            {
                shootingComp.AddRifleAmmo(addRifleAmmo);
                Destroy(gameObject);
            }
        }
    }
}
