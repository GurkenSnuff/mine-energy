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
    private Vector2 vector2;
    



    private void Awake()
    {
        tilemap = FindObjectOfType<Tilemap>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        
        if (isLocalPlayer)
        {
            rigidbody.velocity = Vector3.zero;
            movementApplied();
        }
            
    }
    private void movementApplied()
    {

        rigidbody.velocity = Vector3.zero;
        jump = Input.GetAxisRaw("Vertical");
        move = Input.GetAxisRaw("Horizontal");
        vector2 = new Vector2(move, jump);
        rigidbody.velocity = new Vector2((move * speed), rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jump * speed));
        if (vector2 == new Vector2(0F, 0F))
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }



    }
    



}
  


