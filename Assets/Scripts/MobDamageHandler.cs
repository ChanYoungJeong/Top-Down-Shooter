using UnityEngine;

public class MobDamageHandler : MonoBehaviour
{
    public float damageOnHit = 20f;         // Damage done by this mob when attacking
    public float knockbackStrength = 20f;
    public Animator zombie_anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            // Only damage Player
            return;
        }

        CombatComponent combatComp = collision.gameObject.GetComponent<CombatComponent>();
        if (combatComp)
        {
            zombie_anim.SetTrigger("attack");
            combatComp.ApplyDamage(damageOnHit);
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            combatComp.ApplyKnockback(knockbackDirection, knockbackStrength);
        }
    }
}
