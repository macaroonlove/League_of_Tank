using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_door : MonoBehaviour
{
    public GameObject Ldoor;
    public GameObject Rdoor;

    private bool Is_open_door = false;

    void Update()
    {
        if(Player_UI.KZombie_num >= 10)
        {
            if(Is_open_door == false)
            {
                Is_open_door = true;
                Ldoor.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
                Rdoor.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            }
        }
    }
}
