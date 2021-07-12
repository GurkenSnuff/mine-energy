using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Tilemaps;

public class TileUpdater : NetworkBehaviour
{
    private ClintConnects clintConnects;
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
    public List<TileBase> updateTiles = new List<TileBase>();
    bool t = false,s=false,K=false;
    private Tilemap map;
    public int deleteCount=-1,x = -1,x2 = -1,T= -1,j=0;
    private Vector3 z,k;
    private NetworkConnection networkConnection;
    private MapManager mapManager;
    public List<int> propertys = new List<int>();

    void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        clintConnects = FindObjectOfType<ClintConnects>();
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
    }
    void Update()
    {
        
        SolarZellen();
            KohleGenerator();
        
    }
    
        
        
    
    [ClientRpc]
    private void updateTile(Vector3 number,int tile)
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
                    tileUpdatertilesK.Add(number);
                    K = true;
                }
                if (tile == 0)
                {

                    tileUpdatertiles.Add(number);
                    t = true;
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
    public void deleteTilesButton()
    {
        if (isLocalPlayer)
        {
            foreach (var variable in tileUpdatertiles)
            {
                x2++;

                Vector3 z2 = tileUpdatertiles[x2];


                updateTileServer(z2, -1);
                map.SetTile(map.WorldToCell(z2), deleteTiles[0]);




            }
            foreach (var variable in propertys)
            {
                deleteTileList(deleteCount);
            }
            foreach (var variable in tileUpdatertilesK)
            {

                T++;
                k = tileUpdatertilesK[T];
                updateTileServer(k, -1);
                updateTile(k, -1);

            }
             Destroy(gameObject);
             deleteTileList(-1);
        }

    }
    [Command]
    private void deleteTileList(int ToDelete)
    {
        if (ToDelete != -1) tileUpdatertiles.RemoveAt(ToDelete);
        Destroy(gameObject);
        deletePlayer();
    }
    [ClientRpc]
    private void deletePlayer()
    {
        Destroy(gameObject);
    }
}
