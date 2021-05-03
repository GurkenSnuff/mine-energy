using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DoubleSeller : MonoBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int Money = 0, HowManySeller = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber;

    private SolarZellen solarZellen;
    public int SellerAnzahl;
    public bool EnoughForDS = false;
    private Miner miner;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private Seller seller;
    private SteinSeller steinSeller;
    private Ressourcen ressourcen;
    private GoldSeller goldSeller;

    void Awake()
    {
        goldSeller = FindObjectOfType<GoldSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        steinSeller = FindObjectOfType<SteinSeller>();
        seller = FindObjectOfType<Seller>();
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

        if (Input.GetMouseButtonDown(0) && EnoughForDS == true)
        {
            if (ressourcen.Money >= 1500)
            {
                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);

                if (t == 0)
                {

                    map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                    HowManySeller += 1;
                    ressourcen.Money -= 1500;

                }
            }


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForDS = false;
        }



    }



    public void DSWillK()
    {

        solarZellen.EnoughForSZ = false;
        miner.EnoughForM = false;
        EnoughForDS = true;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        seller.EnoughForS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
    }

}
