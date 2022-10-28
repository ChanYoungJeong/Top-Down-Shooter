using UnityEngine;

public class DebugDamageSource : MonoBehaviour
{
    public float damageOnHit = 20f;

    public float knockbackStrength = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatComponent combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if(combatComp)
        {
            combatComp.ApplyDamage(damageOnHit);

            Vector2 knockbackDirection = collision.transform.position - this.transform.position;
            combatComp.ApplyKnockback(knockbackDirection, knockbackStrength);
        }
    }
}
