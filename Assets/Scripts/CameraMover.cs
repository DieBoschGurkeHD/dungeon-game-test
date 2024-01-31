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

    private int current_floor = 1; 
    private int current_room = 0;

    private void Start(){
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor 0 - Room " + current_room);
    }
    public void MoveCameraUp(){
        if(moved_Camera == false){
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewRoom");
            gameObject.transform.Translate(0, move_in_y, 0);
            playerObj = GameObject.Find("player_0");
            playerObj.transform.Translate(0, 0.50f ,0);
            GUIObj = GameObject.Find("GUI_stats");
            GUIObj.transform.Translate(0, move_in_y ,0);
            moved_Camera = true;
            current_room +=1;
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor 0 - Room " + current_room);
            StartCoroutine(DelayDoorEnter());
        }
        return;
    }
    public void MoveCameraDown(){
        if(moved_Camera == false){
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewRoom");
            gameObject.transform.Translate(0, -1.0f * move_in_y, 0);
            playerObj = GameObject.Find("player_0");
            playerObj.transform.Translate(0, -0.50f ,0);
            GUIObj = GameObject.Find("GUI_stats");
            GUIObj.transform.Translate(0, -1.0f * move_in_y ,0);
            moved_Camera = true;
            current_room -=1;
            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor 0 - Room " + current_room);
            StartCoroutine(DelayDoorEnter());
        }
        return;
    }
    private IEnumerator DelayDoorEnter()
    {
	//stops door enters for door_delay duration
	yield return new WaitForSeconds(door_delay);
	moved_Camera = false;
    }
}
