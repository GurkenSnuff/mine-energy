using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Life : NetworkBehaviour
{
    [SyncVar(hook = "test")]
    public float Leben = 101;
    public float SchadensMult = 1F, LifeBarDamage, LifeVergleich = 100;
    public GameObject Player;
    public abbauen abbauen;
    public GameObject lebensbar;
    public SpriteRenderer spriteRenderer;
    private bool übergang = false, s = true;
    private GameObject parentGO;


    private float LifeDamage, DamageMult = 1;

    public bool wait = true;

    void Awake()
    {
        abbauen = FindObjectOfType<abbauen>();
    }

    void Update()
    {

        if (abbauen.eisen == true)
        {
            SchadensMult = 1.5F;
        }

        if (Leben >= 101)
        {
            spriteRenderer.enabled = false;
            Leben = 101;
        }

        




        if (Leben <= LifeVergleich)
        {
           
            LifeBarDamage = 0.6F * SchadensMult;
            LifeVergleich -= 10F * SchadensMult;
            lebensbar.transform.localScale = new Vector3(lebensbar.transform.localScale.x - LifeBarDamage, 0.6000006F, 1);

            StopAllCoroutines();
            StartCoroutine(LifeRegTimer());
            spriteRenderer.enabled = true;
        }

        if (Leben<= 1)
        {
            Destroy(Player.gameObject);
            
        }
        if (wait == false)
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

        Leben += 10;

        LifeVergleich += 10;
        lebensbar.transform.localScale = new Vector3(lebensbar.transform.localScale.x + 0.6F, 0.6000006F, 1);
        StartCoroutine(LifeReg());
        
    }
    public void test(float s,float t)
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
            Leben -= LifeDamage;
            
            
            

        }

    }
    
}
