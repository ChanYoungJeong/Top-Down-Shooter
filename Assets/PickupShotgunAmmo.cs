using UnityEngine;

public class PickupShotgunAmmo : MonoBehaviour
{
    public int addShotgunAmmo = 10;

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
                shootingComp.AddShotgunAmmo(addShotgunAmmo);
                Destroy(gameObject);
            }
        }
    }
}
