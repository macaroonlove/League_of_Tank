using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTank : MonoBehaviour
{
    [Header("레이케스트를 위한 카메라")]
    public Camera Camera;

    [Header("이동")]
    public float move_speed; //이동속도
    private Vector3 TankVec; //탱크의 위치
    private Quaternion newDirection; //탱크의 회전

    [Header("탱크 머리 돌리기")]
    public int rotTurretSpeed;
    public float rotTurret;
    public GameObject tank_head_Benchmark;

    [Header("탱크 포 위아래")]
    public int updownGunSpeed;
    public float updownGun;
    public GameObject tank_gun_Benchmark;

    [Header("기본공격")]
    public int shotPower;
    public GameObject bulletPrefab;
    public GameObject spPoint;
    public float DestroyTime;

    //스킬_연속사용 방지
    private bool IS_Q_Skill = true;
    private bool IS_W_Skill = true;
    private bool IS_E_Skill = true;
    private bool IS_R_Skill = true;

    //W무적 구현하기 위한 static bool변수
    static public bool Is_E_Skill_On = false;

    [Header("스킬 오브젝트")]
    public GameObject Shield;
    public GameObject TurnGun;
    public GameObject nuclearPrefab;
    public GameObject nuPoint;

    [Header("승리 오브젝트")]
    public GameObject Win;

    void Start()
    {
        TankVec = transform.position;
        move_speed = 5.0f;
        rotTurretSpeed = 80;
        updownGunSpeed = 10000;
        shotPower = 600;
        DestroyTime = 3.0f;

        Player_UI.HP = 5;
    }

    void Update()
    {
        Move();
        Attack();
        Skill();
        Head_Turn();
    }

    void Move()
    {
        if (Input.GetMouseButtonDown(1)) //우클릭시
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //이동 값 계산
                TankVec = hit.point;
                TankVec.y = transform.position.y;
                //회전 값 계산
                Vector3 direction = new Vector3(hit.point.x - transform.position.x, 0f, hit.point.z - transform.position.z);
                newDirection = Quaternion.LookRotation(direction);
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * 10.0f); //회전
        transform.position = Vector3.MoveTowards(transform.position, TankVec, move_speed * Time.deltaTime); //이동
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation) as GameObject;    //포탄 생성
            Rigidbody rigidBullet = bullet.GetComponent<Rigidbody>();                                       //생성한 포탄에 물리효과를 줄 수 있게 초기화
            rigidBullet.AddForce(tank_head_Benchmark.transform.forward * shotPower);                        //tank_head가 보고있는 방향으로 포탄에 힘을 주기
            Destroy(bullet, DestroyTime);
        }
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IS_Q_Skill)
        {
            move_speed = 10.0f;
            Player_UI.Q_Press = true;
            IS_Q_Skill = false;
            Invoke("Q_Skill_Fin", 3.0f);
        }
        if (Input.GetKeyDown(KeyCode.W) && IS_W_Skill)
        {
            Shield.SetActive(true);
            Is_E_Skill_On = true;
            Player_UI.W_Press = true;
            IS_W_Skill = false;
            Invoke("W_Skill_Fin", 4.0f);
        }
        if (Input.GetKeyDown(KeyCode.E) && IS_E_Skill)
        {
            TurnGun.SetActive(true);
            Player_UI.E_Press = true;
            IS_E_Skill = false;
            Invoke("E_Skill_Fin", 7.0f);
        }
        if (Input.GetKeyDown(KeyCode.R) && IS_R_Skill)
        {
            Instantiate(nuclearPrefab, nuPoint.transform.position, nuPoint.transform.rotation);
            Player_UI.R_Press = true;
            IS_R_Skill = false;
            Invoke("R_Skill_Fin", 40.0f);
        }
    }
    //Q스킬
    void Q_Skill_Fin()
    {
        move_speed = 5.0f;
        Invoke("Q_Skill_Delay", 2.0f);
    }
    void Q_Skill_Delay()
    {
        IS_Q_Skill = true;
    }
    //W스킬
    void W_Skill_Fin()
    {
        Shield.SetActive(false);
        Is_E_Skill_On = false;
        Invoke("W_Skill_Delay", 3.0f);
    }
    void W_Skill_Delay()
    {
        IS_W_Skill = true;
    }
    //E스킬
    void E_Skill_Fin()
    {
        TurnGun.SetActive(false);
        Invoke("E_Skill_Delay", 13.0f);
    }
    void E_Skill_Delay()
    {
        IS_E_Skill = true;
    }

    //R스킬
    void R_Skill_Fin()
    {
        IS_R_Skill = true;
    }

    void Head_Turn()
    {
        //터렛 회전
        rotTurret = Input.GetAxis("rotTurret");
        tank_head_Benchmark.transform.Rotate(Vector3.up * rotTurret * rotTurretSpeed * Time.deltaTime);
        //포 상하 조절
        updownGun = Input.GetAxis("Mouse ScrollWheel");
        tank_gun_Benchmark.transform.Rotate(Vector3.right * updownGun * updownGunSpeed * Time.deltaTime);
        //포 상하 조절 제한
        Vector3 ang = tank_gun_Benchmark.transform.eulerAngles;
        if (ang.x > 180)
            ang.x -= 360;
        ang.x = Mathf.Clamp(ang.x, -95.0f, -75.0f);
        tank_gun_Benchmark.transform.eulerAngles = ang;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Win")
        {
            gameObject.GetComponent<MyTank>().enabled = false;
            Win.SetActive(true);
        }
    }
}