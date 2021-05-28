using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Music : MonoBehaviour
{
    [ExecuteInEditMode]
    public AudioSource source;
    [Header("Soundtrack Name")]
    public string soundtrack_name;
    [Header("Soundtrack attributes")]
    [Range(0,1)]public float volume;
    [Space]
    public bool is_muted;

    private void Start()
    {
        soundtrack_name = source.name.ToString();
        source.volume = volume;
    }

    private void LateUpdate()
    {
        source.volume = volume;
    }
}
