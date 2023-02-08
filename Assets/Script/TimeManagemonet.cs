using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TimeManagemonet : MonoBehaviour
{
    public Text text;
    public float limitTime;
    // Update is called once per frame
    void Update()
    {
        if(limitTime > 0.0f)
        {
            limitTime -= Time.deltaTime;
        }
        else if(limitTime <= 0.0f)
        {
            //어플리케이션 종료
            Application.Quit();

            //editer상태에서 종료
            //ExitPlaymode;
            EditorApplication.isPlaying = false;
        }
        text.text = limitTime.ToString("F2");
    }
    
}
