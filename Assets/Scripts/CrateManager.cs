using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour
{
    public int numberOfDrops = 1;
     public GameObject coinDropPrefab;
     public GameObject bombDropPrefab;
     public GameObject healDropPrefab;
    
    public void getDestroyed(){
        //destroys crate, spawns drops
        Destroy(gameObject);
        while(numberOfDrops>0){
            System.Random ran = new System.Random();
            int random = ran.Next(0, 3);
            //spawns coins
            if(random == 0){
                GameObject CoinInstance = Instantiate(coinDropPrefab,gameObject.transform.position, gameObject.transform.rotation);    
            }
            //spawns bombs
            if(random == 1){
                GameObject BombInstance = Instantiate(bombDropPrefab,gameObject.transform.position, gameObject.transform.rotation);    
            }
            //spawns heals
            if(random == 2){
                GameObject HealInstance = Instantiate(healDropPrefab,gameObject.transform.position, gameObject.transform.rotation);    
            }
            numberOfDrops -= 1;
        }
    }
}
