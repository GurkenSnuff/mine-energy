using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private MapManager mapManager;

    private Rigidbody2D rigidbody;
    
    public Animator animator;
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Miner miner;
    public float speed=5F,move,jump, adjustMovement;
    bool Wait = true,eisen=false,gold=false,dia=false;
    float Distance_;
    public Text StoneCount,EisenCount;
    public GameObject lookAtMouse;
    private EisenMiner eisenMiner;

    private void Awake()
    {
        eisenMiner = FindObjectOfType<EisenMiner>();
        
        miner = FindObjectOfType<Miner>();
        mapManager = FindObjectOfType<MapManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        jump = Input.GetAxisRaw("Vertical");
        move = Input.GetAxisRaw("Horizontal");
        
            movementApplied();

        LookAtMouse();
        Hit();
        
        
    }
    private void movementApplied()
    {
        
          rigidbody.velocity = new Vector2((move * speed), rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, (jump * speed));

    }
   private void Hit()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float t = mapManager.GetTileResistance(minerÜber);
            Distance_ = Vector3.Distance(transform.position, minerÜber);
            
            animator.SetBool("Schlag",true);
            if (Distance_ <= 9.7)
            {
                if (Wait == true)
                {
                    if (t == 2)
                    {

                        Wait = false;
                        StartCoroutine(steinMining());


                    }
                    if (t == 3)
                    {
                        Wait = false;
                        StartCoroutine(EisenMining());
                    }
                    
                    }
            }
            
        }
        else
        {
            animator.SetBool("Schlag", false);
        }
       
    }
    IEnumerator steinMining()
    {
        yield return new WaitForSeconds(1);
        miner.Stein += 1;
        Wait = true;
        StoneCount.text = "Stein: " + miner.Stein;
    }
    IEnumerator EisenMining()
    {
        yield return new WaitForSeconds(1);
      
            eisenMiner.Eisen += 1;
            Wait = true;
            EisenCount.text = "Eisen: " + eisenMiner.Eisen;
        

       
    }

    private void LookAtMouse()
    {
        Vector3 position = new Vector3(transform.position.x , transform.position.y, 0F);

        Vector3 targetMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
        float angle = (Mathf.Atan2(targetMouse.y, targetMouse.x) * Mathf.Rad2Deg)+90F;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
  


