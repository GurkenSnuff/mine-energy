using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class TestLevel1 : NetworkBehaviour
{
    public Tile[] tiles;
    public GameObject d;
    public SolarZellen solarZellen;
    [SyncVar(hook=nameof(spawn))]
    public int rand;
    [SerializeField]
    private Tilemap map;
    private MapManager mapManager;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    public GameObject Hitbox,LavaHitbox;
    private float resistanceCheck;
    private int UpdateTile;


    bool isPlaced=false;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        
            
        
        mapManager = FindObjectOfType<MapManager>();
        map = FindObjectOfType<Tilemap>();
    }
    public void Start()
    {
        if (isServer)
        {
            rand = Random.Range(0, tiles.Length);
        }
        map.SetTile(map.WorldToCell(d.transform.position), tiles[rand]);
        resistanceCheck = mapManager.GetTileResistance(transform.position);

        if (mapManager.GetTileResistance(transform.position)>=1&& mapManager.GetTileResistance(transform.position) <= 5)
        {
            GameObject a =Instantiate(Hitbox) as GameObject;
            a.transform.position = transform.position;
        }
        if (mapManager.GetTileResistance(transform.position) == 30)
        {
           GameObject b = Instantiate(LavaHitbox) as GameObject;
            b.transform.position = transform.position;
            
        }

    }
    
    [ClientRpc]
    void GetTile(int t)
    {
        rand = t;
    
    }
    void spawn(int old,int neww)
    {
        
        map.SetTile(map.WorldToCell(d.transform.position), tiles[neww]);
    }
        
        
        


}
