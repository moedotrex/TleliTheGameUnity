using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LightCombo : MonoBehaviour
{
    List<string> animList = new List<string>(new string[] { "LAttack_1", "LAttack_2", "LAttack_3", "LAttack_4" });
    Animator animator;
    int combonum;
    float reset;
    float intResetTime;
    public float resetTime;

    public float attackRate;
    float nextAttackTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && combonum < 3)
        {
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate;
                animator.SetTrigger(animList[combonum]);
                combonum++;
                reset = 0f;
                Debug.Log(combonum);
            }
        }
        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > intResetTime)
            {
                animator.SetTrigger("Reset_LightCombo");
                combonum = 0;
                Debug.Log("combo reset");
            }
        }
        if (combonum == 4)
        {
            intResetTime = 4f;
            combonum = 0;
        }
        else
        {
            intResetTime = resetTime;
        }



    }
}