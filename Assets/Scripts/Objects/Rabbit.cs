using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;

    public string GetName => _name;
}
