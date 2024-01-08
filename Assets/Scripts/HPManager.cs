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
	//initialized HP etc
	currentHP = hpValue;
	maxHP = hpValue;
	isDead = false;
	spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
}
    public void GetHit(int amount, GameObject sender, float strength){
	//when mob/player gets hit

	//checks if its still alive
	if(isDead){
		return;
	}
	//checks if object hit himself
	if(sender.layer == gameObject.layer){
		return;
	}
	
	//takes damage
	currentHP -= amount;
	if(currentHP > 0){
		OnHitWithReference?.Invoke(sender);
		StartCoroutine("ColorHit");
		KnockBack(sender, strength);
	//dies to damage
	}else{
		OnDeathWithReference?.Invoke(sender);
		isDead = true;
		Destroy(gameObject);
	}
    }
	IEnumerator ColorHit()
	{
		//plays hit effect
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.color = Color.white;
	}

	public void KnockBack(GameObject sender, float strength){
		//apply knockback 
		Vector3 direction = gameObject.transform.position - sender.transform.position;
		direction = direction * strength;
		gameObject.transform.Translate(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0);
		return;
}}
