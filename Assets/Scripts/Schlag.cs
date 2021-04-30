using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schlag : MonoBehaviour
{
    public GameObject enemyHitbox,LifeBar;
    public Leben leben;
    public abbauen abbauen;
    private float LifeDamage, DamageMult=1;
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject== enemyHitbox)
        {
            if (abbauen.eisen == true)
            {
              DamageMult=1.5F;
            }
            if (abbauen.gold == true)
            {
                DamageMult = 2F;
            }
            if (abbauen.dia == true)
            {
                DamageMult = 2.5F;
            }
            LifeDamage = 10 * DamageMult;
            leben.Life -= LifeDamage;
            LifeBar.SetActive(true);
            
            
        }
        
    }
    
}
