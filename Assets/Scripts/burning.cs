using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class burning : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private MapManager mapManager;
    
    public Life life;

    void Awake()
    {
        
        mapManager = FindObjectOfType<MapManager>();
    }
    void Update()
    {
        if (mapManager.GetTileResistance(transform.position) == 18)
        {
            
            StartCoroutine(burnDamage());
            
            
        }
        if (mapManager.GetTileResistance(transform.position) == 17)
        {
            StopAllCoroutines();
        }


    }
     IEnumerator burnDamage()
    {
        
        yield return new WaitForSeconds(0.5F);
        
        life.Leben -= 20;
        life.SchadensMult = 2;
        
        life.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(burnDamage());

    }
}
