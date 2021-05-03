using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public GameObject block;
    private int b; 
    void Start()
    {

        while (b>= -3)
        {
            b--;
            GameObject a = Instantiate(block) as GameObject;
            a.transform.position = new Vector3(transform.position.x, transform.position.y + b, transform.position.y);
        }
    }

    

}
