using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;



public class Miner : NetworkBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int e,Stein, HowManyMiner=0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber,Placement;
    public Text StoneCount;
    private SolarZellen solarZellen;
    public int MinenAnzahl;
    public bool EnoughForM = false;
    public bool TileUpdateCheck = false;
    private Seller seller;
    private DiamondMiner diamondMiner;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private Ressourcen ressourcen;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;

    void Awake()
    {
        goldSeller = FindObjectOfType<GoldSeller>();
        steinSeller = FindObjectOfType<SteinSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        seller = FindObjectOfType<Seller>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner= FindObjectOfType<GoldMiner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        map = FindObjectOfType<Tilemap>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        StartCoroutine(SteinCounter());
    }


     void Update()
     {
       
        //if (Stein >= 100 && eisenMiner.Eisen>=50)
        //{
            if (Input.GetMouseButtonDown(0) && EnoughForM == true)
            {
                Placement= Camera.main.ScreenToWorldPoint(Input.mousePosition);
                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);
                if (t <= 1)
                {

                    minerÜber.x += 1;
                    t = mapManager.GetTileResistance(minerÜber);

                    if (t == 2)
                    {
                        TileUpdateCheck = true;
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        HowManyMiner += 1;
                        Stein -= 100;
                        eisenMiner.Eisen -= 50;
                    if (isLocalPlayer)
                    {
                        SentTileUpdateToServer(Placement);
                    }
                }
                    else
                    {
                        e = 1;
                    }




                   
                    if (e == 1)
                    {
                    minerÜber.x -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (t == 2)
                        {
                            TileUpdateCheck = true;
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            Stein -= 100;
                            eisenMiner.Eisen -= 50;
                        if (isLocalPlayer)
                        {
                            SentTileUpdateToServer(Placement);
                        }
                    }
                        else
                        {
                            e = 2;
                        }
                    }

                    
                    if (e == 2)
                    {
                    minerÜber.x += 1;
                    minerÜber.y += 1;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (t == 2)
                        {
                            TileUpdateCheck = true;
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            Stein -= 100;
                            eisenMiner.Eisen -= 50;
                        if (isLocalPlayer)
                        {
                            SentTileUpdateToServer(Placement);
                        }
                    }
                        else
                        {
                            e = 3;
                        }
                    }


                    
                    if (e == 3)
                    {
                    minerÜber.y -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (t == 2)
                        {
                            TileUpdateCheck = true;
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            Stein -= 100;
                            eisenMiner.Eisen -= 50;
                        if (isLocalPlayer)
                        {
                            SentTileUpdateToServer(Placement);
                        }
                    }
                    }

                }

            }
        //}
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EnoughForM = false;
            }
        StoneCount.text = "Stein: " + Stein;


    }
    IEnumerator SteinCounter()
    {
        yield return new WaitForSeconds(1);
        MinenAnzahl = HowManyMiner;
        while (solarZellen.EnergyStand >= 0 && MinenAnzahl >= 1)
        {
            solarZellen.EnergyStand -= 1;
            ressourcen.Energy -= 1;
            Stein += 1;
            MinenAnzahl--;
            
        }

        StartCoroutine(SteinCounter());
    }
    public void MinerWillK()
    {
        EnoughForM = true;
        solarZellen.EnoughForSZ = false;
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
