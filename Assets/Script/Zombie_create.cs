using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_create : MonoBehaviour
{
    public GameObject Zombie_Prefab;
    public GameObject Target;

    float currTime;

    public GameObject Menu_UI;

    void Start()
    {
    }

    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime > 0.5)
        {
            float new1X = Random.Range(-90f, 90f), new1Z = Random.Range(-90f, 90f);
            float new2X = Random.Range(-90f, 90f), new2Z = Random.Range(-90f, 90f);
            Vector3 new1 = new Vector3(new1X, 13.0f, new1Z);
            Vector3 new2 = new Vector3(new2X, 13.0f, new2Z);
            if(new1 != Target.transform.position)
            {
                GameObject Zombie = Instantiate(Zombie_Prefab);
                Zombie.transform.position = new1;
            }
            if(new2 != Target.transform.position)
            {
                GameObject Zombie2 = Instantiate(Zombie_Prefab);
                Zombie2.transform.position = new2;
            }

            currTime = 0;
        }
        //°©ºÐ½Î_¼³Á¤Ã¢_ÄÑ±â
        if (Input.GetButtonDown("Cancel"))
        {
            if (Menu_UI.activeSelf)
            {
                Time.timeScale = 1;
                Menu_UI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                Menu_UI.SetActive(true);
            }
        }
    }
}
