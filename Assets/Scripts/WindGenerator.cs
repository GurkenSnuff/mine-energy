using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class WindGenerator : MonoBehaviour
{
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private MapManager mapManager;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private float t;
    private Vector3 Placement;
    public int EnergyCount = 0, EnergySafe, EnergyStand;
    
    private Miner miner;
    public bool EnoughForWG = false;
    private Seller seller;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private SolarZellen solarZellen;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;

    void Awake()
    {
        StartCoroutine(EnergyCounting());
        goldSeller = FindObjectOfType<GoldSeller>();
        steinSeller = FindObjectOfType<SteinSeller>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
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
        
        EnergyStand = EnergyStand + EnergyCount;
        
        StartCoroutine(EnergyCounting());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EnoughForWG == true)
        {
            Placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            t = mapManager.GetTileResistance(Placement);
            
                if (t == 0)
                {
                    if (eisenMiner.Eisen >= 100&& diamondMiner.Diamond>=20)
                    {
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        EnergyCount++;
                        eisenMiner.Eisen -= 100;
                        diamondMiner.Diamond -= 20;

                    }
                }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForWG = false;
        }
        
    }
    
    public void WGWillK()
    {
        EnoughForWG = true;
        miner.EnoughForM = false;
        seller.EnoughForS = false;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        eisenMiner.EnoughForE = false;
        solarZellen.EnoughForSZ = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;

    }

}

