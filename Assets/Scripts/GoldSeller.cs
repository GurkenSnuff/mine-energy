using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GoldSeller : MonoBehaviour
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
    private Vector3 minerÜber;

    private SolarZellen solarZellen;
    public int SellerAnzahl;
    public bool EnoughForGS = false;
    private Miner miner;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private SteinSeller steinSeller;
    private Seller seller;
    private Ressourcen ressourcen;
    public bool TileUpdateCheck = false;

    void Awake()
    {
        map = FindObjectOfType<Tilemap>();
        ressourcen = FindObjectOfType<Ressourcen>();
        seller = FindObjectOfType<Seller>();
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
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0) && EnoughForGS == true)
        {
            if (goldMiner.Gold >= 200 &&  ressourcen.Money>= 10000)
            {
                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);

                if (t == 0)
                {
                    goldMiner.Gold -= 200;
                   ressourcen.Money -= 10000;
                    map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                    HowManySeller += 1;
                    TileUpdateCheck = true;

                }
            }


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForGS = false;
        }



    }



    public void SZWillK()
    {

        solarZellen.EnoughForSZ = false;
        miner.EnoughForM = false;
        EnoughForGS = true;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        seller.EnoughForS = false;
        
    }

}
