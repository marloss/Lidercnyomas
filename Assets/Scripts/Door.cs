using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Attributes")]
    public bool is_unlocked;
    [Space]
    public bool is_key_required;
    [SerializeField] bool is_available;
    Animator door_animation;

    private void Start()
    {
        door_animation = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && is_available && !is_unlocked)
        {
            is_unlocked = true;
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
