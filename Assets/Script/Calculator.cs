using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Calculator : MonoBehaviour
{
    public InputField InputField;
    public Canvas canvas;
    public int number;
    int xnumber;
    public void Submit()
    {
        number = int.Parse(canvas.gameObject.transform.GetChild(3).GetChild(1).GetComponent<Text>().text);
        xnumber = GameObject.Find("Spawn").GetComponent<MathSpawn>().number();
        if (number == xnumber)
        {
            GameObject.Find("UiManagement").GetComponent<UiManagement>().removeCalculator();
            InputField.text = string.Empty;
            Debug.Log("정답입니다.");
        }
        else
        {
            InputField.text = string.Empty;
            Debug.Log("틀렸습니다.");
        }
    }
    public void Clear()
    {
        InputField.text= string.Empty;
    }
    public void btn0()
    {
        InputField.text += "0";
    }
    public void btn1()
    {
        InputField.text += "1";
    }
    public void btn2()
    {
        InputField.text += "2";
    }
    public void btn3()
    {
        InputField.text += "3";
    }
    public void btn4()
    {
        InputField.text += "4";
    }
    public void btn5()
    {
        InputField.text += "5";

    }
    public void btn6()
    {
        InputField.text += "6";
    }
    public void btn7()
    {
        InputField.text += "7";
    }
    public void btn8()
    {
        InputField.text += "8";
    }
    public void btn9()
    {
        InputField.text += "9";
    }
}
