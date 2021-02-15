using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBtn : MonoBehaviour
{

    public UnityEngine.UI.Scrollbar obj_scrollbar_;

    /*
        시점이 스크롤바에 들어갈 때 처리해준 OnPointEnter 이벤트에서 
        TimeToAction 코루틴을 실행
    */

    public void OnPointEnter()
    {
        StartCoroutine(TimeToAction());
    }

    /*
        시점이 버튼에서 벗어날 때 OnPointExit 이벤트는 버튼 안에 들어가면 돌아가는 
        코루틴 TimeToAction 메서드를 StopAllCoroutines 메서드로 멈추고,
        Scrollbar의 게이지를 0으로 설정해 시점이 벗어난 처리를 해줌.
    */

    public void OnPointExit()
    {
        obj_scrollbar_.size = 0;
        StopAllCoroutines();
    }

    /*
        0.0부터 1.0까지 0.01씩 값을 올리면서 화면을 갱신하고 1.0이 된 후에는 
        "버튼 동작 처리"라는 로그를 출력. 만약 게임의 시작이나 장면의 이동과
        같이 어떤 동작을 위해 만들었다면 실제로 로그를 찍는 이 함수 대신 해당
        기능 코드를 작성해 넣으면 됨
    */

    IEnumerator TimeToAction()
    {
        for (float value = 0.0f; value < 1.0f; value += 0.01f)
        {
            obj_scrollbar_.size = value;
            yield return new WaitForEndOfFrame();
        }
        obj_scrollbar_.size = 1.0f;
        Debug.Log("버튼 동작 처리");
    }

}
