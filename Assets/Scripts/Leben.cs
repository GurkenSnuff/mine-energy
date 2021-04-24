using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leben : MonoBehaviour
{
    public int Life = 101,LifeVergleich=100;
    public GameObject LifeBar;
    
    void Update()
    {
        if (Life <= LifeVergleich)
        {
            LifeVergleich = Life-1;
            transform.localScale = new Vector3(transform.localScale.x-0.6F, 0.6000006F, 1);
            LifeBar.SetActive(true);
        }
    }
}
