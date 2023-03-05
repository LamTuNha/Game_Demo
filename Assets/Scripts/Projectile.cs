using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector2 moveSpeed = new Vector2(20f, 0);
    public Vector2 knockBack = new Vector2(0, 0);

    public int damage = 10;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Start() {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damageable damageable = collision.GetComponent<Damageable>(); 

        Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);

        if(damageable != null){
            bool gotHit = damageable.Hit(damage, deliveredKnockBack);

            if(gotHit){
            Debug.Log(collision.name + " hit for " + damage);
            Destroy(gameObject);
            }        
        }
    }
}
