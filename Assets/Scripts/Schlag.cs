using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Schlag : NetworkBehaviour
{
    private bool übergang = false,s=true;
    public GameObject LifeBar;
    public Life life;
    public abbauen abbauen;
    private float LifeDamage, DamageMult = 1;
    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pickage"))
        {

            if (abbauen.eisen == true)
            {
                DamageMult = 1.5F;
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
            life.Leben -= LifeDamage;
            LifeBar.SetActive(true);
            
            
        }
        
    }
    
}

    


