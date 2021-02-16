using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public bool move = false;

    // Update is called once per frame
    void Update()
    {
        if(move)
            transform.Translate(new Vector3(0, 0, 0.01f));
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Stop")
        {
            move = false;
            col.gameObject.SetActive(false);
        }

    }
}
