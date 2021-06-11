using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Weapon attributes")]
    public bool is_hitscan;
    [Space]
    public int ammunition;
    [Space]
    public float y_offset;
    [Space]
    public float widht;
    public float height;

    private void Start()
    {
        gameObject.GetComponent<Image>().rectTransform.localScale = new Vector3(widht, height);
    }
}
