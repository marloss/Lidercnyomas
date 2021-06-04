using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float view_bobbing_amount;

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
        Weapon_Fire();
        View_Bobbing(true);
        //current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = new Vector3(50, 50);
    }

    private void LateUpdate()
    {
        Switch_Weapons();
    }

    void View_Bobbing(bool is_turned_on)
    {
        Debug.Log(Screen.fullScreen);
        Vector3 rect;
        //Think of a way to compensate for difference between weapon position in fullscreen and not-fullscreen
        //if (Screen.fullScreen)
        //{
        //    weapon_reference_position_offset.y = weapon_reference_position_offset.y * 2;
        //}
        //else
        //{
        //    weapon_reference_position_offset.y = weapon_reference_position_offset.y * 2;
        //}
        if (weapon_holster.Count > 0)
        switch (is_turned_on)
        {
            case true:
                rect = new Vector3((weapon_reference_position.rectTransform.position.x + weapon_reference_position_offset.x) + (Mathf.Sin(-_playermovement.GetComponent<PlayerMovement>().inputX) * view_bobbing_amount), (weapon_reference_position.rectTransform.position.y + weapon_reference_position_offset.y) + (Mathf.Sin(-_playermovement.GetComponent<PlayerMovement>().inputY) * view_bobbing_amount));
                current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = rect;
                break;
            case false:
                rect = new Vector3(weapon_reference_position.rectTransform.position.x, weapon_reference_position.rectTransform.position.y);
                current_Wielded_Weapon.GetComponent<Image>().rectTransform.position = rect;
                break;
        }
    }

    void Switch_Weapons()
    {
        int old_index = weapon_holster_index;
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapon_holster.Count > 0)
        {
            weapon_holster_index = 0;
            Misc_Weapon_Swap(old_index, weapon_holster_index); //Calling function everytime, so viewbobbing isn't buggy
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapon_holster.Count > 1)
        {
            weapon_holster_index = 1;
            Misc_Weapon_Swap(old_index, weapon_holster_index);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weapon_holster.Count > 2)
        {
            weapon_holster_index = 2;
            Misc_Weapon_Swap(old_index, weapon_holster_index);
        }
    }

    private void Misc_Weapon_Swap(int old_value, int new_value)
    {
        weapon_holster[old_value].SetActive(false);
        weapon_holster[new_value].SetActive(true);
        current_Wielded_Weapon = weapon_holster[new_value];
        weapon_reference_position_offset.y = weapon_holster[new_value].GetComponent<Weapon>().y_offset;
    }

    void Weapon_Pickup(Collider weapon)
    {
        int _index = 0;
        bool contain = false;
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
                GameObject w = Instantiate(gamemanager.GetComponent<_GameManager>().weapon_Prefabs[_index]);
                w.transform.parent = weapon_Parent.transform;
                w.GetComponent<RectTransform>().localScale = new Vector3(weapon_holster[_index].GetComponent<Weapon>().widht, weapon_holster[_index].GetComponent<Weapon>().height, weapon_holster[_index].GetComponent<Weapon>().height);
                weapon_holster.Add(w);
                w.SetActive(false);
                //weapon.gameObject.name = weapon.name.Remove((weapon.name.Length - 7), weapon.name.Length);
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
            current_Wielded_Weapon = weapon_holster[0];
            w.GetComponent<Weapon>().ammunition++;
            w.transform.SetParent(weapon_Parent.transform);
            //w.GetComponent<RectTransform>().anchorp
            w.GetComponent<RectTransform>().localScale = new Vector3(weapon_holster[_index].GetComponent<Weapon>().widht, weapon_holster[_index].GetComponent<Weapon>().height, weapon_holster[_index].GetComponent<Weapon>().height);
            weapon_reference_position_offset.y = w.GetComponent<Weapon>().y_offset;
        }
        Destroy(weapon.gameObject);
    }

    private void Weapon_Fire()
    {
        if (weapon_holster.Count > 0)
        if (Input.GetMouseButtonDown(0) && weapon_holster[weapon_holster_index].GetComponent<Weapon>().is_hitscan)
        {
            RaycastHit raycast_hit_information;
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward,out raycast_hit_information, Mathf.Infinity))
            {
                Debug.Log(raycast_hit_information.collider.name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon_Pickup")
        {
            Weapon_Pickup(other);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_camera.transform.position, _camera.transform.forward);
    }
}
