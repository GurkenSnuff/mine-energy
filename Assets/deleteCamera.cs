using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteCamera : MonoBehaviour
{
    private TileUpdater player;
    
    void Start()
    {
        player = FindObjectOfType<TileUpdater>();
    }
    void Update()
    {
        if (player.Alive == false) Destroy(gameObject);
    }
}
