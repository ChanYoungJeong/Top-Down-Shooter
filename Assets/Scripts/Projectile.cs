using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 20f;
    public float knockbackStrength = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Player")))
        {
            // Do nothing if hit Player
            return;
        }

        CombatComponent combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if (combatComp)
        {
            combatComp.ApplyDamage(damage);

            Vector2 knockbackDirection = collision.transform.position - transform.position;
            combatComp.ApplyKnockback(knockbackDirection, knockbackStrength);

            //Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
