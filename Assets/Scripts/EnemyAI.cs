using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    //public PlayerControl playerControl;
    public SpriteRenderer spriteRenderer;
    private float player_pos_x;
    private float player_pos_y;
    private float mob_pos_x;
    private float mob_pos_y;
    private float distance_hor;
    private float distance_ver;
    private float distance_verhaeltnis_x;
    private float distance_verhaeltnis_y;
    private float summe_distance;
    private string facing_dir;
    public float range = 0.4f;
    public WeaponBehaviourMobs weaponBehaviourMobs;

    private GameObject playerObj = null;
    private GameObject mobObj = null;
    public float speed = 0.002f;

    private void Update(){

	//gets current location of player
	playerObj = GameObject.Find("player_0");
	player_pos_x = playerObj.transform.position.x;
	player_pos_y = playerObj.transform.position.y;
	
	//gets current location of mob
	mobObj = GameObject.Find("mob_0");
	mob_pos_x = mobObj.transform.position.x;
	mob_pos_y = mobObj.transform.position.y;
	
	//calculates x and y distance between player and mob
	distance_hor = player_pos_x - mob_pos_x;
	distance_ver = player_pos_y - mob_pos_y;

	//calculates ratio of distance in x and y
	summe_distance = System.Math.Abs(distance_hor) + System.Math.Abs(distance_ver);
	distance_verhaeltnis_x = System.Math.Abs(distance_hor)/summe_distance;
	distance_verhaeltnis_y = System.Math.Abs(distance_ver)/summe_distance;	

	//moves mob into player direction based on its speed
	if(distance_hor < 0f){
		mobObj.transform.Translate(-1 * speed * distance_verhaeltnis_x, 0, 0);	
	}
	if(distance_hor > 0f){
		mobObj.transform.Translate(speed * distance_verhaeltnis_x, 0, 0);
	}
	if(distance_ver < 0f){
		mobObj.transform.Translate(0, -1 * speed * distance_verhaeltnis_y, 0);	
	}
	if(distance_ver > 0f){
		mobObj.transform.Translate(0, speed * distance_verhaeltnis_y, 0);
	}

	//initialized spriteRenderer
	spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

	//turns mob sprite to player direction - TODO: maybe wegen der solution issue mit flip bei weapon? siehe solution player
	if(distance_hor>0){
		spriteRenderer.flipX = false;
		facing_dir = "right";
	}else if(distance_hor<0){
		spriteRenderer.flipX = true;
		facing_dir = "left";
	}

	//checks if player is in range for an attack to the left
	if(facing_dir == "left" && summe_distance <= range){
		weaponBehaviourMobs.AttackLeft();
	}
	//checks if player is in range for an attack to the left
	if(facing_dir == "right" && summe_distance <= range){
		weaponBehaviourMobs.AttackRight();
	}
    }
}
