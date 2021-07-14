using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class Seller : NetworkBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int  HowManySeller = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber;
    public bool TileUpdateCheck = false;
    private SolarZellen solarZellen;
    public int SellerAnzahl;
    public bool EnoughForS = false;
    private Miner miner;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;

    void Awake()
    {
        goldSeller = FindObjectOfType<GoldSeller>();
        steinSeller = FindObjectOfType<SteinSeller>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        miner = FindObjectOfType<Miner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        map = FindObjectOfType<Tilemap>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0) && EnoughForS == true)
        {
           // if (miner.Stein >= 50 && eisenMiner.Eisen >= 100)
           // {
                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);

                if (t == 0)
                {
                    miner.Stein -= 50;
                    eisenMiner.Eisen -= 100;
                    map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                    HowManySeller += 1;
                    TileUpdateCheck = true;
                    if (isLocalPlayer)
                    {
                        SentTileUpdateToServer(minerÜber);
                    }
                }
           // }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForS = false;
        }
    }
            
        [Command]
        void SentTileUpdateToServer(Vector3 position)
        {

            SentTileUpdateToClients(position);

            map.SetTile(map.WorldToCell(position), tiles[0]);
        }
        [ClientRpc]
        void SentTileUpdateToClients(Vector3 position)
        {

            map.SetTile(map.WorldToCell(position), tiles[0]);

        }
        



    

    
    
    public void SZWillK()
    {

        solarZellen.EnoughForSZ = false;
        miner.EnoughForM = false;
        EnoughForS = true;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
    }
}

