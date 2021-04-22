using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressourcen : MonoBehaviour
{
    public int Money = 0;
    private SolarZellen solarZellen;
    private WindGenerator windGenerator;
    public int Energy;
    public Text Energie,money;
    private KohleGenerator kohleGenerator;
    private Seller seller;
    private int SellerAnzahl, SellerAnzahlDS,SellerAnzahlSS,SellerAnzahlGS,SZE,WGE,KGE;
    public int Geld;
    private DoubleSeller doubleSeller;
    private SteinSeller steinSeller;
    private Miner miner;
    private GoldSeller goldSeller;
    private GoldMiner goldMiner;

    void Awake()
    {
        goldMiner = FindObjectOfType <GoldMiner> ();
        goldSeller = FindObjectOfType<GoldSeller>();
        miner = FindObjectOfType<Miner>();
        steinSeller = FindObjectOfType<SteinSeller>();
        doubleSeller = FindObjectOfType<DoubleSeller>();
        seller = FindObjectOfType<Seller>();
        solarZellen = FindObjectOfType<SolarZellen>();
        windGenerator = FindObjectOfType<WindGenerator>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        StartCoroutine(EnergyCounting());
        StartCoroutine(MoneyCounter());
        
    }
    IEnumerator EnergyCounting()
    {
        yield return new WaitForSeconds(1);
        SZE = solarZellen.EnergyStand + solarZellen.EnergyCount;
        WGE = windGenerator.EnergyStand + windGenerator.EnergyCount;
        KGE = kohleGenerator.EnergyStand + kohleGenerator.EnergyCount;
        Energy = SZE + WGE + KGE;
        
       
        StartCoroutine(EnergyCounting());
    }
    IEnumerator MoneyCounter()
    {
        yield return new WaitForSeconds(1);
        SellerAnzahl = seller.HowManySeller;
        while (Energy >= 0 && SellerAnzahl >= 1)
        {
            solarZellen.EnergyStand--;
            windGenerator.EnergyStand--;
            kohleGenerator.EnergyStand--;
            Energy -= 8;
           Money += 1;
           SellerAnzahl--;
          
        }
        SellerAnzahlDS = doubleSeller.HowManySeller;
        while (Energy >= 8 && SellerAnzahlDS >= 1)
        {
            solarZellen.EnergyStand-=8;
            Energy -= 8;
            
            Money += 10;
            SellerAnzahlDS--;
            
        }
        SellerAnzahlSS = steinSeller.HowManySeller;
        while (Energy >= 8 && SellerAnzahlDS >= 1)
        {
            solarZellen.EnergyStand -= 8;
            Energy -= 8;
            miner.Stein -= 2;
            Money += 15;
            SellerAnzahlSS--;

        }
        SellerAnzahlGS = goldSeller.HowManySeller;
        while (Energy >= 15 && SellerAnzahlGS >= 1)
        {
            solarZellen.EnergyStand -= 8;
            Energy -= 15;
            goldMiner.Gold -= 3;
            Money += 30;
            SellerAnzahlGS--;
        }
        money.text = "Money: " + Money;
        Energie.text = "Energy: " + Energy;
        StartCoroutine(MoneyCounter());
    }
    
    
    
}
