using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, ICollectable
{
    [SerializeField] private string _name;

    public string GetName => _name;

    public void TakeItem()
    {
        // take item
        Destroy(gameObject);
    }
}
