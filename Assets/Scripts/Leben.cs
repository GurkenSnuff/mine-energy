using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leben : MonoBehaviour
{
    
    public float SchadensMult=1F,LifeBarDamage, Life = 101, LifeVergleich = 100;
    public GameObject Player;
    public abbauen abbauen;
    public bool wait=true;
    
    void Update()
    {
        if (abbauen.gold == true)
        {
            SchadensMult = 1.5F;
        }
        
        if (Life == 101)
        {
            gameObject.SetActive(false);
            
        }
        if (Life <= LifeVergleich)
        {
            LifeBarDamage = 0.6F * SchadensMult;
            LifeVergleich -= 10F*SchadensMult;
            transform.localScale = new Vector3(transform.localScale.x-LifeBarDamage, 0.6000006F, 1);

            StopAllCoroutines();
            StartCoroutine(LifeRegTimer());
            

        }
        
        if (Life<=1)
        {
            Destroy(Player.gameObject);
        }
        if ( wait == false)
        {
            StartCoroutine(LifeReg());
            wait = true;
        }
    }
    IEnumerator LifeRegTimer()
    {
        yield return new WaitForSeconds(3);
        wait = false;

    }
    IEnumerator LifeReg()
    {
        yield return new WaitForSeconds(1);
        
        Life += 10;

        LifeVergleich += 10;
        transform.localScale = new Vector3(transform.localScale.x + 0.6F, 0.6000006F, 1);
        StartCoroutine(LifeReg());


    }
}
