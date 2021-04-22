using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    
     private MapManager mapManager;

    private Rigidbody2D rigidbody;
    public bool gotHit = false;
    public GameObject bone1;


    public float speed = 10F, move, jump, adjustMovement;
    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        
        
           
        
         lookAtMouse();

    }
    
    private void lookAtMouse()
    {
        Vector3 position = new Vector3(bone1.transform.position.x -0.081F, bone1.transform.position.y+0.748F , 0F);
        
        Vector3 targetMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) -position ;
        float angle = (Mathf.Atan2(targetMouse.y, targetMouse.x) * Mathf.Rad2Deg) -80F;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
     
}
