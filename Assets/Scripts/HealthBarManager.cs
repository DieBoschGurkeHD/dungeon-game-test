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
    Dictionary <int, SpriteRenderer> heartRenderer = new Dictionary<int, SpriteRenderer>();
    public Sprite heart_full;
    public Sprite heart_half;
    public Sprite heart_empty;

    void Start()
    {
        heartRenderer.Add(0, spriteRenderer_0);
        heartRenderer.Add(1, spriteRenderer_1);
        heartRenderer.Add(2, spriteRenderer_2);
        heartRenderer.Add(3, spriteRenderer_3);
        heartRenderer.Add(4, spriteRenderer_4);
    }

    void Update()
    {
        currentHP = hPManager.currentHP;
        for(int i = 0; i<5; i++)
        {
            if((i+1)*2 >= currentHP){
                switch(currentHP %2){
                    case 0:
                        heartRenderer[i].sprite = heart_full;  
                    break;
                    case 1:
                        heartRenderer[i].sprite = heart_half;  
                    break;              
                }
                if(currentHP == 0){
                        heartRenderer[i].sprite = heart_empty;
                }
                currentHP = 0;}
            else{
                heartRenderer[i].sprite = heart_full; 
            } 
        }
    }
}
