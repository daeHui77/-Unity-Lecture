using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MathSpawn : MonoBehaviour
{
    int number1;
    int number2;
    public int numbersum;
    public Text problem;
    public void mathproblem()
    {
        number1 = Random.Range(0, 100);
        number2 = Random.Range(0, 100);
        numbersum = number1 + number2;
        problem.text = number1 + " + " + number2 + " = ";
    }
    public int number()
    {
        return numbersum;
    }
}
