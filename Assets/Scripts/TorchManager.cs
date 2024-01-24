using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    private GameObject torchChild;
    public GameObject torch_0Prefab;

    public void unlightTorch(){
        torchChild = gameObject.transform.GetChild(0).gameObject;
        Destroy(torchChild);
        GameObject TorchInstance = Instantiate(torch_0Prefab,gameObject.transform.position, gameObject.transform.rotation);
    }
}
