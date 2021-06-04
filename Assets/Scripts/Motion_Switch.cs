using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion_Switch : MonoBehaviour
{
    [Header("Motion switch attributes")]
    public GameObject gamemanager;
    [Space]
    public bool is_Activated;
    [Space]
    public Light _light;
    [Space]
    public AudioSource sound;

    private void Start()
    {
        _light.enabled = false;
    }

    private void LateUpdate()
    {
        ActivateLight(_light, is_Activated);
    }

    void ActivateLight(Light l, bool on)
    {
        switch (on)
        {
            case true:
                l.enabled = true;
                break;
            case false:
                l.enabled = false;
                break;
        }
    }

    void ActivateSound(AudioSource s, bool on)
    {
        //
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
            is_Activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
            is_Activated = false;
    }
}
