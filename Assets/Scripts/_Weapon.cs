using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Weapon : MonoBehaviour
{
    [Header("Gamemanager")]
    public GameObject gamemanager;
    [Header("Weapon Attributes")]
    public bool is_Pickedup;
    [Space]
    public float vertical_speed;
    public float altitude;
    [Space]
    public Vector3 _position;

    private void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        _position = transform.position;
    }

    private void Update()
    {
        transform.LookAt(gamemanager.GetComponent<_GameManager>().player.transform.position);
    }

    private void FixedUpdate()
    {
        _position.y = Mathf.Sin(Time.realtimeSinceStartup * vertical_speed) * altitude;
        transform.position = _position;
    }
}
