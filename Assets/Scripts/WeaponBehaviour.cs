using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{    
    public Vector2 Pointerposition { get; set; }

    public Animator animator;
    public float hit_delay = 0.3f;
    private bool attackBlocked;
    public Transform circleOrigin;
    public float radius;

    // Update is called once per frame
    void Update(){
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

	Debug.Log("player: " + rotation_z);

        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z+90);
	
	Vector2 scale = transform.localScale;

/*

        if(rotation_z>90 || rotation_z<-90){ // > 90 //links
            scale.y = -1;
	    Debug.Log("Links!");
	}else if(rotation_z<90 && rotation_z>-90){ // < 90 //rechts 
	    scale.y = -1;
	    Debug.Log("Rechts!");
        }
*/

	scale.y = -1;
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
	animator.SetTrigger("AttackLeft"); //weirder bug mit der weapon rotation, player flippt bei richtungswechsel weapon mit, bei mob aber nicht - deswegen 2x attackleft als temp fix, idk anders funzt einfach nicht
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
		if(health = collider.GetComponent<HPManager>()){
			health.GetHit(1,transform.parent.gameObject);
}
}

}
}