using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuClear : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            GameObject Explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
            Destroy(Explosion, 2.0f);
        }
    }
}
