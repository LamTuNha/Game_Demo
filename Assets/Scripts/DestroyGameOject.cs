using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameOject : MonoBehaviour
{
 void OnTriggerEnter2D(Collider2D target)
    {        
        if (target.tag == "Destroy"){
            Destroy(gameObject);            
        }
    }
}
