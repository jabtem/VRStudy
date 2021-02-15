using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    // 전진 속도
    public int speedForward = 12;
    // 옆걸음 속도
    public int speedSide = 6;
    // 발사할 총알
    public GameObject bullet;
    // 발사되는 위치
    public Transform firePos;

    // 플레이어 트랜스폼
    private Transform tr;
    private float dirX = 0;
    private float dirZ = 0;

	// Use this for initialization
	void Start () {

        tr = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        BulletFire();
        MovePlayer();

	}

    // 총알 발사
    void BulletFire()
    {
        // 트리거를 당겼을 경우(총알 발사)
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Instantiate(bullet, firePos.position, firePos.rotation);
        }
    }

    // 주인공 이동
    void MovePlayer()
    {
        // 좌우 이동 방향(왼쪽:-1, 오른쪽:1)
        dirX = 0;
        // 전진 후진 이동 방향(후진:-1, 전진:1)
        dirZ = 0;

        // 터치패드 클릭 했을 경우...
        if(OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            // 터치패드의 눌린 축을 가져온다
            Vector2 coord = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad,
                                        OVRInput.Controller.RTrackedRemote);
            // 절대값 구하고...
            var absX = Mathf.Abs(coord.x);
            var absY = Mathf.Abs(coord.y);

            // 좌우 이동과 전진/후진 이동을 따로 처리했음...
            // 그렇기 때문에 절대값 비교를 통하여 더 큰 값을 가진 축으로 이동
            if(absX > absY)
            {
                // Right
                if (coord.x > 0)
                    dirX = +1;
                // Left
                else
                    dirX = -1;
            }
            else
            {
                // Up
                if (coord.y > 0)
                    dirZ = +1;
                // Down
                else
                    dirZ = -1;
            }
        }

        // 이동 방향 설정 후 이동
        Vector3 moveDir = new Vector3(dirX * speedSide, 0, dirZ * speedForward);
        transform.Translate(moveDir * Time.smoothDeltaTime);

        /*
            Time.smoothDeltaTime은 Time.deltatime( Time.deltatime = 1초 / 현재프레임)의 중간에 한번씩 값이 뻥튀기
            되는 현상이 있는 문제점을 보완한 방식이다. Time.deltatime 을 사용하여 개발을 진행하다가...이 현상으로
            인해 캐릭터 이동시 약간씩 튄다거나...회전할때 약간 부자연스럽다던가 하는 현상이 발생한다면 이를 발전/보완
            시킨 형태의 Time.smoothDeltaTime 를 사용하자.
         */

    }
}
