using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Clintconnects : NetworkManager
{
    public NetworkConnection connectionDisconnect;
    public bool disconnect=false;
    public mapGenerator mapGenerator;
    public int clintConnectCount = 0;
    
   public  void StopClient()
   {
        
        
        
   }
    public override void OnServerConnect(NetworkConnection conn)
    {
        mapGenerator.newClientJoined = true;
        clintConnectCount++;
    }
    public void hi()
    {

    }
}

