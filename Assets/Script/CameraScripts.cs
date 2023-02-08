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
        //cameraPose = new GameObject.Find();//��Ʈ�� ���±� ������ ���̷�Űâ���� �ϳ��� ���ϱ� ������ ���ϰ� ���⶧���� �����ʴ� ��찡 ����.
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            rotaY = cameraPose.transform.rotation.eulerAngles.y;//eulerAngles�� 0~360�� ������ ���� ��ȯ�Ѵ�.
            rotaX = cameraPose.transform.rotation.eulerAngles.x;

            x = rotaX - mouseY;//Mathf.Clamp()�����
            y = rotaY + mouseX;

            //Debug.Log(x);// 0�� �Ѿ� ������ �Ѿ�� 359���� �ȴ�.

            if (x > 180)
            {
                x = x - 360;
            }

            clampX = Mathf.Clamp(x, -40.0f, 40.0f);

            cameraPose.transform.rotation = Quaternion.Euler(clampX, y, 0);
           
        }
    }
}
