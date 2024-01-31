using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    private WeaponBehaviour weaponBehaviour;
    public UnityEvent OnAttackPerformed;

    public void Start(){
        weaponBehaviour = gameObject.GetComponentInParent<WeaponBehaviour>();
        if(weaponBehaviour != null){
            OnAttackPerformed.AddListener(weaponBehaviour.DetectColliders);
        }   
    }
    public void TriggerAttack(){
	OnAttackPerformed?.Invoke();
}
}
