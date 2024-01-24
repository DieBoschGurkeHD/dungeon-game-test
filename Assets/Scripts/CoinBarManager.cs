using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBarManager : MonoBehaviour
{
    public PlayerControl playerControl;
    private int coin_counter;
    void Update()
    {
        coin_counter = playerControl.coin_counter;
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(coin_counter.ToString());
    }
}
