using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Access Gamemanager & Access Canvas storage")]
    public GameObject gamemanager;
    [Space]
    public GameObject canvas_storage;
    [Header("Inventory button attributes")]
    public TMPro.TMP_Dropdown button_interaction_dropdown; //Thank you Stack Overflow!
    [Space]
    public Vector3 button_interaction_dropdown_position_offset;
    [Space]
    public GameObject selected_item;
    //[Header("Pause menu attributes")]
    

    private void Start()
    {
        //////////////////////////////////
        ///Inventory Item interaction:
        /////////////////////////////////
        
        if (button_interaction_dropdown != null)
        {
            button_interaction_dropdown.gameObject.SetActive(false);
        }
    }

    #region Inventory Item Interaction
    //////////////////////////////////
    ///Inventory Item interaction:
    /////////////////////////////////
    public void OpenInventoryInteractionDropdown(Button _button)
    {
        if (_button.GetComponent<Image>().sprite != null) //If the clicked cell has an item stored
        {
            selected_item = _button.gameObject;
            IdentifyInventoryItemType(_button.GetComponent<Image>().sprite,button_interaction_dropdown);
            button_interaction_dropdown.transform.position = _button.transform.position + button_interaction_dropdown_position_offset;
            button_interaction_dropdown.gameObject.SetActive(true);
            button_interaction_dropdown.Show();
            button_interaction_dropdown.captionText.text = _button.GetComponent<Image>().sprite.name;
        }
    }
    private void IdentifyInventoryItemType(Sprite _item, TMPro.TMP_Dropdown _dropdown)
    {
        List<string> item_attributes;
        _dropdown.ClearOptions();
        switch (_item.name[_item.name.Length - 3])
        {
            case 'w': //Item type: Weapon
                item_attributes = new List<string>() { "Equip Item", " Deplete ammunition","Move Item", "Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'k': //Item type: Key item
                item_attributes = new List<string>() { "About", "Move Item" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'c': //Item type: Consumables
                item_attributes = new List<string>() { "Use item" , "Move Item", "Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'd': //Item type: Dead Drop
                item_attributes = new List<string>() { "Open Dead drop", "Move Item", "Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
        }
    }
    public void Dropdown_interaction()
    {
        //Debug.Log(button_interaction_dropdown.itemText.text);
        Debug.Log(button_interaction_dropdown.options[button_interaction_dropdown.value].text);
        Debug.Log(selected_item.GetComponent<Image>().sprite.name);
        switch (button_interaction_dropdown.itemText.text)
        {
            case "Use item":

                break;
            case "About":

                break;
            case "Move Item":

                break;
            case "Destroy":
                gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.Remove(selected_item.GetComponent<Image>().sprite.name);
                gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().Display_Inventory();
                break;
        }
        //Debug.Log(button_interaction_dropdown.itemText.text);
    }
    #endregion
    #region Pause Menu and Options Menu
    //OpenPauseMenu() is in "Player_Attributes.cs"

    public void ClosePauseMenu()
    {
        canvas_storage.GetComponent<Canvas_Storage>()._Pause_Menu.SetActive(false);
        gamemanager.GetComponent<_GameManager>().player.GetComponent<PlayerMovement>().enabled = true;
        gamemanager.GetComponent<_GameManager>().player.GetComponentInChildren<_Camera_Script>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenOptionsMenu()
    {
        canvas_storage.GetComponent<Canvas_Storage>()._Pause_Menu.SetActive(false);
        canvas_storage.GetComponent<Canvas_Storage>()._Option_Menu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        canvas_storage.GetComponent<Canvas_Storage>()._Pause_Menu.SetActive(true);
        canvas_storage.GetComponent<Canvas_Storage>()._Option_Menu.SetActive(false);
    }

    #endregion
}
