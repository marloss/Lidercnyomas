using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attributes : MonoBehaviour
{
    [Header("Player Attributes")]
    public int health;
    [Space]
    public int weapon_holster_index;
    [Space]
    public GameObject camera;

    private void Start()
    {
            
    }

    private void Update()
    {
        Weapon_Fire();
    }

    private void LateUpdate()
    {
        
    }

    private void Weapon_Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycast_hit_information;
            if (Physics.Raycast(camera.transform.position,camera.transform.forward,out raycast_hit_information, Mathf.Infinity))
            {
                //Debug.Log(raycast_hit_information.collider.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(camera.transform.position, camera.transform.forward);
    }
}
