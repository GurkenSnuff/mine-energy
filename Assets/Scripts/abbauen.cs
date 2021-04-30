using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class abbauen : MonoBehaviour
{
    private MapManager mapManager;

    

    public Animator animator,eisenAnimator,goldAnimator,diaAnimator;
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Miner miner;
    
    public bool Wait = true, eisen = false, gold = false, dia = false,diaprevent=false;
    float Distance_;
    public Text StoneCount, EisenCount,GoldCount,DiaCount;
    private GoldMiner goldMiner;
    private EisenMiner eisenMiner;
    private DiamondMiner diamondMiner;
    public GameObject skin1,skin2,skin3,skin4;
    
    
    
    private void Awake()
    {
        eisenMiner = FindObjectOfType<EisenMiner>();
        diamondMiner = FindObjectOfType<DiamondMiner>();
        goldMiner = FindObjectOfType<GoldMiner>();
        miner = FindObjectOfType<Miner>();
        mapManager = FindObjectOfType<MapManager>();
        
    }

    void Update()
    {
        Hit();
        SkinUpdate();
    }

    private void Hit()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 minerÜber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float t = mapManager.GetTileResistance(minerÜber);
            Distance_ = Vector3.Distance(transform.position, minerÜber);
            
            if (eisen == false)
            {
                animator.SetBool("Schlag", true);
            }
            if (eisen == true&&gold==false)
            {
                eisenAnimator.SetBool("EisenSchlag", true);
            }
            if (gold == true && dia == false)
            {
                goldAnimator.SetBool("GoldSchlag", true);
            }
            if (dia == true)
            {
                diaAnimator.SetBool("DiaSchlag", true);
            }
            if (Distance_ <= 17.2)
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
                    if (eisen == true&&t==4)
                    {
                        Wait = false;
                        StartCoroutine(GoldMiner());
                    }
                    if (gold == true && t == 5)
                    {
                        StartCoroutine(DiamondMining());
                        Wait = false;
                    }
                }
            }

        }
        else
        {
            if (eisen == false)
            {
                animator.SetBool("Schlag", false);
            }
            if (gold == false)
            {
                eisenAnimator.SetBool("EisenSchlag", false);
            }
            if (dia == false)
            {
                goldAnimator.SetBool("GoldSchlag", false);
            }
            if (dia == true)
            {
                diaAnimator.SetBool("DiaSchlag", false);
            }
        }

        
    }
    void SkinUpdate()
    {
        if (eisen ==true&&gold==false )
        {
            
            Destroy(skin1.gameObject);
            skin2.SetActive(true);
        }
        if (gold == true&&dia==false)
        {
            Destroy(skin2.gameObject);
            skin3.SetActive(true);
        }
        if (dia == true)
        {
            Destroy(skin3.gameObject);
            skin4.SetActive(true);
        }
    }
    IEnumerator steinMining()
    {
        yield return new WaitForSeconds(1);
        if (eisen == true)
        {
            miner.Stein += 1;
        }
        if (gold == true)
        {
            miner.Stein += 1;
        }
        if (dia == true)
        {
            miner.Stein += 1;
        }
        miner.Stein += 1;
        Wait = true;
        StoneCount.text = "Stein: " + miner.Stein;
    }
    IEnumerator EisenMining()
    {
        yield return new WaitForSeconds(1);
        if (eisen == true)
        {
            eisenMiner.Eisen += 1;
        }
        if (gold == true)
        {
            eisenMiner.Eisen += 1;
        }
        
        eisenMiner.Eisen += 1;
        Wait = true;
        EisenCount.text = "Eisen: " + eisenMiner.Eisen;
    }
    IEnumerator GoldMiner()
    {
        yield return new WaitForSeconds(1);
        if (gold == true)
        {
            goldMiner.Gold += 1;
        }
        if (dia == true)
        {
            goldMiner.Gold += 1;
        }
        goldMiner.Gold += 1;
        Wait = true;
        GoldCount.text = "Gold: " + goldMiner.Gold;
    }
    IEnumerator DiamondMining()
    {
        yield return new WaitForSeconds(1);
        diamondMiner.Diamond += 1;
        Wait = true;
        DiaCount.text = "Diamond: " + diamondMiner.Diamond;
    }
    public void EisenPickage()
    {
        eisen = true;
    }
    public void GoldPickage()
    {
        if (eisen == true)
        {
            gold = true;
        }
    }
    public void DiaPickage()
    {
        if (gold == true)
        {
            dia = true;
        }
    }
}
