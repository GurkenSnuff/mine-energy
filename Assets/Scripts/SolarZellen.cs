using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class SolarZellen : NetworkBehaviour
{
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private MapManager mapManager;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private float t;
    public Vector3 Placement;
    public int EnergyStand=0;
    public int EnergyCount=0,EnergySafe;
    
    public Miner miner;
    public bool  EnoughForSZ = false;
    public Seller seller;
    public bool TileUpdateCheck=false;
    public GameObject Hitbox;
    public DiamondMiner diamondMiner;
    public GoldMiner goldMiner;
    public EisenMiner eisenMiner;
    public WindGenerator windGenerator;
    public KohleGenerator kohleGenerator;
    public DoubleSeller doubleSeller;
    public SteinSeller steinSeller;
    public GoldSeller goldSeller;

    void Awake()
    {
        map = FindObjectOfType<Tilemap>();
        EnoughForSZ = false;
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        
        StartCoroutine(EnergyCounting());
    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)&&EnoughForSZ == true)
            {
            
            Placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(Placement);
            print(t);

            if (t == 0)
                {
                    
                    //if (miner.Stein>=100&&eisenMiner.Eisen>=50&&goldMiner.Gold>=40)
                   // {
                        map.SetTile(map.WorldToCell(Placement), tiles[0]);
                        EnergyCount++;
                        miner.Stein -= 100;
                        eisenMiner.Eisen -= 50;
                        goldMiner.Gold -= 40;
                        TileUpdateCheck = true;
                t = mapManager.GetTileResistance(Placement);
                 if (t >= 1 && t <= 5)
                 {
                     print("s");
                     GameObject a = Instantiate(Hitbox) as GameObject;
                     a.transform.position = map.WorldToCell(Placement);
                 }


                if (isLocalPlayer)
                     {
                        SentTileUpdateToServer(Placement);
                     }

                    // }
                }
            
                
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForSZ = false;
        }

    }
            
        
    
    IEnumerator EnergyCounting()
    {
        yield return new WaitForSeconds(1);
        EnergyStand = EnergyStand + EnergyCount;
        StartCoroutine(EnergyCounting());
        
    }
    public void SZWillK()
    {
        EnoughForSZ = true;
        miner.EnoughForM = false;
        seller.EnoughForS = false;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
        
    }
    [ClientRpc]
    void SentTileUpdateToClients(Vector3 position)
    {   
        
        map.SetTile(map.WorldToCell(position), tiles[0]);
        
    }
    [Command]
    void SentTileUpdateToServer(Vector3 position)
    {
        
        SentTileUpdateToClients(position);
       
        map.SetTile(map.WorldToCell(position), tiles[0]);
    }

}
