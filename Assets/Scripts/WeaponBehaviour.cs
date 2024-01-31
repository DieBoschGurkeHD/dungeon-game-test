using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{    
    public Vector2 Pointerposition { get; set; }

    private Animator animator;
    public float hit_delay = 0.3f;
    private bool attackOnCooldown;
    public Transform circleOrigin;
    public float radius;
	public int weapon_strength = 20;
	private SpriteRenderer spriteRenderer;

    void Update(){
		animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
		circleOrigin = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Transform>();
		//gets angle to x axis from 0,0 to current mouse location
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		//rotates weapon sprite based on mouse location
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z+90);
	
		//flips weapon sprite - keine Ahnung mehr warum tbh, maybe nicht necessary (TODO)
		//flips sprite in y dir
		Vector2 scale = transform.localScale;
		scale.y = -1;
    	transform.localScale = scale;

		spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		//spriteRenderer.flipY = true;
		if(rotation_z < 90 && rotation_z > 0 || rotation_z < 0 && rotation_z > -90){
			spriteRenderer.flipX = true;
		}else{
			spriteRenderer.flipX = false;
		}
		
    }
    public void AttackLeft(){
	//check for attack cd
	if(attackOnCooldown)
		return;
	//starts attack animation + cooldown
	animator.SetTrigger("AttackLeft");
	attackOnCooldown = true;
	StartCoroutine(DelayAttack());
}
    public void AttackRight(){
	//check for attack cd
	if(attackOnCooldown)
		return;
	//starts attack animation + cooldown
	animator.SetTrigger("AttackRight");
	attackOnCooldown = true;
	StartCoroutine(DelayAttack());
}
    private IEnumerator DelayAttack()
    {
	//stops attacks for hit_delay duration
	yield return new WaitForSeconds(hit_delay);
	attackOnCooldown = false;
}
    private void OnDrawGizmosSelected(){
	//draws circle that simulates weapon hitbox
	Gizmos.color = Color.blue;
	Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
	Gizmos.DrawWireSphere(position, radius);
}
    public void DetectColliders(){
	//checks for collision with other objects
	foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius)){
		HPManager health;
		//if they have an HPManager run hit script
		if(health = collider.GetComponent<HPManager>()){
			health.GetHit(1,transform.parent.gameObject, weapon_strength);
}
}
}
}