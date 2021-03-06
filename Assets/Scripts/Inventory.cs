using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Inventory : NetworkBehaviour
{
    public GameObject InventoryCheck;
    public GameObject GeneratorInventory;
    public GameObject MinenInventory;
    public GameObject SellerInventory;
    public GameObject PickageInventory;
    public GameObject canvas;
    public bool IsActive= false;
    
    void Update()
    {
        if (isLocalPlayer)
        {
            canvas.SetActive(true);
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
    }
    public void Generator()
    { 
        MinenInventory.SetActive(false);
        SellerInventory.SetActive(false);
        GeneratorInventory.SetActive(true);
        PickageInventory.SetActive(false);
    }
    public void Miner()
    {
        SellerInventory.SetActive(false);
        GeneratorInventory.SetActive(false);
        MinenInventory.SetActive(true);
        PickageInventory.SetActive(false);
    }
    public void Seller()
    {
        GeneratorInventory.SetActive(false);
        MinenInventory.SetActive(false);
        SellerInventory.SetActive(true);
        PickageInventory.SetActive(false);
    }
    public void Pickage()
    {
        GeneratorInventory.SetActive(false);
        MinenInventory.SetActive(false);
        SellerInventory.SetActive(false);
        PickageInventory.SetActive(true);
    }
}
