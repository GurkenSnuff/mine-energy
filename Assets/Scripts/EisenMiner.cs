using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class EisenMiner : NetworkBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int e,Eisen, HowManyMiner = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber;
    public Text StoneCount;
    private SolarZellen solarZellen;
    public int MinenAnzahl;
    public bool EnoughForE = false;
    private Miner miner;
    private Seller seller;
    private DiamondMiner diamondMiner;
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
        diamondMiner = FindObjectOfType<DiamondMiner>();
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
        if (ressourcen.Money>=400)
        {
            if (Input.GetMouseButtonDown(0) && EnoughForE == true)
            {

                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                t = mapManager.GetTileResistance(minerÜber);
                if (t <= 1)
                {

                    minerÜber.x += 1;
                    t = mapManager.GetTileResistance(minerÜber);

                    if (t == 3)
                    {
                        
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        HowManyMiner += 1;
                        ressourcen.Money -= 400;
                        TileUpdateCheck = true;
                        if (isLocalPlayer)
                        {
                            SentTileUpdateToServer(minerÜber);
                        }
                    }
                    else
                    {
                        e = 1;
                    }




                    minerÜber.x -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 1)
                    {
                        if (t == 3)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 400;
                            TileUpdateCheck = true;
                            if (isLocalPlayer)
                            {
                                SentTileUpdateToServer(minerÜber);
                            }
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
                        if (t == 3)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 400;
                            TileUpdateCheck = true;
                            if (isLocalPlayer)
                            {
                                SentTileUpdateToServer(minerÜber);
                            }
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
                        if (t == 3)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 400;
                            TileUpdateCheck = true;
                            if (isLocalPlayer)
                            {
                                SentTileUpdateToServer(minerÜber);
                            }
                        }
                    }

                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnoughForE = false;
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
    IEnumerator SteinCounter()
    {
        yield return new WaitForSeconds(1);
        MinenAnzahl = HowManyMiner;
        while (solarZellen.EnergyStand >= 0 && MinenAnzahl >= 1)
        {
            solarZellen.EnergyStand -= 1;
            ressourcen.Energy -= 1;
            Eisen += 1;
            MinenAnzahl--;
            StoneCount.text = "Eisen: " + Eisen;
        }

        StartCoroutine(SteinCounter());
    }
    public void MinerWillK()
    {
        EnoughForE = true;
        kohleGenerator.EnoughForKG = false;
        solarZellen.EnoughForSZ = false;
        seller.EnoughForS = false;
        goldMiner.EnoughForG = false;
        diamondMiner.EnoughForD = false;
        windGenerator.EnoughForWG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
    }
}
