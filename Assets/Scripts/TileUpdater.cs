using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Tilemaps;

public class TileUpdater : NetworkBehaviour
{
    public mapGenerator mapGenerator;
    public GameObject h;
    public Clintconnects clintConnects;
    private SolarZellen solarZellen;
    private KohleGenerator kohleGenerator;
    private SteinSeller steinSeller;
    private Seller seller;
    private Miner miner;
    private GoldSeller goldSeller;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private DoubleSeller doubleSeller;
    private DiamondMiner diamondMiner;
    public List<TileBase> deleteTiles = new List<TileBase>();
    [SyncVar]
    public List<Vector3> tileUpdatertiles = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesK = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesSS = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesS = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesM = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesGS = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesGM = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesEM = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesDS = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesDM = new List<Vector3>();
    [SyncVar]
    public List<Vector3> tileUpdatertilesWG = new List<Vector3>();
    public List<TileBase> updateTiles = new List<TileBase>();
    bool t = false,s=false,K=false,w=false,S=false,DS=false,SS=false,GS=false,M=false,EM=false,GM=false,DM=false;
    private Tilemap map;
    public int deleteCount=-1;
    private Vector3 z,k,z3,i,d,u,y;
    private NetworkConnection networkConnection;
    private MapManager mapManager;
    public List<int> propertys = new List<int>();
    public bool Alive = false;
    private WindGenerator windGenerator;
    private int deleteCount2 = -1, x3 = -1, x4 = -1,U=-1, x = -1, x2 = -1, T = -1, j = 0, I = -1, D = -1,Y=-1;
    public GameObject newPlayer;
    [SerializeField]
    private List<TileBase> junkTiles = new List<TileBase>();
    private List<Vector3> positions = new List<Vector3>();
    private bool transmit = false;
    private Vector3 z2;
    public Tile[] tiles;
    private List<Vector3> positionColliders = new List<Vector3>();
    [SerializeField]
    private GameObject collider;
    public bool oneTimeSpawner = true,c=false;
    [SerializeField]
    private NetworkTransform networkTransform;
    

    void Awake()
    {
        mapGenerator = FindObjectOfType<mapGenerator>();
        windGenerator = FindObjectOfType<WindGenerator>();
        mapManager = FindObjectOfType<MapManager>();
        clintConnects = FindObjectOfType<Clintconnects>();
        solarZellen = FindObjectOfType<SolarZellen>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        steinSeller = FindObjectOfType<SteinSeller>();
        seller = FindObjectOfType<Seller>();
        miner = FindObjectOfType<Miner>();
        goldSeller = FindObjectOfType<GoldSeller>();
        goldMiner = FindObjectOfType<GoldMiner>();
        eisenMiner = FindObjectOfType<EisenMiner>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        map = FindObjectOfType<Tilemap>();
        Alive = true;
        if (isLocalPlayer) test();
        StartCoroutine(Waitforplayer());
        

    }


    void Start()
    {
        GetLifeList();
    }

    [Command]
    void GetLifeList()
    {
        int x = 0;
        foreach (var variable in mapGenerator.TilePosition)
        {
            mapGenerator.newlyJoined(connectionToClient,mapGenerator.TileLife[x],mapGenerator.TilePosition[x]);
            x++;
        }
        
    }

    void Update()
    {
        WindGenerator();
        SolarZellen();
            KohleGenerator();
        Seller();
        DoubleSeller();
        SteinSeller();
        GoldSeller();
        Miner();
        Eisenminer();
        Goldminer();
        Diamondminer();
        // first try of spawning only players who are near you
        //if(isServer)PlayerSpawner();
    }

    private void PlayerSpawner()
    {
        int x = 0,z=0;
        foreach(var h in mapGenerator.Player)
        {
           
            if (mapGenerator.Player[x] != gameObject)
            {
                float Distance = Vector3.Distance(mapGenerator.Player[x].transform.position, gameObject.transform.position);
                
                if (Distance <= 10)
                {
                    if (c == false)
                    {
                        networkTransform.enabled = true;
                        c = true;
                    }
                }
                else
                {
                    networkTransform.enabled = false;
                    c = false;
                }
            }
            x++;
        }
    }

    

    

    IEnumerator Waitforplayer()
    {
        yield return new WaitForSeconds(0.1f);
        test();
    }
    

