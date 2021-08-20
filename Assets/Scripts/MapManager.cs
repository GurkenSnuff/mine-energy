﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach(var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
        
    }
    
    
    public int GetTileResistance(Vector2 worldPosition)
    {

        Vector3Int gridposition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridposition);
        int Resistance = dataFromTiles[tile].Resistance;
        return Resistance;
    }
    
}

