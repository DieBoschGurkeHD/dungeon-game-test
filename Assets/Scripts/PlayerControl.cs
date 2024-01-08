using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    private Vector2 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    public WeaponBehaviour weaponBehaviour;
    private string facing_dir;
    private GameObject playerObj = null;
    //public UnityEvent OnAttackPerformed;

    private float player_pos_x;
    private float player_pos_y;

    private void Start()
    {
    	boxCollider = GetComponent<BoxCollider2D>();	   
    }


    // Update is called once per frame
    void Update()
    {
	//pointerInput = GetPointerInput();

	

	float x = Input.GetAxisRaw("Horizontal");
	float y = Input.GetAxisRaw("Vertical");

	moveDelta = new Vector3(x,y,0);

	Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

	//turn sprite to mouse
	if(rotation_z>-90 && rotation_z<0 || rotation_z>0 && rotation_z<90){
		transform.localScale = Vector3.one;
		facing_dir = "right";
	}else if(rotation_z>90 && rotation_z<180 || rotation_z>-180 && rotation_z<-90){
		transform.localScale = new Vector3(-1,1,1);
		facing_dir = "left";
	}

	//checking for collision in y direction
	hit = Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y*Time.deltaTime),LayerMask.GetMask("Characters","Blocking"));
	if (hit.collider == null)
	{
	transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
	}
	//checking for collision in x direction
	hit = Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(moveDelta.x,0),Mathf.Abs(moveDelta.x*Time.deltaTime),LayerMask.GetMask("Characters","Blocking"));
	if (hit.collider == null)
	{
	transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
	}
	
	if(Input.GetMouseButtonDown(0) && facing_dir == "left"){
		Debug.Log("Hit Left!");
		weaponBehaviour.AttackLeft();
	}
	if(Input.GetMouseButtonDown(0) && facing_dir == "right"){
		Debug.Log("Hit Right!");
		weaponBehaviour.AttackRight();
	}
	
	
	playerObj = GameObject.Find("player_0");
	//Debug.Log(playerObj.transform.position.x);
	player_pos_x = playerObj.transform.position.x;
	player_pos_y = playerObj.transform.position.y;
	//Debug.Log(player_pos);
	
    }
    //private Vector2 GetPointerInput(){
	//Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
	//mousePos.z = Camera.main.nearClipPlane;
	//return Camera.main.ScreenToWorldPoint(mousePos);
    //}
    
    public float getPlayerPosX(){
	return player_pos_x;
}
    public float getPlayerPosY(){
	return player_pos_y;
}
}

    //public void TriggerAttack(){
	//OnAttackPerformed?.Invoke();
//}
//}