    [Command]
        private void test()
        {
        
            mapGenerator.Player.Add(gameObject);
            mapGenerator.Clients.Add(connectionToClient);
            mapGenerator.tileupdater.Add(gameObject.GetComponent<TileUpdater>());
        }

    public void terrainSpawnerStarter(NetworkConnection conn, Vector3 position, int TileType)
    {
        
        terrainSpawner(conn, position, TileType);
    }
    [TargetRpc]
    public void terrainSpawner(NetworkConnection conn, Vector3 position, int TileType)
    {
        
        map.SetTile(map.WorldToCell(position), tiles[TileType]);
        

    }
    public void terrainSpawnerAfterDespawnedStart(NetworkConnection conn, Vector3 position,int TileType,double Life)
    {
        
        terrainSpawnerAfterDespawned(conn, position,TileType,Life);
    }
    [TargetRpc]
    public void terrainSpawnerAfterDespawned(NetworkConnection conn, Vector3 position,int TileType,double Life)
    {
        GameObject a = Instantiate(collider) as GameObject;
        a.transform.position = position;
        //Versuch die Leben zu behalten auch nach despawnen
        //Eine Liste für die Leben und eine für die positionen dann schauen ob eine position zu dem collider passt und dann passendes leben nehmen
        

    }

    public void terrainDespawnerStarter(NetworkConnection conn, Vector3 position)
    {
        terrainDespawner(conn, position);
    }
    [TargetRpc]
    void terrainDespawner(NetworkConnection conn, Vector3 position)
    {
        int x = 0;
        bool alreadyThere = true;
        
        foreach(var Variable in positionColliders)
        {
            
            if (positionColliders[x] != position)
            {
                alreadyThere = false;
            }
            x++;
        }
        if (alreadyThere == false)
        {
            positionColliders.Add(position);
        }
        map.SetTile(map.WorldToCell(position), null);
    }
    



    [ClientRpc]
    public void updateTile(Vector3 number,int tile)
    {

        
        if (tile == -1)
        {
            map.SetTile(map.WorldToCell(number), deleteTiles[0]);
        }
        else
        {

            map.SetTile(map.WorldToCell(number), updateTiles[tile]);
        }
        
    }
    [Command]
    private void updateTileServer(Vector3 number,int tile)
    {
        
        if (tile == -1)
        {
            
            
            map.SetTile(map.WorldToCell(number), deleteTiles[0]);
        }
        else
        {
            
                if (tile == 1)
                {
                    
                    K = true;
                }
                if (tile == 0)
                {

                t = true;
                }
                if (tile == 2)
                {

                     
                     w= true;
                }
            if (tile == 3)
            {
                
                S = true;
            }
            if (tile == 4)
            {
                DS = true;
            }
            if (tile == 5)
            {
                SS = true;
            }
            if (tile == 6)
            {
                GS = true;
            }
            if (tile == 7)
            {
                M = true;
            }
            if (tile == 8)
            {
                EM = true;
            }
            if (tile == 9)
            {
                GM = true;
            }
            if (tile == 10)
            {
                DM = true;
            }

            map.SetTile(map.WorldToCell(number), updateTiles[tile]);
        }
        
        updateTile(number, tile);
        
    }
    private void SolarZellen()
    {
        if (solarZellen.TileUpdateCheck == true )
        {
            

            solarZellen.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertiles.Add(solarZellen.Placement);
                deleteCount++;
                propertys.Add(deleteCount);
                
                foreach (var variable in tileUpdatertiles)
                {

                    x++;
                    z = tileUpdatertiles[x];
                    updateTileServer(z, 0);


                }
            }
            x = -1;
        }


