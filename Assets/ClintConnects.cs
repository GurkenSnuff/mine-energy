using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClintConnects : NetworkManager
{
    public NetworkConnection connectionDisconnect;
    public bool disconnect=false;
    
   public override void OnServerDisconnect(NetworkConnection conn)
    {
        connectionDisconnect = conn;
        disconnect = true;
        
    }

}

