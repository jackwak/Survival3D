using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject _player;
    [SerializeField] private SelectionManager _selectionManager;

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
        // subscribe events
        InventorySystem.Instance.DisabledInventory += OnDisabledInventory;
        InventorySystem.Instance.EnabledInventory += OnEnabledInventory;

        CraftingSystem.Instance.DisabledCraft += OnDisableCraft;
        CraftingSystem.Instance.EnabledCraft += OnEnabledCraft;
    }

    public void EnableMouseMovement()
    {
        _player.GetComponent<MouseMovement>().enabled = true;
    }

    public void DisableMouseMovement()
    {
        _player.GetComponent<MouseMovement>().enabled = false;
    }

    public void EnablePlayerMovement()
    {
        _player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void DisablePlayerMovement()
    {
        _player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void OnDisabledInventory()
    {
        if (!CraftingSystem.Instance.IsOpen)
        {
            EnableMouseMovement();
            EnablePlayerMovement();
        }

        _selectionManager.SetState = State.GAME;
    }

    public void OnEnabledInventory()
    {
        DisableMouseMovement();
        DisablePlayerMovement();
        _selectionManager.SetState = State.INVENTORY;
    }

    public void OnEnabledCraft()
    {
        DisableMouseMovement();
        DisablePlayerMovement();
        _selectionManager.SetState = State.INVENTORY;
    }

    public void OnDisableCraft()
    {
        if (!InventorySystem.Instance.IsOpen)
        {
            EnableMouseMovement();
            EnablePlayerMovement();
        }

        _selectionManager.SetState = State.GAME;
    }

    private void OnDestroy()
    {
        // unsubscribe events
        InventorySystem.Instance.DisabledInventory -= OnDisabledInventory;
        InventorySystem.Instance.EnabledInventory -= OnEnabledInventory;

        CraftingSystem.Instance.DisabledCraft -= OnDisableCraft;
        CraftingSystem.Instance.EnabledCraft -= OnEnabledCraft;
    }
}
