using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shotpower;   //�� �߻� �ӵ�
    //public GameObject exp;

    void Start()
    {
        shotpower = 1000;
        GetComponent<Rigidbody>().AddForce(-transform.right * shotpower);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
