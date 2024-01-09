using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDmgPopUp : MonoBehaviour
{
    public void DestroyParentDmgPopUp(){
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }
}
