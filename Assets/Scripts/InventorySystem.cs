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
    private List<string> _itemList = new List<string>();
    private GameObject _itemToAdd;
    private GameObject _whatSlotToEquip;
    private bool _isOpen = false;
    //private bool _isFull = false;

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
        if (Input.GetKeyDown(KeyCode.I) && !_isOpen)
        {
            EnabledInventory();
            
            _inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && _isOpen)
        {
            DisabledInventory();

            _inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            _isOpen = false;
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
            _itemList.Add(itemName);

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
            _itemList.Add(itemName);
        }
    }

    private GameObject EmptySlot()
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

}
