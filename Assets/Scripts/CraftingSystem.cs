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
    private Text AxeReq1, AxeReq2;
    private bool _isOpen = false;

    //All Blueprints
    

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

        AxeReq1 = _toolsCategoryUI.transform.Find("Axe").transform.Find("Requirement1").GetComponent<Text>();
        AxeReq2 = _toolsCategoryUI.transform.Find("Axe").transform.Find("Requirement2").GetComponent<Text>();

        _craftAxeBTN = _toolsCategoryUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        _craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_isOpen)
        {
            EnabledCraft();

            _craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && _isOpen)
        {
            DisabledCraft();
            _toolsCategoryUI.SetActive(false);
            _craftingScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            _isOpen = false;
        }
    }

    void OpenToolsCategory()
    {
        _craftingScreenUI.SetActive(false);
        _toolsCategoryUI.SetActive(true);
    }

    private void CraftAnyItem()
    {
        //Add item to inventory

        //Remove resources from inventory
    }
}










