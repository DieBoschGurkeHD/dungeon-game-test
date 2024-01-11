using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour
{
    public int numberOfDrops = 1;
     public GameObject coinDropPrefab;
    public void getDestroyed(){
        //destroys crate, drops coin
        Destroy(gameObject);
        while(numberOfDrops>0){
            GameObject CoinInstance = Instantiate(coinDropPrefab,gameObject.transform.position, gameObject.transform.rotation);
            numberOfDrops -= 1;
        }
    }
}
