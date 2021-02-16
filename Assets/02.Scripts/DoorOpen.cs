using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public BoxChange[] boxs;
    public GameObject autoDoor;
    Animator doorAnim;
    public string clear;//문 열리는 조건
    bool check = false;
    void Awake()
    {
        boxs = GetComponentsInChildren<BoxChange>();
        if (autoDoor != null)
            doorAnim = autoDoor.transform.GetComponent<Animator>();
    }

    public void ClerCheck()
    {
        check = true;
        switch (clear)
        {
            case "Red":
                foreach (BoxChange b in boxs)
                {
                    if (b.box != 1)
                        check = false;
                }
                break;
        }
        if (check)
        {
            doorAnim.SetTrigger("Open");
            PlayerMove p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            p.move = true;
        }

    }
}
