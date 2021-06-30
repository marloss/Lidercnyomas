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
    [Tooltip("The offset position to display dropdown (see: Mouse click position + offset position)")]
    public Vector3 button_interaction_dropdown_position_offset;
    [Space]
    [Tooltip("The currently selected cell")]
    public GameObject selected_item;
    [Header("Inventory Description attributes")]
    public GameObject about_window;
    [Space]
    public TMPro.TextMeshProUGUI description_Title;
    [Space]
    public TMPro.TextMeshProUGUI description_body_Text;
    //[Header("Pause menu attributes")]
    

    private void Start()
    {
        //////////////////////////////////
        ///Inventory Item interaction:
        /////////////////////////////////
        
        if (about_window != null)
        {
            about_window.SetActive(false);
        }

        if (button_interaction_dropdown != null) //This is a check, so if the dropdown gameobject exist set false
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
        if (_button.GetComponent<Image>().sprite != null) //If the clicked cell has is an item cell (Has sprite?)
        {
            selected_item = _button.gameObject; 
            IdentifyInventoryItemType(_button.GetComponent<Image>().sprite,button_interaction_dropdown);
            button_interaction_dropdown.gameObject.SetActive(true);
            button_interaction_dropdown.gameObject.SetActive(true);
            button_interaction_dropdown.Show();
            button_interaction_dropdown.Show();
            button_interaction_dropdown.transform.position = _button.transform.position + button_interaction_dropdown_position_offset;
            for (int i = 0; i < gamemanager.GetComponent<_GameManager>().item_Sprite_List.Length; i++)
            {
                if (_button.GetComponent<Image>().sprite == gamemanager.GetComponent<_GameManager>().item_Sprite_List[i])
                {
                    button_interaction_dropdown.captionText.text = gamemanager.GetComponent<_GameManager>().item_Name_List[i].ToString();
                    break;
                }
            }
        }
    }
    private void IdentifyInventoryItemType(Sprite _item, TMPro.TMP_Dropdown _dropdown)
    {
        List<string> item_attributes; //This will display the options to interact with the inventory item
        _dropdown.ClearOptions(); //Clear history item options
        switch (_item.name[_item.name.Length - 3])
        {
            case 'w': //Item type: Weapon
                item_attributes = new List<string>() {" ", "Equip Item" ,"About" ,"Move Item" ,"Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'k': //Item type: Key item
                item_attributes = new List<string>() { " ", "About", "Move Item" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'c': //Item type: Consumables
                item_attributes = new List<string>() { " ", "Use Item", "About", "Move Item" ,"Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
            case 'd': //Item type: Dead Drop
                item_attributes = new List<string>() { " ", "Use Item", "About", "Move Item", "Destroy" };
                foreach (var text_option in item_attributes)
                {
                    _dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = text_option });
                }
                break;
        }
    }
    public void Dropdown_interaction()
    {
        switch (button_interaction_dropdown.options[button_interaction_dropdown.value].text)
        {
            case "Equip Item":
                int temp = 0;
                string[] spl = gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Inventory.GetComponent<GameManager>().selected_item.GetComponent<Image>().sprite.name.Split('_');
                for (int i = 0; i < gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster.Count; i++)
                {
                    if (gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster[i].GetComponent<Image>().sprite.name.Contains(spl[0]))
                    {
                        temp = i;
                        gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().Misc_Weapon_Swap(gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster_index, temp);
                        button_interaction_dropdown.gameObject.SetActive(false);
                        break;
                    }
                }
                break;
            case "Use item":
                //Consumable Or Dead Drop => (Turn to Item, or affect player vitality)
                break;
            case "About":
                about_window.SetActive(true);
                for (int i = 0; i < gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.Count; i++)
                {
                    if (gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Inventory.GetComponent<GameManager>().selected_item.GetComponent<Image>().sprite == gamemanager.GetComponent<_GameManager>().item_Sprite_List[i])
                    {
                        description_Title.text = gamemanager.GetComponent<_GameManager>().item_Name_List[i].ToString();
                        description_body_Text.text = gamemanager.GetComponent<_GameManager>().item_Description_List[i].ToString();
                        button_interaction_dropdown.gameObject.SetActive(false);
                        break;
                    }
                }
                //description_Title = gamemanager.GetComponent<_GameManager>().
                break;
            case "Move Item":
                //Move item to another space (Not affecting weapon holster sequence)
                break;
            case "Destroy":
                for (int i = 0; i < gamemanager.GetComponent<_GameManager>().item_Sprite_List.Length; i++)
                {
                    if (gamemanager.GetComponent<_GameManager>().canvas_Storage.GetComponent<Canvas_Storage>()._Inventory.GetComponent<GameManager>().selected_item.GetComponent<Image>().sprite == gamemanager.GetComponent<_GameManager>().item_Sprite_List[i])
                    {
                        string item_name = gamemanager.GetComponent<_GameManager>().item_list[i].name.ToString();
                        //int _i = gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.FindIndex(a => a.Contains(item_name));
                        //Destroy(gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items[_i])
                        Destroy(gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster[gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster_index], 0.1f);
                        gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster.RemoveAt(gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster_index);
                        gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().inventory_Items.Remove(item_name);
                        gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().Clean_Up_Inventory();
                        gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().Display_Inventory();
                        if (gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster.Count > 0)
                            gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().current_Wielded_Weapon = gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster[gamemanager.GetComponent<_GameManager>().player.GetComponent<Player_Attributes>().weapon_holster_index];
                        button_interaction_dropdown.gameObject.SetActive(false);
                        break;
                    }
                }
                break;
        }
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
