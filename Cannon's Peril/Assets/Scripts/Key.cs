using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }

}
