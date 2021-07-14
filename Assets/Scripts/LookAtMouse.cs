using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class lookAtMouse : NetworkBehaviour
{
    public GameObject Camera;
    public CameraSpawner cameraSpawner;
    public Camera cameraReal;

    void Start()
    {
        //wird benötigt für die multiplayer kamera 
        StartCoroutine(wait());  
    }
   
    void LateUpdate()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0F);
        Vector3 targetMouse = cameraReal.ScreenToWorldPoint(Input.mousePosition) - position;
        float angle = (Mathf.Atan2(targetMouse.y, targetMouse.x) * Mathf.Rad2Deg) + 90F;

        if (isLocalPlayer)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        cameraSpawner = FindObjectOfType<CameraSpawner>();
        Camera = cameraSpawner.a.transform.GetChild(0).gameObject;
        cameraReal = Camera.GetComponent<Camera>();
    }
}
