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

        
        
        
        
    }
    private void movementApplied()
    {
        
          rigidbody.velocity = new Vector2((move * speed), rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jump * speed));

    }
   

    

}
  


