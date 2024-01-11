using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStartExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private GameObject bombObj = null;

    public GameObject explosionPrefab;

    public Transform circleOrigin;
    public float radius;
	public int weapon_strength = 20;

    public void PlayExplosion(){
		bombObj = GameObject.Find("bomb_0(Clone)");
        GameObject ExplosionInstance = Instantiate(explosionPrefab,bombObj.transform.position, bombObj.transform.rotation);
		explosion = ExplosionInstance.transform.GetComponent<ParticleSystem>();
		explosion.Emit(250);
        DetectColliders();
		Destroy(bombObj);
        return;
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
        CrateManager crate;
		//if they have an HPManager run hit script
		if(health = collider.GetComponent<HPManager>()){
			health.GetHit(5,gameObject, weapon_strength);
            }
        if(crate = collider.GetComponentInParent<CrateManager>()){
            crate.getDestroyed();
        }
}
}
}
