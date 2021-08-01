using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class KohleGenerator :NetworkBehaviour
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
    public int EnergyCount = 0, EnergySafe, EnergyStand;
    
    private Miner miner;
    public bool EnoughForKG = false;
    private Seller seller;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private SolarZellen solarZellen;
    private WindGenerator windGenerator;
    private Ressourcen ressourcen;
    private DoubleSeller doubleSeller;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;
    public bool TileUpdateCheck = false;

    void Awake()
    {
        map = FindObjectOfType<Tilemap>();
        goldSeller = FindObjectOfType<GoldSeller>();
        StartCoroutine(EnergyCounting());
        steinSeller = FindObjectOfType<SteinSeller>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        windGenerator = FindObjectOfType<WindGenerator>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        seller = FindObjectOfType<Seller>();
        miner = FindObjectOfType<Miner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }
    IEnumerator EnergyCounting()
    {
        yield return new WaitForSeconds(1);
        if (miner.Stein >2&&EnergyCount>0)
        {
            miner.Stein -= 3;
            EnergyStand = EnergyStand + EnergyCount;
        }
        StartCoroutine(EnergyCounting());
    }
        void Update()
    {
        if (Input.GetMouseButtonDown(0) && EnoughForKG == true)
        {
            Placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            t = mapManager.GetTileResistance(Placement);
            
                if (t == 0)
                {
                   // if (ressourcen.Geld>=500)
                   // {
                    TileUpdateCheck = true;
                    map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        EnergyCount++;
                        ressourcen.Geld -= 500;
                if (isLocalPlayer)
                {
                    SentTileUpdateToServer(Placement);
                }
                //}
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForKG = false;
        }
        

    }

    public void WGWillK()
    {
        EnoughForKG = true;
        windGenerator.EnoughForWG = true;
        miner.EnoughForM = false;
        seller.EnoughForS = false;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        solarZellen.EnoughForSZ = false;
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
