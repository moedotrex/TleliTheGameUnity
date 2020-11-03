using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyComboScript : MonoBehaviour
{
    List<string> animList = new List<string>(new string[] { "HAttack 1", "HAttack 2"});
    public Animator animator;
    public int combonum;
    public float reset;
    public float resettime;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") && combonum < 2)
            {
                animator.SetTrigger(animList[combonum]);
                combonum++;
                reset = 0f;
            }
            if (combonum > 0)
            {
                reset += Time.deltaTime;
                if (reset > resettime)
                {
                    animator.SetTrigger("Reset");
                    combonum = 0;
                }
            }
            if (combonum == 4)
            {
                resettime = 4f;
                combonum = 0;
            }
            else
            {
                resettime = 1f;
            }
            nextAttackTime = Time.time + 2f / attackRate;
        }
        
    }
}
