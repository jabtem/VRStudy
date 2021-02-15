using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오큘러스 버전(미션)
public class BoxCtrl : MonoBehaviour
{

    public OculusPlanesMaker pm;
    public int box;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            if (box == 1)
            {
                pm.StopPlayPlanes();
            }
            else if (box == 2)
            {
                StartCoroutine(pm.PlusPlane());
            }
            else if (box == 3)
            {
                pm.SpeedUp();
            }
        }

    }

}
