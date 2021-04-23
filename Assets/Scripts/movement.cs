using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    

    private Rigidbody2D rigidbody;
    
    public Animator animator;
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    
    public float speed=5F,move,jump;
    
    
    
    public GameObject lookAtMouse;
    

    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        jump = Input.GetAxisRaw("Vertical");
        move = Input.GetAxisRaw("Horizontal");
        
            movementApplied();

        LookAtMouse();
        
        
        
    }
    private void movementApplied()
    {
        
          rigidbody.velocity = new Vector2((move * speed), rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jump * speed));

    }
   

    private void LookAtMouse()
    {
        Vector3 position = new Vector3(transform.position.x , transform.position.y, 0F);

        Vector3 targetMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
        float angle = (Mathf.Atan2(targetMouse.y, targetMouse.x) * Mathf.Rad2Deg)+85F;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
  


