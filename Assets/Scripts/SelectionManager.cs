using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _interactionInfoUI;
    private Camera _mainCamera;
    private TMP_Text _interactionText;

    private State _state = new State();
    public State SetState
    {
        set { _state = value; }
    }

    private void Start()
    {
        _interactionText = _interactionInfoUI.GetComponent<TMP_Text>();
        _mainCamera = Camera.main;

        _state = State.GAME;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.GAME:

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 4))
                {
                    Transform selectionTransform = hit.transform;

                    if (selectionTransform.TryGetComponent<IInteractable>(out var interactableItem))
                    {
                        SetItemNameToInteractText(interactableItem);

                        if (selectionTransform.TryGetComponent<ICollectable>(out var collectableItem))
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                InventorySystem.Instance.AddToInventory(collectableItem);
                            }
                        }
                    }
                    else
                    {
                        _interactionText.text = null;
                    }
                }
                else
                {
                    _interactionText.text = null;
                }

                break;
            case State.INVENTORY:



                break;
            default:
                break;
        }
        
    }

    private void SetItemNameToInteractText(IInteractable item)
    {
        _interactionText.text = item.GetName;
    }
}

public enum State
{
    GAME,
    INVENTORY
}
