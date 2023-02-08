using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class MoveScript : MonoBehaviour
{

    Rigidbody rigid; //선언
    float PlayerHP;
    float H = 0.0f;
    float V = 0.0f;
    //Horizontal = 수평 -1 ~ 1
    //Verticla = 수직 -1 ~ 1
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
        rigid = GetComponent<Rigidbody>();//초기화 
        animator = GetComponent<Animator>();//초기화
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
                //이동
                targetPoint = hit.point;

                Debug.Log(hit.transform.name);
                //회전
                rocationtarget = Quaternion.LookRotation(targetPoint - transform.position);//위치를 회전값으로 바꿔줌
                //rocationtarget = hit.transform.rotation;
            }
        }

        //Debug.Log(transform.rotation.eulerAngles);
        //이동
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime);
        //회전
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

        //position을 이용하여 world좌표기준으로 움직이는 코드
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //transform.position == 이 지금 vector3의 값이에요
        //    transform.position += Vector3.forward * Time.deltaTime;

        //    //컴퓨터마다 성능이 달라 결과에 영향을 주면 안되기 떄문에 이 시간을 곱해주면 컴퓨터에 상관없이 프레임 값이 1이 나온다.
        //    //로컬 피시에서 돌렸을때 프레임당 걸리는 시간 = Time.deltaTime
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
        //    transform.Translate(Vector3.down , Space.Self);//self = local 기준(생략 가능), self = world 기준(생략 불가)
        //}
        ////transform.Translate()
        ////vecter3.forward = new vecter3(0,0,1)

        ////Translate를 이용하여 로컬 좌표기준으로 이동하는 코드
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

        ////Rigidbody 이용하여 이동
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rigid.AddForce(Vector3.forward * 10);//벡터 값을 가지는 힘 변수를 넣어줘라
        //}


        //transform.rotation = Quaternion.Euler(rotaX - mouseY, rotaY + mouseX, transform.rotation.eulerAngles.z);


        //Debug.Log("HP =" + PlayerHP);
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

   

   
    //오디오를 이용해 걷는 소리

    public void EventWalkCall()
    {
        Debug.Log("걷기 소리");
        audioSource.Play();
    }
   
    void MoveFuc()
    {
        //idle상태
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("backWalk", false);
        animator.SetBool("backRun", false);

        //rotaY = transform.rotation.eulerAngles.y;
        rota = rotaY;
        H = Input.GetAxisRaw("Horizontal");

        //앞으로
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("걷기");
            animator.SetBool("Walk",true);
            animator.SetBool("Run", false);
            transform.Translate(Vector3.forward * Time.deltaTime);
            
            //걷는 싸운드 
            //if(!audioSource.isPlaying)//오디오 소스가 실행중이라면
            //{
            //    audioSource.Play();
            //}

            ////걷는 싸운드 
            //if(!audioSource.isPlaying)//오디오 소스가 실행중이라면
            //{
            //    EventWalkCall();
            //}
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("뛰기");
            animator.SetBool("Walk", true);
            animator.SetBool("Run", true);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        //뒤로
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("뒤로 걷기");
            animator.SetBool("backWalk", true);
            animator.SetBool("backRun", false);
            transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("뒤로 뛰기");
            animator.SetBool("backWalk", true);
            animator.SetBool("backRun", true);
            transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("손 흔들기");
            animator.SetTrigger("Wave");
            GameObject.Find("Spawn").GetComponent<ItemSpawn>().EventWaveitem();
        }

        ////회전
        //if (H != 0)
        //{
        //    Debug.Log("방향");
        //    animator.SetBool("Walk", true);
        //    rota += H;
        //    transform.rotation = Quaternion.Euler(0.0f, rota, 0.0f);
        //}
        //손 흔들기






        //rigid.AddForce()
        //rigid.AddRelativeForce(Vector3(0))

        //H = Input.GetAxisRaw("Horizontal")
        //V = Input.GetAxisRaw("Vertical");
        ////대각선움직일때 속도가 올라가는 문제를 해결하기 위해 정류화를 진행
        ////대각선 값또한 1또는 -1로 만들어준다
        //Vector3 vector = new Vector3(H, 0.0f, V).normalized;
        //transform.Translate(vector*speed * Time.deltaTime);

        ////쿼터니언을 사용한 이동방법
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

        ////Translate를 이용하여 로컬 좌표기준으로 이동하는 코드
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
    //private void OnCollisionEnter(Collision collision)//처음 닿았을 때만 만남
    //{
    //    Debug.Log("처음 충돌 하였습니다.");
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("마지막 충돌 하였습니다.");
    //}
    //private void OnCollisionStay(Collision collision)//부하가 심하기 때문에 sleep모드로 들어간다.
    //{
    //    Debug.Log("충돌 중");
    //}
    //private void OnTriggerEnter(Collider other)
    //{

    //}
    //private void OnTriggerExit(Collider other)
    //{

    //}
    //private void OnTriggerStay(Collider other)//닿은 대상의 정보
    //{
    //    if(other.CompareTag("damage"))
    //    {
    //        if(PlayerHP <= 0.0f )
    //        {
    //            Debug.Log("사망하셨습니다");
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
    //            Debug.Log("최대 체력입니다.");
    //        }
    //        else
    //        {
    //            PlayerHP += 1.0f;
    //        }

    //    }
    //    //other.name //Trigger에 닿은 오브젝트의 정보(이름,tag,layer,transform)
    //}

}
