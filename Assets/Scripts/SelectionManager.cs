using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject _interactionInfoUI;

    private TMP_Text _interactionText;

    private void Start()
    {
        _interactionText = _interactionInfoUI.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4))
        {
            Transform selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>())
            {
                _interactionText.text = selectionTransform.GetComponent<InteractableObject>().GetName;
                _interactionInfoUI.SetActive(true);
            }
            else
            {
                _interactionInfoUI.SetActive(false);
            }
        }
        else
        {
            _interactionInfoUI.SetActive(false);
        }
    }
}
