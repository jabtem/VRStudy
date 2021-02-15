using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesMaker : MonoBehaviour
{

    private float spawn_time_ = 0.0f;

    public GameObject prefab_plane_;

    public int max_planes_count_ = 5;
    public int planes_count_ = 0;


    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#elif UNITY_IOS
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
        prefab_plane_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        spawn_time_ += Time.deltaTime;

        if(spawn_time_ > 1.0f && max_planes_count_ >= planes_count_)
        {
            spawn_time_ = 0.0f;

            GameObject obj_plane = GameObject.Instantiate(prefab_plane_, this.transform) as GameObject;
            obj_plane.SetActive(true);
            obj_plane.transform.localPosition = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.5f, 3.0f), 30.0f);

            planes_count_++;
            
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child_plane = transform.GetChild(i);
            child_plane.Translate(new Vector3(0, 0, 0.1f));

            // 비행기가 카메라 뒤에 있는 위치까지 이동하는 경우, 해당 비행기의 좌표를
            // 초기화하는 코드 형식 (비행기를 재활용한 애플리케이션을 위함)
            // 이제 계속 실행해도 비행기가 느려지는 문제는 없을 것이다.
            if( child_plane.localPosition.z < -5.0f )
            {
                // 위치 초기화
                child_plane.transform.localPosition = new Vector3(Random.Range(-2.0f, 2.0f),
                    Random.Range(0.5f, 3.0f), 30.0f);
            }
        }
    }
}
