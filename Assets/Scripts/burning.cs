﻿using System.Collections;
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
    
    public Leben leben;

    void Awake()
    {
        
        mapManager = FindObjectOfType<MapManager>();
    }
    void Update()
    {
        if (mapManager.GetTileResistance(transform.position) == 30)
        {
            
            StartCoroutine(burnDamage());
            
            
        }
        if (mapManager.GetTileResistance(transform.position) == 31)
        {
            StopAllCoroutines();
        }


    }
     IEnumerator burnDamage()
    {
        
        yield return new WaitForSeconds(0.5F);
        
        leben.Life -= 20;
        leben.SchadensMult = 2;
        
        leben.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(burnDamage());

    }
}
