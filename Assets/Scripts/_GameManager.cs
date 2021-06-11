using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _GameManager : MonoBehaviour
{
    [Header("Scene_Name")]
    public string current_Scene_Name;
    Scene current_Scene;
    [Header("Prefabs and Gameobject storage")]
    public GameObject[] weapon_Prefabs;
    [Space]
    public GameObject player;
    [Header("Soundtracks and Sound effects")]
    public Music[] soundtrack;
    [Header("Inventory Item attributes")]
    public GameObject[] item_list;
}
