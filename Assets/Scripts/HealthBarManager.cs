using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    //private gameObject playerObj;
    public HPManager hPManager;
    private int currentHP;

    //public HealthContainer_0 healthContainer_0;
    public SpriteRenderer spriteRenderer_0;
    public SpriteRenderer spriteRenderer_1;
    public SpriteRenderer spriteRenderer_2;
    public SpriteRenderer spriteRenderer_3;
    public SpriteRenderer spriteRenderer_4;

    public Sprite heart_full;
    public Sprite hearth_half;
    public Sprite hearth_empty;

    void Update()
    {
        currentHP = hPManager.currentHP;
        Debug.Log("currentHP: " + currentHP);
        if(currentHP<10){
            spriteRenderer_4.sprite = hearth_half;
        }
        if(currentHP<9){
            spriteRenderer_4.sprite = hearth_empty;
        }
        if(currentHP<8){
            spriteRenderer_3.sprite = hearth_half;
        }
        if(currentHP<7){
            spriteRenderer_3.sprite = hearth_empty;
        }
        if(currentHP<6){
            spriteRenderer_2.sprite = hearth_half;
        }
        if(currentHP<5){
            spriteRenderer_2.sprite = hearth_empty;
        }
        if(currentHP<4){
            spriteRenderer_1.sprite = hearth_half;
        }
        if(currentHP<3){
            spriteRenderer_1.sprite = hearth_empty;
        }
        if(currentHP<2){
            spriteRenderer_0.sprite = hearth_half;
        }
        if(currentHP<1){
            spriteRenderer_0.sprite = hearth_empty;
        }
    }
}
