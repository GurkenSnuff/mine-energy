using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class testauthority : NetworkBehaviour
{
    bool t = false;
    public GameObject s;
    
    
    void Update()
    {
        if (isServer)
        {
            //print("a")
            //s.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
        }
    }
    
}
