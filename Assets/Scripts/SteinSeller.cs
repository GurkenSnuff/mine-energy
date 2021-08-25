using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class SteinSeller : NetworkBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int HowManySeller = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    public Vector3 minerÜber;
    public bool TileUpdateCheck = false;
    private SolarZellen solarZellen;
    public int SellerAnzahl;
    public bool EnoughForSS = false;
    private Miner miner;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private Seller seller;
    private Ressourcen ressourcen;
    private GoldSeller goldSeller;

    void Awake()
    {
        map = FindObjectOfType<Tilemap>();
        goldSeller = FindObjectOfType<GoldSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        seller = FindObjectOfType<Seller>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        miner = FindObjectOfType<Miner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }


    void Update()
    {
        //if (miner.Stein >= 100 && ressourcen.Money >= 3000)
        //{
            if (Input.GetMouseButtonDown(0) && EnoughForSS == true)
            {
                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);
            
                if (t == 1)
                {
                    miner.Stein -= 100;
                    ressourcen.Money -= 3000;
                    TileUpdateCheck = true;
                    map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                    HowManySeller += 1;
                    if (isLocalPlayer)
                    {
                        SentTileUpdateToServer(minerÜber);
                    }

                }



           // }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForSS = false;
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
        seller.EnoughForS = false;
        miner.EnoughForM = false;
        EnoughForSS = true;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        goldSeller.EnoughForGS = false;
    }
}
