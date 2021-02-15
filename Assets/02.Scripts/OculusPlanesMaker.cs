using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusPlanesMaker : MonoBehaviour
{

    private float spawn_time_ = 0.0f;

    public GameObject prefab_plane_;

    public int max_planes_count_ = 5;
    public int planes_count_ = 0;

    // 오큘러스 버전(미션)
    public bool is_update_ = true;
    // 오큘러스 버전(미션)
    public TextMesh obj_swipe_text_;
    // 오큘러스 버전(미션)
    private float speed;

    private void Start()
    {

        /*
            전처리기인 '#if #elif #endif'를 통해 안드로이드, 아이폰으로 각각 빌드할 때의
            코드를 구분해 각 플랫폼 코드를 컴파일 하자.
         */

#if UNITY_ANDROID
        // 스마트폰의 화면이 절전 모드가 되거나 꺼지지 않음
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#elif UNITY_IOS
        // iPhoneSettings.screenCanDarken = false; 사라짐
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

        // 베이스 게임오브젝트 비활성화
        prefab_plane_.SetActive(false);

        // 오큘러스 버전(미션)
        speed = 0.1f;

        // 오큘러스 버전 (구 버전 이젠 안됨...)
        //OVRTouchpad.Create();
        //OVRTouchpad.TouchHandler += HandleTouchHandler; ()
    }

    // 오큘러스 버전 (구 버전 이젠 안됨...)
    //void HandleTouchHandler(object sender, System.EventArgs e)
    //{
    //    OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
    //    if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
    //    {
    //        is_update_ = !is_update_;
    //    }
    //    else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Up)
    //    {
    //        obj_swipe_text_.text = "  비행기수 증가!!";
    //        max_planes_count_++;
    //    }
    //    else
    //    {
    //        obj_swipe_text_.text = touchArgs.TouchType.ToString();
    //    }

    //}

    // Update is called once per frame
    void Update()
    {

        // 오큘러스 버전(미션)
        if (!is_update_) return;

        // Spawn Planes
        spawn_time_ += Time.deltaTime;

        // 최대 비행기 개수를 지정하고 그 이상은 새로 생성되지 못하게 하는 코드 형식
        if (spawn_time_ > 1.0f && max_planes_count_ >= planes_count_)
        {
            spawn_time_ = 0.0f;
            Debug.Log("비행기 출발~!");

            // 비행기 생성
            GameObject obj_plane = GameObject.Instantiate(prefab_plane_, this.transform) as GameObject;
            // 생성과 동시에 게임오브젝트 활성화 및 위치 초기화
            obj_plane.SetActive(true);
            obj_plane.transform.localPosition = new Vector3(Random.Range(-2.0f, 2.0f),
                Random.Range(0.5f, 3.0f), 30.0f);

            // 비행기 생성수 제한
            planes_count_++;

            /*
                저사양 스마트폰에서 비행기가 계속 날아오는 것을 보다 보면
                서서히 느려지는 걸 확인할 수 있다. 이것은 계속해서 비행기들이
                생성되고 그것들이 메모리에 쌓여서 그런 것이다. 

                카메라 뒤로 비행기가 일정 간격으로 지난 경우 메모리에서 삭제할 수 있지만,
                이보다 다시 재활용해서 사용하는 방법을 선호해 본문과 같이 코드를 작성!!!
                메모리 풀 공부~!!!(비행기 게임 pdf 파일에 있음~)
            */
            // 밑에 방법도 있지만...게임 오브젝트의 생성과 소멸은 부담스러운 작업이다.
            // 3 초뒤 삭제
            //Destroy(obj_plane, 3.0f);
        }

        /*
  
            다음 코드에서 for 문은 해당 트리구조 0부터 자신의 하위 오브젝트 수만큼
            반복해서 해당 하위 오브젝트들의 Z 좌표를 카메라 앞쪽으로 이동시킨다.

        */

        // Move Planes
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child_plane = transform.GetChild(i);
            child_plane.Translate(new Vector3(0, 0, speed));

            // 비행기가 카메라 뒤에 있는 위치까지 이동하는 경우, 해당 비행기의 좌표를
            // 초기화하는 코드 형식 (비행기를 재활용한 애플리케이션을 위함)
            // 이제 계속 실행해도 비행기가 느려지는 문제는 없을 것이다.
            if (child_plane.localPosition.z < -5.0f)
            {
                // 위치 초기화
                child_plane.transform.localPosition = new Vector3(Random.Range(-2.0f, 2.0f),
                    Random.Range(0.5f, 3.0f), 30.0f);
            }
        }
    }

    // 오큘러스 버전(미션)
    public void StopPlayPlanes()
    {
        is_update_ = !is_update_;
    }

    // 오큘러스 버전(미션)
    public IEnumerator PlusPlane()
    {
        obj_swipe_text_.text = " 비행기수 증가";
        max_planes_count_++;

        yield return new WaitForSeconds(2.0f);

        obj_swipe_text_.text = "";
    }

    // 오큘러스 버전(미션)
    public void SpeedUp()
    {
        speed += 0.1f;
    }
}

/*
    Time.deltaTime 값은 이전 메서드를 호출했을 때와 현재 프레임 간의 지연된 시간을
    나타낸다. 이를 통해 우리는 spawn_time_ 변수에 시간이 흐르는 것을 저장할 수 있다.
    if 문을 통해 spawn_time_ 의 값이 1.0f, 즉 1초 이상인 경우 다시 spawn_time_ 값을
    초기화하고 Debug.Log("비행기 출발~!"); 이 코드로 콘솔에 특정 메시지를 찍어보면
    콘솔창에 1초 간격으로 비행기 출발~! 라는 로그가 찍힌다.
    (테스트 해보자: 정확한 시간 테스트)
 */

/*
    우리가 방금 이론을 배우고 실습한 것에 헤드 트래킹(Head Tracking) 기술이 접목되면 
    완벽히 HMD 원리를 습득하는 것이다.
    다음 쳅터 : 헤드 트래킹을 적용
*/
