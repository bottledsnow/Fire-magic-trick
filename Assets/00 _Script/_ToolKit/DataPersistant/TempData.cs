using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempData : MonoBehaviour
{
    public Dictionary<string, bool> defeatedObjects;
    public Dictionary<string, bool> activatedMapObjects;

    public TempData()
    {
        defeatedObjects = new();
        activatedMapObjects = new();
    }
}
