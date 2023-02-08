using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    float rotaY;
    float rotaX;
    float x;
    float y;
    float clampX;
    float clampy;
    public GameObject cameraPose;
    // Start is called before the first frame update
    void Start()
    {
        //cameraPose = new GameObject.Find();//스트링 형태기 때문에 하이러키창에서 하나씩 비교하기 때문에 부하가 많기때문에 쓰지않는 경우가 좋다.
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            rotaY = cameraPose.transform.rotation.eulerAngles.y;//eulerAngles는 0~360도 사이의 값을 반환한다.
            rotaX = cameraPose.transform.rotation.eulerAngles.x;

            x = rotaX - mouseY;//Mathf.Clamp()사용전
            y = rotaY + mouseX;

            //Debug.Log(x);// 0을 넘어 음수를 넘어가면 359도가 된다.

            if (x > 180)
            {
                x = x - 360;
            }

            clampX = Mathf.Clamp(x, -40.0f, 40.0f);

            cameraPose.transform.rotation = Quaternion.Euler(clampX, y, 0);
           
        }
    }
}
