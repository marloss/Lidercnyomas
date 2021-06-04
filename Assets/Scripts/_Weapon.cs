using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Weapon : MonoBehaviour
{
    [Header("Weapon Attributes")]
    public bool is_Pickedup;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Vector2 v = new Vector3(transform.position.x + Mathf.PingPong(Time.time,1), transform.position.y);
        //transform.position = v;
    }
}
