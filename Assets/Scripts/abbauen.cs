using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Mirror;

public class abbauen : NetworkBehaviour
{
    private MapManager mapManager;

    

    public Animator animator;
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Miner miner;
    
    public bool Wait = true, eisen = false, gold = false, dia = false,diaprevent=false;
    float Distance_;
    public Text StoneCount, EisenCount,GoldCount,DiaCount;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private DiamondMiner diamondMiner;
    public GameObject skin1,skin2,skin3,skin4;
   
    
    
    private void Awake()
    {
        eisenMiner = FindObjectOfType<EisenMiner>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        miner = FindObjectOfType<Miner>();
        mapManager = FindObjectOfType<MapManager>();
        
    }

    void Update()
    {
        
        Hit();
        SkinUpdate();
        if (isLocalPlayer)
        {
            DiaCount.text = "Diamond: " + diamondMiner.Diamond;
            GoldCount.text = "Gold: " + goldMiner.Gold;
            EisenCount.text = "Eisen: " + eisenMiner.Eisen;
            StoneCount.text = "Stein: " + miner.Stein;
        }
    }

    private void Hit()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector3 minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float t = mapManager.GetTileResistance(minerÜber);
            Distance_ = Vector3.Distance(transform.position, minerÜber)-0.3f;
            if (hasAuthority)
            {
                animator.SetBool("Schlag", true);
            }
            if (Distance_ <= 18.1)
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
                    if (eisen == true&&t==4)
                    {
                        Wait = false;
                        StartCoroutine(GoldMiner());
                    }
                    if (gold == true && t == 5)
                    {
                        StartCoroutine(DiamondMining());
                        Wait = false;
                    }
                }
            }

        }
        else
        {

            if (hasAuthority)
            {
                animator.SetBool("Schlag", false);
            }

        }

        
    }
    void SkinUpdate()
    {
        if (eisen ==true&&gold==false )
        {
            if (isLocalPlayer)
            {
                SkinÜber(1);
            }
            
                
            
            Destroy(skin1.gameObject);
            skin2.SetActive(true);
        }
        if (gold == true&&dia==false)
        {
            if (isLocalPlayer)
            {
                SkinÜber(2);
            }
            Destroy(skin2.gameObject);
            skin3.SetActive(true);
        }
        if (dia == true)
        {
            if (isLocalPlayer)
            {
                SkinÜber(3);
            }
            Destroy(skin3.gameObject);
            skin4.SetActive(true);
        }
    }
    IEnumerator steinMining()
    {
        yield return new WaitForSeconds(1);
        if (eisen == true)
        {
            miner.Stein += 1;
        }
        if (gold == true)
        {
            miner.Stein += 1;
        }
        if (dia == true)
        {
            miner.Stein += 1;
        }
        miner.Stein += 1;
        Wait = true;
        
    }
    IEnumerator EisenMining()
    {
        yield return new WaitForSeconds(1);
        if (eisen == true)
        {
            eisenMiner.Eisen += 1;
        }
        if (gold == true)
        {
            eisenMiner.Eisen += 1;
        }
        
        eisenMiner.Eisen += 1;
        Wait = true;
        
    }
    IEnumerator GoldMiner()
    {
        yield return new WaitForSeconds(1);
        if (gold == true)
        {
            goldMiner.Gold += 1;
        }
        if (dia == true)
        {
            goldMiner.Gold += 1;
        }
        goldMiner.Gold += 1;
        Wait = true;
        
    }
    IEnumerator DiamondMining()
    {
        yield return new WaitForSeconds(1);
        diamondMiner.Diamond += 1;
        Wait = true;
        
    }
    public void EisenPickage()
    {
        eisen = true;
    }
    public void GoldPickage()
    {
        if (eisen == true)
        {
            gold = true;
        }
    }
    public void DiaPickage()
    {
        if (gold == true)
        {
            dia = true;
        }
    }
   [Command]
   public void SkinÜber(int i)
   {
        if (i == 1)
        {
            eisen = true;
            SkinÜberServer(i);
        }
        if (i == 2)
        {
            SkinÜberServer(i);
            gold = true;
        }
        if (i == 3)
        {
            SkinÜberServer(i);
            dia = true;
        }
    }
    [ClientRpc]
    public void SkinÜberServer(int i)
    {
        if (i == 1)
        {
            eisen = true;
        }
        if (i == 2)
        {
            gold = true;
        }
        if (i == 3)
        {
            dia = true;
        }
    }
}
