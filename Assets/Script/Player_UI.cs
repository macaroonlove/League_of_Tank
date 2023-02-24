using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [Header("HP_bar")]
    public Slider HP_Bar;
    public Text HP_Text;
    static public float HP;
    static public bool HP_Is_Change = false;

    [Header("Q_Skill")]
    public Image Q_Skill_Image;
    public float Q_Delay = 5;
    public static bool Q_Press = false;

    [Header("W_Skill")]
    public Image W_Skill_Image;
    public float W_Delay = 7;
    public static bool W_Press = false;

    [Header("E_Skill")]
    public Image E_Skill_Image;
    public float E_Delay = 20;
    public static bool E_Press = false;

    [Header("R_Skill")]
    public Image R_Skill_Image;
    public float R_Delay = 40;
    public static bool R_Press = false;

    [Header("Kill_Zombie")]
    public Text KZombie_text;
    public Text Mari_text;
    public Text Kill_Success;
    static public int KZombie_num = 0;

    [Header("패배")]
    public GameObject Tank;
    public GameObject Lose;

    void Start()
    {
        Q_Skill_Image.fillAmount = 0;
        W_Skill_Image.fillAmount = 0;
        E_Skill_Image.fillAmount = 0;
        R_Skill_Image.fillAmount = 0;
    }

    void Update()
    {
        HP_UI();
        Q_Skill();
        W_Skill();
        E_Skill();
        R_Skill();
        KZ_UI();
    }

    void HP_UI()
    {
        if (HP_Is_Change)
        {
            HP_Bar.value = HP / 5;
            HP_Text.text = HP + "/5";
        }
        if (HP == 0)
        {
            Tank.GetComponent<MyTank>().enabled = false;
            Lose.SetActive(true);
        }
    }

    void Q_Skill()
    {
        if (Q_Press)
        {
            Q_Press = false;
            Q_Skill_Image.fillAmount = 1;
        }
        if(Q_Press == false)
        {
            Q_Skill_Image.fillAmount -= 1 / Q_Delay * Time.deltaTime;
            if(Q_Skill_Image.fillAmount <= 0)
            {
                Q_Skill_Image.fillAmount = 0;
            }
        }
    }

    void W_Skill()
    {
        if (W_Press)
        {
            W_Press = false;
            W_Skill_Image.fillAmount = 1;
        }
        if (W_Press == false)
        {
            W_Skill_Image.fillAmount -= 1 / W_Delay * Time.deltaTime;
            if (W_Skill_Image.fillAmount <= 0)
            {
                W_Skill_Image.fillAmount = 0;
            }
        }
    }
    void E_Skill()
    {
        if (E_Press)
        {
            E_Press = false;
            E_Skill_Image.fillAmount = 1;
        }
        if (E_Press == false)
        {
            E_Skill_Image.fillAmount -= 1 / E_Delay * Time.deltaTime;
            if (E_Skill_Image.fillAmount <= 0)
            {
                E_Skill_Image.fillAmount = 0;
            }
        }
    }
    void R_Skill()
    {
        if (R_Press)
        {
            R_Press = false;
            R_Skill_Image.fillAmount = 1;
        }
        if (R_Press == false)
        {
            R_Skill_Image.fillAmount -= 1 / R_Delay * Time.deltaTime;
            if (R_Skill_Image.fillAmount <= 0)
            {
                R_Skill_Image.fillAmount = 0;
            }
        }
    }

    void KZ_UI()
    {
        if (KZombie_num < 10)
            KZombie_text.text = "" + KZombie_num;
        else
        {
            KZombie_text.text = "";
            Mari_text.text = "";
            Kill_Success.text = "목표달성!!";
        }
    }
}
