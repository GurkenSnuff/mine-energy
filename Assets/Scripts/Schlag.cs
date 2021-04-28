using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schlag : MonoBehaviour
{
    public GameObject enemyHitbox,LifeBar;
    public Leben leben;

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject== enemyHitbox)
        {
            leben.Life -= 10;
            LifeBar.SetActive(true);
            
            
        }
        
    }
    
}
