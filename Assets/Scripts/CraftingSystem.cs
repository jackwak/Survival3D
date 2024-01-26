using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CraftingSystem : MonoBehaviour
{
    public event Action EnabledCraft;
    public event Action DisabledCraft;

    [SerializeField]
    private GameObject _craftingScreenUI;
    [SerializeField]
    private GameObject _toolsCategoryUI;
    [SerializeField]
    private List<string> _inventoryItemList = new List<string>();

    //Category Buttons
    private Button _toolsBTN;

    //Craft Buttons
    private Button _craftAxeBTN;

    //Requirement Text
    private TMP_Text AxeReq1, AxeReq2;
    public bool IsOpen = false;

    //All Blueprints
    public Blueprint AxeBlueprint = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);
    

    public static CraftingSystem Instance;

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
        _toolsBTN = _craftingScreenUI.transform.Find("Tools Button").GetComponent<Button>();
        _toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Axe

        AxeReq1 = _toolsCategoryUI.transform.Find("Axe").transform.Find("Requirement1").GetComponent<TMP_Text>();
        AxeReq2 = _toolsCategoryUI.transform.Find("Axe").transform.Find("Requirement2").GetComponent<TMP_Text>();

        _craftAxeBTN = _toolsCategoryUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        _craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBlueprint); });
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !IsOpen)
        {
            EnabledCraft();

            _craftingScreenUI.SetActive(true);
            RefreshNeededItem();
            Cursor.lockState = CursorLockMode.None;
            IsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && IsOpen)
        {
            DisabledCraft();

            _toolsCategoryUI.SetActive(false);
            _craftingScreenUI.SetActive(false);
            if (!InventorySystem.Instance.IsOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            IsOpen = false;
        }
    }

    void OpenToolsCategory()
    {
        _craftingScreenUI.SetActive(false);
        _toolsCategoryUI.SetActive(true);
    }

    private void CraftAnyItem(Blueprint blueprintToCraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.ItemName);

        if (blueprintToCraft.numberOfRequirement == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Requirement1, blueprintToCraft.Requirement1Amount);
        }
        else if (blueprintToCraft.numberOfRequirement == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Requirement1, blueprintToCraft.Requirement1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Requirement2, blueprintToCraft.Requirement2Amount);
        }

        StartCoroutine(Delay());
    }

    public void RefreshNeededItem()
    {
        int stoneCount = 0;
        int stickCount = 0;

        _inventoryItemList = InventorySystem.Instance.ItemList;

        foreach (var itemName in _inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stoneCount++;
                    break;
                case "Stick":
                    stickCount++;
                    break;
                default:
                    break;
            }
        }
        //------AXE-------

        AxeReq1.text = "3 Stone [" + stoneCount + "]";
        AxeReq2.text = "3 Stick[" + stickCount + "]";

        if (stoneCount >=3 && stickCount >= 3)
        {
            _craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            _craftAxeBTN.gameObject.SetActive(false);
        }
    }

    IEnumerator Delay()
    {
        yield return null;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItem();

         InventorySystem.Instance.EmptySlot();
    }
}










