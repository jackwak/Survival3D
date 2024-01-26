using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public event Action EnabledInventory;
    public event Action DisabledInventory;

    [SerializeField]
    private GameObject _inventoryScreenUI;
    

    public List<GameObject> SlotList = new List<GameObject>();
    [SerializeField] 
    public List<string> ItemList = new List<string>();
    private GameObject _itemToAdd;
    private GameObject _whatSlotToEquip;
    public bool IsOpen = false;

    private string _prefabPath = "Assets/Prefabs/Inventory Item/";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PopulateSlotList();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !IsOpen)
        {
            EnabledInventory();
            
            _inventoryScreenUI.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            IsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && IsOpen)
        {
            DisabledInventory();

            _inventoryScreenUI.SetActive(false);

            if (!CraftingSystem.Instance.IsOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            IsOpen = false;
        }
    }

    private void PopulateSlotList()
    {
        Transform slot = _inventoryScreenUI.transform.GetChild(0);
        foreach (Transform child in slot)
        {
            SlotList.Add(child.gameObject);
        }
    }

    public void AddToInventory(ICollectable item)
    {
        string itemName = item.GetName;

        if (IsFull())
        {
            //_isFull = true;
        }
        else
        {
            _whatSlotToEquip = EmptySlot();

            GameObject prefabRef = (GameObject)AssetDatabase.LoadMainAssetAtPath(_prefabPath + itemName + ".prefab");
            _itemToAdd = Instantiate(prefabRef, _whatSlotToEquip.transform.position, _whatSlotToEquip.transform.rotation);
            _itemToAdd.transform.SetParent(_whatSlotToEquip.transform);
            ItemList.Add(itemName);

            item.TakeItem();
        }
    }

    

    public void AddToInventory(string itemName)
    {

        if (IsFull())
        {
            //_isFull = true;
        }
        else
        {
            _whatSlotToEquip = EmptySlot();

            GameObject prefabRef = (GameObject)AssetDatabase.LoadMainAssetAtPath(_prefabPath + itemName + ".prefab");
            _itemToAdd = Instantiate(prefabRef, _whatSlotToEquip.transform.position, _whatSlotToEquip.transform.rotation);
            _itemToAdd.transform.SetParent(_whatSlotToEquip.transform);
            ItemList.Add(itemName);
        }
    }

    public GameObject EmptySlot()
    {
        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return null;
    }

    private bool IsFull()
    {
        int counter = 0;

        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        if (counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (int i = SlotList.Count - 1; i >= 0; i--)
        {
            if (SlotList[i].transform.childCount > 0)
            {
                if (SlotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(SlotList[i].transform.GetChild(0).gameObject);

                    counter--;
                }
            }
        }
    }

    public void ReCalculateList()
    {
        ItemList.Clear();

        foreach (var slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name; //Stone(Clone)
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");

                ItemList.Add(result);
            }
        }
    }

    
}
