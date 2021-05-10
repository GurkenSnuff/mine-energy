using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class test : NetworkBehaviour
{
    public TestLevel1 testLevel1;

    

    void Update()
    {
        if (isServer)
        {
            blockspawn();
        }
    }

    [ClientRpc]
    void blockspawn()
    {
        print(testLevel1.rand);
    }

}
