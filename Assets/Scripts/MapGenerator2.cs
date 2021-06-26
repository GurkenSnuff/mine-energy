using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapGenerator2 : NetworkBehaviour
{
    public GameObject block;
    private int b = 0;

    void Start()
    {

        
            
            while (b >= -3)
            {
                
                b--;
                GameObject a = Instantiate(block) as GameObject;
                a.transform.position = new Vector3(transform.position.x, transform.position.y + b, transform.position.y);
            }
        
    }
}
