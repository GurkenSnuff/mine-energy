using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class abbauen : MonoBehaviour
{
    private MapManager mapManager;

    

    public Animator animator;
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Miner miner;
    
    bool Wait = true, eisen = false, gold = false, dia = false;
    float Distance_;
    public Text StoneCount, EisenCount,GoldCount;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    
    
    private void Awake()
    {
        eisenMiner = FindObjectOfType<EisenMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        miner = FindObjectOfType<Miner>();
        mapManager = FindObjectOfType<MapManager>();
        
    }

    void Update()
    {
        Hit();
    }

    private void Hit()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float t = mapManager.GetTileResistance(minerÜber);
            Distance_ = Vector3.Distance(transform.position, minerÜber);
            

            animator.SetBool("Schlag", true);
            if (Distance_ <= 10)
            {
                if (Wait == true)
                {
                    if (t == 2)
                    {

                        Wait = false;
                        StartCoroutine(steinMining());


                    }
                    if (t == 3)
                    {
                        Wait = false;
                        StartCoroutine(EisenMining());
                    }
                    if (gold == true&&t==4)
                    {
                        Wait = false;
                        StartCoroutine(GoldMiner());
                    }

                }
            }

        }
        else
        {
            animator.SetBool("Schlag", false);
        }

        
    }
    IEnumerator steinMining()
    {
        yield return new WaitForSeconds(1);
        miner.Stein += 1;
        Wait = true;
        StoneCount.text = "Stein: " + miner.Stein;
    }
    IEnumerator EisenMining()
    {
        yield return new WaitForSeconds(1);

        eisenMiner.Eisen += 1;
        Wait = true;
        EisenCount.text = "Eisen: " + eisenMiner.Eisen;
    }
    IEnumerator GoldMiner()
    {
        yield return new WaitForSeconds(1);

        goldMiner.Gold += 1;
        Wait = true;
        GoldCount.text = "Gold: " + goldMiner.Gold;
    }
    public void EisenPickage()
    {
        gold = true;
    }
}
