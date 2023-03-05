using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int  healthRestore = 50;

    public Vector3 spinRotationSpeed = new Vector3(0,180,0);

    AudioSource pickupSoure;

    void Awake(){
        pickupSoure = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable && damageable.Health<damageable.MaxHealth){
            bool wasHealed = damageable.Heal(healthRestore);
            if(wasHealed){
                if(pickupSoure){
                    AudioSource.PlayClipAtPoint(pickupSoure.clip, gameObject.transform.position, pickupSoure.volume);
                }
                Destroy(gameObject);
            }
            
        }
    }

    private void Update(){
        transform.eulerAngles += spinRotationSpeed* Time.deltaTime;
    }
}
