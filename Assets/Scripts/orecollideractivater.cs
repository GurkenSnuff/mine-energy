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
    public TileUpdater tileUpdater;
    private List<TileUpdater> tileupdater = new List<TileUpdater>();
    public bool t=false;
    

    private void Awake()
    {
        
            dataFromTiles = new Dictionary<TileBase, TileData>();
            clintConnects = FindObjectOfType<Clintconnects>();
        
        mapGenerator = FindObjectOfType<mapGenerator>();
            mapManager = FindObjectOfType<MapManager>();
            map = FindObjectOfType<Tilemap>();
        
    }
    void Update()
    {
        int x = 0;
        if (t == true)
        {
            print(mapGenerator.Player[x].transform.position-gameObject.transform.position);
        }

        if (clintConnects.clintConnectCount > newClient) activater = true;
            newClient = clintConnects.clintConnectCount;
        

            if (mapManager.GetTileResistance(gameObject.transform.position) >= 2 && mapManager.GetTileResistance(gameObject.transform.position) <= 16)
            {
            h.enabled = true;
            if (activater == true)
                {
                    
                    StartCoroutine(WaitUntilPlayerSpawned());
                    activater = false;
                    
                }
            }
            if (mapManager.GetTileResistance(gameObject.transform.position) == 1)
            {
                
                    h.enabled = false;

                    //colliderDisabler();
                    
                

            }

        foreach (var variable in mapGenerator.Player)
        {
            
            int tile = mapManager.GetTileResistance(gameObject.transform.position) - 1;

            if (mapGenerator.Player[x] != null)
            {
                

                if (mapGenerator.Player.Count <= x)
                {
                    return;
                }
                Vector3 position = mapGenerator.Player[x].transform.position;
                Vector3 distance = position - gameObject.transform.position;

                if (distance.x < 7f&& distance.y < 7f)
                {

                    mapGenerator.tileupdater[x].terrainSpawnerStarter(mapGenerator.Clients[x], gameObject.transform.position, tile);
                }
                else
                {
                    if (distance.x < -7f && distance.y < 7f)
                    {

                        mapGenerator.tileupdater[x].terrainSpawnerStarter(mapGenerator.Clients[x], gameObject.transform.position, tile);
                    }

                    else
                    {
                        if (distance.y < -7f&&distance.x < -7f)
                        {

                            mapGenerator.tileupdater[x].terrainSpawnerStarter(mapGenerator.Clients[x], gameObject.transform.position, tile);
                        }

                        else
                        {
                            if (distance.y < 7f&& distance.x < 7f)
                            {

                                mapGenerator.tileupdater[x].terrainSpawnerStarter(mapGenerator.Clients[x], gameObject.transform.position, tile);
                            }
                        }
                    }
                }
            }
            x++;



            
        }
        
            
    }
    IEnumerator WaitUntilPlayerSpawned()
    {
        yield return new WaitForSeconds(0.1f);
        mapGenerator.colliderEnabler(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        
    }

    [ClientRpc]
    public void colliderDisabler()
    {
        h.enabled = false;
    }
    
    
}
