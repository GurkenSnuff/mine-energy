using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Tilemaps;

public class TileUpdater : NetworkBehaviour
{
    private SolarZellen solarZellen;
    public List<Vector3> tileUpdatertiles = new List<Vector3>();
    public List<TileBase> updateTiles = new List<TileBase>();
    bool t = false;
    private Tilemap map;
    public int x = -1;
    private Vector3 z;

    void Awake()
    {
        solarZellen = FindObjectOfType<SolarZellen>();
        map = FindObjectOfType<Tilemap>();
    }
    void Update()
    {
        if (solarZellen.TileUpdateCheck == true)
        {
            tileUpdatertiles.Add(solarZellen.Placement);
            
            solarZellen.TileUpdateCheck = false;
            t = true;
        }

        if (t == true)
        {
            foreach(var variable in tileUpdatertiles)
            {
                x++;
                z = tileUpdatertiles[x];
                updateTile(z);
                
            }
            x = -1;
        }
    }
    [ClientRpc]
    void updateTile(Vector3 number)
    {
        map.SetTile(map.WorldToCell(number), updateTiles[0]);
    }
    
}
