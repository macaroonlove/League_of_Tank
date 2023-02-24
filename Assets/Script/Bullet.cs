using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shotpower;   //포 발사 속도
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
