using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class orecollideractivater : NetworkBehaviour
{
    private Clintconnects clintConnects;
    public BoxCollider2D h;
    private bool updateList = false;
    private Tilemap map;
    private MapManager mapManager;
    private mapGenerator mapGenerator;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private bool activater=true, activater2 = false;
    private int newClient=0,lessClient=0;
    private TileUpdater tileUpdater;
    private List<TileUpdater> tileupdater = new List<TileUpdater>();
    private bool spawned = false,stop=false;
    private List<bool> isSpawned = new List<bool>();
    private Vector3 distance, position;
    private int tile;
    private double DamageMult=1,LifeDamage;
    public double Leben = 1000;
    [SerializeField]
    private GameObject lebensbar;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private float SchadensMult = 1F, LifeBarDamage, LifeVergleich = 999;
    [SerializeField]
    private  Tile[] tiles;
    public int positionForLife=-2;
    

    void Awake()
     {
        
            dataFromTiles = new Dictionary<TileBase, TileData>();
            clintConnects = FindObjectOfType<Clintconnects>();
        tileUpdater = FindObjectOfType<TileUpdater>();
        mapGenerator = FindObjectOfType<mapGenerator>();
            mapManager = FindObjectOfType<MapManager>();
            map = FindObjectOfType<Tilemap>();
        
    }
    
    void Start()
    {
        int x = 0;
        if (gameObject.tag != "Server")
        {
            foreach(var variable in mapGenerator.TilePosition)
            {
                if (mapGenerator.TilePosition[x] == gameObject.transform.position)
                {
                    Leben = mapGenerator.TileLife[x];
                    return;
                }
                x++;
            }
            
        }
    }


    void Update()
    {
        int x = 0;

        if (gameObject.tag != "Server")
        {  
                StartCoroutine(Wait()); 
        }
        
        if (Leben <= LifeVergleich)
        {
            if (gameObject.tag == "Server")
            {
                if (positionForLife >=-1)
                {
                    mapGenerator.AddLife(gameObject.transform.position, Leben, positionForLife);
                }
                else
                {
                    positionForLife = mapGenerator.number;
                    mapGenerator.AddLife(gameObject.transform.position, Leben, positionForLife);
                    mapGenerator.number++;
                    print("Hi");
                }
                
            }
            LifeBarDamage = 0.06F * SchadensMult;
            LifeVergleich -= 10F * SchadensMult;
            lebensbar.transform.localScale = new Vector3(lebensbar.transform.localScale.x - LifeBarDamage, 0.6000006F, 1);
            spriteRenderer.enabled = true;
        }
        if (Leben <= 1|| mapManager.GetTileResistance(gameObject.transform.position) == 1)
        {
            map.SetTile(map.WorldToCell(gameObject.transform.position), tiles[0]);
            spriteRenderer.enabled = false;
            lebensbar.transform.localScale = new Vector3(6,gameObject.transform.position.y, gameObject.transform.position.z);
            Leben = 1000;
            LifeVergleich = 999;
            
        }
        

        if (mapManager.GetTileResistance(gameObject.transform.position) >= 2 && mapManager.GetTileResistance(gameObject.transform.position) <= 16)
            {
                h.enabled = true;
            }
            if (mapManager.GetTileResistance(gameObject.transform.position) == 1)
            {
                h.enabled = false;
            }

            foreach (var variable in mapGenerator.Player)
            {

            if (mapGenerator.Player[x] != null)
            {
                
                if (isSpawned.Count != 0&&gameObject.tag=="Server")
                {
                    if (tile != mapManager.GetTileResistance(gameObject.transform.position) - 1) isSpawned[x] = false;
                }
                if (isSpawned.Count < mapGenerator.Player.Count) isSpawned.Add(spawned);

                if (mapGenerator.Player.Count <= x)
                {
                    return;
                }
                 position = mapGenerator.Player[x].transform.position;
                distance = position - gameObject.transform.position;
                
                    if (distance.y > -11f && distance.x > -11f && distance.x < 11f && distance.y < 11f)
                    {
                    
                    if (mapManager.GetTileResistance(gameObject.transform.position) >= 2 && mapManager.GetTileResistance(gameObject.transform.position) <= 16)
                    {

                        if (isSpawned[x] == false)
                        {
                            mapGenerator.tileupdater[x].terrainSpawnerAfterDespawnedStart(mapGenerator.Clients[x], gameObject.transform.position, tile,Leben);
                            //mapGenerator.CommitLife(gameObject.transform.position, Leben, positionForLife);
                            isSpawned[x] = true;
                        }
                    }
                    
                    mapGenerator.tileupdater[x].terrainSpawner(mapGenerator.Clients[x], gameObject.transform.position, tile);
                    
                    }
                    else
                    {
                        if (isSpawned[x] == true)
                        {
                            isSpawned[x] = false;
                            mapGenerator.tileupdater[x].terrainDespawnerStarter(mapGenerator.Clients[x], gameObject.transform.position);

                        }
                    }
                
            }
                x++;

            }
        tile = mapManager.GetTileResistance(gameObject.transform.position) - 1;
        
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (mapManager.GetTileResistance(gameObject.transform.position) >= 6 && mapManager.GetTileResistance(gameObject.transform.position) <= 16)
        {
            
            if (collision.CompareTag("Pickage"))
            {
               
                if (collision.name == "PickageHitboxIron")
                {
                    DamageMult = 1.5;
                }
                if (collision.name == "PickageHitboxGold")
                {
                    DamageMult = 2;
                }
                if (collision.name == "PickageHitboxDia")
                {
                    DamageMult = 2.5;
                }
                LifeDamage = 10 * DamageMult;
                Leben -= LifeDamage;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        if (mapManager.GetTileResistance(gameObject.transform.position) == 19|| mapManager.GetTileResistance(gameObject.transform.position)==1)
        {
            Destroy(gameObject);
        }
        
    }

    

    [ClientRpc]
    public void colliderDisabler()
    {
        h.enabled = false;
    }
    
    
}
