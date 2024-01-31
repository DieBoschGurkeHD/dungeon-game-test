using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviourMobs : MonoBehaviour
{    
	public Vector2 Pointerposition { get; set; }
    public Animator animator;
    public float hit_delay = 0.3f;
    private bool attackOnCooldown;
    public Transform circleOrigin;
    public float radius;
    private float player_pos_x;
    private float player_pos_y;
    private GameObject playerObj = null;
    private GameObject mobObj = null;
    private float mob_pos_x;
    private float mob_pos_y;
    private float rotation_z;
	public float weapon_strength = 20;
    public void  Update(){
	//gets angle to x axis from 0,0 to current mouse location - unnötig maybe? (TODO)
    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    difference.Normalize();

	//gets current location of player
	playerObj = GameObject.Find("player_0");

	//ends script if player is dead
	if(playerObj == null){
			gameObject.SetActive(false);
			return;
		}

	player_pos_x = playerObj.transform.position.x;
	player_pos_y = playerObj.transform.position.y;

	//gets current location of mob
	//mobObj = GameObject.Find("mob_0");
	mobObj = gameObject.transform.parent.gameObject;
	mob_pos_x = mobObj.transform.position.x;
	mob_pos_y = mobObj.transform.position.y;

	//calculates x and y distance between player and mob
	float xDiff = player_pos_x - mob_pos_x;
	float yDiff = player_pos_y - mob_pos_y;
	//gets angle to x axis from mob position to player position and rotates sprite towards player
	rotation_z = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90);
	Vector2 scale = transform.localScale;

	//handles correct sprite rotation
        if(Mathf.Abs(rotation_z) < 0){
            scale.y = 1;
        }else if(Mathf.Abs(rotation_z) > 0){
            scale.y = -1;
        }
        transform.localScale = scale;
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