using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class SolarZellen : MonoBehaviour
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
    public int EnergyStand;
    public int EnergyCount=0,EnergySafe;
    
   private Miner miner;
    public bool  EnoughForSZ = false;
    private Seller seller;
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
        windGenerator = FindObjectOfType<WindGenerator>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        seller = FindObjectOfType<Seller>();
        miner = FindObjectOfType<Miner>();
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        StartCoroutine(EnergyCounting());
    }


    void Update()
    {
            if (Input.GetMouseButtonDown(0)&&EnoughForSZ == true)
            {
                Placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(Placement);
                if (t <= 1000)
                {
                if (t == 1)
                {
                   // if (miner.Stein>=100&&eisenMiner.Eisen>=50&&goldMiner.Gold>=40)
                   // {
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        EnergyCount++;
                        miner.Stein -= 100;
                        eisenMiner.Eisen -= 50;
                        goldMiner.Gold -= 40;
                         

                   // }
                }
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

}
