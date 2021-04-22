using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryCheck;
    public GameObject GeneratorInventory;
    public GameObject MinenInventory;
    public GameObject SellerInventory;
    private bool IsActive= false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsActive == true)
            {
                IsActive = false;
                InventoryCheck.SetActive(false);
            }
            else
            {
                IsActive = true;
                InventoryCheck.SetActive(true);
            }
            
        }
    }
    public void Generator()
    { 
        MinenInventory.SetActive(false);
        SellerInventory.SetActive(false);
        GeneratorInventory.SetActive(true);
    }
    public void Miner()
    {
        SellerInventory.SetActive(false);
        GeneratorInventory.SetActive(false);
        MinenInventory.SetActive(true);
    }
    public void Seller()
    {
        GeneratorInventory.SetActive(false);
        MinenInventory.SetActive(false);
        SellerInventory.SetActive(true);
    }
}
