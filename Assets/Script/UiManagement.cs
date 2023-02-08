using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UiManagement : MonoBehaviour
{
    public GameObject canvas;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        canvas.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        canvas.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        count = 0;
    }
    public void showCalculator()
    {
        canvas.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        canvas.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        canvas.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        count = 1;
    }
    public void removeCalculator()
    {
        canvas.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        canvas.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        canvas.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        count = 0;
     
    }
    public int behaviorcontrol()
    {
        return count;
    }

}
