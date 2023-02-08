using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class MoveScript : MonoBehaviour
{

    Rigidbody rigid; //����
    float PlayerHP;
    float H = 0.0f;
    float V = 0.0f;
    //Horizontal = ���� -1 ~ 1
    //Verticla = ���� -1 ~ 1
    float speed;
    float rotaY;
    float rotaX;
    float rota;
    Animator animator;
    float mouseY;
    float mouseX;

    int rangeX;
    int rangeZ;
    AudioSource audioSource;
    public AudioClip[] walkSound;
    public GameObject item;

    public int spawnCount;
    public int cubecount;

    public Text ScoreText;
    public int ScoreSum;

    GameObject ScoreTextUI;
    GameObject ButtonUI;

    public Canvas canvas;
    int cubescore;
    RaycastHit hit;
    Ray ray;
    public LayerMask layer;
    NavMeshAgent agent;

    Vector3 targetPoint;
    Quaternion rocationtarget;
    public GameObject target;
   
    void Start()
    {
        speed = 5f;
        PlayerHP = 100.0f;
        rigid = GetComponent<Rigidbody>();//�ʱ�ȭ 
        animator = GetComponent<Animator>();//�ʱ�ȭ
        audioSource = GetComponent<AudioSource>();
        ScoreText.text = "Score = 0";
        agent = GetComponent<NavMeshAgent>();
        targetPoint = transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        rotaY = transform.rotation.eulerAngles.y;
        rotaX = transform.rotation.eulerAngles.x;

        
        if(Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            

            if(Physics.Raycast(ray, out hit, float.MaxValue , layer))
            {
                //�̵�
                targetPoint = hit.point;

                Debug.Log(hit.transform.name);
                //ȸ��
                rocationtarget = Quaternion.LookRotation(targetPoint - transform.position);//��ġ�� ȸ�������� �ٲ���
                //rocationtarget = hit.transform.rotation;
            }
        }

        //Debug.Log(transform.rotation.eulerAngles);
        //�̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime);
        //ȸ��
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rocationtarget, Time.deltaTime * 360);

        //this.transform.LookAt(gameObject.transform);
        

        if (GameObject.Find("UiManagement").GetComponent<UiManagement>().behaviorcontrol() == 0)
        {
            MoveFuc();
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("backWalk", false);
            animator.SetBool("backRun", false);
        }
        
        if(Physics.Raycast(transform.position,transform.forward, out hit))
        {
            
            Debug.DrawRay(transform.position, transform.forward, Color.red);

            if(hit.distance < 1 && hit.transform.CompareTag("item"))
            {
                cubescore = hit.transform.GetComponent<Item>().cubeScore;

                GetComponent<ItemAdd>().soresum(cubescore);

                GetComponent<ItemAdd>().itemadd();

                Destroy(hit.transform.gameObject);
            }
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    transform.rotation = Quaternion.Euler(0, -90, 0);
        //}

        //position�� �̿��Ͽ� world��ǥ�������� �����̴� �ڵ�
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //transform.position == �� ���� vector3�� ���̿���
        //    transform.position += Vector3.forward * Time.deltaTime;

        //    //��ǻ�͸��� ������ �޶� ����� ������ �ָ� �ȵǱ� ������ �� �ð��� �����ָ� ��ǻ�Ϳ� ������� ������ ���� 1�� ���´�.
        //    //���� �ǽÿ��� �������� �����Ӵ� �ɸ��� �ð� = Time.deltaTime
        //    //Time.deltaTime
        //}
        //if (Input.GetKey(KeyCode.S))
        //{   
        //    transform.position += Vector3.back * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -= new Vector3(0.05f, 0.0f, 0.0f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += new Vector3(0.05f, 0.0f, 0.0f);
        //}
        //if(Input.GetKey(KeyCode.Q))
        //{
        //    transform.position += Vector3.up * Time.deltaTime;
        //}
        //if(Input.GetKey(KeyCode.E))
        //{
        //    transform.Translate(Vector3.down , Space.Self);//self = local ����(���� ����), self = world ����(���� �Ұ�)
        //}
        ////transform.Translate()
        ////vecter3.forward = new vecter3(0,0,1)

        ////Translate�� �̿��Ͽ� ���� ��ǥ�������� �̵��ϴ� �ڵ�
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}
        //if(Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(Vector3.back * Time.deltaTime);
        //}
        //if(Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector3.left * Time.deltaTime);
        //}
        //if(Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * Time.deltaTime);
        //}

        ////Rigidbody �̿��Ͽ� �̵�
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rigid.AddForce(Vector3.forward * 10);//���� ���� ������ �� ������ �־����
        //}


        //transform.rotation = Quaternion.Euler(rotaX - mouseY, rotaY + mouseX, transform.rotation.eulerAngles.z);


        //Debug.Log("HP =" + PlayerHP);
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

   

   
    //������� �̿��� �ȴ� �Ҹ�

    public void EventWalkCall()
    {
        Debug.Log("�ȱ� �Ҹ�");
        audioSource.Play();
    }
   
    void MoveFuc()
    {
        //idle����
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("backWalk", false);
        animator.SetBool("backRun", false);

        //rotaY = transform.rotation.eulerAngles.y;
        rota = rotaY;
        H = Input.GetAxisRaw("Horizontal");

        //������
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("�ȱ�");
            animator.SetBool("Walk",true);
            animator.SetBool("Run", false);
            transform.Translate(Vector3.forward * Time.deltaTime);
            
            //�ȴ� �ο�� 
            //if(!audioSource.isPlaying)//����� �ҽ��� �������̶��
            //{
            //    audioSource.Play();
            //}

            ////�ȴ� �ο�� 
            //if(!audioSource.isPlaying)//����� �ҽ��� �������̶��
            //{
            //    EventWalkCall();
            //}
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("�ٱ�");
            animator.SetBool("Walk", true);
            animator.SetBool("Run", true);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        //�ڷ�
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("�ڷ� �ȱ�");
            animator.SetBool("backWalk", true);
            animator.SetBool("backRun", false);
            transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("�ڷ� �ٱ�");
            animator.SetBool("backWalk", true);
            animator.SetBool("backRun", true);
            transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("�� ����");
            animator.SetTrigger("Wave");
            GameObject.Find("Spawn").GetComponent<ItemSpawn>().EventWaveitem();
        }

        ////ȸ��
        //if (H != 0)
        //{
        //    Debug.Log("����");
        //    animator.SetBool("Walk", true);
        //    rota += H;
        //    transform.rotation = Quaternion.Euler(0.0f, rota, 0.0f);
        //}
        //�� ����






        //rigid.AddForce()
        //rigid.AddRelativeForce(Vector3(0))

        //H = Input.GetAxisRaw("Horizontal")
        //V = Input.GetAxisRaw("Vertical");
        ////�밢�������϶� �ӵ��� �ö󰡴� ������ �ذ��ϱ� ���� ����ȭ�� ����
        ////�밢�� ������ 1�Ǵ� -1�� ������ش�
        //Vector3 vector = new Vector3(H, 0.0f, V).normalized;
        //transform.Translate(vector*speed * Time.deltaTime);

        ////���ʹϾ��� ����� �̵����
        //if(Input.GetKey(KeyCode.W))
        //{
        //    rota = 0.0f;
        //    transform.rotation = Quaternion.Euler(0, rota, 0);
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rota = -90.0f; 
        //    transform.rotation = Quaternion.Euler(0, rota, 0);
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rota = 90.0f;
        //    transform.rotation = Quaternion.Euler(0, rota, 0);
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    rota = -180.0f;
        //    transform.rotation = Quaternion.Euler(0, rota, 0);
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}

        ////Translate�� �̿��Ͽ� ���� ��ǥ�������� �̵��ϴ� �ڵ�
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(Vector3.back * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector3.left * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * Time.deltaTime);
        //}

    }
    //private void OnCollisionEnter(Collision collision)//ó�� ����� ���� ����
    //{
    //    Debug.Log("ó�� �浹 �Ͽ����ϴ�.");
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("������ �浹 �Ͽ����ϴ�.");
    //}
    //private void OnCollisionStay(Collision collision)//���ϰ� ���ϱ� ������ sleep���� ����.
    //{
    //    Debug.Log("�浹 ��");
    //}
    //private void OnTriggerEnter(Collider other)
    //{

    //}
    //private void OnTriggerExit(Collider other)
    //{

    //}
    //private void OnTriggerStay(Collider other)//���� ����� ����
    //{
    //    if(other.CompareTag("damage"))
    //    {
    //        if(PlayerHP <= 0.0f )
    //        {
    //            Debug.Log("����ϼ̽��ϴ�");
    //        }
    //        else
    //        {
    //            PlayerHP -= 1.0f;
    //        }

    //    }
    //    else if (other.tag == "healing")
    //    {
    //        if (PlayerHP >= 100.0f)
    //        {
    //            Debug.Log("�ִ� ü���Դϴ�.");
    //        }
    //        else
    //        {
    //            PlayerHP += 1.0f;
    //        }

    //    }
    //    //other.name //Trigger�� ���� ������Ʈ�� ����(�̸�,tag,layer,transform)
    //}

}
