﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Clintconnects : NetworkManager
{
    public NetworkConnection connectionDisconnect;
    public bool disconnect=false;
    public mapGenerator mapGenerator;
    public int clintConnectCount = 0,clintDisConnectCount=0;
    
   public override void OnServerDisconnect(NetworkConnection conn)
   {
        clintDisConnectCount++;
        
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

