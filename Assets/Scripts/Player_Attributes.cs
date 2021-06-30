﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Player_Attributes : MonoBehaviour
{
    //Dictionary for KeyCode (From Overflow, alien code)
    Dictionary<KeyCode, System.Action> keyCodeDic = new Dictionary<KeyCode, System.Action>();
    [Header("Access Gamemanager")]
    public GameObject gamemanager;
    [Header("Access Child and or Parent scripts attached to this GameObject")]
    public PlayerMovement _playermovement;
    [Header("Player Attributes")]
    public int health;
    [Header("Weapon attributes")]
    public GameObject current_Wielded_Weapon;
    [Space]
    public Image weapon_reference_position;
    [Space]
    public Vector3 weapon_reference_position_offset;
    [Space]
    public List<GameObject> weapon_holster;
    public int weapon_holster_index;
    [Space]
    [SerializeField] private GameObject weapon_Parent;
    [Header("View Bobbing")]
    [SerializeField] Camera _camera;
    [Space]
    public bool is_fullscreen;
    [Space]
    public float view_bobbing_amount;
    [Header("Inventory attributes")]
    public GameObject inventory;
    [Space]
    public List<string> inventory_Items;
    public RectTransform[] inventory_Spaces;
    [Space]
    public bool is_Inventory_Opened;

    private void Start()
    {
        weapon_holster_index = 0;
        if (weapon_holster.Count > 0)
        {
            foreach (var item in weapon_holster)
            {
                item.SetActive(false);
            }
            current_Wielded_Weapon = weapon_holster[0];
            current_Wielded_Weapon.SetActive(true);
            current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = new Vector3(weapon_reference_position.rectTransform.position.x + weapon_reference_position_offset.x, weapon_reference_position.rectTransform.position.y + weapon_reference_position_offset.y, 0f);
        }
    }

    private void Update()
    {
        if (!is_Inventory_Opened)
            Weapon_Fire();
        View_Bobbing(true);
    }

    private void LateUpdate()
    {
        if (!is_Inventory_Opened)
            OpenPauseMenu();
        Switch_Weapons();
        Open_Inventory();
    }

    void View_Bobbing(bool is_turned_on)
    {
        Vector3 rect; 
        is_fullscreen = Input.GetKey(KeyCode.Alpha0); //Debug tool
        if (weapon_holster.Count > 0)
        {
            weapon_reference_position_offset.y = current_Wielded_Weapon.GetComponent<Weapon>().y_offset;
            switch (is_turned_on) //Change position based on the fullscreen aspect: If fullscreen just simply *2 (Made with Questionmark operator)
            {
                case true:
                    rect = new Vector3((weapon_reference_position.rectTransform.position.x + weapon_reference_position_offset.x) + (Mathf.Sin(-_playermovement.GetComponent<PlayerMovement>().inputX) * view_bobbing_amount), (weapon_reference_position.rectTransform.position.y + (is_fullscreen ? weapon_reference_position_offset.y * 2 : weapon_reference_position_offset.y * 1)) + (Mathf.Sin(-_playermovement.GetComponent<PlayerMovement>().inputY) * view_bobbing_amount));
                    current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = rect;
                    break;
                case false:
                    rect = new Vector3(weapon_reference_position.rectTransform.position.x, weapon_reference_position.rectTransform.position.y + (is_fullscreen ? weapon_reference_position_offset.y * 2 : weapon_reference_position_offset.y * 1));
                    current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = rect;
                    break;
            }
        }
    }
    private void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Pause_Menu.activeInHierarchy)
        {
            gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Pause_Menu.SetActive(true);
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<_Camera_Script>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Pause_Menu.activeInHierarchy)
        {
            gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Pause_Menu.SetActive(false);
            GetComponent<PlayerMovement>().enabled = true;
            GetComponentInChildren<_Camera_Script>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Open_Inventory()
    {
        if (!is_Inventory_Opened && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<_Camera_Script>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            inventory.SetActive(true);
            is_Inventory_Opened = true;
            gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Inventory.GetComponent<GameManager>().about_window.SetActive(false);
            Display_Inventory();
        }
        else if (is_Inventory_Opened && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponentInChildren<_Camera_Script>().enabled = true;
            inventory.SetActive(false);
            is_Inventory_Opened = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Display_Inventory() //Refresh inventory display everytime it's openeds
    {
        string str;
        inventory.GetComponent<GameManager>().button_interaction_dropdown.gameObject.SetActive(false);
        for (int i = 0; i < inventory_Items.Count; i++)
        {
            str = inventory_Items[i];
            for (int n = 0; n < gamemanager.GetComponent<_GameManager>().item_list.Length; n++)
            {
                if (gamemanager.GetComponent<_GameManager>().item_list[n].name.Contains(str))
                {
                    inventory_Spaces[i].GetComponent<Image>().sprite = gamemanager.GetComponent<_GameManager>().item_Sprite_List[n];
                    inventory_Spaces[i].GetComponent<Image>().color = new Color(inventory_Spaces[i].GetComponent<Image>().color.r, inventory_Spaces[i].GetComponent<Image>().color.g, inventory_Spaces[i].GetComponent<Image>().color.b, 255);
                }
            }          
        }
    }
    public void Clean_Up_Inventory()
    {
        string str;
        if (weapon_holster_index > weapon_holster.Count) //Reset weapon holster index if not matching
            weapon_holster_index = weapon_holster.Count - 1;

        for (int i = 0; i < 6; i++)
        {
            if (i < inventory_Items.Count)
            {
                str = inventory_Items[i];
                for (int n = 0; n < inventory_Items.Count; n++)
                {
                    if (gamemanager.GetComponent<_GameManager>().item_list[n].name.Contains(str))
                    {
                        inventory_Spaces[i].GetComponent<Image>().sprite = gamemanager.GetComponent<_GameManager>().item_Sprite_List[n];
                        inventory_Spaces[i].GetComponent<Image>().color = new Color(inventory_Spaces[i].GetComponent<Image>().color.r, inventory_Spaces[i].GetComponent<Image>().color.g, inventory_Spaces[i].GetComponent<Image>().color.b, 255);
                    }
                }
            }
            else
            {
                inventory_Spaces[i].GetComponent<Image>().sprite = null;
                inventory_Spaces[i].GetComponent<Image>().color = new Color(inventory_Spaces[i].GetComponent<Image>().color.r, inventory_Spaces[i].GetComponent<Image>().color.g, inventory_Spaces[i].GetComponent<Image>().color.b, 0);
            }

            //str = inventory_Items[i];
            //for (int n = 0; n < gamemanager.GetComponent<_GameManager>().item_list.Length; n++)
            //{
            //    if (gamemanager.GetComponent<_GameManager>().item_list[n].name.Contains(str))
            //    {
            //        inventory_Spaces[i].GetComponent<Image>().sprite = gamemanager.GetComponent<_GameManager>().item_Sprite_List[n];
            //        inventory_Spaces[i].GetComponent<Image>().color = new Color(inventory_Spaces[i].GetComponent<Image>().color.r, inventory_Spaces[i].GetComponent<Image>().color.g, inventory_Spaces[i].GetComponent<Image>().color.b, 255);
            //    }
            //}          
        }
    }
    void Switch_Weapons()
    {
        int old_index = weapon_holster_index;
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapon_holster.Count > 0)
        {
            Misc_Weapon_Swap(old_index, 0); //Calling function everytime, so viewbobbing isn't buggy
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapon_holster.Count > 1)
        {
            Misc_Weapon_Swap(old_index, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weapon_holster.Count > 2)
        {
            Misc_Weapon_Swap(old_index, 2);
        }
    }
    public void Misc_Weapon_Swap(int old_value, int new_value)
    {
        weapon_holster[old_value].SetActive(false);
        weapon_holster[new_value].SetActive(true);
        weapon_holster_index = new_value;
        current_Wielded_Weapon = weapon_holster[new_value];
        weapon_reference_position_offset.y = weapon_holster[new_value].GetComponent<Weapon>().y_offset;
    }
    void Weapon_Pickup(Collider weapon) //Argument takes in the collider of the to-be-picked-up object, and adds it into a weapon accordingly
    {
        int _index = 0; //Identify weapon from gamemanager list
        bool contain = false; //Identify if the weapon exist in the holster (YES: picks up ammunition; NO: Adds into inventory)
        string[] splt = weapon.name.Split('_');
        if (weapon_holster.Count > 0)
        {
            for (int i = 0; i < weapon_holster.Count; i++)
            {
                if (weapon_holster[i].gameObject.name.Contains(splt[0]))
                {
                    contain = true;
                    _index = i;
                    break;
                }
            }
            if (contain)
            {
                weapon_holster[_index].GetComponent<Weapon>().ammunition++;
            }
            else
            {
                for (int i = 0; i < gamemanager.GetComponent<_GameManager>().weapon_Prefabs.Length; i++)
                {
                    if (gamemanager.GetComponent<_GameManager>().weapon_Prefabs[i].name.Contains(splt[0]))
                    {
                        _index = i;
                    }
                }
                GameObject w = Instantiate(gamemanager.GetComponent<_GameManager>().weapon_Prefabs[_index]);
                weapon_holster.Add(w);
                inventory_Items.Add(w.name.Replace("(Clone)", ""));
                Destroy(weapon.gameObject);
                w.SetActive(false);
                w.transform.SetParent(weapon_Parent.transform);
            }
        }
        else
        {
            for (int i = 0; i < gamemanager.GetComponent<_GameManager>().weapon_Prefabs.Length; i++)
            {
                if (gamemanager.GetComponent<_GameManager>().weapon_Prefabs[i].name.Contains(splt[0])) 
                {
                    _index = i;
                }
            }
            GameObject w = Instantiate(gamemanager.GetComponent<_GameManager>().weapon_Prefabs[_index]);
            weapon_holster.Add(w);
            inventory_Items.Add(w.name.Replace("(Clone)",""));
            Destroy(weapon.gameObject);
            current_Wielded_Weapon = weapon_holster[0];
            w.GetComponent<Weapon>().ammunition++;
            w.transform.SetParent(weapon_Parent.transform);
            weapon_reference_position_offset.y = w.GetComponent<Weapon>().y_offset;
            //w.GetComponent<RectTransform>().anchorp
            //w.GetComponent<RectTransform>().localScale = new Vector3(weapon_holster[_index].GetComponent<Weapon>().widht, weapon_holster[_index].GetComponent<Weapon>().height, weapon_holster[_index].GetComponent<Weapon>().height);
        }
    }
    void Item_Pickup(Collider item)
    {
        //int index = 0; //Necessary?
        bool contains = false;
        switch (inventory_Items.Count > 0)
        {
            case true:
                for (int i = 0; i < inventory_Items.Count; i++)
                {
                    if (inventory_Items[i].Contains(item.name[0].ToString()))
                    {
                        contains = true;
                    }
                }
                if (contains)
                {
                    Destroy(item.gameObject);
                    //Implement: item addition
                }
                else
                {
                    inventory_Items.Add(item.name);
                    Destroy(item.gameObject);
                }
                break;
            case false:
                inventory_Items.Add(item.name);
                Destroy(item.gameObject);
                break;
        }
    }
    private void Weapon_Fire()
    {
        if (weapon_holster.Count > 0)
        if (Input.GetMouseButtonDown(0) && weapon_holster[weapon_holster_index].GetComponent<Weapon>().is_hitscan) //Fire with Invisible Raycast if hitscan
        {
            RaycastHit raycast_hit_information;
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward,out raycast_hit_information, Mathf.Infinity))
            {
                Debug.Log(raycast_hit_information.collider.name);
            }
        }
        else //If not hitscan, then fire projectile
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Note to self: Refresh Item list, whenever adding new one
        /// </summary>
        switch (other.gameObject.tag)
        {
            case "Weapon_Pickup":
                //if (inventory_Items.Count < 6 && weapon_holster.Capacity < 4)
                    Weapon_Pickup(other);
                break;
            case "Key":
                //if (inventory_Items.Count < 6)
                    Item_Pickup(other);
                break;
            case "Dead_Drop":
                    Item_Pickup(other);
                break;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_camera.transform.position, _camera.transform.forward);
    }
}
