using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attributes : MonoBehaviour
{
    [Header("GameManager gameobject")]
    public GameObject gamemanager;
    [Space]
    public GameObject child_graphics;
    [Header("Enemy Health attributes")]
    public float health;
    [Space]
    public Sprite current_sprite;
    public Sprite[] six_degree_sprite;
    [Space]
    public bool is_inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        current_sprite = six_degree_sprite[0];
        child_graphics.GetComponent<SpriteRenderer>().flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateAngle();
    }

    private void LateUpdate()
    {
        child_graphics.transform.LookAt(gamemanager.GetComponent<_GameManager>().player.transform.position);
        SetSprite();
    }

    public void SetSprite()
    {
        int num = Sprite_Direction_Index_Calculator();
        current_sprite = six_degree_sprite[num];
        child_graphics.GetComponent<SpriteRenderer>().sprite = current_sprite;
        child_graphics.GetComponent<SpriteRenderer>().flipX = is_inverted;
    }

    public int Sprite_Direction_Index_Calculator()
    {
        ///This calculates which index to use for the sprite and decides if sprite invertion is needed.
        ///Maybe hasty on perfomance (fix to implement?)
        int dir = 0;
        float calculated_angle = CalculateAngle();
        if (calculated_angle < 20 && calculated_angle > -20)
        {
            dir = 0;
            is_inverted = false;
        }
        else if (calculated_angle > 20 && calculated_angle < 85)
        {
            dir = 1;
            is_inverted = false;
        }
        else if (calculated_angle > 85 && calculated_angle < 95)
        {
            dir = 2;
            is_inverted = false;
        }
        else if (calculated_angle > 95 && calculated_angle < 140)
        {
            dir = 3;
            is_inverted = false;
        }
        else if (calculated_angle > 140 && calculated_angle < 180)
        {
            dir = 4;
            is_inverted = false;
        }
        else if (calculated_angle < -20 && calculated_angle > -85)
        {
            dir = 1;
            is_inverted = true;
        }
        else if (calculated_angle < -85 && calculated_angle > -95)
        {
            dir = 2;
            is_inverted = true;
        }
        else if (calculated_angle < -95 && calculated_angle > -140)
        {
            dir = 3;
            is_inverted = true;
        }
        else if (calculated_angle < -140 && calculated_angle > -180)
        {
            dir = 4;
            is_inverted = false;
        }
        return dir;
    }

    private float CalculateAngle()
    {
        ///Calculates the angle between two vectors.
        ///One of the Vector is the enemy's forward position
        ///The last Vector is the Enemy/Player vector (Calculate magnitude from Enemy pont and Player point with subtraction)
        ///Signed agle returns positive and negative values
        Vector3 enemy_Directional_Vector = transform.forward;
        Vector3 player_Enemy_Vector = gamemanager.GetComponent<_GameManager>().player.transform.position - transform.position;
        float view_angle = Vector3.SignedAngle(enemy_Directional_Vector,player_Enemy_Vector,Vector3.up);
        return view_angle;
    }
}
