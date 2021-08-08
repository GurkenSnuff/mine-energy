using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;

public class GoldMiner : NetworkBehaviour
{
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    private float t;
    public int e, Gold, HowManyMiner = 0;
    public Tile[] tiles;
    [SerializeField]
    private Tilemap map;
    private Vector3 minerÜber,Placement;
    public Text StoneCount;
    private SolarZellen solarZellen;
    public int MinenAnzahl;
    public bool EnoughForG = false;
    private Miner miner;
    private Seller seller;
    private DiamondMiner diamondMiner;
    private EisenMiner einsenMiner;
    private WindGenerator windGenerator;
    private KohleGenerator kohleGenerator;
    private DoubleSeller doubleSeller;
    private Ressourcen ressourcen;
    private SteinSeller steinSeller;
    private GoldSeller goldSeller;
    public bool TileUpdateCheck = false;

    void Awake()
    {
        map = FindObjectOfType<Tilemap>();
        goldSeller = FindObjectOfType<GoldSeller>();
        steinSeller = FindObjectOfType<SteinSeller>();
        ressourcen = FindObjectOfType<Ressourcen>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        einsenMiner= FindObjectOfType<EisenMiner>();
        seller = FindObjectOfType<Seller>();
        miner = FindObjectOfType<Miner>();
        solarZellen = FindObjectOfType<SolarZellen>();
        mapManager = FindObjectOfType<MapManager>();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        StartCoroutine(SteinCounter());
    }


    void Update()
    {
        //if (ressourcen.Money>=1000)
      //  {
            if (Input.GetMouseButtonDown(0) && EnoughForG == true)
            {

                minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Placement = minerÜber;
                t = mapManager.GetTileResistance(minerÜber);
                if (t <= 1)
                {

                    minerÜber.x += 1;
                    t = mapManager.GetTileResistance(minerÜber);

                    if (t == 4)
                    {
                        map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                        HowManyMiner += 1;
                        ressourcen.Money -= 1000;
                        TileUpdateCheck = true;
                        if (isLocalPlayer)
                        {
                            SentTileUpdateToServer(Placement);
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
                        if (t == 4)
                        {
                            TileUpdateCheck = true;
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 1000;
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

                    minerÜber.x += 1;
                    minerÜber.y += 1;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 2)
                    {
                        if (t == 4)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 1000;
                            TileUpdateCheck = true;
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


                    minerÜber.y -= 2;
                    t = mapManager.GetTileResistance(minerÜber);
                    if (e == 3)
                    {
                        if (t == 4)
                        {
                            map.SetTile(map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), tiles[0]);
                            HowManyMiner += 1;
                            ressourcen.Money -= 1000;
                            TileUpdateCheck = true;
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
            EnoughForG = false;
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
        while (ressourcen.Energy >= 0 && MinenAnzahl >= 1)
        {
            solarZellen.EnergyStand -= 1;
            ressourcen.Energy -= 1;
            Gold += 1;
            MinenAnzahl--;
            StoneCount.text = "Gold: " + Gold;
        }

        StartCoroutine(SteinCounter());
    }
    public void MinerWillK()
    {
        EnoughForG = true;
        solarZellen.EnoughForSZ = false;
        seller.EnoughForS = false;
        einsenMiner.EnoughForE = false;
        diamondMiner.EnoughForD = false;
        windGenerator.EnoughForWG = false;
        kohleGenerator.EnoughForKG = false;
        doubleSeller.EnoughForDS = false;
        steinSeller.EnoughForSS = false;
        goldSeller.EnoughForGS = false;
    }
}
