using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    public int currentHP, maxHP;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    public void InitializeHP(int hpValue){
	currentHP = hpValue;
	maxHP = hpValue;
	isDead = false;
}
    public void GetHit(int amount, GameObject sender){
	Debug.Log("IN HP CLASS");
	if(isDead){
		Debug.Log("HP0");
		return;
	}
	if(sender.layer == gameObject.layer){
		Debug.Log("HP1");
		return;
	}

	currentHP -= amount;
	Debug.Log("HP2");
	if(currentHP > 0){
		OnHitWithReference?.Invoke(sender);
	}else{
		OnDeathWithReference?.Invoke(sender);
		isDead = true;
		Destroy(gameObject);
	}
    }
}
