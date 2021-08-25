using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class hudButtonsTest : NetworkBehaviour
{
    public NetworkManager NetworkManager;
   
    
    
        
    
    
    public void Server()
    {
        NetworkManager.StartServer();
        
    }
    public void Client()
    {
        NetworkManager.StartClient();
        
    }
   
}
