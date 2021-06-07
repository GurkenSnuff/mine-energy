using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class DiamondMiner : MonoBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int e, Diamond, HowManyMiner = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber;
    public Text StoneCount;
    private SolarZellen solarZellen;
    public int MinenAnzahl;
    public bool EnoughForD = false;
    private Miner miner;
    private Seller seller;
    private EisenMiner eisenMiner;
    private GoldMiner goldMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private Ressourcen ressourcen;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;
    public bool TileUpdateCheck = false;

    void Awake()
    {
        goldSeller = FindObjectOfType<GoldSeller>();
        steinSeller = FindObjectOfType<SteinSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        seller = FindObjectOfType<Seller>();
        miner = FindObjectOfType<Miner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        StartCoroutine(SteinCounter());
    }


    void Update()
    {
        if (ressourcen.Money>=2000)
        {
            if (Input.GetMouseButtonDown(0) && EnoughForD == true)
            {

                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);
                if (t <= 1)
                {

                    minerÜber.x += 1;
                    t = mapManager.GetTileResistance(minerÜber);

                    if (t == 5)
                    {
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        HowManyMiner += 1;
                        ressourcen.Money -= 2000;
                        TileUpdateCheck = true;
                    }
                    else
                    {
                        e = 1;
                    }




                    minerÜber.x -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 1)
                    {
                        if (t == 5)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 2000;
                            TileUpdateCheck = true;
                        }
                        else
                        {
                            e = 2;
                        }
                    }

                    minerÜber.x += 1;
                    minerÜber.y += 1;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 2)
                    {
                        if (t == 5)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 2000;
                            TileUpdateCheck = true;
                        }
                        else
                        {
                            e = 3;
                        }
                    }


                    minerÜber.y -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 3)
                    {
                        if (t == 5)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 2000;
                            TileUpdateCheck = true;
                        }
                    }

                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForD = false;
        }



    }
    IEnumerator SteinCounter()
    {
        yield return new WaitForSeconds(1);
        MinenAnzahl = HowManyMiner;
        while (solarZellen.EnergyStand >= 0 && MinenAnzahl >= 1)
        {
            solarZellen.EnergyStand -= 1;
            ressourcen.Energy -= 1;
            Diamond += 1;
            MinenAnzahl--;
            StoneCount.text = "Diamanten: " + Diamond;
        }

        StartCoroutine(SteinCounter());
    }
    public void MinerWillK()
    {
        EnoughForD = true;
        solarZellen.EnoughForSZ = false;
        seller.EnoughForS = false;
        eisenMiner.EnoughForE = false;
        goldMiner.EnoughForG = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
    }
}