        if (t == true )
        {
            
            
           
            if (isServer)
            {
                
                foreach (var variable in tileUpdatertiles)
                {

                    x++;
                    z = tileUpdatertiles[x];

                    updateTile(z, 0);

                }
            }
            x = -1;
        }
    }
    private void WindGenerator()
    {
        
        if (windGenerator.TileUpdateCheck == true)
        {


            windGenerator.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesWG.Add(windGenerator.Placement);
                

                foreach (var variable in tileUpdatertilesWG)
                {

                    x3++;
                    z2 = tileUpdatertilesWG[x3];
                    updateTileServer(z2, 2);
                    

                }
            }
            x3 = -1;
        }


        if (w == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesWG)
                {

                    x3++;
                    z2 = tileUpdatertilesWG[x3];

                    updateTile(z2, 2);

                }
            }
            x3 = -1;
        }
    }

    private void KohleGenerator()
    {
        if (kohleGenerator.TileUpdateCheck == true )
        {
            tileUpdatertilesK.Add(kohleGenerator.Placement);
            
            kohleGenerator.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                
                foreach (var variable in tileUpdatertilesK)
                {
                    
                    T++;
                    k = tileUpdatertilesK[T];
                    updateTileServer(k, 1);
                    

                }
            }
            
            T = -1;
        }

        if (K == true  )
        {
            //K = false;
            if (isServer)
            {
                
                foreach (var variable in tileUpdatertilesK)
                {
                    
                    T++;
                    k = tileUpdatertilesK[T];

                    updateTile(k, 1);

                }
            }
            
            T = -1;
        }
    }
    private void Seller()
    {
        int x3 = -1;
        Vector3 z2;
        if (seller.TileUpdateCheck == true)
        {


            seller.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesS.Add(seller.minerÜber);


                foreach (var variable in tileUpdatertilesS)
                {

                    x3++;
                    z2 = tileUpdatertilesS[x3];
                    updateTileServer(z2, 3);


                }
            }
            x3 = -1;
        }


        if (S == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesS)
                {

                    x3++;
                    z2 = tileUpdatertilesS[x3];

                    updateTile(z2, 3);

                }
            }
            x3 = -1;
        }
    }
    private void DoubleSeller()
    {
        int x6 = -1;
        Vector3 z5;
        if (doubleSeller.TileUpdateCheck == true)
        {


            doubleSeller.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesDS.Add(doubleSeller.Placement);


                foreach (var variable in tileUpdatertilesDS)
                {

                    x6++;
                    z5 = tileUpdatertilesDS[x6];
                    updateTileServer(z5, 4);


                }
            }
            x6 = -1;
        }


        if (DS == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesDS)
                {

                    x6++;
                    z5 = tileUpdatertilesDS[x6];

                    updateTile(z5, 4);

                }
            }
            x6 = -1;
        }
    }
    private void SteinSeller()
    {
        int x3 = -1;
        Vector3 z2;
        if (steinSeller.TileUpdateCheck == true)
        {


            steinSeller.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesSS.Add(steinSeller.minerÜber);


                foreach (var variable in tileUpdatertilesSS)
                {

                    x3++;
                    z2 = tileUpdatertilesSS[x3];
                    updateTileServer(z2, 5);


                }
            }
            x3 = -1;
        }


        if (SS == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesSS)
                {

                    x3++;
                    z2 = tileUpdatertilesSS[x3];

                    updateTile(z2, 5);

                }
            }
            x3 = -1;
        }
    }
    private void GoldSeller()
    {
        int x3 = -1;
        Vector3 z2;
        if (goldSeller.TileUpdateCheck == true)
        {


            goldSeller.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesGS.Add(goldSeller.minerÜber);


                foreach (var variable in tileUpdatertilesGS)
                {

                    x3++;
                    z2 = tileUpdatertilesGS[x3];
                    updateTileServer(z2, 6);


                }
            }
            x3 = -1;
        }


        if (GS == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesGS)
                {

                    x3++;
                    z2 = tileUpdatertilesGS[x3];

                    updateTile(z2, 6);

                }
            }
            x3 = -1;
        }
    }
    private void Miner()
    {
        int x3 = -1;
        Vector3 z2;
        if (miner.TileUpdateCheck == true)
        {


            miner.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesM.Add(miner.Placement);


                foreach (var variable in tileUpdatertilesM)
                {

                    x3++;
                    z2 = tileUpdatertilesM[x3];
                    updateTileServer(z2, 7);


                }
            }
            x3 = -1;
        }


        if (M == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesM)
                {

                    x3++;
                    z2 = tileUpdatertilesM[x3];

                    updateTile(z2, 7);

                }
            }
            x3 = -1;
        }
    }
    private void Eisenminer()
    {
        int x3 = -1;
        Vector3 z2;
        if (eisenMiner.TileUpdateCheck == true)
        {


            eisenMiner.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesEM.Add(eisenMiner.Placement);


                foreach (var variable in tileUpdatertilesEM)
                {

                    x3++;
                    z2 = tileUpdatertilesEM[x3];
                    updateTileServer(z2, 8);


                }
            }
            x3 = -1;
        }


        if (EM == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesEM)
                {

                    x3++;
                    z2 = tileUpdatertilesEM[x3];

                    updateTile(z2, 8);

                }
            }
            x3 = -1;
        }
    }
    private void Goldminer()
    {
        int x3 = -1;
        Vector3 z2;
        if (goldMiner.TileUpdateCheck == true)
        {


            goldMiner.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesGM.Add(goldMiner.Placement);


                foreach (var variable in tileUpdatertilesGM)
                {

                    x3++;
                    z2 = tileUpdatertilesGM[x3];
                    updateTileServer(z2, 9);


                }
            }
            x3 = -1;
        }


        if (GM == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesGM)
                {

                    x3++;
                    z2 = tileUpdatertilesGM[x3];

                    updateTile(z2, 9);

                }
            }
            x3 = -1;
        }
    }
    private void Diamondminer()
    {
        int x3 = -1;
        Vector3 z2;
        if (diamondMiner.TileUpdateCheck == true)
        {


           diamondMiner.TileUpdateCheck = false;
            if (isLocalPlayer)
            {
                tileUpdatertilesDM.Add(diamondMiner.Placement);


                foreach (var variable in tileUpdatertilesDM)
                {

                    x3++;
                    z2 = tileUpdatertilesDM[x3];
                    updateTileServer(z2, 10);


                }
            }
            x3 = -1;
        }


        if (DM == true)
        {
            if (isServer)
            {

                foreach (var variable in tileUpdatertilesDM)
                {

                    x3++;
                    z2 = tileUpdatertilesDM[x3];

                    updateTile(z2, 10);

                }
            }
            x3 = -1;
        }
    }
    public void deleteTilesButton()
    {
        
       
            foreach (var variable in tileUpdatertiles)
            {
                x2++;

                Vector3 z2 = tileUpdatertiles[x2];


                updateTileServer(z2, -1);
                map.SetTile(map.WorldToCell(z2), deleteTiles[0]);




            }

        foreach (var variable in tileUpdatertilesWG)
        {
            x4++;
             z3 = tileUpdatertilesWG[x4];
            updateTileServer(z3, -1);
        }

        foreach (var variable in tileUpdatertilesK)
        {

                T++;
                k = tileUpdatertilesK[T];
                updateTileServer(k, -1);
        }
        foreach (var variable in tileUpdatertilesS)
        {

            D++;
             d= tileUpdatertilesS[D];
            updateTileServer(d, -1);
        }
        foreach (var variable in tileUpdatertilesDS)
        {

            I++;
             i= tileUpdatertilesDS[I];
            updateTileServer(i, -1);
        }
        foreach (var variable in tileUpdatertilesSS)
        {

            U++;
            u = tileUpdatertilesSS[U];
            updateTileServer(u, -1);
        }
        foreach (var variable in tileUpdatertilesGS)
        {

            Y++;
            y = tileUpdatertilesGS[Y];
            updateTileServer(y, -1);
        }
        foreach (var variable in tileUpdatertilesM)
        {
            int Y = -1;
            Vector3 y;
            Y++;
            y = tileUpdatertilesM[Y];
            updateTileServer(y, -1);
        }
        foreach (var variable in tileUpdatertilesEM)
        {
            int Y = -1;
            Vector3 y;
            Y++;
            y = tileUpdatertilesEM[Y];
            updateTileServer(y, -1);
        }
        foreach (var variable in tileUpdatertilesGM)
        {
            int Y = -1;
            Vector3 y;
            Y++;
            y = tileUpdatertilesGM[Y];
            updateTileServer(y, -1);
        }
        foreach (var variable in tileUpdatertilesDM)
        {
            int Y = -1;
            Vector3 y;
            Y++;
            y = tileUpdatertilesDM[Y];
            updateTileServer(y, -1);
        }

        Alive = false;
        NetworkManager.singleton.StopClient();

    }

    [Command]
    private void deletePlayer()
    {
        Destroy(h);
        deletePlayerServer();
    }

    [ClientRpc]
    private void deletePlayerServer()
    {
        Destroy(h);

    }




}
