using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemAdd : MonoBehaviour
{
    Animator animator;
    public int ScoreSum;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            animator.SetTrigger("PickUp");
            Destroy(other.gameObject);
            Debug.Log(ScoreSum);
            ScoreText.text = "Score = " + ScoreSum;
            Debug.Log("아이템을 먹었습니다.");
            GameObject.Find("Spawn").GetComponent<MathSpawn>().mathproblem();
            GameObject.Find("UiManagement").GetComponent<UiManagement>().showCalculator();
        }
    }
    public void soresum(int score)
    {
        ScoreSum += score;
    }
    public void itemadd()
    {
        animator.SetTrigger("PickUp");
        Debug.Log(ScoreSum);
        ScoreText.text = "Score = " + ScoreSum;
        Debug.Log("아이템을 먹었습니다.");
        GameObject.Find("Spawn").GetComponent<MathSpawn>().mathproblem();
        GameObject.Find("UiManagement").GetComponent<UiManagement>().showCalculator();
    }

}
