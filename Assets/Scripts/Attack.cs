using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockBack = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision) {
        Damageable damageable = collision.GetComponent<Damageable>();

        Vector2 deliveredKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);

        if(damageable != null){
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockBack);

            if(gotHit){
            Debug.Log(collision.name + " hit for " + attackDamage);
            }

        }
    }

}
