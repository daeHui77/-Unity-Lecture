using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiScript : MonoBehaviour
{
    public Transform targetTransform1;//필요한 부분만 가져오면 된다.
    public Transform targetTransform2;
    public Transform targetTransform3;
    NavMeshAgent agent;
    
    int count;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    void Update()
    {
        //이동 회전 충돌 모두를 가지고 있는 함수이다(정의는 Pro버전을 결제하면 볼 수있다.)
        movefuc();
    }
    void movefuc()
    {
        agent.SetDestination(targetTransform1.position);

        if(Vector3.Distance(transform.position, targetTransform1.position) <= 1)//조건을 거리로 하면 계속 반복하면서 돌아간다
        {
            count = 1;
        }
        if(count == 1)
        {
            agent.SetDestination(targetTransform2.position);
        }

        if (Vector3.Distance(transform.position, targetTransform2.position) <= 1)
        {
            count = 2;
        }
        if (count == 2)
        {
            agent.SetDestination(targetTransform3.position);
        }

        if (Vector3.Distance(transform.position, targetTransform3.position) <= 1)
        {
            count = 3;
        }
        if (count == 3)
        {
            agent.SetDestination(targetTransform1.position);
        }

    }
}
