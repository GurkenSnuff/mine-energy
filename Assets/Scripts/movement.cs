using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class movement : NetworkBehaviour
{
    

    private Rigidbody2D rigidbody;
    
    
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    
    public float speed=5F,move,jump;
    private Tilemap tilemap;
    
    
    
    

    private void Awake()
    {
        tilemap = FindObjectOfType<Tilemap>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            movementApplied();
        }
            
    }
    private void movementApplied()
    {

        jump = Input.GetAxisRaw("Vertical");
        move = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2((move * speed), rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jump * speed));

    }
   

    

}
  


