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
    private bool spawned = false;
    private List<bool> isSpawned = new List<bool>();
    private Vector3 distance, position;
    private int tile;


    private void Awake()
    {
        
            dataFromTiles = new Dictionary<TileBase, TileData>();
            clintConnects = FindObjectOfType<Clintconnects>();
       // tileUpdater = FindObjectOfType<TileUpdater>();
        mapGenerator = FindObjectOfType<mapGenerator>();
            mapManager = FindObjectOfType<MapManager>();
            map = FindObjectOfType<Tilemap>();
        
    }
    void Update()
    {
        int x = 0;

        if (gameObject.tag != "Server")
        {  
                StartCoroutine(Wait()); 
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
                tile = mapManager.GetTileResistance(gameObject.transform.position) - 1;
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
                        if (isSpawned[x] == false) mapGenerator.tileupdater[x].terrainSpawnerAfterDespawnedStart(mapGenerator.Clients[x], gameObject.transform.position, tile);
                    }
                    isSpawned[x] = true;
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
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        if (mapManager.GetTileResistance(gameObject.transform.position) == 19)
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
