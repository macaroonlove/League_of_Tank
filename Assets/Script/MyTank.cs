using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTank : MonoBehaviour
{
    [Header("�����ɽ�Ʈ�� ���� ī�޶�")]
    public Camera Camera;

    [Header("�̵�")]
    public float move_speed; //�̵��ӵ�
    private Vector3 TankVec; //��ũ�� ��ġ
    private Quaternion newDirection; //��ũ�� ȸ��

    [Header("��ũ �Ӹ� ������")]
    public int rotTurretSpeed;
    public float rotTurret;
    public GameObject tank_head_Benchmark;

    [Header("��ũ �� ���Ʒ�")]
    public int updownGunSpeed;
    public float updownGun;
    public GameObject tank_gun_Benchmark;

    [Header("�⺻����")]
    public int shotPower;
    public GameObject bulletPrefab;
    public GameObject spPoint;
    public float DestroyTime;

    //��ų_���ӻ�� ����
    private bool IS_Q_Skill = true;
    private bool IS_W_Skill = true;
    private bool IS_E_Skill = true;
    private bool IS_R_Skill = true;

    //W���� �����ϱ� ���� static bool����
    static public bool Is_E_Skill_On = false;

    [Header("��ų ������Ʈ")]
    public GameObject Shield;
    public GameObject TurnGun;
    public GameObject nuclearPrefab;
    public GameObject nuPoint;

    [Header("�¸� ������Ʈ")]
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
        if (Input.GetMouseButtonDown(1)) //��Ŭ����
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //�̵� �� ���
                TankVec = hit.point;
                TankVec.y = transform.position.y;
                //ȸ�� �� ���
                Vector3 direction = new Vector3(hit.point.x - transform.position.x, 0f, hit.point.z - transform.position.z);
                newDirection = Quaternion.LookRotation(direction);
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * 10.0f); //ȸ��
        transform.position = Vector3.MoveTowards(transform.position, TankVec, move_speed * Time.deltaTime); //�̵�
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation) as GameObject;    //��ź ����
            Rigidbody rigidBullet = bullet.GetComponent<Rigidbody>();                                       //������ ��ź�� ����ȿ���� �� �� �ְ� �ʱ�ȭ
            rigidBullet.AddForce(tank_head_Benchmark.transform.forward * shotPower);                        //tank_head�� �����ִ� �������� ��ź�� ���� �ֱ�
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
    //Q��ų
    void Q_Skill_Fin()
    {
        move_speed = 5.0f;
        Invoke("Q_Skill_Delay", 2.0f);
    }
    void Q_Skill_Delay()
    {
        IS_Q_Skill = true;
    }
    //W��ų
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
    //E��ų
    void E_Skill_Fin()
    {
        TurnGun.SetActive(false);
        Invoke("E_Skill_Delay", 13.0f);
    }
    void E_Skill_Delay()
    {
        IS_E_Skill = true;
    }

    //R��ų
    void R_Skill_Fin()
    {
        IS_R_Skill = true;
    }

    void Head_Turn()
    {
        //�ͷ� ȸ��
        rotTurret = Input.GetAxis("rotTurret");
        tank_head_Benchmark.transform.Rotate(Vector3.up * rotTurret * rotTurretSpeed * Time.deltaTime);
        //�� ���� ����
        updownGun = Input.GetAxis("Mouse ScrollWheel");
        tank_gun_Benchmark.transform.Rotate(Vector3.right * updownGun * updownGunSpeed * Time.deltaTime);
        //�� ���� ���� ����
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