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
    [Header("Access Canvas Storage")]
    public GameObject canvas_Storage;
    [Header("Prefabs and Gameobject storage")]
    public GameObject[] weapon_Prefabs;
    [Space]
    public GameObject player;
    [Header("Soundtracks and Sound effects")]
    public Music[] soundtrack;
    [Header("Inventory Item attributes")]
    public GameObject[] item_list;
    [Space]
    public string[] item_Name_List;
    [Space]
    public Sprite[] item_Sprite_List;
    [Space]
    public string[] item_Description_List;

    private void LateUpdate()
    {
        Debug_Commands();
    }

    private void Debug_Commands()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(0);
        }
    }
}
