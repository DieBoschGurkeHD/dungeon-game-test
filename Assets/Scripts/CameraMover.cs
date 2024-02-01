using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMover : MonoBehaviour
{
    private bool moved_Camera = false;
    private float door_delay = 1.0f;
    private GameObject playerObj;
    private GameObject GUIObj;
    private float player_pos_y;
    private float move_in_y = 2.55f;

    private int current_floor = 0; 
    private int current_room = 0;

    private float camera_pos = 0;
    public bool doors_locked = false;

    public GameObject mobSpawnPrefab;

    private GameObject mobParentObj;
    public int spawn_amount = 3;

    private int room_progress = 0;


    private void Start(){
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor "+ current_floor + " - Room " + current_room);
    }
    //moves camera up on next level enter
    public void MoveCameraUp(){
        if(moved_Camera == false && doors_locked == false){
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewRoom");
            gameObject.transform.Translate(0, move_in_y, 0);
            camera_pos = camera_pos + move_in_y;
            playerObj = GameObject.Find("player_0");
            playerObj.transform.Translate(0, 0.50f ,0);
            GUIObj = GameObject.Find("GUI_stats");
            GUIObj.transform.Translate(0, move_in_y ,0);
            moved_Camera = true;
            current_room +=1;
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor "+ current_floor + " - Room " + current_room);
            StartCoroutine(DelayDoorEnter());
            SpawnMobs();
        }
        return;
    }
    //moves camera down on last level enter
    public void MoveCameraDown(){
        if(moved_Camera == false && doors_locked == false){
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewRoom");
            gameObject.transform.Translate(0, -1.0f * move_in_y, 0);
            camera_pos = camera_pos - move_in_y;
            playerObj = GameObject.Find("player_0");
            playerObj.transform.Translate(0, -0.50f ,0);
            GUIObj = GameObject.Find("GUI_stats");
            GUIObj.transform.Translate(0, -1.0f * move_in_y ,0);
            moved_Camera = true;
            current_room -=1;
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor "+ current_floor + " - Room " + current_room);
            StartCoroutine(DelayDoorEnter());
        }
        return;
    }
    public void SpawnMobs(){
        //checks if room was defeated before
        if(room_progress >= current_room){
            return;
        }
        for (int i = 0; i != spawn_amount; i++) {
            //locks doors
            doors_locked = true;
            //spawns mob at random pos
            float random_x_pos = Random.Range(-1.5f, 1.5f);
            GameObject MobInstance = Instantiate(mobSpawnPrefab, new Vector3 (random_x_pos,camera_pos,0), Quaternion.identity);
            mobParentObj = GameObject.Find("MobParent");
            MobInstance.transform.parent=mobParentObj.transform;

            //randomly changes skin
            Animator animatorSkin = MobInstance.GetComponent<Animator>();
            System.Random ran = new System.Random();
            int random = ran.Next(0, 2);
            //changes to masked mob
            if(random == 0){
                animatorSkin.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/mob_0") as RuntimeAnimatorController;
            }
            //changes to small ork
            if(random == 1){
                animatorSkin.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/mob_1") as RuntimeAnimatorController;
            }

            //randomly changes weapon
            Animator animatorWeapon = MobInstance.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            WeaponBehaviourMobs weaponBehaviourMobs = MobInstance.transform.GetChild(0).gameObject.GetComponent<WeaponBehaviourMobs>();
            random = ran.Next(0, 2);
            //changes to sword_1
            if(random == 0){
                animatorWeapon.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/WeaponSwordAnimator") as RuntimeAnimatorController;
                weaponBehaviourMobs.radius = 0.17f;
			    weaponBehaviourMobs.weapon_strength = 17;
            }
            //changes to hammer_0
            if(random == 1){
                animatorWeapon.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/WeaponAnimation") as RuntimeAnimatorController;
                weaponBehaviourMobs.radius = 0.20f;
			    weaponBehaviourMobs.weapon_strength = 20;
            }
        }
        
    }
    //reopens doors and plays room cleared
    public void RoomCleared(){
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Room cleared!");
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewRoom");
        doors_locked = false;
        room_progress = room_progress+1;
    }

    private IEnumerator DelayDoorEnter()
    {
	//stops door enters for door_delay duration
	yield return new WaitForSeconds(door_delay);
	moved_Camera = false;
    }
}
