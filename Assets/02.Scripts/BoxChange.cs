using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChange : MonoBehaviour
{
    public int box;
    public Material[] mats;
    MeshRenderer matRen;

    void Awake()
    {
        matRen = GetComponent<MeshRenderer>();
        matRen.material = mats[box];
    }
    public void OnPointEnter()
    {
        box++;
        box %= 4;
        matRen.material = mats[box];
        gameObject.transform.parent.gameObject.GetComponent<DoorOpen>().ClerCheck();
    }
}
