using UnityEngine;

public class PickupHeart : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only Player can pick up items
        if(!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        CombatComponent combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if (combatComp)
        {
            combatComp.ApplyHealing(healAmount);
            Destroy(gameObject);
        }
    }
}
