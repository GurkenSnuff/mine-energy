using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class hudButtonsTest : NetworkBehaviour
{
    public NetworkManager NetworkManager;
    public kcp2k.KcpTransport kcp;
    public Text Text;

    public void Server()
    {
        kcp.Port = Convert.ToUInt16(Text.text);
        NetworkManager.StartServer();
        
    }
    public void Client()
    {
        kcp.Port = Convert.ToUInt16(Text.text);
        NetworkManager.StartClient();
        
    }
   
}
