using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreDoubleColliderCheck : MonoBehaviour
{
    
    public BoxCollider2D h;
    
     void OnCollisionEnter2D(Collision2D col)
     {
        if (col.gameObject.tag == "Collider")
        {
            Destroy(col.gameObject);
        }
        print("Hi");
     }
    
}
