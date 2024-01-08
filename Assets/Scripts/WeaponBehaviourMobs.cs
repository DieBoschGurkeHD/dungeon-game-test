using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviourMobs : MonoBehaviour
{    
    public Vector2 Pointerposition { get; set; }

    public Animator animator;
    public float hit_delay = 0.3f;
    private bool attackBlocked;
    private float player_pos_x;
    private float player_pos_y;
    public Transform circleOrigin;
    public float radius;
    private GameObject playerObj = null;
    private GameObject mobObj = null;
    private float mob_pos_x;
    private float mob_pos_y;


    private float rotation_z;
    // Update is called once per frame
    void Update(){
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

	playerObj = GameObject.Find("player_0");
	player_pos_x = playerObj.transform.position.x;
	player_pos_y = playerObj.transform.position.y;

	mobObj = GameObject.Find("mob_0");
	mob_pos_x = mobObj.transform.position.x;
	mob_pos_y = mobObj.transform.position.y;

/*
	//Atan2 redo aber mit mob position als startpunkt statt 0/0
	if(player_pos_x > mob_pos_x && player_pos_y > mob_pos_y){
		Debug.Log("ecke oben rechts");
		rotation_z = Mathf.Atan(player_pos_x - mob_pos_x/player_pos_y - mob_pos_y) * Mathf.Rad2Deg;;
		Debug.Log("tan= " + rotation_z);
	}
	if(player_pos_x < mob_pos_x && player_pos_y < mob_pos_y){
		Debug.Log("ecke unten links");
		rotation_z = Mathf.Atan(player_pos_x - mob_pos_x/player_pos_y - mob_pos_y) * Mathf.Rad2Deg;
		Debug.Log("tan= " + rotation_z);
	}
	if(player_pos_x > mob_pos_x && player_pos_y < mob_pos_y){
		Debug.Log("ecke unten rechts");
		rotation_z = Mathf.Atan(player_pos_x - mob_pos_x/player_pos_y - mob_pos_y) * Mathf.Rad2Deg;
		Debug.Log("tan= " + rotation_z);
	}
	if(player_pos_x < mob_pos_x && player_pos_y > mob_pos_y){
		Debug.Log("ecke oben links");
		rotation_z = Mathf.Atan(player_pos_x - mob_pos_x/player_pos_y - mob_pos_y) * Mathf.Rad2Deg;
		Debug.Log("tan= " + rotation_z);
	}
*/
	float xDiff = player_pos_x - mob_pos_x;
	float yDiff = player_pos_y - mob_pos_y;
	rotation_z = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;

	//Debug.Log("tan= " + rotation_z);
	
        //rotation_z = Mathf.Atan2(player_pos_x - mob_pos_x, player_pos_y - mob_pos_y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90); //rotation + 90
	
	Vector2 scale = transform.localScale;

	Debug.Log("mob= " + rotation_z);

        if(Mathf.Abs(rotation_z) < 0){
            scale.y = 1;
        }else if(Mathf.Abs(rotation_z) > 0){
            scale.y = -1;
        }
        transform.localScale = scale;
    }
    public void AttackLeft(){
	if(attackBlocked)
		return;
	animator.SetTrigger("AttackLeft");
	//Debug.Log("Attack Left!");
	attackBlocked = true;
	StartCoroutine(DelayAttack());
}

    public void AttackRight(){
	if(attackBlocked)
		return;
	animator.SetTrigger("AttackRight");
	//Debug.Log("Attack Right!");
	attackBlocked = true;
	StartCoroutine(DelayAttack());
}

    private IEnumerator DelayAttack()
    {
	yield return new WaitForSeconds(hit_delay);
	attackBlocked = false;
}

    private void OnDrawGizmosSelected(){
	Gizmos.color = Color.blue;
	Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
	Gizmos.DrawWireSphere(position, radius);
}
    public void DetectColliders(){
	foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius)){
		//Debug.Log(collider.name);
		HPManager health;
		Debug.Log("HP3");
		if(health = collider.GetComponent<HPManager>()){
			health.GetHit(1,transform.parent.gameObject);
}
}

}
}