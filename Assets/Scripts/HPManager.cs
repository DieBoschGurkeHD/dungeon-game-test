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
	public SpriteRenderer spriteRenderer;

    public void InitializeHP(int hpValue){
	currentHP = hpValue;
	maxHP = hpValue;
	isDead = false;
	spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
}
    public void GetHit(int amount, GameObject sender){
	
	
	if(isDead){
		return;
	}
	if(sender.layer == gameObject.layer){
		return;
	}

	currentHP -= amount;
	if(currentHP > 0){
		OnHitWithReference?.Invoke(sender);
		StartCoroutine("ColorHit");
	}else{
		OnDeathWithReference?.Invoke(sender);
		isDead = true;
		Destroy(gameObject);
	}
    }
	IEnumerator ColorHit()
	{
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.color = Color.white;
	}
}
