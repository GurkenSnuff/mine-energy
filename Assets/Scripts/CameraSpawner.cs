using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraSpawner : NetworkBehaviour
{
    public GameObject Camera;
    public GameObject a;
    void Start()
    {
        if (isLocalPlayer) a = Instantiate(Camera);
    }

    void LateUpdate()
    {
        
            if (isLocalPlayer) a.transform.position = new Vector3(transform.position.x, transform.position.y, a.transform.position.z);
    }
}
