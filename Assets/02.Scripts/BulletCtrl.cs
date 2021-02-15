using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{

    private Rigidbody rb;

	void Start ()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 2500.0f);

	}

    // Update is called once per frame
    void Update()
    {
        // 바닥으로 일정거리를 초과하면 삭제....
        if (transform.position.y < -500)
            Destroy(this.gameObject);
    }

}
