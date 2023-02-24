using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent nvAgent;    
    private bool Is_hit;

    void Start()
    {
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        Is_hit = false;
    }

    void Update()
    {
        GameObject target = GameObject.FindWithTag("Player");
        nvAgent.destination = target.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            if(Is_hit == false)
            {
                Is_hit = true;
                Player_UI.KZombie_num++;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            if (MyTank.Is_E_Skill_On == false)
            {
                if(Player_UI.HP != 0)
                {
                    Player_UI.HP--;
                    Player_UI.HP_Is_Change = true;
                }
            }
        }
    }
}
