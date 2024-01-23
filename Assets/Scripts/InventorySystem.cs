using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public event Action EnabledInventory;
    public event Action DisabledInventory;

    [SerializeField]
    private GameObject _inventoryScreenUI;
    private bool _isOpen;

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
        _isOpen = false;
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
}
