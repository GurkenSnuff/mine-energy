using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestLevel1 : MonoBehaviour
{
    public Tile[] tiles;
    public GameObject d;
    int rand;
    [SerializeField]
    private Tilemap map;
    private MapManager mapManager;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    public GameObject Hitbox,LavaHitbox;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        mapManager = FindObjectOfType<MapManager>();
        map = FindObjectOfType<Tilemap>();
    }
    private void Start()
    {
        rand = Random.Range(0, tiles.Length);
        map.SetTile(map.WorldToCell(d.transform.position), tiles[rand]);
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

}
