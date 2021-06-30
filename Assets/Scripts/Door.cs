using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Gamemanager attributes")]
    public GameObject _Gamemanager;
    [Header("Door Attributes")]
    public bool is_unlocked;
    [Space]
    public bool is_key_required;
    [Space]
    [SerializeField] bool has_key = false;
    [Space]
    bool is_available;
    Animator door_animation;
    [Header("Multikey and Colorkey doors")]
    public GameObject special_Door_gameobject;

    private void Start()
    {
        _Gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        door_animation = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        switch (is_key_required)
        {
            case true:
                switch (special_Door_gameobject == null)
                {
                    case true:
                        switch (has_key)
                        {
                            case true:
                                is_unlocked = true;
                                break;
                            case false:
                                if (Input.GetKeyDown(KeyCode.Space) && is_available)
                                {
                                    for (int i = 0; i < _Gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.Count; i++)
                                    {
                                        if (_Gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items[i].Contains("Masterkey"))
                                        {
                                            has_key = true;
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case false:
                        //Implement: A key required door system, which could open if the player has the correct colored key
                        //if (Input.GetKeyDown(KeyCode.Space) && is_available)
                        //{
                        //    for (int i = 0; i < _Gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.Count; i++)
                        //    {
                        //        if ()
                        //    }
                        //}
                        break;
                }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.Space) && is_available && !is_unlocked)
                {
                    is_unlocked = true;
                }
                break;
        }


        door_animation.SetBool("Door_State", is_unlocked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
            is_available = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
            is_available = false;
    }
}
