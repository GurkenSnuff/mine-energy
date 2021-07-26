using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class orecollideractivater : NetworkBehaviour
{
    private Clintconnects clintConnects;
    public BoxCollider2D h;
    
    private Tilemap map;
    private MapManager mapManager;
    private mapGenerator mapGenerator;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private bool activater=true, activater2 = false;
    private int newClient=0,lessClient=0;
    

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
        


            if (clintConnects.clintConnectCount > newClient) activater = true;
            newClient = clintConnects.clintConnectCount;


            if (mapManager.GetTileResistance(gameObject.transform.position) >= 1 && mapManager.GetTileResistance(gameObject.transform.position) <= 5)
            {
            h.enabled = true;
            if (activater == true)
                {
                    
                    StartCoroutine(WaitUntilPlayerSpawned());
                    activater = false;
                    
                }
            }
            if (mapManager.GetTileResistance(gameObject.transform.position) == 0)
            {
                
                    h.enabled = false;

                    //colliderDisabler();
                    
                

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
