using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    private Vector2 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
	private RaycastHit2D pickup_coin;
    public WeaponBehaviour weaponBehaviour;
	public SpriteRenderer spriteRenderer;
	public HPManager hPManager;

	[SerializeField] public ParticleSystem explosion;
    private string facing_dir;

	private bool weapon_swap_cd = false; 
	private bool skin_swap_cd = false;

	private float weapon_swap_cd_time = 0.25f;

	private float skin_swap_cd_time = 0.25f;

	private GameObject coinObj = null;
	private GameObject bombObj = null;
    private GameObject playerObj = null;

	private GameObject healObj = null;
	public int coin_counter;
	public int bomb_counter;

    private float player_pos_x;
    private float player_pos_y;

	public GameObject bombDropPrefab;

	public CameraMover cameraMover;

	private GameObject mobParentObj;

    private void Start()
    {
    	//initializes collision
		boxCollider = GetComponent<BoxCollider2D>();	   
    }

    void Update()
    {

	//gets player keyboard input
	float x = Input.GetAxisRaw("Horizontal");
	float y = Input.GetAxisRaw("Vertical");

	if(Input.GetKeyDown("space")){
		PlaceBomb();
	}

	//player move new position
	moveDelta = new Vector3(x,y,0);

	//gets angle to x axis from 0,0 to current mouse location
	Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

	//turns sprite to mouse
	if(rotation_z>-90 && rotation_z<0 || rotation_z>0 && rotation_z<90){
		spriteRenderer.flipX = false;
		//transform.localScale = Vector3.one;
		facing_dir = "left";
	}else if(rotation_z>90 && rotation_z<180 || rotation_z>-180 && rotation_z<-90){
		//transform.localScale = new Vector3(-1,1,1);
		spriteRenderer.flipX = true;
		facing_dir = "right";
	}

	//checking for collision in y direction
	hit = Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y*Time.deltaTime),LayerMask.GetMask("Characters","Blocking"));
	//no collision
	if (hit.collider == null)
	{
	transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
	}
	//if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Coins"));
	//checking for collision in x direction
	hit = Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(moveDelta.x,0),Mathf.Abs(moveDelta.x*Time.deltaTime),LayerMask.GetMask("Characters","Blocking"));
	//no collision
	if (hit.collider == null)
	{
	transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
	}
	//checks for player attack while facing left
	if(Input.GetMouseButtonDown(0) && facing_dir == "left"){
		weaponBehaviour.AttackLeft();
	}
	//checks for player attack while facing right
	if(Input.GetMouseButtonDown(0) && facing_dir == "right"){
		weaponBehaviour.AttackRight();
	}

	//gets current location of player - was das? needed?
	playerObj = GameObject.Find("player_0");
	player_pos_x = playerObj.transform.position.x;
	player_pos_y = playerObj.transform.position.y;

	//checks if room has been defeated
	mobParentObj = GameObject.Find("MobHolder");
	if(mobParentObj.transform.childCount == 0 && cameraMover.doors_locked == true){
		cameraMover.RoomCleared();
	}
    }
	//places bomb at player_pos
	public void PlaceBomb(){
		if(bomb_counter<=0){
			return;
		}
		bomb_counter -= 1;
		GameObject BombInstance = Instantiate(bombDropPrefab,gameObject.transform.position, gameObject.transform.rotation);
	}

	void OnCollisionStay2D(Collision2D col){
		//checks for collisions with coins
		if(col.gameObject.tag == "coin"){
			coinObj = GameObject.Find("coin_0(Clone)");
			coin_counter += 1;
			Destroy(coinObj);
		}
		//checks for collisions with bombs
		if(col.gameObject.tag == "bomb"){
			bombObj = GameObject.Find("bomb_pickup_0(Clone)");
			bomb_counter += 1;
			Destroy(bombObj);
		//checks for collision with healing
		}
		if(col.gameObject.tag == "heal"){
			healObj = GameObject.Find("heal_0(Clone)");
			int new_currentHP = hPManager.currentHP + 2;
			hPManager.setNewCurrentHP(new_currentHP);
			Destroy(healObj);
		}
		//checks for collision with doors
		if(col.gameObject.tag == "door"){
			if(cameraMover.transform.position.y < player_pos_y){
				cameraMover.MoveCameraUp();
			}
			if(cameraMover.transform.position.y > player_pos_y){
				cameraMover.MoveCameraDown();
			}
		}
		//checks for collisions with weapon_swap sword
		if(col.gameObject.tag == "weapon_sword" && weapon_swap_cd == false){
			weapon_swap_cd = true;
			weaponBehaviour.radius = 0.17f;
			weaponBehaviour.weapon_strength = 17;
			Animator animator = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>();
			animator.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/WeaponSwordAnimator") as RuntimeAnimatorController;
			StartCoroutine(DelayNewWeapon());
		}
		//checks for collisions with weapon_swap hammer
		if(col.gameObject.tag == "weapon_hammer" && weapon_swap_cd == false){
			weapon_swap_cd = true;
			weaponBehaviour.radius = 0.20f;
			weaponBehaviour.weapon_strength = 20;
			Animator animator = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>();
			animator.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/WeaponAnimation") as RuntimeAnimatorController;
			StartCoroutine(DelayNewWeapon());
		}
		//checks for collisions with skin_swap dino_teal
		if(col.gameObject.tag == "skin_dino_t" && skin_swap_cd == false){
			skin_swap_cd = true;
			Animator animator = gameObject.GetComponent<Animator>();
			animator.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/skin_dino_t") as RuntimeAnimatorController;
			StartCoroutine(DelayNewSkin());
		}
		//checks for collisions with skin_swap knight_orange
		if(col.gameObject.tag == "skin_knight_o" && skin_swap_cd == false){
			skin_swap_cd = true;
			Animator animator = gameObject.GetComponent<Animator>();
			animator.runtimeAnimatorController = Resources.Load("Animations/AnimationStates/player_0") as RuntimeAnimatorController; //TODO animation renamen
			StartCoroutine(DelayNewSkin());
		}

	}
	private IEnumerator DelayNewWeapon()
    	{	
		//stops weapon pickup for weapon_swap_cd_time duration
		yield return new WaitForSeconds(weapon_swap_cd_time);
		weapon_swap_cd = false;
		}
	private IEnumerator DelayNewSkin()
    	{	
		//stops skin pickup for skin_swap_cd_time duration
		yield return new WaitForSeconds(skin_swap_cd_time);
		skin_swap_cd = false;
		}	
}

