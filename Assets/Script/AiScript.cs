using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiScript : MonoBehaviour
{
    public Transform targetTransform1;//�ʿ��� �κи� �������� �ȴ�.
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
        //�̵� ȸ�� �浹 ��θ� ������ �ִ� �Լ��̴�(���Ǵ� Pro������ �����ϸ� �� ���ִ�.)
        movefuc();
    }
    void movefuc()
    {
        agent.SetDestination(targetTransform1.position);

        if(Vector3.Distance(transform.position, targetTransform1.position) <= 1)//������ �Ÿ��� �ϸ� ��� �ݺ��ϸ鼭 ���ư���
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
