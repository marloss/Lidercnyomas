using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _GameManager : MonoBehaviour
{
    [Header("Scene_Name")]
    public string current_Scene_Name;
    Scene current_Scene;
    [Header("Soundtracks and Sound effects")]
    public Music[] soundtrack;
}
