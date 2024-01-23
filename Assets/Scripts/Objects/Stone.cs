using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IInteractable, ICollectable
{
    [SerializeField] 
    private string _name;
    private bool isTook;

    public string GetName => _name;

    public bool TakeItem()
    {
        if (!isTook)
        {
            // take item
            gameObject.SetActive(false);
            isTook = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
