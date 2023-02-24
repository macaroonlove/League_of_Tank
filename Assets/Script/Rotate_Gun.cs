using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Gun: MonoBehaviour
{
    public Transform target;
    public float RS_Speed;

    void Start()
    {
        RS_Speed = 400;
    }

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, RS_Speed * Time.deltaTime);
    }
}
