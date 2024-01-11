using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombBarManager : MonoBehaviour
{
    public PlayerControl playerControl;
    private int bomb_counter;
    void Update()
    {
        bomb_counter = playerControl.bomb_counter;
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(bomb_counter.ToString());
    }
}
