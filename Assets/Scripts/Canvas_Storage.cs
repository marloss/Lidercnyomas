using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Storage : MonoBehaviour
{
    /// <summary>
    /// This script is used for storing canvas gameobjects, like Pause menu etc.
    /// </summary>

    [Header("Stored GUI and UI gameobject")]
    public GameObject _GUI;
    [Space]
    public GameObject _Inventory;
    [Space]
    public GameObject _Pause_Menu;
    [Space]
    public GameObject _Option_Menu;
}
