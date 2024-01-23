using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject _player;

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
        InventorySystem.Instance.DisabledInventory += OnDisabledInventory;
        InventorySystem.Instance.EnabledInventory += OnEnabledInventory;
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
        EnableMouseMovement();
        EnablePlayerMovement();
    }

    public void OnEnabledInventory()
    {
        DisableMouseMovement();
        DisablePlayerMovement();
    }

    private void OnDestroy()
    {
        InventorySystem.Instance.DisabledInventory -= OnDisabledInventory;
        InventorySystem.Instance.EnabledInventory -= OnEnabledInventory;
    }
}
